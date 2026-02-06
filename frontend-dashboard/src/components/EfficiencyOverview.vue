  <template>
    <div class="efficiency-overview p-4">
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
              <div v-if="activeFilters.includes('process')">
                <select v-model="filters.process" class="select select-bordered select-sm">
                  <option disabled value="">{{$t('Choose Process')}}</option>
                  <option v-for="process in process" :key="process">{{ process }}</option>
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
      <!-- Efficiency Cards -->
      <div class="grid grid-flow-row-dense grid-cols-4 auto-rows-fr gap-4 ">
        <!-- comprehensive dynamic rate (%) -->
        <div class="card bg-base-300 shadow-xl flex flex-col h-full ">
          <v-chart :option="chartOptionsMorning"
            class="xl:h-[18rem] xl:w-[25rem] lg:h-[18rem] lg:w-[20rem] md:h-[18rem] md:w-[12rem]" />

          <div class="card-body items-center text-center mt-10">
            <h2 class="card-title">{{$t('Comprehensive Dynamic Rate (%)')}}</h2>
            <div class="card-actions">
              <button class="button btn btn-dash btn-accent"><span
                  class="text-white font-normal">{{$t('Detail')}}</span></button>
            </div>
          </div>
        </div>
        <!-- comprehensive machine time -->
        <div class="card bg-base-300 shadow-xl flex flex-col h-full">
          <div class="overflow-x-auto px-10 pt-10">
            <table class="table ">
              <!-- head -->
              <thead>
                <tr>
                  <th></th>
                  <th>{{$t('Time Category')}}</th>
                  <th>{{$t('Time Summary')}}</th>
                </tr>
              </thead>
              <tbody>
                <!-- row 1 -->
                <tr>
                  <th><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="size-4">
                      <path
                        d="M1 4.804a1 1 0 0 1 1.53-.848l5.113 3.196a1 1 0 0 1 0 1.696L2.53 12.044A1 1 0 0 1 1 11.196V4.804ZM13.5 4.5A.5.5 0 0 1 14 4h.5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-.5.5H14a.5.5 0 0 1-.5-.5v-7ZM10.5 4a.5.5 0 0 0-.5.5v7a.5.5 0 0 0 .5.5h.5a.5.5 0 0 0 .5-.5v-7A.5.5 0 0 0 11 4h-.5Z" />
                    </svg>
                  </th>
                  <td>{{$t('Running Time')}}</td>
                  <td>1.40 K</td>
                </tr>
                <!-- row 2 -->
                <tr>
                  <th><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="size-4">
                      <path fill-rule="evenodd"
                        d="M1.22 4.22a.75.75 0 0 1 1.06 0L6 7.94l2.761-2.762a.75.75 0 0 1 1.158.12 24.9 24.9 0 0 1 2.718 5.556l.729-1.261a.75.75 0 0 1 1.299.75l-1.591 2.755a.75.75 0 0 1-1.025.275l-2.756-1.591a.75.75 0 1 1 .75-1.3l1.097.634a23.417 23.417 0 0 0-1.984-4.211L6.53 9.53a.75.75 0 0 1-1.06 0L1.22 5.28a.75.75 0 0 1 0-1.06Z"
                        clip-rule="evenodd" />
                    </svg>
                  </th>
                  <td>{{$t('Down Time')}}</td>
                  <td>5.00</td>
                </tr>
                <!-- row 3 -->
                <tr>
                  <th><svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5"
                      stroke="currentColor" class="size-6">
                      <path stroke-linecap="round" stroke-linejoin="round"
                        d="M18.364 18.364A9 9 0 0 0 5.636 5.636m12.728 12.728A9 9 0 0 1 5.636 5.636m12.728 12.728L5.636 5.636" />
                    </svg>
                  </th>
                  <td>{{$t('Abnormal Time')}}</td>
                  <td>4.00</td>
                </tr>
                <!-- row 4 -->
                <tr>
                  <th><svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5"
                      stroke="currentColor" class="size-6">
                      <path stroke-linecap="round" stroke-linejoin="round"
                        d="M9 12.75 11.25 15 15 9.75M21 12a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z" />
                    </svg>
                  </th>
                  <td>{{$t('Standby Time')}}</td>
                  <td>27.00</td>
                </tr>
              </tbody>
            </table>
          </div>
          <div class="card-body items-center text-center mt-14  ">
            <h2 class="card-title">{{$t('Comprehensive Machine Time')}}</h2>
            <div class="card-actions">
              <button class="button btn btn-dash btn-accent"><span
                  class="text-white font-normal">{{$t('Detail')}}</span></button>
            </div>
          </div>
        </div>
        <!-- comprehensive dynamic rate trend (%) -->
        <div class="card bg-base-300 shadow-xl col-span-2 flex flex-col ">
          <v-chart :option="chartOptionsNight" class="w-full h-full" />
          <div class="card-body items-center text-center mt-10">
            <h2 class="card-title">{{$t('Comprehensive Dynamic Rate Trend (%)')}}</h2>
            <div class="card-actions">
              <button class="button btn btn-dash btn-accent"><span
                  class="text-white font-normal">{{$t('Detail')}}</span></button>
            </div>
          </div>
        </div>
      </div>
      <!-- Efficiency Cards 2 -->
      <div class="grid grid-flow-row-dense grid-cols-4 auto-rows-fr gap-4 mt-8 ">
        <!-- area -->
        <div class="card bg-base-300 shadow-xl flex flex-col h-full">
          <div class="card-body">
            <h2 class="card-title text-center">{{$t('Stamping Workshop')}}</h2>
            <ul class="timeline timeline-vertical mt-4">
              <li>
                <div class="timeline-start timeline-box">{{$t('Material In')}}</div>
                <div class="timeline-middle">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor"
                    class="text-info h-5 w-5">
                    <path fill-rule="evenodd"
                      d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.857-9.809a.75.75 0 00-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 10-1.06 1.061l2.5 2.5a.75.75 0 001.137-.089l4-5.5z"
                      clip-rule="evenodd" />
                  </svg>
                </div>
                <hr class="bg-info" />
              </li>
              <li>
                <hr class="bg-info" />
                <div class="timeline-middle">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor"
                    class="text-info h-5 w-5">
                    <path fill-rule="evenodd"
                      d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.857-9.809a.75.75 0 00-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 10-1.06 1.061l2.5 2.5a.75.75 0 001.137-.089l4-5.5z"
                      clip-rule="evenodd" />
                  </svg>
                </div>
                <div class="timeline-end timeline-box">{{$t('Blanking')}}</div>
                <hr class="bg-info" />
              </li>
              <li>
                <hr class="bg-info" />
                <div class="timeline-start timeline-box">{{$t('Punching')}}</div>
                <div class="timeline-middle">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor"
                    class="text-info h-5 w-5">
                    <path fill-rule="evenodd"
                      d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.857-9.809a.75.75 0 00-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 10-1.06 1.061l2.5 2.5a.75.75 0 001.137-.089l4-5.5z"
                      clip-rule="evenodd" />
                  </svg>
                </div>
                <hr />
              </li>
              <li>
                <hr />
                <div class="timeline-middle">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="h-5 w-5">
                    <path fill-rule="evenodd"
                      d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.857-9.809a.75.75 0 00-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 10-1.06 1.061l2.5 2.5a.75.75 0 001.137-.089l4-5.5z"
                      clip-rule="evenodd" />
                  </svg>
                </div>
                <div class="timeline-end timeline-box">{{$t('Bending/Forming')}}</div>
                <hr />
              </li>
              <li>
                <hr />
                <div class="timeline-start timeline-box">{{$t('Trimming')}}</div>
                <div class="timeline-middle">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="h-5 w-5">
                    <path fill-rule="evenodd"
                      d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.857-9.809a.75.75 0 00-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 10-1.06 1.061l2.5 2.5a.75.75 0 001.137-.089l4-5.5z"
                      clip-rule="evenodd" />
                  </svg>
                </div>
              </li>
              <li>
                <hr />
                <div class="timeline-middle">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="h-5 w-5">
                    <path fill-rule="evenodd"
                      d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.857-9.809a.75.75 0 00-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 10-1.06 1.061l2.5 2.5a.75.75 0 001.137-.089l4-5.5z"
                      clip-rule="evenodd" />
                  </svg>
                </div>
                <div class="timeline-end timeline-box">{{$t('Bending/Forming')}}</div>
                <hr />
              </li>
              <li>
                <hr />
                <div class="timeline-start timeline-box">{{$t('Output')}}</div>
                <div class="timeline-middle">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="h-5 w-5">
                    <path fill-rule="evenodd"
                      d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.857-9.809a.75.75 0 00-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 10-1.06 1.061l2.5 2.5a.75.75 0 001.137-.089l4-5.5z"
                      clip-rule="evenodd" />
                  </svg>
                </div>
              </li>
            </ul>
            <div class="card-body items-center text-center mt-auto">
              <h2 class="card-title">{{$t('Area')}}</h2>
              <div class="card-actions">
                <ul
                  class="menu menu-horizontal flex items-center justify-between bg-info bg-opacity-95 backdrop-blur-md rounded-full px-6 py-3 shadow-lg max-w-md mx-auto transition-all duration-300 hover:shadow-xl hover:bg-opacity-100">
                  <li>
                    <a class="tooltip tooltip-bottom" :data-tip="$t ('Stamping Workshop')">
                      <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="size-4">
                        <path
                          d="M5.268 14A2 2 0 0 0 7 15h4a2 2 0 0 0 2-2v-3a2 2 0 0 0-1-1.732V11a3 3 0 0 1-3 3H5.268ZM6.25 6h1.5V3.56l1.22 1.22a.75.75 0 1 0 1.06-1.06l-2.5-2.5a.75.75 0 0 0-1.06 0l-2.5 2.5a.75.75 0 0 0 1.06 1.06l1.22-1.22V6Z" />
                        <path
                          d="M6.25 8.75a.75.75 0 0 0 1.5 0V6H9a2 2 0 0 1 2 2v3a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V8a2 2 0 0 1 2-2h1.25v2.75Z" />
                      </svg>
                    </a>
                  </li>
                  <li>
                    <a class="tooltip tooltip-bottom" :data-tip="$t('Material Workshop')">
                      <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="size-4">
                        <path fill-rule="evenodd"
                          d="M2.742 2.755A2.25 2.25 0 0 1 4.424 2h7.152a2.25 2.25 0 0 1 1.682.755l1.174 1.32c.366.412.568.944.568 1.495v.68a2.25 2.25 0 0 1-2.25 2.25h-9.5A2.25 2.25 0 0 1 1 6.25v-.68c0-.55.202-1.083.568-1.495l1.174-1.32Zm1.682.745a.75.75 0 0 0-.561.252L2.753 5h2.212a1 1 0 0 1 .832.445l.406.61a1 1 0 0 0 .832.445h1.93a1 1 0 0 0 .832-.445l.406-.61A1 1 0 0 1 11.035 5h2.211l-1.109-1.248a.75.75 0 0 0-.56-.252H4.423Z"
                          clip-rule="evenodd" />
                        <path
                          d="M1 10.75a.75.75 0 0 1 .75-.75h3.215a1 1 0 0 1 .832.445l.406.61a1 1 0 0 0 .832.445h1.93a1 1 0 0 0 .832-.445l.406-.61a1 1 0 0 1 .832-.445h3.215a.75.75 0 0 1 .75.75v1A2.25 2.25 0 0 1 12.75 14h-9.5A2.25 2.25 0 0 1 1 11.75v-1Z" />
                      </svg>
                    </a>
                  </li>
                  <li>
                    <a class="tooltip tooltip-bottom" data-tip="Stats">
                      <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="size-4">
                        <path fill-rule="evenodd"
                          d="M8 3.5c-.771 0-1.537.022-2.297.066a1.124 1.124 0 0 0-1.0 58 1.028l-.018.214a.75.75 0 1 1-1.495-.12l.018-.221a2.624 2.624 0 0 1 2.467-2.399 41.628 41.628 0 0 1 4.766 0 2.624 2.624 0 0 1 2.467 2.399c.056.662.097 1.329.122 2l.748-.748a.75.75 0 1 1 1.06 1.06l-2 2.001a.75.75 0 0 1-1.061 0l-2-1.999a.75.75 0 0 1 1.061-1.06l.689.688a39.89 39.89 0 0 0-.114-1.815 1.124 1.124 0 0 0-1.058-1.028A40.138 40.138 0 0 0 8 3.5ZM3.22 7.22a.75.75 0 0 1 1.061 0l2 2a.75.75 0 1 1-1.06 1.06l-.69-.69c.025.61.062 1.214.114 1.816.048.56.496.996 1.058 1.028a40.112 40.112 0 0 0 4.594 0 1.124 1.124 0 0 0 1.058-1.028 39.2 39.2 0 0 0 .018-.219.75.75 0 1 1 1.495.12l-.018.226a2.624 2.624 0 0 1-2.467 2.399 41.648 41.648 0 0 1-4.766 0 2.624 2.624 0 0 1-2.467-2.399 41.395 41.395 0 0 1-.122-2l-.748.748A.75.75 0 1 1 1.22 9.22l2-2Z"
                          clip-rule="evenodd" />
                      </svg>
                    </a>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>

        <!-- comprehensive duty rate (%) -->
        <div class="card bg-base-300 shadow-xl flex flex-col h-full ">
          <v-chart :option="chartOptionsDay"
            class="xl:h-[28rem] xl:w-[25rem] lg:h-[28rem] lg:w-[20rem] md:h-[28rem] md:w-[12rem]" />
          <div class="card-body items-center text-center mt-auto">
            <h2 class="card-title">{{$t('Comprehensive Duty Rate (%)')}}</h2>
            <div class="card-actions">
              <button class="button-info btn btn-dash btn-info"><span
                  class="text-white font-normal">{{$t('Detail')}}</span></button>
            </div>
          </div>
        </div>
        <!-- trend of workshop mobility during shift (%) -->
        <div class="card bg-base-300 shadow-xl col-span-2 flex flex-col ">
          <v-chart :option="chartOptionsMobilityLive" class="w-full h-full" />
          <div class="card-body items-center text-center mt-auto">
            <h2 class="card-title">{{$t('Trend of Workshop Mobility During Shift (%)')}}</h2>
            <div class="card-actions">
              <button class="button-info btn btn-dash btn-info"><span
                  class="text-white font-normal">{{$t('Detail')}}</span></button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </template>

  <script lang="ts">
    import 'echarts'
    import {
      CanvasRenderer
    } from 'echarts/renderers'
    import {
      PieChart,
      LineChart
    } from 'echarts/charts'
    import {
      GridComponent,
      TooltipComponent,
      LegendComponent,
      GraphicComponent
    } from 'echarts/components'
    import * as echart from 'echarts/core'
    import * as echarts from 'echarts';
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
      data() {
        return {
          selected: 'Group A'  as string | null,
          selectedArea: '',
          selectedProcess: '',
          showDropdown: false,
          selectedShift: '',
          filters: {
            floor: '',
            dateFrom: '',
            dateUntil: '',
            area: '',
            process: ''
          },
          activeFilters: ['area', 'process', 'date', 'floor'],
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
          process: [this.$t('Stamping Process'), this.$t('Production Process'), this.$t('Machining Process')],
          chartOptionsMorning: {
            series: [{
                type: 'gauge',
                center: ['50%', '60%'],
                startAngle: 90,
                endAngle: -270,
                min: 0,
                max: 100,
                splitNumber: 5,
                itemStyle: {
                  color: '#00d3bb'
                },
                progress: {
                  show: true,
                  width: 30
                },
                pointer: {
                  show: false
                },
                axisLine: {
                  lineStyle: {
                    width: 30
                  }
                },
                axisTick: {
                  show: false

                },
                splitLine: {
                  show: false
                },
                axisLabel: {
                  show: false
                },
                anchor: {
                  show: false
                },
                title: {
                  show: false
                },
                detail: {
                  valueAnimation: true,
                  width: '60%',
                  lineHeight: 40,
                  borderRadius: 8,
                  offsetCenter: ['5%', '5%'],
                  fontSize: 30,
                  fontWeight: 'bolder',
                  formatter: '{value} %',
                  color: 'inherit'
                },
                data: [{
                  value: 80
                }]
              },
              {
                type: 'gauge',
                center: ['50%', '60%'],
                startAngle: 90,
                endAngle: -270,
                min: 0,
                max: 100,
                itemStyle: {
                  color: '#49fce8'
                },
                progress: {
                  show: true,
                  width: 6
                },

                pointer: {
                  show: false
                },
                axisLine: {
                  show: false
                },
                axisTick: {
                  show: false
                },
                splitLine: {
                  show: false
                },
                axisLabel: {
                  show: false
                },
                detail: {
                  show: false
                },
                data: [{
                  value: 80
                }]
              }
            ]
          },
          chartOptionsNight: {
            backgroundColor: 'transparent',
            tooltip: {
              trigger: 'axis',
              axisPointer: {
                type: 'line'
              },
              formatter: (params: any[]) => {
                const point = params[0];
                const date = new Date(point.value[0]);
                const options: Intl.DateTimeFormatOptions = {
                  year: 'numeric',
                  month: 'short',
                  day: 'numeric'
                };
                const dateStr = date.toLocaleDateString('en-US', options);
                return `${dateStr}<br/>${point.seriesName}: ${point.value[1]}%`;
              }
            },
            xAxis: {
              type: 'time',
              axisLine: {
                lineStyle: {
                  color: '#FFFFFF'
                }
              },
              axisLabel: {
                color: '#FFFFFF',
                textShadowColor: 'rgba(255, 255, 255, 0.7)',
                textShadowBlur: 10,
                fontSize: 10,
                formatter: (value: number) => {
                  const date = new Date(value);
                  return `${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()}`;
                }
              },
              splitLine: {
                lineStyle: {
                  color: '#454545'
                }
              }
            },
            yAxis: {
              type: 'value',
              name: 'Percentage(%)',
              nameTextStyle: {
                color: '#FFFFFF',
                textShadowColor: 'rgba(255, 255, 255, 0.7)',
                textShadowBlur: 10
              },
              axisLabel: {
                color: '#FFFFFF',
                textShadowColor: 'rgba(255, 255, 255, 0.7)',
                textShadowBlur: 10,
                fontSize: 10
              },
              splitLine: {
                lineStyle: {
                  color: '#454545'
                }
              },
            },
            grid: {
              left: '10%',
              right: '10%',
              bottom: '15%',
              top: '20%'
            },
            series: [{
              name: 'Jam Produksi',
              type: 'line',
              smooth: false,
              showSymbol: true,
              symbolSize: 8,
              itemStyle: {
                color: '#00d3bb'
              },
              lineStyle: {
                color: '#00d3bb'
              },
              areaStyle: {
                color: {
                  type: 'linear',
                  x: 0,
                  y: 0,
                  x2: 0,
                  y2: 1,
                  colorStops: [{
                      offset: 0,
                      color: '#00d3bb'
                    },
                    {
                      offset: 1,
                      color: 'rgba(0, 211, 187, 0)'
                    }
                  ]
                }
              },
              data: [
                [Date.UTC(2024, 0, 1), 68.2],
                [Date.UTC(2024, 1, 1), 90.1],
                [Date.UTC(2024, 2, 1), 66.5],
                [Date.UTC(2024, 3, 1), 91.3],
                [Date.UTC(2024, 4, 1), 63.8]
              ]
            }]
          },
          chartOptionsDay: {
            series: [{
                type: 'gauge',
                center: ['50%', '60%'],
                startAngle: 90,
                endAngle: -270,
                min: 0,
                max: 100,
                splitNumber: 5,
                itemStyle: {
                  color: '#1FC3FF'
                },
                progress: {
                  show: true,
                  width: 30
                },
                pointer: {
                  show: false
                },
                axisLine: {
                  lineStyle: {
                    width: 30
                  }
                },
                axisTick: {
                  show: false
                },
                splitLine: {
                  show: false
                },
                axisLabel: {
                  show: false
                },
                anchor: {
                  show: false
                },
                title: {
                  show: false
                },
                detail: {
                  valueAnimation: true,
                  width: '60%',
                  lineHeight: 40,
                  borderRadius: 8,
                  offsetCenter: ['5%', '5%'],
                  fontSize: 30,
                  fontWeight: 'bolder',
                  formatter: '{value} %',
                  color: 'inherit'
                },
                data: [{
                  value: 60
                }]
              },
              {
                type: 'gauge',
                center: ['50%', '60%'],
                startAngle: 90,
                endAngle: -270,
                min: 0,
                max: 100,
                itemStyle: {
                  color: '#5CD3FF'
                },
                progress: {
                  show: true,
                  width: 6
                },
                pointer: {
                  show: false
                },
                axisLine: {
                  show: false
                },
                axisTick: {
                  show: false
                },
                splitLine: {
                  show: false
                },
                axisLabel: {
                  show: false
                },
                detail: {
                  show: false
                },
                data: [{
                  value: 60
                }]
              }
            ]
          },
          chartOptionsMobilityLive: {
            backgroundColor: 'transparent',
            title: {
              text: '',
              textStyle: {
                color: '#fff'
              }
            },
            tooltip: {
              trigger: 'axis',
              axisPointer: {
                type: 'line'
              },
              formatter: function (params: any[]) {
                const point = params[0];
                const date = new Date(point.value[0]);
                const timeStr = date.toLocaleTimeString();
                return `${timeStr}<br/><b>${point.seriesName}: ${point.value[1].toFixed(2)}%</b>`;
              }
            },
            xAxis: {
              type: 'time',
              name: 'Time',
              nameTextStyle: {
                color: '#ccc'
              },
              axisLine: {
                lineStyle: {
                  color: '#ccc'
                }
              },
              axisLabel: {
                color: '#FFFFFF',
                textShadowColor: 'rgba(255, 255, 255, 0.7)',
                textShadowBlur: 10,
                fontSize: 10
              },
              splitLine: {
                lineStyle: {
                  color: '#454545'
                }
              }
            },
            yAxis: {
              type: 'value',
              name: 'Mobility (%)',
              nameTextStyle: {
                color: '#FFFFFF',
                textShadowColor: 'rgba(255, 255, 255, 0.7)',
                textShadowBlur: 10,
                fontSize: 10
              },
              axisLabel: {
                formatter: '{value} %',
                color: '#FFFFFF',
                textShadowColor: 'rgba(255, 255, 255, 0.7)',
                textShadowBlur: 10,
                fontSize: 10
              },
              max: 100,
              splitLine: {
                lineStyle: {
                  color: '#454545'
                }
              }
            },
            grid: {
              left: '10%',
              right: '10%',
              bottom: '15%',
              top: '10%'
            },
            series: [{
              name: 'Mobility',
              type: 'line',
              smooth: true,
              showSymbol: true,
              symbolSize: 6,
              itemStyle: {
                color: '#99E4FF'
              },
              lineStyle: {
                color: '#99E4FF'
              },
              areaStyle: {
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                    offset: 0,
                    color: '#99E4FF'
                  },
                  {
                    offset: 1,
                    color: '#33C9FF'
                  }
                ])
              },
              data: (function () {
                let data = [];
                let time = (new Date()).getTime() - 19 * 5000;
                for (let i = 0; i < 20; i++) {
                  data.push([time, Math.random() * 100]);
                  time += 5000;
                }
                return data;
              })()
            }]
          },
        };
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

    html[data-theme='light'] .timeline-middle svg{
      fill: white;
    }

    html[data-theme='light'] .tooltip svg{
      fill: white;
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