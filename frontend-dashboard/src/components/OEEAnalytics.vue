<template>
  <div class="oee-analytics p-4 font-inter">
    <div class="overflow-x-auto ">
      <div class="tabs tabs-border min-w-fit justify-center">
        <input type="radio" name="my_tabs_7" class="tab z-1" :aria-label="$t('OEE Trend')"
          :checked="activeTab === 'oee'" @change="activeTab ='oee'" />
        <div class="sticky start-0 tab-content bg-base-100 p-2" v-if="activeTab === 'oee'">
          <OEETrend />
        </div>
        <input type="radio" name="my_tabs_7" class="tab z-1" :aria-label="$t('OEE Trend (Import)')"
          :checked="activeTab === 'oee_import'" @change="activeTab ='oee_import'" />
        <div class="sticky start-0 tab-content bg-base-100 p-2" v-if="activeTab === 'oee_import'">
          <OEETrendImport />
        </div>
        <input type="radio" name="my_tabs_7" class="tab z-1" :aria-label="$t('Machine Timeline')"
          :checked="activeTab === 'timeline'" @change="activeTab ='timeline'" />
        <div class="sticky start-0 tab-content bg-base-100 p-6" v-if="activeTab === 'timeline'">
          <MachineTimeline />
        </div>
        <input type="radio" name="my_tabs_7" class="tab z-1" :aria-label="$t('Comprehensive Productivity (TEEP)')"
          :checked="activeTab === 'teep'" @change="activeTab ='teep'" />
        <div class="sticky start-0 tab-content bg-base-100 p-6" v-if="activeTab === 'teep'">
          <TEEP />
        </div>
      </div>
    </div>
  </div>
</template>
<script>
  import OEETrend from './OEETrend.vue'
  import OEETrendImport from './OEETrendImport.vue'
  import MachineTimeline from './MachineTimeline.vue'
  import TEEP from './TEEP.vue'

  export default {
    components: {
      OEETrend,
      OEETrendImport,
      MachineTimeline,
      TEEP,
    },
    data() {
      return {
        activeTab: 'oee'
      }
    }
  }
</script>
<style scoped>

  html[data-theme='light'] .bg-base-100 {
    background-color: #eee;
  }
</style>