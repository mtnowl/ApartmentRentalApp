<template>
  <v-container>
    <v-row>
      <v-col class="text-right">
        <v-btn to="/users/new" nuxt color="primary">
          Add New User
        </v-btn>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-data-table :headers="headers" :items="list">
          <template v-slot:item.username="{ item }">
            <n-link :to="`/users/${item.id}/edit`">
              {{ item.username }}
            </n-link>
          </template>
          <template v-slot:item.actions="{ item }">
            <n-link :to="`/users/${item.id}/edit`">
              <v-icon small>
                mdi-pencil
              </v-icon>
            </n-link>
            <v-icon
              v-if="$auth.user.id !== item.id"
              small
              @click="setupDeleteDialog(item)"
            >
              mdi-delete
            </v-icon>
          </template>
        </v-data-table>
        <v-dialog v-model="deleteDialog" max-width="290">
          <v-card>
            <v-card-title>Delete {{ itemToDelete.username }}? </v-card-title>
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
    auth: { authority: 2 }
  },
  async fetch({ store }) {
    await store.dispatch('users/get');
  },
  data() {
    return {
      headers: [
        { text: 'Username', value: 'username' },
        { text: 'Role', value: 'role' },
        { text: 'Actions', value: 'actions', sortable: false }
      ],
      valid: true,
      itemToDelete: {},
      deleteDialog: false
    };
  },
  computed: {
    ...mapState('users', {
      list: (state) => {
        return state.list;
      }
    })
  },
  methods: {
    setupDeleteDialog(item) {
      this.itemToDelete = item;
      this.deleteDialog = true;
    },
    deleteItem() {
      this.$store
        .dispatch('users/delete', { id: this.itemToDelete.id })
        .then(() => this.$store.dispatch('users/get'));
      this.deleteDialog = false;
    },
    refreshDataWithFilter() {
      this.$store.dispatch('users/get', { ...this.filter });
    },
    clearFilter() {
      this.filter = {};
    }
  }
};
</script>
