<template>
  <div class="real-time-monitoring font-inter">
     <transition name="fade">
      <div v-if="showAlert" role="alert" class="absolute z-10 bottom-20 right-4 alert alert-warning alert-soft transition duration-700 ease-in-out">
            <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 shrink-0 stroke-current " fill="none"
                viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                    d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z" />
            </svg>
        <span>Sample data – actual data not loaded yet.</span>
        </div> 
     </transition>
    
    <div class="tabs tabs-border justify-center">
      <input type="radio" name="my_tabs_2" class="tab" :aria-label="$t('Efficiency Overview')"
        @click="navigate('EfficiencyOverview', 'Efficiency Overview')" checked/>

      <input type="radio" name="my_tabs_2" class="tab" :aria-label="$t('Status Monitoring')"
        @click="navigate('StatusMonitoring', 'Status Monitoring')" />

      <input type="radio" name="my_tabs_2" class="tab" :aria-label="$t('Abnormality on Duty')"
        @click="navigate('AbnormalityonDuty', 'Abnormality on Duty')" />

      <input type="radio" name="my_tabs_2" class="tab" :aria-label="$t('Maintenance Alert Analysis')"
        @click="navigate('MaintenanceAlertAnalysis', 'Maintenance Alert Analysis')" />

      <input type="radio" name="my_tabs_2" class="tab" :aria-label="$t('Shift Efficiency Comparison')"
        @click="navigate('ShiftEfficiencyComparison', 'Shift Efficiency Comparison')" />

      <input type="radio" name="my_tabs_2" class="tab" :aria-label="$t('Work Order')"
        @click="navigate('WorkOrder', 'Work Order')" />
    </div>

    <!-- Loaded Component -->
    <div v-if="currentComponent">
      <component :is="currentComponent" />
    </div>
  </div>
</template>

<script>
  import EfficiencyOverview from './EfficiencyOverview.vue'
  import StatusMonitoring from './StatusMonitoring.vue'
  import AbnormalityonDuty from './AbnormalityonDuty.vue'
  import MaintenanceAlertAnalysis from './MaintenanceAlertAnalysis.vue'
  import ShiftEfficiencyComparison from './ShiftEfficiencyComparison.vue'
  import WorkOrder from './WorkOrder.vue'
  export default {
    name: 'MainDashboard',
    components: {
      EfficiencyOverview,
      StatusMonitoring,
      AbnormalityonDuty,
      MaintenanceAlertAnalysis,
      ShiftEfficiencyComparison,
      WorkOrder
    },
    data() {
      return {
        currentComponent: 'EfficiencyOverview', // default saat load
        currentLabel: 'Efficiency Overview',
        showAlert : false
      }
    },
    methods: {
      navigate(componentName, label) {
        this.currentComponent = componentName
        this.currentLabel = label
      }
    },
    mounted(){
      this.showAlert = true
      setTimeout(() => {
        setTimeout(() => {
          this.showAlert = false
        }, 10000)
      })
    }
  }
</script>

<style scoped>
  .menu-color {
    background-color: #18181b;
    transition: fill 0.3s ease;
    color: white;
  }

  html[data-theme='dark'] .menu-color {
    background-color: #ecf9ff !important;
    color: black;
  }

  button {
    padding: 0;
    margin: 0;
    border: none;
    background: none;
    cursor: pointer;
  }

  button {
    --primary-color: #ffffff;
    --hovered-color: #35a29f;
    position: relative;
    display: flex;
    font-weight: 200;
    font-size: 10px;
    gap: 10rem;
    align-items: center;
  }

  button p {
    margin: 0;
    position: relative;
    font-size: 15px;
    color: var(--primary-color);
  }

  button::after {
    position: absolute;
    content: "";
    width: 10%;
    left: 0;
    bottom: -7px;
    background: var(--hovered-color);
    height: 2px;
    transition: 0.3s ease-out;
  }

  button p::before {
    position: absolute;
    content: attr(data-text);
    width: 10%;
    inset: 0;
    color: var(--hovered-color);
    /* overflow: hidden; */
    transition: 0.3s ease-out;
  }

  button:hover::after {
    width: 100%;
  }

  button:hover p::before {
    width: 100%;
  }

  .fade-enter-active,
    .fade-leave-active {
        transition: opacity 0.6s ease-in-out;
    }

    .fade-enter-from,
    .fade-leave-to {
        opacity: 0;
    }
</style>