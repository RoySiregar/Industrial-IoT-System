<template>
    <div class="robot-detail font-inter">
        <div class="flex flex-col space-y-4">
            
            <div class="w-full bg-base-300 p-4 rounded-lg shadow mb-4 summary-card">
                <div class="flex flex-wrap items-center justify-between gap-4">
                    <div class="flex gap-4">
                        <span class="font-semibold">{{$t('Filters')}}:</span>
                        <div class="flex flex-wrap items-center gap-2">
                            <div v-if="activeFilters.includes('date')" class="flex items-center gap-2">
                                <input type="datetime-local" v-model="filters.dateFrom"
                                    class="input input-bordered input-sm" />
                                <span class="strip">-</span>
                                <input type="datetime-local" v-model="filters.dateUntil"
                                    class="input input-bordered input-sm" />
                                <button @click="onApply" class="btn btn-sm btn-accent">{{$t('Apply')}}</button>
                            </div>
                        </div>
                    </div>
                    <div @change="handleShiftChange" class="radio-buttons-container flex items-center gap-4 mt-4">
                        <div class="radio-button">
                            <input name="radio-group" id="radio0" class="radio-button__input" type="radio" value=""
                                v-model="selectedShift">
                        </div>
                        <div class="radio-button">
                            <input name="radio-group" id="radio1" class="radio-button__input" type="radio" value="day"
                                v-model="selectedShift">
                            <label for="radio1" class="radio-button__label">
                                <span class="radio-button__custom"></span> {{$t('Day Shift')}}
                            </label>
                        </div>
                        <div class="radio-button">
                            <input name="radio-group" id="radio2" class="radio-button__input" type="radio"
                                value="middle" v-model="selectedShift">
                            <label for="radio2" class="radio-button__label">
                                <span class="radio-button__custom"></span> {{$t('Middle Shift')}}
                            </label>
                        </div>
                        <div class="radio-button">
                            <input name="radio-group" id="radio3" class="radio-button__input" type="radio" value="night"
                                v-model="selectedShift">
                            <label for="radio3" class="radio-button__label">
                                <span class="radio-button__custom"></span> {{$t('Night Shift')}}
                            </label>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card bg-base-300 h-full p-4 summary-card">
                <div class="overflow-x-auto flex">
                    <table class="table">
                        <thead>
                            <tr>
                                <th>{{$t('Floor')}}</th>
                                <th>{{$t('Line')}}</th>
                                <th>{{$t('Robot')}}</th>
                                <th>PASS</th>
                                <th>FAIL</th>
                                <th>{{$t('Total')}}</th>
                                <th>Yield Rate (%)</th>
                            </tr>
                        </thead>
                        <tbody>
                            <template v-for="(lineItem, lIdx) in columns" :key="lIdx">
                                <tr v-for="(robot, rIdx) in lineItem.robots" :key="rIdx">
                                    <th>{{ lineItem.floor }}</th>
                                    <td>{{ lineItem.name }}</td>
                                    <td>{{ robot.name }}</td>
                                    <td>{{ robot.pass }}</td>
                                    <td>
                                        <button 
                                            @click="fetchFailDetails(lineItem, robot)"
                                            class="font-bold hover:scale-110 transition-transform duration-200"
                                            :class="robot.fail > 0 ? 'text-error underline cursor-pointer' : 'text-gray-500 cursor-default'"
                                            :disabled="robot.fail === 0">
                                            {{ robot.fail }}
                                        </button>
                                    </td>
                                    <td>{{ robot.pass + robot.fail }}</td>
                                    <td>
                                        <span class="badge" :class="robot.yieldRate >= 80 ? 'badge-success' 
                                                  : robot.yieldRate >= 50 ? 'badge-warning' 
                                                  : 'badge-error'">
                                            {{ robot.yieldRate }}%
                                        </span>
                                    </td>
                                </tr>
                            </template>
                            <tr v-if="columns.length === 0">
                                <td colspan="7" class="text-center text-gray-400">No data is available</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>

            <div class="grid grid-cols-1 md:grid-cols-2 gap-4 mt-4 w-full">
                
                <div v-if="showDetailCard" class="fade-enter-active">
                    <div class="bg-base-300 p-4 rounded-lg shadow relative w-full summary-card h-full">
                        <button class="absolute top-2 right-2 btn btn-xs btn-circle btn-error"
                            @click="closeDetailCard">×</button>

                        <h4 class="text-md font-semibold mb-4">
                            <span class="text-accent">{{ selectedDetailHeader.line }}</span> - 
                            {{ selectedDetailHeader.robot }} (Fail Details)
                        </h4>

                        <div class="overflow-x-visible mt-4 flex flex-col w-full">
                            <div v-if="isLoadingDetail" class="w-full text-center py-4">
                                <span class="loading loading-spinner text-accent"></span> Loading...
                            </div>

                            <table v-else-if="failDetails.length > 0" class="table w-full text-sm">
                                <thead>
                                    <tr>
                                        <th>Station</th>
                                        <th>Equipment</th>
                                        <th>Fail Count</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(detail, idx) in failDetails" :key="idx">
                                        <td>{{ detail.stationName }}</td>
                                        <td>{{ detail.equipmentName }}</td>
                                        <td>
                                            <button @click="fetchFailChart(detail)" 
                                                class="text-error hover:underline font-bold transition-all">
                                                {{ detail.failCount }}
                                            </button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div v-else class="text-center text-gray-400 py-2">No detail data found.</div>
                        </div>
                    </div>
                </div>

                <div v-if="showChartCard" class="fade-enter-active">
                    <div class="bg-base-300 p-4 rounded-lg shadow relative w-full summary-card h-full">
                        <button class="absolute top-2 right-2 btn btn-xs btn-circle btn-error"
                            @click="closeChartCard">×</button>

                        <h4 class="text-md font-semibold mb-4">
                            <span class="text-accent">{{ selectedChartHeader.equipment }}</span> 
                            ({{$t('Error Message')}})
                        </h4>

                        <div class="w-full h-[400px]">
                            <v-chart class="chart" :option="chartOptions" autoresize />
                        </div>
                    </div>
                </div>

            </div>

        </div>
    </div>
</template>

<script lang="ts">
import axios from 'axios'
import VChart from 'vue-echarts'
import { use } from 'echarts/core'
import { CanvasRenderer } from 'echarts/renderers'
import { BarChart } from 'echarts/charts'
import { GridComponent, TooltipComponent, TitleComponent } from 'echarts/components'

// Registrasi komponen ECharts
use([CanvasRenderer, BarChart, GridComponent, TooltipComponent, TitleComponent])

// --- DEFINISI TIPE DATA ---
type RobotItem = {
    name: string
    pass: number
    fail: number
    yieldRate: number
    status: string
    lastUpdateTime: string
    shift: string
}

type LineItem = {
    name: string
    floor: string
    robots: RobotItem[]
}

type FailDetailItem = {
    line: string
    stationName: string
    equipmentName: string
    failCount: number
}

type FailChartItem = {
    line: string
    stationName: string
    equipmentName: string
    errorMessage: string
    failCount: number
}

export default {
    components: { VChart },
    data() {
        return {
            filters: { floor: '', dateFrom: '', dateUntil: '', line: 'L2' },
            selectedShift: '',
            activeFilters: ['date', 'floor'],
            floors: ['1F', '2F', '3F'],
            columns: [] as LineItem[],
            rawApiData: [] as any[],

            // --- STATE LEVEL 2 (DETAIL LIST) ---
            showDetailCard: false,
            isLoadingDetail: false,
            failDetails: [] as FailDetailItem[],
            selectedDetailHeader: { line: '', robot: '' },

            // --- STATE LEVEL 3 (CHART) ---
            showChartCard: false,
            selectedChartHeader: { equipment: '' },
            
            // ECharts Options (Dengan Tipe Data Eksplisit untuk TypeScript)
            chartOptions: {
                backgroundColor: 'transparent',
                tooltip: {
                    trigger: 'axis',
                    axisPointer: { type: 'shadow' }
                },
                grid: {
                    left: '3%', right: '4%', bottom: '3%', containLabel: true
                },
                xAxis: {
                    type: 'category',
                    data: [] as string[], // Explicit Type
                    axisLabel: { color: '#FFFFFF', rotate: 20, fontSize: 10 },
                    axisLine: { lineStyle: { color: '#FFFFFF' } }
                },
                yAxis: {
                    type: 'value',
                    axisLabel: { color: '#FFFFFF' },
                    splitLine: { lineStyle: { color: '#454545' } }
                },
                series: [{
                    data: [] as number[], // Explicit Type
                    type: 'bar',
                    itemStyle: { color: '#00d3bb' },
                    label: { show: true, position: 'top', color: '#FFFFFF', fontWeight: 'bold' }
                }]
            }
        }
    },
    methods: {
        // --- API 1: LIST ROBOT (LEVEL 1) ---
        async fetchMachineList() {
            try {
                const res = await axios.post(
                    'http://10.175.80.201: /api/machine/list',
                    { factory: '', floor: this.filters.floor || '', line: '' },
                    { headers: { 'Content-Type': 'application/json' } }
                )
                this.rawApiData = Array.isArray(res.data?.data) ? res.data.data : []
                this.filterRobotData()
            } catch (err) {
                console.error('Error fetch list', err)
            }
        },

        // --- API 2: DETAIL FAIL (LEVEL 2) ---
        async fetchFailDetails(lineItem: LineItem, robot: RobotItem) {
            this.showDetailCard = true;
            this.showChartCard = false; // Reset chart saat buka detail baru
            this.isLoadingDetail = true;
            this.selectedDetailHeader = { line: lineItem.name, robot: robot.name };

            try {
                const res = await axios.post('http://10.175.80.201:5001/api/machine/details', {
                    floor: lineItem.floor, 
                    line: lineItem.name
                });
                this.failDetails = res.data || [];
            } catch (error) {
                console.error("Error details:", error);
            } finally {
                this.isLoadingDetail = false;
            }
        },

        // --- API 3: CHART FAIL TYPE (LEVEL 3) ---
        async fetchFailChart(detailItem: FailDetailItem) {
            this.showChartCard = true;
            this.selectedChartHeader = { equipment: detailItem.equipmentName };
            
            // Reset Chart Data sebelum fetch
            this.chartOptions.xAxis.data = [];
            this.chartOptions.series[0].data = [];

            try {
                // Call API Level 3
                const res = await axios.post('http://10.175.80.201:5001/api/machine/fail-type-chart', {
                    floor: this.filters.floor, 
                    line: detailItem.line,
                    stationName: detailItem.stationName,
                    equipmentName: detailItem.equipmentName
                });

                const chartData: FailChartItem[] = res.data || [];

                // Mapping ke Format ECharts
                if (chartData.length > 0) {
                    this.chartOptions = {
                        ...this.chartOptions,
                        xAxis: {
                            ...this.chartOptions.xAxis,
                            data: chartData.map(item => item.errorMessage) // Sumbu X: Error Message
                        },
                        series: [{
                            ...this.chartOptions.series[0],
                            data: chartData.map(item => item.failCount)    // Sumbu Y: Jumlah Fail
                        }]
                    };
                }

            } catch (error) {
                console.error("Error chart:", error);
            }
        },

        closeDetailCard() {
            this.showDetailCard = false;
            this.showChartCard = false; 
        },
        closeChartCard() {
            this.showChartCard = false;
        },

        // --- UTILS ---
        mapStatus(status: string) {
             switch (status) {
                case 'EXECUTING': return 'running'
                case 'ERROR': return 'NG'
                case 'UNKNOW': return 'offline'
                default: return 'pause'
            }
        },
        filterRobotData() {
             const filtered = this.rawApiData.filter(item => {
                const itemTime = new Date(item.lastUpdateTime).getTime()
                const from = this.filters.dateFrom ? new Date(this.filters.dateFrom).getTime() : null
                const until = this.filters.dateUntil ? new Date(this.filters.dateUntil).getTime() : null
                if (from && itemTime < from) return false
                if (until && itemTime > until) return false
                if (this.selectedShift && item.shift !== this.selectedShift) return false
                return true
            })
            this.columns = filtered.map((item: any) => ({
                name: item.line, floor: item.floor,
                robots: [{
                    name: item.code, pass: item.cntPass, fail: item.cntFail,
                    yieldRate: parseFloat(item.yield), status: this.mapStatus(item.status),
                    lastUpdateTime: item.lastUpdateTime, shift: item.shift
                }]
            }))
        },
        handleShiftChange() {
             if (!this.selectedShift) { this.filterRobotData(); return; }
            const baseDateStr = this.filters.dateFrom ? this.filters.dateFrom.split('T')[0] : new Date().toISOString().split('T')[0];
            let startDate = '', endDate = '';
            switch (this.selectedShift) {
                case 'day': startDate = `${baseDateStr}T06:50`; endDate = `${baseDateStr}T14:50`; break;
                case 'middle': startDate = `${baseDateStr}T14:50`; endDate = `${baseDateStr}T22:50`; break;
                case 'night': 
                    const nextDate = new Date(baseDateStr); nextDate.setDate(nextDate.getDate() + 1);
                    startDate = `${baseDateStr}T22:50`; endDate = `${nextDate.toISOString().split('T')[0]}T06:50`; break;
            }
            this.filters.dateFrom = startDate; this.filters.dateUntil = endDate;
            this.filterRobotData();
        },
        async onApply() { this.filterRobotData() }
    },
    computed: {
        lineName(): string { return (this.$route.query.line as string) || 'L2' },
        robotName(): string { return (this.$route.query.robot as string) || 'E2' },
        selectedLine() { return this.columns.find(line => line.name === this.lineName) || null },
        selectedRobot() {
            if (!this.selectedLine) return null
            const robot = this.selectedLine.robots.find(r => r.name === this.robotName)
            if (!robot) return null
            return { ...robot, line: this.selectedLine.name, floor: this.selectedLine.floor }
        },
        pass() { return this.selectedRobot?.pass ?? 0 },
        fail() { return this.selectedRobot?.fail ?? 0 },
        total() { return this.pass + this.fail },
        yieldRate() { return this.selectedRobot?.yieldRate ?? 0 }
    },
    mounted() {
        const today = new Date().toISOString().split('T')[0]
        this.filters.dateFrom = `${today}T00:00`
        this.filters.dateUntil = `${today}T23:59`
        this.fetchMachineList()
    }
}
</script>

<style scoped>
.summary-card { transition: transform 0.2s ease; }
.summary-card:hover { transform: translateY(-4px); }
.radio-buttons-container { display: flex; align-items: center; gap: 30px; }
.radio-button { display: inline-block; position: relative; cursor: pointer; }
.radio-button__input { position: absolute; opacity: 0; width: 0; height: 0; }
.radio-button__label { display: inline-block; padding-left: 30px; margin-bottom: 10px; position: relative; font-size: 12px; color: #fff; cursor: pointer; transition: all 0.3s cubic-bezier(0.23, 1, 0.320, 1); }
.radio-button__custom { position: absolute; top: 50%; left: 0; transform: translateY(-50%); width: 20px; height: 20px; border-radius: 50%; border: 2px solid #555; transition: all 0.3s cubic-bezier(0.23, 1, 0.320, 1); }
.radio-button__input:checked+.radio-button__label .radio-button__custom { transform: translateY(-50%) scale(0.9); border: 5px solid #00d3bb; color: #00d3bb; }
.radio-button__input:checked+.radio-button__label { color: #00d3bb; }
.radio-button__label:hover .radio-button__custom { transform: translateY(-50%) scale(1.2); border-color: #00d3bb; box-shadow: 0 0 10px #00d3bb80; }
.fade-enter-active, .fade-leave-active { transition: opacity 0.6s ease-in-out; }
.fade-enter-from, .fade-leave-to { opacity: 0; }

/* THEME OVERRIDES */
html[data-theme='light'] .font-semibold, html[data-theme='light'] .strip, html[data-theme='light'] .table th, html[data-theme='light'] .font-bold { color: #eee; }
html[data-theme='light'] .table { color: white; }
html[data-theme='light'] tr { border-color: oklch(25.33% 0.016 252.42); }
html[data-theme='light'] .select, html[data-theme='light'] .input { background-color: #eee; color: #15191e; }
html[data-theme='light'] .bg-base-300, html[data-theme='light'] .summary-card { background-color: #15191e; color: white; }
</style>