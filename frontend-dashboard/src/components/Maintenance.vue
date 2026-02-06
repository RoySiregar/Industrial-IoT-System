<template>
    <div class="maintenance p-4 font-inter">
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
        <div class="w-full bg-base-300 p-4 rounded-lg shadow mb-4 summary-card">
            <div class="flex flex-wrap items-center justify-between gap-4">
                <div class="flex gap-4">
                    <span class="font-semibold">{{$t('Filters')}}:</span>
                    <div class="flex flex-wrap items-center gap-2">
                        <div v-if="activeFilters.includes('floor')">
                            <select v-model="filters.floor" class="select select-bordered select-sm">
                                <option disabled value="">{{$t('Choose Floor')}}</option>
                                <option v-for="floor in floor" :key="floor">{{ floor }}</option>
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
        <div class="grid grid-flow-col grid-rows-3 gap-6">
            <div class="col-span-2 row-span-1 p-4 bg-base-300 rounded-lg shadow summary-card">
                <h2 class="mb-5 text-lg font-semibold">Device Information</h2>
                <div class="tabs tabs-lift">
                    <label class="tab">
                        <input type="radio" name="tabs1" checked />
                        Equipment Information History
                    </label>
                    <div class="tab-content bg-base-100 border-base-300 p-4 ">
                        <div class="overflow-x-auto">
                            <table class="table table-zebra">
                                <!-- head -->
                                <thead>
                                    <tr>
                                        <th>Equipment ID</th>
                                        <th>Device name</th>
                                        <th>Location</th>
                                        <th>Status</th>
                                        <th>Updated</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <!-- row 1 -->
                                    <tr>
                                        <th>EQP-001</th>
                                        <td>Conveyor Belt</td>
                                        <td>Line 1</td>
                                        <td>Active</td>
                                        <td class="flex gap-2 items-center">2023-05-10 <div
                                                class="tooltip tooltip-right" data-tip="Replaced major spare parts">
                                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20"
                                                    fill="currentColor" class="size-4">
                                                    <path fill-rule="evenodd"
                                                        d="M18 10a8 8 0 1 1-16 0 8 8 0 0 1 16 0Zm-7-4a1 1 0 1 1-2 0 1 1 0 0 1 2 0ZM9 9a.75.75 0 0 0 0 1.5h.253a.25.25 0 0 1 .244.304l-.459 2.066A1.75 1.75 0 0 0 10.747 15H11a.75.75 0 0 0 0-1.5h-.253a.25.25 0 0 1-.244-.304l.459-2.066A1.75 1.75 0 0 0 9.253 9H9Z"
                                                        clip-rule="evenodd" />
                                                </svg>

                                            </div>
                                        </td>
                                    </tr>
                                    <!-- row 2 -->
                                    <tr>
                                        <th>EQP-002</th>
                                        <td>Press Machine</td>
                                        <td>Line 2</td>
                                        <td>Active</td>
                                        <td class="flex gap-2 items-center">2025-10-10
                                            <div class="tooltip tooltip-right" data-tip="Replaced major spare parts">
                                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20"
                                                    fill="currentColor" class="size-4">
                                                    <path fill-rule="evenodd"
                                                        d="M18 10a8 8 0 1 1-16 0 8 8 0 0 1 16 0Zm-7-4a1 1 0 1 1-2 0 1 1 0 0 1 2 0ZM9 9a.75.75 0 0 0 0 1.5h.253a.25.25 0 0 1 .244.304l-.459 2.066A1.75 1.75 0 0 0 10.747 15H11a.75.75 0 0 0 0-1.5h-.253a.25.25 0 0 1-.244-.304l.459-2.066A1.75 1.75 0 0 0 9.253 9H9Z"
                                                        clip-rule="evenodd" />
                                                </svg>

                                            </div>
                                        </td>
                                    </tr>
                                    <!-- row 3 -->
                                    <tr>
                                        <th>EQP-003</th>
                                        <td>Injection Molding Machine</td>
                                        <td>Line 3</td>
                                        <td>Active</td>
                                        <td class="flex gap-2 items-center">2024-12-10
                                            <div class="tooltip tooltip-right" data-tip="Replaced major spare parts">
                                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20"
                                                    fill="currentColor" class="size-4">
                                                    <path fill-rule="evenodd"
                                                        d="M18 10a8 8 0 1 1-16 0 8 8 0 0 1 16 0Zm-7-4a1 1 0 1 1-2 0 1 1 0 0 1 2 0ZM9 9a.75.75 0 0 0 0 1.5h.253a.25.25 0 0 1 .244.304l-.459 2.066A1.75 1.75 0 0 0 10.747 15H11a.75.75 0 0 0 0-1.5h-.253a.25.25 0 0 1-.244-.304l.459-2.066A1.75 1.75 0 0 0 9.253 9H9Z"
                                                        clip-rule="evenodd" />
                                                </svg>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="flex justify-center items-center">
                            <div class="pagination flex gap-2">
                                <div class="tab-group">
                                    <input type="radio" id="tab1" name="tabDevice" checked>
                                    <label for="tab1">1</label>
                                </div>
                                <div class="tab-group">
                                    <input type="radio" id="tab2" name="tabDevice">
                                    <label for="tab2">2</label>
                                </div>
                                <div class="tab-group">
                                    <input type="radio" id="tabAll" name="tabDevice">
                                    <label for="tabAll">All</label>
                                </div>
                            </div>

                        </div>
                    </div>
                    <label class="tab">
                        <input type="radio" name="tabs1" />
                        Component Category Maintenance
                    </label>
                    <div class="tab-content bg-base-100 border-base-300 p-6">
                        <div class="overflow-x-auto">
                            <table class="table table-zebra">
                                <!-- head -->
                                <thead>
                                    <tr>
                                        <th>Category ID</th>
                                        <th>Category Name</th>
                                        <th>Location</th>
                                        <th>Description</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th>CAT-001</th>
                                        <td>Motor</td>
                                        <td>LINE-1</td>
                                        <td>Penggerak conveyor dan aktuator mekanik</td>
                                    </tr>
                                    <tr>
                                        <th>CAT-002</th>
                                        <td>Sensor</td>
                                        <td>LINE-1</td>
                                        <td>Sensor posisi dan limit switch</td>
                                    </tr>
                                    <tr>
                                        <th>CAT-003</th>
                                        <td>Valve</td>
                                        <td>LINE-2</td>
                                        <td>Katup kontrol aliran cat dan udara</td>
                                    </tr>
                                    <tr>
                                        <th>CAT-004</th>
                                        <td>Compressor</td>
                                        <td>LINE-2</td>
                                        <td>Kompresor untuk sistem spray painting</td>
                                    </tr>
                                    <tr>
                                        <th>CAT-005</th>
                                        <td>Heater</td>
                                        <td>LINE-3</td>
                                        <td>Elemen pemanas untuk lelehan plastik</td>
                                    </tr>
                                    <tr>
                                        <th>CAT-006</th>
                                        <td>PLC</td>
                                        <td>LINE-3</td>
                                        <td>Kontrol otomatis mesin injection molding</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="flex justify-center items-center">
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
                    </div>
                    <label class="tab">
                        <input type="radio" name="tabs1" />
                        Disable Line
                    </label>
                    <div class="tab-content bg-base-100 border-base-300 p-6">
                        <div class="overflow-x-auto">
                            <table class="table table-zebra">
                                <!-- head -->
                                <thead>
                                    <tr>
                                        <th>Line ID</th>
                                        <th>Line Name</th>
                                        <th>Status</th>
                                        <th>Maintenance Updated</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th>LINE-01</th>
                                        <td>Line 1</td>
                                        <td>Active</td>
                                        <td>2024-12-10</td>
                                    </tr>
                                    <tr>
                                        <th>LINE-02</th>
                                        <td>Line 2</td>
                                        <td>Nonactive</td>
                                        <td>2025-10-14</td>
                                    </tr>
                                    <tr>
                                        <th>LINE-03</th>
                                        <td>Line 3</td>
                                        <td>Active</td>
                                        <td>2025-10-16</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="flex justify-center items-center">
                            <div class="pagination flex gap-2">
                                <div class="tab-group">
                                    <input type="radio" id="tab5" name="tabDisable" checked>
                                    <label for="tab5">1</label>
                                </div>
                                <div class="tab-group">
                                    <input type="radio" id="tab6" name="tabDisable">
                                    <label for="tab6">2</label>
                                </div>
                                <div class="tab-group">
                                    <input type="radio" id="tabAll" name="tabDisable">
                                    <label for="tabAll">All</label>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-span-2 row-span-1 p-4 bg-base-300 rounded-lg shadow summary-card">
                <h2 class="mb-5 text-lg font-semibold">Maintenance Content</h2>
                <div class="tabs tabs-lift">
                    <label class="tab">
                        <input type="radio" name="tabs2" checked />
                        Maintenance Content and Standar
                    </label>
                    <div class="tab-content bg-base-100 border-base-300 p-4 ">
                        <div class="overflow-x-auto">
                            <table class="table table-zebra">
                                <!-- head -->
                                <thead>
                                    <tr>
                                        <th>Maintenance ID</th>
                                        <th>Task Name</th>
                                        <th>Implementation Standards</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th>MTN-002</th>
                                        <td>Kalibrasi Sensor</td>
                                        <td>Kalibrasi ulang setiap 3 bulan dengan alat standar</td>
                                    </tr>
                                    <tr>
                                        <th>MTN-003</th>
                                        <td>Inspeksi Gearbox</td>
                                        <td>Periksa pelumasan, celah roda gigi, dan kondisi fisik setiap 6 bulan</td>
                                    </tr>
                                    <tr>
                                        <th>MTN-004</th>
                                        <td>Pembersihan Panel Listrik</td>
                                        <td>Bersihkan debu dan cek koneksi terminal setiap 2 bulan</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="flex justify-center items-center">
                            <div class="pagination flex gap-2">
                                <div class="tab-group">
                                    <input type="radio" id="tab7" name="tabContent" checked>
                                    <label for="tab7">1</label>
                                </div>
                                <div class="tab-group">
                                    <input type="radio" id="tab8" name="tabContent">
                                    <label for="tab8">2</label>
                                </div>
                                <div class="tab-group">
                                    <input type="radio" id="tabAll" name="tabContent">
                                    <label for="tabAll">All</label>
                                </div>
                            </div>

                        </div>
                    </div>
                    <label class="tab">
                        <input type="radio" name="tabs2" />
                        Annual Planned Maintenance
                    </label>
                    <div class="tab-content bg-base-100 border-base-300 p-6">
                        <div class="overflow-x-auto">
                            <table class="table table-zebra">
                                <!-- head -->
                                <thead>
                                    <tr>
                                        <th>Plan ID</th>
                                        <th>Equipment ID</th>
                                        <th>Year</th>
                                        <th>Start Schedule</th>
                                        <th>Finish Schedule</th>
                                        <th>Note</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th>PLAN-001</th>
                                        <td>EQP-001</td>
                                        <td>2025</td>
                                        <td>2025-11-01</td>
                                        <td>2025-11-05</td>
                                        <td>Shutdown tahunan untuk pengecekan penuh</td>
                                    </tr>
                                    <tr>
                                        <th>PLAN-002</th>
                                        <td>EQP-002</td>
                                        <td>2025</td>
                                        <td>2025-07-15</td>
                                        <td>2025-07-18</td>
                                        <td>Kalibrasi dan penggantian sensor utama</td>
                                    </tr>
                                    <tr>
                                        <th>PLAN-003</th>
                                        <td>EQP-003</td>
                                        <td>2025</td>
                                        <td>2025-10-01</td>
                                        <td>2025-10-03</td>
                                        <td>Perawatan sistem pendingin mesin</td>
                                    </tr>

                                </tbody>
                            </table>
                        </div>
                        <div class="flex justify-center items-center">
                            <div class="pagination flex gap-2">
                                <div class="tab-group">
                                    <input type="radio" id="tab9" name="tabAnnual" checked>
                                    <label for="tab9">1</label>
                                </div>
                                <div class="tab-group">
                                    <input type="radio" id="tab10" name="tabAnnual">
                                    <label for="tab10">2</label>
                                </div>
                                <div class="tab-group">
                                    <input type="radio" id="tabAll" name="tabAnnual">
                                    <label for="tabAll">All</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <label class="tab">
                        <input type="radio" name="tabs2" />
                        Frequency Setting
                    </label>
                    <div class="tab-content bg-base-100 border-base-300 p-6">
                        <div class="overflow-x-auto">
                            <table class="table table-zebra">
                                <!-- head -->
                                <thead>
                                    <tr>
                                        <th>Equipment ID</th>
                                        <th>Device Name</th>
                                        <th>Maintenance Type</th>
                                        <th>Interval (Day)</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th>EQP-001</th>
                                        <td>Conveyor Belt</td>
                                        <td>Preventive</td>
                                        <td>30</td>
                                    </tr>
                                    <tr>
                                        <th>EQP-002</th>
                                        <td>Mesin Press</td>
                                        <td>Predictive</td>
                                        <td>15</td>
                                    </tr>
                                    <tr>
                                        <th>EQP-003</th>
                                        <td>Injection Molding</td>
                                        <td>Preventive</td>
                                        <td>60</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="flex justify-center items-center">
                            <div class="pagination flex gap-2">
                                <div class="tab-group">
                                    <input type="radio" id="tab11" name="tabFrequency" checked>
                                    <label for="tab11">1</label>
                                </div>
                                <div class="tab-group">
                                    <input type="radio" id="tab12" name="tabFrequency">
                                    <label for="tab12">2</label>
                                </div>
                                <div class="tab-group">
                                    <input type="radio" id="tabAll" name="tabFrequency">
                                    <label for="tabAll">All</label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-span-2 row-span-1 p-4 bg-base-300 rounded-lg shadow summary-card">
                <h2 class="mb-5 text-lg font-semibold">Maintenance History</h2>
                <div class="tabs tabs-lift">
                    <label class="tab">
                        <input type="radio" name="tabs3" checked />
                        Maintenance Tasks Maintenance
                    </label>
                    <div class="tab-content bg-base-100 border-base-300 p-4 ">
                        <div class="overflow-x-auto">
                            <table class="table table-zebra">
                                <!-- head -->
                                <thead>
                                    <tr>
                                        <th>Task Id</th>
                                        <th>Equipment ID</th>
                                        <th>Task Name</th>
                                        <th>Status</th>
                                        <th>Start Schedule</th>
                                        <th>Finish Schedule</th>
                                        <th>PIC</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th>TASK-001</th>
                                        <td>EQP-001</td>
                                        <td>Change Sensor</td>
                                        <td>
                                            <div class="badge badge-success">Finished </div>
                                        </td>
                                        <td>2025-09-01</td>
                                        <td>2025-09-02</td>
                                        <td>Roy</td>
                                    </tr>
                                    <tr>
                                        <th>TASK-002</th>
                                        <td>EQP-002</td>
                                        <td>Kalibrasi Sensor Suhu</td>
                                        <td>
                                            <div class="badge badge-warning">Inproses</div>
                                        </td>
                                        <td>2025-10-10</td>
                                        <td>-</td>
                                        <td>Roy</td>
                                    </tr>
                                    <tr>
                                        <th>TASK-003</th>
                                        <td>EQP-003</td>
                                        <td>Inspeksi Sistem Hidrolik</td>
                                        <td>
                                            <div class="badge badge-neutral">Not Started Yet</div>
                                        </td>
                                        <td>2025-10-20</td>
                                        <td>-</td>
                                        <td>Roy</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="flex justify-center items-center">
                            <div class="pagination flex gap-2">
                                <div class="tab-group">
                                    <input type="radio" id="tab13" name="tabMaintenance" checked>
                                    <label for="tab13">1</label>
                                </div>
                                <div class="tab-group">
                                    <input type="radio" id="tab14" name="tabMaintenance">
                                    <label for="tab14">2</label>
                                </div>
                                <div class="tab-group">
                                    <input type="radio" id="tabAll" name="tabMaintenance">
                                    <label for="tabAll">All</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <label class="tab">
                        <input type="radio" name="tabs3" />
                        Maintenance Status Inquiry
                    </label>
                    <div class="tab-content bg-base-100 border-base-300 p-6">
                        <div class="overflow-x-auto">
                            <table class="table table-zebra">
                                <!-- head -->
                                <thead>
                                    <tr>
                                        <th>Inquiry ID</th>
                                        <th>Task ID</th>
                                        <th>Equipment ID</th>
                                        <th>Status</th>
                                        <th>Last Update</th>
                                        <th>Note</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th>INQ-001</th>
                                        <td>TASK-001</td>
                                        <td>EQP-001</td>
                                        <td>Selesai</td>
                                        <td>2025-09-02</td>
                                        <td>Penggantian berhasil, tidak ditemukan kesalahan</td>
                                    </tr>
                                    <tr>
                                        <th>INQ-002</th>
                                        <td>TASK-002</td>
                                        <td>EQP-002</td>
                                        <td>Dalam Proses</td>
                                        <td>2025-10-12</td>
                                        <td>Menunggu hasil kalibrasi dari QC</td>
                                    </tr>
                                    <tr>
                                        <th>INQ-003</th>
                                        <td>TASK-003</td>
                                        <td>EQP-003</td>
                                        <td>Belum Mulai</td>
                                        <td>-</td>
                                        <td>Menunggu waktu idle dari produksi</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="flex justify-center items-center">
                            <div class="pagination flex gap-2">
                                <div class="tab-group">
                                    <input type="radio" id="tab15" name="tabInquiry" checked>
                                    <label for="tab15">1</label>
                                </div>
                                <div class="tab-group">
                                    <input type="radio" id="tab16" name="tabInquiry">
                                    <label for="tab16">2</label>
                                </div>
                                <div class="tab-group">
                                    <input type="radio" id="tabAll" name="tabInquiry">
                                    <label for="tabAll">All</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <label class="tab">
                        <input type="radio" name="tabs3" />
                        Permission Maintenance
                    </label>
                    <div class="tab-content bg-base-100 border-base-300 p-6">

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
                showDropdown: false,
                selectedShift: '',
                filters: {
                    floor: '',
                    dateFrom: '',
                    dateUntil: '',
                },
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
                floor: ['Floor 1', 'Floor 2', 'Floor 3'],
                showAlert : false
            }
        }, 
        mounted() {
            this.showAlert = true
            setInterval(() => {
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

    .fade-enter-active,
    .fade-leave-active {
        transition: opacity 0.6s ease-in-out;
    }

    .fade-enter-from,
    .fade-leave-to {
        opacity: 0;
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

    html[data-theme='light'] .strip {
        color: #eee;
    }

    html[data-theme='light'] .select {
        background-color: #eee;
    }

    html[data-theme='light'] .input {
        background-color: #eee;
    }

    html[data-theme='light'] .tab:not(:checked, label:has(:checked), :hover, .tab-active, [aria-selected="true"]) {
        color: #eee;
    }

</style>