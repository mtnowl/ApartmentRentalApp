<template>
  <div>Logging Out...</div>
</template>

<script>
export default {
  async created() {
    await this.$auth.logout();
    this.$router.push('/');
  }
};
</script>
