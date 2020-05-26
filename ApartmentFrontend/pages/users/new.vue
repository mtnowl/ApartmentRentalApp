<template>
  <user-form></user-form>
</template>

<script>
import UserForm from '~/components/UserForm';
export default {
  middleware: 'authy',
  meta: {
    auth: { authority: 2 }
  },
  components: { UserForm },
  async fetch({ store, params }) {
    await store.dispatch('users/show', { id: -1 });
  }
};
</script>
