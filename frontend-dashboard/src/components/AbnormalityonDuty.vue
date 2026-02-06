<template>
    <div class="abnormality-on-duty p-4">
        <!-- Filters -->
        <div class="w-full bg-base-300 p-4 rounded-lg shadow mb-4 summary-card">
            <div class="flex flex-wrap items-center justify-between gap-4">
                <div class="flex gap-4">
                    <div class="flex flex-wrap items-center gap-2">
                        <div v-if="activeFilters.includes('floor')">
                            <select v-model="filters.floor" class="select select-bordered select-sm">
                                <option disabled value="">{{$t('Choose Floor')}}</option>
                                <option v-for="floor in floors" :key="floor">{{ floor }}</option>
                            </select>
                        </div>
                        <div v-if="activeFilters.includes('area')">
                            <select v-model="filters.area" class="select select-bordered select-sm">
                                <option disabled value="">{{$t('Choose Area')}}</option>
                                <option v-for="area in areas" :key="area">{{ area }}</option>
                            </select>
                        </div>
                        <div v-if="activeFilters.includes('process')">
                            <select v-model="filters.process" class="select select-bordered select-sm">
                                <option disabled value="">{{$t('Choose Process')}}</option>
                                <option v-for="process in processes" :key="process">{{ process }}</option>
                            </select>
                        </div>
                        <div v-if="activeFilters.includes('equipment')">
                            <select v-model="filters.equipment" class="select select-bordered select-sm">
                                <option disabled value="">{{$t('Choose Equipment')}}</option>
                                <option v-for="equipment in equipments" :key="equipment">{{ equipment }}</option>
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
        <div class="grid grid-flow-row-dense grid-cols-4 gap-4 mt-8 h-fit">
            <div class="card bg-base-300 shadow-xl flex flex-col h-full ">
                <v-chart :option="chartOptionsPie" class="h-96 w-full" />
                <div class="card-body items-center text-center mt-auto">
                    <h2 class="card-title">{{$t('The Proportion of Time by Abnormal Equipment and Tools (%)')}}</h2>
                    <div class="card-actions">
                        <button class="button btn btn-dash btn-accent"><span
                                class="text-white font-normal">{{$t('Detail')}}</span></button>
                    </div>
                </div>
            </div>
            <div class="card bg-base-300 shadow-xl flex flex-col h-full">
                <div class="overflow-x-auto px-10 pt-10 h-96">
                    <div class="grid grid-cols-2 gap-6 p-8 rounded-lg  text-primary-content">
                        <div class="stat bg-gray-800 rounded-lg p-4">
                            <div class="stat-title">{{$t('Total Damaged')}}</div>
                            <div class="stat-value text-yellow-400">124</div>
                            <div class="stat-desc">Last 24h</div>
                        </div>
                        <div class="stat bg-gray-800 rounded-lg p-4">
                            <div class="stat-title">{{$t('Total Bent')}}</div>
                            <div class="stat-value text-red-500">87</div>
                            <div class="stat-desc">↗︎ 12 (15%)</div>
                        </div>
                        <div class="stat bg-gray-800 rounded-lg p-4">
                            <div class="stat-title">{{$t('Mold Wear')}}</div>
                            <div class="stat-value text-cyan-400">56</div>
                            <div class="stat-desc">Stable</div>
                        </div>
                        <div class="stat bg-gray-800 rounded-lg p-4">
                            <div class="stat-title">{{$t('Mold Bending')}}</div>
                            <div class="stat-value text-accent">43</div>
                            <div class="stat-desc">↘︎ 8 (10%)</div>
                        </div>
                    </div>
                </div>
                <div class="card-body items-center text-center mt-auto ">
                    <h2 class="card-title">{{$t('Statistics of abnormal duration of equipment and tools (Min)')}}</h2>
                    <div class="card-actions">
                        <button class="button btn btn-dash btn-accent"><span
                                class="text-white font-normal">{{$t('Detail')}}</span></button>
                    </div>
                </div>
            </div>
            <div class="card bg-base-300 shadow-xl col-span-2 flex flex-col ">
                <v-chart :option="chartOptionsStatus" class="w-full h-full" />
                <div class="card-body items-center text-center mt-auto">
                    <h2 class="card-title">{{$t('Number of Abnormalities of Equipment 1 Tools During Shift')}}</h2>
                    <div class="card-actions">
                        <button class="button btn btn-dash btn-accent"><span
                                class="text-white font-normal">{{$t('Detail')}}</span></button>
                    </div>
                </div>
            </div>
            <div class="card bg-base-300 shadow-xl col-span-4 flex flex-col ">
                <div class="overflow-x-auto px-10 pt-10">
                    <table class="table">
                        <!-- head -->
                        <thead>
                            <tr>
                                <th></th>
                                <th>{{$t('Equipment ID')}}</th>
                                <th>{{$t('Tooling Name')}}</th>
                                <th>{{$t('Exception Type')}}</th>
                                <th>{{$t('Start Time')}}</th>
                                <th>{{$t('End Time')}}</th>
                                <th>{{$t('Duration (HH:MM)')}}</th>
                                <th>{{$t('Reason/Root Cause')}}</th>
                                <th>{{$t('Action Taken')}}</th>
                                <th>{{$t('Responsible Dept')}}</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <th>1</th>
                                <td>EQ-101</td>
                                <td>Drill Head #3</td>
                                <td>Tool Jam</td>
                                <td>2025-06-15 08:10</td>
                                <td>2025-06-15 11:30</td>
                                <td>03:20</td>
                                <td>Debris in drill bit</td>
                                <td>Cleaned and recalibrated</td>
                                <td>Maintenance</td>
                            </tr>
                            <tr>
                                <th>2</th>
                                <td>EQ-205</td>
                                <td>Welding Arm A</td>
                                <td>Calibration Error</td>
                                <td>2025-06-14 14:00</td>
                                <td>2025-06-14 16:15</td>
                                <td>02:15</td>
                                <td>Sensor misalignment</td>
                                <td>Sensor replaced</td>
                                <td>Engineering</td>
                            </tr>
                            <tr>
                                <th>3</th>
                                <td>EQ-009</td>
                                <td>CNC Lathe #2</td>
                                <td>Spindle Overheat</td>
                                <td>2025-06-12 13:45</td>
                                <td>2025-06-12 15:50</td>
                                <td>02:05</td>
                                <td>Coolant supply failure</td>
                                <td>Refilled coolant tank</td>
                                <td>Maintenance</td>
                            </tr>
                            <tr>
                                <th>4</th>
                                <td>EQ-077</td>
                                <td>Milling Tool X</td>
                                <td>Tool Breakage</td>
                                <td>2025-06-13 09:30</td>
                                <td>2025-06-13 11:20</td>
                                <td>01:50</td>
                                <td>Excessive feed rate</td>
                                <td>Tool replaced & adjusted</td>
                                <td>Production</td>
                            </tr>
                            <tr>
                                <th>5</th>
                                <td>EQ-198</td>
                                <td>Press Machine Y</td>
                                <td>Hydraulic Failure</td>
                                <td>2025-06-11 10:00</td>
                                <td>2025-06-11 11:40</td>
                                <td>01:40</td>
                                <td>Valve leakage</td>
                                <td>Repaired valve</td>
                                <td>Maintenance</td>
                            </tr>
                            <tr>
                                <th>6</th>
                                <td>EQ-034</td>
                                <td>Laser Cutter Z</td>
                                <td>Software Crash</td>
                                <td>2025-06-16 17:20</td>
                                <td>2025-06-16 18:50</td>
                                <td>01:30</td>
                                <td>Firmware bug</td>
                                <td>Updated software</td>
                                <td>IT</td>
                            </tr>
                            <tr>
                                <th>7</th>
                                <td>EQ-112</td>
                                <td>Robot Arm B</td>
                                <td>Motion Interruption</td>
                                <td>2025-06-10 12:00</td>
                                <td>2025-06-10 13:20</td>
                                <td>01:20</td>
                                <td>Loose connector</td>
                                <td>Reconnected and tested</td>
                                <td>Engineering</td>
                            </tr>
                            <tr>
                                <th>8</th>
                                <td>EQ-056</td>
                                <td>Grinder Tool D</td>
                                <td>Bearing Noise</td>
                                <td>2025-06-13 15:40</td>
                                <td>2025-06-13 16:55</td>
                                <td>01:15</td>
                                <td>Worn-out bearing</td>
                                <td>Bearing replaced</td>
                                <td>Maintenance</td>
                            </tr>
                            <tr>
                                <th>9</th>
                                <td>EQ-140</td>
                                <td>Assembly Tool F</td>
                                <td>Air Pressure Drop</td>
                                <td>2025-06-14 09:10</td>
                                <td>2025-06-14 10:15</td>
                                <td>01:05</td>
                                <td>Compressor failure</td>
                                <td>Compressor restarted</td>
                                <td>Utility</td>
                            </tr>
                            <tr>
                                <th>10</th>
                                <td>EQ-301</td>
                                <td>Paint Sprayer #1</td>
                                <td>Nozzle Clog</td>
                                <td>2025-06-15 07:00</td>
                                <td>2025-06-15 08:00</td>
                                <td>01:00</td>
                                <td>Improper cleaning routine</td>
                                <td>Cleaned and trained staff</td>
                                <td>Production</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="card-body items-center text-center mt-auto">
                    <h2 class="card-title">{{$t('Equipment and tooling exception details (Top 10 Processing Time)')}}
                    </h2>
                </div>
            </div>
        </div>
    </div>
</template>
<script lang="ts">
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
                    process: '',
                    equipment: ''

                },
                activeFilters: ['area', 'process', 'date', 'floor', 'equipment'],
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
                floors: [this.$t('floor.Floor 1'), this.$t('floor.Floor 2'), this.$t('floor.Floor 3')],
                areas: [this.$t('Stamping Workshop'), this.$t('Warehouse'), this.$t('Workshop')],
                processes: [this.$t('Stamping Process'), this.$t('Production Process'), this.$t('Machining Process')],
                equipments: [this.$t('ID.ID 1'), this.$t('ID.ID 2'), this.$t('ID.ID 3')],
                currentStatusIndex: 0,
                chartOptionsPie: {
                    legend: {
                        top: 'bottom'
                    },
                    tooltip: {
                        trigger: 'item'
                    },
                    series: [{
                        name: 'Abnormal Proportion',
                        type: 'pie',
                        radius: [60, 100],
                        center: ['50%', '50%'],
                        roseType: 'area',
                        itemStyle: {
                            borderRadius: 8
                        },
                        data: [{
                                value: 25,
                                name: this.$t('Total Damaged'),
                                itemStyle: {
                                    color: '#FFFF00'
                                }
                            },
                            {
                                value: 20,
                                name: this.$t('Total Bent'),
                                itemStyle: {
                                    color: '#FF1700'
                                }

                            },
                            {
                                value: 30,
                                name: this.$t('Mold Wear'),
                                itemStyle: {
                                    color: '#51EAEA'
                                }
                            },
                            {
                                value: 25,
                                name: this.$t('Mold Bending'),
                                itemStyle: {
                                    color: '#00d3bb'
                                }
                            },
                        ]
                    }]
                },
                chartOptionsStatus: {
                    backgroundColor: 'transparent',
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'shadow'
                        },
                        formatter: '{b}: {c}'
                    },
                    xAxis: {
                        type: 'category',
                        data: [this.$t('Total Damaged'), this.$t('Total Bent'), this.$t('Mold Wear'), this.$t(
                            'Mold Bending')],
                        axisLine: {
                            lineStyle: {
                                color: '#ccc'
                            }
                        },
                        axisLabel: {
                            color: '#FFFFFF',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10
                        },
                        nameTextStyle: {
                            color: '#ccc',
                            padding: [0, 0, 10, 0]
                        }
                    },
                    yAxis: {
                        type: 'value',
                        name: 'Frequency',
                        nameTextStyle: {
                            color: '#FFFFFF',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10
                        },
                        axisLine: {
                            lineStyle: {
                                color: '#ccc'
                            }
                        },
                        splitLine: {
                            lineStyle: {
                                color: '#444'
                            }
                        },
                        axisLabel: {
                            color: '#FFFFFF',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10
                        }
                    },
                    series: [{
                        type: 'bar',
                        name: 'Frequency',
                        data: [{
                                value: 15,
                                barWidth: '20%',
                                itemStyle: {
                                    borderRadius: [10, 10, 10, 10],
                                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                            offset: 0,
                                            color: '#FFFF00'
                                        },
                                        {
                                            offset: 1,
                                            color: '#FFFF00'
                                        }
                                    ])
                                },

                            },
                            {
                                value: 8,
                                barWidth: '20%',
                                itemStyle: {
                                    borderRadius: [10, 10, 10, 10],
                                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                            offset: 0,
                                            color: '#FF1700'
                                        },
                                        {
                                            offset: 1,
                                            color: '#FF1700 '
                                        }
                                    ])
                                },
                            },
                            {
                                value: 12,
                                barWidth: '20%',
                                itemStyle: {
                                    borderRadius: [10, 10, 10, 10],
                                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                            offset: 0,
                                            color: '#51EAEA'
                                        },
                                        {
                                            offset: 1,
                                            color: '#51EAEA'
                                        }
                                    ])
                                },
                            },
                            {
                                value: 5,
                                barWidth: '20%',
                                itemStyle: {
                                    borderRadius: [10, 10, 10, 10],
                                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                                            offset: 0,
                                            color: '#00d3bb'
                                        },
                                        {
                                            offset: 1,
                                            color: '#00d3bb'
                                        }
                                    ])
                                },
                            }
                        ],
                        barWidth: '40%',
                        label: {
                            show: true,
                            position: 'top',
                            color: '#fff'
                        }
                    }],
                    legend: {
                        show: false
                    },
                    grid: {
                        left: '5%',
                        right: '5%',
                        bottom: '10%',
                        containLabel: true
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

    html[data-theme='light'] .stat-title{
      color: #eee;
    }

    html[data-theme='light'] .stat-desc{
        color: #eee;
    }
</style>