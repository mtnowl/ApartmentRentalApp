<template>
  <div>
    <div>
      <label>Username</label>
      <input v-model="login.username" type="text" />
    </div>
    <div>
      <label>Password</label>
      <input v-model="login.password" type="text" />
    </div>
    <div>
      <button @click="userLogin">Submit</button>
      <button @click="logout">Logout</button>
      <button @click="getApartments">Apartments</button>
    </div>
    <p>{{ errorMessage }}</p>
  </div>
</template>

<script>
export default {
  data() {
    return {
      login: {
        username: '',
        password: ''
      },
      errorMessage: ''
    };
  },
  methods: {
    async userLogin() {
      try {
        await this.$auth.loginWith('local', {
          data: this.login
        });
        this.$router.push({
          path: '/apartments'
        });
      } catch (err) {
        this.errorMessage = 'Login failed: ' + err;
      }
    },
    async logout() {
      await this.$auth.logout();
    },
    async getApartments() {
      await this.$axios.$get('apartment');
    }
  }
};
</script>
