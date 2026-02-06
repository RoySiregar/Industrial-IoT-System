<template>
    <div class="fatp flex flex-col space-y-4">
        <transition name="fade">
            <div v-if="showAlert" role="alert"
                class="absolute z-10 bottom-20 right-4 alert alert-warning alert-soft transition duration-700 ease-in-out">
                <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 shrink-0 stroke-current " fill="none"
                    viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                        d="M12 9v2m0 4h.01m-6.938 4h13.856c1.54 0 2.502-1.667 1.732-3L13.732 4c-.77-1.333-2.694-1.333-3.464 0L3.34 16c-.77 1.333.192 3 1.732 3z" />
                </svg>
                <span>Sample data – actual data not loaded yet.</span>
            </div>
        </transition>
        <div class="w-full bg-base-300 p-4 rounded-lg shadow mb-2 summary-card">
            <div class="flex flex-wrap items-center justify-between gap-4">
                <div class="flex gap-4">
                    <div class="flex flex-wrap items-center gap-2">
                        <div v-if="activeFilters.includes('floor')">
                            <select v-model="filters.floor" class="select select-bordered select-sm">
                                <option value="">{{$t('Choose Floor')}}</option>
                                <option v-for="f in floor" :key="f">{{ f }}</option>
                            </select>
                        </div>

                        <div v-if="activeFilters.includes('line')">
                            <select v-model="filters.line" class="select select-bordered select-sm">
                                <option value="">{{$t('Choose Line')}}</option>
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
        <div class="bg-base-300 p-4 rounded-lg shadow summary-card overflow-hidden">
            <div class="flex gap-4 justify-between">
                <div class="tabs tabs-box w-fit">
                    <input type="radio" name="my_tabs_1" class="tab" aria-label="All" checked v-model="selectedStatus"
                        value="all" />
                    <input type="radio" name="my_tabs_1" class="tab" aria-label="Pass" checked v-model="selectedStatus"
                        value="pass" />
                    <input type="radio" name="my_tabs_1" class="tab" aria-label="Fail" v-model="selectedStatus"
                        value="fail" />
                </div>
                <div class="tabs tabs-box w-fit ">
                    <input type="radio" name="my_tabs_2" class="tab" aria-label="Data" checked v-model="selectedTab"
                        value="data" />
                    <input type="radio" name="my_tabs_2" class="tab" aria-label="Chart" v-model="selectedTab"
                        value="chart" />
                </div>
                <label class="input place-self-end" v-if="selectedTab === 'data'">
                    <svg class="h-[1em] opacity-50" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
                        <g stroke-linejoin="round" stroke-linecap="round" stroke-width="2.5" fill="none"
                            stroke="currentColor">
                            <circle cx="11" cy="11" r="8"></circle>
                            <path d="m21 21-4.3-4.3"></path>
                        </g>
                    </svg>
                    <input type="search" class="grow" placeholder="Search" v-model="state.search" />
                    <kbd class="kbd kbd-sm">⌘</kbd>
                    <kbd class="kbd kbd-sm">K</kbd>
                </label>
            </div>
            <div class="overflow-x-auto mt-4 flex" v-if="selectedTab === 'data'">
                <table class="table" v-if="selectedStatus === 'pass' || selectedStatus === 'all'">
                    <!-- head -->
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>Section</th>
                            <th>Task</th>
                            <th>Description</th>
                            <th>Spec</th>
                            <th>LSL</th>
                            <th>USL</th>
                            <th>UOM</th>
                            <th>Value</th>  
                            <th>Pass/Fail</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th>1</th>
                            <td>Assembly</td>
                            <td>Screw Torque</td>
                            <td>Check torque of housing screws</td>
                            <td>0.45 ± 0.05</td>
                            <td>0.40</td>
                            <td>0.50</td>
                            <td>N·m</td>
                            <td>0.46</td>
                            <td>Pass</td>
                        </tr>
                        <tr>
                            <th>2</th>
                            <td>Assembly</td>
                            <td>Connector Fit</td>
                            <td>Ensure USB-C connector alignment</td>
                            <td>Center ± 0.10</td>
                            <td>-0.10</td>
                            <td>0.10</td>
                            <td>mm</td>
                            <td>0.05</td>
                            <td>Pass</td>
                        </tr>
                        <tr>
                            <th>3</th>
                            <td>Functional Test</td>
                            <td>Power On Test</td>
                            <td>Verify device boots correctly</td>
                            <td>Boot in ≤ 5s</td>
                            <td>0</td>
                            <td>5</td>
                            <td>s</td>
                            <td>3.8</td>
                            <td>Pass</td>
                        </tr>
                        <tr>
                            <th>4</th>
                            <td>Functional Test</td>
                            <td>Display Brightness</td>
                            <td>Measure LCD brightness at full power</td>
                            <td>400 ± 30</td>
                            <td>370</td>
                            <td>430</td>
                            <td>nits</td>
                            <td>389</td>
                            <td>Pass</td>
                        </tr>
                        <tr>
                            <th>5</th>
                            <td>Functional Test</td>
                            <td>Speaker Output</td>
                            <td>Check speaker SPL at 1kHz</td>
                            <td>80 ± 5</td>
                            <td>75</td>
                            <td>85</td>
                            <td>dB</td>
                            <td>82</td>
                            <td>Pass</td>
                        </tr>
                        <tr>
                            <th>6</th>
                            <td>Functional Test</td>
                            <td>Microphone Sensitivity</td>
                            <td>Check mic response to test tone</td>
                            <td>-42 ± 3</td>
                            <td>-45</td>
                            <td>-39</td>
                            <td>dBV</td>
                            <td>-43</td>
                            <td>Pass</td>
                        </tr>
                        <tr>
                            <th>7</th>
                            <td>Connectivity</td>
                            <td>Wi-Fi Signal Strength</td>
                            <td>Measure RSSI at 1m distance</td>
                            <td>-45 ± 5</td>
                            <td>-50</td>
                            <td>-40</td>
                            <td>dBm</td>
                            <td>-47</td>
                            <td>Pass</td>
                        </tr>
                        <tr>
                            <th>8</th>
                            <td>Connectivity</td>
                            <td>Bluetooth Pairing</td>
                            <td>Check pairing time with headset</td>
                            <td>≤ 10</td>
                            <td>0</td>
                            <td>10</td>
                            <td>s</td>
                            <td>8.2</td>
                            <td>Pass</td>
                        </tr>
                        <tr>
                            <th>9</th>
                            <td>Battery Test</td>
                            <td>Charge Voltage</td>
                            <td>Verify battery charging voltage</td>
                            <td>4.20 ± 0.05</td>
                            <td>4.15</td>
                            <td>4.25</td>
                            <td>V</td>
                            <td>4.18</td>
                            <td>Pass</td>
                        </tr>
                        <tr>
                            <th>10</th>
                            <td>Battery Test</td>
                            <td>Discharge Capacity</td>
                            <td>Measure battery mAh output</td>
                            <td>3000 ± 150</td>
                            <td>2850</td>
                            <td>3150</td>
                            <td>mAh</td>
                            <td>2920</td>
                            <td>Pass</td>
                        </tr>
                        <tr>
                            <th>11</th>
                            <td>Camera Test</td>
                            <td>Image Clarity</td>
                            <td>Test camera focus sharpness</td>
                            <td>≥ 85</td>
                            <td>85</td>
                            <td>100</td>
                            <td>%</td>
                            <td>88</td>
                            <td>Pass</td>
                        </tr>
                        <tr>
                            <th>12</th>
                            <td>Camera Test</td>
                            <td>Color Accuracy</td>
                            <td>Check ΔE value vs reference chart</td>
                            <td>≤ 5</td>
                            <td>0</td>
                            <td>5</td>
                            <td>ΔE</td>
                            <td>4.2</td>
                            <td>Pass</td>
                        </tr>
                        <tr>
                            <th>13</th>
                            <td>Final Inspection</td>
                            <td>Label Alignment</td>
                            <td>Check product label position</td>
                            <td>Center ± 0.5</td>
                            <td>-0.5</td>
                            <td>0.5</td>
                            <td>mm</td>
                            <td>0.3</td>
                            <td>Pass</td>
                        </tr>
                        <tr>
                            <th>14</th>
                            <td>Final Inspection</td>
                            <td>Packaging Weight</td>
                            <td>Weigh full boxed unit</td>
                            <td>550 ± 20</td>
                            <td>530</td>
                            <td>570</td>
                            <td>g</td>
                            <td>555</td>
                            <td>Pass</td>
                        </tr>
                        <tr>
                            <th>15</th>
                            <td>Final Inspection</td>
                            <td>Drop Test</td>
                            <td>Ensure device passes 1m drop test</td>
                            <td>Pass</td>
                            <td>—</td>
                            <td>—</td>
                            <td>—</td>
                            <td>Pass</td>
                            <td>Pass</td>
                        </tr>
                    </tbody>
                </table>
                <table class="table" v-else>
                    <!-- head -->
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>Section</th>
                            <th>Task</th>
                            <th>Description</th>
                            <th>Spec</th>
                            <th>LSL</th>
                            <th>USL</th>
                            <th>UOM</th>
                            <th>Value</th>
                            <th>Pass/Fail</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th>1</th>
                            <td>Assembly</td>
                            <td>Screw Torque</td>
                            <td>Check torque of housing screws</td>
                            <td>0.45 ± 0.05</td>
                            <td>0.40</td>
                            <td>0.50</td>
                            <td>N·m</td>
                            <td>0.38</td>
                            <td>Fail</td>
                        </tr>
                        <tr>
                            <th>2</th>
                            <td>Assembly</td>
                            <td>Connector Fit</td>
                            <td>Ensure USB-C connector alignment</td>
                            <td>Center ± 0.10</td>
                            <td>-0.10</td>
                            <td>0.10</td>
                            <td>mm</td>
                            <td>0.18</td>
                            <td>Fail</td>
                        </tr>
                        <tr>
                            <th>3</th>
                            <td>Functional Test</td>
                            <td>Power On Test</td>
                            <td>Verify device boots correctly</td>
                            <td>Boot in ≤ 5s</td>
                            <td>0</td>
                            <td>5</td>
                            <td>s</td>
                            <td>6.7</td>
                            <td>Fail</td>
                        </tr>
                        <tr>
                            <th>4</th>
                            <td>Functional Test</td>
                            <td>Speaker Output</td>
                            <td>Check speaker SPL at 1kHz</td>
                            <td>80 ± 5</td>
                            <td>75</td>
                            <td>85</td>
                            <td>dB</td>
                            <td>73</td>
                            <td>Fail</td>
                        </tr>
                        <tr>
                            <th>5</th>
                            <td>Battery Test</td>
                            <td>Charge Voltage</td>
                            <td>Verify battery charging voltage</td>
                            <td>4.20 ± 0.05</td>
                            <td>4.15</td>
                            <td>4.25</td>
                            <td>V</td>
                            <td>4.28</td>
                            <td>Fail</td>
                        </tr>
                        <tr>
                            <th>6</th>
                            <td>Battery Test</td>
                            <td>Discharge Capacity</td>
                            <td>Measure battery mAh output</td>
                            <td>3000 ± 150</td>
                            <td>2850</td>
                            <td>3150</td>
                            <td>mAh</td>
                            <td>2740</td>
                            <td>Fail</td>
                        </tr>
                        <tr>
                            <th>7</th>
                            <td>Camera Test</td>
                            <td>Color Accuracy</td>
                            <td>Check ΔE value vs reference chart</td>
                            <td>≤ 5</td>
                            <td>0</td>
                            <td>5</td>
                            <td>ΔE</td>
                            <td>7.1</td>
                            <td>Fail</td>
                        </tr>
                        <tr>
                            <th>8</th>
                            <td>Connectivity</td>
                            <td>Wi-Fi Signal Strength</td>
                            <td>Measure RSSI at 1m distance</td>
                            <td>-45 ± 5</td>
                            <td>-50</td>
                            <td>-40</td>
                            <td>dBm</td>
                            <td>-53</td>
                            <td>Fail</td>
                        </tr>
                        <tr>
                            <th>9</th>
                            <td>Final Inspection</td>
                            <td>Label Alignment</td>
                            <td>Check product label position</td>
                            <td>Center ± 0.5</td>
                            <td>-0.5</td>
                            <td>0.5</td>
                            <td>mm</td>
                            <td>0.8</td>
                            <td>Fail</td>
                        </tr>
                        <tr>
                            <th>10</th>
                            <td>Drop Test</td>
                            <td>Mechanical Shock</td>
                            <td>Device fails 1m drop surface integrity test</td>
                            <td>Pass</td>
                            <td>—</td>
                            <td>—</td>
                            <td>—</td>
                            <td>Cracked Housing</td>
                            <td>Fail</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="flex justify-center items-center" v-if="selectedTab === 'data'">
                <div class="pagination flex gap-2">
                    <div class="tab-group">
                        <input type="radio" id="tab3" name="tabComponent" checked>
                        <label for="tab3">1</label>
                    </div>
                    <div class="tab-group">
                        <input type="radio" id="tab4" name="tabComponent">
                        <label for="tab4">2</label>
                    </div>
                    <div class="tab-group">
                        <input type="radio" id="tabAll" name="tabComponent">
                        <label for="tabAll">All</label>
                    </div>
                </div>

            </div>

            <v-chart v-else :option="chartOptions" autoresize style="width: 100%; height: 400px;"></v-chart>
        </div>
    </div>
</template>
<script lang="ts">
interface tableDataArray{
    no: number,
    section : string,
    task : string,
    description : string,
    spec : string,
    LSL : string,
    USL : string,
    UOM : string,
    Value : string,
    Status : string
}
    export default {
        data() {
            return {
                selectedLine: '3F13',
                selectedstation: 'R1',
                selectedShift: '',
                selectedStatus: 'pass',
                selectedTab: 'data',
                filters: {
                    floor: '4F',
                    dateFrom: '',
                    dateUntil: '',
                    line: '3F13'
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
                lines: ['2F13', '2F16', '2F17', '2F19', '3F13', '3F14', '3F15', '3F17'],
                stations: ['R1', 'R2', 'R3', 'R4', 'R5', 'R6'],
                chartOptions: {
                    title: null,
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'shadow'
                        }
                    },
                    grid: {
                        top: 80,
                        bottom: 30
                    },
                    xAxis: {
                        type: 'value',
                        position: 'top',
                        splitLine: {
                            lineStyle: {
                                type: 'dashed'
                            }
                        }
                    },
                    yAxis: {
                        type: 'category',
                        axisLine: {
                            show: false
                        },
                        axisLabel: {
                            show: false
                        },
                        axisTick: {
                            show: false
                        },
                        splitLine: {
                            show: false
                        },
                        data: [
                            'ten',
                            'nine',
                            'eight',
                            'seven',
                            'six',
                            'five',
                            'four',
                            'three',
                            'two',
                            'one'
                        ]
                    },
                    series: [{
                        name: 'Cost',
                        type: 'bar',
                        stack: 'Total',
                        label: {
                            show: true,
                            formatter: '{b}'
                        },
                        itemStyle: {
                            color: '#00d3bb',
                            shadowColor: 'rgba(255, 255, 255, 0.7)',
                            shadowBlur: 10
                        },
                        data: [{
                                value: -0.07
                            },
                            {
                                value: -0.09
                            },
                            0.2,
                            0.44,
                            {
                                value: -0.23
                            },
                            0.08,
                            {
                                value: -0.17
                            },
                            0.47,
                            {
                                value: -0.36
                            },
                            0.18
                        ]
                    }]
                },
                array: [{
                        no: 1,
                        section: 'Assembly',
                        task: 'Screw Torque',
                        description: 'Check torque of housing screws',
                        spec: '0.45 ± 0.05',
                        LSL: '0.40',
                        USL: '0.50',
                        UOM: 'N·m',
                        Value: '0.38',
                        Status: 'Fail'
                    },
                    {
                        no: 2,
                        section: 'Assembly',
                        task: 'Connector Fit',
                        description: 'Ensure USB-C connector alignment',
                        spec: 'Center ± 0.10',
                        LSL: '-0.10',
                        USL: '0.10',
                        UOM: 'mm',
                        Value: '0.18',
                        Status: 'Fail'
                    },
                    {
                        no: 3,
                        section: 'Functional Test',
                        task: 'Power On Test',
                        description: 'Verify device boots correctly',
                        spec: 'Boot in ≤ 5s',
                        LSL: '0',
                        USL: '5',
                        UOM: 's',
                        Value: '6.7',
                        Status: 'Fail'
                    },
                    {
                        no: 4,
                        section: 'Functional Test',
                        task: 'Speaker Output',
                        description: 'Check speaker SPL at 1kHz',
                        spec: '80 ± 5',
                        LSL: '75',
                        USL: '85',
                        UOM: 'dB',
                        Value: '73',
                        Status: 'Fail'
                    },
                    {
                        no: 5,
                        section: 'Battery Test',
                        task: 'Charge Voltage',
                        description: 'Verify battery charging voltage',
                        spec: '4.20 ± 0.05',
                        LSL: '4.15',
                        USL: '4.25',
                        UOM: 'V',
                        Value: '4.28',
                        Status: 'Fail'
                    },
                    {
                        no: 6,
                        section: 'Battery Test',
                        task: 'Discharge Capacity',
                        description: 'Measure battery mAh output',
                        spec: '3000 ± 150',
                        LSL: '2850',
                        USL: '3150',
                        UOM: 'mAh',
                        Value: '2740',
                        Status: 'Fail'
                    },
                    {
                        no: 7,
                        section: 'Camera Test',
                        task: 'Color Accuracy',
                        description: 'Check ΔE value vs reference chart',
                        spec: '≤ 5',
                        LSL: '0',
                        USL: '5',
                        UOM: 'ΔE',
                        Value: '7.1',
                        Status: 'Fail'
                    },
                    {
                        no: 8,
                        section: 'Connectivity',
                        task: 'Wi-Fi Signal Strength',
                        description: 'Measure RSSI at 1m distance',
                        spec: '-45 ± 5',
                        LSL: '-50',
                        USL: '-40',
                        UOM: 'dBm',
                        Value: '-53',
                        Status: 'Fail'
                    }, {
                        no: 9,
                        section: 'Final Inspection',
                        task: 'Label Alignment',
                        description: 'Check product label position',
                        spec: 'Center ± 0.5',
                        LSL: '-0.5',
                        USL: '0.5',
                        UOM: 'mm',
                        Value: '0.8',
                        Status: 'Fail'
                    }, {
                        no: 10,
                        section: 'Drop Test',
                        task: 'Mechanical Shock',
                        description: 'Device fails 1m drop surface integrity test',
                        spec: 'Pass',
                        LSL: '—',
                        USL: '—',
                        UOM: '—',
                        Value: 'Cracked Housing',
                        Status: 'Fail'
                    },
                    {
                        no: 11,
                        section: 'Assembly',
                        task: 'Screw Torque',
                        description: 'Check torque of housing screws',
                        spec: '0.45 ± 0.05',
                        LSL: '0.40',
                        USL: '0.50',
                        UOM: 'N·m',
                        Value: '0.46',
                        Status: 'Pass'
                    },
                    {
                        no: 12,
                        section: 'Assembly',
                        task: 'Connector Fit',
                        description: 'Ensure USB-C connector alignment',
                        spec: 'Center ± 0.10',
                        LSL: '-0.10',
                        USL: '0.10',
                        UOM: 'mm',
                        Value: '0.05',
                        Status: 'Pass'
                    },
                    {
                        no: 13,
                        section: 'Functional Test',
                        task: 'Power On Test',
                        description: 'Verify device boots correctly',
                        spec: 'Boot in ≤ 5s',
                        LSL: '0',
                        USL: '5',
                        UOM: 's',
                        Value: '3.8',
                        Status: 'Pass'
                    },
                    {
                        no: 14,
                        section: 'Functional Test',
                        task: 'Display Brightness',
                        description: 'Measure LCD brightness at full power',
                        spec: '400 ± 30',
                        LSL: '370',
                        USL: '430',
                        UOM: 'nits',
                        Value: '389',
                        Status: 'Pass'
                    },
                    {
                        no: 15,
                        section: 'Functional Test',
                        task: 'Speaker Output',
                        description: 'Check speaker SPL at 1kHz',
                        spec: '80 ± 5',
                        LSL: '75',
                        USL: '85',
                        UOM: 'dB',
                        Value: '82',
                        Status: 'Pass'
                    },
                    {
                        no: 16,
                        section: 'Functional Test',
                        task: 'Microphone Sensitivity',
                        description: 'Check mic response to test tone',
                        spec: '-42 ± 3',
                        LSL: '-45',
                        USL: '-39',
                        UOM: 'dBV',
                        Value: '-43',
                        Status: 'Pass'
                    },
                    {
                        no: 17,
                        section: 'Connectivity',
                        task: 'Wi-Fi Signal Strength',
                        description: 'Measure RSSI at 1m distance',
                        spec: '-45 ± 5',
                        LSL: '-50',
                        USL: '-40',
                        UOM: 'dBm',
                        Value: '-47',
                        Status: 'Pass'
                    },
                    {
                        no: 18,
                        section: 'Connectivity',
                        task: 'Bluetooth Pairing',
                        description: 'Check pairing time with headset',
                        spec: '≤ 10',
                        LSL: '0',
                        USL: '10',
                        UOM: 's',
                        Value: '8.2',
                        Status: 'Pass'
                    },
                    {
                        no: 19,
                        section: 'Battery Test',
                        task: 'Charge Voltage',
                        description: 'Verify battery charging voltage',
                        spec: '4.20 ± 0.05',
                        LSL: '4.15',
                        USL: '4.25',
                        UOM: 'V',
                        Value: '4.18',
                        Status: 'Pass'
                    },
                    {
                        no: 20,
                        section: 'Battery Test',
                        task: 'Discharge Capacity',
                        description: 'Measure battery mAh output',
                        spec: '3000 ± 150',
                        LSL: '2850',
                        USL: '3150',
                        UOM: 'mAh',
                        Value: '2920',
                        Status: 'Pass'
                    },
                    {
                        no: 21,
                        section: 'Camera Test',
                        task: 'Image Clarity',
                        description: 'Test camera focus sharpness',
                        spec: '≥ 85',
                        LSL: '85',
                        USL: '100',
                        UOM: '%',
                        Value: '88',
                        Status: 'Pass'
                    },
                    {
                        no: 22,
                        section: 'Camera Test',
                        task: 'Color Accuracy',
                        description: 'Check ΔE value vs reference chart',
                        spec: '≤ 5',
                        LSL: '0',
                        USL: '5',
                        UOM: 'ΔE',
                        Value: '4.2',
                        Status: 'Pass'
                    },
                    {
                        no: 23,
                        section: 'Final Inspection',
                        task: 'Label Alignment',
                        description: 'Check product label position',
                        spec: 'Center ± 0.5',
                        LSL: '-0.5',
                        USL: '0.5',
                        UOM: 'mm',
                        Value: '0.3',
                        Status: 'Pass'
                    },
                    {
                        no: 24,
                        section: 'Final Inspection',
                        task: 'Packaging Weight',
                        description: 'Weigh full boxed unit',
                        spec: '550 ± 20',
                        LSL: '530',
                        USL: '570',
                        UOM: 'g',
                        Value: '555',
                        Status: 'Pass'
                    },
                    {
                        no: 25,
                        section: 'Final Inspection',
                        task: 'Drop Test',
                        description: 'Ensure device passes 1m drop test',
                        spec: 'Pass',
                        LSL: '—',
                        USL: '—',
                        UOM: '—',
                        Value: 'Pass',
                        Status: 'Pass'
                    }
                ] as tableDataArray[],
                state: {
                    search: ''
                },
                showAlert: false
            }
        },
        mounted() {
            this.showAlert = true
            setTimeout(() => {
                this.showAlert = false
            }, 10000)
        }
    }
</script>
<style scoped>
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
        width: 48px;
        height: 48px;
        line-height: 1.6;
        border: 1px solid #cccccc;
        border-radius: 12px;
        cursor: pointer;
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
        width: 48px;
        height: 48px;
        border-radius: 12px;
        background-color: #00d3bb;
        transform: translate(-50%, -50%);
        z-index: -1;
        opacity: 0;
        scale: 0;
        transform-origin: 0 0;
        transition: all 0.48s cubic-bezier(0.23, 1, 0.32, 1);
    }

    .tab-group label:hover {
        border-color: #00d3bb;
        color: #00d3bb;
    }

    .tab-group input:checked+label {
        border-color: transparent;
        color: #ffffff;
        scale: 1.1;
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

    html[data-theme='light'] .strip {
        color: #eee;
    }

    .fade-enter-active,
    .fade-leave-active {
        transition: opacity 0.6s ease-in-out;
    }

    .fade-enter-from,
    .fade-leave-to {
        opacity: 0;
    }
</style>