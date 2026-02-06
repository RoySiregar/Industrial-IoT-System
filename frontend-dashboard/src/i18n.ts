import { createI18n } from 'vue-i18n'
import en from './locales/en.json'
import id from './locales/id.json'
import zh from './locales/zh.json'

const defaultLocale = localStorage.getItem('lang') || 'zh'
const i18n = createI18n({
  legacy: false,
  locale: defaultLocale, // default
  fallbackLocale: 'zh',
  messages: {
    en,
    id,
    zh
  }
})

export default i18n
