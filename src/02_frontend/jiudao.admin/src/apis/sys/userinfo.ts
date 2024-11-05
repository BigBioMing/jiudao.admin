import type { Paging } from "@/types/sys/global";
import type { GetPageEntitiesInputParams } from "@/types/sys/userinfo";
import request from "@/utils/http";

/**
 * 获取用户分页数据
 * @param params
 * @returns
 */
export const getPageEntitiesApi = async (
  params: Paging<GetPageEntitiesInputParams>
) => {
  return await request({
    url: "/api/Sys/SysUser/GetPageEntities",
    method: "get",
    // params: params,
    params:params
  });
};
/**
 * 保存用户信息
 * @param params 用户信息
 * @returns
 */
export const saveUserApi = async (params: any) => {
  return await request({
    url: "/api/Sys/SysUser/Save",
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
    url: "/api/Sys/SysUser/Delete",
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
    url: "/api/Sys/SysUser/Enable",
    method: "post",
    data: { Id: id, SetEnableValue: value },
  });
};
