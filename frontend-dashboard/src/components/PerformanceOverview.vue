<template>
    <div class="performance-overview">
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
        <div class="place-self-center mb-4">
            <div class="join">
                <input class="join-item btn" type="radio" name="options" :aria-label="$t('button.Week')"
                    v-model="picked" value="week" />
                <input class="join-item btn" type="radio" name="options" :aria-label="$t('button.Month')"
                    v-model="picked" value="month" />
                <input class="join-item btn" type="radio" name="options" :aria-label="$t('button.Year')"
                    v-model="picked" value="year" />
            </div>
        </div>
        <div class="w-full bg-base-300 p-4 mb-4 rounded-lg shadow summary-card">
            <div class="flex flex-wrap items-center justify-between gap-4">
                <div class="flex gap-4">
                    <div class="flex flex-wrap items-center gap-2">
                        <div v-if="activeFilters.includes('floor')">
                            <select v-model="filters.floor" class="select select-bordered select-sm">
                                <option disabled value="">{{$t('Choose Floor')}}</option>
                                <option v-for="floor in floor" :key="floor">{{ floor }}</option>
                            </select>
                        </div>

                        <div v-if="activeFilters.includes('line')">
                            <select v-model="filters.line" class="select select-bordered select-sm">
                                <option disabled value="">{{$t('Choose Line')}}</option>
                                <option v-for="line in lines" :key="line">{{ line }}</option>   
                            </select>
                        </div>
                        <div v-if="activeFilters.includes('station')">
                            <select v-model="filters.station" class="select select-bordered select-sm">
                                <option disabled value="">{{$t('Choose Station')}}</option>
                                <option v-for="station in stations" :key="station">{{ station }}</option>
                            </select>
                        </div>
                        <div v-if="activeFilters.includes('date')" class="flex items-center gap-2">
                            <label class="input input-sm w-40">
                                <span class="floating-label">{{$t('Select Year')}}</span>
                                <select v-model="year" class="select select-sm select-ghost w-20">
                                    <option :value="2025">2025</option>
                                    <option :value="2024">2024</option>
                                    <option :value="2023">2023</option>
                                </select>
                            </label>
                            <label class="input input-sm w-48" v-if="picked  !== 'year'">
                                <span class="floating-label">{{$t('Select Month')}}</span>
                                <select v-model="month" class="select select-sm select-ghost w-40">
                                    <option v-for="m in 12" :key="m" :value="m">
                                        {{ year }}-{{ m.toString().padStart(2, '0') }}
                                    </option>
                                </select>
                            </label>
                            <label class="input input-sm w-60" v-if="picked  === 'week'">
                                <span class="floating-label">{{$t('Select Week')}}</span>
                                <select v-model="weekInMonth" class="select select-sm select-ghost w-40">
                                    <option v-for="w in 5" :key="w" :value="w">{{$t('No')}} {{ $t('Week') }} {{ w }}
                                        {{ $t('Week zh') }}</option>
                                </select>
                            </label>
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
        <div class="flex flex-col w-full gap-4">
            <div class="summary-card bg-base-300 p-4 rounded-lg">
                <v-chart v-if="picked  === 'week'" :option="chartOptionWeek"
                    class="xl:h-[300px] xl:w-[100%] lg:h-[300px] lg:w-[100%] md:h-[300px] md:w-[100%]"></v-chart>
                <v-chart v-if="picked  === 'month'" :option="chartOptionMonth"
                    class="xl:h-[300px] xl:w-[100%] lg:h-[300px] lg:w-[100%] md:h-[300px] md:w-[100%]"></v-chart>
                <v-chart v-if="picked  === 'year'" :option="chartOptionYear"
                    class="xl:h-[300px] xl:w-[100%] lg:h-[300px] lg:w-[100%] md:h-[300px] md:w-[100%]"></v-chart>
            </div>
            <div class="summary-card bg-base-300 p-4 rounded-lg">
                <div class="overflow-x-auto">
                    <table v-if="picked  === 'week'" class="table">
                        <thead>
                            <tr>
                                <th>2025-41</th>
                                <th>{{$t('Average Efficiency (%)')}}</th>
                                <th>{{$t('Total Runtime (hours)')}}</th>
                                <th>{{$t('Total Downtime (hours)')}}</th>
                                <th>{{$t('Total Error')}}</th>
                                <th>{{$t('Output (unit)')}}</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr class="hover:bg-base-100">
                                <td>10/01</td>
                                <td>92.5</td>
                                <td>720</td>
                                <td>58</td>
                                <td>12</td>
                                <td>14500</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>10/02</td>
                                <td>90.1</td>
                                <td>672</td>
                                <td>65</td>
                                <td>14</td>
                                <td>13900</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>10/03</td>
                                <td>94.3</td>
                                <td>744</td>
                                <td>42</td>
                                <td>9</td>
                                <td>15120</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>10/04</td>
                                <td>89.7</td>
                                <td>720</td>
                                <td>75</td>
                                <td>17</td>
                                <td>14050</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>10/05</td>
                                <td>91.8</td>
                                <td>744</td>
                                <td>61</td>
                                <td>10</td>
                                <td>14780</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>10/06</td>
                                <td>93.2</td>
                                <td>720</td>
                                <td>49</td>
                                <td>11</td>
                                <td>15030</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>10/07</td>
                                <td>90.5</td>
                                <td>744</td>
                                <td>70</td>
                                <td>13</td>
                                <td>14490</td>
                            </tr>
                        </tbody>
                    </table>
                    <table v-if="picked === 'month'" class="table">
                        <thead>
                            <tr>
                                <th>October</th>
                                <th>{{$t('Average Efficiency (%)')}}</th>
                                <th>{{$t('Total Runtime (hours)')}}</th>
                                <th>{{$t('Total Downtime (hours)')}}</th>
                                <th>{{$t('Total Error')}}</th>
                                <th>{{$t('Output (unit)')}}</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr class="hover:bg-base-100">
                                <td>2025-41</td>
                                <td>90.1</td>
                                <td>672</td>
                                <td>65</td>
                                <td>14</td>
                                <td>13900</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>2025-42</td>
                                <td>94.3</td>
                                <td>744</td>
                                <td>42</td>
                                <td>9</td>
                                <td>15120</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>2025-43</td>
                                <td>89.7</td>
                                <td>720</td>
                                <td>75</td>
                                <td>17</td>
                                <td>14050</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>2025-44</td>
                                <td>91.8</td>
                                <td>744</td>
                                <td>61</td>
                                <td>10</td>
                                <td>14780</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>2025-45</td>
                                <td>93.2</td>
                                <td>720</td>
                                <td>49</td>
                                <td>11</td>
                                <td>15030</td>
                            </tr>
                        </tbody>
                    </table>
                    <table v-if="picked  === 'year'" class="table">
                        <thead>
                            <tr>
                                <th>2025</th>
                                <th>{{$t('Average Efficiency (%)')}}</th>
                                <th>{{$t('Total Runtime (hours)')}}</th>
                                <th>{{$t('Total Downtime (hours)')}}</th>
                                <th>{{$t('Total Error')}}</th>
                                <th>{{$t('Output (unit)')}}</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr class="hover:bg-base-100">
                                <td>{{$t('month.January')}}</td>
                                <td>92.5</td>
                                <td>720</td>
                                <td>58</td>
                                <td>12</td>
                                <td>14500</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>{{$t('month.February')}}</td>
                                <td>90.1</td>
                                <td>672</td>
                                <td>65</td>
                                <td>14</td>
                                <td>13900</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>{{$t('month.March')}}</td>
                                <td>94.3</td>
                                <td>744</td>
                                <td>42</td>
                                <td>9</td>
                                <td>15120</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>{{$t('month.April')}}</td>
                                <td>89.7</td>
                                <td>720</td>
                                <td>75</td>
                                <td>17</td>
                                <td>14050</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>{{$t('month.May')}}</td>
                                <td>91.8</td>
                                <td>744</td>
                                <td>61</td>
                                <td>10</td>
                                <td>14780</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>{{$t('month.June')}}</td>
                                <td>93.2</td>
                                <td>720</td>
                                <td>49</td>
                                <td>11</td>
                                <td>15030</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>{{$t('month.July')}}</td>
                                <td>90.5</td>
                                <td>744</td>
                                <td>70</td>
                                <td>13</td>
                                <td>14490</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>{{$t('month.August')}}</td>
                                <td>88.9</td>
                                <td>744</td>
                                <td>82</td>
                                <td>18</td>
                                <td>13870</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>{{$t('month.September')}}</td>
                                <td>92.0</td>
                                <td>720</td>
                                <td>60</td>
                                <td>10</td>
                                <td>14600</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>{{$t('month.October')}}</td>
                                <td>93.6</td>
                                <td>744</td>
                                <td>48</td>
                                <td>8</td>
                                <td>15200</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>{{$t('month.November')}}</td>
                                <td>89.2</td>
                                <td>720</td>
                                <td>77</td>
                                <td>15</td>
                                <td>14110</td>
                            </tr>
                            <tr class="hover:bg-base-100">
                                <td>{{$t('month.December')}}</td>
                                <td>91.3</td>
                                <td>744</td>
                                <td>59</td>
                                <td>11</td>
                                <td>14830</td>
                            </tr>
                        </tbody>
                    </table>
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
        LineChart,
        BarChart
    } from 'echarts/charts'
    import {
        GridComponent,
        TooltipComponent,
        LegendComponent,
        GraphicComponent
    } from 'echarts/components'
    echart.use([
        CanvasRenderer,
        LineChart,
        BarChart,
        TooltipComponent,
        LegendComponent,
        GraphicComponent,
        GridComponent,
    ])
    import * as echart from 'echarts/core'
    import * as echarts from 'echarts';
    export default {
        data() {
            return {
                selected: 'Group A'  as string | null,
                selectedLine: '3F13',
                selectedstation: 'R1',
                showDropdown: false,
                selectedShift: '',
                filters: {
                    floor: '',
                    dateFrom: '',
                    dateUntil: '',
                    line: '3F13',
                    station: 'R1'
                },
                activeFilters: ['line', 'station', 'date', 'floor'],
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
                        label: 'Line',
                        value: 'line'
                    }
                ],
                floor: ['Floor 1', 'Floor 2', 'Floor 3'],
                lines: ['2F13', '2F16', '2F17', '2F19', '3F13', '3F14', '3F15', '3F17'],
                stations: ['R1', 'R2', 'R3', 'R4', 'R5', 'R6'],
                picked: 'year',
                chartOptionWeek: {
                    backgroundColor: 'transparent',
                    title: {
                        text: this.$t('menu.performance'),
                        textStyle: {
                            color: '#ffffff',
                            textShadowColor: 'rgba(255,255,255,0.7)',
                            textShadowBlur: 10
                        },
                        offsetCenter: ['0%', '25%'],
                    },
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'shadow'
                        }
                    },
                    grid: {
                        left: '5%',
                        right: '5%',
                        bottom: '10%',
                        top: '20%'
                    },
                    legend: {
                        data: ['Downtime(hours)', 'Runtime(hours)'],
                        textStyle: {
                            color: '#ccc'
                        }
                    },
                    xAxis: {
                        data: ['10/01', '10/02', '10/03', '10/04', '10/05', '10/06', '10/07'],
                        axisLine: {
                            lineStyle: {
                                color: '#ccc'
                            }
                        }
                    },
                    yAxis: {
                        splitLine: {
                            show: false
                        },
                        axisLine: {
                            lineStyle: {
                                color: '#ccc'
                            }
                        }
                    },
                    series: [{
                            name: 'Downtime (hours)',
                            type: 'line',
                            lineStyle: {
                                color: '#00d3bb',
                                width: 4
                            },
                            smooth: true,
                            showAllSymbol: true,
                            symbol: 'emptyCircle',
                            symbolSize: 25,
                            itemStyle: {
                                color: '#00d3bb'
                            },
                            data: Array.from({
                                length: 7
                            }, () => Math.floor(Math.random() * 200))
                        },
                        {
                            name: 'Runtime (hours)',
                            type: 'bar',
                            barWidth: 50,
                            itemStyle: {
                                borderRadius: 5,
                                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                        offset: 0,
                                        color: '#14c8d4'
                                    },
                                    {
                                        offset: 1,
                                        color: '#43eec6'
                                    }
                                ])
                            },
                            data: Array.from({
                                length: 7
                            }, () => Math.floor(Math.random() * 200))
                        },
                        {
                            name: 'Output (unit)',
                            type: 'pictorialBar',
                            symbol: 'rect',
                            itemStyle: {
                                color: '#00224D'
                            },
                            symbolRepeat: true,
                            symbolSize: [55, 6],
                            symbolMargin: 1,
                            z: -10,
                            data: Array.from({
                                length: 7
                            }, () => Math.floor(Math.random() * 200))
                        }
                    ]
                },
                chartOptionMonth: {
                    backgroundColor: 'transparent',
                    title: {
                        text: this.$t('menu.performance'),
                        textStyle: {
                            color: '#ffffff',
                            textShadowColor: 'rgba(255,255,255,0.7)',
                            textShadowBlur: 10
                        },
                        offsetCenter: ['0%', '25%'],
                    },
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'shadow'
                        }
                    },
                    grid: {
                        left: '5%',
                        right: '5%',
                        bottom: '10%',
                        top: '20%'
                    },
                    legend: {
                        data: ['Downtime(hours)', 'Runtime(hours)'],
                        textStyle: {
                            color: '#ccc'
                        }
                    },
                    xAxis: {
                        data: ['2025-41', '2025-42', '2025-43', '2025-44', '2025-45'],
                        axisLine: {
                            lineStyle: {
                                color: '#ccc'
                            }
                        }
                    },
                    yAxis: {
                        splitLine: {
                            show: false
                        },
                        axisLine: {
                            lineStyle: {
                                color: '#ccc'
                            }
                        }
                    },
                    series: [{
                            name: 'Downtime (hours)',
                            type: 'line',
                            lineStyle: {
                                color: '#00d3bb',
                                width: 4
                            },
                            smooth: true,
                            showAllSymbol: true,
                            symbol: 'emptyCircle',
                            symbolSize: 25,
                            itemStyle: {
                                color: '#00d3bb'
                            },
                            data: Array.from({
                                length: 5
                            }, () => Math.floor(Math.random() * 200))
                        },
                        {
                            name: 'Runtime (hours)',
                            type: 'bar',
                            barWidth: 60,
                            itemStyle: {
                                borderRadius: 5,
                                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                        offset: 0,
                                        color: '#14c8d4'
                                    },
                                    {
                                        offset: 1,
                                        color: '#43eec6'
                                    }
                                ])
                            },
                            data: Array.from({
                                length: 5
                            }, () => Math.floor(Math.random() * 200))
                        },
                        {
                            name: 'Output (unit)',
                            type: 'pictorialBar',
                            symbol: 'rect',
                            itemStyle: {
                                color: '#00224D'
                            },
                            symbolRepeat: true,
                            symbolSize: [65, 6],
                            symbolMargin: 1,
                            z: -10,
                            data: Array.from({
                                length: 5
                            }, () => Math.floor(Math.random() * 200))
                        }
                    ]
                },
                chartOptionYear: {
                    backgroundColor: 'transparent',
                    title: {
                        text: this.$t('menu.performance'),
                        textStyle: {
                            color: '#ffffff',
                            textShadowColor: 'rgba(255,255,255,0.7)',
                            textShadowBlur: 10
                        },
                        offsetCenter: ['0%', '25%'],
                    },
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'shadow'
                        }
                    },
                    grid: {
                        left: '5%',
                        right: '5%',
                        bottom: '10%',
                        top: '20%'
                    },
                    legend: {
                        data: ['Downtime(hours)', 'Runtime(hours)'],
                        textStyle: {
                            color: '#ccc'
                        }
                    },
                    xAxis: {
                        data: [this.$t('month.January'), this.$t('month.February'), this.$t('month.March'), this.$t(
                                'month.April'), this.$t('month.May'), this.$t('month.June'), this.$t('month.July'),
                            this.$t('month.August'), this.$t('month.September'), this.$t('month.October'), this.$t(
                                'month.November'), this.$t('month.December')
                        ],
                        axisLine: {
                            lineStyle: {
                                color: '#ccc'
                            }
                        }
                    },
                    yAxis: {
                        splitLine: {
                            show: false
                        },
                        axisLine: {
                            lineStyle: {
                                color: '#ccc'
                            }
                        }
                    },
                    series: [{
                            name: 'Downtime (hours)',
                            type: 'line',
                            lineStyle: {
                                color: '#00d3bb'
                            },
                            smooth: true,
                            showAllSymbol: true,
                            symbol: 'emptyCircle',
                            symbolSize: 12,
                            itemStyle: {
                                color: '#00d3bb'
                            },
                            data: Array.from({
                                length: 12
                            }, () => Math.floor(Math.random() * 200))
                        },
                        {
                            name: 'Runtime (hours)',
                            type: 'bar',
                            barWidth: 10,
                            itemStyle: {
                                borderRadius: 5,
                                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                        offset: 0,
                                        color: '#14c8d4'
                                    },
                                    {
                                        offset: 1,
                                        color: '#43eec6'
                                    }
                                ])
                            },
                            data: Array.from({
                                length: 12
                            }, () => Math.floor(Math.random() * 200))
                        },
                        {
                            name: 'Output (unit)',
                            type: 'pictorialBar',
                            symbol: 'rect',
                            itemStyle: {
                                color: '#00224D'
                            },
                            symbolRepeat: true,
                            symbolSize: [12, 4],
                            symbolMargin: 1,
                            z: -10,
                            data: Array.from({
                                length: 12
                            }, () => Math.floor(Math.random() * 200))
                        }
                    ]
                },
                year: 2025,
                month: 10,
                weekInMonth: 1,
                selectedDays: [1, 2, 3, 4, 5, 6],
                showAlert: false
            }
        },
        methods: {
            getDatesFromMonthWeek(year: number, month: number, weekNumber: number, days: number[] = [1, 2, 3, 4, 5,
                6
            ]): string[] {
                const firstDayOfMonth = new Date(year, month - 1, 1)

                const startOfWeek = new Date(firstDayOfMonth)
                startOfWeek.setDate(1 + (weekNumber - 1) * 7)

                const result = days.map(day => {
                    const date = new Date(startOfWeek)
                    const currentDay = date.getDay()
                    const offset = (day - currentDay + 7) % 7
                    date.setDate(date.getDate() + offset)
                    return date
                }).filter(date => date.getMonth() === month - 1)

                result.sort((a, b) => a.getTime() - b.getTime())
                return result.map(date =>
                    `${(date.getMonth() + 1).toString().padStart(2, '0')}/${date.getDate().toString().padStart(2, '0')}`
                )
            }
        },
        computed: {
            weeklyDates() {
                return this.getDatesFromMonthWeek(this.year, this.month, this.weekInMonth, this.selectedDays)
            },
            selectedMonthYear() {
                const date = new Date(this.year, this.month - 1)
                return date.toLocaleDateString('en-GB', {
                    month: 'short',
                    year: 'numeric'
                })
            }

        },
        mounted() {
            this.showAlert = true
            setInterval(()=> {
                this.showAlert = false
            },10000)
        }
    }
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

    .btn:where(input:checked:not(.filter .btn)) {
        --btn-color: var(--color-accent) !important;
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

    html[data-theme='light'] tr:hover {
        background-color: oklch(0.25 0.02 252.45);
    }

    html[data-theme='light'] tr {
        border-color: oklch(25.33% 0.016 252.42);
    }

    html[data-theme='light'] .select{
      background-color: #eee;
    }

    html[data-theme='light'] .input{
      background-color: #eee;
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