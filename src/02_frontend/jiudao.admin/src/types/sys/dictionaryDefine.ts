import type { Paging } from "../global";

/**
 * 获取用户分页接口参数
 */
export interface SysDictionaryDefineGetPageEntitiesInputParams extends Paging {
  name?: string | null;
  code?: string | null;
}
