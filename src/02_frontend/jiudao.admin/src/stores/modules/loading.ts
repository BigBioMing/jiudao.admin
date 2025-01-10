import { defineStore } from "pinia";

export const useLoadingStore = defineStore("loading", {
  state: () => {
    return {
      loadingCount: 0,
    };
  },
  getters: {
    /**
     * 当前loading状态 true-显示 false-隐藏
     * @returns
     */
    isLoading: (state) => {
      return state.loadingCount > 0;
    },
  },
  actions: {
    start() {
      if (this.loadingCount < 0) this.loadingCount = 0;
      this.loadingCount++;
    },
    end() {
      this.loadingCount--;
      if (this.loadingCount < 0) this.loadingCount = 0;
    },
    close() {
      this.loadingCount = 0;
    },
  },
});
