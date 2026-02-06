<template>
    <div class="defective font-inter">
        <div class="flex flex-col space-y-4">
            <div class="w-full bg-base-300 p-4 rounded-lg shadow mb-4 summary-card">
                <div class="flex flex-wrap items-center justify-between gap-4">
                    <div class="flex gap-4">
                        <span class="font-semibold">{{$t('Filters')}}:</span>
                        <div class="flex flex-wrap items-center gap-2">
                            <div v-if="activeFilters.includes('floor')">
                                <select v-model="filters.floor" class="select select-bordered select-sm">
                                    <option value="">All Floor</option>
                                    <option v-for="floor in floor" :key="floor">{{ floor }}</option>
                                </select>
                            </div>

                            <div v-if="activeFilters.includes('date')" class="flex items-center gap-2">
                                <input type="datetime-local" v-model="filters.dateFrom"
                                    class="input input-bordered input-sm" />
                                <span class="strip">-</span>
                                <input type="datetime-local" v-model="filters.dateUntil"
                                    class="input input-bordered input-sm" />
                                <button class="btn btn-sm btn-accent">{{$t('Apply')}}</button>
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
                            <input name="radio-group" id="radio2" class="radio-button__input" type="radio"
                                value="middle" v-model="selectedShift">
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
        </div>
    </div>
</template>

<script lang="ts">
    export default {
        data() {
            return {
                filters: {
                    floor: '',
                    dateFrom: '',
                    dateUntil: '',
                    line: '2F17'
                },
                selectedShift: '',
                selectedFloor: '',
                activeFilters: ['date', 'floor'],
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
                floor: ['1F', '2F', '3F'],
                lines: [] as string[],
            }
        },
        methods: {
            handleShiftChange() {
                if (!this.selectedShift) {
                    // this.fetchAllData();
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
            },
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

    html[data-theme='light'] .font-semibold {
        color: #eee;
    }

    html[data-theme='light'] .strip {
        color: #eee;
    }
</style>