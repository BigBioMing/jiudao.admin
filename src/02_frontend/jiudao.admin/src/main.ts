import Antd from 'ant-design-vue';
import * as AntdIcons from '@ant-design/icons-vue';
console.log('AntdIcons', AntdIcons);

import { VueDraggable } from 'vue-draggable-plus';

import { createApp } from 'vue'
import App from './App.vue'
import 'ant-design-vue/dist/reset.css';
import '@/style/global.scss';

import {router} from '@/router'

import { JdaMenu,JdaTable } from '@/components'

import { library } from '@fortawesome/fontawesome-svg-core'
import { fas } from '@fortawesome/free-solid-svg-icons'
import { far } from '@fortawesome/free-regular-svg-icons'
import { fab } from '@fortawesome/free-brands-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

library.add(fas,far,fab)

const app = createApp(App);

app.component('font-awesome-icon',FontAwesomeIcon)
app.component('JdaMenu',JdaMenu)
app.component('JdaTable',JdaTable)
app.component(VueDraggable.name,VueDraggable)
app.use(router);
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