<template>
    <div class="shift-efficiency-comparison p-4">

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

            </div>
        </div>
        <div class="grid grid-flow-row-dense grid-cols-9 auto-rows-fr gap-4 mt-8 ">
            <div class="card bg-base-300 shadow-xl col-span-3 flex flex-col h-full ">

                <v-chart :option="chartOptionsDay" class="w-full h-80" />
                <div class="card-body items-center text-center">
                    <h2 class="card-title">{{$t('Stamping workshop Day Shift Movement Rate Summary')}}</h2>
                    <div class="card-actions">
                        <button class="button-warning btn btn-dash btn-warn"><span
                                class="text-white font-normal">{{$t('Detail')}}</span></button>
                    </div>
                </div>
            </div>
            <div class="card bg-base-300 shadow-xl col-span-3 flex flex-col h-full ">
                <v-chart :option="chartOptionsMiddle" class="w-full h-80" />
                <div class="card-body items-center text-center">
                    <h2 class="card-title">{{$t('Stamping workshop Middle Shift Movement Rate Summary')}}</h2>
                    <div class="card-actions">
                        <button class="button btn btn-dash btn-accent"><span
                                class="text-white font-normal">{{$t('Detail')}}</span></button>
                    </div>
                </div>
            </div>
            <div class="card bg-base-300 shadow-xl col-span-3 flex flex-col h-full ">
                <v-chart :option="chartOptionsNight" class="w-full h-80" />
                <div class="card-body items-center text-center">
                    <h2 class="card-title">{{$t('Stamping workshop Night Shift Movement Rate Summary')}} </h2>
                    <div class="card-actions">
                        <button class="button-info btn btn-dash btn-info"><span
                                class="text-white font-normal">{{$t('Detail')}}</span></button>
                    </div>
                </div>
            </div>
            <div class="card bg-base-300 shadow-xl col-span-3 flex flex-col h-full ">
                <v-chart :option="chartOptionsStamping" class="w-full h-full" />
                <div class="card-body items-center text-center mt-auto">
                    <h2 class="card-title">{{$t('Stamping Current Movement Rate')}}</h2>
                </div>
            </div>
            <div class="card bg-base-300 shadow-xl col-span-6 flex flex-col ">
                <v-chart :option="chartOptionsMobillityStamping" class="w-full h-96" />
                <div class="card-body items-center text-center mt-auto">
                    <h2 class="card-title">{{$t('Mobility Rate Trends')}}</h2>
                </div>
            </div>
            <div class="card bg-base-300 shadow-xl col-span-3 flex flex-col h-full ">
                <v-chart :option="chartOptionsBending" class="w-full h-full" />
                <div class="card-body items-center text-center mt-auto">
                    <h2 class="card-title">{{$t('Bending Forming Current Movement Rate')}}</h2>
                </div>
            </div>
            <div class="card bg-base-300 shadow-xl col-span-6 flex flex-col ">
                <v-chart :option="chartOptionsMobillityBending" class="w-full h-96" />
                <div class="card-body items-center text-center mt-auto">
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
    import * as echarts from 'echarts';
    echart.use([
        CanvasRenderer,
        GaugeChart,
        LineChart,
        TooltipComponent,
        LegendComponent,
        GraphicComponent,
        GridComponent,
    ])
    export default {
        data() {
            return {
                selected: 'Group A' as string | null,
                selectedArea: '',
                selectedProcess: '',
                showDropdown: false,
                selectedShift: '',
                filters: {
                    floor: '',
                    dateFrom: '',
                    dateUntil: '',
                    process: ''
                },
                activeFilters: ['process', 'date', 'floor'],
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
                ],
                floor: [this.$t('floor.Floor 1'), this.$t('floor.Floor 2'), this.$t('floor.Floor 3')],
                process: [this.$t('Stamping Process'), this.$t('Production Process'), this.$t('Machining Process')],
                chartOptionsDay: {
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
                            offsetCenter: [0, '5%']
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
                                color: '#FF9A00'
                            },
                            nameTextStyle: {
                                color: '#FFFFFF',
                                textShadowColor: 'rgba(255, 255, 255, 0.7)',
                                textShadowBlur: 10
                            },
                        }]
                    }]
                },
                chartOptionsMiddle: {
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
                            offsetCenter: [0, '5%']
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
                            value: 85,
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
                chartOptionsNight: {
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
                            offsetCenter: [0, '5%']
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
                            value: 90,
                            itemStyle: {
                                color: '#51EAEA'
                            },
                            nameTextStyle: {
                                color: '#FFFFFF',
                                textShadowColor: 'rgba(255, 255, 255, 0.7)',
                                textShadowBlur: 10
                            },
                        }]
                    }]
                },
                chartOptionsStamping: {
                    backgroundColor: 'transparent',
                    title: {
                        text: '',
                        textStyle: {
                            color: '#FFFFFF',
                            fontFamily: 'Orbitron, sans-serif'
                        }
                    },
                    grid: {
                        left: 80,
                        right: 30,
                        top: 20,
                        bottom: 20
                    },
                    xAxis: {
                        type: 'value',
                        min: 0,
                        max: 100,
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
                            color: '#AAAAAA',
                            formatter: '{value}%'
                        }
                    },
                    yAxis: {
                        type: 'category',
                        data: [this.$t('Day Shift'), this.$t('Middle Shift'), this.$t('Night Shift')],
                        axisLine: {
                            show: false
                        },
                        axisTick: {
                            show: false
                        },
                        axisLabel: {
                            color: '#FFFFFF',
                            formatter: (value: any) => `{a|${value}}\n{b|%}`,
                            rich: {
                                a: {
                                    fontSize: 10
                                },
                                b: {
                                    fontSize: 10,
                                    color: '#aaa'
                                }
                            }
                        }
                    },
                    series: [{
                            type: 'bar',
                            data: [100, 100, 100],
                            barGap: '-70%',
                            silent: true,
                            itemStyle: {
                                color: (params: {
                                    value: any
                                }) => {
                                    const val = params.value;
                                    if (val <= 50) return '#2A323C';
                                    if (val <= 75) return '#2A323C';
                                    return '#2A323C';
                                }
                            }
                        },
                        {
                            type: 'bar',
                            data: [80, 85, 90],
                            barWidth: 20,
                            itemStyle: {
                                color: '#00d3bb'
                            },
                            label: {
                                show: true,
                                position: 'right',
                                formatter: '{c}%',
                                color: '#fff'
                            }
                        }
                    ],
                    legend: {
                        show: false
                    }
                },
                chartOptionsMobillityStamping: {
                    backgroundColor: 'transparent',
                    title: {
                        text: '',
                        textStyle: {
                            color: '#fff'
                        }
                    },
                    subtitle: {
                        text: 'Drag area untuk zoom in',
                        textStyle: {
                            color: '#ccc'
                        }
                    },
                    tooltip: {
                        trigger: 'axis',
                        formatter: (params: any[]) => {
                            const point = params[0];
                            return `${point.axisValue}<br/><b>${point.data.toFixed(2)}%</b>`;
                        }
                    },
                    xAxis: {
                        type: 'category',
                        data: ['6/5 00:00', '6/6 12:00', '6/7 30:00', '6/8 50:00', '6/9 24:00', '6/10 00:00',
                            '6/11 00:00',
                            '6/12 00:00', '6/13 30:00', '6/14 58:00', '6/15 14:00', '6/16 22:00', '6/17 00:00',
                            '6/18 00:00'
                        ],
                        axisLabel: {
                            color: '#FFFFFF',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10,
                            fontSize: 10
                        },
                        nameTextStyle: {
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
                        max: 100,
                        axisLabel: {
                            formatter: '{value}%',
                            color: '#FFFFFF',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10,
                            fontSize: 10
                        },
                        name: 'Percentage (%)',
                        nameTextStyle: {
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

                    dataZoom: [{
                            type: 'inside',
                            xAxisIndex: 0,
                            start: 0,
                            end: 80,
                        },
                        {
                            type: 'slider',
                            show: true,
                            xAxisIndex: 0,
                            backgroundColor: 'transparent',
                            dataBackground: {
                                areaStyle: {
                                    color: 'rgba(0, 211, 187, 0)'
                                },
                                lineStyle: {
                                    color: 'rgba(0, 211, 187, 0)'
                                }
                            },
                            fillerColor: 'rgba(0, 211, 187, 0)',
                            borderColor: 'rgba(255,255,255,0.7)',
                            handleStyle: {
                                color: 'rgba(255,255,255,0.7)',
                                borderColor: '#fff',
                                shadowBlur: 10,
                                shadowColor: 'rgba(255,255,255,0.7)'
                            },
                            color: '#FFFFFF',
                            textShadowColor: 'rgba(0, 211, 187, 0)',
                            textShadowBlur: 10,
                            textStyle: {
                                color: '#ccc'
                            }
                        },
                    ],
                    series: [{
                        name: 'Dynamic Rate',
                        type: 'line',
                        areaStyle: {
                            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                    offset: 0,
                                    color: '#00d3bb'
                                },
                                {
                                    offset: 1,
                                    color: 'rgba(0, 211, 187, 0)'
                                }
                            ])
                        },
                        itemStyle: {
                            color: '#00d3bb'
                        },
                        lineStyle: {
                            width: 3,
                            color: '#00d3bb'
                        },

                        smooth: true,
                        showSymbol: true,
                        symbolSize: 8,
                        data: [89.7, 80.5, 85.3, 67.5, 58.3, 65.8, 55.6, 85.3, 50.9, 45.2, 52.9, 62.3]
                    }],
                    legend: {
                        show: false
                    }
                },
                chartOptionsBending: {
                    backgroundColor: 'transparent',
                    title: {
                        text: '',
                        textStyle: {
                            color: '#FFFFFF',
                            fontFamily: 'Orbitron, sans-serif'
                        }
                    },
                    grid: {
                        left: 80,
                        right: 30,
                        top: 20,
                        bottom: 20
                    },
                    xAxis: {
                        type: 'value',
                        min: 0,
                        max: 100,
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
                            color: '#AAAAAA',
                            formatter: '{value}%'
                        }
                    },
                    yAxis: {
                        type: 'category',
                        data: [this.$t('Day Shift'), this.$t('Middle Shift'), this.$t('Night Shift')],
                        axisLine: {
                            show: false
                        },
                        axisTick: {
                            show: false
                        },
                        axisLabel: {
                            color: '#FFFFFF',
                            formatter: (value: any) => `{a|${value}}\n{b|%}`,
                            rich: {
                                a: {
                                    fontSize: 10
                                },
                                b: {
                                    fontSize: 10,
                                    color: '#aaa'
                                }
                            }
                        }
                    },
                    series: [{
                            type: 'bar',
                            data: [100, 100, 100],
                            barGap: '-70%',
                            silent: true,
                            itemStyle: {
                                color: (params: {
                                    value: any
                                }) => {
                                    const val = params.value;
                                    if (val <= 50) return '#2A323C';
                                    if (val <= 75) return '#2A323C';
                                    return '#2A323C';
                                }
                            }
                        },
                        {
                            type: 'bar',
                            data: [70, 75, 80],
                            barWidth: 20,
                            itemStyle: {
                                color: '#51EAEA'
                            },
                            label: {
                                show: true,
                                position: 'right',
                                formatter: '{c}%',
                                color: '#fff'
                            }
                        }
                    ],
                    legend: {
                        show: false
                    }
                },
                chartOptionsMobillityBending: {
                    backgroundColor: 'transparent',
                    title: {
                        text: '',
                        textStyle: {
                            color: '#fff'
                        }
                    },
                    subtitle: {
                        text: 'Drag area untuk zoom in',
                        textStyle: {
                            color: '#ccc'
                        }
                    },
                    tooltip: {
                        trigger: 'axis',
                        formatter: (params: any[]) => {
                            const point = params[0];
                            return `${point.axisValue}<br/><b>${point.data.toFixed(2)}%</b>`;
                        }
                    },
                    xAxis: {
                        type: 'category',
                        data: ['6/5 00:00', '6/6 12:00', '6/7 30:00', '6/8 50:00', '6/9 24:00', '6/10 00:00',
                            '6/11 00:00',
                            '6/12 00:00', '6/13 30:00', '6/14 58:00', '6/15 14:00', '6/16 22:00', '6/17 00:00',
                            '6/18 00:00'
                        ],
                        axisLabel: {
                            color: '#FFFFFF',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10,
                            fontSize: 10
                        },
                        nameTextStyle: {
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
                        max: 100,
                        axisLabel: {
                            formatter: '{value}%',
                            color: '#FFFFFF',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10,
                            fontSize: 10
                        },
                        name: 'Percentage (%)',
                        nameTextStyle: {
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
                    dataZoom: [{
                            type: 'inside',
                            // xAxisIndex: 0,
                            start: 0,
                            end: 80,
                        },
                        {
                            type: 'slider',
                            show: true,
                            // xAxisIndex: 0,
                            backgroundColor: 'transparent',
                            dataBackground: {
                                areaStyle: {
                                    color: 'rgba(0, 211, 187, 0)'
                                },
                                lineStyle: {
                                    color: 'rgba(0, 211, 187, 0)'
                                }
                            },
                            fillerColor: 'rgba(0, 211, 187, 0)',
                            borderColor: 'rgba(255,255,255,0.7)',
                            handleStyle: {
                                color: 'rgba(255,255,255,0.7)',
                                borderColor: '#fff',
                                shadowBlur: 10,
                                shadowColor: 'rgba(255,255,255,0.7)'
                            },
                            color: '#FFFFFF',
                            textShadowColor: 'rgba(0, 211, 187, 0)',
                            textShadowBlur: 10,
                            textStyle: {
                                color: '#ccc'
                            }
                        },
                    ],
                    series: [{
                        name: 'Dynamic Rate',
                        type: 'line',
                        areaStyle: {
                            color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                    offset: 0,
                                    color: '#51EAEA'
                                },
                                {
                                    offset: 1,
                                    color: 'rgba(0, 190, 211, 0)'
                                }
                            ])
                        },
                        itemStyle: {
                            color: '#51EAEA'
                        },
                        lineStyle: {
                            width: 3,
                            color: '#51EAEA'
                        },
                        smooth: true,
                        showSymbol: true,
                        symbolSize: 8,
                        data: [79.7, 70.5, 75.3, 57.5, 87.3, 81.8, 81.8, 88.3, 80.9, 48.2, 82.9, 82.3]
                    }],
                    legend: {
                        show: false
                    }
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

    .button-warning {
        --orange: #FF9A00;
        font-size: 14px;
        padding: 0.5em 2.7em;
        letter-spacing: 0.06em;
        position: relative;
        font-family: inherit;
        border-radius: 0.6em;
        overflow: hidden;
        transition: all 0.3s;
        line-height: 1.4em;
        border: 2px solid var(--orange);
        background: linear-gradient(to right, rgba(253, 193, 27, 0.1) 1%, transparent 40%, transparent 60%, rgba(253, 193, 27, 0.1) 100%);
        color: var(--orange);
        box-shadow: inset 0 0 10px rgba(253, 219, 27, 0.4), 0 0 9px 3px rgba(253, 193, 27, 0.1);
        height: 42px;
    }

    .button-warning:hover {
        color: #FF9A00;
        box-shadow: inset 0 0 10px rgba(253, 212, 27, 0.6), 0 0 9px 3px rgba(253, 193, 27, 0.2);
    }

    .button-warning:before {
        content: "";
        position: absolute;
        left: -4em;
        width: 4em;
        height: 100%;
        top: 0;
        transition: transform .4s ease-in-out;
        background: linear-gradient(to right, transparent 1%, rgba(253, 200, 27, 0.1) 40%, rgba(253, 181, 27, 0.1) 60%, transparent 100%);
    }

    .button-warning:hover:before {
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