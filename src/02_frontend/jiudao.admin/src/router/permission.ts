import { router } from "./index";
import { useGlobalStore, useMenuStore } from "@/stores";
import { getRouteAndOptionsApi } from "@/apis/resource/route";

const allowList = ["login"]; // no redirect allowList
const loginRoutePath = "/login";
const defaultRoutePath = "/home";

/**
 * to: 即将要进入的目标
 * from: 当前导航正要离开的路由
 */
router.beforeEach((to, from, next) => {
  console.log(to, from, next);
  const globalStore = useGlobalStore();
  const menuStore = useMenuStore();
  const token = globalStore.getToken();
  //登录与未登录的逻辑不同，登录后不能再跳转到登录页，并需要获取当前登录用户拥有的菜单和按钮权限
  if (token) {
    if (to.path == loginRoutePath) {
      next({ path: defaultRoutePath });
    } else {
      getRouteAndOptionsApi({})
        .then((res) => {
          console.log("菜单、按钮：res", res);
          menuStore.setMenus([]);
          // 请求带有 redirect 重定向时，登录自动重定向到该地址
          const redirect = decodeURIComponent(from.query.redirect || to.path);
          if (to.path === redirect) {
            // 设置replace:true，这样导航就不会留下历史记录
            next();
          } else {
            // 跳转到目的路由
            next({ path: redirect });
          }
        })
        .catch((err) => {
          console.log("菜单、按钮：err", err);
        });
    }
  } else {
    if (allowList.includes(to.name)) {
      // 在免登录名单，直接进入
      next();
    } else {
      next({ path: loginRoutePath, query: { redirect: to.fullPath } });
    }
  }
});
