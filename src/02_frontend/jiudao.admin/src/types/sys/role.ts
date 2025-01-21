import type { Paging } from "../global";

/**
 * 获取角色分页接口参数
 */
export interface SysRoleGetPageEntitiesInputParams extends Paging {
  name?: string | null;
  code?: string | null;
}
