export const state = () => ({
  counter: 0
});
export const strict = false;
export const mutations = {
  increment(state) {
    state.counter++;
  }
};
