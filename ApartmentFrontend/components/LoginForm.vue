<template>
  <v-container>
    <v-row align="center" justify="center">
      <v-col cols="12" sm="8" md="6">
        <v-card>
          <v-toolbar color="primary" dark flat>
            <v-toolbar-title>{{
              isSignup ? 'Sign Up' : 'Login'
            }}</v-toolbar-title>
          </v-toolbar>
          <v-card-text>
            <v-form ref="form" v-model="valid" @submit.prevent="submit">
              <v-text-field
                v-model="login.username"
                label="Username"
                :prepend-icon="icons.person"
                :rules="[rules.required]"
                type="text"
                required
              ></v-text-field>
              <v-text-field
                v-model="login.password"
                label="Password"
                :prepend-icon="icons.password"
                :rules="[rules.required]"
                type="password"
                required
              ></v-text-field>
              <v-text-field
                v-if="isSignup"
                v-model="login.confirmPassword"
                label="Confirm Password"
                :prepend-icon="icons.password"
                :rules="[rules.required, rules.confirmPassword]"
                type="password"
                required
              ></v-text-field>
              <v-card-actions>
                <v-btn v-if="!isSignup" to="/signup" nuxt>
                  Need to signup?
                </v-btn>
                <v-spacer />
                <v-btn
                  type="submit"
                  :disabled="!valid || isSubmitting"
                  color="primary"
                  >Submit
                </v-btn>
              </v-card-actions>
              <p>{{ message }}</p>
            </v-form>
          </v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { mdiAccount, mdiLock } from '@mdi/js';

export default {
  name: 'LoginForm',
  props: {
    isSignup: {
      type: Boolean
    }
  },
  data() {
    return {
      valid: false,
      login: {
        username: '',
        password: '',
        confirmPassword: ''
      },
      isSubmitting: false,
      message: '',
      rules: {
        required: (v) => !!v || 'Required',
        confirmPassword: (v) =>
          v === this.login.password || 'Passwords must match'
      },
      icons: {
        person: mdiAccount,
        password: mdiLock
      }
    };
  },
  created() {
    if (this.$auth.loggedIn) {
      this.$router.push('/apartments');
    }
  },
  methods: {
    async submit() {
      if (!this.$refs.form.validate()) {
        return;
      }

      this.isSubmitting = true;
      try {
        if (this.isSignup) {
          await this.userSignup();
        } else {
          await this.userLogin();
        }
      } catch (err) {
        if (this.isSignup) {
          this.message = 'Signup failed: ' + err.response.data.message;
        } else {
          this.message = 'Login failed: ' + err;
        }
      } finally {
        this.isSubmitting = false;
      }
    },
    async userLogin() {
      await this.$auth.loginWith('local', {
        data: this.login
      });
      this.$router.push({
        path: '/apartments'
      });
    },
    async userSignup() {
      const user = {
        username: this.login.username,
        password: this.login.password
      };
      const response = await this.$axios.post(`/user/signup`, { ...user });

      if (response.status === 201) {
        this.message = 'Signup succeeded, redirecting to login...';
        setTimeout(() => this.$router.push('/login'), 1000);
      } else {
        this.message = 'Signup failed: ' + response.statusText;
      }
    }
  }
};
</script>
