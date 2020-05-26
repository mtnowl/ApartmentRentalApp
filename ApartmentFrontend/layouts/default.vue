<template>
  <v-app>
    <v-app-bar fixed app>
      <v-tabs>
        <v-tab to="/apartments">Apartments</v-tab>
        <v-spacer />
        <v-tab disabled> Hello {{ userName }}! </v-tab>
        <v-spacer />
        <v-tab v-if="loggedIn" to="/logout">Logout</v-tab>
        <v-tab v-else to="/login">Login</v-tab>
      </v-tabs>
    </v-app-bar>
    <v-content>
      <v-container>
        <nuxt />
      </v-container>
    </v-content>
    <v-footer app>
      <span>&copy; Henry Jin {{ new Date().getFullYear() }}</span>
    </v-footer>
  </v-app>
</template>

<script>
export default {
  computed: {
    loggedIn() {
      return this.$store.state.auth.loggedIn;
    },
    userName() {
      return this.$store.state.auth.user.username;
    }
  }
};
</script>
