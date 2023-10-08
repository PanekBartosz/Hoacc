import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import Home from '../components/Home.vue'
import AboutUs from '../components/AboutUs.vue'

const routes: RouteRecordRaw[] = [
    { path: '/', name:"Home", component: Home },
    { path: '/about',name:"About", component: AboutUs },
  ]
  
  const router = createRouter({
    history: createWebHistory(),
    routes,
  })
  
  export default router