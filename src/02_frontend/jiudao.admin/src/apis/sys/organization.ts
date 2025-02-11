import type { Paging, TablePageResult } from "@/types/global";
import type { SysOrganizationGetPageEntitiesInputParams } from "@/types/sys/organization";
import request from "@/utils/http";
import qs from "qs";

const URL_PREFIX = "/api/Sys/SysOrganization/";

/**
 * 获取组织机构分页数据
 * @param params
 * @returns
 */
export const getPageEntitiesApi = async (
  params: SysOrganizationGetPageEntitiesInputParams
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
export const exportApi = async (params: SysOrganizationGetPageEntitiesInputParams) => {
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
 * 获取组织机构信息
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
 * 保存组织机构信息
 * @param params 组织机构信息
 * @returns
 */
export const saveApi = async (params: any) => {
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
export const delApi = async (id: number) => {
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
