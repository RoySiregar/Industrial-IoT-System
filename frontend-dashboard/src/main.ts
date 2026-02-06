
import { createApp } from 'vue'
import App from './App.vue'
import './assets/tailwind.css'  
import router from './router'
import i18n from './i18n'
import ECharts from 'vue-echarts'


const app = createApp(App)

app.use(router)
app.component('v-chart', ECharts)    
app.use(i18n)

app.mount('#app')
