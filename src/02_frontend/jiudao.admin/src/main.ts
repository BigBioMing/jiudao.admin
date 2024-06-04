import Antd from 'ant-design-vue';
import * as AntdIcons from '@ant-design/icons-vue';
console.log('AntdIcons', AntdIcons);

import { createApp } from 'vue'
import App from './App.vue'
import 'ant-design-vue/dist/reset.css';
import '@/style/global.scss';
const app = createApp(App);

app.use(Antd);

let a: {
    [key in keyof typeof app]: any
}
for (let key in AntdIcons) {
    //  console.log('key',(AntdIcons as any)[key]);
    //  console.log('key',(<any>AntdIcons)[key]);
    // app.component(key, (AntdIcons as any)[key]);
    app.component(key, AntdIcons[key as keyof typeof AntdIcons]);
}
console.log('typeof(AntdIcons)', typeof (AntdIcons));

app.mount('#app');