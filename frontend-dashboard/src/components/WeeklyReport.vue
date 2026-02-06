<template>
  <div class="weekly-report p-4 font-inter">
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
    <div class="w-full bg-base-300 p-4 mb-4 rounded-lg shadow summary-card">
      <div class="flex flex-wrap items-center justify-between gap-4">
        <div class="flex gap-4">
          <div class="flex flex-wrap items-center gap-2">
            <div v-if="activeFilters.includes('floor')">
              <select v-model="filters.floor" class="select select-bordered select-sm">
                <option disabled value="">{{$t('Choose Floor')}}</option>
                <option v-for="floor in floor" :key="floor">{{ floor }}</option>
              </select>
            </div>

            <div v-if="activeFilters.includes('line')">
              <select v-model="filters.line" class="select select-bordered select-sm">
                <option disabled value="">{{$t('Choose Line')}}</option>
                <option v-for="line in lines" :key="line">{{ line }}</option>
              </select>
            </div>
            <div v-if="activeFilters.includes('station')">
              <select v-model="filters.station" class="select select-bordered select-sm">
                <option disabled value="">{{$t('Choose Station')}}</option>
                <option v-for="station in stations" :key="station">{{ station }}</option>
              </select>
            </div>
            <div v-if="activeFilters.includes('date')" class="flex items-center gap-2">
              <label class="input input-sm w-40">
                <span class="floating-label">{{$t('Select Year')}}</span>
                <select v-model="year" class="select select-sm select-ghost w-20">
                  <option :value="2025">2025</option>
                  <option :value="2024">2024</option>
                  <option :value="2023">2023</option>
                </select>
              </label>
              <label class="input input-sm w-48">
                <span class="floating-label">{{$t('Select Month')}}</span>
                <select v-model="month" class="select select-sm select-ghost w-40">
                  <option v-for="m in 12" :key="m" :value="m">
                    {{ year }}-{{ m.toString().padStart(2, '0') }}
                  </option>
                </select>
              </label>
              <label class="input input-sm w-60">
                <span class="floating-label">{{$t('Select Week')}}</span>
                <select v-model="weekInMonth" class="select select-sm select-ghost w-40">
                  <option v-for="w in 5" :key="w" :value="w">{{$t('No')}} {{ $t('Week') }} {{ w }}
                    {{ $t('Week zh') }}</option>
                </select>
              </label>
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

    <!-- Content -->
    <div class="grid grid-flow-col grid-rows-3 gap-6">
      <!-- Achieve Output  -->
      <div class="col-span-2 row-span-1 p-4 bg-base-300 rounded-lg shadow summary-card">
        <h2 class="mb-5 font-medium text-lg text-accent">Achieve Output</h2>
        <div class="tabs tabs-lift">
          <!-- Table -->
          <label class="tab">
            <input type="radio" name="tabs1" checked />
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-4 me-2">
              <path fill-rule="evenodd"
                d="M.99 5.24A2.25 2.25 0 0 1 3.25 3h13.5A2.25 2.25 0 0 1 19 5.25l.01 9.5A2.25 2.25 0 0 1 16.76 17H3.26A2.267 2.267 0 0 1 1 14.74l-.01-9.5Zm8.26 9.52v-.625a.75.75 0 0 0-.75-.75H3.25a.75.75 0 0 0-.75.75v.615c0 .414.336.75.75.75h5.373a.75.75 0 0 0 .627-.74Zm1.5 0a.75.75 0 0 0 .627.74h5.373a.75.75 0 0 0 .75-.75v-.615a.75.75 0 0 0-.75-.75H11.5a.75.75 0 0 0-.75.75v.625Zm6.75-3.63v-.625a.75.75 0 0 0-.75-.75H11.5a.75.75 0 0 0-.75.75v.625c0 .414.336.75.75.75h5.25a.75.75 0 0 0 .75-.75Zm-8.25 0v-.625a.75.75 0 0 0-.75-.75H3.25a.75.75 0 0 0-.75.75v.625c0 .414.336.75.75.75H8.5a.75.75 0 0 0 .75-.75ZM17.5 7.5v-.625a.75.75 0 0 0-.75-.75H11.5a.75.75 0 0 0-.75.75V7.5c0 .414.336.75.75.75h5.25a.75.75 0 0 0 .75-.75Zm-8.25 0v-.625a.75.75 0 0 0-.75-.75H3.25a.75.75 0 0 0-.75.75V7.5c0 .414.336.75.75.75H8.5a.75.75 0 0 0 .75-.75Z"
                clip-rule="evenodd" />
            </svg>
            Data
          </label>
          <div class="tab-content bg-base-100 border-base-300 p-4 ">
            <div class="overflow-x-auto">
              <table class="table">
                <thead>
                  <tr>
                    <th>{{ selectedMonthYear }}</th>
                    <th v-for="(slot, index) in weeklyDates" :key="index" v-html="slot" />
                    <th>Highest <br /> UPH</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(row, rowIndex) in computedData" :key="row.label"
                    :class="rowIndex % 2 === 0 ? 'bg-base-200' : ''">
                    <th>{{ row.label }}</th>
                    <td v-for="(val, colIndex) in row.values" :key="colIndex"
                      v-html="getCellContent(val, row.highest)" />
                    <td>{{ row.highest }}</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>

          <!-- Chart -->
          <label class="tab">
            <input type="radio" name="tabs1" />
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-4 me-2">
              <path
                d="M15.5 2A1.5 1.5 0 0 0 14 3.5v13a1.5 1.5 0 0 0 1.5 1.5h1a1.5 1.5 0 0 0 1.5-1.5v-13A1.5 1.5 0 0 0 16.5 2h-1ZM9.5 6A1.5 1.5 0 0 0 8 7.5v9A1.5 1.5 0 0 0 9.5 18h1a1.5 1.5 0 0 0 1.5-1.5v-9A1.5 1.5 0 0 0 10.5 6h-1ZM3.5 10A1.5 1.5 0 0 0 2 11.5v5A1.5 1.5 0 0 0 3.5 18h1A1.5 1.5 0 0 0 6 16.5v-5A1.5 1.5 0 0 0 4.5 10h-1Z" />
            </svg>
            Chart
          </label>
          <div class="tab-content bg-base-100 border-base-300 p-6">
            <v-chart :option="chartOptions" autoresize style="width: 100%; height: 400px;"></v-chart>
          </div>
          <!-- Eksport -->
          <label class="tab">
            <input type="radio" name="tabs1" />
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-4 me-2">
              <path
                d="M10.75 2.75a.75.75 0 0 0-1.5 0v8.614L6.295 8.235a.75.75 0 1 0-1.09 1.03l4.25 4.5a.75.75 0 0 0 1.09 0l4.25-4.5a.75.75 0 0 0-1.09-1.03l-2.955 3.129V2.75Z" />
              <path
                d="M3.5 12.75a.75.75 0 0 0-1.5 0v2.5A2.75 2.75 0 0 0 4.75 18h10.5A2.75 2.75 0 0 0 18 15.25v-2.5a.75.75 0 0 0-1.5 0v2.5c0 .69-.56 1.25-1.25 1.25H4.75c-.69 0-1.25-.56-1.25-1.25v-2.5Z" />
            </svg>
            Export
          </label>
          <div class="tab-content bg-base-100 border-base-300 p-6">
            <div class="flex w-full h-auto rounded-lg overflow-hidden">
              <!-- Sidebar Export Menu  -->
              <div class="w-48 border-r border-base-200 flex flex-col">
                <div class="text-sm font-semibold p-4 border-b">Export Options</div>
                <button @click="previewExport('pdf')" class="flex items-center gap-2 p-3 hover:bg-accent transition"
                  value="PDF">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="white" class="size-4">
                    <path
                      d="M2.5 3.5A1.5 1.5 0 0 1 4 2h4.879a1.5 1.5 0 0 1 1.06.44l3.122 3.12a1.5 1.5 0 0 1 .439 1.061V12.5A1.5 1.5 0 0 1 12 14H4a1.5 1.5 0 0 1-1.5-1.5v-9Z" />
                  </svg>
                  <span class="text-white">PDF</span></button>
                <button @click="previewExport('docx')" class="flex items-center gap-2 p-3 hover:bg-accent transition"
                  value="Word"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="white" class="size-4">
                    <path fill-rule="evenodd"
                      d="M4 2a1.5 1.5 0 0 0-1.5 1.5v9A1.5 1.5 0 0 0 4 14h8a1.5 1.5 0 0 0 1.5-1.5V6.621a1.5 1.5 0 0 0-.44-1.06L9.94 2.439A1.5 1.5 0 0 0 8.878 2H4Zm1 5.75A.75.75 0 0 1 5.75 7h4.5a.75.75 0 0 1 0 1.5h-4.5A.75.75 0 0 1 5 7.75Zm0 3a.75.75 0 0 1 .75-.75h4.5a.75.75 0 0 1 0 1.5h-4.5a.75.75 0 0 1-.75-.75Z"
                      clip-rule="evenodd" />
                  </svg>
                  <span class="text-white">Word</span></button>
                <button @click="previewExport('xlsx')" class="flex items-center gap-2 p-3 hover:bg-accent transition"
                  value="Excel"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="white" class="size-4">
                    <path fill-rule="evenodd"
                      d="M4 2a1.5 1.5 0 0 0-1.5 1.5v9A1.5 1.5 0 0 0 4 14h8a1.5 1.5 0 0 0 1.5-1.5V6.621a1.5 1.5 0 0 0-.44-1.06L9.94 2.439A1.5 1.5 0 0 0 8.878 2H4Zm6 5.75a.75.75 0 0 1 1.5 0v3.5a.75.75 0 0 1-1.5 0v-3.5Zm-2.75 1.5a.75.75 0 0 1 1.5 0v2a.75.75 0 0 1-1.5 0v-2Zm-2 .75a.75.75 0 0 0-.75.75v.5a.75.75 0 0 0 1.5 0v-.5a.75.75 0 0 0-.75-.75Z"
                      clip-rule="evenodd" />
                  </svg>
                  <span class="text-white">Excel</span></button>
                <button @click="previewExport('img')" class="flex items-center gap-2 p-3 hover:bg-accent transition"
                  value="Image"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="white" class="size-4">
                    <path fill-rule="evenodd"
                      d="M2 4a2 2 0 0 1 2-2h8a2 2 0 0 1 2 2v8a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V4Zm10.5 5.707a.5.5 0 0 0-.146-.353l-1-1a.5.5 0 0 0-.708 0L9.354 9.646a.5.5 0 0 1-.708 0L6.354 7.354a.5.5 0 0 0-.708 0l-2 2a.5.5 0 0 0-.146.353V12a.5.5 0 0 0 .5.5h8a.5.5 0 0 0 .5-.5V9.707ZM12 5a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"
                      clip-rule="evenodd" />
                  </svg>
                  <span class="text-white">Image</span></button>
              </div>

              <!-- Content Preview -->
              <div class="flex-1 p-6 space-y-4">
                <!-- Upload Gambar -->
                <div id="preview" class="p-4 rounded shadow">
                  <h2 class="text-md font-semibold mb-2">Export To (.{{selectedFormat}})</h2>
                  <!-- <div class="w-60 h-60 bg-gray-50 border flex items-center justify-center rounded">
                    <img src="" alt="Preview" class="object-contain max-h-full max-w-full" />
                  </div> -->
                  <div id="export-setting">
                    <input type="radio" id="page" name="export_option" class="radio radio-sm radio-accent" value="Page"
                      v-model="exportOption" />
                    <label for="page" class="text-sm ms-2">All Page</label><br>

                    <input type="radio" id="range" name="export_option" class="radio radio-sm radio-accent"
                      value="Range" v-model="exportOption" />
                    <label for="range" class="text-sm ms-2">Range</label><br>

                    <div v-if="exportOption === 'Range'" class="mt-4 space-y-2">
                      <label class="block text-sm">Start Date:</label>
                      <input type="date" class="input input-bordered input-sm w-full max-w-xs" />

                      <label class="block text-sm">End Date:</label>
                      <input type="date" class="input input-bordered input-sm w-full max-w-xs" />
                    </div>

                    <div class="mt-4">
                      <input type="checkbox" class="checkbox checkbox-sm checkbox-accent" />
                      <label for="datetime" class="text-sm ms-2">Include Date & Time</label>
                    </div>

                    <div v-if="formatOption === 'docx'" class="space-y-2">
                      <input type="checkbox" class="checkbox checkbox-sm checkbox-accent" v-model="otherOption" />
                      <label class="text-sm ms-2">Other Format</label>

                      <div v-if="otherOption" class="flex ms-6 gap-4">
                        <input type="radio" id="docx" name="other_option" class="radio radio-xs radio-accent"
                          value="docx" />
                        <label for="docx" class="text-xs">Word Document</label>

                        <input type="radio" id="doc" name="other_option" class="radio radio-xs radio-accent"
                          value="doc" />
                        <label for="doc" class="text-xs">Word 97-2003 Document</label>
                      </div>
                    </div>

                    <div v-if="formatOption === 'img'" class="space-y-2">
                      <input type="checkbox" class="checkbox checkbox-sm checkbox-accent" v-model="otherOption" />
                      <label class="text-sm ms-2">Other Format</label>

                      <div v-if="otherOption" class="flex ms-6 gap-4">
                        <input type="radio" id="png" name="other_option" class="radio radio-xs radio-accent"
                          value="png" />
                        <label for="png" class="text-xs">PNG</label>

                        <input type="radio" id="jpg" name="other_option" class="radio radio-xs radio-accent"
                          value="jpg" />
                        <label for="jpg" class="text-xs">JPG</label>

                        <input type="radio" id="SVG" name="other_option" class="radio radio-xs radio-accent"
                          value="SVG" />
                        <label for="SVG" class="text-xs">SVG</label>

                        <input type="radio" id="JPEG" name="other_option" class="radio radio-xs radio-accent"
                          value="JPEG" />
                        <label for="JPEG" class="text-xs">JPEG</label>
                      </div>
                    </div>
                  </div>
                </div>

                <!-- Tombol Utama -->
                <button class="btn btn-accent" @click="performExport">
                  Export to (.{{ selectedFormat?.toLowerCase() || '' }})
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Yield -->
      <div class="col-span-2 row-span-1 p-4 bg-base-300 rounded-lg shadow summary-card">
        <h2 class="mb-5 font-medium text-lg text-accent">Yield</h2>
        <div class="tabs tabs-lift">
          <!-- Table -->
          <label class="tab">
            <input type="radio" name="tabs2" checked />
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-4 me-2">
              <path fill-rule="evenodd"
                d="M.99 5.24A2.25 2.25 0 0 1 3.25 3h13.5A2.25 2.25 0 0 1 19 5.25l.01 9.5A2.25 2.25 0 0 1 16.76 17H3.26A2.267 2.267 0 0 1 1 14.74l-.01-9.5Zm8.26 9.52v-.625a.75.75 0 0 0-.75-.75H3.25a.75.75 0 0 0-.75.75v.615c0 .414.336.75.75.75h5.373a.75.75 0 0 0 .627-.74Zm1.5 0a.75.75 0 0 0 .627.74h5.373a.75.75 0 0 0 .75-.75v-.615a.75.75 0 0 0-.75-.75H11.5a.75.75 0 0 0-.75.75v.625Zm6.75-3.63v-.625a.75.75 0 0 0-.75-.75H11.5a.75.75 0 0 0-.75.75v.625c0 .414.336.75.75.75h5.25a.75.75 0 0 0 .75-.75Zm-8.25 0v-.625a.75.75 0 0 0-.75-.75H3.25a.75.75 0 0 0-.75.75v.625c0 .414.336.75.75.75H8.5a.75.75 0 0 0 .75-.75ZM17.5 7.5v-.625a.75.75 0 0 0-.75-.75H11.5a.75.75 0 0 0-.75.75V7.5c0 .414.336.75.75.75h5.25a.75.75 0 0 0 .75-.75Zm-8.25 0v-.625a.75.75 0 0 0-.75-.75H3.25a.75.75 0 0 0-.75.75V7.5c0 .414.336.75.75.75H8.5a.75.75 0 0 0 .75-.75Z"
                clip-rule="evenodd" />
            </svg>
            Data
          </label>
          <div class="tab-content bg-base-100 border-base-300 p-4">
            <div class="overflow-x-auto">
              <table class="table">
                <thead>
                  <tr>
                    <th>{{ selectedMonthYear}}</th>
                    <th v-for="(slot, index) in weeklyDates" :key="index" v-html="slot" />
                    <th>Average</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(row, rowIndex) in computedYield" :key="row.label"
                    :class="rowIndex % 2 === 0 ? 'bg-base-200' : ''">
                    <th>{{ row.label }}</th>
                    <td v-for="(val, colIndex) in row.values" :key="colIndex" v-html="getYieldContent(val)" />
                    <td>{{ row.average.toFixed(2) }}%</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>

          <!-- Chart -->
          <label class="tab">
            <input type="radio" name="tabs2" />
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-4 me-2">
              <path
                d="M15.5 2A1.5 1.5 0 0 0 14 3.5v13a1.5 1.5 0 0 0 1.5 1.5h1a1.5 1.5 0 0 0 1.5-1.5v-13A1.5 1.5 0 0 0 16.5 2h-1ZM9.5 6A1.5 1.5 0 0 0 8 7.5v9A1.5 1.5 0 0 0 9.5 18h1a1.5 1.5 0 0 0 1.5-1.5v-9A1.5 1.5 0 0 0 10.5 6h-1ZM3.5 10A1.5 1.5 0 0 0 2 11.5v5A1.5 1.5 0 0 0 3.5 18h1A1.5 1.5 0 0 0 6 16.5v-5A1.5 1.5 0 0 0 4.5 10h-1Z" />
            </svg>
            Chart
          </label>
          <div class="tab-content bg-base-100 border-base-300 p-6">
            <v-chart :option="chartYield" autoresize style="width: 100%; height: 400px;"></v-chart>
          </div>

          <!-- Export -->
          <label class="tab">
            <input type="radio" name="tabs2" />
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-4 me-2">
              <path
                d="M10.75 2.75a.75.75 0 0 0-1.5 0v8.614L6.295 8.235a.75.75 0 1 0-1.09 1.03l4.25 4.5a.75.75 0 0 0 1.09 0l4.25-4.5a.75.75 0 0 0-1.09-1.03l-2.955 3.129V2.75Z" />
              <path
                d="M3.5 12.75a.75.75 0 0 0-1.5 0v2.5A2.75 2.75 0 0 0 4.75 18h10.5A2.75 2.75 0 0 0 18 15.25v-2.5a.75.75 0 0 0-1.5 0v2.5c0 .69-.56 1.25-1.25 1.25H4.75c-.69 0-1.25-.56-1.25-1.25v-2.5Z" />
            </svg>
            Export
          </label>
          <div class="tab-content bg-base-100 border-base-300 p-6">
            <div class="flex w-full h-auto rounded-lg overflow-hidden">
              <!-- Sidebar Export Menu  -->
              <div class="w-48 border-r border-base-200 flex flex-col">
                <div class="text-sm font-semibold p-4 border-b">Export Options</div>
                <button @click="previewExport('pdf')" class="flex items-center gap-2 p-3 hover:bg-accent transition"
                  value="PDF">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="white" class="size-4">
                    <path
                      d="M2.5 3.5A1.5 1.5 0 0 1 4 2h4.879a1.5 1.5 0 0 1 1.06.44l3.122 3.12a1.5 1.5 0 0 1 .439 1.061V12.5A1.5 1.5 0 0 1 12 14H4a1.5 1.5 0 0 1-1.5-1.5v-9Z" />
                  </svg>
                  <span class="text-white">PDF</span></button>
                <button @click="previewExport('docx')" class="flex items-center gap-2 p-3 hover:bg-accent transition"
                  value="Word"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="white" class="size-4">
                    <path fill-rule="evenodd"
                      d="M4 2a1.5 1.5 0 0 0-1.5 1.5v9A1.5 1.5 0 0 0 4 14h8a1.5 1.5 0 0 0 1.5-1.5V6.621a1.5 1.5 0 0 0-.44-1.06L9.94 2.439A1.5 1.5 0 0 0 8.878 2H4Zm1 5.75A.75.75 0 0 1 5.75 7h4.5a.75.75 0 0 1 0 1.5h-4.5A.75.75 0 0 1 5 7.75Zm0 3a.75.75 0 0 1 .75-.75h4.5a.75.75 0 0 1 0 1.5h-4.5a.75.75 0 0 1-.75-.75Z"
                      clip-rule="evenodd" />
                  </svg>
                  <span class="text-white">Word</span></button>
                <button @click="previewExport('xlsx')" class="flex items-center gap-2 p-3 hover:bg-accent transition"
                  value="Excel"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="white" class="size-4">
                    <path fill-rule="evenodd"
                      d="M4 2a1.5 1.5 0 0 0-1.5 1.5v9A1.5 1.5 0 0 0 4 14h8a1.5 1.5 0 0 0 1.5-1.5V6.621a1.5 1.5 0 0 0-.44-1.06L9.94 2.439A1.5 1.5 0 0 0 8.878 2H4Zm6 5.75a.75.75 0 0 1 1.5 0v3.5a.75.75 0 0 1-1.5 0v-3.5Zm-2.75 1.5a.75.75 0 0 1 1.5 0v2a.75.75 0 0 1-1.5 0v-2Zm-2 .75a.75.75 0 0 0-.75.75v.5a.75.75 0 0 0 1.5 0v-.5a.75.75 0 0 0-.75-.75Z"
                      clip-rule="evenodd" />
                  </svg>
                  <span class="text-white">Excel</span></button>
                <button @click="previewExport('img')" class="flex items-center gap-2 p-3 hover:bg-accent transition"
                  value="Image"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="white" class="size-4">
                    <path fill-rule="evenodd"
                      d="M2 4a2 2 0 0 1 2-2h8a2 2 0 0 1 2 2v8a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V4Zm10.5 5.707a.5.5 0 0 0-.146-.353l-1-1a.5.5 0 0 0-.708 0L9.354 9.646a.5.5 0 0 1-.708 0L6.354 7.354a.5.5 0 0 0-.708 0l-2 2a.5.5 0 0 0-.146.353V12a.5.5 0 0 0 .5.5h8a.5.5 0 0 0 .5-.5V9.707ZM12 5a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"
                      clip-rule="evenodd" />
                  </svg>
                  <span class="text-white">Image</span></button>
              </div>

              <!-- Konten Preview -->
              <div class="flex-1 p-6 space-y-4">
                <!-- Upload Gambar -->
                <div id="preview" class="p-4 rounded shadow">
                  <h2 class="text-md font-semibold mb-2">Export To (.{{selectedFormat}})</h2>
                  <!-- <div class="w-60 h-60 bg-gray-50 border flex items-center justify-center rounded">
                    <img src="" alt="Preview" class="object-contain max-h-full max-w-full" />
                  </div> -->
                  <div id="export-setting">
                    <input type="radio" id="page" name="export_option" class="radio radio-sm radio-accent" value="Page"
                      v-model="exportOption" />
                    <label for="page" class="text-sm ms-2">All Page</label><br>

                    <input type="radio" id="range" name="export_option" class="radio radio-sm radio-accent"
                      value="Range" v-model="exportOption" />
                    <label for="range" class="text-sm ms-2">Range</label><br>

                    <div v-if="exportOption === 'Range'" class="mt-4 space-y-2">
                      <label class="block text-sm">Start Date:</label>
                      <input type="date" class="input input-bordered input-sm w-full max-w-xs" />

                      <label class="block text-sm">End Date:</label>
                      <input type="date" class="input input-bordered input-sm w-full max-w-xs" />
                    </div>

                    <div class="mt-4">
                      <input type="checkbox" class="checkbox checkbox-sm checkbox-accent" />
                      <label for="datetime" class="text-sm ms-2">Include Date & Time</label>
                    </div>

                    <div v-if="formatOption === 'docx'" class="space-y-2">
                      <input type="checkbox" class="checkbox checkbox-sm checkbox-accent" v-model="otherOption" />
                      <label class="text-sm ms-2">Other Format</label>

                      <div v-if="otherOption" class="flex ms-6 gap-4">
                        <input type="radio" id="docx" name="other_option" class="radio radio-xs radio-accent"
                          value="docx" />
                        <label for="docx" class="text-xs">Word Document</label>

                        <input type="radio" id="doc" name="other_option" class="radio radio-xs radio-accent"
                          value="doc" />
                        <label for="doc" class="text-xs">Word 97-2003 Document</label>
                      </div>
                    </div>

                    <div v-if="formatOption === 'img'" class="space-y-2">
                      <input type="checkbox" class="checkbox checkbox-sm checkbox-accent" v-model="otherOption" />
                      <label class="text-sm ms-2">Other Format</label>

                      <div v-if="otherOption" class="flex ms-6 gap-4">
                        <input type="radio" id="png" name="other_option" class="radio radio-xs radio-accent"
                          value="png" />
                        <label for="png" class="text-xs">PNG</label>

                        <input type="radio" id="jpg" name="other_option" class="radio radio-xs radio-accent"
                          value="jpg" />
                        <label for="jpg" class="text-xs">JPG</label>

                        <input type="radio" id="SVG" name="other_option" class="radio radio-xs radio-accent"
                          value="SVG" />

                        <label for="SVG" class="text-xs">SVG</label>

                        <input type="radio" id="JPEG" name="other_option" class="radio radio-xs radio-accent"
                          value="JPEG" />
                        <label for="JPEG" class="text-xs">JPEG</label>
                      </div>
                    </div>

                  </div>
                </div>
                <!-- Tombol Utama -->
                <button class="btn btn-accent" @click="performExport">
                  Export to (.{{ selectedFormat?.toLowerCase() || '' }})
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Retest Rate -->
      <div class="col-span-2 row-span-1 p-4 bg-base-300 rounded-lg shadow summary-card">
        <h2 class="mb-5 font-medium text-lg text-accent">Retest Rate</h2>
        <div class="tabs tabs-lift">
          <label class="tab">
            <input type="radio" name="tabs3" checked />
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-4 me-2">
              <path fill-rule="evenodd"
                d="M.99 5.24A2.25 2.25 0 0 1 3.25 3h13.5A2.25 2.25 0 0 1 19 5.25l.01 9.5A2.25 2.25 0 0 1 16.76 17H3.26A2.267 2.267 0 0 1 1 14.74l-.01-9.5Zm8.26 9.52v-.625a.75.75 0 0 0-.75-.75H3.25a.75.75 0 0 0-.75.75v.615c0 .414.336.75.75.75h5.373a.75.75 0 0 0 .627-.74Zm1.5 0a.75.75 0 0 0 .627.74h5.373a.75.75 0 0 0 .75-.75v-.615a.75.75 0 0 0-.75-.75H11.5a.75.75 0 0 0-.75.75v.625Zm6.75-3.63v-.625a.75.75 0 0 0-.75-.75H11.5a.75.75 0 0 0-.75.75v.625c0 .414.336.75.75.75h5.25a.75.75 0 0 0 .75-.75Zm-8.25 0v-.625a.75.75 0 0 0-.75-.75H3.25a.75.75 0 0 0-.75.75v.625c0 .414.336.75.75.75H8.5a.75.75 0 0 0 .75-.75ZM17.5 7.5v-.625a.75.75 0 0 0-.75-.75H11.5a.75.75 0 0 0-.75.75V7.5c0 .414.336.75.75.75h5.25a.75.75 0 0 0 .75-.75Zm-8.25 0v-.625a.75.75 0 0 0-.75-.75H3.25a.75.75 0 0 0-.75.75V7.5c0 .414.336.75.75.75H8.5a.75.75 0 0 0 .75-.75Z"
                clip-rule="evenodd" />
            </svg>
            Data
          </label>
          <div class="tab-content bg-base-100 border-base-300 p-4">
            <div class="overflow-x-auto">
              <table class="table">
                <thead>
                  <tr>
                    <th>{{selectedMonthYear}}</th>
                    <th v-for="(slot, index) in weeklyDates" :key="index" v-html="slot" />
                    <th>Worst</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(row, rowIndex) in computedRetest" :key="row.label"
                    :class="rowIndex % 2 === 0 ? 'bg-base-200' : ''">
                    <th>{{ row.label }}</th>
                    <td v-for="(val, colIndex) in row.values" :key="colIndex" v-html="getRetestContent(val)" />
                    <td>{{ row.worst.toFixed(2) }}%</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>

          <label class="tab">
            <input type="radio" name="tabs3" />
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-4 me-2">
              <path
                d="M15.5 2A1.5 1.5 0 0 0 14 3.5v13a1.5 1.5 0 0 0 1.5 1.5h1a1.5 1.5 0 0 0 1.5-1.5v-13A1.5 1.5 0 0 0 16.5 2h-1ZM9.5 6A1.5 1.5 0 0 0 8 7.5v9A1.5 1.5 0 0 0 9.5 18h1a1.5 1.5 0 0 0 1.5-1.5v-9A1.5 1.5 0 0 0 10.5 6h-1ZM3.5 10A1.5 1.5 0 0 0 2 11.5v5A1.5 1.5 0 0 0 3.5 18h1A1.5 1.5 0 0 0 6 16.5v-5A1.5 1.5 0 0 0 4.5 10h-1Z" />
            </svg>
            Chart
          </label>
          <div class="tab-content bg-base-100 border-base-300 p-6">
            <v-chart :option="chartRetest" autoresize style="width: 100%; height: 400px;"></v-chart>
          </div>

          <label class="tab">
            <input type="radio" name="tabs3" />
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-4 me-2">
              <path
                d="M10.75 2.75a.75.75 0 0 0-1.5 0v8.614L6.295 8.235a.75.75 0 1 0-1.09 1.03l4.25 4.5a.75.75 0 0 0 1.09 0l4.25-4.5a.75.75 0 0 0-1.09-1.03l-2.955 3.129V2.75Z" />
              <path
                d="M3.5 12.75a.75.75 0 0 0-1.5 0v2.5A2.75 2.75 0 0 0 4.75 18h10.5A2.75 2.75 0 0 0 18 15.25v-2.5a.75.75 0 0 0-1.5 0v2.5c0 .69-.56 1.25-1.25 1.25H4.75c-.69 0-1.25-.56-1.25-1.25v-2.5Z" />
            </svg>
            Export
          </label>
          <div class="tab-content bg-base-100 border-base-300 p-6">
            <div class="flex w-full h-auto rounded-lg overflow-hidden">
              <!-- Sidebar Export Menu  -->
              <div class="w-48 border-r border-base-200 flex flex-col">
                <div class="text-sm font-semibold p-4 border-b">Export Options</div>
                <button @click="previewExport('pdf')" class="flex items-center gap-2 p-3 hover:bg-accent transition"
                  value="PDF">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="white" class="size-4">
                    <path
                      d="M2.5 3.5A1.5 1.5 0 0 1 4 2h4.879a1.5 1.5 0 0 1 1.06.44l3.122 3.12a1.5 1.5 0 0 1 .439 1.061V12.5A1.5 1.5 0 0 1 12 14H4a1.5 1.5 0 0 1-1.5-1.5v-9Z" />
                  </svg>
                  <span class="text-white">PDF</span></button>
                <button @click="previewExport('docx')" class="flex items-center gap-2 p-3 hover:bg-accent transition"
                  value="Word"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="white" class="size-4">
                    <path fill-rule="evenodd"
                      d="M4 2a1.5 1.5 0 0 0-1.5 1.5v9A1.5 1.5 0 0 0 4 14h8a1.5 1.5 0 0 0 1.5-1.5V6.621a1.5 1.5 0 0 0-.44-1.06L9.94 2.439A1.5 1.5 0 0 0 8.878 2H4Zm1 5.75A.75.75 0 0 1 5.75 7h4.5a.75.75 0 0 1 0 1.5h-4.5A.75.75 0 0 1 5 7.75Zm0 3a.75.75 0 0 1 .75-.75h4.5a.75.75 0 0 1 0 1.5h-4.5a.75.75 0 0 1-.75-.75Z"
                      clip-rule="evenodd" />
                  </svg>
                  <span class="text-white">Word</span></button>
                <button @click="previewExport('xlsx')" class="flex items-center gap-2 p-3 hover:bg-accent transition"
                  value="Excel"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="white" class="size-4">
                    <path fill-rule="evenodd"
                      d="M4 2a1.5 1.5 0 0 0-1.5 1.5v9A1.5 1.5 0 0 0 4 14h8a1.5 1.5 0 0 0 1.5-1.5V6.621a1.5 1.5 0 0 0-.44-1.06L9.94 2.439A1.5 1.5 0 0 0 8.878 2H4Zm6 5.75a.75.75 0 0 1 1.5 0v3.5a.75.75 0 0 1-1.5 0v-3.5Zm-2.75 1.5a.75.75 0 0 1 1.5 0v2a.75.75 0 0 1-1.5 0v-2Zm-2 .75a.75.75 0 0 0-.75.75v.5a.75.75 0 0 0 1.5 0v-.5a.75.75 0 0 0-.75-.75Z"
                      clip-rule="evenodd" />
                  </svg>
                  <span class="text-white">Excel</span></button>
                <button @click="previewExport('img')" class="flex items-center gap-2 p-3 hover:bg-accent transition"
                  value="Image"><svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="white" class="size-4">
                    <path fill-rule="evenodd"
                      d="M2 4a2 2 0 0 1 2-2h8a2 2 0 0 1 2 2v8a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V4Zm10.5 5.707a.5.5 0 0 0-.146-.353l-1-1a.5.5 0 0 0-.708 0L9.354 9.646a.5.5 0 0 1-.708 0L6.354 7.354a.5.5 0 0 0-.708 0l-2 2a.5.5 0 0 0-.146.353V12a.5.5 0 0 0 .5.5h8a.5.5 0 0 0 .5-.5V9.707ZM12 5a1 1 0 1 1-2 0 1 1 0 0 1 2 0Z"
                      clip-rule="evenodd" />
                  </svg>
                  <span class="text-white">Image</span></button>
              </div>

              <!-- Konten Preview -->
              <div class="flex-1 p-6 space-y-4">
                <!-- Upload Gambar -->
                <div id="preview" class="p-4 rounded shadow">
                  <h2 class="text-md font-semibold mb-2">Export To (.{{selectedFormat}})</h2>
                  <!-- <div class="w-60 h-60 bg-gray-50 border flex items-center justify-center rounded">
                    <img src="" alt="Preview" class="object-contain max-h-full max-w-full" />
                  </div> -->
                  <div id="export-setting">
                    <input type="radio" id="page" name="export_option" class="radio radio-sm radio-accent" value="Page"
                      v-model="exportOption" />
                    <label for="page" class="text-sm ms-2">All Page</label><br>

                    <input type="radio" id="range" name="export_option" class="radio radio-sm radio-accent"
                      value="Range" v-model="exportOption" />
                    <label for="range" class="text-sm ms-2">Range</label><br>

                    <div v-if="exportOption === 'Range'" class="mt-4 space-y-2">
                      <label class="block text-sm">Start Date:</label>
                      <input type="date" class="input input-bordered input-sm w-full max-w-xs" />

                      <label class="block text-sm">End Date:</label>
                      <input type="date" class="input input-bordered input-sm w-full max-w-xs" />
                    </div>

                    <div class="mt-4">
                      <input type="checkbox" class="checkbox checkbox-sm checkbox-accent" />
                      <label for="datetime" class="text-sm ms-2">Include Date & Time</label>
                    </div>

                    <div v-if="formatOption === 'docx'" class="space-y-2">
                      <input type="checkbox" class="checkbox checkbox-sm checkbox-accent" v-model="otherOption" />
                      <label class="text-sm ms-2">Other Format</label>

                      <div v-if="otherOption" class="flex ms-6 gap-4">
                        <input type="radio" id="docx" name="other_option" class="radio radio-xs radio-accent"
                          value="docx" />
                        <label for="docx" class="text-xs">Word Document</label>

                        <input type="radio" id="doc" name="other_option" class="radio radio-xs radio-accent"
                          value="doc" />
                        <label for="doc" class="text-xs">Word 97-2003 Document</label>
                      </div>
                    </div>

                    <div v-if="formatOption === 'img'" class="space-y-2">
                      <input type="checkbox" class="checkbox checkbox-sm checkbox-accent" v-model="otherOption" />
                      <label class="text-sm ms-2">Other Format</label>

                      <div v-if="otherOption" class="flex ms-6 gap-4">
                        <input type="radio" id="png" name="other_option" class="radio radio-xs radio-accent"
                          value="png" />
                        <label for="png" class="text-xs">PNG</label>

                        <input type="radio" id="jpg" name="other_option" class="radio radio-xs radio-accent"
                          value="jpg" />
                        <label for="jpg" class="text-xs">JPG</label>

                        <input type="radio" id="SVG" name="other_option" class="radio radio-xs radio-accent"
                          value="SVG" />
                        <label for="SVG" class="text-xs">SVG</label>

                        <input type="radio" id="JPEG" name="other_option" class="radio radio-xs radio-accent"
                          value="JPEG" />
                        <label for="JPEG" class="text-xs">JPEG</label>
                      </div>
                    </div>
                  </div>
                </div>

                <!-- Eksport Button -->
                <button class="btn btn-accent" @click="performExport">
                  Export to (.{{ selectedFormat?.toLowerCase() || '' }})
                </button>
              </div>
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
    LineChart,
    BarChart
  } from 'echarts/charts'
  import {
    GridComponent,
    TooltipComponent,
    LegendComponent,
    GraphicComponent
  } from 'echarts/components'
  echart.use([
    CanvasRenderer,
    LineChart,
    BarChart,
    TooltipComponent,
    LegendComponent,
    GraphicComponent,
    GridComponent,
  ])
  import * as echart from 'echarts/core'
  import * as XLSX from 'xlsx'
  import jsPDF from 'jspdf'
  import autoTable from 'jspdf-autotable'
  import html2canvas from 'html2canvas'
  interface RawDataRow {
    label: string;
    values: number[];
    color: string;
  }

  interface ComputedDataRow extends RawDataRow {
    highest: number;
  }

  interface YieldDataRow {
    label: string;
    values: (number | null)[];
    color: string;
  }

  interface ComputedYieldRow extends YieldDataRow {
    average: number;
  }

  interface RetestDataRow {
    label: string;
    values: (number | null)[];
    color: string;
  }

  interface ComputedRetestRow extends RetestDataRow {
    worst: number;
  }

  export default {
    data() {
      return {
        selectedLine: '3F13',
        selectedstation: 'R1',
        showDropdown: false,
        selectedShift: '',
        year: 2025,
        month: 7,
        weekInMonth: 1,
        selectedDays: [1, 2, 3, 4, 5, 6],
        filters: {
          floor: '',
          dateFrom: '',
          dateUntil: '',
          line: '3F13',
          station: 'R1'
        },
        activeFilters: ['line', 'station', 'date', 'floor'],
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
        stations: ['R1', 'R2', 'R3', 'R4', 'R5', 'R6'],
        rawData: [{
            label: this.$t('Day Shift'),
            values: [0, 0, 0, 1, 16, 7],
            color: '#FF9A00'
          },
          {
            label:this.$t('Middle Shift'),
            values: [0, 0, 0, 4, 6, 10],
            color: '#00d3bb'
          },
          {
            label: this.$t('Night Shift'),
            values: [0, 0, 0, 6, 8, 2],
            color: '#51EAEA'
          }
        ] as RawDataRow[],
        selectedFormat: '' as String | null,
        exportOption: 'Page' as string,
        formatOption: 'PDF'  as string,
        otherOption: false,
        yieldData: [{
            label: this.$t('Day Shift'),
            values: [100, 43.24, 63.64, 50, 64.22],
            color: '#FF9A00'
          },
          {
            label:this.$t('Middle Shift'),
            values: [60, 64, 70, 40, 44, 22.22],
            color: '#00d3bb'
          },
          {
            label: this.$t('Night Shift'),
            values: [100, 66.67, 66.67, 100, 63, 34],
            color: '#51EAEA'
          }
        ] as YieldDataRow[],
        retestData: [{
            label: this.$t('Day Shift'),
            values: [0, 16.22, 9.09, 0],
            color: '#FF9A00'
          },
          {
            label:this.$t('Middle Shift'),
            values: [0, 10, 8, 0],
            color: '#00d3bb'
          },
          {
            label: this.$t('Night Shift'),
            values: [0, 0, 0, 0],
            color: '#51EAEA'
          }
        ] as RetestDataRow[],
           showAlert: false
      }
    },
    methods: {
      getDatesFromMonthWeek(year: number, month: number, weekNumber: number, days: number[] = [1, 2, 3, 4, 5,
        6
      ]): string[] {
        const firstDayOfMonth = new Date(year, month - 1, 1)

        const startOfWeek = new Date(firstDayOfMonth)
        startOfWeek.setDate(1 + (weekNumber - 1) * 7)

        const result = days.map(day => {
          const date = new Date(startOfWeek)
          const currentDay = date.getDay()
          const offset = (day - currentDay + 7) % 7
          date.setDate(date.getDate() + offset)
          return date
        }).filter(date => date.getMonth() === month - 1)

        result.sort((a, b) => a.getTime() - b.getTime())
        return result.map(date =>
          `${(date.getMonth() + 1).toString().padStart(2, '0')}/${date.getDate().toString().padStart(2, '0')}`
        )

      },
      getCellContent(val: number, highest: number): string {
        return val === highest ?
          `<div class="badge badge-info">${val}</div>` :
          `${val}`
      },
      previewExport(format: string) {
        this.selectedFormat = format
        this.formatOption = format
      },
      performExport() {
        if (!this.selectedFormat) {
          alert('Please choose the format before')
          return
        }

        switch (this.selectedFormat) {
          case 'pdf':
            this.exportToPDF()
            break
          case 'docx':
            this.exportToWord()
            break
          case 'xlsx':
            this.exportToExcel()
            break
          case 'img':
            this.exportToImage()
            break
          default:
            console.warn('Format tidak dikenali')
        }
      },
      exportToPDF() {
        const doc = new jsPDF()

        const headers = ['Label', ...this.weeklyDates]
        const body = this.rawData.map(row => [row.label, ...row.values])

        autoTable(doc, {
          head: [headers],
          body: body,
          startY: 20,
          styles: {
            fontSize: 8
          }
        })

        doc.save('daily-report.pdf')
      },
      exportToExcel() {
        const ws = XLSX.utils.aoa_to_sheet(this.formattedData)
        const wb = XLSX.utils.book_new()
        XLSX.utils.book_append_sheet(wb, ws, 'Data')
        XLSX.writeFile(wb, 'export.xlsx')
      },
      exportToImage() {
        const element = document.querySelector('#preview')
        if (!element) return
        html2canvas(element as HTMLElement).then(canvas => {
          const link = document.createElement('a')
          link.href = canvas.toDataURL('image/png')
          link.download = 'export.png'
          link.click()
        })
      },
      exportToWord() {
        let tableHTML = '<table border="1" style="border-collapse:collapse;width:100%;">'
        tableHTML += '<tr>' + this.formattedData[0].map(h => `<th>${h}</th>`).join('') + '</tr>'
        this.formattedData.slice(1).forEach(row => {
          tableHTML += '<tr>' + row.map(cell => `<td>${cell}</td>`).join('') + '</tr>'
        })
        tableHTML += '</table>'

        const content = `
    <html xmlns:o="urn:schemas-microsoft-com:office:office"
          xmlns:w="urn:schemas-microsoft-com:office:word"
          xmlns="http://www.w3.org/TR/REC-html40">
      <head><meta charset="utf-8"></head>
      <body>
        <h2>Data Export</h2>
        ${tableHTML}
      </body>
    </html>
  `

        const blob = new Blob(['\ufeff', content], {
          type: 'application/msword',
        })
        const link = document.createElement('a')
        link.href = URL.createObjectURL(blob)
        link.download = 'export.doc'
        link.click()
      },
      getYieldContent(val: number | null): string {
        if (val === null) return '-';

        const display = `${val.toFixed(2)}%`;

        if (val < 90) {
          return `<div class="badge badge-error">${display}</div>`;
        } else if (val > 95) {
          return `<div class="badge badge-info">${display}</div>`;
        } else {
          return `<div class="badge badge-primary">${display}</div>`;
        }
      },
      getRetestContent(val: number | null): string {
        if (val === null) return '-';

        const display = `${val.toFixed(2)}%`;

        if (val > 5) {
          return `<div class="badge badge-error">${display}</div>`;
        } else if (val < 3) {
          return `<div class="badge badge-info">${display}</div>`;
        } else {
          return `<div class="badge badge-primary">${display}</div>`;
        }
      },
      updateChartTheme(theme:string){
        const isDark = theme === 'dark'

         // chartOptions
        this.chartOptions.legend.textStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptions.xAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptions.yAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartOptions.yAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';

        // chartYield
        this.chartYield.legend.textStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartYield.yAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartYield.xAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartYield.yAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';
        // chartRetest
        this.chartRetest.legend.textStyle.color = isDark ? '#FFFFFF' : '#000000';
        this.chartRetest.yAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartRetest.xAxis.axisLabel.color = isDark ? '#FFFFFF' : '#000000';
        this.chartRetest.yAxis.splitLine.lineStyle.color = isDark ? '#454545' : '#E0E0E0';
        this.chartRetest.yAxis.nameTextStyle.color = isDark ? '#FFFFFF' : '#000000';
      }
    },
    computed: {
      weeklyDates() {
        return this.getDatesFromMonthWeek(this.year, this.month, this.weekInMonth, this.selectedDays)
      },
      selectedMonthYear() {
        const date = new Date(this.year, this.month - 1)
        return date.toLocaleDateString('en-GB', {
          month: 'short',
          year: 'numeric'
        })
      },
      computedData(): ComputedDataRow[] {
        return this.rawData.map(row => ({
          ...row,
          highest: Math.max(...row.values)
        }))
      },
      computedYield(): ComputedYieldRow[] {
        return this.yieldData.map(row => {
          const paddedValues = [...row.values]

          while (paddedValues.length < this.weeklyDates.length) {
            paddedValues.push(null)
          }

          const validValues = paddedValues.filter((v): v is number => v !== null);
          const average = validValues.length ? validValues.reduce((sum, v) => sum + v, 0) / validValues.length :
            0;

          return {
            ...row,
            values: paddedValues,
            average
          }
        })
      },
      computedRetest(): ComputedRetestRow[] {
        return this.retestData.map(row => {
          const paddedValues = [...row.values]

          while (paddedValues.length < this.weeklyDates.length) {
            paddedValues.push(null)
          }

          const validValues = paddedValues.filter((v): v is number => v !== null);
          const worst = validValues.length ? Math.max(...validValues) : 0;


          return {
            ...row,
            values: paddedValues,
            worst
          }
        })
      },
      chartOptions() {
        return {
          backgroundColor: 'rgba(0, 0, 0, 0)',
          tooltip: {
            trigger: 'axis'
          },
          legend: {
            textStyle: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            }
          },
          xAxis: {
            type: 'category',
            data: this.weeklyDates,
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
          },
          yAxis: {
            type: 'value',
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            }
          },
          series: this.rawData.map(item => ({
            name: item.label,
            type: 'bar',
            data: item.values,
            itemStyle: {
              color: item.color,
              shadowColor: 'rgba(255, 255, 255, 0.7)',
              shadowBlur: 10
            },
          }))
        }
      },
      chartYield() {
        return {
          backgroundColor: 'rgba(0, 0, 0, 0)',
          tooltip: {
            trigger: 'axis'
          },
          legend: {
            textStyle: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            }
          },
          xAxis: {
            type: 'category',
            data: this.weeklyDates,
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
              formatter: (value: string) => value.replace('<br>', '\n')
            }
          },
          yAxis: {
            type: 'value',
            boundaryGap: false,
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            }
          },
          series: this.yieldData.map(item => ({
            type: 'line',
            smooth: true,
            showSymbol: true,
            lineStyle: {
              width: 2,
              color: item.color,
              shadowColor: 'rgba(255, 255, 255, 0.7)',
              shadowBlur: 10
            },
            name: item.label,
            data: item.values
          }))
        }
      },
      chartRetest() {
        const source: (string | number)[][] = [
          ['Date', ...this.retestData.map(s => s.label)],
          ...this.weeklyDates.map((date, i) => [
            date,
            ...this.retestData.map(s => s.values[i] ?? 0)
          ])
        ];


        const series = this.retestData.map((s, index) => ({
          type: 'line',
          name: s.label,
          encode: {
            x: 0,
            y: index + 1
          },
          lineStyle: {
            color: s.color,
            width: 2,
            shadowColor: 'rgba(255, 255, 255, 0.7)',
            shadowBlur: 10
          },
          endLabel: {
            show: true,
            formatter: (params: {
              value: any[]
            }) => `${s.label}: ${params.value[index + 1]}%`,
            color: '#FFFFFF',
            textShadowColor: 'rgba(255, 255, 255, 0.7)',
            textShadowBlur: 10,
          },
          labelLayout: {
            moveOverlap: 'shiftY'
          },
          emphasis: {
            focus: 'series'
          },
          showSymbol: false
        }))

        return {
          animationDuration: 5000,
          backgroundColor: 'rgba(0, 0, 0, 0)',
          tooltip: {
            trigger: 'axis'
          },
          legend: {
            textStyle: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            }
          },
          xAxis: {
            type: 'category',
            data: this.weeklyDates,
            name: 'Date',
            nameLocation: 'middle',
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLine: {
              lineStyle: {
                color: '#454545'
              }
            },
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            }
          },
          yAxis: {
            name: 'Percentage (%)',
            max: 100,
            nameTextStyle: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLabel: {
              color: '#FFFFFF',
              textShadowColor: 'rgba(255, 255, 255, 0.7)',
              textShadowBlur: 10,
            },
            axisLine: {
              lineStyle: {
                color: '#454545'
              }
            },
            splitLine: {
              lineStyle: {
                color: '#454545'
              }
            }
          },
          grid: {
            right: 140,
            left: '10%',
            bottom: '15%'
          },
          dataset: {
            source
          },
          series
        };
      },
      formattedData() {
        const headerRow = ['Label', ...this.weeklyDates.map(slot => slot.replace(/<br>/g, ' - '))]
        const rows = this.rawData.map(row => [row.label, ...row.values])
        return [headerRow, ...rows]
      },
      currentTheme() {
        return document.documentElement.getAttribute('data-theme') || 'light'
      }
    },
    watch:{
      currentTheme(newTheme) {
        this.updateChartTheme(newTheme)
      }
    },
    mounted(){
      const savedTheme = localStorage.getItem('theme') || 'light'
      this.updateChartTheme(savedTheme)
      this.showAlert = true
            setTimeout(() => {
                this.showAlert = false
            }, 10000)
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

  .btn:where(input:checked:not(.filter .btn)) {
    --btn-color: var(--color-accent) !important;
  }

  .fade-enter-active,
    .fade-leave-active {
        transition: opacity 0.6s ease-in-out;
    }

    .fade-enter-from,
    .fade-leave-to {
        opacity: 0;
    }

   html[data-theme='light'] .font-bold {
    color: #eee;
  }

  html[data-theme='light'] .tab:not(:checked, label:has(:checked), :hover, .tab-active, [aria-selected="true"]) {
    color: #eee;
  }

  html[data-theme='light'] .select {
    background-color: #eee;
  }

  html[data-theme='light'] .input {
    background-color: #eee;
  }

  html[data-theme='light'] .text-white {
    color: #15191e;
  }

  html[data-theme='light'] .text-white:hover {
    color: #eee;
  } 
</style>