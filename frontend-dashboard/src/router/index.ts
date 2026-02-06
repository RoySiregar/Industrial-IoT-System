import { createRouter, createWebHashHistory } from 'vue-router'
import Login from '../components/Login.vue'
import Home from '../components/Home.vue'
import Dashboard from '../components/Dashboard.vue'
import Production from '../components/Production.vue'
import Monitoring from '../components/Monitoring.vue'
import Report from '../components/Report.vue'
import Settings from '../components/Settings.vue'
import Other from '../components/Other.vue'
import YieldRate from '../components/YieldRate.vue'
import Robot from '../components/Robot.vue'

const routes = [
{path:'/', redirect: '/login'},
{ path: '/login', component: Login },
{ path: '/home', component: Home },
{ path: '/dashboard', component: Dashboard },
{ path: '/production', component: Production },
{ path: '/robot', component: Robot },
{
    path: '/robot/:lineName/:robotName',
    name: 'RobotDetail',
    component: () => import('../components/RobotDetails.vue')
},
{ path: '/monitoring', component: Monitoring },
{ path: '/report', component: Report },
{ path: '/settings', component: Settings },
{ path: '/other', component: Other },
{
path: '/yield-rate',
name: 'YieldRate',  
component: YieldRate
}
]

const router = createRouter({
history: createWebHashHistory(),
routes,
})

router.beforeEach((to, _from, next) => {
  const isLoggedIn = localStorage.getItem('isLoggedIn') === 'true';
  const isLoginPage = to.path === '/login' || to.query.view === 'login';

  if (!isLoggedIn && !isLoginPage) {
    next({ path: '/login', query: { view: 'login' } });
  } else if (isLoggedIn && isLoginPage) {
    next({ path: '/dashboard', query: { view: 'dashboard' } });
  } else {
    next();
  }
});

export default router