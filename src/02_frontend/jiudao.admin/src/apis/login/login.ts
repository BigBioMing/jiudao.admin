import request from "@/utils/http";
import qs from "qs";

const URL_PREFIX = "/api/Login/";

/**
 * 获取用户分页数据
 * @param params
 * @returns
 */
export const loginApi = async (params: any) => {
  return await request({
    url: URL_PREFIX + "Login",
    method: "post",
    // params: params,
    data: params,
    paramsSerializer: (params) => {
      // return qs.stringify(params, { indices: false,allowDots: true })
      return qs.stringify(params, { allowDots: true });
    },
  });
};
