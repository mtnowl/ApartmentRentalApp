<template>
  <v-container>
    <v-row>
      <v-col>
        <v-btn :to="'/users/new'" color="primary">
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
    auth: { authority: 2 }
  },
  async fetch({ store }) {
    await store.dispatch('users/get');
  },
  data() {
    return {
      headers: [
        { text: 'username', value: 'username' },
        { text: 'role', value: 'role' }
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
          return v === null || v === '' || !isNaN(v) || 'Must be a number';
        },
        integer: (v) => Number.isInteger(+v) || 'Must be an integer'
      }
    };
  },
  computed: {
    ...mapState('users', {
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
        .dispatch('users/delete', { id: this.id })
        .then(() => this.$store.dispatch('users/get'));
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
