<template>
  <div
    ref="svgContainer"
    style="width: 1000px; height: 800px; background-color: transparent;"
    v-html="svgContent" class="map_agv"
  ></div>
</template>

<script setup lang="ts">
import { ref, onMounted, nextTick } from 'vue'


const svgContent = ref('')
const svgContainer = ref<HTMLDivElement | null>(null)

onMounted(async () => {
  const res = await fetch('./layout.svg')
  let rawSvg = await res.text()

  // Bersihkan width & height agar scalable
  rawSvg = rawSvg.replace(/<svg([^>]*)width="[^"]*"([^>]*)>/, '<svg$1$2>')
  rawSvg = rawSvg.replace(/<svg([^>]*)height="[^"]*"([^>]*)>/, '<svg$1$2>')
  rawSvg = rawSvg.replace(
    /<svg([^>]*)>/,
    '<svg$1 preserveAspectRatio="xMidYMid meet" style="width: 100%; height: 100%;">'
  )

  svgContent.value = rawSvg

  await nextTick()

  const svgDoc = svgContainer.value?.querySelector('svg')
  if (!svgDoc) return

  const agvs = svgDoc.querySelectorAll<SVGGElement>('g[id^="agv"]')
  console.log('AGVs found:', agvs.length)

  const positions = Array.from(agvs).map((agv) => {
    const transform = agv.getAttribute('transform') || 'translate(0,0)'
    const match = transform.match(/translate\(([-\d.]+),\s*([-\d.]+)\)/)
    let x = 0, y = 0
    if (match) {
      x = parseFloat(match[1])
      y = parseFloat(match[2])
    }
    return { agv, x, y }
  })

  setInterval(() => {
    positions.forEach((pos) => {
      pos.x += 2
      if (pos.x > 800) pos.x = 100
      pos.agv.setAttribute('transform', `translate(${pos.x},${pos.y})`)
    })
  }, 100)
})

</script>
