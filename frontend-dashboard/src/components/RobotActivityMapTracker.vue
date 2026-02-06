<template>
    <div class="robot-activity-map-tracker font-inter">
        <div class="relative overview overflow-y-auto">
            <div class="relative flex flex-col space-y-4 items-center  z-10">
                <div class="w-fit bg-base-300 rounded-lg shadow mb-4  ">
                    <div class="stats">
                        <div
                            class="stat xl:p-4 lg:p-4 md:p-3 sm:p-2 xs:p-2 xl:max-w-full lg:max-w-full md:max-w-full sm:max-w-full  ">
                            <div class="stat-title xl:text-xs lg:text-[8px] md:text-[7px] sm:text-[8px] xs:text-[8px]">
                                Area Production</div>
                            <div class="stat-value ">
                                <span
                                    class="font-mono lg:text-lg xl:text-2xl md:text-[14px] sm:text-[12px] xs:text-[10px] text-light">SMT</span>
                            </div>
                        </div>

                        <div
                            class="stat xl:p-4 lg:p-4 md:p-3 sm:p-2 xs:p-2 xl:max-w-full lg:max-w-full md:max-w-full xs:max-w-full  ">
                            <div class="stat-title xl:text-xs lg:text-[8px] md:text-[7px] sm:text-[8px] xs:text-[8px]">
                                Date</div>
                            <div class="gap-2">
                                <div
                                    class="flex xl:flex-row lg:flex-row md:flex-row sm:flex-col xs:flex-col items-center gap-2">
                                    <div class="flex xl:gap-2 lg:gap-2 md:gap-1">
                                        <input type="datetime-local" v-model="filters.dateFrom" disabled
                                            class="input input-bordered xl:input-sm lg:input-xs md:input-custom sm:input-custom xs:input-custom xl:w-full lg:w-full md:w-28 " />
                                        <span class="strip text-[10px] ">-</span>
                                    </div>

                                    <input type="datetime-local" v-model="filters.dateUntil" disabled
                                        class="input input-bordered xl:input-sm lg:input-xs md:input-custom sm:input-custom xs:input-custom xl:w-full lg:w-full  md:w-28 " />
                                </div>
                            </div>
                        </div>

                        <div class="stat xl:p-4 lg:p-4 md:p-3 sm:p-2 xs:p-2 ">
                            <div class="stat-title xl:text-xs lg:text-[8px] md:text-[7px] sm:text-[8px] xs:text-[8px]">
                                Floor</div>
                            <select v-model="filters.floor"
                                class="select select-bordered xl:select-sm lg:select-xs md:select-custom sm:select-custom xs:select-custom w-full">
                                <option v-for="floor in floors" :key="floor">
                                    {{ floor }}
                                </option>
                            </select>
                        </div>

                        <div class="stat xl:p-4 lg:p-4 md:p-3 sm:p-2 xs:p-2">
                            <div class="stat-title xl:text-xs lg:text-[8px] md:text-[7px] sm:text-[8px] xs:text-[8px]">
                                Shift</div>
                            <div @change="handleShiftChange"
                                class="radio-buttons-container flex flex-col xl:flex-row lg:flex-row md:flex-row xl:gap-4 lg:gap-2 md:gap-1 sm:gap-1 xs:gap-1 ">
                                <div class="radio-button">
                                    <input name="radio-group" id="radio1" class="radio-button__input" type="radio"
                                        value="day" v-model="selectedShift">
                                    <label for="radio1"
                                        class="radio-button__label xl:text-xs lg:text-[8px] md:text-[7px] sm:text-[8px] xs:text-[8px] xl:pl-8 lg:pl-6 md:pl-5 sm:pl-5 xs:pl-5">
                                        <span
                                            class="radio-button__custom xl:w-4 xl:h-4 lg:w-4 lg:h-4 md:w-3 md:h-3 sm:w-3 sm:h-3 xs:w-3 xs:h-3 "></span>
                                        {{$t('Day Shift')}}
                                    </label>
                                </div>
                                <div class="radio-button">
                                    <input name="radio-group" id="radio2" class="radio-button__input" type="radio"
                                        value="middle" v-model="selectedShift">
                                    <label for="radio2"
                                        class="radio-button__label  xl:text-xs lg:text-[8px] md:text-[7px] sm:text-[8px] xs:text-[8px] xl:pl-8 lg:pl-6 md:pl-5 sm:pl-5 xs:pl-5">
                                        <span
                                            class="radio-button__custom xl:w-4 xl:h-4 lg:w-4 lg:h-4 md:w-3 md:h-3 sm:w-3 sm:h-3 xs:w-3 xs:h-3 "></span>
                                        {{$t('Middle Shift')}}
                                    </label>
                                </div>
                                <div class="radio-button">
                                    <input name="radio-group" id="radio3" class="radio-button__input" type="radio"
                                        value="night" v-model="selectedShift">
                                    <label for="radio3"
                                        class="radio-button__label xl:text-xs lg:text-[8px] md:text-[7px] sm:text-[8px] xs:text-[8px] xl:pl-8 lg:pl-6 md:pl-5 sm:pl-5 xs:pl-5">
                                        <span
                                            class="radio-button__custom xl:w-4 xl:h-4 lg:w-4 lg:h-4 md:w-3 md:h-3 sm:w-3 sm:h-3 xs:w-3 xs:h-3 "></span>
                                        {{$t('Night Shift')}}
                                    </label>
                                </div>
                            </div>
                        </div>

                        <div
                            class="stat xl:p-4 lg:p-4 md:p-3 sm:p-2 xs:p-2 xl:max-w-full lg:max-w-24 md:max-w-20   sm:max-w-14">
                            <div class="stat-title xl:text-xs lg:text-[8px] md:text-[7px] sm:text-[8px] xs:text-[8px]">
                                Total Machine</div>
                            <div class="stat-value ">
                                <span
                                    class="text-accent font-mono lg:text-lg xl:text-2xl md:text-[12px] sm:text-[12px] xs:text-[10px]">{{ 61 }}</span>
                            </div>
                        </div>

                        <div
                            class="stat xl:p-4 lg:p-4 md:p-3 sm:p-2 xs:p-2 xl:max-w-full  lg:max-w-24 md:max-w-16 sm:max-w-14">
                            <div class="stat-title xl:text-xs lg:text-[8px] md:text-[7px] sm:text-[8px] xs:text-[8px]">
                                Total Pass</div>
                            <div class="stat-value ">
                                <span
                                    class="font-mono text-success lg:text-lg xl:text-2xl md:text-[12px] sm:text-[12px] xs:text-[10px]">{{ totalPass }}</span>
                            </div>
                        </div>

                        <div
                            class="stat xl:p-4 lg:p-4 md:p-3 sm:p-2 xs:p-2 xl:max-w-full  lg:max-w-24 md:max-w-16 sm:max-w-14">
                            <div class="stat-title xl:text-xs lg:text-[8px] md:text-[7px] sm:text-[8px] xs:text-[8px]">
                                Total Fail</div>
                            <div class="stat-value">
                                <span
                                    class="font-mono text-error lg:text-lg xl:text-2xl md:text-[12px] sm:text-[12px] xs:text-[10px]">{{ totalFail }}</span>
                            </div>
                        </div>

                        <div class="stat xl:p-4 lg:p-4 md:p-3 sm:p-2 xs:p-2 xl:max-w-full lg:max-w-28 md:max-w-28">
                            <div class="stat-title xl:text-xs lg:text-[8px] md:text-[7px] sm:text-[8px] xs:text-[8px]">
                                Total Yield Rate (%)</div>
                            <div class="stat-value ">
                                <span
                                    class="font-mono text-warning lg:text-lg xl:text-2xl md:text-[12px] sm:text-[12px] xs:text-[10px]">{{ totalYield }}%</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Map -->
            <div ref="viewport"
                class="relative w-full xl:h-[650px] lg:h-[450px] md:h-[380px] sm:h-[300px] xs:h-[300px] max-h-screen overflow-hidden select-none cursor-grab active:cursor-grabbing"
                @mousedown="startPan" @mousemove="onPan" @mouseup="endPan" @mouseleave="endPan" @wheel.prevent="onZoom">
                <div class="absolute top-0 left-0 min-w-max" :style="worldStyle" ref="world">

                    <div class="flex gap-14 p-y-4 ml-4">
                        <div v-for="col in columns" :key="col.id" class="flex flex-col gap-10">
                            <div v-for="line in col.lines" :key="line.name" class="flex items-center">
                                <div class="w-10 text-white text-center cursor-pointer flex flex-col items-center"
                                    @mouseenter="onLineHover(line)" @mouseleave="clearLineHover"
                                    @click="onLineHover(line)"
                                    :class="hoveredLine?.name === line.name ? ' scale-105 rounded-md' : ''">
                                    <p class="text-dark text-md"> {{ formatLineName(line.name) }}</p>
                                    <div class="text-sm text-black font-semibold font-mono mt-1">
                                        <span v-if="getRobotByName(line,'INPUT')" class="badge badge-md"
                                            :class="getBadgeClass(getRobotByName(line,'INPUT')!.yieldRate)">
                                            {{ getRobotByName(line,'INPUT')!.yieldRate ?? '-' }}%
                                        </span>

                                    </div>
                                </div>
                                <div class="max-w-full h-full">
                                    <div class="relative w-[48rem] h-full">
                                        <img src="/public/img/layout.png"
                                            :class="{'opacity-40' : !isLoaderRunning(line)}"
                                            class="absolute inset-0 w-full h-full object-contain z-10 pointer-events-none " />
                                        <div class="absolute inset-0 w-full h-full z-0 pointer-events-none loading-try">
                                            <div class="loader" :class="{'loader-paused' : !isLoaderRunning(line)}"
                                                :style="getLoaderStyle(line.name)"></div>
                                        </div>
                                        <div class="relative z-10 flex items-center h-full px-6"
                                            :class="isSingleOutput(line) ? 'justify-end' : 'flex-nowrap justify-between'">
                                            <button v-for="(r) in line.robot" :key="r.name"

                                                class="btn btn-lg rounded-md border-none shadow hover:scale-105 transition bg-white/70 bg-dark"
                                                :class="[  backgorundStatus(r.status),
                                                    hoveredRobot?.name === r.name && hoveredLine?.name === line.name
                                                    ? 'ring-2 ring-error ring-offset-2 ring-offset-base-300 scale-105'
                                                    : '' ]" @mouseenter="onRobotHover(r, line)"
                                                @mouseleave="clearRobotHover" @click.stop="goToRobotDetail(r, line)">

                                                <span class="absolute mt-2 rounded-lg w-14 h-10 blur z-0"
                                                    :class="statusPingClass(r.status)"></span>

                                                <div class="relative flex flex-col gap-1">
                                                    <span class="text-sm mt-4">
                                                        <img src="@/assets/icons/robot.png" alt="" class="w-8 h-8 mb-4">
                                                    </span>
                                                </div>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div class="mr-12 w-10 text-white text-center cursor-pointer flex flex-col items-center"
                                    @mouseenter="onLineHover(line)" @mouseleave="clearLineHover"
                                    @click="onLineHover(line)"
                                    :class="hoveredLine?.name === line.name ? ' scale-105 rounded-md' : ''">
                                    <p class="text-dark text-md"> {{ formatLineName(line.name) }}</p>
                                    <div class="text-sm text-black font-semibold font-mono mt-1">
                                        <span v-if="getRobotByName(line,'OUTPUT')" class="badge badge-md"
                                            :class="getBadgeClass(getRobotByName(line,'OUTPUT')!.yieldRate)">
                                            {{ getRobotByName(line,'OUTPUT')!.yieldRate ?? '' }}%
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="flex items-center mt-4">
                <!-- Indicator -->
                <div class="flex-1 z-10">
                    <div
                        class="flex xl:gap-2 lg:gap-2 md:gap-2 sm:gap-2 xs:gap-2 bg-base-300 w-fit xl:p-2 lg:p-2 md:p-1  sm:p-1  xs:p-1  rounded-full">
                        <div class="flex items-center gap-2">
                            <div class="status status-accent "></div> <span
                                class="text-[8px] text-accent">Running</span>
                        </div>

                        <div class="flex items-center gap-2">
                            <div class="status status-warning "></div> <span
                                class="text-[8px] text-warning">Pause</span>
                        </div>

                        <div class="flex items-center gap-2">
                            <div class="status status-error "></div> <span class="text-[8px] text-error">Stop</span>
                        </div>
                        <div class="flex items-center gap-2">
                            <div class="status status-neutral "></div> <span
                                class="text-[8px] text-gray-400">Offline</span>
                        </div>
                    </div>
                </div>
                <!-- Button -->
                <div class="flex justify-center flex-1 z-10">
                    <div class="flex xl:gap-4 lg:gap-4 md:gap-4 sm:gap-2 xs:gap-2 rounded-full bg-base-300 p-2">
                        <div class="tooltip" data-tip="reset">
                            <button class="btn xl:btn-xs lg:btn-xs md:btn-xs sm:btn-xs  xs:btn-xs btn-circle btn-soft  "
                                type="button" @click="resetZoom"><svg xmlns="http://wqww.w3.org/2000/svg"
                                    viewBox="0 0 20 20" fill="currentColor"
                                    class="xl:size-3 lg:size-3 md:size-3 sm:size-3 xs:size-3">
                                    <path fill-rule="evenodd"
                                        d="M15.312 11.424a5.5 5.5 0 0 1-9.201 2.466l-.312-.311h2.433a.75.75 0 0 0 0-1.5H3.989a.75.75 0 0 0-.75.75v4.242a.75.75 0 0 0 1.5 0v-2.43l.31.31a7 7 0 0 0 11.712-3.138.75.75 0 0 0-1.449-.39Zm1.23-3.723a.75.75 0 0 0 .219-.53V2.929a.75.75 0 0 0-1.5 0V5.36l-.31-.31A7 7 0 0 0 3.239 8.188a.75.75 0 1 0 1.448.389A5.5 5.5 0 0 1 13.89 6.11l.311.31h-2.432a.75.75 0 0 0 0 1.5h4.243a.75.75 0 0 0 .53-.219Z"
                                        clip-rule="evenodd" />
                                </svg>
                            </button>
                        </div>
                        <div class="tooltip" data-tip="resize in">
                            <button class="btn xl:btn-xs lg:btn-xs md:btn-xs sm:btn-xs  xs:btn-xs btn-circle btn-soft "
                                type="button" @click="fullSize"><svg xmlns="http://www.w3.org/2000/svg"
                                    viewBox="0 0 20 20" fill="currentColor"
                                    class="xl:size-3 lg:size-3 md:size-3 sm:size-3 xs:size-3">
                                    <path
                                        d="M3.28 2.22a.75.75 0 0 0-1.06 1.06L5.44 6.5H2.75a.75.75 0 0 0 0 1.5h4.5A.75.75 0 0 0 8 7.25v-4.5a.75.75 0 0 0-1.5 0v2.69L3.28 2.22ZM13.5 2.75a.75.75 0 0 0-1.5 0v4.5c0 .414.336.75.75.75h4.5a.75.75 0 0 0 0-1.5h-2.69l3.22-3.22a.75.75 0 0 0-1.06-1.06L13.5 5.44V2.75ZM3.28 17.78l3.22-3.22v2.69a.75.75 0 0 0 1.5 0v-4.5a.75.75 0 0 0-.75-.75h-4.5a.75.75 0 0 0 0 1.5h2.69l-3.22 3.22a.75.75 0 1 0 1.06 1.06ZM13.5 14.56l3.22 3.22a.75.75 0 1 0 1.06-1.06l-3.22-3.22h2.69a.75.75 0 0 0 0-1.5h-4.5a.75.75 0 0 0-.75.75v4.5a.75.75 0 0 0 1.5 0v-2.69Z" />
                                </svg>
                            </button>
                        </div>
                        <div class="tooltip" data-tip="chart">
                            <button class="btn xl:btn-xs lg:btn-xs md:btn-xs sm:btn-xs xs:btn-xs btn-circle btn-soft "
                                type="button" onclick="my_modal_3.showModal()"><svg xmlns="http://www.w3.org/2000/svg"
                                    viewBox="0 0 20 20" fill="currentColor"
                                    class="xl:size-3 lg:size-3 md:size-3 sm:size-3 xs:size-3">
                                    <path
                                        d="M15.5 2A1.5 1.5 0 0 0 14 3.5v13a1.5 1.5 0 0 0 1.5 1.5h1a1.5 1.5 0 0 0 1.5-1.5v-13A1.5 1.5 0 0 0 16.5 2h-1ZM9.5 6A1.5 1.5 0 0 0 8 7.5v9A1.5 1.5 0 0 0 9.5 18h1a1.5 1.5 0 0 0 1.5-1.5v-9A1.5 1.5 0 0 0 10.5 6h-1ZM3.5 10A1.5 1.5 0 0 0 2 11.5v5A1.5 1.5 0 0 0 3.5 18h1A1.5 1.5 0 0 0 6 16.5v-5A1.5 1.5 0 0 0 4.5 10h-1Z" />
                                </svg>
                            </button>
                        </div>
                        <dialog id="my_modal_3" class="modal">
                            <div class="modal-box xl:max-w-[30%] lg:max-w-[40%] md:max-w-[50%] bg-modal">
                                <form method="dialog">
                                    <button class="btn btn-sm btn-circle btn-ghost absolute right-2 top-2">✕</button>
                                </form>
                                <ul class="list gap-0">
                                    <li
                                        class="list-row xl:text-sm lg:text-xs md:text-xs sm:text-xs xs:text-xs opacity-60 tracking-wide">
                                        Chart Overview
                                    </li>
                                    <li class="list-row xl:text-sm lg:text-xs md:text-xs sm:text-xs xs:text-xs">
                                        <v-chart :option="chartOptionsPie" autoresize
                                            class="xl:w-[30rem] xl:h-96 lg:w-[30rem] lg:h-80 md:w-[28rem] md:h-60" />
                                    </li>
                                </ul>
                            </div>
                        </dialog>
                    </div>
                </div>
                <div class="flex-1"></div>
            </div>
        </div>
    </div>
</template>
<script lang="ts">
    import type {
        ComponentPublicInstance
    } from 'vue'
    import axios from 'axios'


    type Robot = {
        name: string
        type: string
        status: string
        pass: number
        fail: number
        yieldRate: number
    }

    // type Lines = {
    //     currentStep : string,
    //     factory : string,
    //     floor: string,
    //     line: string,
    //     code: string,
    //     status: string,
    //     statusMsg?: string | null,
    //     cntPass: number,
    //     cntFail: number,
    //     yield: number,
    //     lastCT: number,
    //     lastUpdateTime: string,
    //     robot: Robot[]
    // }

    type Line = {
        name: string
        detail: string
        passTotal: number
        failTotal: number
        yieldRateTotal: number
        robot: Robot[]
    }

    // type AllData = {
    //     factory: string,
    //     currentTime: string,
    //     floor: string,
    //     line: string,
    //     totalMachine: number,
    //     totalPass : number,
    //     totalFail : number,
    //     totalYieldRate : number
    // }

    type RobotStat = {
        pass: number
        fail: number
        yield: number
        status: string
    }


    export default {
        data() {
            return {
                filters: {
                    floor: '1F',
                    dateFrom: '',
                    dateUntil: '',
                },
                selectedShift: '',
                selectedFloor: '',
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
                floors: ['1F', '2F', '3F'],
                lines: [] as Line[],
                scale: 0.65,
                translateX: 0,
                translateY: 0,
                isPanning: false,
                startX: 0,
                startY: 0,
                area: [] as any[],
                columns: [] as any[],
                masterApiData: [] as any[],
                filteredApiData: [],
                isLoading: false,
                chartOptionsPie: {},
                isLineReady: false,
                isRobotReady: false,
                hoveredRobot: null as Robot | null,
                hoveredLine: null as any,
                robotRefs: {}  as Record < string,
                HTMLElement[] > ,
                loaderStyles: {} as Record < string,
                any >
            }
        },
        computed: {
            worldStyle() {
                return {
                    transform: `
                        translate(${this.translateX}px, ${this.translateY}px)
                        scale(${this.scale})
                    `,
                    transformOrigin: '0 0'
                }
            },
            totalMachine(): number {
                return this.area.length
            },
            totalPass(): number {
                return this.area.reduce((sum, a) => sum + a.pass, 0)
            },
            totalFail(): number {
                return this.area.reduce((sum, a) => sum + a.fail, 0)
            },
            totalYield(): number {
                const total = this.totalPass + this.totalFail
                return total > 0 ? +(this.totalPass / total * 100).toFixed(2) : 0
            }
        },
        methods: {
            clampTranslate() {
                const viewport = this.$refs.viewport as HTMLElement
                const world = this.$refs.world as HTMLElement

                const maxX = 0
                const maxY = 0
                const minX = viewport.clientWidth - world.scrollWidth * this.scale
                const minY = viewport.clientHeight - world.scrollHeight * this.scale

                this.translateX = Math.min(maxX, Math.max(minX, this.translateX))
                this.translateY = Math.min(maxY, Math.max(minY, this.translateY))
            },
            startPan(e: MouseEvent) {
                if ((e.target as HTMLElement).closest('button, input, select')) return
                this.isPanning = true
                this.startX = e.clientX - this.translateX
                this.startY = e.clientY - this.translateY
            },

            onPan(e: MouseEvent) {
                if (!this.isPanning) return
                this.translateX = e.clientX - this.startX
                this.translateY = e.clientY - this.startY
                this.clampTranslate()
            },

            endPan() {
                this.isPanning = false
            },

            onZoom(e: WheelEvent) {
                const zoomSpeed = 0.0008
                const delta = -e.deltaY * zoomSpeed
                const newScale = Math.min(Math.max(this.scale + delta, 0.4), 2.5)

                const rect = (this.$refs.viewport as HTMLElement).getBoundingClientRect()
                const mouseX = e.clientX - rect.left
                const mouseY = e.clientY - rect.top

                this.translateX =
                    mouseX - ((mouseX - this.translateX) * newScale) / this.scale
                this.translateY =
                    mouseY - ((mouseY - this.translateY) * newScale) / this.scale

                this.scale = newScale
                this.clampTranslate()
            },
            autoFitScale() {
                const viewport = this.$refs.viewport as HTMLElement
                const world = this.$refs.world as HTMLElement

                if (!viewport || !world) return

                const scaleX = viewport.clientWidth / world.scrollWidth
                const scaleY = viewport.clientHeight / world.scrollHeight

                this.scale = Math.min(scaleX, scaleY)
                this.translateX = 0
                this.translateY = 0
            },
            resetZoom() {
                this.autoFitScale()
            },
            fullSize() {
                const viewport = this.$refs.viewport as HTMLElement
                const world = this.$refs.world as HTMLElement

                if (!viewport || !world) return

                const scaleX = viewport.clientWidth / world.scrollWidth
                const scaleY = viewport.clientHeight / world.scrollHeight

                this.scale = Math.min(scaleX, scaleY) * 1.2
                this.translateX = 0
                this.translateY = 0
            },
            formatLineName(name: string) {
                if (!name) return ''

                return name.replace(/^PS(\d+)$/i, 'SMT$1')
            },
            getTooltipClass(val: number | null) {
                if (val == null) return 'tooltip-error'

                if (val >= 71 && val < 81) return 'tooltip-warning'
                if (val >= 81 && val < 92) return 'tooltip-accent'
                if (val >= 92) return 'tooltip-success'

                return 'tooltip-error '
            },
            getBadgeClass(val: number | null) {
                if (val === null) return 'badge-ghost'
                if (val >= 92) return 'badge-success'
                if (val >= 81) return 'badge-accent'
                if (val >= 71) return 'badge-warning'
                return 'badge-error'
            },
            statusPingClass(status: string) {
                switch (status) {
                    case 'running':
                        return 'bg-success '
                    case 'pause':
                        return 'bg-warning '
                    case 'NG':
                        return 'bg-red-500/50 '
                    case 'offline':
                        return 'bg-gray-400  '
                    default:
                        return ''
                }
            },
            backgorundStatus(status: string) {
                switch (status) {
                    case 'running':
                        return 'animate-ping-slow'
                    case 'pause':
                        return 'animate-ping-very-slow'
                    case 'NG':
                        return 'animate-ping-realy-slow '
                    case 'offline':
                        return 'animate-ping-NG'
                    default:
                        return ''
                }
            },
            onRobotHover(robot: {
                name: string;type: string;status: string;pass: number;fail: number;yieldRate: number
            } | null, line: {
                name: string;detail: string;passTotal: number;failTotal: number;yieldRateTotal: number;robot: {
                    name: string;type: string;status: string;pass: number;fail: number;yieldRate: number
                } []
            }) {
                this.hoveredRobot = robot
                this.hoveredLine = line
                this.isRobotReady = true
                this.isLineReady = false
            },
            clearRobotHover() {
                // this.hoveredRobot = null
                this.hoveredLine = null
                // this.isRobotReady = true
            },
            onLineHover(line: {
                name: string;detail: string;passTotal: number;failTotal: number;yieldRateTotal: number;robot: {
                    name: string;type: string;status: string;pass: number;fail: number;yieldRate: number
                } []
            }) {
                this.hoveredLine = line
                this.isLineReady = true
                this.isRobotReady = false
            },
            clearLineHover() {
                this.hoveredRobot = null
                // this.isLineReady = false
            },
            setRobotRefs(
                lineName: string,
                index: number,
                el: Element | ComponentPublicInstance | null
            ) {
                if (!el) return

                const element = el instanceof HTMLElement ?
                    el :
                    (el as ComponentPublicInstance).$el as HTMLElement

                if (!this.robotRefs[lineName]) {
                    this.robotRefs[lineName] = []
                }

                this.robotRefs[lineName][index] = element
            },
            getLoaderStyle(lineName: string) {
                const robots = this.robotRefs[lineName]
                if (!robots || robots.length === 0) return {}

                const first = robots[0]
                const last = robots[robots.length - 1]

                // const left = first.offsetLeft + first.offsetWidth / 60
                const right = last.offsetLeft + last.offsetWidth / 1
                const top = first.offsetTop + first.offsetHeight * 0.84

                return {
                    left: `24px`,
                    top: `${top}px`,
                    width: `${Math.max(500, right - 24)}px`
                }
            },

            getLineStatus(line: any): string {
                if (!line.robot || !line.robot.length) return 'offline'

                if (line.robot.some((r: any) => r.status === 'running')) {
                    return 'running'
                }

                if (line.robot.some((r: any) => r.status === 'pause')) {
                    return 'pause'
                }

                if (line.robot.some((r: any) => r.status === 'NG')) {
                    return 'NG'
                }

                return line ?.status ?? 'offline'
            },
            getOutputStatus(line: any): string {
                const output = line.robot.find((r: any) => r.name === 'OUTPUT')
                return output ?.status ?? 'offline'
            },

            isLoaderRunning(line: any): boolean {
                if (!line.robot || !line.robot.length) return false

                return line.robot.some((r: any) => r.status === 'running')
            },

            async fetchMachineList() {
                try {
                    const res = await axios.post(
                        'http://10.176.33.76:5003/api/machine/list', {
                            factory: '',
                            floor: this.filters.floor || '',
                            line: ''
                        }, {
                            headers: {
                                'Content-Type': 'application/json'
                            }
                        }
                    )

                    this.masterApiData = res.data.data

                    this.columns = this.mapApiResponse(this.masterApiData)

                    const filteredStats = this.filterByDate(this.masterApiData)

                    this.area = this.areaApiResponse(filteredStats)

                    this.updateLineStats(filteredStats)


                    this.$nextTick(() => {
                        this.autoFitScale()
                        this.generateChartsFromAPI()
                    })

                } catch (err) {
                    console.error('Error fetch machine list', err)
                }
            },

            areaApiResponse(apiData: any[]) {
                return apiData.map((item: any) => ({
                    factory: item.factory,
                    floor: item.floor || this.selectedFloor || '',
                    line: item.line,
                    pass: item.cntPass,
                    fail: item.cntFail,
                    yieldRate: parseFloat(item.yield),
                    currentTime: item.lastUpdateTime
                }))
            },
            mapApiResponse(apiData: any[]) {
                const COLUMN_COUNT = 3;
                const MAIN_ROW_COUNT = 11;

                const lineMap: Record < string, Line > = {};

                apiData.forEach(item => {
                    if (!lineMap[item.line]) {
                        lineMap[item.line] = {
                            name: item.line,
                            detail: `${item.factory}-${item.floor}`,
                            passTotal: 0,
                            failTotal: 0,
                            yieldRateTotal: 0,
                            robot: []
                        };
                    }

                    lineMap[item.line].robot.push({
                        name: item.code,
                        type: item.currentStep,
                        status: this.mapStatus(item.status),
                        pass: item.cntPass,
                        fail: item.cntFail,
                        yieldRate: parseFloat(item.yield)
                    });

                    lineMap[item.line].passTotal += item.cntPass;
                    lineMap[item.line].failTotal += item.cntFail;
                });

                Object.values(lineMap).forEach(line => {
                    const total = line.passTotal + line.failTotal;
                    line.yieldRateTotal = total > 0 ? +(line.passTotal / total * 100).toFixed(2) : 0;
                });

                const linesArray = Object.values(lineMap);

                const columns: {
                        id: number;lines: Line[]
                    } [] =
                    Array.from({
                        length: COLUMN_COUNT
                    }, (_, i) => ({
                        id: i + 1,
                        lines: []
                    }));

                for (let col = 0; col < COLUMN_COUNT; col++) {
                    for (let row = 0; row < MAIN_ROW_COUNT; row++) {
                        const idx = col * MAIN_ROW_COUNT + row;
                        if (linesArray[idx]) {
                            columns[col].lines.push(linesArray[idx]);
                        }
                    }
                }

                const extraStart = COLUMN_COUNT * MAIN_ROW_COUNT;
                for (let col = 0; col < COLUMN_COUNT; col++) {
                    const extraIdx = extraStart + col;
                    if (linesArray[extraIdx]) {
                        columns[col].lines.push(linesArray[extraIdx]);
                    }
                }

                return columns;
            },
            generateChartsFromAPI() {
                if (!this.columns || this.columns.length === 0) return

                const allLines = this.columns.flatMap(col => col.lines)

                if (allLines.length === 0) return

                const pieData = allLines.map(line => ({
                    name: this.formatLineName(line.name),
                    value: line.yieldRateTotal
                }))
                //  console.log('data :', pieData)*

                const colors = allLines.map((_, i) => {
                    const palette = [
                        '#00d3bb'
                    ]
                    return palette[i % palette.length]
                })

                this.chartOptionsPie = {
                    backgroundColor: 'transparent',
                    title: {
                        text: 'Yield Rate Trend per Line',
                        left: 'left',
                        top: 5,
                        textStyle: {
                            color: '#ffffff',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10,

                        }
                    },
                    tooltip: {
                        trigger: 'item'
                    },

                    series: [{
                        name: 'Yield Rate',
                        type: 'pie',
                        radius: ['30%', '70%'],
                        center: ['50%', '60%'],
                        avoidLabelOverlap: false,
                        itemStyle: {
                            borderRadius: 6,
                            borderColor: '#fff',
                            borderWidth: 1
                        },
                        label: {
                            show: true,
                            formatter: "{b}",
                            color: "#fff",
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10,
                            fontSize: 10
                        },
                        color: colors,
                        data: pieData,
                    }]
                }
            },

            mapStatus(status: string) {
                switch (status) {
                    case 'EXECUTING':
                        return 'running'
                    case 'ERROR':
                        return 'NG'
                    case 'UNKNOW':
                        return 'offline'
                    default:
                        return 'pause'
                }
            },
            isSingleOutput(line: any): boolean {
                return (
                    line.robot &&
                    line.robot.length === 1 &&
                    line.robot[0].name === 'OUTPUT'
                )
            },
            getRobotByName(line: any, name: string) {
                return line.robot.find((r: any) => r.name === name) || null
            },
            filterByDate(data: any[]) {
                if (!this.filters.dateFrom && !this.filters.dateUntil) {
                    return data
                }

                const from = this.filters.dateFrom ?
                    new Date(this.filters.dateFrom).getTime() :
                    null

                const until = this.filters.dateUntil ?
                    new Date(this.filters.dateUntil).getTime() :
                    null

                return data.filter(item => {
                    const itemTime = new Date(item.lastUpdateTime).getTime()

                    if (from && itemTime < from) return false
                    if (until && itemTime > until) return false

                    return true
                })
            },
            applyFilters() {
                if (!this.masterApiData.length) return

                const filteredStats = this.filterByDate(this.masterApiData)

                this.area = this.areaApiResponse(filteredStats)

                this.updateLineStats(filteredStats)
            },
            updateLineStats(filteredData: any[]) {
                const statMap: Record < string, RobotStat > = {}

                filteredData.forEach(item => {
                    const key = `${item.line}-${item.code}`

                    statMap[key] = {
                        pass: item.cntPass,
                        fail: item.cntFail,
                        yield: parseFloat(item.yield),
                        status: this.mapStatus(item.status)
                    }
                })

                this.columns.forEach(col => {
                    col.lines.forEach((line: {
                        robot: any[];name: any
                    }) => {
                        line.robot.forEach(robot => {
                            const key = `${line.name}-${robot.name}`
                            const stat = statMap[key]

                            if (stat) {
                                robot.pass = stat.pass
                                robot.fail = stat.fail
                                robot.yieldRate = stat.yield
                                robot.status = stat.status
                                robot.isActive = true
                            } else {
                                robot.pass = 0
                                robot.fail = 0
                                robot.yieldRate = '0.00'
                                robot.status = '-'
                                robot.isActive = false
                            }
                        })
                    })
                })
            },
            handleShiftChange() {
                if (!this.selectedShift) {
                    this.fetchMachineList()
                    return;
                }

                const baseDateStr = this.filters.dateFrom ? this.filters.dateFrom.split('T')[0] : new Date()
                    .toISOString()
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

                this.filters.dateFrom = startDate
                this.filters.dateUntil = endDate

                this.fetchMachineList()

            },

            // new page
            goToRobotDetail(robot: any, line: any) {
                this.$router.push({
                    path: '/robot',
                    query: {
                        view: 'detail',
                        line: line.name,
                        robot: robot.name
                    }
                })

            },

        },
        mounted() {

            this.$nextTick(() => {
                this.autoFitScale()
            })
            window.addEventListener('resize', this.autoFitScale)

            const today = new Date().toISOString().split('T')[0]
            this.filters.dateFrom = `${today}T00:00`
            this.filters.dateUntil = `${today}T23:59`

            this.fetchMachineList()
        },
        beforeUnmount() {
            window.removeEventListener('resize', this.autoFitScale)
        },
        watch: {
            scale() {
                this.$nextTick(() => {
                    this.$forceUpdate()
                })
            },
            'filters.floor'() {
                this.fetchMachineList()
            },
            'filters.dateFrom'(val) {
                if (this.filters.dateUntil && val > this.filters.dateUntil) {
                    this.filters.dateUntil = val
                }
                this.applyFilters()
            },
            'filters.dateUntil': 'applyFilters'
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

    .stat-title {
        white-space: wrap !important;
    }

    html[data-theme='light'] .stat-title {
        color: color-mix(in oklab, #fff 60%, transparent);
    }

    .stat-value {
        font-size: 0 !important;
        font-weight: 0 !important;
    }

    .overview {
        /* background-color: #15191e; */
        background-image: radial-gradient(rgba(255, 255, 255, 0.171) 1.5px, transparent 0);
        background-size: 20px 20px;
        background-position: -5px -5px
    }

    html[data-theme='light'] .overview {
        /* background-color: #15191e; */
        background-image: radial-gradient(#c9c7c7 1.5px, transparent 0);
        background-size: 20px 20px;
        background-position: -5px -5px
    }

    html[data-theme='light'] .text-light {
        color: #fff;
    }

    html[data-theme='light'] .text-dark {
        color: #15191e;
    }

    html[data-theme='light'] .text-grey {
        color: #6b6e72;
    }

    html[data-theme='light'] .stat:not(:last-child) {
        border-inline-end: var(--border) dashed color-mix(in oklab, #fff 10%, #0000);
        border-block-end: none;
    }

    html[data-theme='light'] .strip {
        color: #eee;
    }

    html[data-theme='light'] .bg-dark {
        background-color: color-mix(in oklab, #15191e 10%, transparent);

    }

    html[data-theme='light'] .tracking-wide {
        color: #fff;
    }

    html[data-theme='light'] .bg-modal {
        background-color: #15191e;
    }


    .md\:input-custom {
        --size: calc(var(--size-field, 0.25rem) * 4);
        font-size: 8px !important;
    }

    .sm\:input-custom {
        --size: calc(var(--size-field, 0.28rem) * 4);
        font-size: 8px !important;
    }

    .md\:select-custom {
        --size: calc(var(--size-field, 0.25rem) * 5);
        font-size: 9px !important;
    }

    .sm\:select-custom {
        --size: calc(var(--size-field, 0.25rem) * 5);
        font-size: 9px !important;
    }


    .radio-buttons-container {
        display: flex;
        align-items: center;
        /* gap: 30px; */
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
        /* padding-left: 30px; */
        margin-bottom: 10px;
        position: relative;
        /* font-size: 12px; */
        color: #fff;
        cursor: pointer;
        transition: all 0.3s cubic-bezier(0.23, 1, 0.320, 1);
    }

    .radio-button__custom {
        position: absolute;
        top: 50%;
        left: 0;
        transform: translateY(-50%);
        /* width: 20px;
        height: 20px; */
        border-radius: 30%;
        border: 2px solid #555;
        transition: all 0.3s cubic-bezier(0.23, 1, 0.320, 1);
    }

    .radio-button__input:checked+.radio-button__label .radio-button__custom {
        transform: translateY(-50%) scale(0.9);
        border: 3px solid #00d3bb;
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

    .blur {
        top: -12px;
        filter: blur(12px);
    }

    .blur-layout {
        top: -12px;
        filter: blur(6px);
    }

    @keyframes ping-slow {
        0% {

            opacity: 1;
        }

        75%,
        100% {

            opacity: 0.9;
        }
    }

    @keyframes ping-very-slow {
        0% {

            opacity: 1;
        }

        100% {

            opacity: 0.5;
        }
    }

    @keyframes ping-realy-slow {
        0% {

            opacity: 1;
        }

        100% {

            opacity: 0.2;
        }
    }

    .animate-ping-slow {
        animation: ping-slow 1s cubic-bezier(0, 0, 0.2, 1) infinite;
    }

    .animate-ping-very-slow {
        animation: ping-very-slow 2s cubic-bezier(0, 0, 0.2, 1) infinite;
    }

    .animate-ping-realy-slow {
        animation: ping-realy-slow 3s cubic-bezier(0, 0, 0.2, 1) infinite;
    }

    .animate-ping-NG {
        opacity: 0.5;
    }

    .loader {
        position: absolute;
        border-radius: 4px;
        height: 8px;
        /* width: 320px; */
        background-color: #aaa;
        animation: move-arrows 0.5s linear infinite;
        -webkit-animation: move-arrows 0.5s linear infinite;
        /* transform-origin: left; */
        background-image: url("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABkAAAAZCAYAAADE6YVjAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAABDZpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDkuMS1jMDAxIDc5LjE0NjI4OTk3NzcsIDIwMjMvMDYvMjUtMjM6NTc6MTQgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIiB4bWxuczpzdFJlZj0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL3NUeXBlL1Jlc291cmNlUmVmIyIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczpkYz0iaHR0cDovL3B1cmwub3JnL2RjL2VsZW1lbnRzLzEuMS8iIHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDo3Y2M3MzIzZC0xZjc3LTQ1MmYtOGFjZS1jY2JkZWIwNTMyZWIiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6OTUzNjQwRkU3RDNBMTFFRTk5QTg4MkQ5REZCNTY1NzAiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6OTUzNjQwRkQ3RDNBMTFFRTk5QTg4MkQ5REZCNTY1NzAiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIDI1LjAgKE1hY2ludG9zaCkiIHhtcDpDcmVhdGVEYXRlPSIyMDIzLTExLTE3VDE2OjU0OjA4KzA4OjAwIiB4bXA6TW9kaWZ5RGF0ZT0iMjAyMy0xMS0yMFQwNTozNDoxNyswODowMCIgeG1wOk1ldGFkYXRhRGF0ZT0iMjAyMy0xMS0yMFQwNTozNDoxNyswODowMCIgZGM6Zm9ybWF0PSJpbWFnZS9wbmciPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDowRDBDQjlDMjdENEIxMUVFOTU2MkVDNUVBMTUxNTUyMyIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDowRDBDQjlDMzdENEIxMUVFOTU2MkVDNUVBMTUxNTUyMyIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/Pj38waoAAAFKSURBVHjanJbbSwJhEMV3pYTQIOgCPogEPQQZRBf6/18zbyiGRD0YRNEFMuwmuZ2BIwTZfuebAz92H2Y8rDtzvk2zLEuoA1BNwuqCURKhwq/7HhgLPftgzWvyDc7BVOg5BkWPiWkCOkLfCjgCqcfEdAeuhN4NsOs1MV2AZ6F/B1S8JjZyTfApGNlUlj0mpg/QomGelsAJr9EmpkcwFJ6mzCdymZguwb1QV+E7cpmY2uBNqLNp2/SaTLmos0Cd7c0h9yjaxPQC+kJdkUYuk0SYtLlGXpN1hmNI1+DGY1JiKIbqH8DA8+KXuWyh1J0wJbJYk/m0rArT1/jvmAiZ7IEtYRhsj149G18D28LfOQwlQiHnrKgLBreMneioVydpLJ6if37IJumU1zx9gTN+F0SZpDy3S4GeGXPs3fMhUV+UoAtk+fUUk0U/AgwAELo+LcAxjVoAAAAASUVORK5CYII=");
        background-size: auto 100%;
    }

    .loader-paused {
        animation-play-state: paused !important;
        -webkit-animation-play-state: paused !important;
        opacity: 0.5;
    }


    @keyframes move-arrows {
        100% {
            background-position: 20px 0;
        }
    }

    @-webkit-keyframes move-arrows {
        100% {
            background-position: 20px 0;
        }
    }
</style>