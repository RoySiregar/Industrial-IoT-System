<template>
  <div class="oee-board">
    <div class="grid grid-cols-4 gap-4 overflow-hidden w-full justify-between">
      <div class="col-span-4">
        <div class="w-full bg-base-300 p-4 rounded-lg shadow mb-4 summary-card">
          <div class="flex flex-wrap items-center justify-between gap-4">
            <div class="flex gap-4">
              <div class="flex flex-wrap items-center gap-2">
                
                <div v-if="activeFilters.includes('line')">
                  <select v-model="filters.line" @change="fetchOeeData" class="select select-bordered select-sm">
                    <option value="">{{$t('Choose Line')}}</option>
                    <option v-for="line in lines" :key="line">{{ line }}</option>
                  </select>
                </div>

                <div v-if="activeFilters.includes('date')" class="flex items-center gap-2">
                  <input type="datetime-local" v-model="filters.dateFrom" class="input input-bordered input-sm" />
                  <span class="strip">-</span>
                  <input type="datetime-local" v-model="filters.dateUntil" class="input input-bordered input-sm" />
                  <button @click="fetchOeeData" class="btn btn-sm btn-accent">{{$t('Apply')}}</button>
                </div>
              </div>
            </div>
             <div class="radio-buttons-container flex items-center gap-4 mt-4">
                  <div class="radio-button">
                      <input name="radio-group" id="radio0" class="radio-button__input" type="radio" value=""
                          v-model="selectedShift" @change="fetchOeeData">
                      <label for="radio0" class="radio-button__label">
                          <span class="radio-button__custom"></span>
                          {{$t('All Shifts')}}
                      </label>
                  </div>
                  <div class="radio-button">
                      <input name="radio-group" id="radio1" class="radio-button__input" type="radio" value="Morning"
                          v-model="selectedShift" @change="fetchOeeData">
                      <label for="radio1" class="radio-button__label">
                          <span class="radio-button__custom"></span>
                          {{$t('Day Shift')}}
                      </label>
                  </div>
                  <div class="radio-button">
                      <input name="radio-group" id="radio2" class="radio-button__input" type="radio" value="Mid"
                          v-model="selectedShift" @change="fetchOeeData">
                      <label for="radio2" class="radio-button__label">
                          <span class="radio-button__custom"></span>
                          {{$t('Middle Shift')}}
                      </label>
                  </div>
                  <div class="radio-button">
                      <input name="radio-group" id="radio3" class="radio-button__input" type="radio" value="Night"
                          v-model="selectedShift" @change="fetchOeeData">
                      <label for="radio3" class="radio-button__label">
                          <span class="radio-button__custom"></span>
                          {{$t('Night Shift')}}
                      </label>
                  </div>
              </div>
          </div>
        </div>
        <div class="bg-base-300 summary-card w-full rounded-lg justify-between h-full">
          <div class="flex px-5 pt-10">
            <v-chart :option="chartOption" style="height: 500px ; width: 100%"></v-chart>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import axios from 'axios';
import 'echarts'
import { CanvasRenderer } from 'echarts/renderers'
import { LineChart } from 'echarts/charts'
import {
  GridComponent,
  TooltipComponent,
  LegendComponent,
  GraphicComponent,
  ToolboxComponent,
  DataZoomComponent
} from 'echarts/components'
import * as echart from 'echarts/core'

echart.use([
  CanvasRenderer,
  LineChart,
  TooltipComponent,
  LegendComponent,
  GraphicComponent,
  GridComponent,
  ToolboxComponent,
  DataZoomComponent
])

// BARU: Definisikan tipe data untuk hasil API OEE
type OeeApiEntry = {
    workDate: string;
    shiftName: 'Morning' | 'Mid' | 'Night' | string; // Bisa string umum jika ada shift lain
    line: string;
    totalQty: number;
    goodQty: number;
    quality: number;
    availability: number;
    performance: number;
    oee: number;
}

export default {
  data(): Record < string, any > {
    return {
      dataPollingOEE: null as any,
      selected: 'Group A' as string | null,
      selectedLine: '3F13',
      selectedstation: 'R1',
      showDropdown: false,
      selectedShift: '',
      idealCycleTimeSeconds: 16,
      plannedSecondsPerShift: 25200,
      filters: {
        floor: '',
        dateFrom: '',
        dateUntil: '',
        line: '3F13'
      },
      activeFilters: ['line', 'date'],
      availableFilters: [ /* ... filter lainnya ... */ { label: 'Line', value: 'line' }],
      lines: ['2F13', '2F16', '2F17', '2F19', '3F13', '3F14', '3F15', '3F16', '3F17'],
      today: new Date().toISOString().split('T')[0],
      day: new Date().toLocaleDateString('en-US', { weekday: 'long' }),
      time: { hours: '00', minutes: '00', seconds: '00' },
      chartOption: {
          backgroundColor: 'transparent',
          title: { text: 'OEE Trend', left: 'center', textStyle: { color: '#fff' } },
          tooltip: {
            trigger: 'axis',
            formatter: (params: any[]) => {
                let tooltipText = params[0].axisValueLabel + '<br/>';
                params.forEach(param => {
                    tooltipText += `${param.marker} ${param.seriesName}: ${(param.value * 100).toFixed(2)}%<br/>`;
                });
                return tooltipText;
            }
          },
          legend: {
            top: 30, padding: [5, 0, 5, 0], itemGap: 20,
            data: ['OEE', 'Availability', 'Performance', 'Quality'],
            selected: { 'OEE': true, 'Availability': true, 'Performance': true, 'Quality': true },
            textStyle: { fontSize: 10, color: '#fff' }
          },
          toolbox: {
            right: 10,
            feature: {
              dataZoom: { yAxisIndex: 'none' },
              restore: {},
              saveAsImage: { show: true, title: 'Save PNG', backgroundColor: '#1d232a' }
            },
            iconStyle: { borderColor: '#fff' }
          },
          grid: { top: 120, left: 60, right: 40, bottom: 100 },
          xAxis: {
            type: 'category', data: [],
            axisLine: { lineStyle: { color: '#FFFFFF' } },
            axisLabel: {
              show: true, interval: 'auto', fontSize: 10, color: '#fff',
              formatter: (value: string) => value.split(' ')[1] || value
            },
            splitLine: { lineStyle: { color: '#454545' } }
          },
          yAxis: {
            type: 'value', name: 'OEE (%)', nameTextStyle: { color: '#fff' }, max: 1,
            axisLabel: { color: '#fff', fontSize: 12, formatter: (val: number) => `${(val * 100).toFixed(0)}%` },
            splitLine: { lineStyle: { color: '#454545' } }
          },
          dataZoom: [
            {
              type: 'slider', show: true, start: 0, end: 100, bottom: 30,
              backgroundColor: 'rgba(47,69,84,0.4)',
              dataBackground: { areaStyle: { color: 'rgba(47,69,84,0.3)' }, lineStyle: { opacity: 0.8, color: '#8392A5' } },
              fillerColor: 'rgba(167,183,204,0.4)', borderColor: '#ddd',
              handleStyle: { color: '#fff', shadowBlur: 3, shadowColor: 'rgba(0,0,0,0.6)', shadowOffsetX: 2, shadowOffsetY: 2 },
              textStyle: { color: '#fff' }
            },
            { type: 'inside', start: 0, end: 100 }
          ],
          series: []
        }
    }
  },
              
  mounted() {
      // 1. Ambil waktu sekarang
      const now = new Date();
      
      // 2. Hitung waktu 24 jam yang lalu (24 jam * 60 menit * 60 detik * 1000 milidetik)
      const past24h = new Date(now.getTime() - (24 * 60 * 60 * 1000));

      // 3. Format dan masukkan ke state filters
      // dateUntil = Waktu sekarang
      this.filters.dateUntil = this.formatDateTimeLocal(now);
      
      // dateFrom = 24 jam yang lalu
      this.filters.dateFrom = this.formatDateTimeLocal(past24h);

      // 4. Panggil data
      this.fetchOeeData();
      
      // 5. Jalankan polling
      this.dataPollingOEE = setInterval(this.fetchOeeData, 30000);
    },

  beforeUnmount() {
    clearInterval(this.dataPollingOEE);
  },

  methods: {
    formatDateTimeLocal(date: Date): string {
      const pad = (n: number) => String(n).padStart(2, '0')
      const year = date.getFullYear();
      const month = pad(date.getMonth() + 1);
      const day = pad(date.getDate());
      const hours = pad(date.getHours());
      const minutes = pad(date.getMinutes());
      return `${year}-${month}-${day}T${hours}:${minutes}`;
    },
    async fetchOeeData() {
      const { line, dateFrom, dateUntil } = this.filters;
      const shift = this.selectedShift;

      if (!line) {
        this.chartOption.xAxis.data = [];
        this.chartOption.series = [];
        return;
      }

      const startDate = dateFrom ? dateFrom.split('T')[0] : '';
      const endDate = dateUntil ? dateUntil.split('T')[0] : '';

      const params = new URLSearchParams({
        line: line,
        startDate: startDate,
        endDate: endDate,
        idealCycleTimeSeconds: this.idealCycleTimeSeconds.toString(),
        plannedSecondsPerShift: this.plannedSecondsPerShift.toString(),
      });

      const cacheBuster = `_=${new Date().getTime()}`;
      const apiUrl = `http://10.175.80.201:5001/api/OeeByWorkshifts/calculate?${params.toString()}&${cacheBuster}`;

      try {
        const response = await axios.get(apiUrl);
        // MODIFIKASI: Beri tipe spesifik pada apiData
        let apiData: OeeApiEntry[] = response.data || [];

        if (shift) {
          apiData = apiData.filter((item) => item.shiftName === shift);
        }

        if (apiData.length === 0) {
            this.chartOption.xAxis.data = [];
            this.chartOption.series = [];
            return;
        }

        const shiftOrder: Record<string, number> = { "Morning": 1, "Mid": 2, "Night": 3 };
        // MODIFIKASI: Beri tipe spesifik pada a dan b
        apiData.sort((a: OeeApiEntry, b: OeeApiEntry) => {
            const dateA = new Date(a.workDate).getTime();
            const dateB = new Date(b.workDate).getTime();
            if (dateA !== dateB) {
                return dateA - dateB;
            }
            // PERBAIKAN: Akses shiftOrder dengan key yang tipenya diketahui
            const shiftA = a.shiftName as keyof typeof shiftOrder;
            const shiftB = b.shiftName as keyof typeof shiftOrder;
            return (shiftOrder[shiftA] || 99) - (shiftOrder[shiftB] || 99);
        });

        const categories = apiData.map((item) => `${item.workDate.split('T')[0]} ${item.shiftName}`);
        const oeeData = apiData.map((item) => item.oee);
        const availabilityData = apiData.map((item) => item.availability);
        const performanceData = apiData.map((item) => item.performance);
        const qualityData = apiData.map((item) => item.quality);

        this.chartOption.xAxis.data = categories;
        this.chartOption.series = [
            { name: 'OEE', type: 'line', data: oeeData, smooth: false, showSymbol: true, symbolSize: 6, itemStyle: { color: '#00DFA2' }, lineStyle: { color: '#00DFA2', width: 3 }, label: { show: true, position: 'top', formatter: (params: any) => `${(Number(params.value)* 100).toFixed(1)}%`, fontSize: 9, color: '#00DFA2' } },
            { name: 'Availability', type: 'line', data: availabilityData, smooth: false, showSymbol: true, symbolSize: 6, itemStyle: { color: '#0079FF' }, lineStyle: { color: '#0079FF', width: 3 }, label: { show: true, position: 'top', formatter: (params: any) => `${(Number(params.value)* 100).toFixed(1)}%`, fontSize: 9, color: '#0079FF' } },
            { name: 'Performance', type: 'line', data: performanceData, smooth: false, showSymbol: true, symbolSize: 6, itemStyle: { color: '#F6FA70' }, lineStyle: { color: '#F6FA70', width: 3 }, label: { show: true, position: 'top', formatter: (params: any) => `${(Number(params.value)* 100).toFixed(1)}%`, fontSize: 9, color: '#F6FA70' } },
            { name: 'Quality', type: 'line', data: qualityData, smooth: false, showSymbol: true, symbolSize: 6, itemStyle: { color: '#FF0060' }, lineStyle: { color: '#FF0060', width: 3 }, label: { show: true, position: 'top', formatter: (params: any) => `${(Number(params.value)* 100).toFixed(1)}%`, fontSize: 9, color: '#FF0060' } }
        ];

      } catch (error) {
        console.error("Gagal mengambil data OEE:", error);
        this.chartOption.xAxis.data = [];
        this.chartOption.series = [];
      }
    },
  },
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
    gap: 15px; /* Mengurangi gap */
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
    padding-left: 25px; /* Sedikit mengurangi padding */
    margin-bottom: 0; /* Menghapus margin bawah */
    position: relative;
    font-size: 11px; /* Sedikit mengecilkan font */
    color: #fff;
    cursor: pointer;
    line-height: 20px; /* Menyesuaikan line-height */
    transition: all 0.3s cubic-bezier(0.23, 1, 0.320, 1);
  }

  .radio-button__custom {
    position: absolute;
    top: 50%;
    left: 0;
    transform: translateY(-50%);
    width: 18px; /* Mengecilkan ukuran radio */
    height: 18px;
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
    transform: translateY(-50%) scale(1.1); /* Sedikit mengurangi efek hover */
    border-color: #00d3bb;
    box-shadow: 0 0 8px #00d3bb80; /* Mengurangi shadow */
  }

html[data-theme='light'] .bg-base-300 {
    background-color: #15191e;
  }

  html[data-theme='light'] .select {
    background-color: #eee;
  }

  html[data-theme='light'] .input {
    background-color: #eee;
  }

  html[data-theme='light'] .strip{
      color: #eee;
    }

</style>






