<template>
    <div class="work-order p-4">
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
                        <div v-if="activeFilters.includes('line')">
                            <select v-model="filters.line" class="select select-bordered select-sm">
                                <option disabled value="">{{$t('Choose Line')}}</option>
                                <option v-for="line in lines" :key="line">{{ line }}</option>
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
        <!-- Status Cards -->
        <div class="grid grid-flow-row-dense grid-cols-4 auto-rows-fr gap-4 mt-8 ">
            <!-- Station 1 -->
            <div class="card bg-base-300 shadow-xl flex flex-col h-full ">
                <v-chart :option="chartOptionsStation1" class="w-full h-80" />
                <div class="card-body items-center text-center">
                    <h2 class="card-title">R1 {{$t('Movement Rate')}}</h2>
                    <div class="card-actions">
                        <button class="button btn btn-dash btn-accent"><span
                                class="text-white font-normal">{{$t('Detail')}}</span></button>
                    </div>
                </div>
            </div>
            <!-- Mobility Rate Trends -->
            <div class="card bg-base-300 shadow-xl col-span-3 flex flex-col   ">
                <figure class="px-10 pt-10 items-center">
                    <v-chart :option="chartOptionsTime1" class=" w-full h-64" />
                </figure>

                <div class="card-body items-center text-center">
                    <h2 class="card-title">{{$t('Mobility Rate Trends')}}</h2>
                </div>
            </div>
            <!-- Station 2 -->
            <div class="card bg-base-300 shadow-xl flex flex-col h-full ">
                <v-chart :option="chartOptionsStation2" class="w-full h-full" />
                <div class="card-body items-center text-center">
                    <h2 class="card-title">R2 {{$t('Movement Rate')}}</h2>
                    <button class="button btn btn-dash btn-accent"><span
                            class="text-white font-normal">{{$t('Detail')}}</span></button>
                </div>
            </div>
            <!-- Mobility Rate Trends -->
            <div class="card bg-base-300 shadow-xl col-span-3 flex flex-col ">
                <figure class="px-10 pt-10 items-center">
                    <v-chart :option="chartOptionsTime1" class="w-full h-64" />
                </figure>

                <div class="card-body items-center text-center">
                    <h2 class="card-title">{{$t('Mobility Rate Trends')}}</h2>
                </div>
            </div>
            <!-- Station 3 -->
            <div class="card bg-base-300 shadow-xl flex flex-col h-full ">
                <v-chart :option="chartOptionsStation3" class="w-full h-full" />
                <div class="card-body items-center text-center">
                    <h2 class="card-title">R3 {{$t('Movement Rate')}}</h2>
                    <button class="button btn btn-dash btn-accent"><span
                            class="text-white font-normal">{{$t('Detail')}}</span></button>
                </div>
            </div>
            <!-- Mobility Rate Trends -->
            <div class="card bg-base-300 shadow-xl col-span-3 flex flex-col ">
                <figure class="px-10 pt-10 items-center">
                    <v-chart :option="chartOptionsTime1" class="w-full h-64" />
                </figure>

                <div class="card-body items-center text-center">
                    <h2 class="card-title">{{$t('Mobility Rate Trends')}}</h2>
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
    //   import * as echarts from 'echarts';
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
                    process: '',
                    line: ''
                },
                activeFilters: ['area', 'process', 'date', 'floor', 'line'],
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
                lines: ['2F13', '2F16', '2F17', '2F19', '3F13', '3F14', '3F15', '3F17'],
                chartOptionsStation1: {
                    series: [{
                        type: 'gauge',
                        min: 0,
                        max: 100,
                        splitNumber: 5,
                        progress: {
                            show: true,
                            width: 18
                        },
                        axisLine: {
                            lineStyle: {
                                width: 18
                            }
                        },
                        axisTick: {
                            show: false
                        },
                        splitLine: {
                            length: 10,
                            lineStyle: {
                                width: 2,
                                color: '#999'
                            }
                        },
                        axisLabel: {
                            distance: 25,
                            color: '#999',
                            fontSize: 10,
                            textStyle: {
                                color: '#FFFFFF',
                                textShadowColor: 'rgba(255, 255, 255, 0.7)',
                                textShadowBlur: 10
                            }
                        },
                        pointer: {
                            icon: 'path://M2090.36389,615.30999 L2090.36389,615.30999 C2091.48372,615.30999 2092.40383,616.194028 2092.44859,617.312956 L2096.90698,728.755929 C2097.05155,732.369577 2094.2393,735.416212 2090.62566,735.56078 C2090.53845,735.564269 2090.45117,735.566014 2090.36389,735.566014 L2090.36389,735.566014 C2086.74736,735.566014 2083.81557,732.63423 2083.81557,729.017692 C2083.81557,728.930412 2083.81732,728.84314 2083.82081,728.755929 L2088.2792,617.312956 C2088.32396,616.194028 2089.24407,615.30999 2090.36389,615.30999 Z',
                            length: '45%',
                            width: 10,
                            offsetCenter: [0, '5%'],
                        },
                        title: {
                            show: false
                        },
                        detail: {
                            valueAnimation: true,
                            fontSize: 30,
                            color: '#FFFFFF',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10,
                            offsetCenter: [0, '70%']
                        },
                        data: [{
                            value: 65,
                            itemStyle: {
                                color: '#00d3bb'
                            },
                            nameTextStyle: {
                                color: '#FFFFFF',
                                textShadowColor: 'rgba(255, 255, 255, 0.7)',
                                textShadowBlur: 10
                            },
                        }]
                    }]
                },
                chartOptionsTime1: {
                    backgroundColor: 'transparent',
                    title: {
                        text: null
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
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'shadow'
                        },
                        formatter: function (params: any[]) {
                            let tooltip = `${params[0].name}<br/>`;
                            params.forEach(item => {
                                tooltip += `${item.marker} ${item.seriesName}: ${item.value} hour<br/>`;
                            });
                            return tooltip;
                        }
                    },
                    xAxis: {
                        type: 'value',
                        min: 0,
                        max: 24,
                        name: 'Hours',
                        axisLabel: {
                            color: '#FFFFFF',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10,
                            formatter: function (value: number) {
                                const hour = Math.floor(value);
                                const minute = (value % 1) * 60;
                                return `${hour.toString().padStart(2, '0')}:${minute === 0 ? '00' : '30'}`;
                            }
                        },
                        splitLine: {
                            lineStyle: {
                                color: '#454545'
                            }
                        },
                        splitNumber: 24
                    },
                    yAxis: {
                        type: 'category',
                        data: ['IM001'],
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
                    },
                    series: [{
                            name: this.$t('Running'),
                            type: 'bar',
                            stack: 'total',
                            label: {
                                show: true
                            },
                            emphasis: {
                                focus: 'series'
                            },
                            itemStyle: {
                                color: '#00DFA2'
                            },
                            data: [8]
                        },
                        {
                            name: this.$t('Error'),
                            type: 'bar',
                            stack: 'total',
                            label: {
                                show: true
                            },
                            emphasis: {
                                focus: 'series'
                            },
                            itemStyle: {
                                color: '#FF1700'
                            },
                            data: [1]
                        },
                        {
                            name: this.$t('Wait'),
                            type: 'bar',
                            stack: 'total',
                            label: {
                                show: true
                            },
                            emphasis: {
                                focus: 'series'
                            },
                            itemStyle: {
                                color: '#FF9A00'
                            },
                            data: [1.5]
                        },
                        {
                            name: this.$t('Stop'),
                            type: 'bar',
                            stack: 'total',
                            label: {
                                show: true
                            },
                            emphasis: {
                                focus: 'series'
                            },
                            itemStyle: {
                                color: '#AAAAAA'
                            },
                            data: [2]
                        },
                        {
                            name: this.$t('Schedule Break'),
                            type: 'bar',
                            stack: 'total',
                            label: {
                                show: true
                            },
                            emphasis: {
                                focus: 'series'
                            },
                            itemStyle: {
                                color: '#51EAEA'
                            },
                            data: [3]
                        }
                    ]
                },
                chartOptionsStation2: {
                    series: [{
                        type: 'gauge',
                        min: 0,
                        max: 100,
                        splitNumber: 5,
                        progress: {
                            show: true,
                            width: 18
                        },
                        axisLine: {
                            lineStyle: {
                                width: 18
                            }
                        },
                        axisTick: {
                            show: false
                        },
                        splitLine: {
                            length: 10,
                            lineStyle: {
                                width: 2,
                                color: '#999'
                            }
                        },
                        axisLabel: {
                            distance: 25,
                            color: '#999',
                            fontSize: 10,
                            textStyle: {
                                color: '#FFFFFF',
                                textShadowColor: 'rgba(255, 255, 255, 0.7)',
                                textShadowBlur: 10
                            }
                        },
                        pointer: {
                            icon: 'path://M2090.36389,615.30999 L2090.36389,615.30999 C2091.48372,615.30999 2092.40383,616.194028 2092.44859,617.312956 L2096.90698,728.755929 C2097.05155,732.369577 2094.2393,735.416212 2090.62566,735.56078 C2090.53845,735.564269 2090.45117,735.566014 2090.36389,735.566014 L2090.36389,735.566014 C2086.74736,735.566014 2083.81557,732.63423 2083.81557,729.017692 C2083.81557,728.930412 2083.81732,728.84314 2083.82081,728.755929 L2088.2792,617.312956 C2088.32396,616.194028 2089.24407,615.30999 2090.36389,615.30999 Z',
                            length: '45%',
                            width: 10,
                            offsetCenter: [0, '5%'],
                        },
                        title: {
                            show: false
                        },
                        detail: {
                            valueAnimation: true,
                            fontSize: 30,
                            color: '#FFFFFF',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10,
                            offsetCenter: [0, '70%']
                        },
                        data: [{
                            value: 75,
                            itemStyle: {
                                color: '#00d3bb'
                            },
                            nameTextStyle: {
                                color: '#FFFFFF',
                                textShadowColor: 'rgba(255, 255, 255, 0.7)',
                                textShadowBlur: 10
                            },
                        }]
                    }]
                },
                chartOptionsStation3: {
                    series: [{
                        type: 'gauge',
                        min: 0,
                        max: 100,
                        splitNumber: 5,
                        progress: {
                            show: true,
                            width: 18
                        },
                        axisLine: {
                            lineStyle: {
                                width: 18
                            }
                        },
                        axisTick: {
                            show: false
                        },
                        splitLine: {
                            length: 10,
                            lineStyle: {
                                width: 2,
                                color: '#999'
                            }
                        },
                        axisLabel: {
                            distance: 25,
                            color: '#999',
                            fontSize: 10,
                            textStyle: {
                                color: '#FFFFFF',
                                textShadowColor: 'rgba(255, 255, 255, 0.7)',
                                textShadowBlur: 10
                            }
                        },
                        pointer: {
                            icon: 'path://M2090.36389,615.30999 L2090.36389,615.30999 C2091.48372,615.30999 2092.40383,616.194028 2092.44859,617.312956 L2096.90698,728.755929 C2097.05155,732.369577 2094.2393,735.416212 2090.62566,735.56078 C2090.53845,735.564269 2090.45117,735.566014 2090.36389,735.566014 L2090.36389,735.566014 C2086.74736,735.566014 2083.81557,732.63423 2083.81557,729.017692 C2083.81557,728.930412 2083.81732,728.84314 2083.82081,728.755929 L2088.2792,617.312956 C2088.32396,616.194028 2089.24407,615.30999 2090.36389,615.30999 Z',
                            length: '45%',
                            width: 10,
                            offsetCenter: [0, '5%'],
                        },
                        title: {
                            show: false
                        },
                        detail: {
                            valueAnimation: true,
                            fontSize: 30,
                            color: '#FFFFFF',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10,
                            offsetCenter: [0, '70%']
                        },
                        data: [{
                            value: 80,
                            itemStyle: {
                                color: '#00d3bb'
                            },
                            nameTextStyle: {
                                color: '#FFFFFF',
                                textShadowColor: 'rgba(255, 255, 255, 0.7)',
                                textShadowBlur: 10
                            },
                        }]
                    }]
                }

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