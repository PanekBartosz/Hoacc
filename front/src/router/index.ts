import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import Home from "../components/Home.vue";
import AboutUs from "../components/AboutUs.vue";
import LoginPage from "../components/LoginPage.vue";
import SignupPage from "../components/SignupPage.vue";
import MainDashboard from "../components/MainDashboard.vue";
import AuthPage from "../components/AuthPage.vue"

const routes: RouteRecordRaw[] = [
  { path: "/", name: "Home", component: Home },
  { path: "/about", name: "About", component: AboutUs },
  { path: "/login", name: "Login", component: LoginPage },
  { path: "/signup", name: "SignUp", component: SignupPage },
  {
    path: "/dashboard/:id",
    name: "Dashboard",
    component: MainDashboard,
    props: true,
  },
  { path: "/dashboard/notFound", name: "NotFound", component: AuthPage },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
