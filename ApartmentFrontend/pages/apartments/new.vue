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
  fetch({ store }) {
    store.commit('apartments/set', {});
  }
};
</script>
