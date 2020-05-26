<template>
  <div>
    <login-form></login-form>
    <n-link to="/signup">
      <v-btn>Need to signup?</v-btn>
    </n-link>
  </div>
</template>

<script>
import LoginForm from '~/components/LoginForm';

export default {
  components: { LoginForm }
};
</script>
