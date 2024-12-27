import { defineStore } from "pinia";

export const useMenuStore = defineStore("menu", {
  state: () => {
    return <{ menus: any[] }>{
      menus: [],
    };
  },
  actions: {
    setMenus(_menus: any[]) {
      this.menus = _menus;
    },
    getMenus() {
      return this.menus;
    },
  },
});
