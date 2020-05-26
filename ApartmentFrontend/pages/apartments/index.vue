<template>
  <v-container>
    <v-row>
      <v-col>
        <v-btn :to="'/apartments/new'" color="primary">
          Add New Apartment
        </v-btn>
        <v-btn :to="'/apartments/map'">
          View Map
        </v-btn>
      </v-col>
    </v-row>
    <v-row>
      <v-form v-model="valid" @submit.prevent="refreshDataWithFilter">
        <v-col>
          <v-text-field
            v-model="filter.minArea"
            label="Min Size"
            :rules="[rules.number]"
            required
          ></v-text-field>
          <v-text-field
            v-model="filter.maxArea"
            label="Max Size"
            :rules="[rules.number]"
            required
          ></v-text-field>
          <v-text-field
            v-model="filter.minPrice"
            label="Min Price"
            :rules="[rules.number]"
            required
          ></v-text-field>
          <v-text-field
            v-model="filter.maxPrice"
            label="Max Price"
            :rules="[rules.number]"
            required
          ></v-text-field>
          <v-text-field
            v-model="filter.minRooms"
            label="Min # of Rooms"
            :rules="[rules.number, rules.integer]"
            required
          ></v-text-field>
          <v-text-field
            v-model="filter.maxRooms"
            label="Max # of Rooms"
            :rules="[rules.number, rules.integer]"
            required
          ></v-text-field>
          <v-btn type="submit" :disabled="!valid">Filter</v-btn>
          <v-btn @click="clearFilter">Clear</v-btn>
        </v-col>
      </v-form>
    </v-row>
    <v-row>
      <v-col>
        <v-data-table :headers="headers" :items="list">
          <template v-slot:item.name="{ item }">
            <n-link v-if="canEdit" :to="`/apartments/${item.id}/edit`">
              {{ item.name }}
            </n-link>
            <div v-else>
              {{ item.name }}
            </div>
          </template>
          <template v-slot:item.isRented="{ item }">
            <v-simple-checkbox
              v-model="item.isRented"
              disabled
            ></v-simple-checkbox>
          </template>
          <template v-slot:item.actions="{}">
            <v-icon small class="mr-2">
              mdi-pencil
            </v-icon>
          </template>
          <!--
        

          <v-btn
            v-v-modal.confirmDestroy
            variant="outline-secondary"
            @click="id = data.item.id"
          >
            {{ 'remove' }}
          </v-btn>
        </template> -->
        </v-data-table>
        <v-dialog
          id="confirmDestroy"
          :title="'destroy_confirm_title'"
          @ok="destroy"
        >
          {{ 'destroy_confirm' }}
        </v-dialog>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { mapState } from 'vuex';

export default {
  middleware: 'authy',
  meta: {
    auth: { authority: 0 }
  },
  async fetch({ store }) {
    await store.dispatch('apartments/get');
  },
  data() {
    return {
      headers: [
        { text: 'Name', value: 'name' },
        { text: 'Description', value: 'description' },
        { text: 'Rooms', value: 'rooms' },
        { text: 'Size', value: 'area' },
        { text: 'Price', value: 'monthlyPrice' },
        { text: 'Occupied', value: 'isRented' }
      ],
      fields: [
        {
          key: 'name',
          label: 'name',
          sortable: true
        },
        {
          key: 'updated_at',
          sortable: true,
          label: 'updated_at'
        },
        {
          key: 'actions',
          label: 'actions'
        }
      ],
      valid: true,
      filter: {
        minArea: null,
        maxArea: null,
        minPrice: null,
        maxPrice: null,
        minRooms: null,
        maxRooms: null
      },
      rules: {
        number: (v) => {
          console.log(v);
          return v === null || v === '' || !isNaN(v) || 'Must be a number';
        },
        integer: (v) => Number.isInteger(+v) || 'Must be an integer'
      }
    };
  },
  computed: {
    ...mapState('apartments', {
      list: (state) => {
        return state.list;
      }
    }),
    canEdit() {
      return (
        this.$auth.user.role === 'Admin' || this.$auth.user.role === 'Realtor'
      );
    }
  },
  methods: {
    destroy() {
      this.$store
        .dispatch('apartments/delete', { id: this.id })
        .then(() => this.$store.dispatch('apartments/get'));
    },
    refreshDataWithFilter() {
      this.$store.dispatch('apartments/get', { ...this.filter });
    },
    clearFilter() {
      this.filter = {};
    }
  }
};
</script>
