import { defineStore } from "pinia";
import { constantRouterMap } from "@/router";

export const useMenuStore = defineStore("menu", {
  state: () => {
    return <
      {
        allMenuMap: Map<string, any>; //所有菜单
        allMenus: any[]; //所有菜单
        topLevelMenus: any[]; //顶级菜单
        actions: any[];
        addRouters: any[];
        routers: any[];
        isAddRouter: boolean;
      }
    >{
      allMenuMap: new Map(),
      allMenus: [],
      topLevelMenus: [],
      actions: [],
      addRouters: [],
      routers: constantRouterMap,
      //是否添加了路由
      isAddRouter: false,
    };
  },
  actions: {
    setMenus(_topLevelMenus: any[], _allMenus: any[]) {
      this.topLevelMenus = _topLevelMenus;
      this.allMenus = _allMenus;

      let allMenuMap = new Map();
      for (let i = 0; i < this.allMenus.length; i++) {
        let menu = this.allMenus[i];
        allMenuMap.set(menu.key, menu);
      }

      this.allMenuMap = allMenuMap;
    },
    getMenus() {
      return {
        topLevelMenus: this.topLevelMenus,
        allMenus: this.allMenus,
        allMenuMap: this.allMenuMap,
      };
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
    getActions(): string[] {
      return this.actions.map((n) => n.code);
    },
    /**
     * 将路由和菜单重置成初始状态
     */
    reset() {
      this.topLevelMenus = [];
      this.allMenus = [];
      this.actions = [];
      this.addRouters = [];
      this.routers = constantRouterMap;
      this.isAddRouter = false;
    },
  },
});
