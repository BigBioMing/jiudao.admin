import { defineStore } from "pinia";

export const useGlobalStore = defineStore("global", {
  state: () => {
    return <{ dics: any[] }>{
      dics: [],
    };
  },
  //类似computed，修饰一些值
  getters: {},
  //methods，同步异步都可以，提交state
  actions: {
    setDics(dics: any[]) {
      this.dics = dics;
    },
    getDics() {
      return this.dics;
    },
  },
});
