import Antd from 'ant-design-vue';
import AntdIcons from '@ant-design/icons-vue';

import { createApp } from 'vue'
import App from './App.vue'
import 'ant-design-vue/dist/reset.css';

const app = createApp(App);

app.use(Antd).mount('#app');