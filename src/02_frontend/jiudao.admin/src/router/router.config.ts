import Home from "@/views/home/home.vue";
import BlankLayout from "@/components/layout/blank-layout.vue";
import Login from "@/views/login/index.vue";
import Error403 from "@/views/error/403.vue";
import Error404 from "@/views/error/404.vue";
import Error500 from "@/views/error/500.vue";
import Error from "@/views/error/error.vue";
import BasicLayout from "@/components/layout/basic-layout.vue";
import type { RouteRecordRaw } from "vue-router";

/**
 * 异步路由
 */
export const asyncRouterMap = [
  {
    path: "/sys",
    name: "sys",
    //   component: BasicLayout,
    meta: { title: "系统管理" },
    component: BasicLayout,
    children: [
      {
        path: "/sys/userinfo",
        name: "sys.userinfo",
        redirect: "/sys/userinfo/index",
        meta: { title: "menu.sys.userinfo" },
        children: [
          {
            path: "/sys/userinfo/index",
            name: "sys.userinfo.index",
            meta: { title: "menu.sys.userinfo.index" },
            icon: 'far fa-square-caret-right',
            component:() => import("@/views/pages/sys/userInfo/index.vue")
          },
        ],
      },
    ],
  },
  {
    path: "/:catchAll(.*)",
    redirect: "/404",
    hidden: true,
  },
];

/**
 * 基础路由
 */
// export const constantRouterMap: RouteRecordRaw[] = [
export const constantRouterMap = [
  {
    path: "/basic",
    name: "登录页",
    code: "login",
    component: BlankLayout,
    meta: { title: "登录页", code: "login" },
    redirect: "/login",
    children: [
      {
        path: "/login",
        name: "login",
        component: Login,
        meta: { title: "登录页", code: "login.login" },
      },
    ],
  },
  {
    path: "/",
    name: "index",
    component: BasicLayout,
    meta: { title: "menu.home", code: "menu.home" },
    redirect: "/home",
    children: [
      {
        path: "/home",
        name: "home",
        component: Home,
        meta: { title: "menu.home" },
      },
    ],
  },
  {
    path: "/err",
    name: "错误信息",
    code: "err",
    component: BlankLayout,
    meta: { title: "错误信息", code: "err" },
    children: [
      {
        name: "error.403",
        path: "/403",
        component: Error403,
        hidden: true,
        meta: { title: "403" },
      },
      {
        name: "error.404",
        path: "/404",
        component: Error404,
        hidden: true,
        meta: { title: "404" },
      },
      {
        name: "error.500",
        path: "/500",
        component: Error500,
        hidden: true,
        meta: { title: "500" },
      },
      {
        name: "error",
        path: "/error",
        component: Error,
        hidden: true,
        meta: { title: "error" },
      },
    ],
  },
  {
    path: "/:catchAll(.*)",
    redirect: "/404",
    hidden: true,
  },
];
