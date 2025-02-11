import axios, { type AxiosRequestConfig } from "axios";
import { message } from "ant-design-vue";
import { useGlobalStore, useLoadingStore } from "@/stores";
import common from "@/utils/common";

const instance = axios.create({
  baseURL: "https://localhost:7256",
  // timeout: 20000,
  headers: { "X-Custom-Header": "foobar" },
  // transformResponse: [
  //   function (data, headers,a,b) {
  //     debugger;
  //     // 处理返回数据
  //     return data;
  //   },
  // ],
});

instance.interceptors.request.use(
  function (config) {
    //判断该请求是否需要展示loading（默认展示）
    const isLoading = config.isLoading;
    if (isLoading || isLoading === null || isLoading === undefined) {
      const loadingStore = useLoadingStore();
      loadingStore.start();
    }

    // console.log("config.data:", config.data);
    // if (
    //   config.method === "post" ||
    //   config.method === "put" ||
    //   config.method === "delete"
    // ) {
    //   // qs序列化
    //   config.data = qs.parse(config.data);
    // }
    // 若是有做鉴权token , 就给头部带上token
    //   if (storage.get(store.state.Roles)) {
    //     store.state.Roles
    //     config.headers.Authorization = storage.get(store.state.Roles);
    //   }

    const globalStore = useGlobalStore();
    const token = globalStore.getToken();
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
  },
  (error) => {
    messageError("error:", error);
    return Promise.reject(error);
  }
);
// 响应拦截器
instance.interceptors.response.use(
  function (config) {
    //关闭loading
    const isLoading = config.config.isLoading;
    if (isLoading || isLoading === null || isLoading === undefined) {
      const loadingStore = useLoadingStore();
      loadingStore.end();
    }

    //   dataList.show = true
    if (config.status === 200 || config.status === 204) {
      setTimeout(() => {
        //   dataList.show = false
      }, 400);
      // console.log(config)
      // return Promise.resolve(config);

      let contentType = config.headers?.getContentType();
      if (
        contentType &&
        (contentType.indexOf("application/json") > -1 ||
          contentType.indexOf("text/plain") > -1)
      ) {
        const res = config.data;
        if (!common.isBlob(res)) {
          if (res?.code === "0") {
            return Promise.resolve(config.data.data);
          } else {
            messageError(res?.message || "请求错误");
            return Promise.reject(config);
          }
        } else {//如果下载文件报错，后端可能会返回blob类型的报错信息
          const textBlob = common.readBlobAsJson(res);
          textBlob
            .then((r) => {
              if (r?.code === "0") {
                return Promise.resolve(r.data);
              } else {
                messageError(r?.message || "请求错误");
                return Promise.reject(config);
              }
            })
            .catch((err2) => {
              return Promise.reject(err2);
            });
        }
      } else {
        return Promise.resolve(config);
      }
    } else {
      messageError("非正常响应==>" + config.status);
      return Promise.reject(config);
    }
  },
  function (error) {
    //关闭loading
    const isLoading = error.config.isLoading;
    if (isLoading || isLoading === null || isLoading === undefined) {
      const loadingStore = useLoadingStore();
      loadingStore.end();
    }
    if (error.response && error.response.status) {
      switch (error.response.status) {
        case 400:
          messageError(
            "发出的请求有错误，服务器没有进行新建或修改数据的操作==>" +
              error.response.status
          );
          break;
        // 401: 未登录
        // 未登录则跳转登录页面，并携带当前页面的路径
        // 在登录成功后返回当前页面，这一步需要在登录页操作。
        case 401: //未授权，重定向到登录界面
          messageError("token:登录失效==>" + error.response.status);
          //   storage.remove(store.state.Roles)
          //   storage.get(store.state.Roles)
          //   router.replace({
          //     path: '/Login',
          //   });
          break;
        // 403 token过期
        // 登录过期对用户进行提示
        // 清除本地token和清空vuex中token对象
        // 跳转登录页面
        case 403:
          messageError(
            "用户得到授权，但是访问是被禁止的==>" + error.response.status
          );
          break;
        case 404:
          messageError("网络请求不存在==>" + error.response.status);
          break;
        case 406:
          messageError("请求的格式不可得==>" + error.response.status);
          break;
        case 410:
          messageError(
            "请求的资源被永久删除，且不会再得到的==>" + error.response.status
          );
          break;
        case 422:
          messageError(
            "当创建一个对象时，发生一个验证错误==>" + error.response.status
          );
          break;
        case 500:
          messageError(
            "服务器发生错误，请检查服务器==>" + error.response.status
          );
          break;
        case 502:
          messageError("网关错误==>" + error.response.status);
          break;
        case 503:
          messageError(
            "服务不可用，服务器暂时过载或维护==>" + error.response.status
          );
          break;
        case 504:
          messageError("网关超时==>" + error.response.status);
          break;
        default:
          messageError("其他错误错误==>" + error.response.status);
      }
      return Promise.reject(error);
    } else {
      // 处理断网的情况
      // eg:请求超时或断网时，更新state的network状态
      // network状态在app.vue中控制着一个全局的断网提示组件的显示隐藏
      // 关于断网组件中的刷新重新获取数据，会在断网组件中说明
      //   store.commit('changeNetwork', false);
      messageError("网络暂时不可用，请检查下哦~");
      return Promise.reject(error);
    }
  }
);

const messageError = (text: string, error?: any) => {
  message.error(text, error);
  console.error(text);
};

export default instance;
