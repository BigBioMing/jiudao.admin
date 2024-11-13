import { useGlobalStore } from "@/stores";
import { computed } from "vue";
export default function () {
  const globalStore = useGlobalStore();

  /**
   * 根据code获取字典项
   */
  const dicItem = computed(() => {
    return (defineKey: string, dataKey: string | number) => {
      const dics: any = globalStore.getDics();
      if (!dics) return null;

      if (typeof dataKey === "string") {
        var define = dics.find((n: any) => n.code === defineKey);
        if (!define) return null;

        let childrens = define.childrens || [];
        let dataItem = childrens.find((n: any) => n.code === dataKey);
        return dataItem;
      } else if (typeof dataKey === "number") {
        var define = dics.find((n: any) => n.code === defineKey);
        if (!define) return null;

        let childrens = define.childrens || [];
        let dataItem = childrens.find((n: any) => n.id === dataKey);
        return dataItem;
      }
    };
  });

  /**
   * 根据code获取字典项名称
   */
  const dicItemName = computed(() => {
    return (defineKey: string, dataKey: string | number) => {
      const dics: any = globalStore.getDics();
      if (!dics) return null;
      
      if (typeof dataKey === "string") {
        var define = dics.find((n: any) => n.code === defineKey);
        if (!define) return null;

        let childrens = define.childrens || [];
        let dataItem = childrens.find((n: any) => n.code === dataKey);
        return dataItem?.name;
      } else if (typeof dataKey === "number") {
        var define = dics.find((n: any) => n.code === defineKey);
        if (!define) return null;

        let childrens = define.childrens || [];
        let dataItem = childrens.find((n: any) => n.id === dataKey);
        return dataItem?.name;
      }
    };
  });

  /**
   * 获取字典项定义下的所有字典项
   * @param defineKey 字典项定义
   */
  const getDicItems = (defineKey: string) => {
    const dics: any = globalStore.getDics();
    var define = dics.find((n: any) => n.code === defineKey);
    if (!define) return [];

    return define.childrens || [];
  };

  return { dicItem, dicItemName, getDicItems };
}
