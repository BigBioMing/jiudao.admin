import request from "@/utils/http";

export const getDicApi = async () => {
  return await request({
    url: "/api/Sys/SysDictionaryDefine/GetDictionaryTree",
    method: "get",
    data: {},
  });
};
