import { useGlobalStore } from "@/stores";
const globalStore = useGlobalStore();
export const getDicItem = (defineKey: string, dataKey: string) => {
  const dics: any = globalStore.getDics();
  if (!dics) return null;

  var define = dics.filter((n: any) => n.code === defineKey);
  if (!define) return null;

  let children = define.children || [];
  let dataItem = children.filter((n: any) => n.code === dataKey);
  return dataItem;
};
