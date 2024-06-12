

import Home from '@/views/home/home.vue'
import type { RouteRecordRaw } from 'vue-router'

/**
 * 异步路由
 */
export const asyncRouterMap = [
    {
        path: '/',
        name: 'index',
        //   component: BasicLayout,
        meta: { title: 'menu.home' },
        redirect: '/home/home',
        children: [
            {
                path: '/home',
                name: 'home',
                component: Home,
                meta: { title: 'menu.home' },
            }
        ]
    },
    {
        path: '*',
        redirect: '/404',
        hidden: true
    }
]

/**
 * 基础路由
 */
// export const constantRouterMap: RouteRecordRaw[] = [
    export const constantRouterMap= [
    //   {
    //     path: '/user',
    //     component: UserLayout,
    //     redirect: '/user/login',
    //     hidden: true,
    //     children: [
    //       {
    //         path: 'login',
    //         name: 'login',
    //         component: () => import(/* webpackChunkName: "user" */ '@/views/user/Login')
    //       },
    //       {
    //         path: 'register',
    //         name: 'register',
    //         component: () => import(/* webpackChunkName: "user" */ '@/views/user/Register')
    //       },
    //       {
    //         path: 'register-result',
    //         name: 'registerResult',
    //         component: () => import(/* webpackChunkName: "user" */ '@/views/user/RegisterResult')
    //       },
    //       {
    //         path: 'recover',
    //         name: 'recover',
    //         component: undefined
    //       }
    //     ]
    //   },
    {
        path: '/',
        name: 'index',
        //   component: BasicLayout,
        meta: { title: 'menu.home' },
        redirect: '/home',
        children: [
            {
                path: '/home',
                name: 'home',
                component: Home,
                meta: { title: 'menu.home' },
            }
        ]
    },
    {
        path: '/404',
        component: () => import('@/views/exception/404.vue')
    },
    {
        path: '/:catchAll(.*)',
        redirect: '/404',
        hidden: true
    }
]