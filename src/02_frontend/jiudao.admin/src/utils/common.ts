import type { AxiosResponseHeaders, RawAxiosResponseHeaders } from "axios";

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
  /**
   * 解析 Content-Disposition
   * @param headers
   * @returns
   */
  static parseContentDisposition(
    headers: RawAxiosResponseHeaders | AxiosResponseHeaders | string | null
  ) {
    let contentDisposition: string = "";

    if (!headers) return null;

    let filename = "";

    if (typeof headers === "string") {
      contentDisposition = headers;
    } else {
      contentDisposition = headers["content-disposition"];
    }

    let map = new Map();
    let strs = contentDisposition.split(";");
    for (let i = 0; i < strs.length; i++) {
      let str = strs[i];
      if (str) {
        str = str.trim();
        let fns = str.split("=");
        map.set(fns[0], fns[1]);
      }
    }

    if (map.has("filename*")) {
      let s = map.get("filename*");
      if (s) s = s.replace("UTF-8''", "");
      if (s) filename = decodeURIComponent(s);
    } else if (map.has("filename")) {
      filename = map.get("filename");
    }

    return filename;
  }
  /**
   * 下载文件
   * @param data 文件内容
   * @param filename 文件名
   * @param ext 扩展名
   * @param type 文件MIME类型
   */
  static downloadFile(
    data: any,
    filename: string,
    ext = ".xlsx",
    type = "application/vnd.ms-excel"
  ) {
    if (filename && !filename.endsWith(ext)) filename = filename + ext;

    let blob = new Blob([data], { type });
    if (window.navigator.msSaveBlob) {
      try {
        // ie浏览器自带下载文件的方法
        window.navigator.msSaveBlob(data, filename);
      } catch (e) {
        console.log(e);
      }
    } else {
      let elink = document.createElement("a");
      elink.download = filename;
      elink.style.display = "none";
      let href = window.URL.createObjectURL(blob);
      elink.href = href;
      document.body.appendChild(elink);
      elink.click();
      document.body.removeChild(elink);
      window.URL.revokeObjectURL(href); // 释放掉blob对象
    }
  }
  static isBlob(obj: any) {
    return (
      obj instanceof Blob ||
      (obj && Object.prototype.toString.call(obj) === "[object Blob]")
    );
  }
  /**
   * 读取blob
   * @param blob 文本类型的blob
   * @returns
   */
  static readBlobAsText(blob: Blob) {
    return new Promise((resolve, reject) => {
      try {
        // 创建FileReader对象来读取Blob
        var reader = new FileReader();
        // 设置FileReader的onload事件处理程序，当读取操作完成时调用
        reader.onload = function (e: any) {
          // 此时e.target.result包含了文本内容
          try {
            resolve(e.target.result);
          } catch (err) {
            reject(err);
          }
        };

        // 使用readAsText方法读取Blob中的内容
        reader.readAsText(blob);
      } catch (err2) {
        reject(err2);
      }
    });
  }
  /**
   * 读取blob并转换成json
   * @param blob 文本类型的blob
   * @returns
   */
  static readBlobAsJson(blob: Blob) {
    return new Promise((resolve, reject) => {
      common
        .readBlobAsText(blob)
        .then((res) => {
          try {
            const json = JSON.parse(res);
            resolve(json);
          } catch (err) {
            reject(err);
          }
        })
        .catch((err2) => {
          reject(err2);
        });
    });
  }
}
