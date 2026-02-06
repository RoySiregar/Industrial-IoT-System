<template>
  <div class="home xl:p-10 md:p-0 font-mono  ">
    <div class="grid xl:grid-cols-10 lg:grid-cols-4 md:grid-cols-4 xl:gap-[6rem] lg:gap-2 md:gap-8 ">
      <div class="xl:col-span-4 lg:col-span-1 md:col-span-1 row-span-3 p-2">
        <div class="row-span-2 px-10 xl:h-[35rem] lg:h-[22rem] md:h-[22rem] ">
          <div class="loader ">
            <div class="cube">
              <div class="face middle front">
                <div class="cube cube-front">
                  <div class="face front"></div>
                  <div class="face back"></div>
                  <div class="face left"></div>
                  <div class="face right"></div>
                  <div class="face top"></div>
                  <div class="face bottom"></div>
                </div>
              </div>
              <div class="face middle back">
                <div class="cube cube-back">
                  <div class="face front"></div>
                  <div class="face back"></div>
                  <div class="face left"></div>
                  <div class="face right"></div>
                  <div class="face top"></div>
                  <div class="face bottom"></div>
                </div>
              </div>
              <div class="face middle left">
                <div class="cube cube-left">
                  <div class="face front"></div>
                  <div class="face back"></div>
                  <div class="face left"></div>
                  <div class="face right"></div>
                  <div class="face top"></div>
                  <div class="face bottom"></div>
                </div>
              </div>
              <div class="face middle right">
                <div class="cube cube-right">
                  <div class="face front"></div>
                  <div class="face back"></div>
                  <div class="face left"></div>
                  <div class="face right"></div>
                  <div class="face top"></div>
                  <div class="face bottom"></div>
                </div>
              </div>
              <div class="face middle top">
                <div class="cube cube-top">
                  <div class="face front"></div>
                  <div class="face back"></div>
                  <div class="face left"></div>
                  <div class="face right"></div>
                  <div class="face top"></div>
                  <div class="face bottom"></div>
                </div>
              </div>
              <div class="face middle bottom">
                <div class="cube cube-bottom">
                  <div class="face front"></div>
                  <div class="face back"></div>
                  <div class="face left"></div>
                  <div class="face right"></div>
                  <div class="face top"></div>
                  <div class="face bottom"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="row-span-1 ">
          <h1 class="xl:text-2xl lg:text-xl md:text-lg font-bold text-default">{{$t('Welcome')}}
          </h1>
          <p class="floating-label font-inter mt-2 text-wrap">{{ $t('Back') }}</p>
        </div>
        <router-link :to="{ path: 'dashboard' || '/', query: { view: 'dashboard' } }" tabindex="0"
          class="inline-flex items-center">
          <div class="row-span-1 mt-10 font-inter">
            <button class="btn-home">
              <div class="text text-white">
                <span>{{ $t('Get') }}</span>
                <span>{{ $t('to') }}</span>
                <span>{{ $t('started') }}</span>
              </div>
              <div class="clone text-white">
                <span>{{ $t('Go') }}</span>
                <span>{{ $t('to2') }}</span>
                <span>{{ $t('dashboard') }}</span>
              </div>
              <svg stroke-width="2" stroke="white" viewBox="0 0 24 24" fill="none" class="h-6 w-6"
                xmlns="http://www.w3.org/2000/svg" width="20px">
                <path d="M14 5l7 7m0 0l-7 7m7-7H3" stroke-linejoin="round" stroke-linecap="round"></path>
              </svg>
            </button>
          </div>
        </router-link>
      </div>

      <div class="xl:col-span-3 lg:col-span-2 md:col-span-2 xl:p-6 lg:p-2 md:p-1">
        <h1 class="text-2xl font-bold mb-8 tracking-wider">{{$t('Automation Development Section')}}
        </h1>
        <div class="grid xl:grid-cols-1 lg:grid-cols-4 md:grid-cols-4 xl:gap-4 lg:gap-[15rem] md:gap-[11.5rem]">
          <div class="container" v-for="card in cards" :key="card.label">
            <div class="box justify-items-start">
              <div class="grid grid-flow-col grid-rows-3 lg:gap-12 md:gap-2">
                <div class="row-span-3">
                  <span class="title">
                    <img :src="card.icon" :alt="card.label" class="text-accent icon-card " />
                  </span>
                </div>
                <div class="grid xl:gap-4">
                  <div class="col-span-2"> <strong>{{ $t(card.label) }}</strong></div>
                  <div
                    class="col-span-2 row-span-2 text-xl sm:text-3xl md:text-lg lg:text-xl xl:text-4xl value font-mono">
                    <p>{{ card.value }}</p>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="xl:col-span-3 lg:col-span-2 md:col-span-2 xl:p-6 lg:p-2">
        <h1 class="text-2xl font-bold mb-8 tracking-wider">{{ $t('Robots Error')}}
        </h1>
        <div class="grid xl:grid-cols-1 lg:grid-cols-2 md:grid-cols-2 xl:gap-6 lg:gap-[18rem] md:gap-[16rem]">
          <div class="container-week">
            <div class="box">
              <h1 class="title">{{$t('Top')}}</h1>
              <div class="m-0 p-0">
                <v-chart :option="chartTopLine"
                  class="lg:w-[24rem] md:w-[18rem] lg:h-[18rem] md:h-[17rem] m-0 p-0 bottom-8"></v-chart>
              </div>
            </div>
          </div>
          <div class="container-week">
            <div class="box">
              <h1 class="title">{{$t('Status')}}</h1>
              <div class="py-5">
                <v-chart :option="chartStatusMachine"
                  class="lg:w-[24rem] md:w-[20rem] lg:h-[20rem] md:h-[18rem] m-0 p-0 bottom-10"></v-chart>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  import axios from 'axios';
  import factoryIcon from '@/assets/icons/factory.png'
  import lineIcon from '@/assets/icons/line.png'
  import robotIcon from '@/assets/icons/robot.png'
  import autoIcon from '@/assets/icons/auto.png'
  import {
    BarChart,
    LineChart
  } from 'echarts/charts'

  import {
    GridComponent,
    TooltipComponent,
    TitleComponent
  } from 'echarts/components'

  import {
    CanvasRenderer
  } from 'echarts/renderers'
  import {
    LegendComponent
  } from 'echarts/components';

  import * as echarts from 'echarts/core'

  echarts.use([
    BarChart,
    LineChart,
    GridComponent,
    TooltipComponent,
    TitleComponent,
    CanvasRenderer,
    LegendComponent
  ])
  export default {
    props:{
      loggedInUserType:{
        type: String,
        default: ''
      }
    },
     created() {
    if (this.loggedInUserType === 'guest') {
      console.log('Logged in as Guest');
    } else {
      console.log('Logged in as User');
    }
   },
    data() {
      return {
        dataPolling: null,
        currentDate: '',
        factoryIcon,
        lineIcon,
        robotIcon,
        autoIcon,
        cards: [],
        chartTopLine: {
          backgroundColor: 'transparent',
          tooltip: {
            trigger: 'axis',
            axisPointer: {
              type: 'shadow'
            },
            formatter: '{b}: Total Error: <b>{c}</b>'
          },
          grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
          },
          xAxis: {
            type: 'category',
            data: [],
            axisLine: {
              lineStyle: {
                color: '#FFFFFF'
              }
            },
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
              fontSize: 10
            },
            splitLine: {
              show: true,
              lineStyle: {
                color: '#454545'
              }
            }
          },
          yAxis: {
            type: 'value',
            name: null,
            nameTextStyle: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10
            },
            axisLine: {
              lineStyle: {
                color: '#FFFFFF'
              }
            },
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10
            },
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            }
          },
          series: [{
            name: 'Line',
            type: 'bar',
            data: [],
            itemStyle: {
              color: '#00d3bb',
              shadowColor: 'rgba(255, 255, 255, 0.7)',
              shadowBlur: 10,
            },
            label: {
              show: true,
              position: 'top',
              formatter: '{c}',
              color: '#FFFFFF',
              fontWeight: 'bold',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10
            }
          }]
        },
        chartStatusMachine: {
          backgroundColor: 'rgba(0, 0, 0, 0)',
          title: {
            text: '',
          },
          tooltip: {
            trigger: 'axis',
            formatter: (params) => {
              return params
                .map((p) => `${p.seriesName}: ${p.value} times`)
                .join('<br/>');
            }
          },

          xAxis: {
            type: 'category',
            data: [],
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            }
          },
          yAxis: {
            type: 'value',
            nameTextStyle: {
              color: '#FFFFFF'
            },
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            min: 0,
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            }
          },
           color: ['#FF9A00', '#F6FA70', '#00d3bb', '#51EAEA', '#00DFA2', '#FF1700'],
          series: []
        }
      }
    },
    beforeUnmount() {
      clearInterval(this.dataPolling);
    },
    methods: {
      formatNumber(num) {
        return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
      },
      updateDate() {
        const now = new Date();
        const month = (now.getMonth() + 1).toString().padStart(2, '0');
        const day = now.getDate().toString().padStart(2, '0');
        this.currentDate = `${month}/${day}`;
      },
      async fetchSummaryStats() {
        this.updateDate();
        try {
          const apiUrl = `http://10.175.80.201:5001/api/Dashboard/summary-stats?_=${new Date().getTime()}`;
          const response = await axios.get(apiUrl);
          const stats = response.data;
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
            },
          ];
        } catch (error) {
          console.error("Error fetching summary stats:", error);
          this.cards = [];
        }
      },
      async fetchTopLinesData() {
        try {
          // URL diperbarui ke endpoint RobotErrors
          const apiUrl = `http://10.175.80.201:5001/api/RobotErrors/monthly/top5-lines?_=${new Date().getTime()}`;
          
          const response = await axios.get(apiUrl);
          const topLines = response.data;

          if (Array.isArray(topLines)) {
            // Mapping data: 'line' tetap sama
            const categories = topLines.map(item => item.line);
            
            // PERUBAHAN DISINI: Menggunakan 'totalErrorCount' sesuai struktur JSON baru
            const seriesData = topLines.map(item => item.totalErrorCount);
            
            this.chartTopLine.xAxis.data = categories;
            this.chartTopLine.series[0].data = seriesData;
          }
        } catch (error) {
          console.error("Error fetching top lines data:", error);
          this.chartTopLine.xAxis.data = []; // Perbaikan: properti xAxis biasanya 'data', bukan 'categories' di ECharts config Anda
          this.chartTopLine.series[0].data = [];
        }
      },
      // BARU: Fungsi untuk mengambil data status mesin
      async fetchMachineStatusData() {
        try {
          // Siapkan parameter tanggal untuk hari ini
          const today = new Date().toISOString().split('T')[0];
          const startDate = `${today}T00:00:00`;
          const endDate = `${today}T23:59:59`;
          const line = '3F13'; // Line masih statis sesuai contoh

          const apiUrl =
            `http://10.175.80.201:5001/api/MachineStatus/summary?startDate=${startDate}&endDate=${endDate}&line=${line}&_=${new Date().getTime()}`;
          const response = await axios.get(apiUrl);
          const apiData = response.data;

          if (!Array.isArray(apiData) || apiData.length === 0) {
            this.chartStatusMachine.series = [];
            this.chartStatusMachine.xAxis.data = [];
            return;
          }

          // 1. Dapatkan semua kategori waktu (sumbu X) dan urutkan
          const timeStamps = [...new Set(apiData.map(item => item.timeStamp))].sort();

          // 2. Dapatkan semua jenis status unik
          const statusTypes = [...new Set(apiData.map(item => item.machineStatus))];

          // 3. Olah data menjadi format series untuk ECharts
          const series = statusTypes.map(status => {
            return {
              name: status,
              type: 'line', // WAJIB untuk ECharts
              data: timeStamps.map(ts => {
                const dataPoint = apiData.find(d => d.timeStamp === ts && d.machineStatus === status);
                return dataPoint ? dataPoint.statusCount : 0; // Jika tidak ada data, beri nilai 0
              }),
              lineStyle: {
                width: 3
              },
              symbolSize: 6 // Mirip marker.radius di Highcharts
            };
          });

          // 4. Update konfigurasi chart ECharts
          this.chartStatusMachine.xAxis.data = timeStamps;
          this.chartStatusMachine.series = series;

        } catch (error) {
          console.error("Error fetching machine status data:", error);
          this.chartStatusMachine.series = [];
          this.chartStatusMachine.xAxis.data = [];
        }
      }
    },
    mounted() {
      this.fetchSummaryStats();
      this.fetchTopLinesData();
      this.fetchMachineStatusData(); // BARU: Panggil fungsi fetch status mesin

      this.dataPolling = setInterval(() => {
        console.log("Memeriksa data baru...");
        this.fetchSummaryStats();
        this.fetchTopLinesData();
        this.fetchMachineStatusData(); // BARU: Tambahkan refresh untuk chart status mesin
      }, 30000);
    }
  }
</script>

<style scoped>
  html[data-theme='light'] .home{
    background-color: #eeeeee !important;
  } 
    html[data-theme='light'] .title{
      color: #fff;
    }
  .container {
    color: white;
    position: relative;
    font-family: sans-serif;
  }

  .container .box {
    width: 25.875em;
    height: 8.875em;
    padding: 2rem;
    background-color: rgba(255, 255, 255, 0.074);
    -webkit-backdrop-filter: blur(5px);
    backdrop-filter: blur(20px);
    border-radius: 10px;
    transition: all ease 0.3s;
    box-shadow: 0px 0px 5px 1px var(--glow-color),
      0 0 2em 1px var(--glow-spread-color);
    --glow-color: rgb(176, 255, 248);
    --glow-spread-color: rgba(123, 255, 237, 0.781);
    --enhanced-glow-color: rgb(206, 255, 251);
  }

  html[data-theme='light'] .container .box{
    background-color: #15191e;
    box-shadow: none;
  } 

  .container-week .box {
    width: 25.875em;
    height: 20.875em;
    padding: 2rem;
    background-color: rgba(255, 255, 255, 0.074);
    -webkit-backdrop-filter: blur(5px);
    backdrop-filter: blur(20px);
    border-radius: 10px;
    transition: all ease 0.3s;
    box-shadow: 0px 0px 5px 1px var(--glow-color),
      0 0 2em 1px var(--glow-spread-color);
    --glow-color: rgb(176, 255, 248);
    --glow-spread-color: rgba(123, 255, 237, 0.781);
    --enhanced-glow-color: rgb(206, 255, 251);
  }

  html[data-theme='light'] .container-week .box{
    background-color: #15191e;
    box-shadow: none;
  } 

  .container .box {
    display: flex;
    justify-content: space-between;
  }

  .container .box .title {
    font-size: 2rem;
    font-weight: 500;
    letter-spacing: 0.1em;
  }

  .container .box div strong {
    display: block;
    margin-bottom: 0.5rem;
  }

  .container .box div p {
    margin: 0;
    font-size: 0.9em;
    font-weight: 300;
    letter-spacing: 0.1em;
  }

  .container .box div span {
    font-size: 0.7rem;
    font-weight: 300;
  }

  .container .box div span:nth-child(3) {
    font-weight: 500;
    margin-right: 0.2rem;
  }

  .loader {
    position: relative;
    display: flex;
    top: 140px;
    align-items: center;
    justify-content: center;
  }

  .cube {
    position: absolute;
    width: 140px;
    transform-style: preserve-3d;
    transform: rotateX(-30deg) rotateY(45deg);
    transition: 300ms ease;
    cursor: pointer;
    animation: rotateCube 10s infinite linear;
  }

  .cube-front,
  .cube-back {
    transform: translateX(140px) translateZ(-80px);
    animation: none;
  }

  .cube-top,
  .cube-bottom {
    transform: translateZ(60px);
    animation: none;
  }

  .cube-left,
  .cube-right {
    transform: translateX(140px) translateZ(-80px);
    animation: none;
  }

  .face {
    position: absolute;
    transform-style: preserve-3d;
    width: 140px;
    height: 140px;
    background: rgb(101, 169, 184);
    background: radial-gradient(circle,
        rgba(167, 211, 220) 0%,
        rgba(110, 182, 197) 100%);
  }

  html[data-theme='light'] .face{
    background: rgb(21, 25, 30) !important;
    background: radial-gradient(circle,
        rgb(185, 219, 226) 0%,
        rgb(43, 59, 59) 100%)!important;
  }
  .front {
    transform: rotateY(0deg) translateZ(80px);
  }

  .back {
    transform: rotateY(180deg) translateZ(60px);
  }

  .left {
    transform: rotateY(-90deg) translateZ(70px) translateX(10px);
  }

  .right {
    transform: rotateY(90deg) translateZ(70px) translateX(-10px);
  }

  .top {
    transform: rotateX(90deg) translateZ(70px) translateY(10px);
  }

  .bottom {
    transform: rotateX(-90deg) translateZ(70px) translateY(-10px);
  }

  .cube-back:hover .face,
  .cube-front:hover .face,
  .cube-top:hover .face,
  .cube-bottom:hover .face,
  .cube-left:hover .face,
  .cube-right:hover .face {
    background: rgb(255, 255, 255);
    background: radial-gradient(circle, white 60%, rgb(176, 255, 248) 100%);
    filter: drop-shadow(0px 0px 5px #e7faff) drop-shadow(0px 0px 15px rgba(123, 255, 237, 0.781)) drop-shadow(0px 0px 30px rgb(95, 186, 189));
  }

  .cube:active {
    transform: translateX(0px) translateZ(-80px);
  }

  .cube-back:active .face,
  .cube-front:active .face,
  .cube-top:active .face,
  .cube-bottom:active .face,
  .cube-left:active .face,
  .cube-right:active .face {
    background: rgb(255, 255, 255);
    background: radial-gradient(circle, white 60%, rgb(176, 255, 248) 100%);
    filter: drop-shadow(0px 0px 5px #e7faff) drop-shadow(0px 0px 15px rgba(123, 255, 237, 0.781)) drop-shadow(0px 0px 30px rgb(95, 186, 189));
  }


  .middle {
    background: transparent;
  }

  @keyframes rotateCube {
    0% {
      transform: rotateX(-30deg) rotateY(45deg);
    }

    100% {
      transform: rotateX(-30deg) rotateY(405deg);
    }
  }

  .btn-home {
    width: 240px;
    height: 60px;
    padding: 1em;
    overflow: hidden;
    border-radius: 10px;
    color: #fff;
    background: none;
    position: relative;
    padding-bottom: 3em;
    cursor: pointer;
    box-shadow: 0 0 1em .25em var(--glow-color),
      0 0 4em 1em var(--glow-spread-color),
      inset 0 0 .75em .25em var(--glow-color);
    --glow-color: rgb(176, 255, 248);
    --glow-spread-color: rgba(123, 255, 237, 0.781);
    --enhanced-glow-color: rgb(206, 255, 251);
    --btn-color: rgb(95, 186, 189);
  }

  html[data-theme='light'] .btn-home{
    background-color: #15191e;
    box-shadow: none;
  }

  .btn-home>div,
  .btn-home>svg {
    position: absolute;
    width: 100%;
    height: 100%;
    display: flex;
  }
  
  .btn-home:before {
    content: "";
    position: absolute;
    height: 2px;
    bottom: 0;
    left: 0;
    width: 100%;
    transform: scaleX(0);
    transform-origin: bottom right;
    transition: transform 0.25s ease-out;
  }

  .btn-home:hover:before {
    transform: scaleX(1);
    transform-origin: bottom left;
  }

  .btn-home .clone>*,
  .btn-home .text>* {
    opacity: 1;
    font-size: 1.3rem;
    transition: 0.2s;
    margin-left: 4px;
  }

  .btn-home .clone>* {
    transform: translateY(60px);
  }

  .btn-home:hover .clone>* {
    opacity: 1;
    transform: translateY(0px);
    transition: all 0.2s cubic-bezier(0.215, 0.61, 0.355, 1) 0s;
  }

  .btn-home:hover .text>* {
    opacity: 1;
    transform: translateY(-60px);
    transition: all 0.2s cubic-bezier(0.215, 0.61, 0.355, 1) 0s;
  }

  .btn-home:hover .clone> :nth-child(1) {
    transition-delay: 0.15s;
  }

  .btn-home:hover .clone> :nth-child(2) {
    transition-delay: 0.2s;
  }

  .btn-home:hover .clone> :nth-child(3) {
    transition-delay: 0.25s;
  }

  .btn-home:hover .clone> :nth-child(4) {
    transition-delay: 0.3s;
  }

  .btn-home svg {
    width: 20px;
    right: 18px;
    top: 50%;
    transform: translateY(-50%) rotate(-50deg);
    transition: 0.2s ease-out;
  }

  .btn-home:hover svg {
    transform: translateY(-50%) rotate(90deg);
  }
</style>