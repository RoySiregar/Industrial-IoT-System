<template>
  <div class="production font-inter ">
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

    <!--  Default konten saat tidak ada query view -->
    <div v-if="!currentComponent">
      <div class="grid grid-cols-1 xl:grid-cols-5 md:grid-cols-4 gap-4 ">

        <!-- Output, Yield , And Retest Rate -->
        <div class="col-span-5 row-span-1 p-3 font-mono ">
          <div class="flex items-center bg-glasses justify-between gap-10 w-full mb-6 summary-card px-4 rounded-lg box">
            <!-- LEFT: Input radio -->
            <div class="radio-buttons-container flex gap-4 ">
              <div class="radio-button">
                <input name="radio-group" id="radio1" class="radio-button__input" type="radio" value="day"
                  v-model="selectedShift">
                <label for="radio1" class="radio-button__label ">
                  <span class="radio-button__custom"></span>
                  Day Shift
                </label>
              </div>
              <div class="radio-button">
                <input name="radio-group" id="radio2" class="radio-button__input" type="radio" value="middle"
                  v-model="selectedShift">
                <label for="radio2" class="radio-button__label">
                  <span class="radio-button__custom"></span>
                  Middle Shift
                </label>
              </div>
              <div class="radio-button">
                <input name="radio-group" id="radio3" class="radio-button__input" type="radio" value="night"
                  v-model="selectedShift">
                <label for="radio3" class="radio-button__label">
                  <span class="radio-button__custom"></span>
                  Night Shift
                </label>
              </div>
            </div>

            <div class="wrap">
              <input checked type="radio" id="rd-1" name="radio" class="rd-1" hidden />
              <label for="rd-1" class="label font-semibold"><span>Output</span></label>

              <input type="radio" id="rd-2" name="radio" class="rd-2" hidden />
              <label for="rd-2" class="label font-semibold"><span>Yield</span></label>

              <input type="radio" id="rd-3" name="radio" class="rd-3" hidden />
              <label for="rd-3" class="label font-semibold"><span>Retest Rate</span></label>

              <div class="bar"></div>
              <div class="slidebar"></div>
            </div>

            <!-- RIGHT: Tanggal + Countdown -->
            <div class="text-right">
              <div class="text-sm font-semibold text-white">
                {{ currentDate }}
                <span class="countdown font-mono text-xl ml-5">
                  <span :style="`--value:${time.hours}`">{{ time.hours }}</span> :
                  <span :style="`--value:${time.minutes}`">{{ time.minutes }}</span> :
                  <span :style="`--value:${time.seconds}`">{{ time.seconds }}</span>
                </span>
              </div>
            </div>
          </div>
        </div>

        <!-- Card -->
        <div class="col-span-5 row-span-1 p-3">
          <div class="relative w-full overflow-hidden">
            <!-- Wrapper -->
            <div class="slider" style="--width: 18rem; --height: 30rem; --quantity: 5;">
              <div class="list relative">

                <!-- Card 1 -->
                <div class="item bg-white shadow-md rounded-xl p-4 flex-shrink-0 border border-gray-200 "
                  style="--position: 1;">
                  <p class="text-center font-semibold text-gray-700">Injection Line 1</p>
                  <div class="stats bg-base-100 justify-center w-full border mt-4">
                    <div class="stat text-center">
                      <div class="stat-title">Output</div>
                      <div class="stat-value text-accent">44</div>
                      <div class="stat-desc">L2-FootPad</div>
                    </div>
                  </div>
 
                  <ul class="list bg-base-100 rounded-box shadow-md mt-8 w:80">
                    <li class="p-4 pb-2 text-xs opacity-60 tracking-wide">Detail</li>
                    <li v-for="(line, idx) in productionLines" :key="idx"
                      class="list-row flex justify-between items-center border-b px-4 py-2 last:border-b-0">
                      <div class="font-semibold">{{ line.name }}</div>
                      <div class="font-bold text-right btn btn-soft btn-accent w-32">{{ line.output }}</div>
                    </li>
                  </ul>
                </div>

                <!-- Card 2 -->
                <div class="item bg-white shadow-md rounded-xl p-4 flex-shrink-0 border border-gray-200"
                  style="--position: 2;">
                  <p class="text-center font-semibold text-gray-700">Welding Section</p>
                  <div class="stats bg-base-100 justify-center w-full border mt-4">
                    <div class="stat text-center">
                      <div class="stat-title">Output</div>
                      <div class="stat-value text-accent">65</div>
                      <div class="stat-desc">Rear-Frame</div>
                    </div>
                  </div>
                  <ul class="list bg-base-100 rounded-box shadow-md mt-8 w:80">
                    <li class="p-4 pb-2 text-xs opacity-60 tracking-wide">Detail</li>
                    <li v-for="(line, idx) in productionLines" :key="idx"
                      class="list-row flex justify-between items-center border-b px-4 py-2 last:border-b-0">
                      <div class="font-semibold">{{ line.name }}</div>
                      <div class="font-bold text-right btn btn-soft btn-accent w-32">{{ line.output }}</div>
                    </li>
                  </ul>
                </div>

                <!-- Card 3 -->
                <div class="item bg-white shadow-md rounded-xl p-4 flex-shrink-0 border border-gray-200"
                  style="--position: 3;">
                  <p class="text-center font-semibold text-gray-700">Painting Zone</p>
                  <div class="stats bg-base-100 justify-center w-full border mt-4">
                    <div class="stat text-center">
                      <div class="stat-title">Output</div>
                      <div class="stat-value text-accent">53</div>
                      <div class="stat-desc">Top Cover</div>  
                    </div>
                  </div>
                  <ul class="list bg-base-100 rounded-box shadow-md mt-8 w:80">
                    <li class="p-4 pb-2 text-xs opacity-60 tracking-wide">Detail</li>
                    <li v-for="(line, idx) in productionLines" :key="idx"
                      class="list-row flex justify-between items-center border-b px-4 py-2 last:border-b-0">
                      <div class="font-semibold">{{ line.name }}</div>
                      <div class="font-bold text-right btn btn-soft btn-accent w-32">{{ line.output }}</div>
                    </li>
                  </ul>  
                </div>

                <!-- Card 4 -->
                <div class="item bg-white shadow-md rounded-xl p-4 flex-shrink-0 border border-gray-200"
                  style="--position: 4;">
                  <p class="text-center font-semibold text-gray-700">Assembly Unit</p>
                  <div class="stats bg-base-100 justify-center w-full border mt-4">
                    <div class="stat text-center">
                      <div class="stat-title">Output</div>
                      <div class="stat-value text-accent">78</div>
                      <div class="stat-desc">Full Body</div>
                    </div>
                  </div>
                  <ul class="list bg-base-100 rounded-box shadow-md mt-8 w:80">
                    <li class="p-4 pb-2 text-xs opacity-60 tracking-wide">Detail</li>
                    <li v-for="(line, idx) in productionLines" :key="idx"
                      class="list-row flex justify-between items-center border-b px-4 py-2 last:border-b-0">
                      <div class="font-semibold">{{ line.name }}</div>
                      <div class="font-bold text-right btn btn-soft btn-accent w-32">{{ line.output }}</div>
                    </li>
                  </ul>
                </div>

                <!-- Card 5 -->
                <div class="item bg-white shadow-md rounded-xl p-4 flex-shrink-0 border border-gray-200"
                  style="--position: 5;">
                  <p class="text-center font-semibold  text-gray-700">Final Check</p>
                  <div class="stats bg-base-100 border justify-center w-full mt-4">
                    <div class="stat text-center">
                      <div class="stat-title">Output</div>
                      <div class="stat-value text-accent">90</div>
                      <div class="stat-desc">QC Passed</div>
                    </div>
                  </div>
                  <ul class="list bg-base-100 rounded-box shadow-md mt-8 w:80">
                    <li class="p-4 pb-2 text-xs opacity-60 tracking-wide">Detail</li>
                    <li v-for="(line, idx) in productionLines" :key="idx"
                      class="list-row flex justify-between items-center border-b px-4 py-2 last:border-b-0">
                      <div class="font-semibold">{{ line.name }}</div>
                      <div class="font-bold text-right btn btn-soft btn-accent w-32">{{ line.output }}</div>
                    </li>
                  </ul>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <component :is="currentComponent" :key="$route.fullPath" v-else />
  </div>
</template>

<script lang="ts">
  import YieldRate from './YieldRate.vue'
  import UphUpd from './UphUpd.vue'

  export default {
    components: {
      YieldRate,
      UphUpd
    },

    computed: {
      currentComponent() {
        const view = this.$route.query.view
        switch (view) {
          case 'yield':
            return 'YieldRate'
          case 'uphd':
            return 'UphUpd'
          default:
            return null
        }
      },
      currentDate() {
        return new Date().toLocaleDateString('en-GB', {
          weekday: 'long',
          year: 'numeric',
          month: 'long',
          day: 'numeric'
        })
      }
    },

    data(): Record < string,
    any > {
      return {
        countdownInterval: null,
        autoSlideInterval: null,
        time: {
          hours: '00',
          minutes: '00',
          seconds: '00'
        },
        exampleCards: [{
            title: 'Cutting'
          },
          {
            title: 'Sewing'
          },
          {
            title: 'Assembling'
          },
          {
            title: 'Quality Check'
          },
          {
            title: 'Final Check'
          },
        ],
        productionLines: [{
            name: 'L2-FootPad',
            output: 44
          },
          {
            name: 'L1',
            output: 27
          },
          {
            name: 'L3',
            output: 68
          },
          {
            name: 'L4',
            output: 120
          }
        ],
        showSummaryCard: true,
        stationList: ['R1', 'R2', 'R3'],
        selectedLine: '3F15',
        selectedSubmenu: 'Bootup',
        expandedStation: [],
        stationDetailsCards: [],
        stationSubDetailsCards: [],
        showDropdown: false,
        filters: {
          site: '',
          bu: '',
          dateFrom: '',
          dateUntil: '',
          factory: '',
          line: ''
        },
        activeFilters: ['site', 'bu', 'date'],
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
        sites: ['Site A', 'Site B'],
        bus: ['BU1', 'BU2'],
        factories: ['Factory 1', 'Factory 2'],
        stationData: [{
            station: 'R1',
            group: 'Bootup',
            total: 10000,
            pass: 9200,
            fail: 800,
            passRate: 92,
            repeatRate: 5
          },
          {
            station: 'R2',
            group: 'Wifi+BT',
            total: 12000,
            pass: 11000,
            fail: 1000,
            passRate: 91.7,
            repeatRate: 4.8
          },
          {
            station: 'R3',
            group: 'Bootup',
            total: 8409,
            pass: 8059,
            fail: 350,
            passRate: 95.7,
            repeatRate: 5.5
          }
        ],
        selectedShift : 'day' ,
        showAlert : false
      }
    },

    mounted() {
      this.updateTime();
      this.countdownInterval = setInterval(this.updateTime, 1000);
      this.showAlert = true
      setInterval(() => {
        this.showAlert = false
      }, 10000)
    },

    beforeUnmount() {
      clearInterval(this.countdownInterval);
      clearInterval(this.autoSlideInterval);
    },

    methods: {
      updateTime() {
        const now = new Date();
        const pad = (val: number) => val.toString().padStart(2, '0');
        this.time.hours = pad(now.getHours());
        this.time.minutes = pad(now.getMinutes());
        this.time.seconds = pad(now.getSeconds());
      },

      toggleDropdown() {
        this.showDropdown = !this.showDropdown;
      },

    }
  }
</script>

<style scoped>
  /* .btn-accent {
  background-color : #77b4c1  !important;
} */

  .summary-card {
    transition: transform 0.2s ease;
  }

  .summary-card:hover {
    transform: translateY(-4px);
  }

  .scroll-smooth {
    scroll-behavior: smooth;
  }

  .flex {
    scroll-padding: 1rem;
    /* opsional, agar scroll posisinya pas */
  }

  .card-item {
    transition: transform 0.3s ease, box-shadow 0.3s ease;
    cursor: pointer;
    position: relative;
    will-change: transform;
    backface-visibility: hidden;
  }

  .card-item:hover {
    transform: scale(1.05) translateY(-8px);
    box-shadow: 0 10px 20px rgba(0, 0, 0, 0.15);
    z-index: 10;
  }

  /* main style */
  .wrap {
    --round: 10px;
    --p-x: 8px;
    --p-y: 4px;
    --w-label: 125px;
    display: flex;
    align-items: center;
    padding: var(--p-y) var(--p-x);
    position: relative;
    /* --color-accent : #77b4c1; */
    /* background: var(--color-accent); */
    border-radius: var(--round);
    max-width: 100%;
    overflow-x: auto;
    scrollbar-width: none;
    -webkit-overflow-scrolling: touch;
    top: 0;
    z-index: 1;
  }

  .wrap input {
    height: 0;
    width: 0;
    position: absolute;
    overflow: hidden;
    display: none;
    visibility: hidden;
  }

  .label {
    cursor: pointer;
    outline: none;
    font-size: 0.875rem;
    letter-spacing: initial;
    font-weight: 500;
    color: var(--color-base-content);
    background: transparent;
    padding: 12px 16px;
    width: var(--w-label);
    min-width: var(--w-label);
    text-decoration: none;
    -webkit-user-select: none;
    user-select: none;
    transition: color 0.25s ease;
    outline-offset: -6px;
    display: flex;
    align-items: center;
    justify-content: center;
    position: relative;
    z-index: 2;
    -webkit-tap-highlight-color: transparent;
  }

  .label span {
    overflow: hidden;
    display: -webkit-box;
    -webkit-box-orient: vertical;
    /* -webkit-line-clamp: 1; */
  }

  .wrap input[class*="rd-"]:checked+label {
    color: var(--color-pure);
  }

  .bar {
    display: flex;
    align-items: center;
    justify-content: center;
    flex-shrink: 0;
    position: absolute;
    transform-origin: 0 0 0;
    height: 100%;
    width: var(--w-label);
    z-index: 0;
    transition: transform 0.5s cubic-bezier(0.33, 0.83, 0.99, 0.98);
  }

  .bar::before,
  .bar::after {
    content: "";
    position: absolute;
    height: 4px;
    width: 100%;
    background: var(--color-accent);
  }

  .bar::before {
    top: 0;
    border-radius: 0 0 9999px 9999px;
  }

  .bar::after {
    bottom: 0;
    border-radius: 9999px 9999px 0 0;
  }

  .slidebar {
    position: absolute;
    height: calc(100% - (var(--p-y) * 4));
    width: var(--w-label);
    border-radius: calc(var(--round) - var(--p-y));
    background: var(--muted);
    transform-origin: 0 0 0;
    z-index: 0;
    transition: transform 0.5s cubic-bezier(0.33, 0.83, 0.99, 0.98);
  }

  .rd-1:checked~.bar,
  .rd-1:checked~.slidebar,
  .rd-1+label:hover~.slidebar {
    transform: translateX(0) scaleX(1);
  }

  .rd-2:checked~.bar,
  .rd-2:checked~.slidebar,
  .rd-2+label:hover~.slidebar {
    transform: translateX(100%) scaleX(1);
  }

  .rd-3:checked~.bar,
  .rd-3:checked~.slidebar,
  .rd-3+label:hover~.slidebar {
    transform: translateX(200%) scaleX(1);
  }

  /* Card */
  @keyframes card-carousel {
    0% {
      transform: translateX(0%);
    }

    20% {
      transform: translateX(0%);
    }

    25% {
      transform: translateX(-100%);
    }

    45% {
      transform: translateX(-100%);
    }

    50% {
      transform: translateX(-200%);
    }

    70% {
      transform: translateX(-200%);
    }

    75% {
      transform: translateX(-300%);
    }

    95% {
      transform: translateX(-300%);
    }

    100% {
      transform: translateX(0%);
    }
  }

  .no-scrollbar::-webkit-scrollbar {
    display: none;
  }

  .no-scrollbar {
    -ms-overflow-style: none;
    scrollbar-width: none;
  }

  /* Day Night Shift */
  .app {
    --light-col: #ffffff;
    --dark-col: #1d232a;
    display: flex;
    flex-direction: column;
    box-shadow: 0 4px 35px rgba(0, 0, 0, 0.1);
    position: relative;
    z-index: 1;
    width: 16rem;
    height: 18rem;
    background-color: #ffffff;
    border-radius: 10px;
    overflow: hidden;
    user-select: none;
  }

  .app::before {
    --dimension: 50px;
    content: "";
    width: var(--dimension);
    height: var(--dimension);
    border-radius: 50%;
    background: #1d232a;
    position: absolute;
    top: calc(50% - (var(--dimension) / 2));
    left: calc(50% - (var(--dimension)) / 2);
    display: block;
    box-shadow: 0px 0px 0 0px var(--dark-col);
    z-index: -1;
    transition: 400ms ease-out;
  }

  nav {
    display: flex;
    justify-content: space-around;
    padding: 10px 0;
    width: 100%;
  }

  .battery {
    display: inline-block;
    width: 0.85rem;
    height: 0.45rem;
    background-color: var(--dark-col);
  }

  .network {
    display: inline-block;
    width: 0;
    height: 0;
    border-style: solid;
    border-width: 0 6.8px 7.2px 6.8px;
    border-color: transparent transparent var(--dark-col) transparent;
    transform: rotate(135deg);
  }

  /*circle*/
  .circle {
    position: relative;
    border-radius: 100%;
    width: 6rem;
    height: 6rem;
    background: linear-gradient(40deg, #ff0080, #ff8c00 70%);
    margin: auto;
  }

  .circle::before {
    content: "";
    position: absolute;
    border-radius: 50%;
    right: 0;
    width: 4rem;
    height: 4rem;
    z-index: 1;
    background: var(--bg);
    transform: scale(0);
    transform-origin: top right;
    transition: 450ms;
  }

  .label-switch {
    width: auto;
    margin: 20px 30px;
    padding: 15px 35px;
    background-color: rgba(0, 0, 0, 0.1);
    border-radius: 50px;
    position: relative;
    cursor: pointer;
    font-size: 0.8rem;
    font-weight: bolder;
    color: var(--dark-col);
    display: flex;
    justify-content: space-between;
  }

  .label-switch::before {
    content: "";
    position: absolute;
    width: 50%;
    inset: 0;
    border-radius: inherit;
    display: block;
    z-index: -1;
    background-color: #fff;
    -webkit-box-shadow: 0 2px 15px rgba(0, 0, 0, 0.15);
    box-shadow: 0 2px 15px rgba(0, 0, 0, 0.15);
    -webkit-transition: -webkit-transform 0.3s cubic-bezier(0.25, 0.46, 0.45, 0.94);
    transition: -webkit-transform 0.3s cubic-bezier(0.25, 0.46, 0.45, 0.94);
    transition: transform 0.3s cubic-bezier(0.25, 0.46, 0.45, 0.94);
    transition:
      transform 0.3s cubic-bezier(0.25, 0.46, 0.45, 0.94),
      -webkit-transform 0.3s cubic-bezier(0.25, 0.46, 0.45, 0.94);
  }

  [type="checkbox"] {
    appearance: none;
  }

  [type="checkbox"]:checked+.app .label-switch span.light {
    color: var(--light-col);
  }

  [type="checkbox"]:checked+.app .label-switch::before {
    transform: translateX(100%);
  }

  [type="checkbox"]:checked+.app::before {
    box-shadow: 0px 0px 0 150px var(--dark-col);
  }

  /* Circle */
  [type="checkbox"]:checked+.app .circle::before {
    transform: scale(1);
    background: var(--dark-col);
  }

  [type="checkbox"]:checked+.app .circle {
    background: linear-gradient(40deg, #35a29f, #a3e9fb 70%);
  }

  [type="checkbox"]:checked+.app .label {
    color: #ffffff !important;
  }

  [type="checkbox"]:checked+.app .time {
    color: var(--light-col);
  }

  [type="checkbox"]:checked+.app .icons .network {
    border-bottom-color: var(--light-col);
  }

  [type="checkbox"]:checked+.app .icons .battery {
    background-color: var(--light-col);
  }

  html[data-theme='dark'] .app {
    --light-col: #ecf9ff;
    background-color: #ecf9ff;
  }

  button.group {
    position: relative;
    overflow: hidden;
  }

  button.group::before {
    content: "";
    position: absolute;
    inset: 0;
    background-color: #00d3bb;
    /* warna btn-accent */
    border-radius: 9999px;
    z-index: -1;
    transition: all 0.7s ease;
    transform: translateX(-100%) scale(1);
  }

  button.group:hover::before {
    transform: translateX(0) scale(1.5);
    box-shadow:
      0 0 8px 2px #00d3bb,
      0 0 20px 5px #00d3bb;
  }

  button.group:hover {
    color: white;
  }

  .slider {
    width: 100%;
    height: var(--height);
    overflow: hidden;
    position: relative;
    margin-right: 3rem;
    --gap: 5rem;
  }

  .list {
    position: relative;
  }

  .item {
    width: var(--width);
    height: var(--height);
    position: absolute;
    left: calc((var(--width) + var(--gap)) * (var(--position) - 1));
    animation: autoRun 30s linear infinite;
    animation-delay: calc((30s / var(--quantity)) * (var(--position) - 1) - 30s) !important;
    transition: filter 0.3s;
  }

  @keyframes autoRun {
    from {
      left: 100%;
    }

    to {
      left: calc((var(--width) + var(--gap)) * -1);
    }
  }

  /* Pause on hover effect */
  .slider:hover .item {
    animation-play-state: paused !important;
    transform: scale(1);
    filter: blur(10px);
  }

  .slider .item:hover {
    filter: blur(0);
    z-index: 10;
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
    
  html[data-theme='light'] .box{
    background-color: #15191e;
    color: #fff;
    box-shadow: none;
  }
  
  html[data-theme='light'] .label {
    color: #fff;
  }

  html[data-theme='light'] .item{
    background-color: #15191e;
  }

  html[data-theme='light'] p {
    color: #fff;  
  }

  html[data-theme='light'] .stats{
    background-color: #eee;
  }

  html[data-theme='light'] .list{
    background-color: #eee;
  }

   html[data-theme='light'] .btn-soft{
    background-color: #15191e;
  }
</style>