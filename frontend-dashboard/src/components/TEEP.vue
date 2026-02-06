<template>
    <div class="teep">
        <div class="w-full bg-base-300 p-4 rounded-lg shadow mb-4 summary-card">
            <div class="flex flex-wrap items-center justify-between gap-4">
                <div class="flex gap-4">
                    <div class="flex flex-wrap items-center gap-2">
                        <div v-if="activeFilters.includes('line')">
                            <select v-model="filters.line" class="select select-bordered select-sm">
                                <option disabled value="">{{$t('Choose Line')}}</option>
                                <option v-for="line in lines" :key="line">{{ line }}</option>
                            </select>
                        </div>

                        <div v-if="activeFilters.includes('date')" class="flex items-center gap-2">
                            <input type="date" v-model="filters.dateFrom" class="input input-bordered input-sm" />
                            <span>-</span>
                            <input type="date" v-model="filters.dateUntil" class="input input-bordered input-sm" />
                            <button @click="fetchData" class="btn btn-sm btn-accent">{{$t('Apply')}}</button>
                        </div>
                    </div>
                </div>
                <div class="radio-buttons-container flex items-center gap-4 mt-4">
                    <div class="radio-button">
                        <input name="radio-group" id="radio0" class="radio-button__input" type="radio" value=""
                            v-model="selectedShift">
                         <label for="radio0" class="radio-button__label">
                            <span class="radio-button__custom"></span>
                            All Shift
                        </label>
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

        <div class="grid grid-cols-3 gap-4 overflow-hidden w-full justify-between">
            <div class="col-span-1">
                <div class="bg-base-300 summary-card p-6 rounded-lg w-full h-[40rem]">
                    <v-chart :option="chartOption"
                        class="xl:h-[500px] xl:w-[100%] lg:h-[400px] lg:w-[100%] md:h-[300px] md:w-[100%]"></v-chart>
                    <div class="overflow-x-auto">
                        <table class="table">
                            <tbody>
                                <tr>
                                    <th>OEE (Overall Equipment Effectiveness)</th>
                                    <th>:</th>
                                    <td>{{ (summary.oee * 100).toFixed(1) }}%</td>
                                </tr>
                                <tr>
                                    <th>TEEP (Total Effective Equipment Performance)</th>
                                    <th>:</th>
                                    <td>{{ (summary.teep * 100).toFixed(1) }}%</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

            <div class="col-span-2 flex flex-col gap-2">
                <div class="bg-base-300 summary-card w-full rounded-lg justify-between h-full">
                    <v-chart :option="chartTrend" class="py-2" style="height: 300px; width: 100%"></v-chart>
                </div>
                <div class="bg-base-300 summary-card w-full rounded-lg justify-between h-full ">
                    <div class="overflow-x-auto p-4" style="height: 300px;">
                        <table class="table table-pin-rows">
                            <thead>
                                <tr>
                                    <th>{{$t('Date')}}</th>
                                    <th>{{$t('Shift')}}</th>
                                    <th>Availability</th>
                                    <th>Performance</th>
                                    <th>Quality</th>
                                    <th>OEE</th>
                                    <th>TEEP</th>
                                    <th>{{$t('Downtime (hrs)')}}</th>
                                    <th>{{$t('Reject (unit)')}}</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-if="filteredData.length === 0">
                                    <td colspan="9" class="text-center">No data available for selected filters.</td>
                                </tr>
                                <tr v-for="item in filteredData" :key="item.workDate + item.shiftName" class="hover:bg-base-100">
                                    <th>{{ new Date(item.workDate).toLocaleDateString() }}</th>
                                    <td>{{ item.shiftName }}</td>
                                    <td>{{ (item.availability * 100).toFixed(1) }}%</td>
                                    <td>{{ (item.performance * 100).toFixed(1) }}%</td>
                                    <td>{{ (item.quality * 100).toFixed(1) }}%</td>
                                    <td>{{ (item.oee * 100).toFixed(1) }}%</td>
                                    <td>{{ (item.teep * 100).toFixed(1) }}%</td>
                                    <td>{{ item.downtime_hours }}</td>
                                    <td>{{ item.reject_units }}</td>
                                </tr>
                            </tbody>
                        </table>
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
    LineChart,
    GaugeChart,
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
    GaugeChart,
    BarChart,
    TooltipComponent,
    LegendComponent,
    GraphicComponent,
    GridComponent,
])

// Definisikan tipe data untuk hasil API
interface ApiData {
    workDate: string;
    shiftName: string;
    line: string;
    totalQty: number;
    goodQty: number;
    quality: number;
    availability: number;
    performance: number;
    oee: number;
    teep: number;
    utilization: number;
    downtime_hours: number;
    reject_units: number;
}

export default {
    data(): Record < string, any > {
        return {
            selected: 'Group A' as string | null,
            selectedLine: '3F13',

            showDropdown: false,
            selectedShift: '', // '' (All), 'day', 'middle', 'night'
            filters: {
                floor: '',
                dateFrom: '2025-10-24', // Nilai default
                dateUntil: '2025-10-30', // Nilai default
                line: '2F13', // Nilai default

            },
            activeFilters: ['line', 'date', 'floor'],
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
        
            apiData: [] as ApiData[],
            filteredData: [] as ApiData[],
            summary: {
                oee: 0,
                teep: 0,
                availability: 0,
                performance: 0,
                quality: 0,
                utilization: 0
            },
        }
    },

    mounted() {
        this.fetchData();
    },

    watch: {
        selectedShift() {
            this.processData();
        }
    },

    computed: {
        chartOption() {
            return {
                title: {
                    text: this.$t('TEEP Components Overview'),
                    left: 'center',
                    textStyle: {
                        color: '#FFFFFF',
                        textShadowColor: 'rgba(255,255,255,0.7)',
                        textShadowBlur: 10
                    }
                },
                series: [{
                    type: 'gauge',
                    startAngle: 90,
                    endAngle: -270,
                    pointer: {
                        show: false
                    },
                    progress: {
                        show: true,
                        overlap: false,
                        roundCap: true,
                        clip: false,
                        itemStyle: {
                            borderWidth: 1,
                            borderColor: 'rgba(255, 255, 255, 0.7)'
                        }
                    },
                    axisLine: {
                        lineStyle: {
                            width: 40
                        }
                    },
                    splitLine: {
                        show: false,
                        distance: 0,
                        length: 10
                    },
                    axisTick: {
                        show: false
                    },
                    axisLabel: {
                        show: false,
                        distance: 50
                    },
                    data: [{
                        value: (this.summary.quality * 100).toFixed(1),
                        name: 'Quality',
                        title: {
                            offsetCenter: ['0%', '15%'],
                            fontSize: 10
                        },
                        detail: {
                            valueAnimation: true,
                            offsetCenter: ['0%', '25%']
                        },
                        itemStyle: {
                            color: '#00DFA2',
                            borderColor: '#2c3e50',
                            borderWidth: 2
                        }
                    },
                    {
                        value: (this.summary.availability * 100).toFixed(1),
                        name: 'Availability',
                        title: {
                            offsetCenter: ['0%', '-45%'],
                            fontSize: 10
                        },
                        detail: {
                            valueAnimation: true,
                            offsetCenter: ['0%', '-35%']
                        },
                        itemStyle: {
                            color: '#FF1700',
                            borderColor: '#2c3e50',
                            borderWidth: 2
                        }
                    },
                    {
                        value: (this.summary.performance * 100).toFixed(1),
                        name: 'Performance',
                        title: {
                            offsetCenter: ['0%', '-15%'],
                            fontSize: 10
                        },
                        detail: {
                            valueAnimation: true,
                            offsetCenter: ['0%', '-5%']
                        },
                        itemStyle: {
                            color: '#FF9A00',
                            borderColor: '#2c3e50',
                            borderWidth: 2
                        }
                    },
                    {
                        value: (this.summary.utilization).toFixed(1),
                        name: 'Loading',
                        title: {
                            offsetCenter: ['0%', '45%'],
                            fontSize: 10
                        },
                        detail: {
                            valueAnimation: true,
                            offsetCenter: ['0%', '55%']
                        },
                        itemStyle: {
                            color: '#51EAEA',
                            borderColor: '#2c3e50',
                            borderWidth: 2
                        }
                    }
                    ],
                    title: {
                        fontSize: 16
                    },
                    detail: {
                        width: 50,
                        height: 14,
                        fontSize: 14,
                        color: 'inherit',
                        borderColor: 'inherit',
                        borderRadius: 20,
                        borderWidth: 0,
                        formatter: '{value}%'
                    }
                }]
            }
        },

        chartTrend() {
            const labels = this.filteredData.map((d: ApiData) => `${new Date(d.workDate).toLocaleDateString()} (${d.shiftName})`);
            const oeeData = this.filteredData.map((d: ApiData) => (d.oee * 100).toFixed(1));
            const teepData = this.filteredData.map((d: ApiData) => (d.teep * 100).toFixed(1));

            return {
                backgroundColor: 'transparent',
                title: {
                    text: `OEE & TEEP Trend for Line: ${this.filters.line}`,
                    left: 'center',
                    textStyle: {
                        color: '#FFFFFF',
                        textShadowColor: 'rgba(255,255,255,0.7)',
                        textShadowBlur: 10
                    }
                },
                tooltip: {
                    trigger: 'axis',
                    axisPointer: {
                        type: 'shadow'
                    }
                },
                legend: {
                    data: ['OEE', 'TEEP'],
                    top: 'bottom',
                    textStyle: {
                        color: '#FFFFFF'
                    }
                },
                grid: {
                    left: '3%',
                    right: '4%',
                    bottom: '10%',
                    containLabel: true
                },
                xAxis: {
                    type: 'category',
                    data: labels,
                    axisLabel: {
                        color: '#FFFFFF',
                        rotate: 30
                    },
                    axisLine: {
                        lineStyle: {
                            color: '#FFFFFF'
                        }
                    },
                    splitLine: {
                        show: false
                    },
                },
                yAxis: {
                    type: 'value',
                    min: 0,
                    max: 100,
                    axisLabel: {
                        color: '#FFFFFF',
                        formatter: '{value} %'
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
                    name: 'Value (%)',
                    nameTextStyle: {
                        color: '#FFFFFF',
                    }
                },
                series: [{
                    name: 'OEE',
                    data: oeeData,
                    type: 'bar',
                    itemStyle: {
                        color: '#00d3bb'
                    },
                },
                {
                    name: 'TEEP',
                    data: teepData,
                    type: 'bar',
                    itemStyle: {
                        color: '#FF9A00'
                    },
                }]
            }
        }
    },

    methods: {
        async fetchData() {
            if (!this.filters.dateFrom || !this.filters.dateUntil || !this.filters.line) {
                console.warn("Harap isi filter tanggal dan line.");
                return;
            }
            
            const params = new URLSearchParams({
                startDate: this.filters.dateFrom,
                endDate: this.filters.dateUntil,
                line: this.filters.line,
                idealCycleTimeSeconds: '16',
                plannedSecondsPerShift: '25200',
                calendarSecondsPerDay: '86400'
            });

            const url = `http://10.176.33.76:6002/api/OEEandTEEP/calculate?${params.toString()}`;

            try {
                const response = await fetch(url);
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                this.apiData = await response.json();
                this.processData();

            } catch (error) {
                console.error("Gagal mengambil data API:", error);
                this.apiData = [];
                this.processData();
            }
        },

        processData() {
            const shiftMap: { [key: string]: string } = {
                'day': 'Morning Shift',
                'middle': 'Middle Shift',
                'night': 'Night Shift'
            };
            const targetShift = shiftMap[this.selectedShift];

            if (targetShift) {
                this.filteredData = this.apiData.filter((item: ApiData) => item.shiftName === targetShift);
            } else {
                this.filteredData = this.apiData;
            }

            if (this.filteredData.length === 0) {
                this.summary = { oee: 0, teep: 0, availability: 0, performance: 0, quality: 0, utilization: 0 };
                return;
            }

            const avg = (arr: number[]) => arr.reduce((a, b) => a + b, 0) / arr.length;

            this.summary.availability = avg(this.filteredData.map((d: ApiData) => d.availability));
            this.summary.performance = avg(this.filteredData.map((d: ApiData) => d.performance));
            this.summary.quality = avg(this.filteredData.map((d: ApiData) => d.quality));
            this.summary.oee = avg(this.filteredData.map((d: ApiData) => d.oee));
            this.summary.teep = avg(this.filteredData.map((d: ApiData) => d.teep));
            this.summary.utilization = avg(this.filteredData.map((d: ApiData) => d.utilization));
        }
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
</style>