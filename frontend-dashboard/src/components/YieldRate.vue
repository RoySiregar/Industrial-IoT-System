<template>
  <div class="yield-rate">
    <div class="grid grid-cols-[18rem_1fr] gap-4">
      <div class="max-w-[18rem] max-h-[32rem] overflow-y-auto bg-base-300 p-4 rounded-lg sh0ont-bold mb-4 summary-card">
        <h2 class="text-base-content font-semibold">{{ selectedLine }}</h2>
        <ul class="menu menu-xs bg-base-300 rounded-box max-w-xs w-full">
          <li v-for="station in stationList" :key="station" class="text-white">
            <details>
              <summary>
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="size-4">
                  <path fill-rule="evenodd"
                    d="M2 3.75A.75.75 0 0 1 2.75 3h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 3.75ZM2 8a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 8Zm0 4.25a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                    clip-rule="evenodd" />
                </svg>
                <button class="text-teal-400 w-4"><span class="text-white">{{ station
                  }}</span></button>
              </summary>
              <ul>
                <li v-for="item in stationData.filter(d => d.station === station)" :key="item.group">
                  <a @click.prevent="() => {selectSubMenu(item.station, item.group); toggleSummary(); }">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="size-4">
                      <path d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                    </svg>
                    <span class="text-blue-400 hover:underline">{{ item.group }}</span>
                  </a>
                </li>
              </ul>
            </details>
          </li>
        </ul>
      </div>

      <div class="flex flex-col space-y-4">
        <div class="w-full bg-base-300 p-4 rounded-lg shadow mb-4 summary-card">
          <div class="flex flex-wrap items-center justify-between gap-4">
            <div class="flex gap-4">
              <span class="font-semibold">{{$t('Filters')}}:</span>
              <div class="flex flex-wrap items-center gap-2">
                <div v-if="activeFilters.includes('floor')">
                  <select v-model="filters.floor" class="select select-bordered select-sm">
                    <option disabled value="">{{$t('Choose Floor')}}</option>
                    <option v-for="floor in floors" :key="floor">{{ floor }}</option>
                  </select>
                </div>

                <div v-if="activeFilters.includes('line')">
                  <select v-model="filters.line" @change="onLineChange" class="select select-bordered select-sm">
                    <option disabled value="">{{$t('Choose Line')}}</option>
                    <option v-for="line in lines" :key="line">{{ line }}</option>
                  </select>
                </div>
                <div v-if="activeFilters.includes('date')" class="flex items-center gap-2">
                  <input type="datetime-local" v-model="filters.dateFrom" class="input input-bordered input-sm" />
                  <span class="strip">-</span>
                  <input type="datetime-local" v-model="filters.dateUntil" class="input input-bordered input-sm" />
                  <button @click="fetchSummaryData" class="btn btn-sm btn-accent">{{$t('Apply')}}</button>
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
          <div class="flex justify-between items-center">
            <h3 class="text-lg font-bold">
              {{ selectedLine ? `${selectedLine} ${$t('Summary')}` : $t('Overall Summary') }}</h3>
            <button @click="toggleSummary"
              :class="[showSummaryCard? 'btn btn-sm btn-circle btn-error' : 'btn btn-sm btn-circle btn-accent']">
              {{ showSummaryCard ? '×' : '+' }}
            </button>
          </div>

          <div v-if="showSummaryCard && stationData.length > 0">
            <div class="overflow-x-auto mt-4 flex">
              <table class="table w-full">
                <thead>
                  <tr>
                    <th v-if="columnsVisibility.floor">{{$t('Floor')}}</th>
                    <th v-if="columnsVisibility.station">{{$t('Station')}}</th>
                    <th v-if="columnsVisibility.group">{{$t('Group')}}</th>
                    <th v-if="columnsVisibility.pass">PASS</th>
                    <th v-if="columnsVisibility.fail">FAIL</th>
                    <th v-if="columnsVisibility.total">{{$t('Total')}}</th>
                    <th v-if="columnsVisibility.passRate">Pass Rate (%)</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(item) in stationData" :key="`${item.station}-${item.group}`">
                    <td v-if="columnsVisibility.floor">{{ item.floor }}</td>
                    <td v-if="columnsVisibility.station">
                      {{ item.station }}
                    </td>
                    <td v-if="columnsVisibility.group">
                      <button @click="() => {selectSubMenu(item.station, item.group); toggleSummary();}">
                        <span class="text-info hover:underline"> {{ item.group }}</span>
                      </button>
                    </td>
                    <td v-if="columnsVisibility.pass">{{ item.pass }}</td>
                    <td v-if="columnsVisibility.fail">{{ item.fail }}</td>
                    <td v-if="columnsVisibility.total">{{ item.total }}</td>
                    <td v-if="columnsVisibility.passRate" v-html="getPassRate(item.passRate)" />
                  </tr>
                </tbody>
              </table>
              <div class="tooltip tooltip-left" data-tip="customize list layout">
                <div class="dropdown dropdown-bottom dropdown-end ">
                  <div tabindex="0" role="button" class="btn btn-circle btn-ghost btn-xs text-accent">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-5">
                      <path
                        d="M10 3.75a2 2 0 1 0-4 0 2 2 0 0 0 4 0ZM17.25 4.5a.75.75 0 0 0 0-1.5h-5.5a.75.75 0 0 0 0 1.5h5.5ZM5 3.75a.75.75 0 0 1-.75.75h-1.5a.75.75 0 0 1 0-1.5h1.5a.75.75 0 0 1 .75.75ZM4.25 17a.75.75 0 0 0 0-1.5h-1.5a.75.75 0 0 0 0 1.5h1.5ZM17.25 17a.75.75 0 0 0 0-1.5h-5.5a.75.75 0 0 0 0 1.5h5.5ZM9 10a.75.75 0 0 1-.75.75h-5.5a.75.75 0 0 1 0-1.5h5.5A.75.75 0 0 1 9 10ZM17.25 10.75a.75.75 0 0 0 0-1.5h-1.5a.75.75 0 0 0 0 1.5h1.5ZM14 10a2 2 0 1 0-4 0 2 2 0 0 0 4 0ZM10 16.25a2 2 0 1 0-4 0 2 2 0 0 0 4 0Z" />
                    </svg>
                  </div>
                  <ul tabindex="0"
                    class="dropdown-content menu menu-horizontal bg-base-100 rounded-box z-50 w-auto p-2 shadow-sm flex flex-row gap-2">
                    <li class="label-menu font-bold text-xs text-white px-2 py-1 pointer-events-none">
                      <span class="text-accent">{{$t('Customize List Layout')}}</span>
                    </li>
                    <div class="flex flex-row">
                      <li>
                        <div class="collapse collapse-arrow bg-base-100 text-white w-40 min-w-fit whitespace-nowrap">
                          <input type="checkbox" />
                          <div class="collapse-title font-semibold">{{$t('Floor')}}</div>
                          <div class="collapse-content text-sm">
                            <div class="flex flex-row gap-2">
                              <label class="label cursor-pointer flex gap-2">
                                <span class="label-text">{{$t('Show')}}</span>
                                <input type="radio" name="radio-floor" class="radio radio-sm"
                                  @change="setColumnVisibility('floor', true)" :checked="columnsVisibility.floor" />
                              </label>
                              <label class="label cursor-pointer flex gap-2">
                                <span class="label-text">{{$t('Hide')}}</span>
                                <input type="radio" name="radio-floor" class="radio radio-sm"
                                  @change="setColumnVisibility('floor', false)" :checked="!columnsVisibility.floor" />
                              </label>
                            </div>
                          </div>
                        </div>
                      </li>
                      <li>
                        <div class="collapse collapse-arrow bg-base-100 text-white w-40 min-w-fit whitespace-nowrap">
                          <input type="checkbox" />
                          <div class="collapse-title font-semibold">{{$t('Station')}}</div>
                          <div class="collapse-content text-sm">
                            <div class="flex flex-row gap-2">
                              <label class="label cursor-pointer flex gap-2">
                                <span class="label-text">{{$t('Show')}}</span>
                                <input type="radio" name="radio-station" class="radio radio-sm"
                                  @change="setColumnVisibility('station', true)" :checked="columnsVisibility.station" />
                              </label>
                              <label class="label cursor-pointer flex gap-2">
                                <span class="label-text">{{$t('Hide')}}</span>
                                <input type="radio" name="radio-station" class="radio radio-sm"
                                  @change="setColumnVisibility('station', false)"
                                  :checked="!columnsVisibility.station" />
                              </label>
                            </div>
                          </div>
                        </div>
                      </li>
                      <li>
                        <div class="collapse collapse-arrow bg-base-100 text-white w-40 min-w-fit whitespace-nowrap">
                          <input type="checkbox" />
                          <div class="collapse-title font-semibold">{{$t('Group')}}</div>
                          <div class="collapse-content text-sm">
                            <div class="flex flex-row gap-2">
                              <label class="label cursor-pointer flex gap-2">
                                <span class="label-text">{{$t('Show')}}</span>
                                <input type="radio" name="radio-group" class="radio radio-sm"
                                  @change="setColumnVisibility('group', true)" :checked="columnsVisibility.group" />
                              </label>
                              <label class="label cursor-pointer flex gap-2">
                                <span class="label-text">{{$t('Hide')}}</span>
                                <input type="radio" name="radio-group" class="radio radio-sm"
                                  @change="setColumnVisibility('group', false)" :checked="!columnsVisibility.group" />
                              </label>
                            </div>
                          </div>
                        </div>
                      </li>
                    </div>
                    <div class="flex flex-row">
                      <li>
                        <div class="collapse collapse-arrow bg-base-100 text-white w-40 min-w-fit whitespace-nowrap">
                          <input type="checkbox" />
                          <div class="collapse-title font-semibold">{{$t('Total')}}</div>
                          <div class="collapse-content text-sm">
                            <div class="flex flex-row gap-2">
                              <label class="label cursor-pointer flex gap-2">
                                <span class="label-text">{{$t('Show')}}</span>
                                <input type="radio" name="radio-total" class="radio radio-sm"
                                  @change="setColumnVisibility('total', true)" :checked="columnsVisibility.total" />
                              </label>
                              <label class="label cursor-pointer flex gap-2">
                                <span class="label-text">{{$t('Hide')}}</span>
                                <input type="radio" name="radio-total" class="radio radio-sm"
                                  @change="setColumnVisibility('total', false)" :checked="!columnsVisibility.total" />
                              </label>
                            </div>
                          </div>
                        </div>
                      </li>
                      <li>
                        <div class="collapse collapse-arrow bg-base-100 text-white w-40 min-w-fit whitespace-nowrap">
                          <input type="checkbox" />
                          <div class="collapse-title font-semibold">Pass</div>
                          <div class="collapse-content text-sm">
                            <div class="flex flex-row gap-2">
                              <label class="label cursor-pointer flex gap-2">
                                <span class="label-text">{{$t('Show')}}</span>
                                <input type="radio" name="radio-pass" class="radio radio-sm"
                                  @change="setColumnVisibility('pass', true)" :checked="columnsVisibility.pass" />
                              </label>
                              <label class="label cursor-pointer flex gap-2">
                                <span class="label-text">{{$t('Hide')}}</span>
                                <input type="radio" name="radio-pass" class="radio radio-sm"
                                  @change="setColumnVisibility('pass', false)" :checked="!columnsVisibility.pass" />
                              </label>
                            </div>
                          </div>
                        </div>
                      </li>
                    </div>
                    <div class="flex flex-row">
                      <li>
                        <div class="collapse collapse-arrow bg-base-100 text-white w-40 min-w-fit whitespace-nowrap">
                          <input type="checkbox" />
                          <div class="collapse-title font-semibold">Fail</div>
                          <div class="collapse-content text-sm">
                            <div class="flex flex-row gap-2">
                              <label class="label cursor-pointer flex gap-2">
                                <span class="label-text">{{$t('Show')}}</span>
                                <input type="radio" name="radio-fail" class="radio radio-sm"
                                  @change="setColumnVisibility('fail', true)" :checked="columnsVisibility.fail" />
                              </label>
                              <label class="label cursor-pointer flex gap-2">
                                <span class="label-text">{{$t('Hide')}}</span>
                                <input type="radio" name="radio-fail" class="radio radio-sm"
                                  @change="setColumnVisibility('fail', false)" :checked="!columnsVisibility.fail" />
                              </label>
                            </div>
                          </div>
                        </div>
                      </li>
                      <li>
                        <div class="collapse collapse-arrow bg-base-100 text-white w-40 min-w-fit whitespace-nowrap">
                          <input type="checkbox" />
                          <div class="collapse-title font-semibold">Pass Rate</div>
                          <div class="collapse-content text-sm">
                            <div class="flex flex-row gap-2">
                              <label class="label cursor-pointer flex gap-2">
                                <span class="label-text">{{$t('Show')}}</span>
                                <input type="radio" name="radio-passRate" class="radio radio-sm"
                                  @change="setColumnVisibility('passRate', true)"
                                  :checked="columnsVisibility.passRate" />
                              </label>
                              <label class="label cursor-pointer flex gap-2">
                                <span class="label-text">{{$t('Hide')}}</span>
                                <input type="radio" name="radio-passRate" class="radio radio-sm"
                                  @change="setColumnVisibility('passRate', false)"
                                  :checked="!columnsVisibility.passRate" />
                              </label>
                            </div>
                          </div>
                        </div>
                      </li>
                    </div>
                  </ul>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="flex flex-wrap gap-4 mt-4">
      <div v-for="card in stationSubDetailsCards" :key="card.key"
        class="bg-base-300 p-4 rounded-lg shadow relative w-full md:w-[49%]">
        <button class="absolute top-2 right-2 btn btn-xs btn-circle btn-error"
          @click="() => {closeStationCard(card); summaryToogle();}">×</button>

        <h4 class="text-md font-semibold mb-4">
          {{ card.station }} – {{ card.submenu }} ( Details)
          <span v-if="categoryDetailData[card.key]" class="text-sm font-normal text-gray-400 ml-2">
            Fixture Usage: Total: {{ categoryDetailData[card.key].totalFixture }} / Used:
            {{ categoryDetailData[card.key].usedFixture }}
          </span>
        </h4>

        <div class="overflow-x-visible mt-4 flex ">
        <table v-if="categoryDetailData[card.key] && categoryDetailData[card.key].data" class="table w-full text-sm">
          <thead>
            <tr>
              <th>Fixture No</th>
              <th>Total</th>
              <th>PASS</th>
              <th>FAIL</th>
              <th>Pass Rate (%)</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="detail in categoryDetailData[card.key].data" :key="detail.categoryNumber">
              <td>{{ detail.categoryNumber }}</td>
              <td>{{ detail.total }}</td>
              
              <td>{{ detail.passCount ?? detail.passcount ?? 0 }}</td>
              <td>{{ detail.failCount ?? detail.failcount ?? 0 }}</td>
              
              <td v-html="getPassRate(detail.passRate ?? detail.passrate)" />
            </tr>
          </tbody>
        </table>
          <p v-else>Loading details...</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
  import axios from 'axios'

  // Definisikan tipe data
  type StationSubDetailCard = {
    station: string
    submenu: string // 'groupName'
    key: string
  }

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

  // Tipe untuk satu item di dalam array 'data' dari API detail
type CategoryDetailItem = {
  categoryNumber: string;
  passCount?: number; // Optional
  passcount?: number; // Optional (Legacy)
  failCount?: number;
  failcount?: number;
  total: number;
  passRate?: number;
  passrate?: number;
  usageStatus: string;
}

  // BARU: Tipe untuk keseluruhan respon dari API detail
  type CategoryDetailResponse = {
    totalFixture: number;
    usedFixture: number;
    data: CategoryDetailItem[];
  }

  type FixtureData = {
    station: string;
    group: string;
    fixture: string;
    fixtureNo: string;
    total: number;
    pass: number;
    fail: number;
    passRate: number;
  }


  export default {
    data() {
      return {
        dataPolling: null as any,
        showSummaryCard: true,
        stationList: [] as string[],
        selectedLine: '3F13',
        selectedSubmenu: null as string | null,
        expandedStation: [] as string[],
        stationDetailsCards: [] as string[],
        stationSubDetailsCards: [] as StationSubDetailCard[],
        stationData: [] as StationData[],

        // DIUBAH: Strukturnya sekarang menyimpan objek CategoryDetailResponse, bukan array
        categoryDetailData: {} as Record < string,
        CategoryDetailResponse > ,

        fixtureData: [] as FixtureData[],

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
        floors: ['Floor 1', 'Floor 2', 'Floor 3'],
        lines: ['2F13', '2F16', '2F17', '2F19', '3F13', '3F14', '3F15', '3F17'],
        columnsVisibility: {
          floor: true,
          station: true,
          group: true,
          total: true,
          pass: true,
          fail: true,
          passRate: true,
          repeatRate: true
        },
        stationVisibility: {
          group: true,
          total: true,
          pass: true,
          fail: true,
          passRate: true,
          repeatRate: true
        },
        fixtureVisibilty: {
          fixture: true,
          fixtureNo: true,
          total: true,
          pass: true,
          fail: true,
          passRate: true
        }
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
          `http://10.175.80.201:5001/api/CategoryDetail/group-summary?${params.toString()}&${cacheBuster}`;

        try {
          const response = await axios.get(apiUrl);
          const apiData = response.data || [];

          // DIUBAH: Disesuaikan dengan nama field baru dari API (passCount, failCount, passRate)
          this.stationData = apiData.map((item: any) => ({
            floor: item.floor,
            station: item.stationName,
            group: item.groupName,
            total: item.total,
            
            // --- UBAH JADI SEPERTI INI (BACA DUA-DUANYA) ---
            pass: item.passCount || item.passcount || 0, 
            fail: item.failCount || item.failcount || 0,
            passRate: item.passRate || item.passrate || 0,
            // -----------------------------------------------
            
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
        this.stationSubDetailsCards = []
        this.showSummaryCard = true
      },
      summaryToogle() {
        this.showSummaryCard = true
      },

      async selectSubMenu(station: string, groupName: string) {
        const key = `${station}-${groupName}`;

        if (!this.stationSubDetailsCards.find(item => item.key === key)) {
          this.stationSubDetailsCards.push({
            station,
            submenu: groupName,
            key
          });
        }
        const {
          line,
          dateFrom,
          dateUntil
        } = this.filters;
        const startDate = dateFrom ? `${dateFrom}:00` : '';
        const endDate = dateUntil ? `${dateUntil}:00` : '';

        const params = new URLSearchParams({
          line: line,
          stationName: station,
          groupName: groupName,
          startDate: startDate,
          endDate: endDate
        });
        if (this.selectedShift) {
          params.append('shift', this.selectedShift);
        }

        const cacheBuster = `_=${new Date().getTime()}`;
        // DIUBAH: URL API diperbarui
        const apiUrl = `http://10.175.80.201:5001/api/CategoryDetail/details?${params.toString()}&${cacheBuster}`;

        try {
          console.log("Fetching category details from:", apiUrl);
          const response = await axios.get(apiUrl);
          this.categoryDetailData[key] = response.data;
        } catch (error) {
          console.error(`Gagal mengambil detail untuk ${key}:`, error);
          // Set an empty state to avoid template errors
          this.categoryDetailData[key] = {
            totalFixture: 0,
            usedFixture: 0,
            data: []
          };
        }
      },

      toggleDropdown() {
        this.showDropdown = !this.showDropdown
      },

      closeStationCard(card: string | StationSubDetailCard) {
        if (typeof card !== 'string' && 'key' in card) {
          this.stationSubDetailsCards = this.stationSubDetailsCards.filter(
            item => item.key !== card.key
          );
          delete this.categoryDetailData[card.key];
          this.showSummaryCard
        } else if (typeof card === 'string') {
          this.stationDetailsCards = this.stationDetailsCards.filter(station => station !== card);
          this.showSummaryCard
        }
      },
      handleShiftChange() {
        if (!this.selectedShift) {
          this.fetchSummaryData();
          return;
        }

        const baseDateStr = this.filters.dateFrom ? this.filters.dateFrom.split('T')[0] : new Date().toISOString()
          .split('T')[0];

        let startDate = '';
        let endDate = '';

        switch (this.selectedShift) {
          case 'day':
            startDate = `${baseDateStr}T06:50`;
            endDate = `${baseDateStr}T14:50`;
            break;

          case 'middle':
            startDate = `${baseDateStr}T14:50`;
            endDate = `${baseDateStr}T22:50`;
            break;

          case 'night':
            const baseDate = new Date(baseDateStr);
            const nextDate = new Date(baseDate);
            nextDate.setDate(baseDate.getDate() + 1);

            const nextDateStr = nextDate.toISOString().split('T')[0];

            startDate = `${baseDateStr}T22:50`;
            endDate = `${nextDateStr}T06:50`;
            break;
        }

        this.filters.dateFrom = startDate;
        this.filters.dateUntil = endDate;

        this.fetchSummaryData();
      },
      /*------------------------ Sela ------------------------ */
      getPassRate(val: number | null | undefined): string {
        // Cek jika val itu null ATAU undefined
        if (val === null || val === undefined) return '-';
        
        const display = `${val.toFixed(2)}%`;

        if (val >= 71 && val <= 81) {
          return `<div class="badge badge-warning">${display}</div>`;
        } else if (val >= 81 && val <= 92) {
          return `<div class="badge badge-accent">${display}</div>`;
        } else if (val >= 92) {
          return `<div class="badge badge-success">${display}</div>`;
        } else {
          return `<div class="badge badge-error">${display}</div>`;
        }
      },
      setColumnVisibility(column: keyof typeof this.columnsVisibility, value: boolean) {
        this.columnsVisibility[column] = value
        localStorage.setItem('columnsVisibility', JSON.stringify(this.columnsVisibility))
      },

      setStationVisibility(column: keyof typeof this.stationVisibility, value: boolean) {
        this.stationVisibility[column] = value
        localStorage.setItem('stationVisibility', JSON.stringify(this.stationVisibility))
      },

      setFixtureVisibility(column: keyof typeof this.fixtureVisibilty, value: boolean) {
        this.fixtureVisibilty[column] = value
        localStorage.setItem('fixtureVisibilty', JSON.stringify(this.fixtureVisibilty))
      }
      /*------------------------ Sela ------------------------ */
    },

    mounted() {
      /*------------------------ Sela ------------------------ */
      const saved = localStorage.getItem('columnsVisibility')
      if (saved) {
        this.columnsVisibility = JSON.parse(saved)
      }
      const stationSaved = localStorage.getItem('stationVisibility')
      if (stationSaved) {
        this.stationVisibility = JSON.parse(stationSaved)
      }
      const fixtureSaved = localStorage.getItem('fixtureVisibilty')
      if (fixtureSaved) {
        this.fixtureVisibilty = JSON.parse(fixtureSaved)
      }
      /*------------------------ Sela ------------------------ */
      const today = new Date().toISOString().split('T')[0];
      this.filters.dateFrom = `${today}T00:00`;
      this.filters.dateUntil = `${today}T23:59`;

      this.fetchSummaryData();

      this.dataPolling = setInterval(() => {
        console.log('Polling FixtureReport summary...');
        this.fetchSummaryData();

        this.stationSubDetailsCards.forEach(card => {
          this.selectSubMenu(card.station, card.submenu);
        });
      }, 6000);
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

  html[data-theme='light'] .collapse-title{
     color: #15191e;
  }

  html[data-theme='light'] .label-text{
     color: #15191e;
  }

  html[data-theme='light'] .radio{
     color: #15191e;
  }

  html[data-theme='light'] .strip{
     color: #eee;
  }
  
</style>