import router, { resetRouter } from "./index";
import { useGlobalStore, useMenuStore } from "@/stores";
import { getRouteAndOptionsApi } from "@/apis/resource/route";
import BlankLayout from "@/components/layout/blank-layout.vue";
import BasicLayout from "@/components/layout/basic-layout.vue";
import type { RouteRecordRaw } from "vue-router";
import { defineAsyncComponent } from "vue";

const allowList = ["login"]; // no redirect allowList
const loginRoutePath = "/login";
const defaultRoutePath = "/home";

/**
 * to: 即将要进入的目标
 * from: 当前导航正要离开的路由
 */
router.beforeEach((to, from, next) => {console.log(to, from, next);
  const globalStore = useGlobalStore();
  const menuStore = useMenuStore();
  const token = globalStore.getToken();
  //登录与未登录的逻辑不同，登录后不能再跳转到登录页，并需要获取当前登录用户拥有的菜单和按钮权限
  if (token) {
    if (to.path == loginRoutePath) {
      next({ path: defaultRoutePath });
    } else {
      //如果未登录，则查询权限
      if (!menuStore.isAddRouter) {
        getRouteAndOptionsApi({})
          .then((res) => {
            console.log("菜单、按钮：res", res);
            let addRouters: RouteRecordRaw[] = loopAddRouters(
              res.menuTreeNodes
            );
            let addMenus = loopAddMenus(res.menuTreeNodes);

            addRouters.push({
              path: "/:catchAll(.*)",
              redirect: "/404",
            });

            menuStore.setRouters(addRouters);
            menuStore.setMenus(addMenus);
            menuStore.setActions(res.actions);
            resetRouter(); // 重置路由 防止退出重新登录或者 token 过期后页面未刷新，导致的路由重复添加
            addRouters.forEach((r) => {
              router.addRoute(r);
            });
            // 请求带有 redirect 重定向时，登录自动重定向到该地址
            const redirect = decodeURIComponent(from.query.redirect || to.path);
            if (to.path === redirect) {
              // 设置replace:true，这样导航就不会留下历史记录
              // next();
              next({ ...to, replace: true });
            } else {
              // 跳转到目的路由
              next({ path: redirect });
            }
          })
          .catch((err) => {
            console.log("菜单、按钮：err", err);
          });
      } else {
        next();
      }
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

/**
 * 获取路由
 * @param menuTreeNode 后端传过来的路由树
 * @returns 返回路由
 */
const loopAddRouters = function (menuTreeNodes: any[]): RouteRecordRaw[] {
  let addRouters = [];
  for (let i = 0; i < menuTreeNodes.length; i++) {
    let menu = menuTreeNodes[i];
    var addRouter: RouteRecordRaw = {
      name: menu.code,
      path: menu.url,
      meta: { name: menu.name, title: menu.title },
    };
    addRouters.push(addRouter);
    if (menu.redirect) addRouter.redirect = menu.redirect;
    if (menu.component) {
      if (menu.component === "BasicLayout") addRouter.component = BasicLayout;
      else if (menu.component === "BlankLayout")
        addRouter.component = BlankLayout;
      // addRouter.component = defineAsyncComponent(
      //   () => import(`..${menu.component}`)
      // );
      else
        addRouter.component = () =>
          import(/* @vite-ignore */ `..${menu.component}`);
    }
    if (menu.childrens && menu.childrens.length > 0) {
      addRouter.children = loopAddRouters(menu.childrens);
    }
  }

  return addRouters;
};

/**
 * 获取菜单
 * @param menuTreeNode 后端传过来的路由树
 * @returns 返回菜单
 */
const loopAddMenus = function (menuTreeNodes: any[]): any[] {
  let addMenus = [];
  for (let i = 0; i < menuTreeNodes.length; i++) {
    let menu = menuTreeNodes[i];
    var addMenu = {
      key: menu.code,
      label: menu.name,
      title: menu.title,
      icon: menu.icon,
      path: menu.url,
    };
    if (menu.showInMenu) {
      addMenus.push(addMenu);
    }
    if (menu.childrens && menu.childrens.length > 0) {
      addMenu.children = loopAddMenus(menu.childrens);
    }
  }

  return addMenus;
};
