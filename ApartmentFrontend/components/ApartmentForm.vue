<template>
  <v-container>
    <v-form ref="form" v-model="valid" @submit.prevent="submit">
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
          <v-select
            v-model="apartment.realtorUserId"
            label="Assigned Realtor"
            :items="realtors"
            item-text="username"
            item-value="id"
            :rules="[rules.required]"
            required
          ></v-select>
          <v-checkbox
            v-model="apartment.isRented"
            label="Occupied"
            required
          ></v-checkbox>
        </v-col>
      </v-row>
      <v-btn @click="$router.go(-1)">
        Cancel
      </v-btn>
      <v-btn :disabled="!valid" type="submit" color="primary">{{
        isUpdate() ? 'Update' : 'Submit'
      }}</v-btn>
      <p>{{ errorMessage }}</p>
    </v-form>
  </v-container>
</template>
<script>
import { mapState } from 'vuex';

export default {
  name: 'ApartmentForm',
  async fetch() {
    const response = await this.$axios.get('user/realtor');
    this.realtors = response.data;
  },
  data: () => ({
    valid: false,
    errorMessage: '',
    rules: {
      required: (v) => !!v || 'Required',
      number: (v) => !isNaN(v) || 'Must be a number',
      integer: (v) => Number.isInteger(+v) || 'Must be an integer'
    },
    realtors: []
  }),
  computed: {
    ...mapState('apartments', ['apartment'])
  },
  methods: {
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
