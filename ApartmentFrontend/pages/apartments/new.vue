<template>
  <apartment-form></apartment-form>
</template>

<script>
import ApartmentForm from '~/components/ApartmentForm';
export default {
  middleware: 'authy',
  meta: {
    auth: { authority: 1 }
  },
  components: { ApartmentForm },
  async fetch({ store, params }) {
    await store.dispatch('apartments/show', { id: -1 });
  }
};
</script>
