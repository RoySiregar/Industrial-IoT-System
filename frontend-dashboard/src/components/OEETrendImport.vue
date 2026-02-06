<template>
    <div class="oee-board">

        <div class="grid grid-cols-4 gap-4 overflow-hidden w-full justify-between">
            <!-- Card 1 Line Description -->
            <div class="col-span-1">
                <div class="bg-base-300 summary-card p-4 rounded-lg w-full h-full">
                    <div class="xl:flex xl:justify-between lg:flex lg:justify-between">
                        <div class="col-span-1 row-span-1">
                            <span class="font-mono text-2l">{{ today }}</span>
                        </div>

                        <div class="col-span-1 row-span-1">
                            <span class="font-mono text-2l">{{ $t(`days.${day}`) }}</span>
                        </div>

                        <div class="col-span-1 row-span-1">
                            <span class="countdown font-mono text-2l">
                                <span :style="`--value:${time.hours}`">{{ time.hours }}</span> :
                                <span :style="`--value:${time.minutes}`">{{ time.minutes }}</span> :
                                <span :style="`--value:${time.seconds}`">{{ time.seconds }}</span>
                            </span>

                        </div>
                    </div>

                    <div class="summary-card rounded-lg w-full h-full mt-5">
                        <h2 class="text-lg font-bold font-mono">{{ $t('Upload your data OEE for show the Chart!')}}</h2>
                        <div class="flex flex-col gap-4 mt-5">
                            <div class="xl:flex xl:gap-2 lg:grid lg:gap-y-4 md:grid md:gap-y-2 ">
                                <label class="input input-sm w-40" disabled>
                                    <span class="floating-label">{{$t('Select Year')}}</span>
                                    <select v-model="year" class="select select-sm select-ghost w-20" disabled>
                                        <option v-for="year in years" :key="year">{{ year}}</option>
                                    </select>
                                </label>
                                <label class="input input-sm xl:w-48 lg:w-48 md:w-40" disabled>
                                    <span class="floating-label">{{$t('Select Month')}}</span>
                                    <select v-model="month"
                                        class="select select-sm select-ghost xl:w-48 lg:w-48 md:w-30" disabled>
                                        <option v-for="m in 12" :key="m" :value="m">
                                            {{ year }} - {{ m.toString().padStart(2,'0') }}
                                        </option>
                                    </select>
                                </label>
                            </div>
                            <div class="xl:flex xl:gap-2 lg:grid lg:gap-y-4 md:grid md:gap-y-2">
                                <label class="input input-sm w-40" disabled>
                                    <span class="floating-label">{{$t('Select Floor')}}</span>
                                    <select v-model="f" class="select select-sm select-ghost w-20" disabled>
                                        <option v-for="f in floor" :key="f">{{ f }}</option>
                                    </select>
                                </label>
                                <label class="input input-sm xl:w-48 lg:w-48 md:w-40" disabled>
                                    <span class="floating-label">{{$t('Select Format')}}</span>
                                    <select v-model="format"
                                        class="select select-sm select-ghost xl:w-48 lg:w-48 md:w-30" disabled>
                                        <option v-for="format in formats" :key="format">
                                            {{ format }}
                                        </option>
                                    </select>
                                </label>
                            </div>
                            <div class="place-self-center m-5 ">
                                <form class="file-upload-form" @dragover.prevent="onDragOver" @drop.prevent="onDrop">
                                    <label for="file" class="file-upload-label bg-base-200">
                                        <div class="file-upload-design">
                                            <svg viewBox="0 0 640 512" height="1em">
                                                <path
                                                    d="M144 480C64.5 480 0 415.5 0 336c0-62.8 40.2-116.2 96.2-135.9c-.1-2.7-.2-5.4-.2-8.1c0-88.4 71.6-160 160-160c59.3 0 111 32.2 138.7 80.2C409.9 102 428.3 96 448 96c53 0 96 43 96 96c0 12.2-2.3 23.8-6.4 34.6C596 238.4 640 290.1 640 352c0 70.7-57.3 128-128 128H144zm79-217c-9.4 9.4-9.4 24.6 0 33.9s24.6 9.4 33.9 0l39-39V392c0 13.3 10.7 24 24 24s24-10.7 24-24V257.9l39 39c9.4 9.4 24.6 9.4 33.9 0s9.4-24.6 0-33.9l-80-80c-9.4-9.4-24.6-9.4-33.9 0l-80 80z">
                                                </path>
                                            </svg>
                                            <p>{{$t('Drag and Drop')}}</p>
                                            <p>{{$t('or')}}</p>
                                            <span class="browse-button">{{$t('Browse file')}}</span>
                                        </div>
                                        <input id="file" type="file" @change="handleFile" accept=".xlsx, .xls" />
                                    </label>
                                </form>
                            </div>
                            <span class="place-self-center txt text-sm">{{$t('Click')}} <a
                                    href="./oee-trend-sheet.xlsx" class="text-info underline">{{$t('here')}}</a>
                                {{$t('to download the ready-to-use template!')}}</span>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Card 2 OEE Chart  -->
            <div class="col-span-3">
                <div class="w-full bg-base-300 p-4 rounded-lg shadow mb-4 summary-card">
                    <div class="flex flex-wrap items-center justify-between gap-4">
                        <div class="flex gap-4">
                            <span class="font-semibold">{{$t('Filters')}} :</span>
                            <div class="flex flex-wrap items-center gap-2">
                                <div v-if="activeFilters.includes('floor')">
                                    <select v-model="filters.floor" class="select select-bordered select-sm" disabled>
                                        <option disabled value="">{{$t('Choose Floor')}}</option>
                                        <option v-for="f in floor" :key="f">{{ f }}</option>
                                    </select>
                                </div>

                                <div v-if="activeFilters.includes('line')">
                                    <select v-model="filters.line" class="select select-bordered select-sm" disabled>
                                        <option disabled value="">{{$t('Choose Line')}}</option>
                                        <option v-for="line in lines" :key="line">{{ line }}</option>
                                    </select>
                                </div>

                                <div v-if="activeFilters.includes('date')" class="flex items-center gap-2">
                                    <input type="date" v-model="filters.dateFrom" class="input input-bordered input-sm"
                                        disabled />
                                    <span class="strip">-</span>
                                    <input type="date" v-model="filters.dateUntil" class="input input-bordered input-sm"
                                        disabled />
                                    <button class="btn btn-sm btn-accent" disabled>{{$t('Apply')}}</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="bg-base-300 summary-card w-full rounded-lg justify-between h-full">
                    <div class="flex px-5 pt-10">
                        <v-chart v-if="chartOption" :option="chartOption" style="height: 500px ; width: 100%"></v-chart>
                        <div v-else>
                            <pre>{{ chartOption }}</pre>
                        </div>
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
        LineChart
    } from 'echarts/charts'
    import {
        GridComponent,
        TooltipComponent,
        LegendComponent,
        GraphicComponent
    } from 'echarts/components'
    import * as echart from 'echarts/core'
    import * as XLSX from 'xlsx'
    import type {
        EChartsOption
    } from 'echarts';

    echart.use([
        CanvasRenderer,
        LineChart,
        TooltipComponent,
        LegendComponent,
        GraphicComponent,
        GridComponent,
    ])

    export default {
        data(): Record < string,
        any > {
            const xAxisLabels: string[] = []
            const series: any[] = []
            return {
                selected: 'Group A'  as string | null,
                selectedLine: '3F13',
                selectedstation: 'R1',
                showDropdown: false,
                selectedShift: '',
                filters: {
                    floor: '4F',
                    dateFrom: '',
                    dateUntil: '',
                    line: '4A'
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
                floor: ['1F', '2F', '3F', '4F', '5F'],
                f: '4F',
                lines: ['4A', '4B', '4C', '4D', '4E'],
                stations: ['R1', 'R2', 'R3', 'R4', 'R5', 'R6'],
                today: new Date().toLocaleDateString(this.$i18n.locale, {
                    year: 'numeric',
                    month: 'long',
                    day: 'numeric'
                }),
                day: new Date().toLocaleDateString('en-US', {
                    weekday: 'long'
                }),
                countdownInterval: null,
                time: {
                    hours: '00',
                    minutes: '00',
                    seconds: '00'
                },
                years: ['2024', '2023', '2025', '2026'],
                year: '2025',
                month: '7',
                formats: ['.xlsx', '.xls'],
                format: '.xlsx',
                xAxisLabels,
                series,
                chartOption: null as EChartsOption | null
            }
        },

        mounted() {
            this.updateTime()
            this.countdownInterval = setInterval(this.updateTime, 1000)
        },

        beforeMount() {
            clearInterval(this.countdownInterval)
        },

        methods: {
            updateTime() {
                const now = new Date()
                const pad = (val: number) => val.toString().padStart(2, '0')
                this.time.hours = pad(now.getHours())
                this.time.minutes = pad(now.getMinutes())
                this.time.seconds = pad(now.getSeconds())
            },
            getDatesFromMonth(year: number, month: number, locale: string): string[] {
                const result: string[] = []
                const date = new Date(year, month - 1, 1)
                const formatter = new Intl.DateTimeFormat(locale, {
                    month: '2-digit',
                    day: '2-digit'
                })

                while (date.getMonth() === month - 1) {
                    result.push(formatter.format(date))
                    date.setDate(date.getDate() + 1)
                }
                return result
            },
            parseExcelDate(raw: any): string {
                if (typeof raw === 'string') {
                    return raw;
                } else if (typeof raw === 'number') {
                    const parsed = XLSX.SSF.parse_date_code(raw);
                    if (parsed) {
                        const m = parsed.m.toString().padStart(2, '0');
                        const d = parsed.d.toString().padStart(2, '0');
                        return `${m}/${d}`;
                    }
                }
                return '';
            },

            parseFormatA(rows: any[][], sheetName: string) {
                const xAxisLabels: string[] = [];
                const oeeValues: number[] = [];

                rows.forEach(row => {
                    if (row.length >= 2) {
                        const dateLabel = this.parseExcelDate(row[0]);
                        xAxisLabels.push(dateLabel);

                        let val = 0;
                        if (typeof row[1] === 'string') {
                            val = parseFloat(row[1].replace('%', '').trim()) / 100;
                        } else if (typeof row[1] === 'number') {
                            val = row[1];
                        }
                        oeeValues.push(val);
                    }
                });

                return {
                    xAxisLabels,
                    series: [{
                        name: sheetName,
                        type: 'line',
                        data: oeeValues,
                    }],
                };
            },

            parseFormatB(rows: any[][]) {
                const groupedData: Record < string, number[] > = {};
                const xAxisSet = new Set < string > ();
                const xAxisLabels: string[] = [];

                rows.forEach((row) => {
                    const dateRaw = row[3];
                    const line = row[8] ?.toString().trim() || 'Unknown Line';
                    const floor = row[7] ?.toString().trim() || 'Unknown Floor';
                    const oee = typeof row[10] === 'string' ?
                        parseFloat(row[10].replace('%', '').trim()) / 100 :
                        Number(row[10]) || 0;

                    let dataLabel = '';
                    if (typeof dateRaw === 'string') {
                        const date = new Date(dateRaw);
                        const m = (date.getMonth() + 1).toString().padStart(2, '0');
                        const d = date.getDate().toString().padStart(2, '0');
                        dataLabel = `${m}/${d}`;
                    } else if (typeof dateRaw === 'number') {
                        const parsed = XLSX.SSF.parse_date_code(dateRaw);
                        if (parsed) {
                            const m = parsed.m.toString().padStart(2, '0');
                            const d = parsed.d.toString().padStart(2, '0');
                            dataLabel = `${m}/${d}`;
                        }
                    }

                    if (dataLabel && !xAxisSet.has(dataLabel)) {
                        xAxisSet.add(dataLabel);
                        xAxisLabels.push(dataLabel);
                    }

                    const key = `${line} - ${floor}`;
                    if (!groupedData[key]) {
                        groupedData[key] = [];
                    }

                    groupedData[key].push(oee);
                });

                const series = Object.entries(groupedData).map(([key, data]) => ({
                    name: key,
                    type: 'line',
                    data,
                }));

                return {
                    xAxisLabels,
                    series,
                };
            },

            handleFile(event: Event | {
                dataTransfer: DataTransfer
            }) {
                let files: FileList | null = null;

                if ('dataTransfer' in event && event.dataTransfer) {
                    files = event.dataTransfer.files;
                } else if (
                    'target' in event &&
                    event.target instanceof HTMLInputElement &&
                    event.target.files
                ) {
                    files = event.target.files;
                }

                if (!files || !files.length) return;

                const file = files[0];
                console.log('File selected via input or drop:', file.name);

                this.readExcelFile(file);
            },
            onDragOver(_event: DragEvent) {
                console.log('Dragging file over');
            },

            onDrop(event: DragEvent) {
                console.log('File dropped:', event.dataTransfer ?.files ?. [0] ?.name);
                this.handleFile(event);
            },
            readExcelFile(file: File) {
                const reader = new FileReader();
                reader.onload = (e) => {
                    const data = new Uint8Array(e.target ?.result as ArrayBuffer);
                    const workbook = XLSX.read(data, {
                        type: 'array'
                    });
                    const sheetNames = workbook.SheetNames;

                    let xAxisLabels: string[] = [];
                    let allSeries: any[] = [];

                    sheetNames.forEach(sheetName => {
                        const worksheet = workbook.Sheets[sheetName];
                        const jsonData: any[][] = XLSX.utils.sheet_to_json(worksheet, {
                            header: 1
                        });

                        if (jsonData.length < 2) return;

                        const headerRow = jsonData[0].map((h: any) => h.toString().trim());
                        const dataRows = jsonData.slice(1);

                        if (headerRow.length === 2 && headerRow[0] === '日' && headerRow[1] === 'OEE') {
                            const parsed = this.parseFormatA(dataRows, sheetName);

                            if (xAxisLabels.length === 0) xAxisLabels = parsed.xAxisLabels;

                            allSeries = allSeries.concat(parsed.series);

                        } else if (
                            headerRow.length >= 11 &&
                            headerRow[0] === '年' &&
                            headerRow[3] === '日' &&
                            headerRow[10] === 'OEE'
                        ) {
                            const parsedB = this.parseFormatB(dataRows);
                            if (xAxisLabels.length === 0) xAxisLabels = parsedB.xAxisLabels;
                            allSeries = allSeries.concat(parsedB.series);
                        }
                    });

                    if (xAxisLabels.length === 0 || allSeries.length === 0) {
                        alert('Invalid format to show, please use the format template');
                        return;
                    }

                    this.chartOption = {
                        title: {
                            text: 'OEE 趨勢',
                            left: 'center'
                        },
                        tooltip: {
                            trigger: 'axis'
                        },
                        legend: {
                            top: 30,
                            padding: [5, 0, 5, 0],
                            itemGap: 20,
                            textStyle: {
                                fontSize: 10
                            },
                            selected: allSeries.reduce((acc, s, i) => {
                                    acc[s.name] = i === 0
                                    return acc
                                }, {} as Record < string, boolean > )
                        },
                        toolbox: {
                            right: 10,
                            feature: {
                                dataZoom: {
                                    yAxisIndex: 'none'
                                },
                                restore: {},
                                saveAsImage: {
                                    show: true,
                                    title: 'Save as Image',
                                    type: 'png',
                                    pixelRatio: 4,
                                    excludeComponents: ['dataZoom', 'toolbox'],
                                    name: 'oee-trend',
                                    color: '#fff',
                                    backgroundColor: '#fff',
                                    renderAsImage: true
                                }
                            }
                        },
                        grid: {
                            top: 120,
                            left: 60,
                            right: 40,
                            bottom: 100
                        },
                        xAxis: {
                            type: 'category',
                            data: xAxisLabels,
                            axisLabel: {
                                show: true,
                                interval: 0,
                                rotate: 40,
                                boundaryGap: true,
                                hideOverlap: false,
                                fontSize: 10,
                                color: '#000',
                                textShadowColor: 'rgba(255, 255, 255, 0.7)',
                                textShadowBlur: 10,
                                formatter(value: string) {
                                    if (typeof value !== 'string') return value;
                                    const [m, d] = value.split('/');
                                    return `${m?.padStart(2, '0')}/${d?.padStart(2, '0')}`;
                                }
                            },
                            splitLine: {
                                lineStyle: {
                                    color: '#454545'
                                }
                            }
                        },
                        yAxis: {
                            type: 'value',
                            name: 'OEE (%)',
                            max: 1,
                            axisLabel: {
                                color: '#FF1700',
                                fontSize: 12,
                                textShadowColor: 'rgba(255, 255, 255, 0.7)',
                                textShadowBlur: 10,
                                formatter: (val: number) => `${(val * 100).toFixed(2)}%`,
                            },
                            splitLine: {
                                lineStyle: {
                                    color: '#454545'
                                }
                            }
                        },
                        dataZoom: [{
                                type: 'slider',
                                show: true,
                                start: 0,
                                end: 100,
                                backgroundColor: 'transparent',
                                dataBackground: {
                                    areaStyle: {
                                        color: 'rgba(255, 255, 255, 0.7)'
                                    },
                                    lineStyle: {
                                        color: 'rgba(255, 255, 255, 0.7)'
                                    }
                                },
                                fillerColor: 'rgba(255, 255, 255, 0.7)',
                                borderColor: 'rgba(255,255,255,0.7)',
                                handleStyle: {
                                    color: 'rgba(255,255,255,0.7)',
                                    borderColor: '#fff',
                                    shadowBlur: 10,
                                    shadowColor: 'rgba(255,255,255,0.7)'
                                },
                                color: '#FFFFFF',
                                textShadowColor: 'rgba(255, 255, 255, 0.7)',
                                textShadowBlur: 10
                            },
                            {
                                start: 0,
                                end: 100
                            }
                        ],
                        series: allSeries.map(s => ({
                            ...s,
                            smooth: false,
                            symbol: 'circle',
                            symbolSize: 6,
                            color: '#0065F8',
                            label: {
                                show: true,
                                position: 'top',
                                formatter: (params: any) =>
                                    `${(Number(params.value)* 100).toFixed(2)}%`,
                                fontSize: 11,
                                color: '#FF9A00',
                                textShadowColor: 'rgba(255, 255, 255, 0.7)',
                                textShadowBlur: 12
                            },
                            lineStyle: {
                                width: 3,
                                type: 'solid'
                            }
                        })),
                    };
                };
                reader.readAsArrayBuffer(file);
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

    .file-upload-form {
        width: fit-content;
        height: fit-content;
        display: flex;
        align-items: center;
        justify-content: center;
    }

    .file-upload-label input {
        display: none;
    }

    .file-upload-label svg {
        height: 50px;
        fill: #00d3bb;
        margin-bottom: 20px;
    }

    .file-upload-label {
        cursor: pointer;
        padding: 30px 70px;
        border-radius: 40px;
        border: 2px dashed #00d3bb;
        box-shadow: 0px 0px 200px -50px rgba(0, 0, 0, 0.5);
        color: #00d3bb;
    }

    .file-upload-design {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        gap: 5px;
    }

    .browse-button {
        background-color: #00d3bb;
        padding: 5px 15px;
        border-radius: 10px;
        color: #eee;
        transition: all 0.3s;
    }

    .browse-button:hover {
        background-color: #15191e;
        color: #fff;
    }

    html[data-theme='light'] .bg-base-300 {
      background-color: #15191e;
    }

     html[data-theme='light'] .font-semibold {
    color: #eee;
  }

  html[data-theme='light'] .font-bold {
    color: #eee;
  }

   html[data-theme='light'] .font-mono {
    color: #eee;
  }

  html[data-theme='light'] .floating-label {
    color: #15191e;
  }

  html[data-theme='light'] .strip{
      color: #eee;
    }

    html[data-theme='light'] .select{
      background-color: #eee;
      color: #15191e;
    }

    html[data-theme='light'] .input{
      background-color: #eee;
    }


    html[data-theme='light'] .file-upload-label{
        background-color: #191e24;
    }

    html[data-theme='light'] .txt{
        color: #eee;
    }
</style>