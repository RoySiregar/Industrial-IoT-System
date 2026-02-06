<template>
  <Login v-if="!isLoggedIn" @login-success="goToHome" />

  <section v-else-if="currentComponent === 'Home' && isLoggedIn" class="flex min-h-screen ">

    <h2 class="font-poppins text-3xl font-bold mb-4">{{ currentComponentTitle }}</h2>
    <component :is="currentComponent" :key="$route.fullPath" :logged-in-user-type="loggedInUserType" />
  </section>

  <div v-else class="flex h-screen bg-base-200 text-base-content bg-light">
    <!-- Sidebar -->
    <div :class="[
        'bg-base-100 transition-all duration-300 max-w-80 rounded-xl shadow-xl xl:m-5 lg:m-3 md:m-3 sm:m-2 xs:m-2',
        isSidebarOpen ? 'w-[19rem] max-h-[96%] overflow-y-auto overflow-x-hidden no-scrollbar' : 'xl:w-24 lg:w-20 md:w-16 sm:w-18 xs:w-18 max-h-[96%] flex flex-col justify-between'
      ]">
      <div class="flex flex-col items-center">
        <!-- Logo -->
        <router-link :to="{ path: '/home', query: { view: 'home' } }" tabindex="0"
          class="inline-flex items-center xl:h-16 lg:h-12 md:h-10 sm:h-10 xs:h-10">
          <div class="flex mb-4 items-center xl:mt-6 lg:mt-6 md:mt-6 sm:mt-4 xs:mt-4">
            <div class="rounded bg-base-300 p-2 ">
              <img src="/public/logo.png"
                class="xl:w-6 xl:h-6 lg:w-3 lg:h-3 md:w-3 md:h-3 sm:w-3 sm:h-3 xs:w-3 xs:h-3" />
            </div>
            <span v-if="isSidebarOpen"
              class="ml-4 text-white xl:text-xl lg:text-sm md:text-sm sm:text-sm xs:text-sm font-popins font-semibold company">PEGA AUTO</span>
          </div>
        </router-link>

        <!-- Search -->
        <div v-if="!isSidebarOpen"
          :class="['w-full', isSidebarOpen ? '' : 'relative-group dropdown dropdown-right xl:mt-2 lg:mt-0 md:mt-0 sm:mt-0 xs:mt-0']">
          <div :class="['flex h-full justify-center items-center', isSidebarOpen ? 'flex' : 'flex-col']">
            <button @click.stop="toggleSearch"
              class="btn btn-ghost btn-circle btn-sm bg-transparent text-base-content border-none hover:scale-125 hover:shadow-[0px_4px_10px] transition-all duration-300">
              <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16"
                class="xl:size-4 lg:size-3 md:size-3  sm:size-3  xs:size-3 fill-white-search">
                <path fill-rule="evenodd"
                  d="M9.965 11.026a5 5 0 1 1 1.06-1.06l2.755 2.754a.75.75 0 1 1-1.06 1.06l-2.755-2.754ZM10.5 7a3.5 3.5 0 1 1-7 0 3.5 3.5 0 0 1 7 0Z"
                  clip-rule="evenodd" />
              </svg>
            </button>
          </div>
          <div tabindex="0" class="dropdown-content menu relative">
            <div :class="[
                isSidebarOpen ? 'w-full list' : 'absolute left-full top-0 ml-2 z-[9999] w-fit p-0',
                'list bg-base-200 rounded-lg shadow-inner '
              ]" v-if="isSearchOpen">
              <div class="card-body p-4">
                <label
                  class="input xl:w-64 lg:w-60 md:w-40 border-none bg-transparent shadow-none px-4 py-3 flex items-center gap-2">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="#ffffff"
                    class="xl:size-5 lg:size-4 md:size-3  sm:size-3  xs:size-3">
                    <path fill-rule="evenodd"
                      d="M9.965 11.026a5 5 0 1 1 1.06-1.06l2.755 2.754a.75.75 0 1 1-1.06 1.06l-2.755-2.754ZM10.5 7a3.5 3.5 0 1 1-7 0 3.5 3.5 0 0 1 7 0Z"
                      clip-rule="evenodd" />
                  </svg>
                  <input type="search" v-model="searchQuery"
                    class="grow xl:text-xs lg:text-xs md:text-[10px] sm:text-[10px] xs:text-[10px] text-white"
                    placeholder="Search Menu…" />
                  <kbd class="kbd xl:kbd-sm lg:kbd-xs md:kbd-xs sm:kbd-xs xs:kbd-xs">⌘</kbd>
                </label>

                <div class="border-b border-base-200"></div>
                <ul class="menu rounded-box xl:w-64 lg:w-60 md:w-40">
                  <template v-if="searchQuery">
                    <li
                      class="opacity-50 xl:text-xs lg:text-xs md:text-[10px] sm:text-[10px] xs:text-[10px] text-white font-semibold">
                      Search
                      Results</li>
                    <li v-for="item in filteredMenus" :key="item.id"
                      class=" font-inter xl:text-xs lg:text-xs md:text-[10px] sm:text-[10px] xs:text-[10px] text-white">
                      <router-link :to="{path: item.path || '/', query: {view: item.id }}">{{ $t(item.label) }}
                      </router-link>
                    </li>
                  </template>

                  <template v-else>
                    <li
                      class="opacity-50 xl:text-xs lg:text-xs md:text-[10px] sm:text-[10px] xs:text-[10px] text-white font-semibold">
                      Recent
                    </li>
                    <li v-for="item in recentMenus" :key="item.id">
                      <div class="flex justify-between font-inter text-white">
                        <router-link :to="{path: item.path || '/', query: {view: item.id}}"
                          class="flex-1 xl:text-xs lg:text-xs md:text-[10px] sm:text-[10px] xs:text-[10px]">
                          {{ $t(item.label) }}</router-link>
                        <button class="flex-2 text-red-500 hover:text-red-700 " @click="removeRecent(item.id)">
                          <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="oklch(63.7% 0.237 25.331)"
                            class="xl:size-5 lg:size-4 md:size-3">
                            <path fill-rule="evenodd"
                              d="M10 18a8 8 0 1 0 0-16 8 8 0 0 0 0 16ZM8.28 7.22a.75.75 0 0 0-1.06 1.06L8.94 10l-1.72 1.72a.75.75 0 1 0 1.06 1.06L10 11.06l1.72 1.72a.75.75 0 1 0 1.06-1.06L11.06 10l1.72-1.72a.75.75 0 0 0-1.06-1.06L10 8.94 8.28 7.22Z"
                              clip-rule="evenodd" />
                          </svg>
                        </button>
                      </div>
                    </li>
                  </template>
                </ul>
              </div>
            </div>
          </div>
        </div>

        <div v-if="isSidebarOpen"
          class="relative-group dropdown dropdown-right xl:mt-2 lg:mt-0 md:mt-0 sm:mt-0 xs:mt-0">
          <div :class="['flex h-full justify-center items-center', isSidebarOpen ? 'flex' : 'flex-col']">
            <label @click.stop="openSearch"
              class="input xl:w-64 lg:w-60 md:w-40 border-none bg-base-100 shadow-none px-4 py-3 flex items-center gap-2">
              <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor"
                class="xl:size-5 lg:size-4 md:size-3">
                <path fill-rule="evenodd"
                  d="M9.965 11.026a5 5 0 1 1 1.06-1.06l2.755 2.754a.75.75 0 1 1-1.06 1.06l-2.755-2.754ZM10.5 7a3.5 3.5 0 1 1-7 0 3.5 3.5 0 0 1 7 0Z"
                  clip-rule="evenodd" />
              </svg>

              <input type="search" v-model="searchQuery" @click="openSearch"
                class="grow xl:text-xs lg:text-xs md:text-[10px] sm:text-[10px] xs:text-[10px]"
                placeholder="Search Menu…" />
              <kbd class="kbd xl:kbd-sm lg:kbd-xs md:kbd-xs sm:kbd-xs xs:kbd-xs">⌘</kbd>

            </label>
          </div>
          <div v-if="isSearchOpen" ref="searchDropdown"
            class="absolute z-10 list bg-base-200 rounded-lg shadow-inner p-2" @click.outside="closeSearch">
            <ul class="menu rounded-box xl:w-64 lg:w-60 md:w-40">
              <template v-if="searchQuery">
                <li
                  class="opacity-50 xl:text-xs lg:text-xs md:text-[10px] sm:text-[10px] xs:text-[10px] text-white font-semibold">
                  Search
                  Results</li>
                <li v-for="item in filteredMenus" :key="item.id"
                  class=" font-inter xl:text-xs lg:text-xs md:text-[10px] sm:text-[10px] xs:text-[10px] text-white">
                  <router-link :to="{path: item.path || '/', query: {view: item.id }}">{{ $t(item.label) }}
                  </router-link>
                </li>
              </template>

              <template v-else>
                <li
                  class="opacity-50 xl:text-xs lg:text-xs md:text-[10px] sm:text-[10px] xs:text-[10px] text-white font-semibold">
                  Recent
                </li>
                <li v-for="item in recentMenus" :key="item.id">
                  <div class="flex justify-between font-inter">
                    <router-link :to="{path: item.path || '/', query: {view: item.id}}"
                      class="flex-1 xl:text-xs lg:text-xs md:text-[10px] sm:text-[10px] xs:text-[10px] text-white">
                      {{ $t(item.label) }}</router-link>
                    <button class="flex-2 text-red-500 hover:text-red-700 " @click="removeRecent(item.id)">
                      <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="oklch(63.7% 0.237 25.331)"
                        class="xl:size-5 lg:size-4 md:size-3">
                        <path fill-rule="evenodd"
                          d="M10 18a8 8 0 1 0 0-16 8 8 0 0 0 0 16ZM8.28 7.22a.75.75 0 0 0-1.06 1.06L8.94 10l-1.72 1.72a.75.75 0 1 0 1.06 1.06L10 11.06l1.72 1.72a.75.75 0 1 0 1.06-1.06L11.06 10l1.72-1.72a.75.75 0 0 0-1.06-1.06L10 8.94 8.28 7.22Z"
                          clip-rule="evenodd" />
                      </svg>
                    </button>
                  </div>
                </li>
              </template>
            </ul>
          </div>
        </div>

        <!-- Main Menu -->
        <ul ref="menuRef" @click.outside="closeSearch"
          :class="['menu xl:space-y-0 lg:space-y-0 xl:gap-2  `md:gap-1 xl:p-2 md:p-0' , isSidebarOpen ? '' : 'items-center']">

          <li class="menu-title xl:text-md lg:text-[10px] md:text-[8px] sm:text-[8px] xs:text-[8px]" v-if="isSidebarOpen">{{ $t('Main Menu') }}
          </li>

          <li v-for="menu in menus" v-if="isSidebarOpen" :key="menu.id" :class="[' relative group flex items-start rounded-lg ',
             activeSection === menu.id ? 'active shadow-inner bg-white/20' : 'hover:shadow-sm']">
            <details v-if="menu.children.length" :open="activeSection === menu.id">
              <summary :class="['text-default ', activeSection === menu.id ? 'text-default' : 'text-white arrow']">
                <div class="flex items-center">
                  <div class="w-1 h-5 mr-2" :class="activeSection === menu.id ? 'bg-default rounded-r' : ''"></div>

                  <!-- Menu utama -->
                  <router-link :to="{ path: menu.path || '/', query: { view: menu.id } }" tabindex="0"
                    @click="addToRecent({ label: menu.label, path: menu.path ?? '', query: { view: menu.id }, id: menu.id })">
                    <component :is="menu.icon" :class="[
                    'btn btn-sm btn-rounded text-base w-8 h-8 px-2 py-1 border-none icon',
                    activeSection === menu.id ? 'active bg-white/30 text-default' : 'bg-transparent text-base-content hover:bg-gray-300 ' 
                  ]" />

                    <span v-if="isSidebarOpen" :class="['ml-3 font-popins text-sm font-semibold menus',
                   activeSection === menu.id ? 'text-default' : 'text-base-content']">
                      {{ $t(menu.label) }}
                    </span>
                  </router-link>
                </div>
              </summary>

              <!-- Children -->
              <ul tabindex="0" class="p-4">
                <li v-for="child in menu.children" :key="child.label" :class="[
                'flex gap-2 text-default font-inter ',
                child.view === activeChildView ? 'font-bold underline text-white' : ''
              ]">
                  <router-link v-if="child.path" :to="{ path: child.path, query: { view: child.view }}"
                    class="hover:underline flex gap-2" @click="addToRecent({ 
                    label: child.label,
                    path: child.path ?? menu.path, 
                    query: { view: child.id },
                    id: child.id
                  }); ">
                    <span>{{ $t(child.label) }}</span>
                    <template v-if="menu.id === 'other'">
                      <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor"
                        class="size-4 icon">
                        <path
                          d="M6.22 8.72a.75.75 0 0 0 1.06 1.06l5.22-5.22v1.69a.75.75 0 0 0 1.5 0v-3.5a.75.75 0 0 0-.75-.75h-3.5a.75.75 0 0 0 0 1.5h1.69L6.22 8.72Z" />
                        <path
                          d="M3.5 6.75c0-.69.56-s1.25 1.25-1.25H7A.75.75 0 0 0 7 4H4.75A2.75 2.75 0 0 0 2 6.75v4.5A2.75 2.75 0 0 0 4.75 14h4.5A2.75 2.75 0 0 0 12 11.25V9a.75.75 0 0 0-1.5 0v2.25c0 .69-.56 1.25-1.25 1.25h-4.5c-.69 0-1.25-.56-1.25-1.25v-4.5Z" />
                      </svg>
                    </template>
                  </router-link>

                  <span v-else class="flex gap-2">
                    {{ $t(child.label) }}
                    <template v-if="menu.id === 'other'">
                      <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="size-4">
                        <path
                          d="M6.22 8.72a.75.75 0 0 0 1.06 1.06l5.22-5.22v1.69a.75.75 0 0 0 1.5 0v-3.5a.75.75 0 0 0-.75-.75h-3.5a.75.75 0 0 0 0 1.5h1.69L6.22 8.72Z" />
                        <path
                          d="M3.5 6.75c0-.69.56-1.25 1.25-1.25H7A.75.75 0 0 0 7 4H4.75A2.75 2.75 0 0 0 2 6.75v4.5A2.75 2.75 0 0 0 4.75 14h4.5A2.75 2.75 0 0 0 12 11.25V9a.75.75 0 0 0-1.5 0v2.25c0 .69-.56 1.25-1.25 1.25h-4.5c-.69 0-1.25-.56-1.25-1.25v-4.5Z" />
                      </svg>
                    </template>
                  </span>
                </li>
              </ul>
            </details>
          </li>

          <!-- Sidebar Close -->
          <li class="menu-title xl:text-md lg:text-[10px] md:text-[8px] sm:text-[8px] xs:text-[8px]"
            v-if="!isSidebarOpen">{{ $t('Menu') }}</li>
          <li v-for="menu in menus" v-if="!isSidebarOpen" :key="menu.id"
            class="relative group flex items-center justify-center">
            <div v-if="menu.id !== 'home' && menu.children.length"
              class="dropdown dropdown-hover dropdown-right dropdown-center relative z-[9999]" tabindex="0">
              <router-link v-if="menu.path" :to="{ path: menu.path || '/', query: { view: menu.id } }"
                @click="() =>{ toggleDropdown(menu.id); addToRecent({ label: menu.label, path: menu.path ?? '', query: { view: menu.id }, id: menu.id }); }"
                :class="[
                'btn xl:btn-sm lg:btn-sm md:btn-xs sm:btn-xs xs:btn-xs btn-rounded rounded-md text-base min-w-0 px-2 py-1 border-none xl:h-8 xl:w-8 lg:h-6 lg:w-6 md:h-6 md:w-6 sm:h-6 sm:w-6 xs:h-6 xs:w-6',
                activeSection === menu.id
                  ? 'dark:bg-white/30 text-default shadow-[0px_1px_4px] '
                  : 'bg-base-200 text-base-content hover:bg-gray-300 hover:text-white shadow-[0px_1px_4px] hover:shadow-[0px_4px_10px] try '
              ]">
                <component :is="menu.icon"
                  :class="['xl:w-4 xl:h-4 lg:w-4 lg:h-4 md:w-3 md:h-3 sm:w-4 sm:h-3 xs:w-4 xs:h-3 fill-white', activeSection === menu.id ? 'active-icon fill-[#6EB6C5]' : 'icon ']" />
              </router-link>

              <ul tabindex="0" v-if="menu.children.length" :class="[
                'dropdown-content menu-child absolute left-full top-0 ml-0 bg-base-300 rounded-box xl:w-52 lg:w-44 md:w-40 sm:w-40 xs:w-40 p-2 shadow font-poppins z-[9999]',
                openDropdownId === menu.id ? 'block' : 'hidden group-hover:block'
              ]">
                <li
                  class="label-menu font-bold xl:text-xs lg:text-[10px] md:text-[8px] sm:text-[8px] xs:text-[8px] px-2 py-1 pointer-events-none opacity-60 ">
                  {{ $t(menu.label) }}
                </li>
                <li v-for="child in menu.children" :key="child.label"
                  class="flex gap-2 xl:text-xs lg:text-[10px] md:text-[8px] sm:text-[8px] xs:text-[8px]">
                  <router-link v-if="child.path" :to="{ path: child.path, query: { view: child.view } }"
                    @click="addToRecent({ label: child.label, path: child.path ?? '',query: { view: child.view }, id: child.view });"
                    class="hover:underline flex gap-2">
                    <span>{{ $t(child.label) }}</span>
                  </router-link>
                  <span v-else class="flex gap-2">{{ $t(child.label) }}
                    <template v-if="menu.id === 'other'">
                      <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 16 16" fill="currentColor" class="size-4">
                        <path
                          d="M6.22 8.72a.75.75 0 0 0 1.06 1.06l5.22-5.22v1.69a.75.75 0 0 0 1.5 0v-3.5a.75.75 0 0 0-.75-.75h-3.5a.75.75 0 0 0 0 1.5h1.69L6.22 8.72Z" />
                        <path
                          d="M3.5 6.75c0-.69.56-1.25 1.25-1.25H7A.75.75 0 0 0 7 4H4.75A2.75 2.75 0 0 0 2 6.75v4.5A2.75 2.75 0 0 0 4.75 14h4.5A2.75 2.75 0 0 0 12 11.25V9a.75.75 0 0 0-1.5 0v2.25c0 .69-.56 1.25-1.25 1.25h-4.5c-.69 0-1.25-.56-1.25-1.25v-4.5Z" />
                      </svg>
                    </template>
                  </span>
                </li>
              </ul>
            </div>
          </li>
          <!-- /Sidebar Close -->

          <!-- Other --->
          <li class="menu-title xl:text-md lg:text-[10px] md:text-[8px] sm:text-[8px] xs:text-[8px]">{{$t('other')}}
          </li>
          <div :class="['w-full', isSidebarOpen ? '' : 'relative-group dropdown dropdown-right']">
            <div :class="['flex h-full justify-center items-center', isSidebarOpen ? 'flex' : 'flex-col']">
              <div
                :class="['flex rounded-lg  w-full justify-center items-center', isSidebarOpen ? 'flex gap-x-5  px-4 py-0 ' : 'flex-col xl:gap-y-5 md:gap-y-3 sm:gap-y-3 xs:gap-y-3 ']">
                <div class="indicator">
                  <button @click="setActive('email')"
                    class="transition-all duration-300 hover:scale-125 flex xl:h-8 xl:w-8 lg:h-6 lg:w-6 md:h-6 md:w-6 sm:h-6 sm:w-6 xs:h-6 xs:w-6 items-center justify-center rounded-md bg-base-200 text-[#6EB6C5] shadow-[0px_1px_4px] hover:shadow-[0px_4px_10px] setting">

                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="#6EB6C5"
                      class="xl:size-4 lg:size-3 md:size-2 sm:size-2 xs:size-2 ">
                      <path
                        d="M3 4a2 2 0 0 0-2 2v1.161l8.441 4.221a1.25 1.25 0 0 0 1.118 0L19 7.162V6a2 2 0 0 0-2-2H3Z" />
                      <path
                        d="m19 8.839-7.77 3.885a2.75 2.75 0 0 1-2.46 0L1 8.839V14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2V8.839Z" />
                    </svg>

                    <span
                      class="badge badge-xs xl:text-[10px] lg:text-[8px] md:text-[8px] badge-white indicator-item">10</span>

                  </button>
                </div>
                <div class="indicator">
                  <button @click="setActive('notification')"
                    class="transition-all duration-300 hover:scale-125 flex xl:h-8 xl:w-8 lg:h-6 lg:w-6 md:h-6 md:w-6 sm:h-6 sm:w-6 xs:h-6 xs:w-6 items-center justify-center rounded-md bg-base-200 text-[#6EB6C5] shadow-[0px_1px_4px] hover:shadow-[0px_4px_10px] setting">

                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="#6EB6C5"
                      class="xl:size-4 lg:size-3 md:size-2 sm:size-2 xs:size-2">
                      <path
                        d="M4.214 3.227a.75.75 0 0 0-1.156-.955 8.97 8.97 0 0 0-1.856 3.825.75.75 0 0 0 1.466.316 7.47 7.47 0 0 1 1.546-3.186ZM16.942 2.272a.75.75 0 0 0-1.157.955 7.47 7.47 0 0 1 1.547 3.186.75.75 0 0 0 1.466-.316 8.971 8.971 0 0 0-1.856-3.825Z" />
                      <path fill-rule="evenodd"
                        d="M10 2a6 6 0 0 0-6 6c0 1.887-.454 3.665-1.257 5.234a.75.75 0 0 0 .515 1.076 32.91 32.91 0 0 0 3.256.508 3.5 3.5 0 0 0 6.972 0 32.903 32.903 0 0 0 3.256-.508.75.75 0 0 0 .515-1.076A11.448 11.448 0 0 1 16 8a6 6 0 0 0-6-6Zm0 14.5a2 2 0 0 1-1.95-1.557 33.54 33.54 0 0 0 3.9 0A2 2 0 0 1 10 16.5Z"
                        clip-rule="evenodd" />
                    </svg>
                    <span
                      class="badge badge-xs xl:text-[10px] lg:text-[8px] md:text-[8px] badge-white indicator-item">8</span>

                  </button>
                </div>
                <button @click="setActive('language')"
                  class="transition-all duration-300 hover:scale-125 flex xl:h-8 xl:w-8 lg:h-6 lg:w-6 md:h-6 md:w-6 sm:h-6 sm:w-6 xs:h-6 xs:w-6 items-center justify-center rounded-md bg-base-200 text-[#6EB6C5] shadow-[0px_1px_4px] hover:shadow-[0px_4px_10px] setting">
                  <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="#6EB6C5"
                    class="xl:size-4 lg:size-3 md:size-2 sm:size-2 xs:size-2">
                    <path
                      d="M7.75 2.75a.75.75 0 0 0-1.5 0v1.258a32.987 32.987 0 0 0-3.599.278.75.75 0 1 0 .198 1.487A31.545 31.545 0 0 1 8.7 5.545 19.381 19.381 0 0 1 7 9.56a19.418 19.418 0 0 1-1.002-2.05.75.75 0 0 0-1.384.577 20.935 20.935 0 0 0 1.492 2.91 19.613 19.613 0 0 1-3.828 4.154.75.75 0 1 0 .945 1.164A21.116 21.116 0 0 0 7 12.331c.095.132.192.262.29.391a.75.75 0 0 0 1.194-.91c-.204-.266-.4-.538-.59-.815a20.888 20.888 0 0 0 2.333-5.332c.31.031.618.068.924.108a.75.75 0 0 0 .198-1.487 32.832 32.832 0 0 0-3.599-.278V2.75Z" />
                    <path fill-rule="evenodd"
                      d="M13 8a.75.75 0 0 1 .671.415l4.25 8.5a.75.75 0 1 1-1.342.67L15.787 16h-5.573l-.793 1.585a.75.75 0 1 1-1.342-.67l4.25-8.5A.75.75 0 0 1 13 8Zm2.037 6.5L13 10.427 10.964 14.5h4.073Z"
                      clip-rule="evenodd" />
                  </svg>
                </button>
              </div>
            </div>
            <div tabindex="0" class="dropdown-content menu relative">
        <div :class="[
            isSidebarOpen ? 'w-60 ' : 'absolute left-full top-0 ml-2 z-[9999] xl:w-52 lg:w-44 md:w-40 sm:w-40 xs:w-40  p-2',
            'list bg-base-200 rounded-lg shadow-inner '
          ]" v-if="activeSetting === 'email' ">
                <h2 class="menu-litle xl:text-md lg:text-xs md:text-[8px] opacity-40">{{$t('Inbox')}}</h2>
                <ul class="list">
                  <li class="list-row p-0">
                    <span
                      class="xl:text-xs lg:text-xs md:text-[10px] sm:text-[10px] xs:text-[10px] font-semibold text-white">10
                      Inbox</span>
                  </li>
                  <div class="p-4">
                    <button class="btn xl:btn-xs lg:btn-xs md:btn-xs sm:btn-xs xs:btn-xs btn-accent btn-block  "><span
                        class="xl:text-[10px] lg:text-[10px] md:text-[8px]">View All</span></button>
                  </div>

                </ul>
              </div>

              <div :class="[
            isSidebarOpen ? 'w-60' : 'absolute left-full top-0 ml-2 z-[9999] xl:w-52 lg:w-44 md:w-40 sm:w-40 xs:w-40  p-2',
            'list bg-base-200 rounded-lg shadow-inner'
          ]" v-if="activeSetting === 'notification'">

                <h2 class="menu-litle xl:text-md lg:text-xs md:text-[8px] opacity-40">{{$t('Notification')}}
                </h2>
                <ul class="list">
                  <li class="list-row p-0">
                    <span
                      class="xl:text-xs lg:text-xs md:text-[10px] sm:text-[10px] xs:text-[10px] font-semibold text-white">8
                      Notification</span>
                  </li>

                  <div class="p-4">
                    <button class="btn xl:btn-xs lg:btn-xs md:btn-xs sm:btn-xs xs:btn-xs btn-accent btn-block  "><span
                        class="xl:text-[10px] lg:text-[10px] md:text-[8px]">View All</span></button>
                  </div>
                </ul>
              </div>

              <div :class="[
              isSidebarOpen ? 'w-60' : 'absolute left-full top-0 ml-1 z-[9999] xl:w-52 lg:w-44 md:w-40 sm:w-40 xs:w-40 p-2', 
              'list bg-base-200 rounded-lg shadow-inner'
            ]" v-if="activeSetting === 'language'">
                <h2 class="menu-litle  xl:text-md lg:text-xs md:text-[8px] opacity-40">{{$t('Language Options')}}</h2>
                <ul class="list">
                  <li class="list-row xl:p-1 md:p-0" v-for="lang in languages" :key="lang.code">
                    <label class="xl:w-40 lg:w-36 md:w-36 sm:w-36  xs:w-36 flex gap-6 items-center cursor-pointer">
                      <input v-model="selectedLang" type="radio" :value="lang.code" name="language"
                        class="radio radio-xs radio-accent" @change="changeLanguage(lang.code)" />
                      <div class="font-inter xl:text-md lg:text-[10px] md:text-[8px] text-white">{{ lang.label }}</div>
                    </label>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </ul>
      </div>
      <div
        class="xl:mb-5 lg:mb-2 md:mb-1 xl:mt-0 lg:mt-2 md:mt-1 flex flex-col items-center xl:gap-4 lg:gap-2 md:gap-1 sm:gap-1 xs:gap-1 pb-4 ">
        <div
          class="badge badge-dash badge-accent badge-custom font-mono bg-base-300 place-self-center xl:w-16 xl:h-5 xl:text-xs lg:h-6 lg:w-12 lg:text-[10px] md:w-8 md:h-4 md:text-[7px] sm:w-8 sm:h-4 sm:text-[7px] xs:w-8 xs:h-4 xs:text-[7px] ">
          v1.7.5
          <span></span>
        </div>

        <div class="flex place-content-end inset-x-0 bottom-0 place-self-center  ">
          <label v-if="isSidebarOpen" class="inline-flex items-center relative mt-5 ">
            <input class="peer hidden" id="toggle" type="checkbox" @change="toggleTheme" />
            <div
              class="relative w-[275px] h-[50px] bg-base=100 peer-checked:bg-base-100 rounded-full after:absolute after:content-[''] after:w-[40px] after:h-[40px] after:bg-gradient-to-r from-orange-500 to-yellow-400 peer-checked:after:from-zinc-900 peer-checked:after:to-zinc-900 after:rounded-full after:top-[5px] after:left-[5px] active:after:w-[50px] peer-checked:after:left-[270px] peer-checked:after:translate-x-[-100%] shadow-sm duration-300 after:duration-300 after:shadow-md">
            </div>

            <svg height="0" width="100" viewBox="0 0 24 24" data-name="Layer 1" id="Layer_1"
              xmlns="http://www.w3.org/2000/svg"
              class="fill-white peer-checked:opacity-60 absolute w-6 h-6 left-[13px]">
              <path
                d="M12,17c-2.76,0-5-2.24-5-5s2.24-5,5-5,5,2.24,5,5-2.24,5-5,5ZM13,0h-2V5h2V0Zm0,19h-2v5h2v-5ZM5,11H0v2H5v-2Zm19,0h-5v2h5v-2Zm-2.81-6.78l-1.41-1.41-3.54,3.54,1.41,1.41,3.54-3.54ZM7.76,17.66l-1.41-1.41-3.54,3.54,1.41,1.41,3.54-3.54Zm0-11.31l-3.54-3.54-1.41,1.41,3.54,3.54,1.41-1.41Zm13.44,13.44l-3.54-3.54-1.41,1.41,3.54,3.54,1.41-1.41Z">
              </path>
            </svg>

            <svg height="512" width="512" viewBox="0 0 24 24" data-name="Layer 1" id="Layer_1"
              xmlns="http://www.w3.org/2000/svg"
              class="fill-black opacity-60 peer-checked:opacity-70 peer-checked:fill-white absolute w-6 h-6 right-[13px]">
              <path
                d="M12.009,24A12.067,12.067,0,0,1,.075,10.725,12.121,12.121,0,0,1,10.1.152a13,13,0,0,1,5.03.206,2.5,2.5,0,0,1,1.8,1.8,2.47,2.47,0,0,1-.7,2.425c-4.559,4.168-4.165,10.645.807,14.412h0a2.5,2.5,0,0,1-.7,4.319A13.875,13.875,0,0,1,12.009,24Zm.074-22a10.776,10.776,0,0,0-1.675.127,10.1,10.1,0,0,0-8.344,8.8A9.928,9.928,0,0,0,4.581,18.7a10.473,10.473,0,0,0,11.093,2.734.5.5,0,0,0,.138-.856h0C9.883,16.1,9.417,8.087,14.865,3.124a.459.459,0,0,0,.127-.465.491.491,0,0,0-.356-.362A10.68,10.68,0,0,0,12.083,2ZM20.5,12a1,1,0,0,1-.97-.757l-.358-1.43L17.74,9.428a1,1,0,0,1,.035-1.94l1.4-.325.351-1.406a1,1,0,0,1,1.94,0l.355,1.418,1.418.355a1,1,0,0,1,0,1.94l-1.418.355-.355,1.418A1,1,0,0,1,20.5,12ZM16,14a1,1,0,0,0,2,0A1,1,0,0,0,16,14Zm6,4a1,1,0,0,0,2,0A1,1,0,0,0,22,18Z">
              </path>
            </svg>
          </label>

          <label v-if="!isSidebarOpen"
            class="swap swap-rotate bg-transparent rounded-full items-center xl:w-10 xl:h-10 lg:w-10 lg:h-10 md:w-4 md:h-4  sm:w-4 sm:h-4  xs:w-4 xs:h-4 p-4  shadow-[0px_1px_4px] ">
            <input id="toggle" type="checkbox" @change="toggleTheme" />
            <svg class="swap-on fill-current text-yellow-400 size-5 " xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 24 24">
              <path
                d="M12,17c-2.76,0-5-2.24-5-5s2.24-5,5-5,5,2.24,5,5-2.24,5-5,5ZM13,0h-2V5h2V0Zm0,19h-2v5h2v-5ZM5,11H0v2H5v-2Zm19,0h-5v2h5v-2Zm-2.81-6.78l-1.41-1.41-3.54,3.54,1.41,1.41,3.54-3.54ZM7.76,17.66l-1.41-1.41-3.54,3.54,1.41,1.41,3.54-3.54Zm0-11.31l-3.54-3.54-1.41,1.41,3.54,3.54,1.41-1.41Zm13.44,13.44l-3.54-3.54-1.41,1.41,3.54,3.54,1.41-1.41Z">
              </path>
            </svg>

            <svg class="swap-off fill-current text-zinc-600 size-5" xmlns="http://www.w3.org/2000/svg"
              viewBox="0 0 24 24">
              <path
                d="M12.009,24A12.067,12.067,0,0,1,.075,10.725,12.121,12.121,0,0,1,10.1.152a13,13,0,0,1,5.03.206,2.5,2.5,0,0,1,1.8,1.8,2.47,2.47,0,0,1-.7,2.425c-4.559,4.168-4.165,10.645.807,14.412h0a2.5,2.5,0,0,1-.7,4.319A13.875,13.875,0,0,1,12.009,24Zm.074-22a10.776,10.776,0,0,0-1.675.127,10.1,10.1,0,0,0-8.344,8.8A9.928,9.928,0,0,0,4.581,18.7a10.473,10.473,0,0,0,11.093,2.734.5.5,0,0,0,.138-.856h0C9.883,16.1,9.417,8.087,14.865,3.124a.459.459,0,0,0,.127-.465.491.491,0,0,0-.356-.362A10.68,10.68,0,0,0,12.083,2ZM20.5,12a1,1,0,0,1-.97-.757l-.358-1.43L17.74,9.428a1,1,0,0,1,.035-1.94l1.4-.325.351-1.406a1,1,0,0,1,1.94,0l.355,1.418,1.418.355a1,1,0,0,1,0,1.94l-1.418.355-.355,1.418A1,1,0,0,1,20.5,12ZM16,14a1,1,0,0,0,2,0A1,1,0,0,0,16,14Zm6,4a1,1,0,0,0,2,0A1,1,0,0,0,22,18Z">
              </path>
            </svg>
          </label>
        </div>
        <div class="divider p-0 m-0"></div>
        <div :class="[isSidebarOpen ? 'dropdown dropdown-top': 'dropdown dropdown-right']">
          <div class="flex max-w-sm">
            <div tabindex="0" role="button"
              class="ring-1 ring-inset ring-default flex items-center justify-center rounded-md bg-base-100 text-default font-semibold"
              :class="[
                    'xl:w-10 xl:h-10',
                    'lg:w-6 lg:h-6',
                    'md:w-6 md:h-6',
                    'sm:w-6 sm:h-6',
                    'xs:w-6 xs:h-6'
                  ]">
              {{ userInitials }}
            </div>
            <div tabindex="0" role="button" class="wrap-anywhere" v-if="isSidebarOpen">
              <a class="ml-4 xl:text-sm lg:text-xs md:text-[10px] sm:text-[10px] xs:text-[10px]">Guest</a>
              <p class="ml-4 xl:text-xs  lg:text-[10px] md:text-[8px] sm:text-[8px] xs:text-[8px]">
                guest@intra.pegatroncorp.com</p>
            </div>
          </div>

          <ul tabindex="0"
            class="menu menu-sm dropdown-content bg-base-100 rounded-box z-1 xl:w-52 lg:w-40 md:w-28 p-2 shadow">
            <li class="text-base-content">
              <a class="justify-between xl:text-xs lg:text-xs md:text-[10px] sm:text-[10px] xs:text-[10px]">
                Profile
              </a>
            </li>
            <li class="text-base-content"><button @click="logout"><span
                  class="text-base-content xl:text-xs lg:text-xs md:text-[10px] sm:text-[10px] xs:text-[10px]">Logout</span></button>
            </li>
          </ul>
        </div>
      </div>
    </div>

    <!-- Main Content -->
    <div class="flex-1 overflow-y-auto" @click.outside="closeSearch">
      <div class="flex flex-wrap xl:m-2 lg:m-2 md:m-2 sm:m-2 xs:m-2 gap-4 items-center">
        <button class="btn btn-ghost btn-xs flex items-centerbg-transparent text-base-content " @click="toggleSidebar">
          <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" stroke-linejoin="round" stroke-linecap="round"
            stroke-width="2" fill="none" class="my-1.5 inline-block xl:size-4 lg:size-4 md:size-3 stroke-white">
            <path d="M4 4m0 2a2 2 0 0 1 2 -2h12a2 2 0 0 1 2 2v12a2 2 0 0 1 -2 2h-12a2 2 0 0 1 -2 -2z"></path>
            <path d="M9 4v16"></path>
            <path d="M14 10l2 2l-2 2"></path>
          </svg>
        </button>
        <!-- Breadcrumbs -->
        <div v-if="currentComponent"
          class="breadcrumbs xl:text-xs lg:text-xs md:text-[10px] sm:text-[10px] xs:text-[10px]">
          <ul>
            <li class="">
              {{ currentComponentTitle }}
            </li>
            <li v-if="currentComponent"
              class="xl:text-xs lg:text-xs md:text-[10px] sm:text-[10px] xs:text-[10px] text-accent">
              {{ currentChildernTitle }}
            </li>
          </ul>
        </div>

      </div>

      <section v-if="currentComponent" class="min-h-full p-4 bg-base-100">
        <component :is="currentComponent" :key="$route.fullPath" />
      </section>

      <!-- Footer -->
      <footer
        class="mt-8 items-center place-self-center text-center bg-base-200 text-white font-inter xl:text-xs lg:text-[10px] md:text-[8px] sm:text-[8px] xs:text-[8px]">
        <aside>
          <p>Copyright &copy; 2025 - All right reserved by PT. Pegaunihan Technology Indonesia</p>
        </aside>
      </footer>
    </div>

    <div class="toast">
      <div class="dropdown dropdown-top dropdown-end">
        <div tabindex="0" role="button"
          class="btn-ai xl:w-40 xl:h-10 lg:w-32 lg:h-8 md:w-24 md:h-6 sm:w-24 sm:h-6 xs:w-24 xs:h-6  items-center flex gap-4 xl:text-sm lg:text-xs md:text-[8px] sm:text-[8px] xs:text-[8px]">
          Pega AI<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor"
            class="xl:size-5 lg:size-4 md:size-3 sm:size-3 xs:size-3">
            <path
              d="M15.98 1.804a1 1 0 0 0-1.96 0l-.24 1.192a1 1 0 0 1-.784.785l-1.192.238a1 1 0 0 0 0 1.962l1.192.238a1 1 0 0 1 .785.785l.238 1.192a1 1 0 0 0 1.962 0l.238-1.192a1 1 0 0 1 .785-.785l1.192-.238a1 1 0 0 0 0-1.962l-1.192-.238a1 1 0 0 1-.785-.785l-.238-1.192ZM6.949 5.684a1 1 0 0 0-1.898 0l-.683 2.051a1 1 0 0 1-.633.633l-2.051.683a1 1 0 0 0 0 1.898l2.051.684a1 1 0 0 1 .633.632l.683 2.051a1 1 0 0 0 1.898 0l.683-2.051a1 1 0 0 1 .633-.633l2.051-.683a1 1 0 0 0 0-1.898l-2.051-.683a1 1 0 0 1-.633-.633L6.95 5.684ZM13.949 13.684a1 1 0 0 0-1.898 0l-.184.551a1 1 0 0 1-.632.633l-.551.183a1 1 0 0 0 0 1.898l.551.183a1 1 0 0 1 .633.633l.183.551a1 1 0 0 0 1.898 0l.184-.551a1 1 0 0 1 .632-.633l.551-.183a1 1 0 0 0 0-1.898l-.551-.184a1 1 0 0 1-.633-.632l-.183-.551Z" />
          </svg>
        </div>
        <ul tabindex="0" class="dropdown-content menu bg-base-100 rounded-box z-1 w-96 shadow-sm">
          <div class="w-full mx-auto bg-base-100 shadow-md rounded-lg overflow-hidden">
            <div class="flex flex-col h-[400px]">
              <div class="px-4 py-3 border-b border-zinc-700">
                <div class="flex justify-between items-center">
                  <h2 class="text-lg font-semibold text-white company">
                    Pega AI
                  </h2>
                  <div class="bg-accent text-white text-xs px-2 py-1 rounded-full">
                    Online
                  </div>
                </div>
              </div>
              <div class="flex-1 p-3 overflow-y-auto flex flex-col space-y-2" id="chatDisplay">
                <div class="chat-message self-start bg-zinc-500 text-white max-w-xs rounded-lg px-3 py-1.5 text-sm">
                  Hello! How can I help you?
                </div>
                <div class="chat-message self-end bg-accent text-white max-w-xs rounded-lg px-3 py-1.5 text-sm">
                </div>
              </div>
              <div class="px-3 py-2 border-t border-zinc-700">
                <div class="flex gap-2">
                  <input placeholder="Type your message..."
                    class="flex-1 p-2 border rounded-lg bg-base-300 text-white border-zinc-600 text-sm" id="chatInput"
                    type="text" />
                  <button
                    class="btn btn-dash btn-accent text-white font-bold rounded-lg transition duration-300 ease-in-out text-sm"
                    id="sendButton">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="white" class="size-5">
                      <path
                        d="M3.105 2.288a.75.75 0 0 0-.826.95l1.414 4.926A1.5 1.5 0 0 0 5.135 9.25h6.115a.75.75 0 0 1 0 1.5H5.135a1.5 1.5 0 0 0-1.442 1.086l-1.414 4.926a.75.75 0 0 0 .826.95 28.897 28.897 0 0 0 15.293-7.155.75.75 0 0 0 0-1.114A28.897 28.897 0 0 0 3.105 2.288Z" />
                    </svg>
                  </button>
                </div>
              </div>
            </div>
          </div>
        </ul>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
  import {
    useRoute
  } from 'vue-router'
  import Login from './components/Login.vue'
  import Home from './components/Home.vue'
  import Dashboard from './components/Dashboard.vue'
  import Production from './components/Production.vue'
  import Report from './components/Report.vue'
  import Monitoring from './components/Monitoring.vue'
  import Other from './components/Other.vue'
  import RealMonitoring from './components/RealMonitoring.vue'
  import OEEAnalytics from './components/OEEAnalytics.vue'
  import PerformanceOverview from './components/PerformanceOverview.vue'
  import YieldRate from './components/YieldRate.vue'
  import RobotActivityMapTracker from './components/RobotActivityMapTracker.vue'
  import AOIQueryData from './components/AOIQueryData.vue'
  import RobotDefectiveQueryData from './components/RobotDefectiveQueryData.vue'
  import FATP from './components/FATP.vue'
  import UphUpd from './components/UphUpd.vue'
  import DailyReport from './components/DailyReport.vue'
  import MonthlyReport from './components/MonthlyReport.vue'
  import WeeklyReport from './components/WeeklyReport.vue'
  import HistoricalData from './components/HistoricalData.vue'
  import Maintenance from './components/Maintenance.vue'
  import TeamSchedule from './components/TeamSchedule.vue'
  import Robot from './components/Robot.vue'
  import RobotDetail from './components/RobotDetails.vue'
  // import Note from './components/note.vue'

  import {
    ChartBarIcon,
    InformationCircleIcon,
    ComputerDesktopIcon,
    DocumentArrowDownIcon,
    SquaresPlusIcon,
    ChevronDoubleLeftIcon,
    ChevronDoubleRightIcon,
    CubeTransparentIcon
  } from '@heroicons/vue/24/solid'
  type SectionId = 'login' | 'home' | 'dashboard' | 'production' | 'robot' | 'monitoring' | 'report' | 'other' |
    'real' | 'oee' | 'performance' | 'yield' | 'tracker' | 'aoi' | 'defective' | 'fatp' | 'uphd' | 'daily' | 'weekly' |
    'monthly' | 'historical' | 'maintenance' | 'schedule'

  export default {
    components: {
      Login,
      Home,
      Dashboard,
      Production,
      Robot,
      Monitoring,
      Report,
      Other,
      RealMonitoring,
      OEEAnalytics,
      PerformanceOverview,
      YieldRate,
      RobotActivityMapTracker,
      AOIQueryData,
      RobotDefectiveQueryData,
      FATP,
      UphUpd,
      DailyReport,
      WeeklyReport,
      MonthlyReport,
      HistoricalData,
      Maintenance,
      TeamSchedule,
      RobotDetail
      // Note
    },
    data(): Record < string,
    any > {
      return {
        isLoggedIn: false,
        loggedInUserType: '',
        isSidebarOpen: false,
        isFAQOpen: false,
        activeSection: 'dashboard' as SectionId,
        activeChildView: '',
        activeSetting: 'email' as string | null,
        menus: [{
            id: 'dashboard',
            label: 'menu.dashboard',
            icon: ChartBarIcon,
            path: '/dashboard',
            component: Dashboard,
            children: [{
              id: 'real',
              label: 'menu.realtime',
              path: '/dashboard',
              view: 'real',
              component: RealMonitoring
            }, {
              id: 'oee',
              label: 'menu.oee',
              path: '/dashboard',
              view: 'oee',
              component: OEEAnalytics,
            }, {
              id: 'performance',
              label: 'menu.performance',
              path: '/dashboard',
              view: 'performance',
              component: PerformanceOverview,
            }]
          },
          {
            id: 'production',
            label: 'menu.production',
            icon: InformationCircleIcon,
            path: '/production',
            component: Production,
            children: [{
                id: 'yield',
                label: 'menu.yield',
                path: '/production',
                view: 'yield',
                component: YieldRate
              },
              {
                id: 'fatp',
                label: 'menu.fatp',
                path: '/production',
                view: 'fatp',
                component: FATP
              },
              {
                id: 'uphd',
                label: 'menu.uphd',
                path: '/production',
                view: 'uphd',
                component: UphUpd
              },
              {
                label: 'menu.error'
              }
            ]
          },
          {
            id: 'robot',
            label: 'menu.robot',
            icon: CubeTransparentIcon,
            path: '/robot',
            component: Robot,
            children: [{
                id: 'tracker',
                label: 'menu.tracker',
                path: '/robot',
                view: 'tracker',
                component: RobotActivityMapTracker
              },
              {
                id: 'detail',
                label: 'menu.robotDetail',
                path: '/robot',
                view: 'detail',
                component: RobotDetail
              },
              {
                id: 'aoi',
                label: 'menu.aoi',
                path: '/robot',
                view: 'aoi',
                component: AOIQueryData
              },
              // {
              //   label: 'menu.packing'
              // },
              // {
              //   label: 'menu.vacuum'
              // },
              {
                id: 'defective',
                label: 'menu.defective',
                path: '/robot',
                view: 'defective',
                component: RobotDefectiveQueryData
              },
              // {
              //   id: 'note',
              //   label: 'menu.note',
              //   path: '/robot',
              //   view: 'note',
              //   component: Note
              // }
            ]
          },
          {
            id: 'monitoring',
            label: 'menu.monitoring',
            icon: ComputerDesktopIcon,
            path: '/monitoring',
            component: Monitoring,
            children: [{
              id: 'historical',
              label: 'menu.historical',
              path: '/monitoring',
              view: 'historical',
              component: HistoricalData
            }, {
              id: 'maintenance',
              label: 'menu.maintenance',
              path: '/monitoring',
              view: 'maintenance',
              component: Maintenance
            }, {
              id: 'schedule',
              label: 'menu.schedule',
              path: '/monitoring',
              view: 'schedule',
              component: TeamSchedule
            }]
          },
          {
            id: 'report',
            label: 'menu.report',
            icon: DocumentArrowDownIcon,
            path: '/report',
            component: Report,
            children: [{
                id: 'daily',
                label: 'menu.daily',
                path: '/report',
                view: 'daily',
                component: DailyReport
              }, {
                id: 'weekly',
                label: 'menu.weekly',
                path: '/report',
                view: 'weekly',
                component: WeeklyReport
              },
              {
                id: 'monthly',
                label: 'menu.monthly',
                path: '/report',
                view: 'monthly',
                component: MonthlyReport
              }
            ]
          },
          {
            id: 'other',
            label: 'menu.other',
            icon: SquaresPlusIcon,
            path: '/other',
            children: [{
              label: 'menu.automation_admin'
            }, {
              label: 'menu.bg6'
            }, {
              label: 'menu.shelf'
            }, {
              label: 'menu.agv'
            }]
          }
        ],
        icon: {
          left: ChevronDoubleLeftIcon,
          right: ChevronDoubleRightIcon
        },
        selectedLang: this.$i18n.locale || 'zh',
        languages: [{
            code: 'en',
            label: this.$t('English')
          },
          {
            code: 'zh',
            label: this.$t('Chinese (Traditional)')
          },
          {
            code: 'zh',
            label: this.$t('Chinese (Simplified)')
          },
          {
            code: 'id',
            label: this.$t('Indonesia')
          }
        ],
        openDropdownId: null,
        // currentPath: ref(window.location.hash),
        route: useRoute(),
        // recentMenus: JSON.parse(localStorage.getItem("recentMenus")) || [],
        recentMenus: [],
        maxRecent: 3,
        isSearchOpen: false,
        searchQuery: '',
        userName: 'Guest'
      }
    },
    methods: {
      goToHome(userType: string) {
        this.isLoggedIn = true;
        this.loggedInUserType = userType;
        // this.currentComponent = 'Home';
      },
      toggleTheme() {
        const currentTheme = document.documentElement.getAttribute('data-theme');
        const newTheme = currentTheme === 'light' ? 'dark' : 'light';
        document.documentElement.setAttribute('data-theme', newTheme);
        localStorage.setItem('theme', newTheme);
      },
      changeLanguage(code: string) {
        console.log('Language switched to:', code)
        this.$i18n.locale = code
        localStorage.setItem('lang', code)
      },
      setActive(setting: string) {
        this.activeSetting = setting
      },
      toggleSidebar() {
        this.isSidebarOpen = !this.isSidebarOpen
      },
      toggleDropdown(menuId: any) {
        this.openDropdownId = this.openDropdownId === menuId ? null : menuId;
      },
      handleClickOutside(event: {
        target: any
      }) {
        if (!this.$el.contains(event.target)) {
          this.openDropdownId = null;
          this.isSearchOpen = false
        }
      },
      logout() {
        localStorage.removeItem('isLoggedIn');
        localStorage.removeItem('userType');
        this.isSearchOpen = false
        this.openDropdownId = null
        this.isLoggedIn = false;
        this.loggedInUserType = '';
        this.$router.replace({
          path: '/',
          query: {
            view: 'login'
          }
        });
      },
      addToRecent(menu: {
          id: any;label: string;path: string;query: {
          view: any
        };
      }) {
        this.recentMenus = this.recentMenus.filter((item: {
          id: any
        }) => item.id !== menu.id)
        this.recentMenus.unshift(menu)
        if (this.recentMenus.length > this.maxRecent) this.recentMenus.pop()
        localStorage.setItem('recentMenus', JSON.stringify(this.recentMenus))

        setTimeout(() => {
          window.location.reload()
        }, 5)
      },
      removeRecent(id: string) {
        this.recentMenus = this.recentMenus.filter((item: {
          id: string
        }) => item.id != id)
        localStorage.setItem("recentMenus", JSON.stringify(this.recentMenus))
      },
      toggleSearch() {
        this.isSearchOpen = !this.isSearchOpen
      },
      openSearch() {
        this.isSearchOpen = true
      },
      closeSearch() {
        this.isSearchOpen = false
      }
      // reloadPage() {
      //   requestAnimationFrame(() => {
      //     setTimeout(() => {
      //       window.location.reload();
      //     }, 10);
      //   });
      // },
      // async startVersionChecker(){
      //   console.log("CALLED!");
      //   const CHECK_INTERVAL = 60 * 1000;
      //   let lastHash = localStorage.getItem("appHash")

      //   const check = async () => {
      //     try{
      //        console.log("CHECK RUNNING..."); 
      //        const res = await fetch("manifest.json?cache=" + Date.now());
      //        const manifest = await res.json();
      //        const newHash = manifest["index.html"].file;
      //        console.log("lastHash:", lastHash);
      //        console.log("newHash:", newHash);
      //       if(!lastHash){
      //         localStorage.setItem("appHash", newHash)
      //         lastHash = newHash
      //         return;
      //       }

      //       if(newHash !== lastHash){
      //         console.log("UPDATE DETECTED!");
      //         localStorage.setItem("appHash", newHash)
      //         window.location.reload()
      //       }
      //     } catch(err){
      //       console.error("Version chcek error:", err)
      //     }
      //   }
      //   check()
      //   setInterval(check, CHECK_INTERVAL)
      // }

    },
    created() {
      const isLoggedIn = localStorage.getItem('isLoggedIn') === 'true';
      const userType = localStorage.getItem('userType') || '';

      this.isLoggedIn = isLoggedIn;
      this.loggedInUserType = userType;

      const currentView = this.$route.query.view;
      if (isLoggedIn && (currentView === 'login' || this.$route.path === '/login')) {
        this.$router.replace({
          path: '/home',
          query: {
            view: 'home'
          }
        });
      }
      const savedLang = localStorage.getItem('lang')
      if (savedLang) {
        this.selectedLang = savedLang
      }
    },
    computed: {
      currentComponent() {
        const view = this.$route.query.view
        switch (view) {
          case 'login':
            return 'Login'
          case 'home':
            return 'Home'
          case 'dashboard':
            return 'Dashboard'
          case 'production':
            return 'Production'
          case 'robot':
            return 'Robot'
          case 'monitoring':
            return 'Monitoring'
          case 'report':
            return 'Report'
          case 'other':
            return 'Other'
          case 'real':
            return 'RealMonitoring'
          case 'oee':
            return 'OEEAnalytics'
          case 'performance':
            return 'PerformanceOverview'
          case 'yield':
            return 'YieldRate'
          case 'tracker':
            return 'RobotActivityMapTracker'
          case 'aoi':
            return 'AOIQueryData'
          case 'defective':
            return 'RobotDefectiveQueryData'
          case 'fatp':
            return 'FATP'
          case 'uphd':
            return 'UphUpd'
          case 'daily':
            return 'DailyReport'
          case 'weekly':
            return 'WeeklyReport'
          case 'monthly':
            return 'MonthlyReport'
          case 'historical':
            return 'HistoricalData'
          case 'maintenance':
            return 'Maintenance'
          case 'schedule':
            return 'TeamSchedule'
          case 'note':
            return 'Note'
          case 'detail':
            return 'RobotDetail'
          default:
            return 'Login'
        }
      },
      currentComponentTitle(): string {
        for (const menu of this.menus) {
          if (menu.path === this.currentPath) {
            return this.$t(menu.label)
          }
        }
        return ''
      },
      currentChildernTitle(): string {
        for (const menu of this.menus) {
          if (menu.path === this.currentPath) {
            const child = menu.children ?.find((c: {
              view: string
            }) => c.view === this.currentView)
            return this.$t(child ?.label || '')
          }
        }
        return ''
      },
      currentPath(): string {
        return this.$route.path
      },
      currentView(): string {
        const view = this.$route.query.view
        return typeof view === 'string' ? view : ''
      },
      allMenuItems() {
        let items: any[] = [];

        this.menus.forEach((menu: {
          path: any;id: any;label: any;children: any[]
        }) => {
          if (menu.path) {
            items.push({
              id: menu.id,
              label: menu.label,
              path: menu.path,
              view: menu.id,
            });
          }

          if (menu.children) {
            menu.children.forEach(child => {
              if (child.path) {
                items.push({
                  id: child.view,
                  label: child.label,
                  path: child.path,
                  view: child.view
                });
              }
            });
          }
        });
        return items;
      },
      filteredMenus() {
        return this.allMenuItems.filter(item =>
          this.$t(item.label).toLowerCase().includes(this.searchQuery.toLowerCase())
        );
      },
      userInitials() {
        if (!this.userName) return 'G'
        return this.userName
          .split(' ')
          .map((n: any[]) => n[0])
          .join('')
          .slice(0, 2)
          .toUpperCase()
      }

    },
    mounted() {
      const savedTheme = localStorage.getItem('theme');
      const systemPrefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;

      const currentTheme = savedTheme || (systemPrefersDark ? 'dark' : 'light');
      document.documentElement.setAttribute('data-theme', currentTheme);

      if (savedTheme) {
        localStorage.setItem('theme', savedTheme);
      }

      const toggle = document.querySelector < HTMLInputElement > ('#toggle');
      if (toggle) {
        toggle.checked = currentTheme === 'light';
      }

      document.addEventListener('click', this.handleClickOutside);

      const stored = JSON.parse(localStorage.getItem("recentMenus") ?? "[]");
      this.recentMenus = stored.slice(0, this.maxRecent);

      // this.startVersionChecker()
    },

    beforeUnmount() {
      document.removeEventListener('click', this.handleClickOutside);
    },

    watch: {
      '$route.query.view': {
        immediate: true,
        handler(newView: string) {
          const sectionMap: Record < string, string > = {
            real: 'dashboard',
            oee: 'dashboard',
            yield: 'production',
            tracker: 'robot',
            aoi: 'robot',
            defective: 'robot',
            fatp: 'production',
            uphd: 'production',
            daily: 'report',
            weekly: 'report',
            monthly: 'report',
            historical: 'monitoring',
            maintenance: 'monitoring',
            // note: 'robot'
          };

          if (sectionMap[newView]) {
            this.activeSection = sectionMap[newView];
          } else if (['dashboard', 'production', 'robot', 'monitoring', 'report', 'other'].includes(newView)) {
            this.activeSection = newView;
          } else {
            this.activeSection = 'dashboard';
          }
          this.activeChildView = newView;
        }
      }
    }
  }
</script>
<style scoped>
  html,
  body {
    margin: 0;
    padding: 0;
    scroll-behavior: smooth;
  }

  .icon-color {
    fill: white;
    transition: fill 0.3s ease;
  }

  .min-h-full {
    min-height: 78%;
  }

  html[data-theme='dark'] .icon-color {
    fill: black !important;
  }

  button {
    color: black !important;
  }

  html[data-theme='dark'] .try {
    color: black !important;
  }

  html[data-theme='dark'] .menu {
    color: rgb(255, 255, 255) !important;
  }

  html[data-theme='dark'] .label-menu {
    color: rgb(255, 255, 255) !important;
  }

  html[data-theme='light'] section {
    background-color: #eeeeee !important;
  }

  html[data-theme='light'] .bg-base-100 {
    background-color: #eeeeee;
  }

  html[data-theme='light'] .rounded {
    background-color: #15191e;
  }

  html[data-theme='light'] .company {
    color: #15191e !important;
  }

  html[data-theme='light'] .menu-title {
    color: #15191e !important;
  }

  html[data-theme='light'] .btn {
    background-color: #15191e;
  }

  html[data-theme='light'] .btn-ghost {
    background-color: transparent;
  }

  html[data-theme='light'] .icon {
    fill: rgb(255, 255, 255);
  }

  html[data-theme='light'] .arrow {
    color: #15191e;
  }

  html[data-theme='light'] .active {
    background-color: #15191e !important;
    color: white !important;
    fill: #6EB6C5;
  }

  html[data-theme='light'] .bg-light {
    background-color: #ddd;
  }

  html[data-theme='light'] .child-label {
    color: #15191e;
  }

  html[data-theme='light'] .setting {
    background-color: #15191e;
  }

  html[data-theme='light'] .list {
    background-color: #15191e !important;
    border-radius: 10px;
  }

  html[data-theme='light'] .select {
    background-color: #1d232a !important;
  }

  html[data-theme='light'] .toggle {
    background-color: #eee;
  }

  html[data-theme='light'] .menu-child {
    color: rgb(255, 255, 255) !important;
    background-color: #15191e !important;
  }

  html[data-theme='light'] .btn-accent {
    background-color: #00d3bb;
  }

  html[data-theme='light'] footer {
    background-color: #ddd;
    color: #15191e;
  }

  html[data-theme='light'] .btn-ai {
    background-color: #00d3bb !important;
    color: #fff;
    box-shadow: 0 0 1em .25em var(--glow-color),
      0 0 3em 1em var(--glow-spread-color),
      inset 0 0 .75em .25em var(--glow-color);
    --glow-color: rgb(176, 255, 248);
    --glow-spread-color: rgba(123, 255, 237, 0.781);
    --enhanced-glow-color: rgb(206, 255, 251);
    --btn-color: rgb(95, 186, 189);
  }

  html[data-theme='light'] .stroke-white {
    stroke: #15191e;
  }

  html[data-theme='light'] .fill-white-search {
    fill: #15191e;
  }

  .fill-white-search {
    fill: #ffffff;
  }

  .menu-litle {
    padding-inline: calc(0.25rem * 3);
    padding-block: calc(0.25rem * 2);
    /* font-size: 0.7rem; */
    /* font-weight: 600; */
    color: white;
  }

  .fade-enter-active,
  .fade-leave-active {
    transition: opacity 0.1s;
  }

  .fade-enter-from,
  .fade-leave-to {
    opacity: 0;
  }

  .btn-ai {
    box-shadow: 0 0 1em .25em var(--glow-color),
      0 0 2em 1em var(--glow-spread-color),
      inset 0 0 .75em .25em var(--glow-color);
    --glow-color: rgb(176, 255, 248);
    --glow-spread-color: rgba(123, 255, 237, 0.781);
    --enhanced-glow-color: rgb(206, 255, 251);
    --btn-color: rgb(95, 186, 189);
    backdrop-filter: blur(20px);
    border-radius: 15px;
    padding: 0.5em;
    justify-content: center;
  }

  .badge-custom {
    text-shadow: 0 1px 2px rgba(0, 0, 0, 0.2);
    /* background: #15191e; */
    backdrop-filter: blur(10px);
    color: white;
    -webkit-backdrop-filter: blur(10px);
    border: 1px solid rgba(255, 255, 255, 0.18);
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
  }


  .badge-custom span {
    width: 25px;
    height: 25px;
    position: absolute;
    top: -12px;
    right: -2px;
    transform: rotate(-20deg);
    filter: blur(0.5px);
  }

  .badge-custom span:before,
  .badge-custom span:after {
    content: "";
    position: absolute;
  }

  .badge-custom span:before {
    width: 1px;
    height: 100%;
    left: 12px;
    background: linear-gradient(to bottom,
        transparent,
        rgba(255, 255, 255, 0.7),
        transparent);
  }

  .badge-custom span:after {
    width: 100%;
    height: 1px;
    top: 12px;
    background: linear-gradient(to left,
        transparent,
        rgba(255, 255, 255, 0.7),
        transparent);
  }

  .badge-custom:hover span:after {
    display: block;
    animation: rotate 3s ease-in-out;
    /* Adjust timing as needed */
  }

  .badge-custom:hover span::before {
    display: block;
    animation: rotate 3s ease-in-out;
    /* Adjust timing as needed */
  }

  @keyframes rotate {
    0% {
      transform: rotate(0deg) scale(1);
    }

    50% {
      transform: rotate(180deg) scale(1.8);
    }

    100% {
      transform: rotate(360deg) scale(1);
    }
  }

  .badge-custom:before {
    content: "";
    position: absolute;
    z-index: -1;
    background: rgba(255, 255, 255, 0.25);
    box-shadow: 0 8px 32px 0 rgba(105, 106, 111, 0.37);
    backdrop-filter: blur(10px);
    -webkit-backdrop-filter: blur(10px);
    border: 1px solid rgba(255, 255, 255, 0.18);
    top: -1px;
    right: -1px;
    bottom: -1px;
    left: -1px;
    border-radius: 999px;
  }

  .inspired {
    position: absolute;
    bottom: 8%;
    color: rgba(255, 255, 255, 0.5);
    font-size: 12px;
    text-decoration: none;
    transition: color 0.2s ease;
  }

  .inspired:hover {
    color: rgba(255, 255, 255, 0.8);
  }

  .no-scrollbar::-webkit-scrollbar {
    width: 0px;
    height: 0px;
  }

  .no-scrollbar {
    scrollbar-width: none;
    -ms-overflow-style: none;
  }

  .lg\:range-custom {
    --range-thumb-size: calc(var(--size-selector, 0.25rem
          /* 4px */
        ) * 2);
  }

  .md\:range-custom {
    --range-thumb-size: calc(var(--size-selector, 0.25rem
          /* 4px */
        ) * 2);
  }

  .sm\:range-custom {
    --range-thumb-size: calc(var(--size-selector, 0.25rem
          /* 4px */
        ) * 2);
  }

  .xs\:range-custom {
    --range-thumb-size: calc(var(--size-selector, 0.25rem
          /* 4px */
        ) * 2);
  }

  .badge-white {
    background-color: #6EB6C5;
    color: #15191e;
    border: none;

  }
</style>