/**
 * 分页参数
 */
export interface Paging<T> {
  pageIndex?: number;
  pageSize?: number;
  params?: T;
}

/**
 * 表格分页选中事件
 */
export interface PaginationChangeEvent {
  page: {
    /**
     * 当前页码
     */
    pageIndex: number;
    /**
     * 每页条数
     */
    pageSize: number;
  };
}

// /**
//  * 接口响应参数
//  */
// export interface ApiResponse<T> {
//   code?: number;
//   message?: string;
//   data?: T;
// }

export interface TablePageResult<T> {
  /**
   * 数据总条数
   */
  totalRecords: number;
  /**
   * 数据集合
   */
  items: T[];
}
