export {}
declare module 'axios' {
  export interface AxiosRequestConfig {
    isShowProgress?: boolean
    isLoading?: boolean
  }
}

/**
 * export {}这个一定要加，我刚开始没加就导致了axios自身的类型没了，出现了一些奇奇怪怪的ts错误；
 * declare module ‘axios’：这告诉 TypeScript 你正在扩展名为 axios 的模块。
 * export interface AxiosRequestConfig：在这里，你重新声明了 AxiosRequestConfig 接口。由于这是在 declare module 内部，它实际上是对原有 AxiosRequestConfig 接口的扩展，而不是替换。
 * 通过这种方式，你现在可以在使用 Axios 发起请求时，在请求配置对象中包含 loading 属性。这个属性不会被 Axios 本身处理（除非你在拦截器或其他地方显式地处理它），但它会作为请求配置的一部分被传递。
 */

/**
 * 1、在根目录创建axios.d.ts文件
 * 2、将axios.d.ts添加到tsconfig.json文件里
 * 3、如果ts还报错，重启编辑器后还是不行，就将axios.d.ts这个文件移到src目录下 ，为什么要这样我也不清楚，机器人给的回答是跟ts版本及编辑器版本有关
 * https://blog.csdn.net/weixin_50583912/article/details/144604843
 */


// "include": [
//     "env.d.ts",
//     "src/**/*",
//     "src/**/*.vue",
//     "./auto-imports.d.ts",
//     "src/**/**/*.vue",
//     "axios.d.ts"
//   ],