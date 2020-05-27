<template>
  <v-container>
    <v-row>
      <v-col>
        <v-btn to="/apartments/map" nuxt>
          View Map
        </v-btn>
      </v-col>
      <v-col class="text-right">
        <v-btn v-if="canEdit" to="/apartments/new" nuxt color="primary">
          Add New Apartment
        </v-btn>
      </v-col>
    </v-row>
    <v-form v-model="valid" @submit.prevent="refreshDataWithFilter">
      <v-container>
        <v-expansion-panels>
          <v-expansion-panel>
            <v-expansion-panel-header>Filters</v-expansion-panel-header>
            <v-expansion-panel-content>
              <v-row>
                <v-col cols="12" sm="6" md="4">
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
                </v-col>
                <v-col cols="12" sm="6" md="4">
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
                </v-col>
                <v-col cols="12" sm="6" md="4">
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
                </v-col>
                <v-col cols="12" sm="6" md="12" class="text-right">
                  <v-btn @click="clearFilter">Clear</v-btn>
                  <v-btn type="submit" :disabled="!valid" color="primary">
                    Filter
                  </v-btn>
                </v-col>
              </v-row>
            </v-expansion-panel-content>
          </v-expansion-panel>
        </v-expansion-panels>
      </v-container>
    </v-form>
    <v-row>
      <v-col>
        <v-data-table
          :headers="headers"
          :items="list"
          :sort-by="['dateAdded']"
          :sort-desc="[true]"
        >
          <template v-slot:item.name="{ item }">
            <n-link v-if="canEdit" :to="`/apartments/${item.id}/edit`">
              {{ item.name }}
            </n-link>
            <div v-else>
              {{ item.name }}
            </div>
          </template>
          <template v-slot:item.dateAdded="{ item }">
            {{ $moment(item.dateAdded).format('YYYY/MM/DD') }}
          </template>
          <template v-if="canEdit" v-slot:item.isRented="{ item }">
            <v-simple-checkbox
              v-model="item.isRented"
              disabled
            ></v-simple-checkbox>
          </template>
          <template v-if="canEdit" v-slot:item.actions="{ item }">
            <n-link :to="`/apartments/${item.id}/edit`">
              <v-icon small>
                mdi-pencil
              </v-icon>
            </n-link>
            <v-icon small @click="setupDeleteDialog(item)">
              mdi-delete
            </v-icon>
          </template>
        </v-data-table>
        <v-dialog v-if="canEdit" v-model="deleteDialog" max-width="290">
          <v-card>
            <v-card-title>Delete {{ itemToDelete.name }}? </v-card-title>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="primary" text @click="deleteDialog = false">
                No
              </v-btn>
              <v-btn color="error" text @click="deleteItem">
                Yes
              </v-btn>
            </v-card-actions>
          </v-card>
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
    const headers = [
      { text: 'Name', value: 'name' },
      { text: 'Description', value: 'description' },
      { text: 'Rooms', value: 'rooms' },
      { text: 'Size', value: 'area' },
      { text: 'Price', value: 'monthlyPrice' },
      { text: 'Date Added', value: 'dateAdded' }
    ];

    return {
      headers,
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
          return v === null || v === '' || !isNaN(v) || 'Must be a number';
        },
        integer: (v) => Number.isInteger(+v) || 'Must be an integer'
      },
      itemToDelete: {},
      deleteDialog: false
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
  created() {
    if (this.canEdit) {
      this.headers.push(
        { text: 'Occupied?', value: 'isRented' },
        { text: 'Actions', value: 'actions', sortable: false }
      );
    }
  },
  methods: {
    setupDeleteDialog(item) {
      this.itemToDelete = item;
      this.deleteDialog = true;
    },
    deleteItem() {
      this.$store
        .dispatch('apartments/delete', { id: this.itemToDelete.id })
        .then(() => this.$store.dispatch('apartments/get'));
      this.deleteDialog = false;
    },
    refreshDataWithFilter() {
      this.$store.dispatch('apartments/get', { ...this.filter });
    },
    clearFilter() {
      Object.keys(this.filter).forEach((p) => (this.filter[p] = null));
      this.refreshDataWithFilter();
    }
  }
};
</script>

<style>
/* fix how the headers don't align properly, 
   awaiting bug fix: https://github.com/vuetifyjs/vuetify/issues/10164 */
.v-data-table-header th {
  white-space: nowrap;
}
</style>
