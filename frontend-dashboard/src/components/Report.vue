<template>
  <div class="report p-4 font-inter">
      <transition name="fade">
            <div v-if="showAlert" role="alert"
                class="absolute z-10 bottom-20 right-4 alert alert-warning alert-soft transition duration-700 ease-in-out">
                <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 shrink-0 stroke-current " fill="none"
                    viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                        d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z" />
                </svg>
                <span>Sample data – actual data not loaded yet.</span>
            </div>
        </transition>
    <div v-if="!currentComponent">
      <div class="tabs tabs-border">
        <!-- Last Week -->
        <input type="radio" name="my_tabs_2" class="tab" aria-label="Last Week" checked />
        <div class="tab-content border-base-300 bg-base-100 p-10 ">
          <!-- Cards Menu -->
          <div class="cards justify-center">
            <div class="card bg-base-200 p-6" :class="{ active: activeIndex === 0 }" @click="setActive(0)">
              <div class="grid grid-flow-col grid-rows-3 gap-4">
                <div class="row-span-3 h-full text-5xl">
                  82
                </div>
                <div class="col-span-2 text-lg font-semibold">
                  Achieve Output
                </div>
                <div class="col-span-2 text-xs row-span-2 text-gray-400 ">
                  Highest UPH Since Last Week
                </div>
              </div>
            </div>
            <div class="card bg-base-200 p-6" :class="{ active: activeIndex === 1 }" @click="setActive(1)">
              <div class="grid grid-flow-col grid-rows-3 gap-4">
                <div class="row-span-3 h-full text-5xl">
                  97%
                </div>
                <div class="col-span-2 text-lg font-semibold">
                  Yield
                </div>
                <div class="col-span-2 text-xs row-span-2 text-gray-400 ">
                  Average Since Last Week
                </div>
              </div>
            </div>
            <div class="card bg-base-200 p-6" :class="{ active: activeIndex === 2 }" @click="setActive(2)">
              <div class="grid grid-flow-col grid-rows-3 gap-4">
                <div class="row-span-3 h-full text-5xl">
                  17%
                </div>
                <div class="col-span-2 text-lg font-semibold">
                  Retest Rate
                </div>
                <div class="col-span-2 text-xs row-span-2 text-gray-400 ">
                  Worst Since Last Week
                </div>
              </div>
            </div>
          </div>

          <!-- Chart  -->
          <div class="mt-10">
            <v-chart v-if="activeIndex === 0" :option="chartOptionsAchieveOutput" autoresize style="height: 400px">
            </v-chart>
            <v-chart v-else-if="activeIndex === 1" :option="chartOptionsYield" autoresize style="height: 400px">
            </v-chart>
            <v-chart v-if="activeIndex === 2" :option="chartOptionsRetest" autoresize style="height: 400px" />
          </div>
        </div>

        <!-- Last Month -->
        <input type="radio" name="my_tabs_2" class="tab" aria-label="Last Month" />
        <div class="tab-content border-base-300 bg-base-100 p-10">
          <!-- Cards Menu -->
          <div class="cards justify-center">
            <div class="card bg-base-200 p-6" :class="{ active: activeIndex === 0 }" @click="setActive(0)">
              <div class="grid grid-flow-col grid-rows-3 gap-4">
                <div class="row-span-3 h-full text-5xl">
                  467
                </div>
                <div class="col-span-2 text-lg font-semibold">
                  Achieve Output
                </div>
                <div class="col-span-2 text-xs row-span-2 text-gray-400 ">
                  Highest UPH Since Last Month
                </div>
              </div>
            </div>
            <div class="card bg-base-200 p-6" :class="{ active: activeIndex === 1 }" @click="setActive(1)">
              <div class="grid grid-flow-col grid-rows-3 gap-4">
                <div class="row-span-3 h-full text-5xl">
                  45%
                </div>
                <div class="col-span-2 text-lg font-semibold">
                  Yield
                </div>
                <div class="col-span-2 text-xs row-span-2 text-gray-400 ">
                  Average Since Last Month
                </div>
              </div>
            </div>
            <div class="card bg-base-200 p-6" :class="{ active: activeIndex === 2 }" @click="setActive(2)">
              <div class="grid grid-flow-col grid-rows-3 gap-4">
                <div class="row-span-3 h-full text-5xl">
                  24%
                </div>
                <div class="col-span-2 text-lg font-semibold">
                  Retest Rate
                </div>
                <div class="col-span-2 text-xs row-span-2 text-gray-400 ">
                  Worst Since Last Month
                </div>
              </div>
            </div>
          </div>

          <!-- Chart  -->
          <div class="mt-10">
            <v-chart v-if="activeIndex === 0" :option="chartOptionsAchieveOutputMonthly" autoresize
              style="height: 400px"></v-chart>
            <v-chart v-else-if="activeIndex === 1" :option="chartOptionsYieldMonthly" autoresize style="height: 400px">
            </v-chart>
            <v-chart v-else-if="activeIndex === 2" :option="chartOptionsRetestMonthly" autoresize style="height: 400px">
            </v-chart>
          </div>
        </div>

        <!-- Last Year -->
        <input type="radio" name="my_tabs_2" class="tab" aria-label="Last Year" />
        <div class="tab-content border-base-300 bg-base-100 p-10">
          <!-- Cards Menu -->
          <div class="cards justify-center">
            <div class="card bg-base-200 p-6" :class="{ active: activeIndex === 0 }" @click="setActive(0)">
              <div class="grid grid-flow-col grid-rows-3 gap-4">
                <div class="row-span-3 h-full text-5xl">
                  867
                </div>
                <div class="col-span-2 text-lg font-semibold">
                  Achieve Output
                </div>
                <div class="col-span-2 text-xs row-span-2 text-gray-400 ">
                  Highest UPH Since Last Year
                </div>
              </div>
            </div>
            <div class="card bg-base-200 p-6" :class="{ active: activeIndex === 1 }" @click="setActive(1)">
              <div class="grid grid-flow-col grid-rows-3 gap-4">
                <div class="row-span-3 h-full text-5xl">
                  85%
                </div>
                <div class="col-span-2 text-lg font-semibold">
                  Yield
                </div>
                <div class="col-span-2 text-xs row-span-2 text-gray-400 ">
                  Average Since Last Year
                </div>
              </div>
            </div>
            <div class="card bg-base-200 p-6" :class="{ active: activeIndex === 2 }" @click="setActive(2)">
              <div class="grid grid-flow-col grid-rows-3 gap-4">
                <div class="row-span-3 h-full text-5xl">
                  38%
                </div>
                <div class="col-span-2 text-lg font-semibold">
                  Retest Rate
                </div>
                <div class="col-span-2 text-xs row-span-2 text-gray-400 ">
                  Worst Since Last Year
                </div>
              </div>
            </div>
          </div>

          <!-- Chart  -->
          <div class="mt-10">
            <v-chart v-if="activeIndex === 0" :option="chartOptionsAchieveOutputYear" autoresize style="height: 400px">
            </v-chart>
            <v-chart v-else-if="activeIndex === 1" :option="chartOptionsYieldYear" autoresize style="height: 400px">
            </v-chart>
            <v-chart v-else-if="activeIndex === 2" :option="chartOptionsRetestYear" autoresize style="height: 400px">
            </v-chart>
          </div>
        </div>
      </div>
    </div>
    <component :is="currentComponent" :key="$route.fullPath" v-else />
  </div>
</template>

<script lang="ts">
  import DailyReport from './DailyReport.vue'
  import MonthlyReport from './MonthlyReport.vue'
  import WeeklyReport from './WeeklyReport.vue'

  import * as echarts from 'echarts';
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

  export default {
    data() {
      return {
        activeIndex: 0,
        chartOptionsAchieveOutput: {
          backgroundColor: 'transparent',
          title: {
            text: null
          },
          tooltip: {
            trigger: 'axis',
            axisPointer: {
              type: 'shadow'
            },
            formatter: function (params: any[]) {
              let tooltipText = `<b>${params[0].axisValue}</b><br/>`;
              params.forEach(item => {
                tooltipText += `${item.seriesName}: ${item.data}<br/>`;
              });
              return tooltipText;
            }
          },
          legend: {
            textStyle: {
              color: '#FFFFFF'
            }
          },
          grid: {
            left: '10%',
            right: '10%',
            bottom: '15%',
            containLabel: true
          },
          xAxis: {
            type: 'category',
            data: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'],
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLine: {
              lineStyle: {
                color: '#FFFFFF'
              }
            },
            splitLine: {
              show: true,
              lineStyle: {
                color: '#454545'
              }
            },
            name: '7/7/2025 - 13/7/2025',
            nameTextStyle: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            }
          },
          yAxis: {
            type: 'value',
            min: 0,
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLine: {
              lineStyle: {
                color: '#FFFFFF'
              }
            },
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            },
            name: 'Production',
            nameTextStyle: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            }
          },
          color: ['#4090A0', '#50A8B9', '#6DB6C5', '#8AC5D0', '#A7D4DC', '#C5E2E8'],
          series: [{
              name: 'Line 1',
              type: 'bar',
              data: [5, 3, 4, 7, 2, 5, 3],
              barGap: 0,
              barCategoryGap: '20%'
            },
            {
              name: 'Line 2',
              type: 'bar',
              data: [2, 2, 3, 2, 1, 2, 1]
            },
            {
              name: 'Line 3',
              type: 'bar',
              data: [3, 4, 4, 2, 5, 3, 4]
            },
            {
              name: 'Line 4',
              type: 'bar',
              data: [3, 4, 4, 2, 5, 3, 4]
            },
            {
              name: 'Line 5',
              type: 'bar',
              data: [3, 4, 4, 2, 5, 3, 4]
            },
            {
              name: 'Line 6',
              type: 'bar',
              data: [3, 4, 4, 2, 5, 3, 4]
            }
          ]
        },
        chartOptionsYield: {
          backgroundColor: 'transparent',
          color: ['#4090A0', '#50A8B9', '#6DB6C5', '#8AC5D0', '#A7D4DC'],
          title: {
            text: null
          },
          tooltip: {
            trigger: 'axis',
            axisPointer: {
              type: 'cross',
              label: {
                backgroundColor: '#6a7985'
              }
            }
          },
          legend: {
            textStyle: {
              color: '#FFFFFF'
            }
          },
          grid: {
            left: '10%',
            right: '10%',
            bottom: '15%',
            containLabel: true
          },
          xAxis: {
            type: 'category',
            boundaryGap: false,
            data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            },
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            }
          },
          yAxis: {
            type: 'value',
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            },
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            }
          },
          series: [{
              name: 'Line 1',
              type: 'line',
              smooth: true,
              showSymbol: false,
              lineStyle: {
                width: 2
              },
              areaStyle: {
                opacity: 0.8,
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                    offset: 0,
                    color: '#4090A0'
                  },
                  {
                    offset: 1,
                    color: 'rgba(64, 144, 160, 0)'
                  }
                ])
              },
              emphasis: {
                focus: 'series'
              },
              data: [140, 232, 101, 264, 90, 340, 250]
            },
            {
              name: 'Line 2',
              type: 'line',
              smooth: true,
              showSymbol: false,
              lineStyle: {
                width: 2
              },
              areaStyle: {
                opacity: 0.8,
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                    offset: 0,
                    color: '#50A8B9'
                  },
                  {
                    offset: 1,
                    color: 'rgba(80, 168, 185, 0)'
                  }
                ])
              },
              emphasis: {
                focus: 'series'
              },
              data: [120, 282, 111, 234, 220, 340, 310]
            },
            {
              name: 'Line 3',
              type: 'line',
              smooth: true,
              showSymbol: false,
              lineStyle: {
                width: 2
              },
              areaStyle: {
                opacity: 0.8,
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                    offset: 0,
                    color: '#6DB6C5'
                  },
                  {
                    offset: 1,
                    color: 'rgba(109, 182, 197, 0)'
                  }
                ])
              },
              emphasis: {
                focus: 'series'
              },
              data: [320, 132, 201, 334, 190, 130, 220]
            },
            {
              name: 'Line 4',
              type: 'line',
              smooth: true,
              showSymbol: false,
              lineStyle: {
                width: 2
              },
              areaStyle: {
                opacity: 0.8,
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                    offset: 0,
                    color: '#8AC5D0'
                  },
                  {
                    offset: 1,
                    color: 'rgba(138, 197, 208, 0)'
                  }
                ])
              },
              emphasis: {
                focus: 'series'
              },
              data: [220, 402, 231, 134, 190, 230, 120]
            },
            {
              name: 'Line 5',
              type: 'line',
              smooth: true,
              showSymbol: false,
              lineStyle: {
                width: 2
              },
              label: {
                show: true,
                position: 'top'
              },
              areaStyle: {
                opacity: 0.8,
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                    offset: 0,
                    color: '#A7D4DC'
                  },
                  {
                    offset: 1,
                    color: 'rgba(167, 212, 220, 0)'
                  }
                ])
              },
              emphasis: {
                focus: 'series'
              },
              data: [220, 302, 181, 234, 210, 290, 150]
            }
          ]
        },
        chartOptionsAchieveOutputMonthly: {
          backgroundColor: 'rgba(0, 0, 0, 0)',
          tooltip: {
            trigger: 'axis',
            axisPointer: {
              type: 'shadow'
            },
            formatter: (params: any[]) => {
              const day = params[0] ?.axisValue;
              let result = `<b>Day ${day}</b><br/>`;
              params.forEach(p => {
                result += `${p.seriesName}: ${p.value}<br/>`;
              });
              return result;
            }
          },
          legend: {
            textStyle: {
              color: '#FFFFFF'
            }
          },
          xAxis: {
            type: 'category',
            data: Array.from({
              length: 30
            }, (_, i) => (i + 1).toString()),
            name: '(1–30)',
            nameTextStyle: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLine: {
              lineStyle: {
                color: '#454545'
              }
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
            name: 'Production',
            nameTextStyle: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLine: {
              lineStyle: {
                color: '#454545'
              }
            },
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            },

          },
          grid: {
            left: '5%',
            right: '5%',
            bottom: '10%',
            containLabel: true
          },
          color: ['#4090A0', '#50A8B9', '#6DB6C5', '#8AC5D0', '#A7D4DC', '#C5E2E8'],
          series: [{
              name: 'Line 1',
              type: 'bar',
              stack: 'total',
              barGap: 0,
              emphasis: {
                focus: 'series'
              },
              data: Array(30).fill(0).map((_, i) => 30 + (i % 5) * 2)
            },
            {
              name: 'Line 2',
              type: 'bar',
              stack: 'total',
              barGap: 0,
              emphasis: {
                focus: 'series'
              },
              data: Array(30).fill(0).map((_, i) => 25 + (i % 4) * 3)
            },
            {
              name: 'Line 3',
              type: 'bar',
              stack: 'total',
              barGap: 0,
              emphasis: {
                focus: 'series'
              },
              data: Array(30).fill(0).map((_, i) => 20 + (i % 6) * 2)
            },
            {
              name: 'Line 4',
              type: 'bar',
              stack: 'total',
              barGap: 0,
              emphasis: {
                focus: 'series'
              },
              data: Array(30).fill(0).map((_, i) => 15 + (i % 5) * 2)
            },
            {
              name: 'Line 5',
              type: 'bar',
              stack: 'total',
              barGap: 0,
              emphasis: {
                focus: 'series'
              },
              data: Array(30).fill(0).map((_, i) => 15 + (i % 5) * 2)
            },
            {
              name: 'Line 6',
              type: 'bar',
              stack: 'total',
              barGap: 0,
              emphasis: {
                focus: 'series'
              },
              data: Array(30).fill(0).map((_, i) => 20 + (i % 6) * 2)
            }
          ]
        },
        chartOptionsYieldMonthly: {
          backgroundColor: 'rgba(0, 0, 0, 0)',
          tooltip: {
            trigger: 'axis',
          },
          legend: {
            textStyle: {
              color: '#FFFFFF'
            }
          },
          xAxis: {
            type: 'category',
            data: Array.from({
              length: 30
            }, (_, i) => (i + 1).toString()),
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLine: {
              lineStyle: {
                color: '#454545'
              }
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
            max: 100,
            name: 'Persentase (%)',
            nameTextStyle: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLine: {
              lineStyle: {
                color: '#454545'
              }
            },
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            }
          },
          grid: {
            left: '5%',
            right: '5%',
            bottom: '10%',
            containLabel: true
          },
          color: ['#4090A0', '#50A8B9', '#6DB6C5', '#8AC5D0', '#A7D4DC', '#C5E2E8'],
          series: [{
              name: 'Line 1',
              type: 'line',
              smooth: true,
              areaStyle: {
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                    offset: 0,
                    color: '#6EB6C5'
                  },
                  {
                    offset: 1,
                    color: 'rgba(110, 182, 197, 0)'
                  }
                ])
              },
              lineStyle: {
                color: '#4090A0'
              },
              data: Array(30).fill(0).map((_, i) => 80 + (i % 5) * 2)
            },
            {
              name: 'Line 2',
              type: 'line',
              smooth: true,
              areaStyle: {
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                    offset: 0,
                    color: '#6EB6C5'
                  },
                  {
                    offset: 1,
                    color: 'rgba(110, 182, 197, 0)'
                  }
                ])
              },
              lineStyle: {
                color: '#50A8B9'
              },
              data: Array(30).fill(0).map((_, i) => 78 + (i % 4) * 2)
            },
            {
              name: 'Line 3',
              type: 'line',
              smooth: true,
              areaStyle: {
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                    offset: 0,
                    color: '#6EB6C5'
                  },
                  {
                    offset: 1,
                    color: 'rgba(110, 182, 197, 0)'
                  }
                ])
              },
              lineStyle: {
                color: '#6DB6C5'
              },
              data: Array(30).fill(0).map((_, i) => 76 + (i % 6) * 2)
            },
            {
              name: 'Line 4',
              type: 'line',
              smooth: true,
              areaStyle: {
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                    offset: 0,
                    color: '#6EB6C5'
                  },
                  {
                    offset: 1,
                    color: 'rgba(110, 182, 197, 0)'
                  }
                ])
              },
              lineStyle: {
                color: '#8AC5D0'
              },
              data: Array(30).fill(0).map((_, i) => 82 + (i % 5) * 2)
            },
            {
              name: 'Line 5',
              type: 'line',
              smooth: true,
              areaStyle: {
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                    offset: 0,
                    color: '#6EB6C5'
                  },
                  {
                    offset: 1,
                    color: 'rgba(110, 182, 197, 0)'
                  }
                ])
              },
              lineStyle: {
                color: '#A7D4DC'
              },
              data: Array(30).fill(0).map((_, i) => 79 + (i % 4) * 2)
            },
            {
              name: 'Line 6',
              type: 'line',
              smooth: true,
              areaStyle: {
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                    offset: 0,
                    color: '#6EB6C5'
                  },
                  {
                    offset: 1,
                    color: 'rgba(110, 182, 197, 0)'
                  }
                ])
              },
              lineStyle: {
                color: '#C5E2E8'
              },
              data: Array(30).fill(0).map((_, i) => 77 + (i % 6) * 2)
            }
          ],
        },
        chartOptionsAchieveOutputYear: {
          backgroundColor: 'rgba(0, 0, 0, 0)',
          tooltip: {
            trigger: 'axis',
            axisPointer: {
              type: 'shadow'
            },
            formatter: (params: any[]) => {
              const day = params[0] ?.axisValue;
              let result = `<b>Day ${day}</b><br/>`;
              params.forEach(p => {
                result += `${p.seriesName}: ${p.value}<br/>`;
              });
              return result;
            }
          },
          legend: {
            textStyle: {
              color: '#FFFFFF'
            }
          },
          xAxis: {
            type: 'category',
            data: [
              'January', 'February', 'March', 'April', 'May',
              'Juny', 'July', 'August', 'September',
              'October', 'November', 'December'
            ],
            name: '(2025)',
            nameTextStyle: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLine: {
              lineStyle: {
                color: '#454545'
              }
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
            name: 'Production',
            nameTextStyle: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLine: {
              lineStyle: {
                color: '#454545'
              }
            },
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            }
          },
          grid: {
            left: '5%',
            right: '5%',
            bottom: '10%',
            containLabel: true
          },
          color: ['#4090A0', '#50A8B9', '#6DB6C5', '#8AC5D0', '#A7D4DC', '#C5E2E8'],
          series: [{
              name: 'Line 1',
              type: 'bar',
              stack: 'total',
              barGap: 0,
              emphasis: {
                focus: 'series'
              },
              data: Array(30).fill(0).map((_, i) => 30 + (i % 5) * 2)
            },
            {
              name: 'Line 2',
              type: 'bar',
              stack: 'total',
              barGap: 0,
              emphasis: {
                focus: 'series'
              },
              data: Array(30).fill(0).map((_, i) => 25 + (i % 4) * 3)
            },
            {
              name: 'Line 3',
              type: 'bar',
              stack: 'total',
              barGap: 0,
              emphasis: {
                focus: 'series'
              },
              data: Array(30).fill(0).map((_, i) => 20 + (i % 6) * 2)
            },
            {
              name: 'Line 4',
              type: 'bar',
              stack: 'total',
              barGap: 0,
              emphasis: {
                focus: 'series'
              },
              data: Array(30).fill(0).map((_, i) => 15 + (i % 5) * 2)
            },
            {
              name: 'Line 5',
              type: 'bar',
              stack: 'total',
              barGap: 0,
              emphasis: {
                focus: 'series'
              },
              data: Array(30).fill(0).map((_, i) => 15 + (i % 5) * 2)
            },
            {
              name: 'Line 6',
              type: 'bar',
              stack: 'total',
              barGap: 0,
              emphasis: {
                focus: 'series'
              },
              data: Array(30).fill(0).map((_, i) => 20 + (i % 6) * 2)
            }
          ]
        },
        chartOptionsYieldYear: {
          backgroundColor: 'transparent',
          color: ['#4090A0', '#50A8B9', '#6DB6C5', '#8AC5D0', '#A7D4DC'],
          title: {
            text: null
          },
          tooltip: {
            trigger: 'axis',
            axisPointer: {
              type: 'cross',
              label: {
                backgroundColor: '#6a7985'
              }
            }
          },
          legend: {
            textStyle: {
              color: '#FFFFFF'
            }
          },
          grid: {
            left: '10%',
            right: '10%',
            bottom: '15%',
            containLabel: true
          },
          xAxis: {
            type: 'category',
            boundaryGap: false,
            data: [
              'January', 'February', 'March', 'April', 'May',
              'Juny', 'July', 'August', 'September',
              'October', 'November', 'December'
            ],
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            },
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            }
          },
          yAxis: {
            type: 'value',
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            },
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            }
          },
          series: [{
              name: 'Line 1',
              type: 'line',
              smooth: true,
              showSymbol: false,
              lineStyle: {
                width: 2
              },
              areaStyle: {
                opacity: 0.8,
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                    offset: 0,
                    color: '#4090A0'
                  },
                  {
                    offset: 1,
                    color: 'rgba(64, 144, 160, 0)'
                  }
                ])
              },
              emphasis: {
                focus: 'series'
              },
              data: [140, 232, 101, 264, 90, 340, 250, 101, 264, 90, 340, 250]
            },
            {
              name: 'Line 2',
              type: 'line',
              smooth: true,
              showSymbol: false,
              lineStyle: {
                width: 2
              },
              areaStyle: {
                opacity: 0.8,
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                    offset: 0,
                    color: '#50A8B9'
                  },
                  {
                    offset: 1,
                    color: 'rgba(80, 168, 185, 0)'
                  }
                ])
              },
              emphasis: {
                focus: 'series'
              },
              data: [120, 282, 111, 234, 220, 340, 310, 111, 234, 220, 340, 310]
            },
            {
              name: 'Line 3',
              type: 'line',
              smooth: true,
              showSymbol: false,
              lineStyle: {
                width: 2
              },
              areaStyle: {
                opacity: 0.8,
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                    offset: 0,
                    color: '#6DB6C5'
                  },
                  {
                    offset: 1,
                    color: 'rgba(109, 182, 197, 0)'
                  }
                ])
              },
              emphasis: {
                focus: 'series'
              },
              data: [320, 132, 201, 334, 190, 130, 220, 201, 334, 190, 130, 220]
            },
            {
              name: 'Line 4',
              type: 'line',
              smooth: true,
              showSymbol: false,
              lineStyle: {
                width: 2
              },
              areaStyle: {
                opacity: 0.8,
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                    offset: 0,
                    color: '#8AC5D0'
                  },
                  {
                    offset: 1,
                    color: 'rgba(138, 197, 208, 0)'
                  }
                ])
              },
              emphasis: {
                focus: 'series'
              },
              data: [220, 402, 231, 134, 190, 230, 120, 231, 134, 190, 230, 120]
            },
            {
              name: 'Line 5',
              type: 'line',
              smooth: true,
              showSymbol: false,
              lineStyle: {
                width: 2
              },
              label: {
                show: true,
                position: 'top'
              },
              areaStyle: {
                opacity: 0.8,
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                    offset: 0,
                    color: '#A7D4DC'
                  },
                  {
                    offset: 1,
                    color: 'rgba(167, 212, 220, 0)'
                  }
                ])
              },
              emphasis: {
                focus: 'series'
              },
              data: [220, 302, 181, 234, 210, 290, 150, 181, 234, 210, 290, 150]
            }
          ]
        },
        days: ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'],
        rawSeries: [{
            name: 'Line 1',
            color: '#4090A0',
            data: [85, 90, 78, 92, 88, 95, 80, 85, 90, 78, 92, 88, 95, 80, 88, 95, 80, 85, 90, 78, 85, 90, 78, 92, 88,
              95, 78, 92, 88, 95
            ]
          },
          {
            name: 'Line 2',
            color: '#50A8B9',
            data: [80, 85, 75, 90, 85, 93, 77, 80, 85, 75, 90, 85, 93, 77, 80, 85, 75, 90, 85, 93, 77, 80, 85, 75, 90,
              85, 93, 77, 80, 85, 75, 90, 85, 93, 77
            ]
          },
          {
            name: 'Line 3',
            color: '#6DB6C5',
            data: [82, 88, 76, 91, 86, 94, 79, 82, 88, 76, 91, 86, 94, 79, 82, 88, 76, 91, 86, 94, 79, 82, 88, 76, 91,
              86, 94, 79, 82, 88, 76, 91, 86, 94, 79
            ]
          },
          {
            name: 'Line 4',
            color: '#8AC5D0',
            data: [87, 92, 80, 93, 89, 96, 81, 87, 92, 80, 93, 89, 96, 81, 87, 92, 80, 93, 89, 96, 81, 87, 92, 80, 93,
              89, 96, 81, 87, 92, 80, 93, 89, 96, 81
            ]
          },
          {
            name: 'Line 5',
            color: '#A7D4DC',
            data: [84, 89, 77, 88, 87, 95, 78, 84, 89, 77, 88, 87, 95, 78, 84, 89, 77, 88, 87, 95, 78, 84, 89, 77, 88,
              87, 95, 78, 84, 89, 77, 88, 87, 95, 78
            ]
          },
          {
            name: 'Line 6',
            color: '#C5E2E8',
            data: [81, 86, 74, 89, 84, 92, 76, 81, 86, 74, 89, 84, 92, 76, 81, 86, 74, 89, 84, 92, 76, 81, 86, 74, 89,
              84, 92, 76, 81, 86, 74, 89, 84, 92, 76
            ]
          }
        ],
        showAlert : false
      }
    },
    components: {
      DailyReport,
      WeeklyReport,
      MonthlyReport
    },
    computed: {
      currentComponent() {
        const view = this.$route.query.view
        switch (view) {
          case 'daily':
            return DailyReport
          case 'weekly':
            return WeeklyReport
          case 'monthly':
            return MonthlyReport
          default:
            return null
        }
      },
      chartOptionsRetest() {
        const source: (string | number)[][] = [
          ['Day', ...this.rawSeries.map(s => s.name)]
        ];

        for (let i = 0; i < this.days.length; i++) {
          const row: (string | number)[] = [this.days[i]];
          for (const series of this.rawSeries) {
            row.push(series.data[i]);
          }
          source.push(row);
        }

        const series = this.rawSeries.map((s, index) => ({
          type: 'line',
          name: s.name,
          encode: {
            x: 0,
            y: index + 1
          },
          itemStyle: {
            color: s.color
          },
          endLabel: {
            show: true,
            formatter: (params: {
              value: any[]
            }) => `${s.name}: ${params.value[index + 1]}%`,
            color: '#FFFFFF',
            textShadowColor: 'rgba(255, 255, 255, 0.7)',
            textShadowBlur: 10,
          },
          labelLayout: {
            moveOverlap: 'shiftY'
          },
          emphasis: {
            focus: 'series'
          },
          showSymbol: false
        }))

        return {
          backgroundColor: 'rgba(0, 0, 0, 0)',
          tooltip: {
            trigger: 'axis'
          },
          legend: {
            textStyle: {
              color: '#fff'
            }
          },
          dataset: {
            source
          },
          xAxis: {
            type: 'category',
            name: 'Day',
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLine: {
              lineStyle: {
                color: '#454545'
              }
            },
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            }
          },
          yAxis: {
            name: 'Percentage (%)',
            nameTextStyle: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            max: 100,
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLine: {
              lineStyle: {
                color: '#454545'
              }
            },
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            }
          },
          grid: {
            left: '10%',
            right: '10%',
            bottom: '15%'
          },
          series
        }
      },
      chartOptionsRetestMonthly() {
        const days = Array.from({
          length: 30
        }, (_, i) => i + 1);

        const source: (string | number)[][] = [
          ['Day', ...this.rawSeries.map(s => s.name)]
        ];

        for (let i = 0; i < days.length; i++) {
          const row: (string | number)[] = [days[i]];
          for (const series of this.rawSeries) {
            row.push(series.data[i]);
          }
          source.push(row);
        }

        const series = this.rawSeries.map((s, index) => ({
          type: 'line',
          name: s.name,
          encode: {
            x: 0,
            y: index + 1
          },
          itemStyle: {
            color: s.color
          },
          endLabel: {
            show: true,
            formatter: (params: {
              value: any[]
            }) => `${s.name}: ${params.value[index + 1]}%`,
            color: '#FFFFFF',
            textShadowColor: 'rgba(255, 255, 255, 0.7)',
            textShadowBlur: 10,
          },
          labelLayout: {
            moveOverlap: 'shiftY'
          },
          emphasis: {
            focus: 'series'
          },
          showSymbol: false
        }));

        return {
          backgroundColor: 'rgba(0, 0, 0, 0)',
          tooltip: {
            trigger: 'axis'
          },
          legend: {
            textStyle: {
              color: '#fff'
            }
          },
          dataset: {
            source
          },
          xAxis: {
            type: 'category',
            name: 'Hari',
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLine: {
              lineStyle: {
                color: '#454545'
              }
            },
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            }
          },
          yAxis: {
            name: 'Percentage (%)',
            nameTextStyle: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            max: 100,
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLine: {
              lineStyle: {
                color: '#454545'
              }
            },
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            }
          },
          grid: {
            left: '10%',
            right: '10%',
            bottom: '15%'
          },
          series
        };
      },
      chartOptionsRetestYear() {
        const month = ['January', 'February', 'March', 'April', 'May', 'Juny', 'July', 'Agustus', 'September',
          'October', 'November', 'December'
        ]

        const source: (string | number)[][] = [
          ['Day', ...this.rawSeries.map(s => s.name)]
        ];

        for (let i = 0; i < month.length; i++) {
          const row: (string | number)[] = [month[i]];
          for (const series of this.rawSeries) {
            row.push(series.data[i]);
          }
          source.push(row);
        }

        const series = this.rawSeries.map((s, index) => ({
          type: 'line',
          name: s.name,
          encode: {
            x: 0,
            y: index + 1
          },
          itemStyle: {
            color: s.color
          },
          endLabel: {
            show: true,
            formatter: (params: {
              value: any[]
            }) => `${s.name}: ${params.value[index + 1]}%`,
            color: '#FFFFFF',
            textShadowColor: 'rgba(255, 255, 255, 0.7)',
            textShadowBlur: 10,
          },
          labelLayout: {
            moveOverlap: 'shiftY'
          },
          emphasis: {
            focus: 'series'
          },
          showSymbol: false
        }))

        return {
          backgroundColor: 'rgba(0, 0, 0, 0)',
          tooltip: {
            trigger: 'axis'
          },
          legend: {
            textStyle: {
              color: '#fff'
            }
          },
          dataset: {
            source
          },
          xAxis: {
            type: 'category',
            name: 'Hari',
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLine: {
              lineStyle: {
                color: '#454545'
              }
            },
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            }
          },
          yAxis: {
            name: 'Percentage (%)',
            nameTextStyle: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            max: 100,
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLine: {
              lineStyle: {
                color: '#454545'
              }
            },
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            }
          },
          grid: {
            left: '10%',
            right: '10%',
            bottom: '15%'
          },
          series
        }
      },
      currentTheme() {
        return document.documentElement.getAttribute('data-theme') || 'light'
      }
    },
    watch: {
      currentTheme(newTheme) {
        this.updateChartTheme(newTheme)
      }
    },
    mounted() {
      const savedTheme = localStorage.getItem('theme') || 'light';
      this.updateChartTheme(savedTheme);
      this.showAlert = true
      setInterval(() => {
        this.showAlert = false
      }, 10000)
    },
    methods: {
      setActive(index: number) {
        console.log('Clicked card index:', index)
        this.activeIndex = index
      },
      updateChartTheme(theme: string) {
        const isDark = theme === 'dark'

        // chartOptionsAchieveOutput
        this.chartOptionsAchieveOutput.legend.textStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsAchieveOutput.xAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsAchieveOutput.xAxis.axisLine.lineStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsAchieveOutput.xAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';
        this.chartOptionsAchieveOutput.xAxis.nameTextStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsAchieveOutput.yAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsAchieveOutput.yAxis.axisLine.lineStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsAchieveOutput.yAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';
        this.chartOptionsAchieveOutput.yAxis.nameTextStyle.color = isDark ? '#FFFFFF' : '#000000';

        const shadowColor = isDark ? 'rgba(255,255,255,0.7)' : 'transparent'
        const shadowBlur = isDark ? 10 : 0
        this.chartOptionsAchieveOutput.xAxis.axisLabel.textShadowColor = shadowColor;
        this.chartOptionsAchieveOutput.xAxis.axisLabel.textShadowBlur = shadowBlur;
        this.chartOptionsAchieveOutput.yAxis.axisLabel.textShadowColor = shadowColor;
        this.chartOptionsAchieveOutput.yAxis.axisLabel.textShadowBlur = shadowBlur;

        // chartOptionsYield
        this.chartOptionsYield.legend.textStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsYield.xAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsYield.yAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsYield.xAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';
        this.chartOptionsYield.yAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';

        // chartOptionsRetest
        this.chartOptionsRetest.legend.textStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsRetest.xAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsRetest.yAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsRetest.yAxis.nameTextStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsRetest.yAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsRetest.yAxis.axisLine.lineStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsRetest.yAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';
        this.chartOptionsRetest.yAxis.nameTextStyle.color = isDark ? '#FFFFFF' : '#000000';

        //chartOptionsAchieveOutputMonthly
        this.chartOptionsAchieveOutputMonthly.legend.textStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsAchieveOutputMonthly.xAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsAchieveOutputMonthly.xAxis.axisLine.lineStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsAchieveOutputMonthly.xAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';
        this.chartOptionsAchieveOutputMonthly.xAxis.nameTextStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsAchieveOutputMonthly.yAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsAchieveOutputMonthly.yAxis.axisLine.lineStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsAchieveOutputMonthly.yAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';
        this.chartOptionsAchieveOutputMonthly.yAxis.nameTextStyle.color = isDark ? '#FFFFFF' : '#000000';

        // chartOptionsYieldMonthly
        this.chartOptionsYieldMonthly.legend.textStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsYieldMonthly.xAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsYieldMonthly.yAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsYieldMonthly.xAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';
        this.chartOptionsYieldMonthly.yAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';
        this.chartOptionsYieldMonthly.yAxis.nameTextStyle.color = isDark ? '#FFFFFF' : '#000000';

        // chartOptionsRetestMonthly
        this.chartOptionsRetestMonthly.legend.textStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsRetestMonthly.xAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsRetestMonthly.xAxis.axisLine.lineStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsRetestMonthly.xAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';
        this.chartOptionsRetestMonthly.yAxis.nameTextStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsRetestMonthly.yAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsRetestMonthly.yAxis.axisLine.lineStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsRetestMonthly.yAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';
        this.chartOptionsRetestMonthly.yAxis.nameTextStyle.color = isDark ? '#FFFFFF' : '#000000';

        // chartOptionsAchieveOutputYear
        this.chartOptionsAchieveOutputYear.legend.textStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsAchieveOutputYear.xAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsAchieveOutputYear.xAxis.axisLine.lineStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsAchieveOutputYear.xAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';
        this.chartOptionsAchieveOutputYear.xAxis.nameTextStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsAchieveOutputYear.yAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsAchieveOutputYear.yAxis.axisLine.lineStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsAchieveOutputYear.yAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';
        this.chartOptionsAchieveOutputYear.yAxis.nameTextStyle.color = isDark ? '#FFFFFF' : '#000000';

        // chartOptionsYieldYear
        this.chartOptionsYieldYear.legend.textStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsYieldYear.xAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsYieldYear.yAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsYieldYear.xAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';
        this.chartOptionsYieldYear.yAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';

        // chartOptionsRetestYear
        this.chartOptionsRetestYear.legend.textStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsRetestYear.xAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsRetestYear.xAxis.axisLine.lineStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsRetestYear.xAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';
        this.chartOptionsRetestYear.yAxis.nameTextStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsRetestYear.yAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsRetestYear.yAxis.axisLine.lineStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptionsRetestYear.yAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';
        this.chartOptionsRetestYear.yAxis.nameTextStyle.color = isDark ? '#FFFFFF' : '#000000';

      }
    }
  }
</script>

<style scoped>
  .cards {
    display: flex;
    flex-direction: row;
    gap: 15px;
  }

  .cards .card {
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: column;
    text-align: left;
    height: 200px;
    width: 300px;
    border-radius: 10px;
    color: white;
    cursor: pointer;
    transition: 400ms;
  }

  .cards .card p.tip {
    font-size: 1em;
    font-weight: 700;
  }

  .cards .card p.second-text {
    font-size: .7em;
  }

  .cards .card:hover {
    transform: scale(1.2, 1.2);
  }

  .cards:hover>.card:not(:hover) {
    filter: blur(3px);
    transform: scale(0.9, 0.9);
  }

  .active {
    border: 3px solid #6EB6C5;
    /* border-bottom: 8px solid #ffffff; */
  }

  .fade-enter-active,
    .fade-leave-active {
        transition: opacity 0.6s ease-in-out;
    }

    .fade-enter-from,
    .fade-leave-to {
        opacity: 0;
    }
    
  html[data-theme='light'] .bg-base-200 {
    background-color: oklch(25.33% 0.016 252.42);
  }

  html[data-theme='light'] .bg-base-100 {
    background-color: #eeeeee !important;
  }

  html[data-theme='light'] .tab-content {
    border-color: #ddd;
  }
</style>