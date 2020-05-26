<template>
  <v-card>
    <v-toolbar>
      <v-toolbar-title>Login</v-toolbar-title>
    </v-toolbar>
    <v-form v-model="valid" @submit.prevent="userLogin">
      <v-text-field
        v-model="login.username"
        label="Username"
        prepend-icon="person"
        :rules="[rules.required]"
        type="text"
        required
      ></v-text-field>
      <v-text-field
        v-model="login.password"
        label="Password"
        prepend-icon="lock"
        :rules="[rules.required]"
        type="password"
        required
      ></v-text-field>
    </v-form>
    <v-card-actions>
      <v-btn type="submit" color="primary">Submit</v-btn>
      <p>{{ errorMessage }}</p>
    </v-card-actions>
  </v-card>
</template>

<script>
export default {
  data() {
    return {
      login: {
        username: '',
        password: ''
      },
      errorMessage: '',
      rules: {
        required: (v) => !!v || 'Required'
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
