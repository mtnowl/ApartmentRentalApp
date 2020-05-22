<template>
  <apartment-form></apartment-form>
</template>

<script>
import ApartmentForm from '~/components/ApartmentForm';

export default {
  name: 'EditApartment',
  middleware: 'authy',
  meta: {
    auth: { authority: 1 }
  },
  components: { ApartmentForm },
  async fetch({ store, params }) {
    await store.dispatch('apartments/show', { id: params.apartment_id });
  }
};
</script>
