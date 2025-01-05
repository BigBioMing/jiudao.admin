import Antd from "ant-design-vue";
import * as AntdIcons from "@ant-design/icons-vue";

import { VueDraggable } from "vue-draggable-plus";

import { createApp } from "vue";
import App from "./App.vue";
import "ant-design-vue/dist/reset.css";
import "@/style/global.scss";

import pinia from "@/stores";

import router from "@/router";
import permissionDirective from '@/directives/permission';

import {
  JdaMenu,
  JdaTable,
  JdaTableSearch,
  JdaModalFooter,
  JdaModal,
  JdaCard,
  JdaEditCard,
  JdaForm,
  JdaTreeSelect,
} from "@/components";

import { library } from "@fortawesome/fontawesome-svg-core";
import { fas } from "@fortawesome/free-solid-svg-icons";
import { far } from "@fortawesome/free-regular-svg-icons";
import { fab } from "@fortawesome/free-brands-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

library.add(fas, far, fab);

const app = createApp(App);

app.component("font-awesome-icon", FontAwesomeIcon);
app.component("JdaMenu", JdaMenu);
app.component("JdaTable", JdaTable);
app.component("JdaTableSearch", JdaTableSearch);
app.component("JdaModal", JdaModal);
app.component("JdaModalFooter", JdaModalFooter);
app.component("JdaCard", JdaCard);
app.component("JdaEditCard", JdaEditCard);
app.component("JdaForm", JdaForm);
app.component("JdaTreeSelect", JdaTreeSelect);
app.component("VueDraggable", VueDraggable);
app.use(pinia);
app.use(router);
app.use(Antd);
app.use(permissionDirective); // 注册自定义指令

import "@/router/permission";

let a: {
  [key in keyof typeof app]: any;
};
for (let key in AntdIcons) {
  //  console.log('key',(AntdIcons as any)[key]);
  //  console.log('key',(<any>AntdIcons)[key]);
  // app.component(key, (AntdIcons as any)[key]);
  app.component(key, AntdIcons[key as keyof typeof AntdIcons]);
}

app.mount("#app");
