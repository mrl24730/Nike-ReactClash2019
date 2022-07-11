import Vue from 'vue'
import App from './App.vue'
import router from './router'
import VueAnalytics from 'vue-analytics'
import VueI18n from 'vue-i18n'
import { messages } from './message'

Vue.use(VueI18n)
Vue.config.productionTip = false

Vue.use(VueAnalytics, {
  id: 'UA-103800537-22'
})

// Create VueI18n instance with options
const i18n = new VueI18n({
  locale: 'tc', // set locale
  messages, // set locale messages,
  fallbackLocale: 'tc'
})

new Vue({
  i18n,
  router,
  render: h => h(App)
}).$mount('#app')
