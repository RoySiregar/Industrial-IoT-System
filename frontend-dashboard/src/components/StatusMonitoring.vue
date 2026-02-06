<template>
  <div class="status-monitoring p-4">
    <!-- Filters -->
    <div class="w-full bg-base-300 p-4 rounded-lg shadow mb-4 summary-card">
      <div class="flex flex-wrap items-center justify-between gap-4">
        <div class="flex gap-4">
          <div class="flex flex-wrap items-center gap-2">
            <div v-if="activeFilters.includes('floor')">
              <select v-model="filters.floor" class="select select-bordered select-sm">
                <option disabled value="">{{$t('Choose Floor')}}</option>
                <option v-for="floor in floor" :key="floor">{{ floor }}</option>
              </select>
            </div>
            <div v-if="activeFilters.includes('area')">
              <select v-model="filters.area" class="select select-bordered select-sm">
                <option disabled value="">{{$t('Choose Area')}}</option>
                <option v-for="area in area" :key="area">{{ area }}</option>
              </select>
            </div>
            <div v-if="activeFilters.includes('group')">
              <select v-model="filters.group" class="select select-bordered select-sm">
                <option disabled value="">{{$t('Choose Group')}}</option>
                <option v-for="group in group" :key="group">{{ group }}</option>
              </select>
            </div>
            <div v-if="activeFilters.includes('date')" class="flex items-center gap-2">
              <input type="date" v-model="filters.dateFrom" class="input input-bordered input-sm" />
              <span class="strip">-</span>
              <input type="date" v-model="filters.dateUntil" class="input input-bordered input-sm" />
              <button class="btn btn-sm btn-accent">{{$t('Apply')}}</button>
            </div>
          </div>
        </div>
        <div class="radio-buttons-container flex items-center gap-4 mt-4">
          <div class="radio-button">
            <input name="radio-group" id="radio0" class="radio-button__input" type="radio" value=""
              v-model="selectedShift">
          </div>
          <div class="radio-button">
            <input name="radio-group" id="radio1" class="radio-button__input" type="radio" value="day"
              v-model="selectedShift">
            <label for="radio1" class="radio-button__label">
              <span class="radio-button__custom"></span>
              {{$t('Day Shift')}}
            </label>
          </div>
          <div class="radio-button">
            <input name="radio-group" id="radio2" class="radio-button__input" type="radio" value="middle"
              v-model="selectedShift">
            <label for="radio2" class="radio-button__label">
              <span class="radio-button__custom"></span>
              {{$t('Middle Shift')}}
            </label>
          </div>
          <div class="radio-button">
            <input name="radio-group" id="radio3" class="radio-button__input" type="radio" value="night"
              v-model="selectedShift">
            <label for="radio3" class="radio-button__label">
              <span class="radio-button__custom"></span>
              {{$t('Night Shift')}}
            </label>
          </div>
        </div>
      </div>
    </div>

    <div class="grid grid-flow-row-dense grid-cols-4 auto-rows-fr gap-4 ">
      <div class="flex w-full gap-4">
        <div class="card bg-base-300 rounded-box image-full w-96 h-86 shadow-sm flex">
          <figure class="absolute inset-0 opacity-20">
            <img :src="runImg" alt="Status Image" class="w-full h-full object-cover" />
          </figure>

          <div class="card-body items-center text-center z-10 justify-center">
            <h2 class="card-title text-white">{{$t('Device Status')}}</h2>
            <h1 id="statusText" class="text-4xl font-bold text-success">{{$t('Running')}}</h1>
          </div>
        </div>

        <div class="card bg-base-300 rounded-box image-full w-96 h-86 shadow-sm flex ">
          <div class="card-body items-center text-center z-10 justify-center">
            <h2 class="card-title text-white">{{$t('Duration')}}</h2>
            <h1 class="duration-text text-3xl font-bold text-zinc-400">95 {{$t('init.minutes')}}</h1>
          </div>
        </div>
      </div>

      <div class="card bg-base-300 shadow-xl flex flex-col h-full">
        <div class="overflow-x-auto px-10 pt-10">
          <table class="table ">
            <thead>
              <tr>
                <th></th>
                <th>{{$t('State')}}</th>
                <th>{{$t('Duration')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <th><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-5">
                    <path fill-rule="evenodd"
                      d="M15.28 9.47a.75.75 0 0 1 0 1.06l-4.25 4.25a.75.75 0 1 1-1.06-1.06L13.69 10 9.97 6.28a.75.75 0 0 1 1.06-1.06l4.25 4.25ZM6.03 5.22l4.25 4.25a.75.75 0 0 1 0 1.06l-4.25 4.25a.75.75 0 0 1-1.06-1.06L8.69 10 4.97 6.28a.75.75 0 0 1 1.06-1.06Z"
                      clip-rule="evenodd" />
                  </svg>
                </th>
                <td>{{ $t('Running') }}</td>
                <td>1 {{$t('init.hours')}}</td>
              </tr>
              <tr>
                <th><svg xmlns="http://www.w3.org/2000/ svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5"
                    stroke="currentColor" class="size-6">
                    <path stroke-linecap="round" stroke-linejoin="round"
                      d="M9 12.75 11.25 15 15 9.75M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z" />
                  </svg>
                </th>
                <td>{{$t('Standby')}}</td>
                <td>27 {{$t('init.minutes')}}</td>
              </tr>
              <tr>
                <th><svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5"
                    stroke="currentColor" class="size-6">
                    <path stroke-linecap="round" stroke-linejoin="round"
                      d="M18.364 18.364A9 9 0 0 0 5.636 5.636m12.728 12.728A9 9 0 0 1 5.636 5.636m12.728 12.728L5.636 5.636" />
                  </svg>
                </th>
                <td>{{$t('Abnormal')}}</td>
                <td>4 {{$t('init.minutes')}}</td>
              </tr>
              <tr>
                <th><svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5"
                    stroke="currentColor" class="size-6">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z" />
                    <path stroke-linecap="round" stroke-linejoin="round"
                      d="M9 9.563C9 9.252 9.252 9 9.563 9h4.874c.311 0 .563.252.563.563v4.874c0 .311-.252.563-.563.563H9.564A.562.562 0 0 1 9 14.437V9.564Z" />
                  </svg>
                </th>
                <td>{{$t('Shutdown')}}</td>
                <td>5 {{$t('init.minutes')}}</td>
              </tr>
            </tbody>
          </table>
        </div>
        <div class="card-body items-center text-center mt-auto  ">
          <h2 class="card-title">{{$t('Status Statistics')}}</h2>

        </div>
      </div>
      <div class="card bg-base-300 shadow-xl col-span-2 flex flex-col ">
        <figure class="px-10 pt-10">
          <v-chart :option="chartOptionsStatus" class="w-[80rem] h-80" />
        </figure>
        <div class="card-body items-center text-center mt-auto">
          <h2 class="card-title">{{$t('Time Trend of Each State')}}</h2>

        </div>
      </div>
      <div class="flex w-full gap-4">
        <div class="card bg-base-300 rounded-box image-full w-96 h-86 shadow-sm flex">
          <figure class="absolute inset-0 opacity-20 ">
            <img :src="standbyImg" alt="Status Image" class="w-96 h-96 object-cover" />
          </figure>

          <div class="card-body items-center text-center z-10 justify-center">
            <h2 class="card-title text-white">{{$t('Device Status')}}</h2>
            <h1 id="statusText" class="text-4xl font-bold text-warning">{{$t('Standby')}}</h1>
          </div>
        </div>

        <div class="card bg-base-300 rounded-box image-full w-96 h-86 shadow-sm flex ">
          <div class="card-body items-center text-center z-10 justify-center">
            <h2 class="card-title text-white">{{$t('Duration')}}</h2>
            <h1 class="duration-text text-3xl font-bold text-zinc-400">95 {{$t('init.minutes')}}</h1>
          </div>
        </div>
      </div>

      <div class="card bg-base-300 shadow-xl flex flex-col h-full">
        <div class="overflow-x-auto px-10 pt-10">
          <table class="table ">
            <thead>
              <tr>
                <th></th>
                <th>{{$t('State')}}</th>
                <th>{{$t('Duration')}}</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <th><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-5">
                    <path fill-rule="evenodd"
                      d="M15.28 9.47a.75.75 0 0 1 0 1.06l-4.25 4.25a.75.75 0 1 1-1.06-1.06L13.69 10 9.97 6.28a.75.75 0 0 1 1.06-1.06l4.25 4.25ZM6.03 5.22l4.25 4.25a.75.75 0 0 1 0 1.06l-4.25 4.25a.75.75 0 0 1-1.06-1.06L8.69 10 4.97 6.28a.75.75 0 0 1 1.06-1.06Z"
                      clip-rule="evenodd" />
                  </svg>
                </th>
                <td>{{$t('Running')}}</td>
                <td>1H {{$t('init.hours')}}</td>
              </tr>
              <tr>
                <th><svg xmlns="http://www.w3.org/2000/ svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5"
                    stroke="currentColor" class="size-6">
                    <path stroke-linecap="round" stroke-linejoin="round"
                      d="M9 12.75 11.25 15 15 9.75M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z" />
                  </svg>
                </th>
                <td>{{$t('Standby')}}</td>
                <td>27 {{$t('init.minutes')}}</td>
              </tr>
              <tr>
                <th><svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5"
                    stroke="currentColor" class="size-6">
                    <path stroke-linecap="round" stroke-linejoin="round"
                      d="M18.364 18.364A9 9 0 0 0 5.636 5.636m12.728 12.728A9 9 0 0 1 5.636 5.636m12.728 12.728L5.636 5.636" />
                  </svg>
                </th>
                <td>{{$t('Abnormal')}}</td>
                <td>4 {{$t('init.minutes')}}</td>
              </tr>
              <tr>
                <th><svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5"
                    stroke="currentColor" class="size-6">
                    <path stroke-linecap="round" stroke-linejoin="round" d="M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z" />
                    <path stroke-linecap="round" stroke-linejoin="round"
                      d="M9 9.563C9 9.252 9.252 9 9.563 9h4.874c.311 0 .563.252.563.563v4.874c0 .311-.252.563-.563.563H9.564A.562.562 0 0 1 9 14.437V9.564Z" />
                  </svg>
                </th>
                <td>{{$t('Shutdown')}}</td>
                <td>5 {{$t('init.minutes')}}</td>
              </tr>
            </tbody>
          </table>
        </div>
        <div class="card-body items-center text-center mt-auto  ">
          <h2 class="card-title">{{$t('Status Statistics')}}</h2>

        </div>
      </div>
      <div class="card bg-base-300 shadow-xl col-span-2 flex flex-col ">
        <figure class="px-10 pt-10">
          <v-chart :option="chartOptionsStatus" class="w-[80rem] h-80" />
        </figure>
        <div class="card-body items-center text-center mt-auto">
          <h2 class="card-title">{{$t('Time Trend of Each State')}}</h2>

        </div>
      </div>
    </div>

  </div>
</template>
<script lang="ts">
  import runImg from '@/assets/img/run.png'
  import standbyImg from '@/assets/img/standby.png'
  import abnormalImg from '@/assets/img/abnormal.png'
  import shutdownImg from '@/assets/img/shutdown.png'

  import {
    CanvasRenderer
  } from 'echarts/renderers'
  import {
    LineChart,
    BarChart
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
    LineChart,
    BarChart,
    GridComponent,
    TooltipComponent,
    LegendComponent,
    GraphicComponent
  ])

  function applyStackBarRadius(series: any[]) {
    const stackInfo: {
      [key: string]: {
        stackStart: number[];stackEnd: number[]
      }
    } = {};

    for (let i = 0; i < series[0].data.length; ++i) {
      for (let j = 0; j < series.length; ++j) {
        const stackName = series[j].stack;
        if (!stackName) continue;

        if (!stackInfo[stackName]) {
          stackInfo[stackName] = {
            stackStart: [],
            stackEnd: [],
          };
        }

        const info = stackInfo[stackName];
        const data = series[j].data[i];

        if (data != null && data !== '-') {
          if (info.stackStart[i] == null) {
            info.stackStart[i] = j;
          }
          info.stackEnd[i] = j;
        }
      }
    }

    for (let i = 0; i < series.length; ++i) {
      const data = series[i].data as(number | string | {
        value: number;itemStyle ? : object
      })[];
      const stackName = series[i].stack;

      if (!stackName || !stackInfo[stackName]) continue;

      const info = stackInfo[stackName];

      for (let j = 0; j < data.length; ++j) {
        const val = data[j];
        const rawValue =
          typeof val === 'object' && val !== null ? (val as any).value : val;

        if (rawValue == null || rawValue === '-') continue;

        const isEnd = info.stackEnd[j] === i;
        const topBorder = isEnd ? 20 : 0;
        const bottomBorder = 0;

        data[j] = {
          value: rawValue,
          itemStyle: {
            borderRadius: [topBorder, topBorder, bottomBorder, bottomBorder],
          },
        };
      }
    }
  }

  export default {
    data() {
      return {
        selected: 'Group A' as string | null,
        selectedArea: '',
        selectedGroup: '',
        showDropdown: false,
        selectedShift: '',
        filters: {
          floor: '',
          dateFrom: '',
          dateUntil: '',
          area: '',
          group: ''
        },
        runImg,
        standbyImg,
        abnormalImg,
        shutdownImg,
        activeFilters: ['area', 'group', 'date', 'floor'],
        availableFilters: [{
            label: 'Site',
            value: 'site'
          },
          {
            label: 'BU',
            value: 'bu'
          },
          {
            label: 'Date Range',
            value: 'date'
          },
          {
            label: 'Factory',
            value: 'factory'
          },
          {
            label: 'Area',
            value: 'area'
          }
        ],
        floor: [this.$t('floor.Floor 1'), this.$t('floor.Floor 2'), this.$t('floor.Floor 3')],
        area: [this.$t('Stamping Workshop'), this.$t('Warehouse'), this.$t('Workshop')],
        group: [this.$t('Group 01'), this.$t('Group 02'), this.$t('Group 03')],
        statuses: ['RUN', 'STANDBY', 'ABNORMAL', 'SHUTDOWN'],
        statusColors: ['text-green-400', 'text-yellow-400', 'text-red-500', 'text-gray-400'],
        chartOptionsStatus: {
          backgroundColor: 'transparent',
          title: {
            text: '',
          },
          tooltip: {
            trigger: 'axis',
            axisPointer: {
              type: 'shadow'
            },
            formatter: (params: any) => {
              let result = `<strong>${params[0].axisValue}</strong><br/>`;
              params.forEach((item: any) => {
                result +=
                  `<span style="color:${item.color}">●</span> ${item.seriesName}: ${item.data} menit<br/>`;
              });
              return result;
            }
          },
          legend: {
            textStyle: {
              color: '#fff'
            }
          },
          xAxis: {
            type: 'category',
            data: ['6/5 00:00', '6/6 00:00', '6/7 00:00', '6/8 00:00', '6/9 00:00', '6/10 00:00', '6/11 00:00'],
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10
            },
            axisLine: {
              lineStyle: {
                color: '#555'
              }
            }
          },
          yAxis: {
            type: 'value',
            name: 'Date / Hours',
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
              formatter: (val: number) => (val >= 1000 ? val / 1000 + 'K' : val)
            },
            nameTextStyle: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10
            },
            splitLine: {
              lineStyle: {
                color: '#333'
              }
            }
          },
          series: [{
              name: this.$t('Running'),
              type: 'bar',
              stack: 'status',
              data: [600, 720, 480, 300, 200, 500, 800],
              itemStyle: {
                color: '#00DFA2'
              }
            },
            {
              name: this.$t('Standby'),
              type: 'bar',
              stack: 'status',
              data: [120, 60, 180, 90, 200, 100, 50],
              itemStyle: {
                color: '#FF9A00'
              }
            },
            {
              name: this.$t('Abnormal'),
              type: 'bar',
              stack: 'status',
              data: [30, 15, 60, 45, 65, 25, 10],
              itemStyle: {
                color: '#51EAEA'
              }
            },
            {
              name: this.$t('Shutdown'),
              type: 'bar',
              stack: 'status',
              data: [90, 120, 180, 60, 80, 140, 150],
              itemStyle: {
                color: '#FF1700'
              }
            }
          ]
        }

      };
    },

    created() {
      applyStackBarRadius(this.chartOptionsStatus.series);
    }
  };
</script>


<style scoped>
  .round {
    width: 12px;
    height: 12px;
    border-radius: 100%;
    box-shadow: inset;
  }

  .radio-buttons-container {
    display: flex;
    align-items: center;
    gap: 30px;
  }

  .radio-button {
    display: inline-block;
    position: relative;
    cursor: pointer;
  }

  .radio-button__input {
    position: absolute;
    opacity: 0;
    width: 0;
    height: 0;
  }

  .radio-button__label {
    display: inline-block;
    padding-left: 30px;
    margin-bottom: 10px;
    position: relative;
    font-size: 12px;
    color: #fff;
    cursor: pointer;
    transition: all 0.3s cubic-bezier(0.23, 1, 0.320, 1);
  }

  .radio-button__custom {
    position: absolute;
    top: 50%;
    left: 0;
    transform: translateY(-50%);
    width: 20px;
    height: 20px;
    border-radius: 50%;
    border: 2px solid #555;
    transition: all 0.3s cubic-bezier(0.23, 1, 0.320, 1);
  }

  .radio-button__input:checked+.radio-button__label .radio-button__custom {
    transform: translateY(-50%) scale(0.9);
    border: 5px solid #00d3bb;
    color: #00d3bb;
  }

  .radio-button__input:checked+.radio-button__label {
    color: #00d3bb;
  }

  .radio-button__label:hover .radio-button__custom {
    transform: translateY(-50%) scale(1.2);
    border-color: #00d3bb;
    box-shadow: 0 0 10px #00d3bb80;
  }

  .button {
    --green: #00d3bb;
    font-size: 14px;
    padding: 0.5em 2.7em;
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

  .button-info {
    --blue: #00bafe;
    font-size: 14px;
    padding: 0.5em 2.7em;
    letter-spacing: 0.06em;
    position: relative;
    font-family: inherit;
    border-radius: 0.6em;
    overflow: hidden;
    transition: all 0.3s;
    line-height: 1.4em;
    border: 2px solid var(--blue);
    background: linear-gradient(to right, rgba(27, 170, 253, 0.1) 1%, transparent 40%, transparent 60%, rgba(27, 181, 253, 0.1) 100%);
    color: var(--blue);
    box-shadow: inset 0 0 10px rgba(27, 181, 253, 0.4), 0 0 9px 3px rgba(27, 219, 253, 0.1);
    height: 42px;
  }

  .button-info:hover {
    color: #00bafe;
    box-shadow: inset 0 0 10px rgba(27, 200, 253, 0.6), 0 0 9px 3px rgba(27, 170, 253, 0.2);
  }

  .button-info:before {
    content: "";
    position: absolute;
    left: -4em;
    width: 4em;
    height: 100%;
    top: 0;
    transition: transform .4s ease-in-out;
    background: linear-gradient(to right, transparent 1%, rgba(27, 227, 253, 0.1) 40%, rgba(27, 253, 215, 0.1) 60%, transparent 100%);
  }

  .button-info:hover:before {
    transform: translateX(15em);
  }

  html[data-theme='light'] .bg-base-300 {
      background-color: #15191e;
    }

    html[data-theme='light'] .table {
      color: white;
    }

    html[data-theme='light'] th {
      color: #eee;
    }

    html[data-theme='light'] .card-title {
      color: white;
    }
     html[data-theme='light'] .select{
      background-color: #eee;
    }

    html[data-theme='light'] .input{
      background-color: #eee;
    }

    html[data-theme='light'] .strip{
      color: #eee;
    }
</style>