import request from "@/utils/http";
import qs from "qs";

/**
 * 获取当前登录用户拥有的菜单和按钮
 * @param params
 * @returns
 */
export const getUserRouteAndOptionsApi = async (params: any): Promise<any> => {
  return await request({
    url: "/api/Sys/SysUser/GetUserRouteAndOptions",
    method: "get",
    // params: params,
    params: params,
    paramsSerializer: (params) => {
      // return qs.stringify(params, { indices: false,allowDots: true })
      return qs.stringify(params, { allowDots: true });
    },
  });
};
