<template>
  <v-form ref="form" v-model="valid" @submit.prevent="submit">
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
            :rules="[rules.required, rules.number, rules.integer]"
            required
          ></v-text-field>
          <v-text-field
            v-model="apartment.area"
            label="Area"
            :rules="[rules.required, rules.number]"
            required
          ></v-text-field>
          <v-text-field
            v-model="apartment.monthlyPrice"
            label="Price"
            :rules="[rules.required, rules.number]"
            required
          ></v-text-field>
          <v-text-field
            v-model="apartment.latitude"
            label="Latitude"
            :rules="[rules.required, rules.number]"
            required
          ></v-text-field>
          <v-text-field
            v-model="apartment.longitude"
            label="Longitude"
            :rules="[rules.required, rules.number]"
            required
          ></v-text-field>
          <v-text-field
            v-model="apartment.realtorUserId"
            label="Assigned Realtor"
            :rules="[rules.required, rules.number, rules.integer]"
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
    <v-btn :disabled="!valid" type="submit">{{
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
      number: (v) => !isNaN(v) || 'Must be a number',
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
      if (!this.$refs.form.validate()) {
        return;
      }

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
