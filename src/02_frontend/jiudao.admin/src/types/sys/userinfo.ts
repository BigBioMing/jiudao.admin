import type { Paging, ImportDataFieldInputParams } from "../global";

/**
 * 获取用户分页接口参数
 */
export interface SysUserInfoGetPageEntitiesInputParams extends Paging {
  name?: string | null;
  account?: string | null;
  mobile?: string | null;
  email?: string | null;
}
