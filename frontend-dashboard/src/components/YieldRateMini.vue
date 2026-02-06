<template>
    <div class="yield-rate font-inter ">
        <div class="flex flex-col space-y-1">
            <div class="w-full bg-base-300  rounded-lg shadow mb-4">
                <div class="flex flex-wrap items-center justify-between gap-4">
                    <div class="flex gap-4">
                        <span class="font-semibold text-xs">{{$t('Filters')}}:</span>
                        <div class="flex flex-wrap items-center gap-2">
                            <div v-if="activeFilters.includes('line')">
                                <select v-model="filters.line" @change="onLineChange"
                                    class="select select-bordered select-xs w-20">
                                    <option disabled value="">{{$t('Choose Line')}}</option>
                                    <option v-for="line in lines" :key="line" class="text-xs">{{ line }}</option>
                                </select>
                            </div>
                            <div v-if="activeFilters.includes('date')" class="flex items-center gap-2 text-xs">
                                <input type="datetime-local" v-model="filters.dateFrom"
                                    class="input input-bordered input-xs" />
                                <span class="strip">-</span>
                                <input type="datetime-local" v-model="filters.dateUntil"
                                    class="input input-bordered input-xs" />
                                <button @click="fetchSummaryData"
                                    class="btn btn-xs btn-accent ">{{$t('Apply')}}</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="bg-base-300  rounded-lg shadow overflow-hidden">
                <div class="flex justify-between items-center">
                    <h3 class="text-xs font-bold">
                        {{ selectedLine ? `${selectedLine} ${$t('Summary')}` : $t('Overall Summary') }}</h3>
                </div>
                <div>
                    <div class="overflow-x-auto mt-4 flex">
                        <table class="table w-full text-xs">
                            <thead>
                                <tr class="text-xs">
                                    <th>{{$t('Floor')}}</th>
                                    <th>{{$t('Station')}}</th>
                                    <th>{{$t('Group')}}</th>
                                    <th>PASS</th>
                                    <th>FAIL</th>
                                    <th>{{$t('Total')}}</th>
                                    <th>Pass Rate (%)</th>
                                </tr>
                            </thead>
                            <tbody v-if="showSummaryCard && stationData.length > 0">
                                <tr v-for="(item) in stationData" :key="`${item.station}-${item.group}`">
                                    <td>{{ item.floor }}</td>
                                    <td>
                                        {{ item.station }}
                                    </td>
                                    <td>
                                        {{ item.group }}
                                    </td>
                                    <td>{{ item.pass }}</td>
                                    <td>{{ item.fail }}</td>
                                    <td>{{ item.total }}</td>
                                    <td class="text-xs" v-html="getPassRate(item.passRate)" />
                                </tr>
                            </tbody>
                            <p class="ms-5 mt-5" v-else-if="stationData.length < 0">No product data available for
                                this line
                            </p>

                            <p class="ms-5 mt-5 animate-pulse" v-else>Loading data...</p>
                        </table>

                    </div>

                </div>
                
            </div>
        </div>

    </div>
</template>

<script lang="ts">
    import axios from 'axios'

    type StationData = {
        floor: string // NEW: Added floor property
        station: string
        group: string
        total: number
        pass: number
        fail: number
        passRate: number
        repeatRate: number | string
    }

    export default {
        name: "YieldRateMini",
        props: {},
        data() {
            return {
                dataPolling: null as any,
                showSummaryCard: true,
                stationList: [] as string[],
                selectedLine: '3F13',
                selectedSubmenu: null as string | null,
                expandedStation: [] as string[],
                stationData: [] as StationData[],
                showDropdown: false,
                filters: {
                    floor: '',
                    dateFrom: '',
                    dateUntil: '',
                    line: '3F13'
                },
                selectedShift: '',
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
                lines: ['2F13', '2F16', '2F17', '2F19', '3F13', '3F14', '3F15', '3F17']
            }
        },

        beforeUnmount() {
            clearInterval(this.dataPolling);
        },

        methods: {
            async fetchSummaryData() {
                const {
                    line,
                    dateFrom,
                    dateUntil
                } = this.filters;
                if (!line) return;
                this.selectedLine = line;

                const startDate = dateFrom ? `${dateFrom}:00` : '';
                const endDate = dateUntil ? `${dateUntil}:00` : '';

                const params = new URLSearchParams();
                params.append('line', line);
                if (this.selectedShift) {
                    params.append('shift', this.selectedShift);
                }
                if (startDate) params.append('startDate', startDate);
                if (endDate) params.append('endDate', endDate);

                const cacheBuster = `_=${new Date().getTime()}`;
                // DIUBAH: URL API diperbarui
                const apiUrl =
                    `http://10.176.33.76:6002/api/CategoryDetail/group-summary?${params.toString()}&${cacheBuster}`;

                try {
                    const response = await axios.get(apiUrl);
                    const apiData = response.data || [];

                    // DIUBAH: Disesuaikan dengan nama field baru dari API (passCount, failCount, passRate)
                    this.stationData = apiData.map((item: any) => ({
                        floor: item.floor,
                        station: item.stationName,
                        group: item.groupName,
                        total: item.total,
                        pass: item.passCount,
                        fail: item.failCount,
                        passRate: item.passRate,
                        repeatRate: 'N/A'
                    }));

                    this.stationList = [...new Set(apiData.map((item: any) => item.stationName))] as string[];
                } catch (error) {
                    console.error("Gagal mengambil data summary:", error);
                    this.stationData = [];
                    this.stationList = [];
                }
            },
            toggleSummary() {
                this.showSummaryCard = !this.showSummaryCard
            },
            onLineChange() {
                this.fetchSummaryData()
                this.showSummaryCard = true
            },
            summaryToogle() {
                this.showSummaryCard = true
            },

            toggleDropdown() {
                this.showDropdown = !this.showDropdown
            },

            /*------------------------ Sela ------------------------ */
            getPassRate(val: number | null): string {
                if (val === null) return '-';
                const display = `${val.toFixed(2)}%`

                if (val >= 71 && val <= 81) {
                    return `<div class="badge badge-xs badge-warning">${display}</div>`
                } else if (val >= 81 && val <= 92) {
                    return `<div class="badge badge-xs badge-accent">${display}</div>`
                } else if (val >= 92) {
                    return `<div class="badge badge-xs badge-success">${display}</div>`
                } else {
                    return `<div class="badge badge-xs badge-error">${display}</div>`
                }
            }
            /*------------------------ Sela ------------------------ */
        },

        mounted() {
            const today = new Date().toISOString().split('T')[0];
            this.filters.dateFrom = `${today}T00:00`;
            this.filters.dateUntil = `${today}T23:59`;
            this.fetchSummaryData();
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

    html[data-theme='dark'] .menu :where(li ul):before {
        background-color: #15191e;
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

    html[data-theme='light'] .bg-base-300 {
        background-color: #15191e;
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
</style>