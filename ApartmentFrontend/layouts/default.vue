<template>
  <v-app>
    <v-app-bar fixed app>
      <v-tabs>
        <v-tab v-if="loggedIn" to="/apartments">Apartments</v-tab>
        <v-tab v-else disabled>Welcome!</v-tab>
        <v-tab v-if="isAdmin" to="/users">Users</v-tab>
        <v-spacer />
        <v-tab v-if="loggedIn && $vuetify.breakpoint.smAndUp" disabled>
          Hello {{ userName }}!
        </v-tab>
        <v-spacer />
        <v-tab v-if="loggedIn" to="/logout">Logout</v-tab>
        <v-tab v-else to="/login">Login</v-tab>
      </v-tabs>
    </v-app-bar>
    <v-content>
      <nuxt />
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
      return this.$auth.loggedIn;
    },
    userName() {
      return !this.$auth.user ? '' : this.$auth.user.username;
    },
    isAdmin() {
      return this.$auth.user && this.$auth.user.role === 'Admin';
    }
  }
};
</script>

<style>
a {
  text-decoration: none;
}
</style>
