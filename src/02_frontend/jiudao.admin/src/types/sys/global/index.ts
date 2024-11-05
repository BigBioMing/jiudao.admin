/**
 * 分页参数
 */
export interface Paging<T> {
  pageIndex?: number;
  pageSize?: number;
  params?: T;
}
