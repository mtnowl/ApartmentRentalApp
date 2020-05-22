import merge from 'lodash/merge';
import assign from 'lodash/assign';

export const state = () => ({
  list: [],
  apartment: {}
});

export const mutations = {
  set(state, apt) {
    state.list = apt;
  },
  add(state, value) {
    merge(state.list, value);
  },
  remove(state, { apt }) {
    state.list.filter((c) => apt.id !== c.id);
  },
  mergeapts(state, form) {
    assign(state.apartment, form);
  },
  setapts(state, form) {
    state.apartment = form;
  }
};

export const actions = {
  async get({ commit }, params) {
    await this.$axios.get(`apartment`, { params }).then((res) => {
      if (res.status === 200) {
        commit('set', res.data);
      }
    });
  },
  async show({ commit }, params) {
    if (params.id === -1) {
      commit('setapts', {});
    } else {
      await this.$axios.get(`/apartment/${params.id}`).then((res) => {
        if (res.status === 200) {
          commit('mergeapts', res.data);
        }
      });
    }
  },
  async set({ commit }, apts) {
    await commit('set', apts);
  },
  async form({ commit }, form) {
    await commit('mergeapts', form);
  },
  async add({ commit }, apt) {
    await commit('add', apt);
  },
  create({ commit }, params) {
    return this.$axios.post(`/apartment`, { ...params });
  },
  update({ commit }, params) {
    return this.$axios.put(`/apartment/${params.id}`, { ...params });
  },
  delete({ commit }, params) {
    return this.$axios.delete(`/apartment/${params.id}`);
  }
};
