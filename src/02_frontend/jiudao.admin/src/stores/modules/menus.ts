import { defineStore } from "pinia";
import { constantRouterMap } from "@/router";

export const useMenuStore = defineStore("menu", {
  state: () => {
    return <
      {
        menus: any[];
        actions: any[];
        addRouters: any[];
        routers: any[];
        isAddRouter: boolean;
      }
    >{
      menus: [],
      actions: [],
      addRouters: [],
      routers: constantRouterMap,
      //是否添加了路由
      isAddRouter: false,
    };
  },
  actions: {
    setMenus(_menus: any[]) {
      this.menus = _menus;
    },
    getMenus() {
      return this.menus;
    },
    setRouters(routers: any[]) {
      this.addRouters = routers;
      this.routers = constantRouterMap.concat(routers);
      this.isAddRouter = true;
    },
    getAddRouters() {
      return this.addRouters;
    },
    setActions(_actions: any[]) {
      this.actions = _actions;
    },
    getActions() {
      return this.actions;
    },
    /**
     * 将路由和菜单重置成初始状态
     */
    reset() {
      this.menus = [];
      this.actions = [];
      this.addRouters = [];
      this.routers = constantRouterMap;
      this.isAddRouter = false;
    },
  },
});
