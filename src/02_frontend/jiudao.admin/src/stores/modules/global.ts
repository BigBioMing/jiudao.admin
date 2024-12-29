import { defineStore } from "pinia";
import localStorageUtils from "@/utils/localStorageUtils";

export const useGlobalStore = defineStore("global", {
  state: () => {
    return <{ dics: any[]; token: string | null }>{
      dics: [],
      token: null,
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
    isLogin() {
      let tk = this.getToken();
      return tk != null && tk != undefined;
    },
    getToken() {
      if (!this.token) {
        this.token = localStorageUtils.getItemWithExpire("token");
      }
      return this.token;
    },
    setToken(_token: string | null, expireTime: number) {
      this.token = _token;
      localStorageUtils.setItemWithExpire(
        "token",
        _token,
        expireTime * 60 * 1000
      );
    },
    /**
     * 删除token
     */
    clearToken() {
      this.token = null;
      localStorageUtils.removeItem("token");
    },
  },
});
