import { createApp } from 'vue'
import App from './App.vue'
import './assets/globalStyle.css'
import router from './router'
import initAuth from './components/authInit'

const app = createApp(App)
initAuth()
app.use(router)
app.mount('#app')
