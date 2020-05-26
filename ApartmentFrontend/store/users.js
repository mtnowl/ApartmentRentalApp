import merge from 'lodash/merge';
import assign from 'lodash/assign';

export const state = () => ({
  list: [],
  user: {}
});

export const mutations = {
  set(state, user) {
    state.list = user;
  },
  add(state, value) {
    merge(state.list, value);
  },
  remove(state, { user }) {
    state.list.filter((c) => user.id !== c.id);
  },
  merge(state, form) {
    state.user.password = '';
    assign(state.user, form);
  },
  setuser(state, form) {
    state.user = form;
  }
};

export const actions = {
  async get({ commit }, params) {
    await this.$axios.get(`user`, { params }).then((res) => {
      if (res.status === 200) {
        commit('set', res.data);
      }
    });
  },
  async show({ commit }, params) {
    if (params.id === -1) {
      commit('setuser', {});
    } else {
      await this.$axios.get(`/user/${params.id}`).then((res) => {
        if (res.status === 200) {
          commit('merge', res.data);
        }
      });
    }
  },
  async set({ commit }, users) {
    await commit('set', users);
  },
  async form({ commit }, form) {
    await commit('merge', form);
  },
  async add({ commit }, user) {
    await commit('add', user);
  },
  create({ commit }, params) {
    return this.$axios.post(`/user`, { ...params });
  },
  update({ commit }, params) {
    return this.$axios.put(`/user/${params.id}`, { ...params });
  },
  delete({ commit }, params) {
    return this.$axios.delete(`/user/${params.id}`);
  }
};
