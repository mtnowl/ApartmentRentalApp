<template>
  <div>
    <GMap
      ref="gMap"
      language="en"
      :center="{ lat: list[0].latitude, lng: list[0].longitude }"
      :options="{ fullscreenControl: false }"
      :zoom="4"
    >
      <GMapMarker
        v-for="apt in list"
        :key="apt.id"
        :position="{ lat: apt.latitude, lng: apt.longitude }"
      >
        <GMapInfoWindow>
          <p>
            <strong>{{ apt.name }}</strong>
          </p>
          <p>
            {{ apt.description }}
          </p>
        </GMapInfoWindow>
      </GMapMarker>
    </GMap>
  </div>
</template>

<script>
import { mapState } from 'vuex';

export default {
  async fetch({ store }) {
    await store.dispatch('apartments/get');
  },

  computed: {
    ...mapState('apartments', {
      list: (state) => {
        return state.list;
      }
    })
  }
};
</script>
