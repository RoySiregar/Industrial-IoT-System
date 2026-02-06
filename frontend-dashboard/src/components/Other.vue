<template>
  <div class="other p-4 font-inter">
    <div class="carousel w-full" id="carousel">
      <div id="item1" class="carousel-item w-full">
        <img src="@/assets/img/shelf.gif" class="w-full" />
      </div>
      <div id="item2" class="carousel-item w-full">
        <img src="@/assets/img/agv.gif" class="w-full" />
      </div>
      <div id="item3" class="carousel-item w-full">
        <img src="@/assets/img/bg6.gif" class="w-full" />
      </div>
    </div>

    <div class="flex w-full justify-center gap-2 py-2">
      <button @click.prevent="scrollTo(1)" class="btn btn-xs">1</button>
      <button @click.prevent="scrollTo(2)" class="btn btn-xs">2</button>
      <button @click.prevent="scrollTo(3)" class="btn btn-xs">3</button>
    </div>
  </div>
</template>

<script setup >

import { onMounted, onUnmounted } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
let intervalId = null
let current = 1
const totalItems = 4

const scrollTo = (index) => {
  current = index
  const el = document.getElementById(`item${index}`)
  el?.scrollIntoView({ behavior: 'smooth', inline: 'start'})
}

onMounted(() => {
  if (route.path === '/other') {
    intervalId = setInterval(() => {
      current = current < totalItems ? current + 1 : 1
     scrollTo(current)
    }, 3000)
  }
})

onUnmounted(() => {
  clearInterval(intervalId)
})
</script>