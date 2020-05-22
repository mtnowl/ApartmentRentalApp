<template>
  <v-container>
    <v-row>
      <v-col>
        <v-btn variant="outline-success" class="mv-3" :to="'/apartments/new'">
          Add New Apartment
        </v-btn>
      </v-col>
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
        { text: 'Area', value: 'area' },
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
      ]
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
    }
  }
};
</script>
