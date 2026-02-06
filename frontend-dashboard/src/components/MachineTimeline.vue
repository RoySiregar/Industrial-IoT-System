<template>
    <div class="machine-timeline">
        <div class="w-full bg-base-300 p-4 rounded-lg shadow mb-2 summary-card">
            <div class="flex flex-wrap items-center justify-between gap-4">
                <div class="flex gap-4">
                    <span class="font-semibold">{{$t('Filters')}}:</span>
                    <div class="flex flex-wrap items-center gap-2">

                        <div v-if="activeFilters.includes('line')">
                            <select v-model="filters.line" class="select select-bordered select-sm">
                                <option value="">{{$t('Choose Line')}}</option>
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
                            {{$t('All Shifts')}}
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

        <div class="w-full bg-base-300 p-4 summary-card justify-between rounded-lg shadow">
            <h3 class="text-lg font-bold">{{ selectedLine ? `${selectedLine} ${$t('Timeline')}` : $t('Overall Timeline') }}</h3>
            <v-chart :option="chartOptionsOEETimeline" autoresize style="height: 400px" @finished="onChartReady">
            </v-chart>

            <div v-if="selectedMachine" class="mt-6 bg-base-200 p-4 rounded">
                <h2 class="text-lg font-bold mb-2">Detail for {{ selectedMachine }}</h2>
                <table class="table-auto w-full text-left border border-gray-700">
                    <thead>
                        <tr>
                            <th class="px-4 py-2 border-b border-gray-600">Status</th>
                            <th class="px-4 py-2 border-b border-gray-600">Duration (hours)</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item, index) in getMachineData(selectedMachine)" :key="index">
                            <td class="px-4 py-2 border-b border-gray-700">{{ item.status }}</td>
                            <td class="px-4 py-2 border-b border-gray-700">{{ item.value }}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { CanvasRenderer } from 'echarts/renderers'
import { BarChart } from 'echarts/charts'
import { GridComponent, TooltipComponent, LegendComponent } from 'echarts/components' // GraphicComponent tidak terpakai
import * as echart from 'echarts/core'
import { nextTick } from 'vue'

echart.use([
    CanvasRenderer,
    BarChart,
    GridComponent,
    TooltipComponent,
    LegendComponent
])

// Tipe untuk data dari API
type ApiData = {
    cell_id: string;
    status: string;
    totalHours: number;
};

// Menghapus tipe MachineId yang hardcoded
// type MachineId = 'M001' | 'M002' | 'M003' | 'M004' | 'M005';

export default {
    data(): Record<string, any> {
        // Helper untuk default tanggal (7 hari lalu s/d hari ini)
        const today = new Date();
        const lastWeek = new Date(today.getTime() - 7 * 24 * 60 * 60 * 1000);

        return {
            selected: 'Group A' as string | null,
            selectedLine: '3F13', // Ini tampaknya tidak terpakai, `filters.line` yang dipakai
            selectedstation: 'R1',
            selectedShift: '', // Default ke "All Shifts"
            filters: {
                dateFrom: lastWeek.toISOString().split('T')[0], // Default: 7 hari lalu
                dateUntil: today.toISOString().split('T')[0], // Default: hari ini
                line: '3F13'
            },
            activeFilters: ['line', 'date'],
            availableFilters: [
                { label: 'Site', value: 'site' },
                { label: 'BU', value: 'bu' },
                { label: 'Date Range', value: 'date' },
                { label: 'Factory', value: 'factory' },
                { label: 'Line', value: 'line' }
            ],

            lines: ['2F13', '2F16', '2F17', '2F19', '3F13', '3F14', '3F15', '3F17'],
            stations: ['R1', 'R2', 'R3', 'R4', 'R5', 'R6'],
            years: ['2024', '2023', '2025', '2026'],
            year: '2025',
            months: '7',
            chartInstance: null as echart.ECharts | null,
            selectedMachine: null as string | null, // Tipe diubah ke string

            chartOptionsOEETimeline: {
                backgroundColor: 'transparent',
                title: { text: null },
                legend: {
                    textStyle: { color: '#FFFFFF' }
                },
                grid: {
                    left: '10%',
                    right: '10%',
                    bottom: '15%',
                    containLabel: true
                },
                tooltip: {
                    trigger: 'axis',
                    axisPointer: { type: 'shadow' },
                    formatter: function (params: any[]) {
                        let tooltip = params[0].name + '<br/>';
                        params.forEach(item => {
                            // Bulatkan jam ke 2 desimal
                            const hours = parseFloat(item.value).toFixed(2);
                            tooltip += `${item.marker} ${item.seriesName}: ${hours} hour(s)<br/>`;
                        });
                        return tooltip;
                    }
                },
                xAxis: {
                    type: 'value',
                    name: 'Total Hours', // Nama diubah
                    axisLabel: {
                        color: '#FFFFFF',
                        textShadowColor: 'rgba(255, 255, 255, 0.7)',
                        textShadowBlur: 10,
                        // Formatter 24 jam dihapus karena datanya adalah total jam
                    },
                    splitLine: { lineStyle: { color: '#454545' } },
                    // min, max, splitNumber dihapus agar ECharts bisa auto-scale
                },
                yAxis: {
                    type: 'category',
                    data: [], // Akan diisi oleh API
                    axisLabel: {
                        color: '#FFFFFF',
                        textShadowColor: 'rgba(255, 255, 255, 0.7)',
                        textShadowBlur: 10,
                    },
                    axisLine: { lineStyle: { color: '#454545' } },
                },
                series: [] // Akan diisi oleh API
            }
        }
    },
    watch: {
        // Muat ulang data jika line atau shift berubah
        'filters.line'(newLine: string) {
            if (newLine) {
                this.fetchData();
            }
        },
        selectedShift() {
            this.fetchData();
        }
    },
    mounted() {
        // Ambil data saat komponen pertama kali dimuat
        this.fetchData();
    },
    methods: {
        async fetchData() {
            // (Opsional) Tampilkan loading state di sini
            this.selectedMachine = null; // Sembunyikan detail tabel saat memuat data baru

            const { line, dateFrom, dateUntil } = this.filters;
            const shift = this.selectedShift;

            if (!line || !dateFrom || !dateUntil) {
                console.warn('Filter tidak lengkap. Data tidak diambil.');
                return;
            }

            // Format tanggal agar sesuai dengan contoh API
            const startDate = `${dateFrom} 00:00:00`;
            const endDate = `${dateUntil} 23:59:59`; // Gunakan 23:59:59 untuk mencakup seluruh hari

            // Bangun URL dengan parameter
            const baseUrl = 'http://10.176.33.76:6002/api/MachineTimeline/summary';
            const params = new URLSearchParams({
                line: line,
                startDate: startDate,
                endDate: endDate
            });

            // Hanya tambahkan parameter 'shift' jika nilainya ada (bukan string kosong)
            if (shift) {
                params.append('shift', shift);
            }

            try {
                const response = await fetch(`${baseUrl}?${params.toString()}`);
                if (!response.ok) {
                    throw new Error(`API request failed with status ${response.status}`);
                }
                const apiData: ApiData[] = await response.json();
                
                // Proses data dan update chart
                this.processApiData(apiData);

            } catch (error) {
                console.error('Error fetching timeline data:', error);
                // (Opsional) Tampilkan pesan error ke user
                // Kosongkan chart jika terjadi error
                this.chartOptionsOEETimeline.yAxis.data = [];
                this.chartOptionsOEETimeline.series = [];
            } finally {
                // (Opsional) Sembunyikan loading state di sini
            }
        },

        processApiData(apiData: ApiData[]) {
            if (!apiData || apiData.length === 0) {
                console.warn('API returned no data.');
                this.chartOptionsOEETimeline.yAxis.data = [];
                this.chartOptionsOEETimeline.series = [];
                return;
            }

            // 1. Dapatkan semua cell_id (kategori) unik dan urutkan
            const categories = [...new Set(apiData.map(d => d.cell_id))].sort();
            
            // 2. Dapatkan semua status unik dan urutkan
            const statuses = [...new Set(apiData.map(d => d.status))].sort(); // Misal: ['DOWN', 'FAILED', 'PASSED']

            // 3. Buat mapping dari cell_id ke index (untuk performa)
            const categoryIndex = new Map(categories.map((cat, i) => [cat, i]));

            // 4. Siapkan struktur data untuk series
            const seriesData: Record<string, number[]> = {};
            statuses.forEach(status => {
                seriesData[status] = new Array(categories.length).fill(0); // Buat array [0, 0, 0, ...]
            });

            // 5. Isi data series dari API
            for (const item of apiData) {
                const index = categoryIndex.get(item.cell_id); // Dapatkan index (0, 1, 2, ...)
                if (index !== undefined) {
                    // Bulatkan data untuk tampilan
                    seriesData[item.status][index] = parseFloat(item.totalHours.toFixed(2));
                }
            }

            // 6. Definisikan warna untuk setiap status
            const statusColors: Record<string, string> = {
                'PASSED': '#00DFA2', // Dari kode asli
                'DOWN': '#FF1700',   // Dari kode asli
                'FAILED': '#FFC700'  // Warna baru untuk FAILED
            };

            // 7. Format data menjadi ECharts series
            const chartSeries = statuses.map(status => ({
                name: status,
                type: 'bar',
                stack: 'total',
                label: { 
                    show: true,
                    // Sembunyikan label jika nilainya 0
                    formatter: (params: any) => params.value > 0 ? params.value : '' 
                },
                emphasis: { focus: 'series' },
                itemStyle: { color: statusColors[status] || '#FFFFFF' }, // Fallback ke putih
                data: seriesData[status]
            }));

            // 8. Update chart options
            this.chartOptionsOEETimeline.yAxis.data = categories;
            this.chartOptionsOEETimeline.series = chartSeries;
        },

        onChartReady() {
            // Logika ini sudah benar, akan mengambil instance chart
            // dan menambahkan click listener
            nextTick(() => {
                // Gunakan $el untuk mencari di dalam komponen ini saja
                const chartEl = this.$el.querySelector('.v-chart canvas') as HTMLCanvasElement;
                if (!chartEl) return;

                this.chartInstance = echart.getInstanceByDom(chartEl);
                if (!this.chartInstance) return;

                this.chartInstance.off('click'); // Hapus listener lama

                this.chartInstance.on('click', (params: any) => {
                    if (params.componentType === 'series') {
                        const clickedIndex = params.dataIndex;
                        // Ambil nama mesin dari yAxis.data yang sudah dinamis
                        const clickedMachine = this.chartOptionsOEETimeline.yAxis.data[clickedIndex];
                        this.selectedMachine = clickedMachine;
                    }
                });
            });
        },

        getMachineData(machineId: string) { // Tipe diubah ke string
            // Fungsi ini diupdate untuk membaca dari data dinamis
            const index = this.chartOptionsOEETimeline.yAxis.data.indexOf(machineId);
            if (index === -1) {
                return []; // Mesin tidak ditemukan
            }

            // Map data dari semua series untuk index mesin yang di-klik
            return this.chartOptionsOEETimeline.series.map((s: any) => ({
                status: s.name,
                value: s.data[index] // Ambil data pada index yang sesuai
            }));
        }
    }
}
</script>

<style scoped>
/* CSS Anda (tidak ada perubahan) */
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
.radio-button__input:checked + .radio-button__label .radio-button__custom {
    transform: translateY(-50%) scale(0.9);
    border: 5px solid #00d3bb;
    color: #00d3bb;
}
.radio-button__input:checked + .radio-button__label {
    color: #00d3bb;
}
.radio-button__label:hover .radio-button__custom {
    transform: translateY(-50%) scale(1.2);
    border-color: #00d3bb;
    box-shadow: 0 0 10px #00d3bb80;
}
</style>