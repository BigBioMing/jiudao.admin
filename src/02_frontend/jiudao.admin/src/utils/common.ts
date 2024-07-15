/**
 * 工具类
 */
export default class common {
  /**
   * 对象克隆
   * @param obj 被克隆的对象
   * @returns 克隆后的新对象
   */
  static clone(obj: any) {
    if (!obj) return obj;
    return JSON.parse(JSON.stringify(obj));
  }
}
