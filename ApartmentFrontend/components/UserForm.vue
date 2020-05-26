<template>
  <v-form ref="form" v-model="valid" @submit.prevent="submit">
    <v-container>
      <v-row>
        <v-col>
          <v-text-field
            v-model="user.username"
            label="Username"
            :rules="[rules.required]"
            required
          ></v-text-field>
          <v-text-field
            v-model="user.password"
            label="Password"
            type="password"
            :rules="[rules.required]"
            required
          ></v-text-field>
          <v-select
            v-model="user.role"
            :items="['Admin', 'Realtor', 'Client']"
            label="Role"
            :rules="[rules.required]"
            required
          ></v-select>
        </v-col>
      </v-row>
    </v-container>
    <v-btn :disabled="!valid" type="submit" color="primary">{{
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
      required: (v) => !!v || 'Required'
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
