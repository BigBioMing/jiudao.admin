import type { Paging, TablePageResult } from "@/types/global";
import type { SysRoleGetPageEntitiesInputParams } from "@/types/sys/role";
import request from "@/utils/http";
import qs from "qs";

const URL_PREFIX = "/api/Sys/SysRole/";

/**
 * 获取用户分页数据
 * @param params
 * @returns
 */
export const getPageEntitiesApi = async (
  params: SysRoleGetPageEntitiesInputParams
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
 * 导出
 * @param params
 * @returns
 */
export const exportApi = async (params: SysRoleGetPageEntitiesInputParams) => {
  return await request({
    url: URL_PREFIX + "Export",
    method: "get",
    // params: params,
    responseType: "blob",
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
export const saveRoleApi = async (params: any) => {
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
export const delRoleApi = async (id: number) => {
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
export const getRouteAndOptionsApi = async (params: any) => {
  return await request({
    url: URL_PREFIX + "GetRouteAndOptions",
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
 * 授权
 * @param params
 * @returns
 */
export const empowerApi = async (data: any) => {
  return await request({
    url: URL_PREFIX + "Empower",
    method: "post",
    // params: params,
    data: data,
    paramsSerializer: (params) => {
      console.log('hhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh')
      // return qs.stringify(params, { indices: false,allowDots: true })
      return qs.stringify(params, { allowDots: true });
    },
  });
};
