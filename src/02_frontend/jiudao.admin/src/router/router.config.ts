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
    meta: { title: "系统管理", name: "系统管理" },
    component: BasicLayout,
    children: [
      {
        path: "/sys/userinfo",
        name: "sys.userinfo",
        redirect: "/sys/userinfo/index",
        meta: { title: "menu.sys.userinfo", name: "用户管理" },
        children: [
          {
            path: "/sys/userinfo/index",
            name: "sys.userinfo.index",
            meta: { title: "menu.sys.userinfo.index", name: "用户管理" },
            icon: "far fa-square-caret-right",
            component: () => import("@/views/pages/sys/userInfo/index.vue"),
          },
        ],
      },
      {
        path: "/sys/role",
        name: "sys.role",
        redirect: "/sys/role/index",
        meta: { title: "menu.sys.role" },
        children: [
          {
            path: "/sys/role/index",
            name: "sys.role.index",
            meta: { title: "menu.sys.role.index" },
            icon: "far fa-square-caret-right",
            component: () => import("@/views/pages/sys/role/index.vue"),
          },
        ],
      },
      {
        path: "/sys/organization",
        name: "sys.organization",
        redirect: "/sys/organization/index",
        meta: { title: "menu.sys.organization" },
        children: [
          {
            path: "/sys/organization/index",
            name: "sys.organization.index",
            meta: { title: "menu.sys.organization.index" },
            icon: "far fa-square-caret-right",
            component: () => import("@/views/pages/sys/organization/index.vue"),
          },
        ],
      },
      {
        path: "/sys/routeResource",
        name: "sys.routeResource",
        redirect: "/sys/routeResource/index",
        meta: { title: "menu.sys.routeResource" },
        children: [
          {
            path: "/sys/routeResource/index",
            name: "sys.routeResource.index",
            meta: { title: "menu.sys.routeResource.index" },
            icon: "far fa-square-caret-right",
            component: () =>
              import("@/views/pages/sys/routeResource/index.vue"),
          },
        ],
      },
      {
        path: "/sys/dictionaryDefine",
        name: "sys.dictionaryDefine",
        redirect: "/sys/dictionaryDefine/index",
        meta: { title: "menu.sys.dictionaryDefine" },
        children: [
          {
            path: "/sys/dictionaryDefine/index",
            name: "sys.dictionaryDefine.index",
            meta: { title: "menu.sys.dictionaryDefine.index" },
            icon: "far fa-square-caret-right",
            component: () =>
              import("@/views/pages/sys/dictionaryDefine/index.vue"),
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
export const constantRouterMap: RouteRecordRaw[] = [
  {
    path: "/basic",
    name: "登录页",
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
    component: BlankLayout,
    meta: { title: "错误信息", code: "err" },
    children: [
      {
        name: "error.403",
        path: "/403",
        component: Error403,
        meta: { title: "403" },
      },
      {
        name: "error.404",
        path: "/404",
        component: Error404,
        meta: { title: "404" },
      },
      {
        name: "error.500",
        path: "/500",
        component: Error500,
        meta: { title: "500" },
      },
      {
        name: "error",
        path: "/error",
        component: Error,
        meta: { title: "error" },
      },
    ],
  },
  {
    path: '/404',
    redirect: "/404",
  },
  // {
  //   path: "/:catchAll(.*)",
  //   redirect: "/404",
  // },
];
