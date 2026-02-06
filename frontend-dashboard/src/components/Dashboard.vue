<template>
  <div class="dashboard p-4 font-inter ">

    <div class="flex justify-between">
      <div>
        <h1 class="text-3xl font-bold font-poppins">
          <div class="loader">
            <div class="words">
              <span class="word">Halo!</span>
              <span class="word">你好!</span>
              <span class="word">Helo!</span>
              <span class="word">Xin chào!</span>
              <span class="word">¡Hola!</span>
              <span class="word">नमस्ते!</span>
            </div>
          </div>
        </h1>
        <p class="text-sm opacity-80 mt-6 ">
         {{ $t('Let’s check your production information or add some Quick View!')}}
        </p>
      </div>
      <div>
        <div class="tooltip tooltip-left" data-tip="Customize View">
          <a class="btn btn-light" onclick="my_modal_3.showModal()"><svg xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 20 20" fill="currentColor" class="size-5">
              <path
                d="M2 4.25A2.25 2.25 0 0 1 4.25 2h2.5A2.25 2.25 0 0 1 9 4.25v2.5A2.25 2.25 0 0 1 6.75 9h-2.5A2.25 2.25 0 0 1 2 6.75v-2.5ZM2 13.25A2.25 2.25 0 0 1 4.25 11h2.5A2.25 2.25 0 0 1 9 13.25v2.5A2.25 2.25 0 0 1 6.75 18h-2.5A2.25 2.25 0 0 1 2 15.75v-2.5ZM11 4.25A2.25 2.25 0 0 1 13.25 2h2.5A2.25 2.25 0 0 1 18 4.25v2.5A2.25 2.25 0 0 1 15.75 9h-2.5A2.25 2.25 0 0 1 11 6.75v-2.5ZM15.25 11.75a.75.75 0 0 0-1.5 0v2h-2a.75.75 0 0 0 0 1.5h2v2a.75.75 0 0 0 1.5 0v-2h2a.75.75 0 0 0 0-1.5h-2v-2Z" />
            </svg>
          </a>
        </div>
        <dialog id="my_modal_3" class="modal">
          <div class="modal-box max-w-xl">
            <form method="dialog">
              <button class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2" aria-label="Close">✕</button>
            </form>
            <h3 class="text-md font-semibold">Add Quick View</h3>

            <ul class="mt-5 space-y-4">
              <li v-for="item in quickItems" :key="item.id"
                class="flex items-start gap-4 p-4 bg-base-200 rounded-box shadow-md">
                <input type="checkbox" v-model="item.checked" class="checkbox checkbox-accent mt-1" />

                <div class="flex-1">
                  <div class="font-semibold">{{item.title}}</div>
                  <div class="text-xs opacity-60">{{item.page}}</div>

                  <p class="text-xs mt-2 opacity-70">
                    {{item.description}}
                  </p>
                </div>
                
                <router-link :to="{path: item.path || '/', query: {view: item.view}}" class="btn btn-square btn-ghost">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-5">
                    <path fill-rule="evenodd"
                      d="M5.22 14.78a.75.75 0 0 0 1.06 0l7.22-7.22v5.69a.75.75 0 0 0 1.5 0v-7.5a.75.75 0 0 0-.75-.75h-7.5a.75.75 0 0 0 0 1.5h5.69l-7.22 7.22a.75.75 0 0 0 0 1.06Z"
                      clip-rule="evenodd" />
                  </svg>
                </router-link>
              </li>
            </ul>
          </div>
        </dialog>
      </div>
    </div>

    <div class="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-4 lg:grid-cols-4 xl:grid-cols-4 gap-6 p-6">
        <div class="e-card playing xl:h-[13rem] lg:h-[11rem] md:h-[10rem] sm:h-[10rem] xs:h-[10rem]" v-for="card in cards" :key="card.label">
          <div class="image"></div>
          <div class="wave"></div>
          <div class="wave"></div>
          <div class="wave"></div>
          <div class="infotop">
            <div class="grid grid-flow-col grid-rows-3 xl:gap-4 lg:gap-3 md:gap-1 justify-items-start">
              <div class="row-span-3">
                <img :src="card.icon" :alt="card.label" class="text-accent icon-card" />
              </div>
              <div class="col-span-2 label">{{ $t(card.label) }}</div>
              <div class="col-span-2 row-span-2 xl:text-2xl lg:text-1xl md:text-lg sm:text-lg font-bold ">{{ card.value }}</div>
            </div>    
          </div>
        </div>
      </div>

    <div class="flex flex-wrap gap-2 mt-8 ">
      <div v-for="item in quickViewSelected" :key="item.id" class="card bg-base-300 shadow-xl flex flex-col h-full">
        <div class="card-body items-center text-center mt-auto ">
            <input type="checkbox" v-model="item.checked" class="absolute top-2 right-2 checkbox checkbox-xs checkbox-accent mt-1 " />
          <h2 class="card-title">{{ item.title }}</h2>
          <div class="text-xs opacity-60 mb-4">
            {{ item.page }}
          </div>
          <component :is="item.component" />
          <div class="card-actions mt-2">
            <button class="button btn btn-accent">
              <router-link :to="{path: item.path || '/', query: {view: item.view}}"
                class="text-white font-normal text-xs">
                {{$t('Detail')}}
              </router-link>
            </button>
          </div>
        </div>
      </div>
    </div>

    <!-- <component :is="currentComponent" :key="$route.fullPath" v-else /> -->
  </div>
</template>
<script>
  import axios from 'axios';
  import productionIcon from '@/assets/icons/production.png'
  import lineIcon from '@/assets/icons/line.png'
  import robotIcon from '@/assets/icons/robot.png'
  import autoIcon from '@/assets/icons/auto.png'
  import factoryIcon from '@/assets/icons/factory.png'
  import OEETrendMini from './OEETrendMini.vue'
  import YieldRateMini from './YieldRateMini.vue';
  import AOIQueryDataMini from './AOIQueryDataMini.vue';
  import {
    markRaw
  } from 'vue'

  import {
    CanvasRenderer
  } from 'echarts/renderers'
  import {
    PieChart,
    GaugeChart,
    LineChart
  } from 'echarts/charts'
  import {
    GridComponent,
    TooltipComponent,
    LegendComponent,
    GraphicComponent
  } from 'echarts/components'
  import * as echart from 'echarts/core'

  echart.use([
    CanvasRenderer,
    PieChart,
    LineChart,
    TooltipComponent,
    LegendComponent,
    GraphicComponent,
    GridComponent,
  ])

  export default {
    components: {
      OEETrendMini,
      YieldRateMini,
      AOIQueryDataMini
    },
    data() {
      return {
        dataPolling: null, // BARU: Untuk menyimpan ID interval polling
        productionIcon,
        factoryIcon,
        lineIcon,
        robotIcon,
        autoIcon,
        cards: [],
        quickItems: [{
            id: 1,
            title: "OEE Trend",
            page: "Dashboard - OEE Analytics",
            checked: false,
            component: markRaw(OEETrendMini),
            description: this.$t('Displays an OEE chart showing availability, performance, and quality over a specified period.'),
            path: '/dashboard',
            view: 'oee'
          },
          {
            id: 2,
            title: "Yield Rate",
            page: "Production - Yield / Retest Rate (Yield)_Tree",
            checked: false,
            component: markRaw(YieldRateMini),
            description: this.$t('Displays PASS, FAIL, and Pass Rate (%) over a specified period.'),
            path: '/production',
            view: 'yield'
          },
          {
            id: 3,
            title: "AOI Data",
            page: "Robot - AOI Query Data",
            checked: false,
            component: markRaw(AOIQueryDataMini),
            description: this.$t('Displays PASS, FAIL, and Yield Rate (%) from AOI results over a specified period.'),
            path: '/robot',
            view: 'aoi'
          }
        ]
      }
    },
    // BARU: Tambahkan lifecycle hook beforeUnmount untuk membersihkan interval
    beforeUnmount() {
      clearInterval(this.dataPolling);
    },
    methods: {
      formatNumber(num) {
        return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
      },
      async fetchSummaryStats() {
        try {
          // MODIFIKASI: URL API diperbarui dan ditambahkan parameter unik untuk mencegah cache
          const apiUrl = `http://10.176.33.76:6002/api/Dashboard/summary-stats?_=${new Date().getTime()}`;
          const response = await axios.get(apiUrl);
          const stats = response.data;

          // MODIFIKASI: Ikon untuk 'Total Record' diubah menjadi 'factoryIcon' agar lebih sesuai
          this.cards = [{
              label: 'Production',
              icon: this.factoryIcon,
              value: this.formatNumber(stats.totalRecords)
            },
            {
              label: 'Line',
              icon: this.lineIcon,
              value: stats.totalLines
            },
            {
              label: 'Station',
              icon: this.robotIcon,
              value: stats.totalStations
            },
            {
              label: 'Fixture',
              icon: this.autoIcon,
              value: stats.totalFixtures
            }
          ];
        } catch (error) {
          console.error("Gagal mengambil data summary:", error);
          this.cards = []; // Kosongkan data jika error
        }
      }
    },
    computed: {

      quickViewSelected() {
        return this.quickItems.filter(item => item.checked)
      }
    },
    mounted() {
      // Panggil data pertama kali saat komponen dimuat
      this.fetchSummaryStats();

      // BARU: Atur interval untuk memanggil fetchSummaryStats setiap 30 detik
      this.dataPolling = setInterval(() => {
        console.log('Memeriksa data baru...');
        this.fetchSummaryStats();
      }, 30000);
    },
    created() {
      const saved = localStorage.getItem('quickItems');
      if (saved) {
        const parsed = JSON.parse(saved);

        this.quickItems = this.quickItems.map(item => {
          const savedItem = parsed.find(p => p.id === item.id);
          if (savedItem) item.checked = savedItem.checked;
          return item;
        });
      }
    },
    watch: {
      quickItems: {
        deep: true,
        handler(newVal) {
          const toSave = newVal.map(i => ({
            id: i.id,
            checked: i.checked
          }));
          localStorage.setItem('quickItems', JSON.stringify(toSave));
        }
      }
    }
  }
</script>

<style scoped>
  .summary-card {
    transition: transform 0.2s ease;
  }

  .summary-card:hover {
    transform: translateY(-4px);
  }

  .icon {
    width: 3em;
    margin-top: -1em;
    padding-bottom: 1em;
  }

  .infotop {
    text-align: center;
    font-size: 20px;
    position: absolute;
    top: 3.5rem;
    left: 1rem;
    right: 0;
    color: rgb(255, 255, 255);
    font-weight: 600;
  }

  .label {
    font-size: 1.25rem;
  }

  .value {
    font-size: 2rem;
    font-weight: 600;
  }

  @media (max-width: 768px) {
    .label {
      font-size: 1rem;
    }

    .value {
      font-size: 1.5rem;
    }
  }

  .name {
    font-size: 14px;
    font-weight: 100;
    position: relative;
    top: 1em;
    text-transform: lowercase;
  }

  .e-card {
    background: transparent;
    box-shadow: 0px 8px 28px -9px rgba(0, 0, 0, 0.45);
    position: relative;
    max-width: 370px;
    width: 100%;
    /* height: 200px; */
    border-radius: 16px;
    overflow: hidden;
    margin: 1rem auto;
  }


  .wave {
    position: absolute;
    width: 540px;
    height: 700px;
    opacity: 0.6;
    left: 0;
    top: 0;
    margin-left: -50%;
    margin-top: -70%;
    background: linear-gradient(744deg, #6EB6C5, #8AC5D0 80%, #50A8B9);
  }

  html[data-theme='light'] .wave {
    background: linear-gradient(744deg, #15191e, #1b2729 80%, #183238);
  }

  .wave:nth-child(2),
  .wave:nth-child(3) {
    top: 210px;
  }

  .playing .wave {
    border-radius: 40%;
    animation: wave 3000ms infinite linear;
  }

  .wave {
    border-radius: 40%;
    animation: wave 55s infinite linear;
  }

  .playing .wave:nth-child(2) {
    animation-duration: 4000ms;
  }

  .wave:nth-child(2) {
    animation-duration: 50s;
  }

  .playing .wave:nth-child(3) {
    animation-duration: 5000ms;
  }

  .wave:nth-child(3) {
    animation-duration: 45s;
  }

  @keyframes wave {
    0% {
      transform: rotate(0deg);
    }

    100% {
      transform: rotate(360deg);
    }
  }

  .button {
    --green: #00d3bb;
    font-size: 14px;
    padding: 0.5em 1.7em;
    letter-spacing: 0.06em;
    position: relative;
    font-family: inherit;
    border-radius: 0.6em;
    overflow: hidden;
    transition: all 0.3s;
    line-height: 1.4em;
    border: 2px solid var(--green);
    background: linear-gradient(to right, rgba(27, 253, 242, 0.1) 1%, transparent 40%, transparent 60%, rgba(27, 253, 196, 0.1) 100%);
    color: var(--green);
    box-shadow: inset 0 0 10px rgba(27, 253, 242, 0.4), 0 0 9px 3px rgba(27, 253, 196, 0.1);
    height: 42px;
  }

  .button:hover {
    color: #00d3bb;
    box-shadow: inset 0 0 10px rgba(27, 253, 223, 0.6), 0 0 9px 3px rgba(27, 253, 234, 0.2);
  }

  .button:before {
    content: "";
    position: absolute;
    left: -4em;
    width: 4em;
    height: 100%;
    top: 0;
    transition: transform .4s ease-in-out;
    background: linear-gradient(to right, transparent 1%, rgba(27, 253, 215, 0.1) 40%, rgba(27, 253, 215, 0.1) 60%, transparent 100%);
  }

  .button:hover:before {
    transform: translateX(15em);
  }

  .loader {
    color: rgb(124, 124, 124);
    font-family: "Poppins", sans-serif;
    font-weight: 500;
    font-size: 25px;
    -webkit-box-sizing: content-box;
    box-sizing: content-box;
    height: 40px;

    display: -webkit-box;
    display: -ms-flexbox;
    display: flex;
  }

  .words {
    overflow: hidden;
    position: relative;
  }

  .words::after {
    content: "";
    position: absolute;
    inset: 0;
    background: linear-gradient(var(--bg-color) 10%,
        transparent 30%,
        transparent 70%,
        var(--bg-color) 90%);
    z-index: 20;
  }

  .word {
    display: block;
    height: 100%;
    color: #ffffff;
    animation: spin_4991 8s infinite;
  }

  html[data-theme='light'] .word {
    color: #15191e;
  }

  html[data-theme='light'] .card {
    background-color: #15191e;
  }

  html[data-theme='light'] .btn-light {
    background-color: #15191e;
    color: white;
  }

  html[data-theme='light'] .table {
    color: white;
  }

  html[data-theme='light'] th {
    color: #eee;
  }

  html[data-theme='light'] .card-title {
    color: #eee;
  }

  @keyframes spin_4991 {
    10% {
      -webkit-transform: translateY(-102%);
      transform: translateY(-102%);
    }

    25% {
      -webkit-transform: translateY(-100%);
      transform: translateY(-100%);
    }

    35% {
      -webkit-transform: translateY(-202%);
      transform: translateY(-202%);
    }

    50% {
      -webkit-transform: translateY(-200%);
      transform: translateY(-200%);
    }

    60% {
      -webkit-transform: translateY(-302%);
      transform: translateY(-302%);
    }

    75% {
      -webkit-transform: translateY(-300%);
      transform: translateY(-300%);
    }

    85% {
      -webkit-transform: translateY(-402%);
      transform: translateY(-402%);
    }

    100% {
      -webkit-transform: translateY(-400%);
      transform: translateY(-400%);
    }
  }
</style>