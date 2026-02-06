<template>
    <div class="team-schedule p-4">
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
        <div class="grid grid-cols-5 gap-4">
            <div class="col-span-1 bg-base-300 rounded-lg font-bold mb-4 p-6 summary-card  ">
                <div class="flex flex-col space-y-24">
                    <DayPilotNavigator :selectMode="viewType" :showMonths="1" :skipMonths="1"
                        @timeRangeSelected="args => startDate = args.day" style="width: 200px; border: none; ">
                    </DayPilotNavigator>

                    <ul class="list bg-base-100 rounded-box shadow-md">
                        <li class="p-4 pb-2 text-xs opacity-60 tracking-wide">Upcoming Schedule</li>
                        <li class="list-row gap-0">
                            <div>
                                <h2 class="text-lg font-semibold text-accent hover:underline cursor-pointer ">
                                    Maintenance Line 1 </h2>
                                <div class="flex flex-col gap-2 mt-2 ml-2">
                                    <div class="text-xs uppercase font-semibold opacity-60 flex gap-2">
                                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor"
                                            class="size-4">
                                            <path fill-rule="evenodd"
                                                d="M4 1.75a.75.75 0 0 1 1.5 0V3h5V1.75a.75.75 0 0 1 1.5 0V3a2 2 0 0 1 2 2v7a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2V1.75ZM4.5 6a1 1 0 0 0-1 1v4.5a1 1 0 0 0 1 1h7a1 1 0 0 0 1-1V7a1 1 0 0 0-1-1h-7Z"
                                                clip-rule="evenodd" />
                                        </svg> 2025-10-11</div>
                                    <div class="text-xs uppercase font-semibold opacity-60 flex gap-2">
                                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor"
                                            class="size-4">
                                            <path fill-rule="evenodd"
                                                d="M1 8a7 7 0 1 1 14 0A7 7 0 0 1 1 8Zm7.75-4.25a.75.75 0 0 0-1.5 0V8c0 .414.336.75.75.75h3.25a.75.75 0 0 0 0-1.5h-2.5v-3.5Z"
                                                clip-rule="evenodd" />
                                        </svg>
                                        09.50 - 11.00</div>
                                </div>
                            </div>
                        </li>
                        <li class="list-row gap-0">
                            <div>
                                <h2 class="text-lg font-semibold text-accent hover:underline cursor-pointer ">
                                    Change Sensor AOI </h2>
                                <div class="flex flex-col gap-2 mt-2 ml-2">
                                    <div class="text-xs uppercase font-semibold opacity-60 flex gap-2">
                                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor"
                                            class="size-4">
                                            <path fill-rule="evenodd"
                                                d="M4 1.75a.75.75 0 0 1 1.5 0V3h5V1.75a.75.75 0 0 1 1.5 0V3a2 2 0 0 1 2 2v7a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2V1.75ZM4.5 6a1 1 0 0 0-1 1v4.5a1 1 0 0 0 1 1h7a1 1 0 0 0 1-1V7a1 1 0 0 0-1-1h-7Z"
                                                clip-rule="evenodd" />
                                        </svg> 2025-10-16</div>
                                    <div class="text-xs uppercase font-semibold opacity-60 flex gap-2">
                                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor"
                                            class="size-4">
                                            <path fill-rule="evenodd"
                                                d="M1 8a7 7 0 1 1 14 0A7 7 0 0 1 1 8Zm7.75-4.25a.75.75 0 0 0-1.5 0V8c0 .414.336.75.75.75h3.25a.75.75 0 0 0 0-1.5h-2.5v-3.5Z"
                                                clip-rule="evenodd" />
                                        </svg>
                                        09.50 - 11.00</div>
                                </div>
                            </div>
                        </li>
                        <li class="list-row gap-0">
                            <div>
                                <h2 class="text-lg font-semibold text-accent hover:underline cursor-pointer ">
                                    Kalibrasi Sensor </h2>
                                <div class="flex flex-col gap-2 mt-2 ml-2">
                                    <div class="text-xs uppercase font-semibold opacity-60 flex gap-2">
                                        <svg xmlns="http://www.w3.org/2000/svg"     viewBox="0 0 16 16" fill="currentColor"
                                            class="size-4">
                                            <path fill-rule="evenodd"
                                                d="M4 1.75a.75.75 0 0 1 1.5 0V3h5V1.75a.75.75 0 0 1 1.5 0V3a2 2 0 0 1 2 2v7a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2V1.75ZM4.5 6a1 1 0 0 0-1 1v4.5a1 1 0 0 0 1 1h7a1 1 0 0 0 1-1V7a1 1 0 0 0-1-1h-7Z"
                                                clip-rule="evenodd" />
                                        </svg> 2025-10-20</div>
                                    <div class="text-xs uppercase font-semibold opacity-60 flex gap-2">
                                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor"
                                            class="size-4">
                                            <path fill-rule="evenodd"
                                                d="M1 8a7 7 0 1 1 14 0A7 7 0 0 1 1 8Zm7.75-4.25a.75.75 0 0 0-1.5 0V8c0 .414.336.75.75.75h3.25a.75.75 0 0 0 0-1.5h-2.5v-3.5Z"
                                                clip-rule="evenodd" />
                                        </svg>
                                        09.50 - 11.00</div>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="col-span-4 flex flex-col gap-4">
                <div class="flex justify-between items-center">
                    <h2 class="text-base-content text-xl font-semibold">Team Schedule Calendar</h2>
                    <button @click="addClick" class="btn btn-accent w-40 place-self-end">+ Add Schedule</button>
                </div>

                <div class="flex flex-wrap bg-base-300 p-4 mb-4 rounded-lg shadow summary-card gap-4 h-full">
                    <div class="buttons">
                        <button @click="viewType='Day'" :class="{ selected: viewType === 'Day' }"><span class="text-white">Day</span></button>
                        <button @click="viewType='Week'" :class="{ selected: viewType === 'Week' }"><span class="text-white">Week</span></button>
                        <button @click="viewType='Month'" :class="{ selected: viewType === 'Month' }"><span class="text-white">Month</span></button>
                    </div>
                    <DayPilotCalendar :viewType="'Day'" :startDate="startDate" :visible="viewType === 'Day'"
                        :events="events" @beforeEventRender="onBeforeEventRender"
                        @timeRangeSelected="onTimeRangeSelected" ref="dayRef">
                        <template #event="{event}">
                            <CalendarEvent :event="event" @edit="onEventEdit" @delete="onEventDelete" />
                        </template>
                    </DayPilotCalendar>
                    <DayPilotCalendar :viewType="'Week'" :startDate="startDate" :visible="viewType === 'Week'"
                        :events="events" :eventBorderRadius="5" :durationBarVisible="false"
                        @beforeEventRender="onBeforeEventRender" @timeRangeSelected="onTimeRangeSelected" ref="weekRef">
                        <template #event="{event}">
                            <CalendarEvent :event="event" @edit="onEventEdit" @delete="onEventDelete" />
                        </template>
                    </DayPilotCalendar>
                    <DayPilotMonth 
                    :startDate="startDate" 
                    :visible="viewType === 'Month'" 
                    :events="events"
                        @beforeEventRender="onBeforeEventRender" @timeRangeSelected="onTimeRangeSelected"
                        ref="monthRef">
                        <template #event="{event}">
                            <CalendarEvent :event="event" :use-header="false" @edit="onEventEdit"
                                @delete="onEventDelete" />
                        </template>
                    </DayPilotMonth>
                </div>
            </div>
        </div>
    </div>
</template>
<script setup>
    import {
        DayPilot,
        DayPilotCalendar,
        DayPilotMonth,
        DayPilotNavigator
    } from '@daypilot/daypilot-lite-vue';
    import {
        ref,
        onMounted
    } from 'vue';
    import CalendarEvent from "@/components/CalendarEvent.vue";

    const events = ref([]);
    const viewType = ref("Week");
    const startDate = ref(DayPilot.Date.today());

    const dayRef = ref(null);
    const weekRef = ref(null);
    const monthRef = ref(null);

    const onTimeRangeSelected = async (args) => {
        const modal = await DayPilot.Modal.prompt("Create a new event:", "Event 1");
        const calendar = args.control;
        calendar.clearSelection();
        if (modal.canceled) {
            return;
        }
        calendar.events.add({
            start: args.start,
            end: args.end,
            id: DayPilot.guid(),
            text: modal.result
        });
    };

    const addClick = async () => {
        const modal = await DayPilot.Modal.prompt("New event name:", "Untitled Event");
        if (modal.canceled) {
            return;
        }

        const now = startDate.value.addHours(10);
        const end = now.addHours(1);
        const newEvent = {
            id: DayPilot.guid(),
            start: now,
            end: end,
            text: modal.result,
            color: "#00d3bb"
        };

        events.value = [...events.value, newEvent];
    };

    const onBeforeEventRender = (args) => {
        args.data.barHidden = true;
        args.data.borderRadius = "0px";
        args.data.backColor = (args.data.color || "#cccccc") + "cc";
        args.data.borderColor = "darker";
    };

    const colors = [{
            name: "Green",
            id: "#00d3bb"
        },
        {
            name: "Blue",
            id: "#51EAEA"
        },
        {
            name: "Orange",
            id: "#FF9A00"
        },
        {
            name: "Yellow",
            id: "#ffff00"
        },
        {
            name: "Gray",
            id: "#cccccc"
        },
    ];

    const onEventEdit = async (event) => {
        const form = [{
                name: "Text",
                id: "text",
                type: "text"
            },
            {
                name: "Color",
                id: "color",
                type: "select",
                options: colors
            },
        ];
        const modal = await DayPilot.Modal.form(form, event.data);
        if (modal.canceled) {
            return;
        }
        event.data.text = modal.result.text;
        event.data.color = modal.result.color;
    };

    const onEventDelete = (event) => {
        const data = event.data;
        events.value = events.value.filter(e => e.id !== data.id);
    }

    const loadEvents = () => {
        const firstDay = DayPilot.Date.today().firstDayOfWeek().addDays(1);
        events.value = [{
                id: 1,
                start: firstDay.addHours(9),
                end: firstDay.addHours(10),
                text: "Event 1",
                color: "#00d3bb"
            },
            {
                id: 2,
                start: firstDay.addDays(1).addHours(11),
                end: firstDay.addDays(1).addHours(12),
                text: "Event 2",
                color: "#51EAEA"
            },
            {
                id: 3,
                start: firstDay.addDays(1).addHours(13),
                end: firstDay.addDays(1).addHours(15),
                text: "Event 3",
                color: "#FF9A00"
            },
            {
                id: 4,
                start: firstDay.addHours(15),
                end: firstDay.addHours(16),
                text: "Event 4",
                color: "#F6FA70"
            },
            {
                id: 5,
                start: firstDay.addHours(11),
                end: firstDay.addHours(14),
                text: "Event 5",
                color: "#cccccc"
            },
        ];
    };
    const showAlert = ref(false)
    onMounted(() => {
        loadEvents();
        showAlert.value = true
        setInterval(() => {
            showAlert.value = false
        }, 10000)
    });
</script>

<style>
    body .calendar_default_event_inner {
        padding: 0px;
    }
    .event-header {
        color: #15191e!important;
    }

    .navigator_default_main {
        background-color: #15191e!important;
        --dp-nav-border-color: #15191e !important;
        --dp-nav-font-family: -apple-system, system-ui, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
        --dp-nav-font-size: 12px;
        --dp-nav-title-color: #fff !important;
        --dp-nav-title-bg-color: #15191e !important;
        --dp-nav-dayheader-color: #fff !important;
        --dp-nav-dayheader-bg-color: #15191e !important;
        --dp-nav-weeknumber-color: #fff !important;
        --dp-nav-weeknumber-bg-color: #15191e !important;
        --dp-nav-day-color: #fff !important;
        --dp-nav-day-bg-color: #15191e !important;
        --dp-nav-dayother-color: grey !important;
        --dp-nav-dayother-bg-color: #15191e !important;
        --dp-nav-weekend-bg-color: #15191e !important;
        --dp-nav-select-bg-color: #15191e !important;
        --dp-nav-text-align: center !important;
        transform: scale(1.3) !important;
        transform-origin: top left !important;
    }

    body .navigator_default_cell {
        height: 40px !important;
        font-size: 9px !important;
        padding: 0px !important;
    }

    body .navigator_default_dayheader {
        font-size: 9px !important;
        height: 30px !important;
        padding: 5px !important;
    }

    body .navigator_default_title {
        font-size: 11px !important;
        padding: 0px 0 !important;
    }

    body .navigator_default_todaybox {
        border: none !important;
        background-color: #00d3bb !important;
    }

    body .calendar_default_main {
        --dp-calendar-border-color: #c0c0c0;
        --dp-calendar-font-family: -apple-system, system-ui, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
        --dp-calendar-font-size: 13px;
        --dp-calendar-header-bg-color: #15191e !important;
        --dp-calendar-header-color: #fff !important;
        --dp-calendar-colheader-padding: 0px;
        --dp-calendar-rowheader-font-size: 16pt;
        --dp-calendar-rowheader-padding: 3px;
        --dp-calendar-cell-bg-color: #15191e !important;
        --dp-calendar-cell-business-bg-color: #15191e !important;
        --dp-calendar-cell-border-color: #ddd;
        --dp-calendar-colheader-horizontal-align: center;
        --dp-calendar-colheader-vertical-align: center;
        --dp-calendar-allday-event-color: #333;
        --dp-calendar-allday-event-border-color: #999;
        --dp-calendar-allday-event-border: 1px solid var(--dp-calendar-allday-event-border-color);
        --dp-calendar-allday-event-border-radius: 0px;
        --dp-calendar-allday-event-bg-top-color: #15191e !important;
        --dp-calendar-allday-event-bg-bottom-color: #15191e !important;
        --dp-calendar-allday-event-background: linear-gradient(to bottom, var(--dp-calendar-allday-event-bg-top-color) 0%, var(--dp-calendar-allday-event-bg-bottom-color) 100%);
        --dp-calendar-allday-event-box-shadow: none;
        --dp-calendar-allday-event-padding: 4px;
        --dp-calendar-allday-event-horizontal-align: flex-start;
        --dp-calendar-event-color: #333;
        --dp-calendar-event-border-color: #999;
        --dp-calendar-event-border: 1px solid var(--dp-calendar-event-border-color);
        --dp-calendar-event-border-radius: 0px;
        --dp-calendar-event-box-shadow: none;
        --dp-calendar-event-bg-top-color: #15191e !important;
        --dp-calendar-event-bg-bottom-color: #15191e !important;
        --dp-calendar-event-background: linear-gradient(to bottom, var(--dp-calendar-event-bg-top-color) 0%, var(--dp-calendar-event-bg-bottom-color) 100%);
        --dp-calendar-event-bar-bg-color: #9dc8e8;
        --dp-calendar-event-bar-color: #1066a8;
        --dp-calendar-event-bar-width: 6px;
        --dp-calendar-event-bar-left: 0px;
        --dp-calendar-event-bar-bottom: 0px;
        --dp-calendar-event-bar-top: 0px;
        --dp-calendar-event-bar-display: block;
        --dp-calendar-event-padding: 2px;
        --dp-calendar-event-padding-left: 8px;
        --dp-calendar-message-bg-color: #ffa216;
        --dp-calendar-message-color: #ffffff;
        --dp-calendar-message-padding: 10px;
        --dp-calendar-message-opacity: 0.9;
        --dp-calendar-selected-event-bg-color: #ddd;
        --dp-calendar-shadow-color: #bbbbbb;
        --dp-calendar-shadow-border-color: #888888;
        --dp-calendar-forbidden-shadow-border-color: #cc0000;
        --dp-calendar-forbidden-shadow-bg-color: #cc4125;
        --dp-calendar-now-indicator-color: red;
        --dp-calendar-scroll-bg-color: #f3f3f3;
    }

    body .month_default_main {
        --dp-month-border-color: #c0c0c0;
        --dp-month-font-family: -apple-system, system-ui, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
        --dp-month-font-size: 13px;
        --dp-month-cell-border-color: #fff;
        --dp-month-cell-bg-color: #15191e !important;
        --dp-month-cell-business-bg-color: #15191e !important;
        --dp-month-event-color: #fff;
        --dp-month-event-border-color: #999;
        --dp-month-event-border: 1px solid var(--dp-month-event-border-color);
        --dp-month-event-bg-top-color: #15191e !important;
        --dp-month-event-bg-bottom-color: #15191e !important;
        --dp-month-event-background: linear-gradient(to bottom, var(--dp-month-event-bg-top-color) 0%, var(--dp-month-event-bg-bottom-color) 100%);
        --dp-month-event-horizontal-align: flex-start;
        --dp-month-event-vertical-align: center;
        --dp-month-event-padding: 2px;
        --dp-month-event-padding-left: 10px;
        --dp-month-event-padding-rtl: 2px 10px 2px 1px;
        --dp-month-event-border-radius: 0px;
        --dp-month-event-box-shadow: none;
        --dp-month-event-bar-top: 1px;
        --dp-month-event-bar-left: 2px;
        --dp-month-event-bar-bottom: 1px;
        --dp-month-event-bar-width: 6px;
        --dp-month-event-bar-color: #1066a8;
        --dp-month-event-bar-display: block;
        --dp-month-header-bg-color: #15191e !important;
        --dp-month-header-color: #fff;
        --dp-month-header-horizontal-align: center;
        --dp-month-header-vertical-align: center;
        --dp-month-header-padding: 0px;
        --dp-month-message-bg-color: #ffa216;
        --dp-month-message-color: #ffffff;
        --dp-month-message-padding: 10px;
        --dp-month-selected-event-bg-color: #ddd;
        --dp-month-shadow-color: #bbbbbb;
        --dp-month-shadow-border-color: #888888;
        color: #fff;
    }

    .buttons {
        position: relative;
        display: flex;
        flex-wrap: wrap;
        border-radius: 0.5rem;
        background-color: #1d232a;
        box-sizing: border-box;
        box-shadow: 0 0 0px 1px rgba(0, 0, 0, 0.06);
        padding: 0.5rem;
        width: 300px;
        font-size: 12px;
        gap: 10px;
    }

    .buttons button {
        flex: 1 1 auto;
        text-align: center;
        display: flex;
        cursor: pointer;
        align-items: center;
        justify-content: center;
        border-radius: 0.5rem;
        border: none;
        padding: .5rem 0;
        color: #fff;
        transition: all .15s ease-in-out;
        background-color: #00d3bb;
        font-weight: 600;
    }

    .fade-enter-active,
    .fade-leave-active {
    transition: opacity 0.6s ease; 
    }

    .fade-enter-from,
    .fade-leave-to {
    opacity: 0; 
    }

    html[data-theme='light'] .bg-base-300 {
      background-color: #15191e;
    }

</style>