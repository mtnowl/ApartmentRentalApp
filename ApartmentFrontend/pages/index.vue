<template>
  <div>
    Welcome to the home page.
  </div>
</template>

<script>
export default {
  middleware: 'auth',
  created() {
    this.$router.push('/apartments');
  }
};
</script>
