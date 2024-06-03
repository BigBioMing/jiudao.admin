<script setup lang="ts">
import { reactive, ref, watch, h, createVNode, render, createCommentVNode, onMounted } from 'vue';
// import {
//   MenuUnfoldOutlined,
//   MenuFoldOutlined,
// } from '@ant-design/icons-vue';

import {
  MenuFoldOutlined,
  MenuUnfoldOutlined,
  PieChartOutlined,
  MailOutlined,
  DesktopOutlined,
  InboxOutlined,
  AppstoreOutlined,
} from '@ant-design/icons-vue';
console.log(import.meta.env)
const selectedKeys = ref<string[]>(['1']);
const collapsed = ref<boolean>(false);

const state = reactive({
  collapsed: false,
  selectedKeys: ['1'],
  openKeys: ['sub1'],
  preOpenKeys: ['sub1'],
});
let vn1 = createVNode('PieChartOutlined');
const items = reactive([
  {
    key: '1',
    // icon: () => h(PieChartOutlined),
    icon: h(vn1),
    // icon:  (pa:any)=>{
    //   console.log(pa);
    //   return vn1
    // },
    label: 'Option 1',
    title: 'Option 1',
  },
  {
    key: '2',
    icon: () => h(DesktopOutlined),
    label: 'Option 2',
    title: 'Option 2',
  },
  {
    key: '3',
    icon: () => h(InboxOutlined),
    label: 'Option 3',
    title: 'Option 3',
  },
  {
    key: 'sub1',
    icon: () => h(MailOutlined),
    label: 'Navigation One',
    title: 'Navigation One',
    children: [
      {
        key: '5',
        label: 'Option 5',
        title: 'Option 5',
      },
      {
        key: '6',
        label: 'Option 6',
        title: 'Option 6',
      },
      {
        key: '7',
        label: 'Option 7',
        title: 'Option 7',
      },
      {
        key: '8',
        label: 'Option 8',
        title: 'Option 8',
      },
    ],
  },
  {
    key: 'sub2',
    icon: () => h(AppstoreOutlined),
    label: 'Navigation Two',
    title: 'Navigation Two',
    children: [
      {
        key: '9',
        label: 'Option 9',
        title: 'Option 9',
      },
      {
        key: '10',
        label: 'Option 10',
        title: 'Option 10',
      },
      {
        key: 'sub3',
        label: 'Submenu',
        title: 'Submenu',
        children: [
          {
            key: '11',
            label: 'Option 11',
            title: 'Option 11',
          },
          {
            key: '12',
            label: 'Option 12',
            title: 'Option 12',
          },
        ],
      },
    ],
  },
]);
watch(
  () => state.openKeys,
  (_val, oldVal) => {
    state.preOpenKeys = oldVal;
  },
);
const toggleCollapsed = () => {
  state.collapsed = !state.collapsed;
  state.openKeys = state.collapsed ? [] : state.preOpenKeys;
};

//改变语言
const languagesOptions = ['cn 简体中文', 'us English'];
const onChangeLanguage = (item: 'cn 简体中文' | 'us English'): void => {

}

//昵称
let nickName = ref<string>('张三')
</script>

<template>
  <a-layout>
    <a-layout-sider v-model:collapsed="state.collapsed" :trigger="null" collapsible>
      <div class="logo" />
      <a-menu v-model:openKeys="state.openKeys" v-model:selectedKeys="state.selectedKeys" mode="inline" theme="dark"
        :inline-collapsed="state.collapsed" :items="items"></a-menu>
    </a-layout-sider>
    <a-layout>
      <a-layout-header style="background: #fff; padding: 0">
        <menu-unfold-outlined v-if="collapsed" class="trigger" @click="toggleCollapsed" />
        <menu-fold-outlined v-else class="trigger" @click="toggleCollapsed" />
        <div style="display: inline-block;" class="heaer-menu">
          <a-tooltip title="刷新页面">
            <ReloadOutlined />
          </a-tooltip>
        </div>
        <div style="display: inline-block;float:right;margin-right: 20px;">

          <!-- 头像昵称 -->
          <a-dropdown :trigger="['click']" class="heaer-menu heaer-menu-right" placement="bottomRight">
            <a @click.prevent>
              <a-avatar v-if="true"
                src="https://gw.alipayobjects.com/zos/antfincdn/XAosXuNZyF/BiazfanxmamNRoxxVxka.png" />
              <a-avatar v-else>
                <template #icon>
                  <UserOutlined />
                </template>
              </a-avatar>
              <span style="margin-left:5px;    position: relative; top: 2px;">{{ nickName }}</span>
            </a>
            <template #overlay>
              <a-menu class="dropdown-menu" style="width:160px;">
                <a-menu-item key="0">
                  <div>
                    <UserOutlined />
                    <span style="margin-left: 15px;">个人中心</span>
                  </div>
                </a-menu-item>
                <a-menu-item key="1">
                  <div>
                    <SettingOutlined />
                    <span style="margin-left: 15px;">个人设置</span>
                  </div>
                </a-menu-item>
                <a-menu-divider />
                <a-menu-item key="3">
                 <LogoutOutlined />
                    <span style="margin-left: 15px;">退出登录</span>
                </a-menu-item>
              </a-menu>
            </template>
          </a-dropdown>

          <!-- 语言 -->
          <a-dropdown :trigger="['click']" class="heaer-menu heaer-menu-right" placement="bottomRight">
            <a @click.prevent>
              <global-outlined style="position: relative; top: 4px;" />
            </a>
            <template #overlay>
              <a-menu class="dropdown-menu" style="width:140px;">
                <a-menu-item v-for="(item, index) in languagesOptions" :key="index">
                  <span>{{ item }}</span>
                </a-menu-item>
              </a-menu>
            </template>
          </a-dropdown>
        </div>
      </a-layout-header>
      <a-layout-content :style="{ margin: '24px 16px', padding: '24px', background: '#fff', minHeight: '280px' }">
        Content
      </a-layout-content>
    </a-layout>
  </a-layout>
</template>

<style lang="scss" scoped>
.trigger {
  font-size: 18px;
  line-height: 64px;
  padding: 0 24px;
  cursor: pointer;
  transition: color 0.3s;
}

.trigger:hover {
  color: #1890ff;
}

.logo {
  height: 32px;
  background: rgba(255, 255, 255, 0.3);
  margin: 16px;
}

.site-layout .site-layout-background {
  background: #fff;
}

.heaer-menu {
  cursor: pointer;
  display: inline-block;
  height: 100%;
  color: rgba(0, 0, 0, 0.65);

  .anticon {
    font-size: 18px;
  }
}

.heaer-menu-right {
  padding: 0 10px;

  &:hover {
    background: rgba(0, 0, 0, 0.025);
  }
}

.dropdown-menu{
  :deep(.ant-dropdown-menu-item){
  color: rgba(0, 0, 0, 0.65);
  }
}
</style>
