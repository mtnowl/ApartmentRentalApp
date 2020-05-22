<template>
  <v-form v-model="valid" @submit.prevent="submit">
    <v-container>
      <v-row>
        <v-col>
          <v-text-field
            v-model="apartment.name"
            label="Name"
            :rules="[rules.required]"
            required
          ></v-text-field>
          <v-text-field
            v-model="apartment.description"
            label="Description"
            :rules="[rules.required]"
            required
          ></v-text-field>
          <v-text-field
            v-model="apartment.rooms"
            label="Rooms"
            type="number"
            :rules="[rules.number, rules.integer]"
            required
          ></v-text-field>
          <v-text-field
            v-model="apartment.area"
            label="Area"
            type="number"
            :rules="[rules.number]"
            required
          ></v-text-field>
          <v-text-field
            v-model="apartment.monthlyPrice"
            label="Price"
            type="number"
            :rules="[rules.number]"
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
    <v-btn :disabled="!valid" class="my-3" type="submit">{{
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
    errorMessage: '',
    rules: {
      required: (v) => !!v || 'Required',
      number: (v) => !isNaN(parseFloat(v)) || 'Must be a number',
      integer: (v) => Number.isInteger(+v) || 'Must be an integer'
    }
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
