import { createRouter, createWebHistory } from "vue-router";
import { constantRouterMap, asyncRouterMap } from "./router.config";

// const arr = [...constantRouterMap,...asyncRouterMap]
const arr = [...constantRouterMap];
let router = createRouter({
  history: createWebHistory(),
  routes: arr,
});

// 定义一个resetRouter 方法，在退出登录后或token过期后 需要重新登录时，调用即可
export function resetRouter() {
  const newRouter = createRouter({
    history: createWebHistory(),
    routes: [],
  });
  router.matcher = newRouter.matcher;
}

export { constantRouterMap };
export default router;
