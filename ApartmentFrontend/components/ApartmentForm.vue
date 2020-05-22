<template>
  <v-form v-model="valid" @submit.prevent="submit">
    <v-container>
      <v-row>
        <v-col>
          <v-text-field
            v-model="apartment.name"
            label="Name"
            required
          ></v-text-field>
          <v-text-field
            v-model="apartment.description"
            label="Description"
            required
          ></v-text-field>
          <v-text-field
            v-model="apartment.rooms"
            label="Rooms"
            type="number"
            required
          ></v-text-field>
          <v-text-field
            v-model="apartment.area"
            label="Area"
            type="number"
            required
          ></v-text-field>
          <v-text-field
            v-model="apartment.monthlyPrice"
            label="Price"
            type="number"
            required
          ></v-text-field>
          <v-checkbox
            v-model="apartment.isRented"
            label="Occupied"
            required
          ></v-checkbox>
        </v-col>
      </v-row>
    </v-container>
    <v-btn class="my-3" type="submit">{{
      isUpdate() ? 'Update' : 'Submit'
    }}</v-btn>
    <p>{{ errorMessage }}</p>
  </v-form>
</template>
<script>
import { mapMutations, mapState } from 'vuex';
export default {
  name: 'ApartmentForm',
  data: () => ({
    valid: false,
    errorMessage: ''
  }),
  computed: {
    ...mapState('apartments', ['apartment'])
  },
  methods: {
    ...mapMutations({ mergeApartment: 'apartments/mergeapts' }),
    isUpdate() {
      return Object.prototype.hasOwnProperty.call(this.apartment, 'id');
    },
    submit() {
      const action = 'apartments/' + (this.isUpdate() ? 'update' : 'create');

      this.$store
        .dispatch(action, this.apartment)
        .then((resp) => {
          this.$router.push('/apartments');
        })
        .catch((e) => {
          this.errorMessage = e;
        });
    }
  }
};
</script>
