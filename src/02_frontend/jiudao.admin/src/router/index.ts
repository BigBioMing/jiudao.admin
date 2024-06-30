import { createRouter, createWebHistory } from 'vue-router'
import { constantRouterMap, asyncRouterMap } from './router.config'


const arr = [...constantRouterMap,...asyncRouterMap]
export const router = createRouter({
    history: createWebHistory(),
    routes: arr,
})

