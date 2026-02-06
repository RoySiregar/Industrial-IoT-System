import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import path from 'path'; 

export default defineConfig({
  base: './',
  plugins: [
    vue({
      template: {
        compilerOptions: {
          isCustomElement: tag => ['calendar-date', 'calendar-month'].includes(tag)
        }
      }
    }),
  ],

  resolve: {
    alias: {
      '@': path.resolve(__dirname, './src') 
    }
  },

  optimizeDeps: {
    include: [
      'highcharts',
      'highcharts/modules/solid-gauge',
      'highcharts/modules/gauge'
    ]
  }
  // build:{
  //   manifest: true,
  //   outDir: 'dist' ,
  //   rollupOptions:{
  //     input: './index.html'
  //   }
  // }
});
