<template>
  <v-form ref="form" v-model="valid" @submit.prevent="submit">
    <v-container>
      <v-row>
        <v-col>
          <v-text-field
            v-model="user.name"
            label="Name"
            :rules="[rules.required]"
            required
          ></v-text-field>
          <v-text-field
            v-model="user.description"
            label="Description"
            :rules="[rules.required]"
            required
          ></v-text-field>
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
import { mapState } from 'vuex';

export default {
  name: 'UserForm',
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
    ...mapState('users', ['user'])
  },
  methods: {
    isUpdate() {
      return Object.prototype.hasOwnProperty.call(this.user, 'id');
    },
    submit() {
      if (!this.$refs.form.validate()) {
        return;
      }

      const action = 'users/' + (this.isUpdate() ? 'update' : 'create');

      this.$store
        .dispatch(action, this.user)
        .then((resp) => {
          this.$router.push('/users');
        })
        .catch((e) => {
          this.errorMessage = e;
        });
    }
  }
};
</script>
