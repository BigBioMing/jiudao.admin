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
  params: Paging<SysRoleGetPageEntitiesInputParams>
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
 * 获取用户信息
 * @param params
 * @returns
 */
export const getEntityApi = async (id: number): Promise<TablePageResult<any>> => {
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
export const saveUserApi = async (params: any) => {
  return await request({
    url: URL_PREFIX + "Save",
    method: "post",
    data: params,
  });
};

/**
 * 删除用户
 * @param id 用户Id
 * @returns
 */
export const delUserApi = async (id: number) => {
  return await request({
    url: URL_PREFIX + "Delete",
    method: "post",
    data: { Id: id },
  });
};

/**
 * 启用/禁用
 * @param id 用户id
 * @param value 启用/禁用
 * @returns
 */
export const enableUserApi = async (id: number, value: boolean) => {
  return await request({
    url: URL_PREFIX + "Enable",
    method: "post",
    data: { Id: id, SetEnableValue: value },
  });
};
