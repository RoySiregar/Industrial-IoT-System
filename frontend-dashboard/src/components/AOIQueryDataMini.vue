<template>
    <div class="aoi font-inter">
        <div class="flex flex-col space-y-1">
            <div class="w-full bg-base-300 rounded-lg shadow mb-4 summary-card">
                <div class="flex flex-wrap items-center justify-between gap-4">
                    <div class="flex gap-4">
                        <span class="font-semibold text-xs">{{$t('Filters')}}:</span>
                        <div class="flex flex-wrap items-center gap-2">
                            <div v-if="activeFilters.includes('floor')">
                                <select v-model="filters.floor" @change="onFloorChange"
                                    class="select select-bordered select-xs w-20">
                                    <option value="">All Floor</option>
                                    <option v-for="floor in floor" :key="floor">{{ floor }}</option>
                                </select>
                            </div>

                            <div v-if="activeFilters.includes('date')" class="flex items-center gap-2">
                                <input type="datetime-local" v-model="filters.dateFrom"
                                    class="input input-bordered input-xs" />
                                <span class="strip">-</span>
                                <input type="datetime-local" v-model="filters.dateUntil"
                                    class="input input-bordered input-xs" />
                                <button @click="fetchAllData" class="btn btn-xs btn-accent">{{$t('Apply')}}</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="tabs tabs-border">
                <input type="radio" name="my_tabs_2" class="tab text-xs" v-model="selectedTab" :aria-label="$t('All Info')"
                    value="all" />
                <div class="tab-content bg-transparent p-4 overflow-hidden rounded-lg" v-if="selectedTab==='all'">
                    <div class="card bg-base-300  h-full">
                        <div class="overflow-x-auto flex">
                            <table class="table text-xs">
                                <!-- head -->
                                <thead>
                                    <tr class="text-xs">
                                        <th>{{$t('Floor')}}</th>
                                        <th v-if="columnsVisibility.line">{{$t('Line')}}</th>
                                        <th v-if="columnsVisibility.pass">PASS</th>
                                        <th v-if="columnsVisibility.fail">FAIL</th>
                                        <th v-if="columnsVisibility.total">{{$t('Total')}}</th>
                                        <th v-if="columnsVisibility.yieldRate">Yield Rate (%)</th>
                                    </tr>
                                </thead>
                                <tbody v-if="showSummaryCard">
                                    <tr v-for="(item) in allData" :key="`${item.floor}-${item.line}`"
                                        v-if="allData.length > 0">
                                        <th>{{ item.floor }}</th>
                                        <td v-if="columnsVisibility.line">{{ item.line }}</td>

                                        <td v-if="columnsVisibility.pass">{{ item.pass }}</td>
                                        <td v-if="columnsVisibility.fail">
                                           {{ item.fail }}
                                        </td>
                                        <td v-if="columnsVisibility.total">{{ item.total }}</td>
                                        <td v-if="columnsVisibility.yieldRate">{{ item.yieldRate }}</td>
                                    </tr>

                                    <p class="ms-5 mt-5" v-else-if="allData.length < 0">No product data available for
                                        this line
                                    </p>
                                    <p class="ms-5 mt-5 animate-pulse" v-else>Loading data...</p>
                                </tbody>
                            </table>
                        
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import axios from 'axios'
    type AOIData = {
        floor: string,
        line: string,
        pass: number,
        fail: number,
        total: number,
        yieldRate: number
    }


    export default {
        name: "OEETrendMini",
        props: {},
        data() {
            return {
                filters: {
                    floor: '',
                    dateFrom: '',
                    dateUntil: '',
                    line: '2F17'
                },
                selectedShift: '',
                selectedFloor: '',
                activeFilters: ['date', 'floor'],
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
                    }
                ],
                floor: ['1F', '2F', '3F'],
                lines: [] as string[],
                columnsVisibility: {
                    line: true,
                    model: true,
                    total: true,
                    pass: true,
                    fail: true,
                    yieldRate: true,
                },
                selectedTab: 'all' as String,
                count: 0,
                chartOptionsDynamic: {},
                showAlert: false,
                showSummaryCard: true,
                showDetails: false,
                allData: [] as AOIData[],
                selectedLine: '' as string | '',
              
            }
        },
        mounted() {
            const today = new Date().toISOString().split('T')[0]
            this.filters.dateFrom = `${today}T00:00`
            this.filters.dateUntil = `${today}T23:59`

            this.fetchAllData()
            this.showAlert = true
            setTimeout(() => {
                this.showAlert = false
            }, 10000)
        },
        methods: {
            toggleSummary() {
                this.showSummaryCard = !this.showSummaryCard
            },
            async fetchAllData() {
                const {
                    floor,
                    dateFrom,
                    dateUntil
                } = this.filters;

                this.selectedFloor = floor || '';
                const startDate = dateFrom ? `${dateFrom}:00` : ''
                const endDate = dateUntil ? `${dateUntil}:00` : ''

                const params = new URLSearchParams()

                if (floor) {
                    params.append('floor', floor);
                }
                if (this.selectedShift) {
                    params.append('shift', this.selectedShift)
                }
                if (startDate) params.append('startDate', startDate)
                if (endDate) params.append('endDate', endDate)

                const cacheBuster = `_=${new Date().getTime()}`;
                const apiUrl =
                    `http://10.176.33.76:6002/api/AoiData/yield-summary?${params.toString()}&${cacheBuster}`;
                try {
                    const response = await axios.get(apiUrl)
                    const apiData = response.data || []
                    // console.log("API RAW DATA:", apiData);

                    this.allData = apiData.map((item: any) => ({
                        floor: item.floor || this.selectedFloor || '',
                        line: item.line,
                        pass: item.pass,
                        fail: item.fail,
                        total: item.total,
                        yieldRate: item.yieldRate
                    }))

                    this.lines = [...new Set(this.allData.map(item => item.line ?.toUpperCase()))];

                    if (!this.filters.line && this.lines.length > 0) {
                        this.filters.line = this.lines[0];
                    }

                    console.log("success get data from api")
                } catch (error) {
                    console.error("Gagal mengambil data summary:", error)
                    this.allData = []
                }
            },
             onFloorChange() {
                this.fetchAllData()
            },
   
        }
    }
</script>
<style scoped>
    .summary-card {
        transition: transform 0.2s ease;
    }

    .summary-card:hover {
        transform: translateY(-4px);
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

    .fade-enter-active,
    .fade-leave-active {
        transition: opacity 0.6s ease-in-out;
    }

    .fade-enter-from,
    .fade-leave-to {
        opacity: 0;
    }

    html[data-theme='light'] .font-semibold {
        color: #eee;
    }

    html[data-theme='light'] .strip {
        color: #eee;
    }

    html[data-theme='light'] .tabs-box> :is(.tab-active, [aria-selected="true"]):not(.tab-disabled, [disabled]),
    .tabs-box> :is(input:checked),
    .tabs-box> :is(label:has(:checked)) {
        background-color: #15191e;
        color: #eee;
    }


    html[data-theme='light'] .table {
        color: white;
    }

    html[data-theme='light'] tr {
        border-color: oklch(25.33% 0.016 252.42);
    }

    html[data-theme='light'] th {
        color: #eee;
    }

    html[data-theme='light'] tr:hover {
        background-color: oklch(25.33% 0.016 252.42);
    }

    html[data-theme='light'] .stat-title {
        color: #eee;
    }

    html[data-theme='light'] .stat-value {
        color: #eee;
    }

    html[data-theme='light'] .select {
        background-color: #eee;
    }

    html[data-theme='light'] .input {
        background-color: #eee;
    }

    html[data-theme='light'] .font-semibold {
        color: #eee;
    }

    html[data-theme='light'] .font-bold {
        color: #eee;
    }


    html[data-theme='light'] .collapse-title {
        color: #15191e;
    }

    html[data-theme='light'] .label-text {
        color: #15191e;
    }

    html[data-theme='light'] .radio {
        color: #15191e;
    }

    html[data-theme='light'] .strip {
        color: #eee;
    }

    @keyframes zoomIn {
        from {
            transform: scale(0);
            opacity: 0;
        }

        to {
            transform: scale(1);
            opacity: 1;
        }
    }

    .animate-zoomIn {
        animation: zoomIn .3s ease-out;
    }

    .btn:is(:disabled, [disabled], .btn-disabled):not(.btn-link, .btn-ghost) {
        background-color:
            color-mix(in oklab, #fff 10%, transparent);
        color: color-mix(in oklab, #00d3bb 10%, transparent);
        ;
        box-shadow: none;
    }

    .pagination {
        display: flex;
        gap: 8px;
    }

    .tab-group input {
        appearance: none;
    }

    .tab-group label {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 38px;
        height: 38px;
        line-height: 1.6;
        border: 1px solid #cccccc;
        border-radius: 8px;
        cursor: pointer;
        font-size: 12px;
        font-weight: 700;
        position: relative;
        background-color: transparent;
        transition: all 0.48s cubic-bezier(0.23, 1, 0.32, 1);
    }

    .tab-group label::after {
        content: "";
        position: absolute;
        bottom: -32px;
        left: 50%;
        width: 8px;
        height: 8px;
        border-radius: 50%;
        background-color: #00d3bb;
        transform: translateX(-50%);
        transform-origin: 0 0;
        scale: 0;
        opacity: 0;
        z-index: -1;
        transition: all 0.48s 0.2s cubic-bezier(0.23, 1, 0.32, 1);
    }

    .tab-group label::before {
        content: "";
        position: absolute;
        top: -24px;
        left: 50%;
        width: 38px;
        height: 38px;
        border-radius: 8px;
        background-color: #00d3bb;
        transform: translate(-50%, -50%);
        z-index: -1;
        opacity: 0;
        scale: 0;
        transform-origin: 0 0;
        transition: all 0.48s cubic-bezier(0.23, 1, 0.32, 1);
    }

    .tab-group label span {
        pointer-events: none;
    }

    .tab-group label.active {
        background-color: #00d3bb;
        color: #fff;
        transform: scale(1.1);
    }

    .tab-group label.disabled {
        opacity: 0.5;
        cursor: not-allowed;
    }

    .tab-group label:not(.active):not(.disabled):hover {
        border-color: #00d3bb;
        color: #00d3bb;
    }

    .tab-group label {
        cursor: pointer;
        border-radius: 8px;
        border: 1px solid #cccccc;
        font-size: 12px;
        font-weight: 700;
        background-color: transparent;
        transition: all 0.3s ease;
    }

    .tab-group label:hover {
        border-color: #00d3bb;
        color: #00d3bb;
    }

    .tab-group input:checked+label {
        border-color: transparent;
        color: #ffffff;
        background-color: #00d3bb;
        transform: scale(1.1);
    }

    .tab-group input:checked+label::after {
        bottom: -16px;
        opacity: 1;
        /* scale: 1; */
        box-shadow:
            0rem 6px 13px rgba(10, 255, 214, 0.1),
            0rem 24px 24px rgba(10, 255, 214, 0.09),
            0rem 55px 33px rgba(10, 255, 214, 0.05),
            0rem 97px 39px rgba(10, 255, 214, 0.01),
            0rem 152px 43px rgba(10, 255, 214, 0);
    }

    .tab-group input:checked+label::before {
        opacity: 1;
        scale: 1;
        top: 50%;
        box-shadow:
            0rem 6px 13px rgba(10, 255, 214, 0.1),
            0rem 24px 24px rgba(10, 255, 214, 0.09),
            0rem 55px 33px rgba(10, 255, 214, 0.05),
            0rem 97px 39px rgba(10, 255, 214, 0.01),
            0rem 152px 43px rgba(10, 255, 214, 0);
    }
</style>