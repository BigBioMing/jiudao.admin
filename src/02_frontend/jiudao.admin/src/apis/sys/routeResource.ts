import type { Paging, TablePageResult } from "@/types/global";
import type { SysRouteResourceGetPageEntitiesInputParams } from "@/types/sys/routeResource";
import request from "@/utils/http";
import qs from "qs";

const URL_PREFIX = "/api/Sys/SysRouteResource/";

/**
 * 获取分页数据
 * @param params
 * @returns
 */
export const getPageEntitiesApi = async (
  params: Paging<SysRouteResourceGetPageEntitiesInputParams>
): Promise<TablePageResult<any>> => {
  return await request({
    url: URL_PREFIX + "GetPageEntities",
    method: "get",
    // params: params,
    params: params,
    paramsSerializer: (params) => {
      // return qs.stringify(params, { indices: false,allowDots: true })
      return qs.stringify(params, { allowDots: true });
    },
  });
};
/**
 * 获取全部数据
 * @param params
 * @returns
 */
export const getEntitiesApi = async (params: any) => {
  return await request({
    url: URL_PREFIX + "GetEntities",
    method: "get",
    // params: params,
    params: params,
    paramsSerializer: (params) => {
      // return qs.stringify(params, { indices: false,allowDots: true })
      return qs.stringify(params, { allowDots: true });
    },
  });
};
/**
 * 获取路由资源树
 * @param params
 * @returns
 */
export const getRouteTreeApi = async (params: any) => {
  return await request({
    url: URL_PREFIX + "GetRouteTree",
    method: "get",
    // params: params,
    params: params,
    paramsSerializer: (params) => {
      // return qs.stringify(params, { indices: false,allowDots: true })
      return qs.stringify(params, { allowDots: true });
    },
  });
};
/**
 * 获取用户信息
 * @param params
 * @returns
 */
export const getEntityApi = async (
  id: number
): Promise<TablePageResult<any>> => {
  return await request({
    url: URL_PREFIX + "GetEntityById",
    method: "get",
    // params: params,
    params: { id: id },
    paramsSerializer: (params) => {
      // return qs.stringify(params, { indices: false,allowDots: true })
      return qs.stringify(params, { allowDots: true });
    },
  });
};
/**
 * 保存用户信息
 * @param params 用户信息
 * @returns
 */
export const saveRouteResourceApi = async (params: any) => {
  return await request({
    url: URL_PREFIX + "Save",
    method: "post",
    data: params,
  });
};

/**
 * 删除角色
 * @param id 角色Id
 * @returns
 */
export const delRouteResourceApi = async (id: number) => {
  return await request({
    url: URL_PREFIX + "Delete",
    method: "post",
    data: { Id: id },
  });
};

/**
 * 启用/禁用
 * @param id 角色id
 * @param value 启用/禁用
 * @returns
 */
export const enableApi = async (id: number, value: boolean) => {
  return await request({
    url: URL_PREFIX + "Enable",
    method: "post",
    data: { Id: id, SetEnableValue: value },
  });
};

/**
 * 获取按钮和菜单权限
 * @param params
 * @returns
 */
export const getRouteAndActionsApi = async (params: any) => {
  return await request({
    url: URL_PREFIX + "GetRouteAndActions",
    method: "get",
    // params: params,
    params: params,
    paramsSerializer: (params) => {
      // return qs.stringify(params, { indices: false,allowDots: true })
      return qs.stringify(params, { allowDots: true });
    },
  });
};
/**
 * 获取指定菜单下的按钮集合
 * @param params
 * @returns
 */
export const getActionsApi = async (params: any) => {
  return await request({
    url: URL_PREFIX + "GetActions",
    method: "get",
    // params: params,
    params: params,
    paramsSerializer: (params) => {
      // return qs.stringify(params, { indices: false,allowDots: true })
      return qs.stringify(params, { allowDots: true });
    },
  });
};
/**
 * 保存指定菜单下的按钮集合
 * @param params
 * @returns
 */
export const saveActionsApi = async (data: any) => {
  return await request({
    url: URL_PREFIX + "SaveActions",
    method: "post",
    // params: params,
    data: data,
    paramsSerializer: (params) => {
      // return qs.stringify(params, { indices: false,allowDots: true })
      return qs.stringify(params, { allowDots: true });
    },
  });
};
