<template>
    <div class="uph-upd">
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
        <div class="grid grid-cols-[18rem_1fr] gap-4">

            <!-- Sidebar -->
            <div class="max-w-[18rem] max-h-[32rem] overflow-y-auto bg-base-300 p-4 rounded-lg shadow summary-card ">
                <button @click="selectedLine = '377_TestLine'; selectedContent = 'line'" class="font-bold mb-4 "><span
                        class="text-base-content hover:underline">377_TestLine</span></button>
                <ul class="menu menu-xs bg-base-300 rounded-box max-w-xs w-full">
                    <li>
                        <details open>
                            <summary>
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor"
                                    class="size-4">
                                    <path fill-rule="evenodd"
                                        d="M2 3.75A.75.75 0 0 1 2.75 3h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 3.75ZM2 8a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 8Zm0 4.25a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                                        clip-rule="evenodd" />
                                </svg>
                                <span class="cursor-pointer hover:underline"
                                    @click.stop="selectedStation = 'R1'; selectedContent = 'station'">R1</span>
                            </summary>
                            <ul>
                                <li>
                                    <details open>
                                        <summary>
                                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                fill="currentColor" class="size-4">
                                                <path fill-rule="evenodd"
                                                    d="M2 4.75A.75.75 0 0 1 2.75 4h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 4.75Zm0 6.5a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                                                    clip-rule="evenodd" />
                                            </svg>
                                            <span class="cursor-pointer hover:underline"
                                                @click.stop="selectedStation = 'R1'; selectedGroup = 'Bootup'; selectedContent = 'group'">
                                                Bootup
                                            </span>

                                        </summary>
                                        <ul>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>
                                                    <button class="flex items-center gap-2"
                                                        @click.stop="selectedStation = 'R1'; selectedGroup = 'Bootup'; selectedProduction = '377_R1#1';  selectedContent = 'production'">
                                                        <span class=" text-white hover:underline">377_R1#1</span>
                                                    </button>
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    <button class="flex items-center gap-2 text-white hover:underline"
                                                        @click.stop="selectedStation = 'R1'; selectedGroup = 'Bootup'; selectedProduction = '377_R1#3(3)';  selectedContent = 'production'">377_R1#3(3)</button>
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>
                                                    <button class="flex items-center gap-2 text-white hover:underline"
                                                        @click.stop="selectedStation = 'R1'; selectedGroup = 'Bootup'; selectedProduction = '377_R1#11(11)';  selectedContent = 'production'">377_R1#11(11)</button>
                                                </a>
                                            </li>
                                        </ul>
                                    </details>
                                    <details open>
                                        <summary>
                                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                fill="currentColor" class="size-4">
                                                <path fill-rule="evenodd"
                                                    d="M2 4.75A.75.75 0 0 1 2.75 4h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 4.75Zm0 6.5a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                                                    clip-rule="evenodd" />
                                            </svg>

                                            <a class="block hover:underline">WiFi+BT</a>
                                        </summary>
                                        <ul>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    <button
                                                        class="flex items-center gap-2 text-white hover:underline">377_R1#2(2)</button>
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    <button
                                                        class="flex items-center gap-2 text-white hover:underline">377_R1#4(4)</button>
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    <button
                                                        class="flex items-center gap-2 text-white hover:underline">377_R1#6(6)</button>
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    <button
                                                        class="flex items-center gap-2 text-white hover:underline">377_R1#10(10)</button>
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    <button
                                                        class="flex items-center gap-2 text-white hover:underline">377_R1#12(12)</button>
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>
                                                    <button
                                                        class="flex items-center gap-2 text-white hover:underline">377_R1#14(14)</button>
                                                </a>
                                            </li>
                                        </ul>
                                    </details>
                                </li>
                            </ul>
                        </details>
                    </li>
                    <li>
                        <details>
                            <summary>
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor"
                                    class="size-4">
                                    <path fill-rule="evenodd"
                                        d="M2 3.75A.75.75 0 0 1 2.75 3h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 3.75ZM2 8a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 8Zm0 4.25a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                                        clip-rule="evenodd" />
                                </svg>
                                <span class="cursor-pointer hover:underline"
                                    @click.stop="selectedStation = 'R2'; selectedContent = 'station'">R2</span>
                            </summary>
                            <ul>
                                <li>
                                    <details>
                                        <summary>
                                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                fill="currentColor" class="size-4">
                                                <path fill-rule="evenodd"
                                                    d="M2 4.75A.75.75 0 0 1 2.75 4h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 4.75Zm0 6.5a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                                                    clip-rule="evenodd" />
                                            </svg>
                                            Bootup
                                        </summary>
                                        <ul>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#1(1)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#3(3)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#11(11)
                                                </a>
                                            </li>
                                        </ul>
                                    </details>
                                    <details>
                                        <summary>
                                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                fill="currentColor" class="size-4">
                                                <path fill-rule="evenodd"
                                                    d="M2 4.75A.75.75 0 0 1 2.75 4h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 4.75Zm0 6.5a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                                                    clip-rule="evenodd" />
                                            </svg>

                                            WiFi+BT
                                        </summary>
                                        <ul>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#2(2)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#4(4)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#6(6)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#10(10)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#12(12)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#14(14)
                                                </a>
                                            </li>
                                        </ul>
                                    </details>
                                </li>
                            </ul>
                        </details>
                    </li>
                    <li>
                        <details>
                            <summary>
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor"
                                    class="size-4">
                                    <path fill-rule="evenodd"
                                        d="M2 3.75A.75.75 0 0 1 2.75 3h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 3.75ZM2 8a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 8Zm0 4.25a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                                        clip-rule="evenodd" />
                                </svg>
                                R3
                            </summary>
                            <ul>
                                <li>
                                    <details>
                                        <summary>
                                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                fill="currentColor" class="size-4">
                                                <path fill-rule="evenodd"
                                                    d="M2 4.75A.75.75 0 0 1 2.75 4h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 4.75Zm0 6.5a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                                                    clip-rule="evenodd" />
                                            </svg>

                                            Bootup
                                        </summary>
                                        <ul>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#1(1)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#3(3)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#11(11)
                                                </a>
                                            </li>
                                        </ul>
                                    </details>
                                    <details>
                                        <summary>
                                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                fill="currentColor" class="size-4">
                                                <path fill-rule="evenodd"
                                                    d="M2 4.75A.75.75 0 0 1 2.75 4h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 4.75Zm0 6.5a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                                                    clip-rule="evenodd" />
                                            </svg>

                                            WiFi+BT
                                        </summary>
                                        <ul>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#2(2)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#4(4)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#6(6)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#10(10)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#12(12)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#14(14)
                                                </a>
                                            </li>
                                        </ul>
                                    </details>
                                </li>
                            </ul>
                        </details>
                    </li>
                    <li>
                        <details>
                            <summary>
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor"
                                    class="size-4">
                                    <path fill-rule="evenodd"
                                        d="M2 3.75A.75.75 0 0 1 2.75 3h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 3.75ZM2 8a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 8Zm0 4.25a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                                        clip-rule="evenodd" />
                                </svg>
                                R4
                            </summary>
                            <ul>
                                <li>
                                    <details>
                                        <summary>
                                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                fill="currentColor" class="size-4">
                                                <path fill-rule="evenodd"
                                                    d="M2 4.75A.75.75 0 0 1 2.75 4h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 4.75Zm0 6.5a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                                                    clip-rule="evenodd" />
                                            </svg>

                                            Bootup
                                        </summary>
                                        <ul>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#1(1)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#3(3)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#11(11)
                                                </a>
                                            </li>
                                        </ul>
                                    </details>
                                    <details>
                                        <summary>
                                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                fill="currentColor" class="size-4">
                                                <path fill-rule="evenodd"
                                                    d="M2 4.75A.75.75 0 0 1 2.75 4h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 4.75Zm0 6.5a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                                                    clip-rule="evenodd" />
                                            </svg>

                                            WiFi+BT
                                        </summary>
                                        <ul>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#2(2)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#4(4)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#6(6)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#10(10)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#12(12)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#14(14)
                                                </a>
                                            </li>
                                        </ul>
                                    </details>
                                </li>
                            </ul>
                        </details>
                    </li>
                    <li>
                        <details>
                            <summary>
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor"
                                    class="size-4">
                                    <path fill-rule="evenodd"
                                        d="M2 3.75A.75.75 0 0 1 2.75 3h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 3.75ZM2 8a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 8Zm0 4.25a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                                        clip-rule="evenodd" />
                                </svg>
                                R5
                            </summary>
                            <ul>
                                <li>
                                    <details>
                                        <summary>
                                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                fill="currentColor" class="size-4">
                                                <path fill-rule="evenodd"
                                                    d="M2 4.75A.75.75 0 0 1 2.75 4h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 4.75Zm0 6.5a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                                                    clip-rule="evenodd" />
                                            </svg>
                                            Bootup
                                        </summary>
                                        <ul>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#1(1)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#3(3)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#11(11)
                                                </a>
                                            </li>
                                        </ul>
                                    </details>
                                    <details>
                                        <summary>
                                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                fill="currentColor" class="size-4">
                                                <path fill-rule="evenodd"
                                                    d="M2 4.75A.75.75 0 0 1 2.75 4h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 4.75Zm0 6.5a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                                                    clip-rule="evenodd" />
                                            </svg>

                                            WiFi+BT
                                        </summary>
                                        <ul>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#2(2)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#4(4)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#6(6)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>
                                                    377_R1#10(10)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#12(12)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#14(14)
                                                </a>
                                            </li>
                                        </ul>
                                    </details>
                                </li>
                            </ul>
                        </details>
                    </li>
                    <li>
                        <details>
                            <summary>
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor"
                                    class="size-4">
                                    <path fill-rule="evenodd"
                                        d="M2 3.75A.75.75 0 0 1 2.75 3h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 3.75ZM2 8a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 8Zm0 4.25a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                                        clip-rule="evenodd" />
                                </svg>
                                R6
                            </summary>
                            <ul>
                                <li>
                                    <details>
                                        <summary>
                                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                fill="currentColor" class="size-4">
                                                <path fill-rule="evenodd"
                                                    d="M2 4.75A.75.75 0 0 1 2.75 4h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 4.75Zm0 6.5a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                                                    clip-rule="evenodd" />
                                            </svg>
                                            Bootup
                                        </summary>
                                        <ul>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#1(1)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#3(3)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#11(11)
                                                </a>
                                            </li>
                                        </ul>
                                    </details>
                                    <details>
                                        <summary>
                                            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                fill="currentColor" class="size-4">
                                                <path fill-rule="evenodd"
                                                    d="M2 4.75A.75.75 0 0 1 2.75 4h10.5a.75.75 0 0 1 0 1.5H2.75A.75.75 0 0 1 2 4.75Zm0 6.5a.75.75 0 0 1 .75-.75h10.5a.75.75 0 0 1 0 1.5H2.75a.75.75 0 0 1-.75-.75Z"
                                                    clip-rule="evenodd" />
                                            </svg>

                                            WiFi+BT
                                        </summary>
                                        <ul>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#2(2)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#4(4)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#6(6)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#10(10)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#12(12)
                                                </a>
                                            </li>
                                            <li>
                                                <a>
                                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                                                        fill="currentColor" class="size-4">
                                                        <path
                                                            d="M3.75 7.25a.75.75 0 0 0 0 1.5h8.5a.75.75 0 0 0 0-1.5h-8.5Z" />
                                                    </svg>

                                                    377_R1#14(14)
                                                </a>
                                            </li>
                                        </ul>
                                    </details>
                                </li>
                            </ul>
                        </details>
                    </li>
                </ul>
            </div>

            <!-- Content Wrapper -->
            <div class="flex flex-col space-y-4">
                <!-- Horizontal Filter Navbar -->
                <div class="w-full bg-base-300 p-4 rounded-lg shadow mb-4">
                    <div class="flex flex-wrap items-center gap-4">
                        <!-- Label -->
                        <span class="font-semibold">Filters:</span>

                        <!-- Toggle Dropdown -->
                        <div class="relative">
                            <button @click="toggleDropdown" class="btn btn-sm btn-accent">
                                All Filters
                            </button>

                            <!-- Dropdown Checklist -->
                            <div v-if="showDropdown" id="checklist" class="mb-4">
                                <div v-for="filter in availableFilters" :key="filter.value" class="flex items-center">
                                    <input type="checkbox" :id="filter.value" :value="filter.value"
                                        v-model="activeFilters" />
                                    <label :for="filter.value" class="ml-2">{{ filter.label }}</label>
                                </div>
                            </div>
                        </div>
                        <!-- Filters -->
                        <div class="flex flex-wrap items-center gap-2">
                            <div v-if="activeFilters.includes('site')">
                                <select v-model="filters.site" class="select select-bordered select-sm">
                                    <option disabled value="">Choose Site</option>
                                    <option v-for="site in sites" :key="site">{{ site }}</option>
                                </select>
                            </div>

                            <div v-if="activeFilters.includes('bu')">
                                <select v-model="filters.bu" class="select select-bordered select-sm">
                                    <option disabled value="">Choose BU</option>
                                    <option v-for="bu in bus" :key="bu">{{ bu }}</option>
                                </select>
                            </div>
                            <div v-if="activeFilters.includes('factory')">
                                <select v-model="filters.factory" class="select select-bordered select-sm">
                                    <option disabled value="">Choose Factory</option>
                                    <option v-for="f in factories" :key="f">{{ f }}</option>
                                </select>
                            </div>
                            <div v-if="activeFilters.includes('line')">
                                <select v-model="filters.line" class="select select-bordered select-sm">
                                    <option disabled value="">Choose Line</option>
                                    <option v-for="line in lines" :key="line">{{ line }}</option>
                                </select>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="flex items-center flex-nowrap gap-4">
                    <!-- Method Toggle -->
                    <label for="filter" class=" switch flex items-center gap-2" aria-label="Toggle Filter">
                        <input type="checkbox" id="filter" @change="toggleFilter" />
                        <span>Date</span>
                        <span>Hour</span>
                    </label>

                    <div v-show="timeFilterType === 'date'" class="flex items-center gap-2">
                        <label for="dateOnly" class="date-label">Select Date:</label>
                        <input type="date" id="dateOnly" v-model="selectedDate" class="input input-bordered input-sm" />
                    </div>

                    <div v-show="timeFilterType === 'hour'" class="flex items-center gap-2">
                        <label for="dateTime" class="date-label">Select Date & Time:</label>
                        <input type="datetime-local" id="dateTime" v-model="selectedDateTime"
                            class="input input-bordered input-sm" />
                    </div>
                </div>

                <!-- Content Area Line -->
                <div id="content-area-line" v-show="selectedContent === 'line'"
                    class="bg-base-300 p-4 rounded-lg shadow summary-card overflow-hidden">
                    <!-- Header + Close/Open -->
                    <div class="flex justify-between items-center">
                        <h3 class="text-lg font-bold">
                            {{ selectedLine ? `${selectedLine} ` : '377_TestLine' }}
                        </h3>
                    </div>

                    <!-- Switch -->
                    <div class="flex flex-wrap items-center gap-2 p-5">
                        <!-- Toggle switch -->
                        <div class="checkbox-wrapper-35">
                            <input type="checkbox" id="toggleFirstChart" class="switch"
                                @change="toggleFirstChart(firstChartChecked)" v-model="firstChartChecked" />
                            <label for="toggleFirstChart">
                                <span class="switch-x-toggletext">
                                    <span class="switch-x-unchecked">
                                        <span class="flex items-center gap-1 text-white">Chart Off</span>
                                    </span>
                                    <span class="switch-x-checked">
                                        <span class="flex items-center gap-1 text-white">Chart On</span>
                                    </span>
                                </span>
                            </label>
                        </div>

                        <!-- Chart container -->
                        <v-chart v-if="showFirstChart" :option="chartOptions" autoresize style="width: 100%; height: 400px;"></v-chart>
                    </div>

                    <!-- Table -->
                    <div class="overflow-x-auto">
                        <table class="table">
                            <!-- head -->
                            <thead>
                                <tr>
                                    <th>Date</th>
                                    <th>R1</th>
                                    <th>R2</th>
                                    <th>R3</th>
                                    <th>R4</th>
                                    <th>R5</th>
                                    <th>R6</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-if="tableRows.length === 0">
                                    <td colspan="3" class="text-center italic">No data found for selected date.</td>
                                </tr>
                                <tr v-for="row in tableRows" :key="row.date">
                                    <td>{{ row.date }}</td>
                                    <td>{{ row.R1 }}</td>
                                    <td>{{ row.R2 }}</td>
                                    <td>{{ row.R3 }}</td>
                                    <td>{{ row.R4 }}</td>
                                    <td>{{ row.R5 }}</td>
                                    <td>{{ row.R6 }}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>

                <!-- Content Area Station -->
                <div id="content-area-station" v-show="selectedContent === 'station'"
                    class="bg-base-300 p-4 rounded-lg shadow summary-card overflow-hidden mt-6">
                    <div class="flex justify-between items-center">
                        <h3 class="text-lg font-bold">
                            {{ selectedStation || 'Choose Station' }}
                        </h3>
                    </div>

                    <!-- Switch -->
                    <div class="flex flex-wrap items-center gap-2 p-5">
                        <!-- Toggle switch -->
                        <div class="checkbox-wrapper-35">
                            <input type="checkbox" id="toggleSecondChart" class="switch"
                                @change="toggleSecondChart(secondChartChecked)" v-model="secondChartChecked" />
                            <label for="toggleSecondChart">
                                <span class="switch-x-toggletext">
                                    <span class="switch-x-unchecked">
                                        <span class="flex items-center gap-1 text-white">Chart Off</span>
                                    </span>
                                    <span class="switch-x-checked">
                                        <span class="flex items-center gap-1 text-white">Chart On</span>
                                    </span>
                                </span>
                            </label>
                        </div>

                        <!-- Chart container -->
                       <v-chart
                        v-if="showSecondChart"
                        :option="chartStations"
                        autoresize
                        style="width: 100%; height: 400px;"
                        ></v-chart>
                    </div>
                    <div class="overflow-x-auto mt-4">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>Date</th>
                                    <th>Bootup</th>
                                    <th>WiFi+BT</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-if="visibleStationRows.length === 0">
                                    <td colspan="3" class="text-center italic">No data found for selected date.</td>
                                </tr>
                                <tr v-for="row in visibleStationRows" :key="row.station + '-' + row.date">
                                    <td>{{ row.date }}</td>
                                    <td>{{ row.bootup }}</td>
                                    <td>{{ row.wifi }}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>

                <!-- Content Area Group -->
                <div id="content-area-group" v-show="selectedContent === 'group'"
                    class="bg-base-300 p-4 rounded-lg shadow summary-card overflow-hidden mt-6">
                    <div class="flex justify-between items-center">
                        <h3 class="text-lg font-bold">
                            {{ selectedGroup || 'Choose Group' }}
                        </h3>
                    </div>

                    <!-- Switch -->
                    <div class="flex flex-wrap items-center gap-2 p-5">
                        <!-- Toggle switch -->
                        <div class="checkbox-wrapper-35">
                            <input type="checkbox" id="toggleThirdChart" class="switch"
                                @change="toggleThirdChart(thirdChartChecked)" v-model="thirdChartChecked" />
                            <label for="toggleThirdChart">
                                <span class="switch-x-toggletext">
                                    <span class="switch-x-unchecked">
                                        <span class="flex items-center gap-1 text-white">Chart Off</span>
                                    </span>
                                    <span class="switch-x-checked">
                                        <span class="flex items-center gap-1 text-white">Chart On</span>
                                    </span>
                                </span>
                            </label>
                        </div>

                        <!-- Chart container -->
                        <v-chart v-if="showThirdChart" :option="chartGroup" autoresize style="width: 100%; height: 400px;"></v-chart>
                    </div>

                    <div class="overflow-x-auto mt-4">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>Date</th>
                                    <th>377_R1#1(1)</th>
                                    <th>377_R1#11(11)</th>
                                    <th>377_R1#3(3)</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-if="visibleGroupRows.length === 0">
                                    <td colspan="3" class="text-center italic">No data found for selected date.</td>
                                </tr>
                                <tr v-for="row in visibleGroupRows" :key="row.group + '-' + row.date">
                                    <td>{{ row.date }}</td>
                                    <td>{{ row.production1 }}</td>
                                    <td>{{ row.production2 }}</td>
                                    <td>{{ row.production3 }}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>

                <!-- Content Area Production -->
                <div id="content-area-production" v-show="selectedContent === 'production'"
                    class="bg-base-300 p-4 rounded-lg shadow summary-card overflow-hidden mt-6">
                    <div class="flex justify-between items-center">
                        <h3 class="text-lg font-bold">
                            {{ selectedProduction || 'Choose Production' }}
                        </h3>
                    </div>

                    <!-- Switch -->
                    <div class="flex flex-wrap items-center gap-2 p-5">
                        <!-- Toggle switch -->
                        <div class="checkbox-wrapper-35">
                            <input type="checkbox" id="toggleFourthChart" class="switch"
                                @change="toggleFourthChart(fourthChartChecked)" v-model="fourthChartChecked" />
                            <label for="toggleFourthChart">
                                <span class="switch-x-toggletext">
                                    <span class="switch-x-unchecked">
                                        <span class="flex items-center gap-1 text-white">Chart Off</span>
                                    </span>
                                    <span class="switch-x-checked">
                                        <span class="flex items-center gap-1 text-white">Chart On</span>
                                    </span>
                                </span>
                            </label>
                        </div>

                        <!-- Chart container -->
                        <v-chart  v-if="showFourthChart" :option="chartProduction" autoresize style="width: 100%; height: 400px;"></v-chart>
                    </div>

                    <div class="overflow-x-auto mt-4">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>Date</th>
                                    <th>{{ selectedProduction}}</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-if="visibleProductionRows.length === 0">
                                    <td colspan="3" class="text-center italic">No data found for selected date.</td>
                                </tr>
                                <tr v-for="row in visibleProductionRows" :key="row.production + '-' + row.date">
                                    <td>{{ row.date }}</td>
                                    <td>{{ row.value }}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    interface GroupRow {
        station: string;
        date: string;
        group: string;
        production1: number;
        production2: number;
        production3: number;
    }
    interface StationRow {
        station: string;
        date: string;
        bootup: number;
        wifi: number;
    }
    interface ProductionRow {
        station: string,
            date: string,
            group: string,
            production: string,
            value: number
    }

    export default {
        data() {
            return {
                showSummaryCard: true,
                stationList: ['R1', 'R2', 'R3', 'R4', 'R5', 'R6'] as string[],
                selectedLine: '377_TestLine'  as string | null,
                selectedSubmenu: 'Bootup' as string | null,
                expandedStation: [] as string[],
                stationDetailsCards: [] as string[],
                timeFilterType: 'date' as 'date' | 'hour',
                selectedContent: 'line',
                showDropdown: false,
                firstChartChecked: true,
                secondChartChecked: true,
                thirdChartChecked: true,
                fourthChartChecked: true,
                showFirstChart: false,
                showSecondChart: false,
                showThirdChart: false,
                showFourthChart: false,
                selectedDate: '2025-11-01',
                selectedDateTime: '2025-11-01T08:00',
                chartOptions: {},
                chartStations: {},
                chartGroup: {},
                chartProduction: {},
                filters: {
                    site: '',
                    bu: '',
                    dateFrom: '',
                    dateUntil: '',
                    factory: '',
                    line: ''
                },
                activeFilters: ['site', 'bu', 'date'] as string[],
                availableFilters: [{
                        label: 'Site',
                        value: 'site'
                    },
                    {
                        label: 'BU',
                        value: 'bu'
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
                sites: ['Site A', 'Site B'] as string[],
                bus: ['BU1', 'BU2'] as string[],
                factories: ['Factory 1', 'Factory 2'] as string[],
                lines: ['Line 1', 'Line 2'] as string[],
                stationProductionData: [{
                        station: 'R1',
                        date: '2025-11-01',
                        value: 590
                    },
                    {
                        station: 'R2',
                        date: '2025-11-01',
                        value: 643
                    },
                    {
                        station: 'R3',
                        date: '2025-11-01',
                        value: 378
                    },
                    {
                        station: 'R4',
                        date: '2025-11-01',
                        value: 582
                    },
                    {
                        station: 'R5',
                        date: '2025-11-01',
                        value: 430
                    },
                    {
                        station: 'R6',
                        date: '2025-11-01',
                        value: 411
                    },
                    {
                        station: 'R1',
                        date: '2025-11-02',
                        value: 110
                    },
                    {
                        station: 'R2',
                        date: '2025-11-02',
                        value: 200
                    },
                    {
                        station: 'R3',
                        date: '2025-11-02',
                        value: 300
                    },
                    {
                        station: 'R4',
                        date: '2025-11-02',
                        value: 400
                    },
                    {
                        station: 'R5',
                        date: '2025-11-02',
                        value: 500
                    },
                    {
                        station: 'R6',
                        date: '2025-11-02',
                        value: 600
                    },
                    {
                        station: 'R1',
                        date: '2025-11-01T08:00',
                        value: 110
                    },
                    {
                        station: 'R1',
                        date: '2025-11-01T09:00',
                        value: 111
                    },
                    {
                        station: 'R2',
                        date: '2025-11-01T08:00',
                        value: 120
                    },
                    {
                        station: 'R2',
                        date: '2025-11-01T09:00',
                        value: 130
                    },
                    {
                        station: 'R3',
                        date: '2025-11-01T08:00',
                        value: 90
                    },
                    {
                        station: 'R3',
                        date: '2025-11-01T09:00',
                        value: 95
                    }
                ],
                selectedStation: null as string | null,
                filteredStationRows: [{
                        station: 'R1',
                        date: '2025-11-01',
                        bootup: 590,
                        wifi: 500
                    },
                    {
                        station: 'R2',
                        date: '2025-11-01',
                        bootup: 643,
                        wifi: 500
                    },
                    {
                        station: 'R3',
                        date: '2025-11-01',
                        bootup: 378,
                        wifi: 500
                    },
                    {
                        station: 'R4',
                        date: '2025-11-01',
                        bootup: 582,
                        wifi: 500
                    },
                    {
                        station: 'R5',
                        date: '2025-11-01',
                        bootup: 430,
                        wifi: 500
                    },
                    {
                        station: 'R6',
                        date: '2025-11-01',
                        bootup: 410,
                        wifi: 500
                    },
                    {
                        station: 'R1',
                        date: '2025-11-01T08:00',
                        bootup: 100,
                        wifi: 100
                    },
                    {
                        station: 'R1',
                        date: '2025-11-01T09:00',
                        bootup: 110,
                        wifi: 100
                    },
                    {
                        station: 'R2',
                        date: '2025-11-01T08:00',
                        bootup: 120,
                        wifi: 100
                    },
                    {
                        station: 'R2',
                        date: '2025-11-01T09:00',
                        bootup: 130,
                        wifi: 100
                    },
                    {
                        station: 'R3',
                        date: '2025-11-01T08:00',
                        bootup: 90,
                        wifi: 100
                    },
                    {
                        station: 'R3',
                        date: '2025-11-01T09:00',
                        bootup: 95,
                        wifi: 100
                    }
                ] as StationRow[],
                selectedGroup: null as string | null,
                filteredGroupRows: [{
                        station: 'R1',
                        date: '2025-11-01',
                        group: 'Bootup',
                        production1: 110,
                        production2: 120,
                        production3: 130
                    },
                    {
                        station: 'R2',
                        date: '2025-11-01',
                        group: 'Bootup',
                        production1: 70,
                        production2: 80,
                        production3: 90
                    },
                    {
                        station: 'R3',
                        date: '2025-11-01',
                        group: 'Bootup',
                        production1: 60,
                        production2: 50,
                        production3: 40
                    },
                    {
                        station: 'R4',
                        date: '2025-11-01',
                        group: 'Bootup',
                        production1: 60,
                        production2: 70,
                        production3: 80
                    },
                    {
                        station: 'R5',
                        date: '2025-11-01',
                        group: 'Bootup',
                        production1: 150,
                        production2: 160,
                        production3: 170
                    },
                    {
                        station: 'R6',
                        date: '2025-11-01',
                        group: 'Bootup',
                        production1: 110,
                        production2: 120,
                        production3: 130
                    },
                    {
                        station: 'R1',
                        date: '2025-11-01T08:00',
                        group: 'Bootup',
                        production1: 110,
                        production2: 120,
                        production3: 130
                    },
                    {
                        station: 'R1',
                        date: '2025-11-01T09:00',
                        group: 'Bootup',
                        production1: 150,
                        production2: 180,
                        production3: 200
                    },
                    {
                        station: 'R2',
                        date: '2025-11-01T08:00',
                        group: 'Bootup',
                        production1: 110,
                        production2: 120,
                        production3: 130
                    },
                    {
                        station: 'R2',
                        date: '2025-11-01T09:00',
                        group: 'Bootup',
                        production1: 110,
                        production2: 120,
                        production3: 130
                    },
                    {
                        station: 'R3',
                        date: '2025-11-01T08:00',
                        group: 'Bootup',
                        production1: 110,
                        production2: 120,
                        production3: 130
                    },
                    {
                        station: 'R3',
                        date: '2025-11-01T09:00',
                        group: 'Bootup',
                        production1: 110,
                        production2: 120,
                        production3: 130
                    }
                ] as GroupRow[],
                selectedProduction: null as string | null,
                filteredProductionRows: [{
                        station: 'R1',
                        date: '2025-11-01',
                        group: 'Bootup',
                        production: '377_R1#1',
                        value: 100
                    },
                    {
                        station: 'R1',
                        date: '2025-11-01',
                        group: 'Bootup',
                        production: '377_R1#3(3)',
                        value: 120
                    },
                    {
                        station: 'R1',
                        date: '2025-11-01',
                        group: 'Bootup',
                        production: '377_R1#11(11)',
                        value: 130
                    },
                    {
                        station: 'R2',
                        date: '2025-11-01',
                        group: 'Bootup',
                        production: '377_R2#1',
                        value: 100
                    },
                    {
                        station: 'R5',
                        date: '2025-11-01',
                        group: 'Bootup',
                        production: '377_R5#3(3)',
                        value: 120
                    },
                    {
                        station: 'R6',
                        date: '2025-11-01',
                        group: 'Bootup',
                        production: '377_R6#11(11)',
                        value: 130
                    },
                    {
                        station: 'R1',
                        date: '2025-11-01T08:00',
                        group: 'Bootup',
                        production: '377_R1#1',
                        value: 120
                    },
                    {
                        station: 'R1',
                        date: '2025-11-01T09:00',
                        group: 'Bootup',
                        production: '377_R1#3(3)',
                        value: 110
                    },
                    {
                        station: 'R2',
                        date: '2025-11-01T08:00',
                        group: 'Bootup',
                        production: '377_R1#11(11)',
                        value: 130
                    },
                    {
                        station: 'R2',
                        date: '2025-11-01T09:00',
                        group: 'Bootup',
                        production: '377_R1#1',
                        value: 120
                    },
                    {
                        station: 'R3',
                        date: '2025-11-01T08:00',
                        group: 'Bootup',
                        production: '377_R1#3(3)',
                        value: 110
                    },
                    {
                        station: 'R3',
                        date: '2025-11-01T09:00',
                        group: 'Bootup',
                        production: '377_R1#11(11)',
                        value: 130
                    }
                ] as ProductionRow[],
                showAlert : false
            }
        },
        computed: {
            filteredProductionData() {
                if (this.timeFilterType === 'date') {
                    if (!this.selectedDate) return [];
                    return this.stationProductionData.filter(item =>
                        item.date === this.selectedDate
                    );
                } else if (this.timeFilterType === 'hour') {
                    if (!this.selectedDateTime) return [];
                    const datetimePrefix = this.selectedDateTime.slice(0, 16);
                    return this.stationProductionData.filter(d =>
                        d.date.startsWith(datetimePrefix)
                    );
                }
                return [];
            },
            tableRows() {
                const grouped: Record < string, Record < string, number | string >> = {}

                this.filteredProductionData.forEach(item => {
                    if (!grouped[item.date]) grouped[item.date] = {}
                    grouped[item.date][item.station] = item.value
                })

                return Object.entries(grouped).map(([date, stations]) => ({
                    date,
                    R1: stations['R1'] ?? '-',
                    R2: stations['R2'] ?? '-',
                    R3: stations['R3'] ?? '-',
                    R4: stations['R4'] ?? '-',
                    R5: stations['R5'] ?? '-',
                    R6: stations['R6'] ?? '-',
                }))
            },
            visibleStationRows() {
                if (!this.selectedStation) return [];

                if (this.timeFilterType === 'date') {
                    if (!this.selectedDate) return [];
                    return this.filteredStationRows.filter(
                        (row: StationRow) =>
                        row.station === this.selectedStation &&
                        row.date === this.selectedDate
                    );
                } else if (this.timeFilterType === 'hour') {
                    if (!this.selectedDateTime) return [];
                    const prefix = this.selectedDateTime.slice(0, 16);
                    return this.filteredStationRows.filter(
                        (row: StationRow) =>
                        row.station === this.selectedStation &&
                        row.date.startsWith(prefix)
                    );
                }
                return [];
            },
            visibleGroupRows() {
                if (!this.selectedGroup) return [];

                const filterByStation = this.selectedStation ?
                    (row: GroupRow) => row.station === this.selectedStation :
                    () => true;

                if (this.timeFilterType === 'date') {
                    if (!this.selectedDate) return [];
                    return this.filteredGroupRows.filter(
                        (row: GroupRow) =>
                        row.group === this.selectedGroup &&
                        row.date === this.selectedDate &&
                        filterByStation(row)
                    );
                } else if (this.timeFilterType === 'hour') {
                    if (!this.selectedDateTime) return [];
                    const prefix = this.selectedDateTime.slice(0, 16);
                    return this.filteredGroupRows.filter(
                        (row: GroupRow) =>
                        row.group === this.selectedGroup &&
                        row.date.startsWith(prefix) &&
                        filterByStation(row)
                    );
                }
                return [];
            },
            visibleProductionRows() {
                if (!this.selectedProduction) return [];

                const filterByStation = this.selectedStation ?
                    (row: ProductionRow) => row.station === this.selectedStation :
                    () => true;

                const filterByGroup = this.selectedGroup ?
                    (row: ProductionRow) => row.group === this.selectedGroup :
                    () => true;

                if (this.timeFilterType === 'date') {
                    if (!this.selectedDate) return [];
                    return this.filteredProductionRows.filter(
                        (row: ProductionRow) =>
                        row.production === this.selectedProduction &&
                        row.date === this.selectedDate &&
                        filterByStation(row) &&
                        filterByGroup(row)
                    );
                } else if (this.timeFilterType === 'hour') {
                    if (!this.selectedDateTime) return [];
                    const prefix = this.selectedDateTime.slice(0, 16);
                    return this.filteredProductionRows.filter(
                        (row: ProductionRow) =>
                        row.production === this.selectedProduction &&
                        row.date.startsWith(prefix) &&
                        filterByStation(row) &&
                        filterByGroup(row)
                    );
                }
                return [];
            }
        },
        methods: {
            toggleSummary() {
                this.showSummaryCard = !this.showSummaryCard

                if (!this.showSummaryCard) {
                    this.selectedLine = null
                    this.selectedSubmenu = null
                    this.expandedStation = []
                } else {
                    this.selectedLine = this.selectedLine || '377_TestLine'
                    this.selectedSubmenu = this.selectedSubmenu || 'Bootup'
                }
            },
            toggleDropdown() {
                this.showDropdown = !this.showDropdown
            },
            setTimeFilterType(type: 'date' | 'hour') {
                this.timeFilterType = type
            },
            toggleFilter() {
                const checkbox = document.getElementById('filter') as HTMLInputElement
                this.timeFilterType = checkbox.checked ? 'hour' : 'date'
                this.selectedDate = ''
                this.selectedDateTime = ''
            },
            toggleFirstChart(isChecked: boolean) {
                if (!isChecked) {
                    this.showFirstChart = false;
                    return;
                }

                let filteredData = [];

                if (this.timeFilterType === 'date') {
                    if (!this.selectedDate) {
                        alert('Please select a date first.');
                        this.firstChartChecked = false;
                        return;
                    }

                    filteredData = this.stationProductionData.filter(d => d.date === this.selectedDate);
                } else {
                    if (!this.selectedDateTime) {
                        alert('Please select date & time first.');
                        this.firstChartChecked = false;
                        return;
                    }
                    filteredData = this.stationProductionData.filter(d => d.date.startsWith(this.selectedDateTime.slice(
                        0, 16)));
                }

                const stations = filteredData.map(d => d.station);
                const values = filteredData.map(d => d.value);

                this.chartOptions = {
                    backgroundColor: 'transparent',
                    title: {
                        text: this.timeFilterType === 'date' ?
                            `${this.selectedLine} on ${this.selectedDate}` :
                            `${this.selectedLine} on ${this.selectedDateTime}`,
                        textStyle: {
                            fontSize: 16,
                            color: '#fff'
                        },
                        left: 'center'
                    },
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'shadow'
                        }
                    },
                    xAxis: {
                        type: 'category',
                        data: stations,
                        axisLabel: {
                            show: true,
                            interval: 'auto',
                            color: '#FFFFFF',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10,
                            fontSize: 10
                        },
                        splitLine: {
                            show: true,
                            lineStyle: {
                                color: '#454545'
                            }
                        },
                        axisLine: {
                            lineStyle: {
                                color: '#888'
                            }
                        }
                    },
                    yAxis: {
                        type: 'value',
                        min: 0,
                        name: 'Units Produced',
                        nameTextStyle: {
                            fontSize: 16,
                            color: '#fff',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10,
                        },
                        axisLabel: {
                            color: '#fff',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10,
                        },
                        axisLine: {
                            lineStyle: {
                                color: '#888'
                            }
                        },
                        splitLine: {
                            lineStyle: {
                                color: '#444'
                            }
                        }
                    },
                    series: [{
                        type: 'bar',
                        name: 'Units Produced',
                        data: values,
                        itemStyle: {
                            color: '#00d3bb',
                            shadowColor: 'rgba(255, 255, 255, 0.7)',
                            shadowBlur: 10
                        }
                    }],
                    grid: {
                        left: '3%',
                        right: '4%',
                        bottom: '3%',
                        containLabel: true  
                    }
                }
                this.showFirstChart = true;
            },

            // Chart Station
            toggleSecondChart(isChecked: boolean) {
                if (!isChecked) {
                    this.showSecondChart = false;
                    return;
                }
                let filteredData = [];
                if (this.timeFilterType === 'date') {
                    if (!this.selectedDate) {
                        alert('Please select a date first.');
                        this.secondChartChecked = false;
                        return;
                    }
                    filteredData = this.filteredStationRows.filter(d => d.date === this.selectedDate);
                } else {
                    if (!this.selectedDateTime) {
                        alert('Please select date & time first.');
                        this.secondChartChecked = false;
                        return;
                    }
                    filteredData = this.filteredStationRows.filter(d => d.date.startsWith(this.selectedDateTime.slice(0,
                        16)));
                }

                const bootupData = filteredData.map(d => d.bootup)
                const wifiData = filteredData.map(d => d.wifi)
                const stations = filteredData.map(d => d.station)

                this.chartStations = {
                    backgroundColor: 'transparent',
                    title: {
                        text: this.timeFilterType === 'date' ?
                            `${this.selectedLine} on ${this.selectedDate}` :
                            `${this.selectedLine} on ${this.selectedDateTime}`,
                        textStyle: {
                            fontSize: 16,
                            color: '#fff'
                        },
                        left: 'center'
                    },
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'shadow'
                        }
                    },
                    xAxis: {
                        type: 'category',
                        data: stations,
                        axisLabel: {
                            show: true,
                            interval: 'auto',
                            color: '#FFFFFF',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10,
                            fontSize: 10
                        },
                        splitLine: {
                            show: true,
                            lineStyle: {
                                color: '#454545'
                            }
                        },
                        axisLine: {
                            lineStyle: {
                                color: '#888'
                            }
                        }
                    },
                    yAxis: {
                        type: 'value',
                        min: 0,
                        name: 'Units Produced',
                        nameTextStyle: {
                            fontSize: 16,
                            color: '#fff',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10,
                        },
                        axisLabel: {
                            color: '#fff',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10

                        },
                        axisLine: {
                            lineStyle: {
                                color: '#888'
                            }
                        },
                        splitLine: {
                            lineStyle: {
                                color: '#444'
                            }
                        }
                    },
                    series: [{
                            name: 'Bootup',
                            type: 'bar',
                            data: bootupData,
                            itemStyle: {
                                color: '#00d3bb',
                                shadowColor: 'rgba(255, 255, 255, 0.7)',
                                shadowBlur: 10
                            }
                        },
                        {
                            name: 'WiFi',
                            type: 'bar',
                            data: wifiData,
                            itemStyle: {
                                color: '#51EAEA',
                                shadowColor: 'rgba(255, 255, 255, 0.7)',
                                shadowBlur: 10
                            }
                        }
                    ],
                    grid: {
                        left: '3%',
                        right: '4%',
                        bottom: '3%',
                        containLabel: true
                    }
                }

                this.showSecondChart = true;
            },

            // Chart Group
            toggleThirdChart(isChecked: boolean) {

                if (!isChecked) {
                    this.showThirdChart = false;
                    return;
                }

                let filteredData = [];

                if (this.timeFilterType === 'date') {
                    if (!this.selectedDate) {
                        alert('Please select a date first.');
                        this.thirdChartChecked = false;
                        return;
                    }
                    filteredData = this.filteredGroupRows.filter(d => d.date === this.selectedDate);
                } else {
                    if (!this.selectedDateTime) {
                        alert('Please select date & time first.');
                        this.thirdChartChecked = false;
                        return;
                    }
                    filteredData = this.filteredGroupRows.filter(d => d.date.startsWith(this.selectedDateTime.slice(0,
                        16)));
                }

                const production1 = filteredData.map(d => d.production1)
                const production2 = filteredData.map(d => d.production2)
                const production3 = filteredData.map(d => d.production3)
                const stations = filteredData.map(d => d.station)

                this.chartGroup = {
                    backgroundColor: 'transparent',
                    title: {
                        text: this.timeFilterType === 'date' ?
                            `${this.selectedLine} on ${this.selectedDate}` :
                            `${this.selectedLine} on ${this.selectedDateTime}`,
                        textStyle: {
                            fontSize: 16,
                            color: '#fff'
                        },
                        left: 'center'
                    },
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'shadow'
                        }
                    },
                    xAxis: {
                        type: 'category',
                        data: stations,
                        axisLabel: {
                            show: true,
                            interval: 'auto',
                            color: '#FFFFFF',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10,
                            fontSize: 10
                        },
                        splitLine: {
                            show: true,
                            lineStyle: {
                                color: '#454545'
                            }
                        },
                        axisLine: {
                            lineStyle: {
                                color: '#888'
                            }
                        }
                    },
                    yAxis: {
                        type: 'value',
                        min: 0,
                        name: 'Units Produced',
                        nameTextStyle: {
                            fontSize: 16,
                            color: '#fff',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10,
                        },
                        axisLabel: {
                            color: '#fff',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10

                        },
                        axisLine: {
                            lineStyle: {
                                color: '#888'
                            }
                        },
                        splitLine: {
                            lineStyle: {
                                color: '#444'
                            }
                        }
                    },
                    series: [{
                            name: '377_R1#1',
                            type: 'bar',
                            data: production1,
                            itemStyle: {
                                color: '#00d3bb',
                                shadowColor: 'rgba(255, 255, 255, 0.7)',
                                shadowBlur: 10
                            }
                        },
                        {
                            name: '377_R1#3(3)',
                            type: 'bar',
                            data: production2,
                            itemStyle: {
                                color: '#51EAEA',
                                shadowColor: 'rgba(255, 255, 255, 0.7)',
                                shadowBlur: 10
                            }
                        },
                        {
                            name: '377_R1#3(3)',
                            type: 'bar',
                            data: production3,
                            itemStyle: {
                                color: '#FF9A00',
                                shadowColor: 'rgba(255, 255, 255, 0.7)',
                                shadowBlur: 10
                            }
                        }
                    ],
                    grid: {
                        left: '3%',
                        right: '4%',
                        bottom: '3%',
                        containLabel: true
                    }
                }

                this.showThirdChart = true;
            },
            // Chart Production
            toggleFourthChart(isChecked: boolean) {
                if (!isChecked) {
                    this.showFourthChart = false;
                    return;
                }

                let filteredData = [];

                if (this.timeFilterType === 'date') {
                    if (!this.selectedDate) {
                        alert('Please select a date first.');
                        this.fourthChartChecked = false
                        return;
                    }
                    filteredData = this.filteredProductionRows.filter(d => d.date === this.selectedDate);
                } else {
                    if (!this.selectedDateTime) {
                        alert('Please select date & time first.');
                        this.fourthChartChecked = false
                        return;
                    }
                    filteredData = this.filteredProductionRows.filter(d => d.date.startsWith(this.selectedDateTime
                        .slice(0,
                            16)));
                }

                const production = filteredData.map(d => d.production)
                const value = filteredData.map(d => d.value)

                this.chartProduction = {
                    backgroundColor: 'transparent',
                    title: {
                        text: this.timeFilterType === 'date' ?
                            `${this.selectedLine} on ${this.selectedDate}` :
                            `${this.selectedLine} on ${this.selectedDateTime}`,
                        textStyle: {
                            fontSize: 16,
                            color: '#fff'
                        },
                        left: 'center'
                    },
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'shadow'
                        }
                    },
                    xAxis: {
                        type: 'category',
                        data: production,
                        axisLabel: {
                            show: true,
                            interval: 'auto',
                            color: '#FFFFFF',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10,
                            fontSize: 10
                        },
                        splitLine: {
                            show: true,
                            lineStyle: {
                                color: '#454545'
                            }
                        },
                        axisLine: {
                            lineStyle: {
                                color: '#888'
                            }
                        }
                    },
                    yAxis: {
                        type: 'value',
                        min: 0,
                        name: 'Units Produced',
                        nameTextStyle: {
                            fontSize: 16,
                            color: '#fff',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10,
                        },
                        axisLabel: {
                            color: '#fff',
                            textShadowColor: 'rgba(255, 255, 255, 0.7)',
                            textShadowBlur: 10

                        },
                        axisLine: {
                            lineStyle: {
                                color: '#888'
                            }
                        },
                        splitLine: {
                            lineStyle: {
                                color: '#444'
                            }
                        }
                    },
                    series: [{
                        type: 'bar',
                        name: 'Units Produced',
                        data: value,
                        itemStyle: {
                            color: '#00d3bb',
                            shadowColor: 'rgba(255, 255, 255, 0.7)',
                            shadowBlur: 10
                        }
                    }],
                    grid: {
                        left: '3%',
                        right: '4%',
                        bottom: '3%',
                        containLabel: true
                    }
                }

                this.showFourthChart = true;
            },
            onStationClick(station: string) {
                this.selectedStation = station;
            },
            onGroupClick(group: string) {
                this.selectedGroup = group;
            },

        },
        mounted() {
            if (this.firstChartChecked) {
                this.toggleFirstChart(this.firstChartChecked);
            } 
            if (this.secondChartChecked) {
                this.toggleSecondChart(this.secondChartChecked);
            }
            if (this.thirdChartChecked) {
                this.toggleThirdChart(this.thirdChartChecked);
            }
            if (this.fourthChartChecked) {
                this.toggleFourthChart(this.fourthChartChecked);
            }
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

    #checklist {
        --background: #ecf9ff;
        --text: #5d6474;
        --check: #35A29F;
        --disabled: #d3c8de;
        background: var(--background);
        border-radius: 10px;
        padding: 20px;
        width: 200px;
        display: grid;
        grid-template-columns: 60px auto;
        gap: 10px;
        box-shadow: 0 10px 30px rgba(65, 72, 86, 0.1);
    }

    #checklist label {
        color: var(--text);
        position: relative;
        cursor: pointer;
        display: grid;
        align-items: center;
        width: fit-content;
        transition: color 0.3s ease;
        margin-right: 10px;
        font-size: 10px;
    }

    #checklist label::before,
    #checklist label::after {
        content: "";
        position: absolute;
    }

    #checklist label::before {
        height: 2px;
        width: 8px;
        left: -27px;
        background: var(--check);
        border-radius: 2px;
        transition: background 0.3s ease;
    }

    #checklist label:after {
        height: 4px;
        width: 4px;
        top: 8px;
        left: -25px;
        border-radius: 50%;
    }

    #checklist input[type="checkbox"] {
        appearance: none;
        -webkit-appearance: none;
        position: relative;
        height: 15px;
        width: 15px;
        outline: none;
        border: 0;
        margin: 0 15px 0 0;
        cursor: pointer;
        background: var(--background);
        display: grid;
        align-items: center;
        margin-right: 20px;
    }

    #checklist input[type="checkbox"]::before,
    #checklist input[type="checkbox"]::after {
        content: "";
        position: absolute;
        height: 2px;
        top: auto;
        background: var(--check);
        border-radius: 2px;
    }

    #checklist input[type="checkbox"]::before {
        width: 0px;
        right: 60%;
        transform-origin: right bottom;
    }

    #checklist input[type="checkbox"]::after {
        width: 0px;
        left: 40%;
        transform-origin: left bottom;
    }

    #checklist input[type="checkbox"]:checked::before {
        animation: check-01 0.4s ease forwards;
    }

    #checklist input[type="checkbox"]:checked::after {
        animation: check-02 0.4s ease forwards;
    }

    #checklist input[type="checkbox"]:checked+label {
        color: var(--disabled);
        animation: move 0.3s ease 0.1s forwards;
    }

    #checklist input[type="checkbox"]:checked+label::before {
        background: var(--disabled);
        animation: slice 0.4s ease forwards;
    }

    #checklist input[type="checkbox"]:checked+label::after {
        animation: firework 0.5s ease forwards 0.1s;
    }

    @keyframes move {
        50% {
            padding-left: 8px;
            padding-right: 0px;
        }

        100% {
            padding-right: 4px;
        }
    }

    @keyframes slice {
        60% {
            width: 100%;
            left: 4px;
        }

        100% {
            width: 100%;
            left: -2px;
            padding-left: 0;
        }
    }

    @keyframes check-01 {
        0% {
            width: 4px;
            top: auto;
            transform: rotate(0);
        }

        50% {
            width: 0px;
            top: auto;
            transform: rotate(0);
        }

        51% {
            width: 0px;
            top: 8px;
            transform: rotate(45deg);
        }

        100% {
            width: 5px;
            top: 8px;
            transform: rotate(45deg);
        }
    }

    @keyframes check-02 {
        0% {
            width: 4px;
            top: auto;
            transform: rotate(0);
        }

        50% {
            width: 0px;
            top: auto;
            transform: rotate(0);
        }

        51% {
            width: 0px;
            top: 8px;
            transform: rotate(-45deg);
        }

        100% {
            width: 10px;
            top: 8px;
            transform: rotate(-45deg);
        }
    }

    @keyframes firework {
        0% {
            opacity: 1;
            box-shadow: 0 0 0 -2px #35A29F, 0 0 0 -2px #35A29F, 0 0 0 -2px #35A29F,
                0 0 0 -2px #35A29F, 0 0 0 -2px #35A29F, 0 0 0 -2px #35A29F;
        }

        30% {
            opacity: 1;
        }

        100% {
            opacity: 0;
            box-shadow: 0 -15px 0 0px #35A29F, 14px -8px 0 0px #35A29F,
                14px 8px 0 0px #35A29F, 0 15px 0 0px #35A29F, -14px 8px 0 0px #35A29F,
                -14px -8px 0 0px #35A29F;
        }
    }

    .fade-enter-active,
    .fade-leave-active {
        transition: opacity 0.6s ease-in-out;
    }

    .fade-enter-from,
    .fade-leave-to {
        opacity: 0;
    }

    /* switch date/hour */
    .switch {
        --_switch-bg-clr: #00d3bb;
        --_switch-padding: 6px;
        /* padding around button*/
        --_slider-bg-clr: rgba(215, 235, 236, 0.65);
        /* slider color unchecked */
        --_slider-bg-clr-on: rgba(215, 235, 236, 0.65);
        /* slider color checked */
        --_slider-txt-clr: #ffffff;
        --_label-padding: 1rem 1rem;
        /* padding around the labels -  this gives the switch it's global width and height */
        --_switch-easing: cubic-bezier(0.47,
                1.64,
                0.41,
                0.8);
        color: white;
        width: fit-content;
        display: flex;
        justify-content: center;
        position: relative;
        border-radius: 10px;
        cursor: pointer;
        display: grid;
        grid-template-columns: repeat(2, minmax(0, 1fr));
        position: relative;
        isolation: isolate;
    }

    .switch input[type="checkbox"] {
        position: absolute;
        width: 1px;
        height: 1px;
        padding: 0;
        margin: -1px;
        overflow: hidden;
        clip: rect(0, 0, 0, 0);
        white-space: nowrap;
        border-width: 0;
    }

    .switch>span {
        display: grid;
        place-content: center;
        transition: opacity 300ms ease-in-out 150ms;
        padding: var(--_label-padding);
    }

    .switch::before,
    .switch::after {
        content: "";
        position: absolute;
        border-radius: inherit;
        transition: inset 150ms ease-in-out;
    }


    .switch::before {
        background-color: var(--_slider-bg-clr);
        inset: var(--_switch-padding) 50% var(--_switch-padding) var(--_switch-padding);
        transition:
            inset 500ms var(--_switch-easing),
            background-color 500ms ease-in-out;
        z-index: -1;
        box-shadow:
            inset 0 1px 1px rgba(0, 0, 0, 0.3),
            0 1px rgba(255, 255, 255, 0.3);
    }

    .switch::after {
        background-color: var(--_switch-bg-clr);
        inset: 0;
        z-index: -2;
    }

    /* .switch:focus-within::after {
        inset: -0.25rem;
    } */

    .switch:has(input:checked):hover>span:first-of-type,
    .switch:has(input:not(:checked)):hover>span:last-of-type {
        opacity: 1;
        transition-delay: 0ms;
        transition-duration: 100ms;
    }

    .switch:has(input:checked):hover::before {
        inset: var(--_switch-padding) var(--_switch-padding) var(--_switch-padding) 45%;
    }

    .switch:has(input:not(:checked)):hover::before {
        inset: var(--_switch-padding) 45% var(--_switch-padding) var(--_switch-padding);
    }

    .switch:has(input:checked)::before {
        background-color: var(--_slider-bg-clr-on);
        inset: var(--_switch-padding) var(--_switch-padding) var(--_switch-padding) 50%;
    }

    .switch>span:last-of-type,
    .switch>input:checked+span:first-of-type {
        opacity: 0.75;
    }

    .switch>input:checked~span:last-of-type {
        opacity: 1;
    }

    /* Switch chart / table button */
    .checkbox-wrapper-35 .switch {
        display: none;
    }

    .checkbox-wrapper-35 .switch+label {
        -webkit-box-align: center;
        -webkit-align-items: center;
        -ms-flex-align: center;
        align-items: center;
        color: #ffffff;
        cursor: pointer;
        display: -webkit-box;
        display: -webkit-flex;
        display: -ms-flexbox;
        display: flex;
        font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
        font-size: 17px;
        line-height: 17px;
        position: relative;
        -webkit-user-select: none;
        -moz-user-select: none;
        -ms-user-select: none;
        user-select: none;
    }

    .checkbox-wrapper-35 .switch+label::before,
    .checkbox-wrapper-35 .switch+label::after {
        content: '';
        display: block;
    }

    .checkbox-wrapper-35 .switch+label::before {
        background-color: #2b323b;
        border-radius: 500px;
        height: 15px;
        margin-right: 8px;
        -webkit-transition: background-color 0.125s ease-out;
        transition: background-color 0.125s ease-out;
        width: 25px;
    }

    .checkbox-wrapper-35 .switch+label::after {
        background-color: #fff;
        border-radius: 13px;
        box-shadow: 0 3px 1px 0 rgba(37, 34, 71, 0.05), 0 2px 2px 0 rgba(37, 34, 71, 0.1), 0 3px 3px 0 rgba(37, 34, 71, 0.05);
        height: 13px;
        left: 1px;
        position: absolute;
        top: 1px;
        -webkit-transition: -webkit-transform 0.125s ease-out;
        transition: -webkit-transform 0.125s ease-out;
        transition: transform 0.125s ease-out;
        transition: transform 0.125s ease-out, -webkit-transform 0.125s ease-out;
        width: 13px;
    }

    .checkbox-wrapper-35 .switch+label .switch-x-text {
        display: block;
        margin-right: .3em;
    }

    .checkbox-wrapper-35 .switch+label .switch-x-toggletext {
        display: flex;
        align-items: center;
        font-weight: bold;
        height: 15px;
        overflow: hidden;
        position: relative;
        width: 150px;
    }

    .checkbox-wrapper-35 .switch+label .switch-x-unchecked,
    .checkbox-wrapper-35 .switch+label .switch-x-checked {
        left: 0;
        position: absolute;
        top: 0;
        -webkit-transition: opacity 0.125s ease-out, -webkit-transform 0.125s ease-out;
        transition: opacity 0.125s ease-out, -webkit-transform 0.125s ease-out;
        transition: transform 0.125s ease-out, opacity 0.125s ease-out;
        transition: transform 0.125s ease-out, opacity 0.125s ease-out, -webkit-transform 0.125s ease-out;
    }

    .checkbox-wrapper-35 .switch+label .switch-x-unchecked {
        opacity: 1;
        -webkit-transform: none;
        transform: none;
    }

    .checkbox-wrapper-35 .switch+label .switch-x-checked {
        opacity: 0;
        -webkit-transform: translate3d(0, 100%, 0);
        transform: translate3d(0, 100%, 0);
    }

    .checkbox-wrapper-35 .switch+label .switch-x-hiddenlabel {
        position: absolute;
        visibility: hidden;
    }

    .checkbox-wrapper-35 .switch:checked+label::before {
        background-color: #35A29F;
    }

    .checkbox-wrapper-35 .switch:checked+label::after {
        -webkit-transform: translate3d(10px, 0, 0);
        transform: translate3d(10px, 0, 0);
    }

    .checkbox-wrapper-35 .switch:checked+label .switch-x-unchecked {
        opacity: 0;
        -webkit-transform: translate3d(0, -100%, 0);
        transform: translate3d(0, -100%, 0);
    }

    .checkbox-wrapper-35 .switch:checked+label .switch-x-checked {
        opacity: 1;
        -webkit-transform: none;
        transform: none;
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

  html[data-theme='light'] .text-base-content {
    color: #eee;
  }

  html[data-theme='light'] .menu{
    color: #eee;
  }

</style>