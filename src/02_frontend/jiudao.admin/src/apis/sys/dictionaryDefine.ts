import type { Paging, TablePageResult } from "@/types/global";
import type { SysDictionaryDefineGetPageEntitiesInputParams } from "@/types/sys/dictionaryDefine";
import request from "@/utils/http";
import qs from "qs";

const URL_PREFIX = "/api/Sys/SysDictionaryDefine/";

/**
 * 获取分页数据
 * @param params
 * @returns
 */
export const getPageEntitiesApi = async (
  params: SysDictionaryDefineGetPageEntitiesInputParams
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
export const exportApi = async (params: SysDictionaryDefineGetPageEntitiesInputParams) => {
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
 * 获取信息
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
 * 保存信息
 * @param params 信息
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
 * 删除字典定义
 * @param id 字典定义Id
 * @returns
 */
export const delDictionaryDefineApi = async (id: number) => {
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
