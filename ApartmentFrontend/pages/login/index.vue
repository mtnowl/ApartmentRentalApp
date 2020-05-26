<template>
  <v-row align="center" justify="center">
    <v-col cols="12" sm="8" md="4">
      <v-card>
        <v-toolbar color="primary" dark flat>
          <v-toolbar-title>Login</v-toolbar-title>
        </v-toolbar>
        <v-form v-model="valid" @submit.prevent="userLogin">
          <v-text-field
            v-model="login.username"
            label="Username"
            :prepend-icon="icons.person"
            :rules="[rules.required]"
            type="text"
            required
          ></v-text-field>
          <v-text-field
            v-model="login.password"
            label="Password"
            :prepend-icon="icons.password"
            :rules="[rules.required]"
            type="password"
            required
          ></v-text-field>
          <v-card-actions>
            <v-btn type="submit" :disabled="!valid" color="primary"
              >Submit</v-btn
            >
            <p>{{ errorMessage }}</p>
          </v-card-actions>
        </v-form>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
import { mdiAccount, mdiLock } from '@mdi/js';

export default {
  data() {
    return {
      valid: false,
      login: {
        username: '',
        password: ''
      },
      errorMessage: '',
      rules: {
        required: (v) => !!v || 'Required'
      },
      icons: {
        person: mdiAccount,
        password: mdiLock
      }
    };
  },
  methods: {
    async userLogin() {
      try {
        await this.$auth.loginWith('local', {
          data: this.login
        });
        this.$router.push({
          path: '/apartments'
        });
      } catch (err) {
        this.errorMessage = 'Login failed: ' + err;
      }
    },
    async logout() {
      await this.$auth.logout();
    },
    async getApartments() {
      await this.$axios.$get('apartment');
    }
  }
};
</script>
