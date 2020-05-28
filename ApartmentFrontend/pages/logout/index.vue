<template>
  <v-container>
    <v-row>
      <v-col>
        <h2>Logging Out...</h2>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
export default {
  async created() {
    await this.$auth.logout();
    this.$router.push('/');
  }
};
</script>
