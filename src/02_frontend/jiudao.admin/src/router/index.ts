import { createRouter, createWebHistory } from 'vue-router'
import { constantRouterMap } from './router.config'



export const router = createRouter({
    history: createWebHistory(),
    routes: constantRouterMap,
})

