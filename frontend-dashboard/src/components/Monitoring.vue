<template>
  <div class="monitoring p-4 font-inter ">
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
    <div class="grid grid-flow-col lg:grid-cols-3 md:grid-cols-3 grid-rows-8 gap-1">
      <!-- Sidebar -->
      <div class="row-span-8 max-w-[30rem] h-full overflow-y-auto bg-base-200 p-4 rounded-lg shadow">
        <h2>Operations Information</h2>
        <!-- Search -->
        <label class="input mt-5 xl:w-96 flex items-center gap-2">
          <svg class="h-[1em] opacity-50" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
            <g stroke-linejoin="round" stroke-linecap="round" stroke-width="2.5" fill="none" stroke="currentColor">
              <circle cx="11" cy="11" r="8"></circle>
              <path d="m21 21-4.3-4.3"></path>
            </g>
          </svg>
          <input type="search" class="grow input input-bordered w-full" placeholder="Search" />
          <select class="select w-32 bg-base-300 h-7">
            <option>Floor 1</option>
            <option>Floor 2</option>
            <option>Floor 3</option>
          </select>
        </label>
        <!-- Filter -->
        <form class="filter mt-5">
          <input class="btn btn-square btn-accent" type="reset" value="×" />
          <input class="btn btn-accent" type="radio" name="frameworks" aria-label="AGV" />
          <input class="btn btn-accent" type="radio" name="frameworks" aria-label="AOI" />
          <input class="btn btn-accent" type="radio" name="frameworks" aria-label="ROBOT" />
          <input class="btn btn-accent" type="radio" name="frameworks" aria-label="FIXTURE" />
        </form>
        <!-- Card -->
        <div class="card bg-base-300 text-primary-content xl:w-96 lg:w-80 mt-5">
          <div class="card-body">
            <h2 class="card-title">
              AGV-1606
            </h2>

            <!-- Range dengan label kiri dan kanan -->
            <div class="flex items-center gap-4">
              <span class="text-sm text-white whitespace-nowrap">Factory A</span>
              <input type="range" min="0" max="100" value="40" class="range range-accent h-2" />
              <span class="text-sm text-white whitespace-nowrap">Factory B</span>
            </div>

            <div class="divider"></div>
            <div class="card-actions justify-center">
              <div class="tooltip" data-tip="status">
                <span
                  class="flex items-center justify-center gap-4 bg-base-200 xl:w-32 lg:w-24 h-10 border-2 border-accent rounded-lg">
                  <div class="status status-info animate-bounce"></div> Stand By
                </span>
              </div> 
              <div class="tooltip" data-tip="queue">
                <span
                  class="flex items-center justify-center gap-4 bg-base-200 w-24 h-10 border-2 border-accent rounded-lg">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-4">
                    <path fill-rule="evenodd"
                      d="M1.045 6.954a2.75 2.75 0 0 1 .217-.678L2.53 3.58A2.75 2.75 0 0 1 5.019 2h9.962a2.75 2.75 0 0 1 2.488 1.58l1.27 2.696c.101.216.174.444.216.678A1 1 0 0 1 19 7.25v1.5a2.75 2.75 0 0 1-2.75 2.75H3.75A2.75 2.75 0 0 1 1 8.75v-1.5a1 1 0 0 1 .045-.296Zm2.843-2.736A1.25 1.25 0 0 1 5.02 3.5h9.962c.484 0 .925.28 1.13.718l.957 2.032H14a1 1 0 0 0-.86.49l-.606 1.02a1 1 0 0 1-.86.49H8.236a1 1 0 0 1-.894-.553l-.448-.894A1 1 0 0 0 6 6.25H2.932l.956-2.032Z"
                      clip-rule="evenodd" />
                    <path
                      d="M1 14a1 1 0 0 1 1-1h4a1 1 0 0 1 .894.553l.448.894a1 1 0 0 0 .894.553h3.438a1 1 0 0 0 .86-.49l.606-1.02A1 1 0 0 1 14 13h4a1 1 0 0 1 1 1v2a2 2 0 0 1-2 2H3a2 2 0 0 1-2-2v-2Z" />
                  </svg>
                  1/4
                </span>
              </div>
              <div class="tooltip" data-tip="battery">
                <span
                  class="flex items-center justify-center gap-4 bg-base-200 w-24 h-10 border-2 border-accent rounded-lg">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-5">
                    <path
                      d="M4.75 8a.75.75 0 0 0-.75.75v2.5c0 .414.336.75.75.75H9.5a.75.75 0 0 0 .75-.75v-2.5A.75.75 0 0 0 9.5 8H4.75Z" />
                    <path fill-rule="evenodd"
                      d="M3.25 5A2.25 2.25 0 0 0 1 7.25v5.5A2.25 2.25 0 0 0 3.25 15h12.5A2.25 2.25 0 0 0 18 12.75v-1.085a1.5 1.5 0 0 0 1-1.415v-.5a1.5 1.5 0 0 0-1-1.415V7.25A2.25 2.25 0 0 0 15.75 5H3.25ZM2.5 7.25a.75.75 0 0 1 .75-.75h12.5a.75.75 0 0 1 .75.75v5.5a.75.75 0 0 1-.75.75H3.25a.75.75 0 0 1-.75-.75v-5.5Z"
                      clip-rule="evenodd" />
                  </svg>
                  55%
                </span>
              </div>
            </div>
          </div>
        </div>
        <div class="card bg-base-300 text-primary-content xl:w-96 lg:w-80 mt-5">
          <div class="card-body">
            <h2 class="card-title">
              AGV-1607
            </h2>

            <!-- Range dengan label kiri dan kanan -->
            <div class="flex items-center gap-4">
              <span class="text-sm text-white whitespace-nowrap">Factory A</span>
              <input type="range" min="0" max="100" value="40" class="range range-accent h-2" />
              <span class="text-sm text-white whitespace-nowrap">Factory B</span>
            </div>

            <div class="divider"></div>
            <div class="card-actions justify-center">
              <div class="tooltip" data-tip="status">
                <span
                  class="flex items-center justify-center gap-4 bg-base-200 xl:w-32 lg:w-24 h-10 border-2 border-accent rounded-lg">
                  <div class="status status-accent animate-bounce"></div> Running
                </span>
              </div>
              <div class="tooltip" data-tip="queue">
                <span
                  class="flex items-center justify-center gap-4 bg-base-200 w-24 h-10 border-2 border-accent rounded-lg">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-4">
                    <path fill-rule="evenodd"
                      d="M1.045 6.954a2.75 2.75 0 0 1 .217-.678L2.53 3.58A2.75 2.75 0 0 1 5.019 2h9.962a2.75 2.75 0 0 1 2.488 1.58l1.27 2.696c.101.216.174.444.216.678A1 1 0 0 1 19 7.25v1.5a2.75 2.75 0 0 1-2.75 2.75H3.75A2.75 2.75 0 0 1 1 8.75v-1.5a1 1 0 0 1 .045-.296Zm2.843-2.736A1.25 1.25 0 0 1 5.02 3.5h9.962c.484 0 .925.28 1.13.718l.957 2.032H14a1 1 0 0 0-.86.49l-.606 1.02a1 1 0 0 1-.86.49H8.236a1 1 0 0 1-.894-.553l-.448-.894A1 1 0 0 0 6 6.25H2.932l.956-2.032Z"
                      clip-rule="evenodd" />
                    <path
                      d="M1 14a1 1 0 0 1 1-1h4a1 1 0 0 1 .894.553l.448.894a1 1 0 0 0 .894.553h3.438a1 1 0 0 0 .86-.49l.606-1.02A1 1 0 0 1 14 13h4a1 1 0 0 1 1 1v2a2 2 0 0 1-2 2H3a2 2 0 0 1-2-2v-2Z" />
                  </svg>
                  2/4
                </span>
              </div>
              <div class="tooltip" data-tip="battery">
                <span
                  class="flex items-center justify-center gap-4 bg-base-200 w-24 h-10 border-2 border-accent rounded-lg">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-5">
                    <path
                      d="M4.75 8a.75.75 0 0 0-.75.75v2.5c0 .414.336.75.75.75H9.5a.75.75 0 0 0 .75-.75v-2.5A.75.75 0 0 0 9.5 8H4.75Z" />
                    <path fill-rule="evenodd"
                      d="M3.25 5A2.25 2.25 0 0 0 1 7.25v5.5A2.25 2.25 0 0 0 3.25 15h12.5A2.25 2.25 0 0 0 18 12.75v-1.085a1.5 1.5 0 0 0 1-1.415v-.5a1.5 1.5 0 0 0-1-1.415V7.25A2.25 2.25 0 0 0 15.75 5H3.25ZM2.5 7.25a.75.75 0 0 1 .75-.75h12.5a.75.75 0 0 1 .75.75v5.5a.75.75 0 0 1-.75.75H3.25a.75.75 0 0 1-.75-.75v-5.5Z"
                      clip-rule="evenodd" />
                  </svg>
                  50%
                </span>
              </div>
            </div>
          </div>
        </div>
        <div class="card bg-base-300 text-primary-content xl:w-96 lg:w-80 mt-5">
          <div class="card-body">
            <h2 class="card-title">
              AGV-1608
            </h2>
            <!-- Range dengan label kiri dan kanan -->
            <div class="flex items-center gap-4">
              <span class="text-sm text-white whitespace-nowrap">Factory A</span>
              <input type="range" min="0" max="100" value="40" class="range range-accent h-2" />
              <span class="text-sm text-white whitespace-nowrap">Factory B</span>
            </div>

            <div class="divider"></div>
            <div class="card-actions justify-center">
              <div class="tooltip" data-tip="status">
                <span
                  class="flex items-center justify-center gap-4 bg-base-200 xl:w-32 lg:w-24 h-10 border-2 border-accent rounded-lg">
                  <div class="status status-error animate-bounce"></div> Stop
                </span>
              </div>
              <div class="tooltip" data-tip="queue">
                <span
                  class="flex items-center justify-center gap-4 bg-base-200 w-24 h-10 border-2 border-accent rounded-lg">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-4">
                    <path fill-rule="evenodd"
                      d="M1.045 6.954a2.75 2.75 0 0 1 .217-.678L2.53 3.58A2.75 2.75 0 0 1 5.019 2h9.962a2.75 2.75 0 0 1 2.488 1.58l1.27 2.696c.101.216.174.444.216.678A1 1 0 0 1 19 7.25v1.5a2.75 2.75 0 0 1-2.75 2.75H3.75A2.75 2.75 0 0 1 1 8.75v-1.5a1 1 0 0 1 .045-.296Zm2.843-2.736A1.25 1.25 0 0 1 5.02 3.5h9.962c.484 0 .925.28 1.13.718l.957 2.032H14a1 1 0 0 0-.86.49l-.606 1.02a1 1 0 0 1-.86.49H8.236a1 1 0 0 1-.894-.553l-.448-.894A1 1 0 0 0 6 6.25H2.932l.956-2.032Z"
                      clip-rule="evenodd" />
                    <path
                      d="M1 14a1 1 0 0 1 1-1h4a1 1 0 0 1 .894.553l.448.894a1 1 0 0 0 .894.553h3.438a1 1 0 0 0 .86-.49l.606-1.02A1 1 0 0 1 14 13h4a1 1 0 0 1 1 1v2a2 2 0 0 1-2 2H3a2 2 0 0 1-2-2v-2Z" />
                  </svg>
                  3/4
                </span>
              </div>
              <div class="tooltip" data-tip="battery">
                <span
                  class="flex items-center justify-center gap-4 bg-base-200 w-24 h-10 border-2 border-accent rounded-lg">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-5">
                    <path
                      d="M4.75 8a.75.75 0 0 0-.75.75v2.5c0 .414.336.75.75.75H9.5a.75.75 0 0 0 .75-.75v-2.5A.75.75 0 0 0 9.5 8H4.75Z" />
                    <path fill-rule="evenodd"
                      d="M3.25 5A2.25 2.25 0 0 0 1 7.25v5.5A2.25 2.25 0 0 0 3.25 15h12.5A2.25 2.25 0 0 0 18 12.75v-1.085a1.5 1.5 0 0 0 1-1.415v-.5a1.5 1.5 0 0 0-1-1.415V7.25A2.25 2.25 0 0 0 15.75 5H3.25ZM2.5 7.25a.75.75 0 0 1 .75-.75h12.5a.75.75 0 0 1 .75.75v5.5a.75.75 0 0 1-.75.75H3.25a.75.75 0 0 1-.75-.75v-5.5Z"
                      clip-rule="evenodd" />
                  </svg>
                  50%
                </span>
              </div>
            </div>
          </div>
        </div>
        <div class="card bg-base-300 text-primary-content xl:w-96 lg:w-80 mt-5">
          <div class="card-body">
            <h2 class="card-title">
              AGV-1606
            </h2>

            <!-- Range dengan label kiri dan kanan -->
            <div class="flex items-center gap-4">
              <span class="text-sm text-white whitespace-nowrap">Factory A</span>
              <input type="range" min="0" max="100" value="40" class="range range-accent h-2" />
              <span class="text-sm text-white whitespace-nowrap">Factory B</span>
            </div>

            <div class="divider"></div>
            <div class="card-actions justify-center">
              <div class="tooltip" data-tip="status">
                <span
                  class="flex items-center justify-center gap-4 bg-base-200 xl:w-32 lg:w-24 h-10 border-2 border-accent rounded-lg">
                  <div class="status status-warning animate-bounce"></div> Warning
                </span>
              </div>
              <div class="tooltip" data-tip="queue">
                <span
                  class="flex items-center justify-center gap-4 bg-base-200 w-24 h-10 border-2 border-accent rounded-lg">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-4">
                    <path fill-rule="evenodd"
                      d="M1.045 6.954a2.75 2.75 0 0 1 .217-.678L2.53 3.58A2.75 2.75 0 0 1 5.019 2h9.962a2.75 2.75 0 0 1 2.488 1.58l1.27 2.696c.101.216.174.444.216.678A1 1 0 0 1 19 7.25v1.5a2.75 2.75 0 0 1-2.75 2.75H3.75A2.75 2.75 0 0 1 1 8.75v-1.5a1 1 0 0 1 .045-.296Zm2.843-2.736A1.25 1.25 0 0 1 5.02 3.5h9.962c.484 0 .925.28 1.13.718l.957 2.032H14a1 1 0 0 0-.86.49l-.606 1.02a1 1 0 0 1-.86.49H8.236a1 1 0 0 1-.894-.553l-.448-.894A1 1 0 0 0 6 6.25H2.932l.956-2.032Z"
                      clip-rule="evenodd" />
                    <path
                      d="M1 14a1 1 0 0 1 1-1h4a1 1 0 0 1 .894.553l.448.894a1 1 0 0 0 .894.553h3.438a1 1 0 0 0 .86-.49l.606-1.02A1 1 0 0 1 14 13h4a1 1 0 0 1 1 1v2a2 2 0 0 1-2 2H3a2 2 0 0 1-2-2v-2Z" />
                  </svg>
                  1/4
                </span>
              </div> 
              <div class="tooltip" data-tip="battery">
                <span
                  class="flex items-center justify-center gap-4 bg-base-200 w-24 h-10 border-2 border-accent rounded-lg">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" class="size-5">
                    <path
                      d="M4.75 8a.75.75 0 0 0-.75.75v2.5c0 .414.336.75.75.75H9.5a.75.75 0 0 0 .75-.75v-2.5A.75.75 0 0 0 9.5 8H4.75Z" />
                    <path fill-rule="evenodd"
                      d="M3.25 5A2.25 2.25 0 0 0 1 7.25v5.5A2.25 2.25 0 0 0 3.25 15h12.5A2.25 2.25 0 0 0 18 12.75v-1.085a1.5 1.5 0 0 0 1-1.415v-.5a1.5 1.5 0 0 0-1-1.415V7.25A2.25 2.25 0 0 0 15.75 5H3.25ZM2.5 7.25a.75.75 0 0 1 .75-.75h12.5a.75.75 0 0 1 .75.75v5.5a.75.75 0 0 1-.75.75H3.25a.75.75 0 0 1-.75-.75v-5.5Z"
                      clip-rule="evenodd" />
                  </svg>
                  50%
                </span>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Selection Layout -->
      <div class="col-span-2 row-span-1 bg-base-200 p-6 rounded-lg shadow summary-card">
        <div class="flex flex-row justify-center items-center">
          <div class="basis-1/3 " >
            <select class="select select-ghost xl:select-xl lg:select-lg ">
              <option>Production 1</option>
              <option>Production 2</option>
              <option>Production 3</option>
            </select>
          </div>
          <div class="basis-1/3 justify-center p-4 flex gap-2">
            <div class="tooltip" data-tip="AGV">
              <span class="flex items-center justify-center gap-4 bg-base-300 w-24 h-10 rounded-full">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="size-4">
                  <path fill-rule="evenodd"
                    d="M4.784 3A2.25 2.25 0 0 0 2.68 4.449L1.147 8.475A2.25 2.25 0 0 0 1 9.276v1.474A2.25 2.25 0 0 0 3.25 13h9.5A2.25 2.25 0 0 0 15 10.75V9.276c0-.274-.05-.545-.147-.801l-1.534-4.026A2.25 2.25 0 0 0 11.216 3H4.784Zm-.701 1.983a.75.75 0 0 1 .7-.483h6.433a.75.75 0 0 1 .701.483L13.447 9h-2.412a1 1 0 0 0-.832.445l-.406.61a1 1 0 0 1-.832.445h-1.93a1 1 0 0 1-.832-.445l-.406-.61A1 1 0 0 0 4.965 9H2.553l1.53-4.017Z"
                    clip-rule="evenodd" />
                </svg>
                14
              </span>
            </div>
            <div class="tooltip" data-tip="line">
              <span class="flex items-center justify-center gap-4 bg-base-300 w-24 h-10  rounded-full">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="size-4">
                  <path fill-rule="evenodd"
                    d="M15 11a2 2 0 0 1-2 2H3a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h10a2 2 0 0 1 2 2v6ZM7.25 7.5a.5.5 0 0 0-.5-.5H3a.5.5 0 0 0-.5.5V8a.5.5 0 0 0 .5.5h3.75a.5.5 0 0 0 .5-.5v-.5Zm1.5 3a.5.5 0 0 1 .5-.5H13a.5.5 0 0 1 .5.5v.5a.5.5 0 0 1-.5.5H9.25a.5.5 0 0 1-.5-.5v-.5ZM13.5 8v-.5A.5.5 0 0 0 13 7H9.25a.5.5 0 0 0-.5.5V8a.5.5 0 0 0 .5.5H13a.5.5 0 0 0 .5-.5Zm-6.75 3.5a.5.5 0 0 0 .5-.5v-.5a.5.5 0 0 0-.5-.5H3a.5.5 0 0 0-.5.5v.5a.5.5 0 0 0 .5.5h3.75Z"
                    clip-rule="evenodd" />
                </svg>
                8
              </span>
            </div>
            <div class="tooltip" data-tip="area">
              <span class="flex items-center justify-center gap-4 bg-base-300 w-24 h-10  rounded-full">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="size-4">
                  <path
                    d="M3.5 2A1.5 1.5 0 0 0 2 3.5v2A1.5 1.5 0 0 0 3.5 7h2A1.5 1.5 0 0 0 7 5.5v-2A1.5 1.5 0 0 0 5.5 2h-2ZM3.5 9A1.5 1.5 0 0 0 2 10.5v2A1.5 1.5 0 0 0 3.5 14h2A1.5 1.5 0 0 0 7 12.5v-2A1.5 1.5 0 0 0 5.5 9h-2ZM9 3.5A1.5 1.5 0 0 1 10.5 2h2A1.5 1.5 0 0 1 14 3.5v2A1.5 1.5 0 0 1 12.5 7h-2A1.5 1.5 0 0 1 9 5.5v-2ZM10.5 9A1.5 1.5 0 0 0 9 10.5v2a1.5 1.5 0 0 0 1.5 1.5h2a1.5 1.5 0 0 0 1.5-1.5v-2A1.5 1.5 0 0 0 12.5 9h-2Z" />
                </svg>
                4
              </span>
            </div>
          </div>
          <div class="basis-1/3 p-4">
            <label class="input input-md">
              <span class="label">Date</span>
              <input type="date" />
            </label>
          </div>
        </div>
      </div>

      <!-- Maps Layout -->
      <div class="col-span-2 row-span-7 bg-base-200 p-4 rounded-lg shadow summary-card">
         <Map2D />
        <!-- <div class="relative w-[1000px] h-[700px]">
          <img src="@/assets/layout.svg" alt="Peta AGV" class="w-full h-full" />

          <div v-for="agv in agvs" :key="agv.id" class="absolute w-4 h-4 rounded-full"
            :class="agv.status === 'error' ? 'bg-red-500' : 'bg-green-500'"
            :style="{ top: agv.y + 'px', left: agv.x + 'px' }" />
        </div> -->
      </div>          
    </div>
  </div>
</template>

<style>
  .summary-card {
    transition: transform 0.2s ease;
  }

  .summary-card:hover {
    transform: translateY(-4px);
  }
</style>

<script lang="ts">
import Map2D from './Map2D.vue'
import RobotActivityMapTracker from './RobotActivityMapTracker.vue'
import HistoricalData from './HistoricalData.vue'
import Maintenance  from './Maintenance.vue'
export default {
  components: {
     RobotActivityMapTracker,
     HistoricalData, 
     Map2D,
     Maintenance
  },
  computed:{
    currentComponent(){
      const view = this.$route.query.view
      switch(view){
        case 'tracker':
          return 'RobotActivityMapTracker'
        case 'historical' :
          return 'HistoricalData'
          case 'maintenance' :
          return 'Maintenance'
        default:
          return null
      }
    } 
  },
  data(){
    return{
      showAlert : false
    }
  },
  mounted() {
    this.showAlert = true
    setInterval(() =>{
      this.showAlert = false
    }, 10000)
  }
}

</script>
<style scoped>
.fade-enter-active,
    .fade-leave-active {
        transition: opacity 0.6s ease-in-out;
    }

    .fade-enter-from,
    .fade-leave-to {
        opacity: 0;
    }
    
html[data-theme='light'] .bg-base-200{
  background-color: #15191e;
} 

html[data-theme='light'] h2{
   color: #fff;
}

html[data-theme='light'] div .bg-base-300{
   background-color: #fff;
   color: #15191e;
}

html[data-theme='light'] .card-title{
   color: #15191e;

}
html[data-theme='light'] span{
   color: #15191e;
}

html[data-theme='light'] .tooltip span{
  background-color: #fff !important;
}

html[data-theme='light'] .select{
  background-color: #15191e;
  color: #fff;
}

html[data-theme='light'] .input{
  background-color: #fff;
}

</style>