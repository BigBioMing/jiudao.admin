<script setup lang="ts">
import { computed, reactive, ref, watch, h, createVNode, markRaw, toRaw } from 'vue';
// import {
//   MenuUnfoldOutlined,
//   MenuFoldOutlined,
// } from '@ant-design/icons-vue';

defineOptions({
  name: 'App'
})

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

const state = reactive({
  collapsed: false,
  selectedKeys: ['1'],
  openKeys: ['sub1'],
  preOpenKeys: ['sub1'],
});
const onCollapse = (collapsed: boolean, type: string) => {
  console.log(collapsed, type);
};

const onBreakpoint = (broken: boolean) => {
  console.log(broken);
};
let vn1 = createVNode('PieChartOutlined');
const items = ref([
  {
    key: '1',
    // icon: () => h(PieChartOutlined),
    icon: () => h(vn1),
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
  {
    key: 'sub21',
    icon: () => h(AppstoreOutlined),
    label: 'Navigation Two',
    title: 'Navigation Two',
    children: [
      {
        key: '91',
        label: 'Option 9',
        title: 'Option 9',
      },
      {
        key: '101',
        label: 'Option 10',
        title: 'Option 10',
      },
      {
        key: 'sub31',
        label: 'Submenu',
        title: 'Submenu',
        children: [
          {
            key: '111',
            label: 'Option 11',
            title: 'Option 11',
          },
          {
            key: '121',
            label: 'Option 12',
            title: 'Option 12',
          },
        ],
      },
    ],
  }
]);
const items2 = ref([
  {
    key: '1',
    // icon: () => h(PieChartOutlined),
    icon: 'far fa-square-caret-right',
    // icon:  (pa:any)=>{
    //   console.log(pa);
    //   return vn1
    // },
    label: 'Option 1',
    title: 'Option 1',
  },
  {
    key: '2',
    icon: 'fab fa-bandcamp',
    label: 'Option 2',
    title: 'Option 2',
  },
  {
    key: '3',
    icon: 'far fa-square-caret-right',
    label: 'Option 3',
    title: 'Option 3',
  },
  {
    key: 'aub1',
    icon: 'far fa-square-caret-right',
    label: 'Navigation One',
    title: 'Navigation One',
    children: [
      {
        key: 'a5',
        icon: 'fas fa-plane-departure',
        label: 'Option 5',
        title: 'Option 5',
      },
      {
        key: 'a6',
        label: 'Option 6',
        title: 'Option 6',
      }
    ],
  },
  {
    key: 'bub1',
    icon: 'far fa-square-caret-right',
    label: 'Navigation One',
    title: 'Navigation One',
    children: [
      {
        key: 'b5',
        icon: 'fas fa-plane-departure',
        label: 'Option 5',
        title: 'Option 5',
      },
      {
        key: 'b6',
        label: 'Option 6',
        title: 'Option 6',
      }
    ],
  },
  {
    key: 'cub1',
    icon: 'far fa-square-caret-right',
    label: 'Navigation One',
    title: 'Navigation One',
    children: [
      {
        key: 'c5',
        icon: 'fas fa-plane-departure',
        label: 'Option 5',
        title: 'Option 5',
      },
      {
        key: 'c6',
        label: 'Option 6',
        title: 'Option 6',
      }
    ],
  },
  {
    key: 'dub1',
    icon: 'far fa-square-caret-right',
    label: 'Navigation One',
    title: 'Navigation One',
    children: [
      {
        key: 'd5',
        icon: 'fas fa-plane-departure',
        label: 'Option 5',
        title: 'Option 5',
      },
      {
        key: 'd6',
        label: 'Option 6',
        title: 'Option 6',
      }
    ],
  },
  {
    key: 'sub1',
    icon: 'far fa-square-caret-right',
    label: 'Navigation One',
    title: 'Navigation One',
    children: [
      {
        key: '5',
        icon: 'fas fa-plane-departure',
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
    icon: 'fas fa-umbrella-beach',
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
  {
    key: 'sub21',
    icon: 'fas fa-cloud-sun',
    label: 'Navigation Two',
    title: 'Navigation Two',
    children: [
      {
        key: '91',
        label: 'Option 9',
        title: 'Option 9',
      },
      {
        key: '101',
        label: 'Option 10',
        title: 'Option 10',
      },
      {
        key: 'sub31',
        label: 'Submenu',
        title: 'Submenu',
        children: [
          {
            key: '111',
            label: 'Option 111',
            title: 'Option 111',
          },
          {
            key: '121',
            label: 'Option 121',
            title: 'Option 121',
            children: [
              {
                key: '1111',
                label: 'Option 111',
                title: 'Option 111',
              },
              {
                key: '1211',
                label: 'Option 121',
                title: 'Option 121',
              },
            ]
          },
        ],
      },
    ],
  }
]);

const items3 = ref([
  {
    key: '1',
    // icon: () => h(PieChartOutlined),
    icon: 'far fa-square-caret-right',
    // icon:  (pa:any)=>{
    //   console.log(pa);
    //   return vn1
    // },
    label: 'Option 1',
    title: 'Option 1',
  },
  {
    key: '2',
    icon: 'fab fa-bandcamp',
    label: 'Option 2',
    title: 'Option 2',
  },
  {
    key: '3',
    icon: 'far fa-square-caret-right',
    label: 'Option 3',
    title: 'Option 3',
  },
  {
    key: 'aub1',
    icon: 'far fa-square-caret-right',
    label: 'Navigation One',
    title: 'Navigation One',
    children: [
      {
        key: 'a5',
        icon: 'fas fa-plane-departure',
        label: 'Option 5',
        title: 'Option 5',
      },
      {
        key: 'a6',
        label: 'Option 6',
        title: 'Option 6',
      }
    ],
  },
  {
    key: 'bub1',
    icon: 'far fa-square-caret-right',
    label: 'Navigation One',
    title: 'Navigation One',
    children: [
      {
        key: 'b5',
        icon: 'fas fa-plane-departure',
        label: 'Option 5',
        title: 'Option 5',
      },
      {
        key: 'b6',
        label: 'Option 6',
        title: 'Option 6',
      }
    ],
  },
  {
    key: 'cub1',
    icon: 'far fa-square-caret-right',
    label: 'Navigation One',
    title: 'Navigation One',
    children: [
      {
        key: 'c5',
        icon: 'fas fa-plane-departure',
        label: 'Option 5',
        title: 'Option 5',
      },
      {
        key: 'c6',
        label: 'Option 6',
        title: 'Option 6',
      }
    ],
  },
  {
    key: 'dub1',
    icon: 'far fa-square-caret-right',
    label: 'Navigation One',
    title: 'Navigation One',
    children: [
      {
        key: 'd5',
        icon: 'fas fa-plane-departure',
        label: 'Option 5',
        title: 'Option 5',
      },
      {
        key: 'd6',
        label: 'Option 6',
        title: 'Option 6',
      }
    ],
  },
  {
    key: 'sub1',
    icon: 'far fa-square-caret-right',
    label: 'Navigation One',
    title: 'Navigation One',
    children: [
      {
        key: '5',
        icon: 'fas fa-plane-departure',
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
    icon: 'fas fa-umbrella-beach',
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
  {
    key: 'sub21',
    icon: 'fas fa-cloud-sun',
    label: 'Navigation Two',
    title: 'Navigation Two',
    children: [
      {
        key: '91',
        label: 'Option 9',
        title: 'Option 9',
      },
      {
        key: '101',
        label: 'Option 10',
        title: 'Option 10',
      },
      {
        key: 'sub31',
        label: 'Submenu',
        title: 'Submenu',
        children: [
          {
            key: '111',
            label: 'Option 111',
            title: 'Option 111',
          },
          {
            key: '121',
            label: 'Option 121',
            title: 'Option 121',
            children: [
              {
                key: '1111',
                label: 'Option 111',
                title: 'Option 111',
              },
              {
                key: '1211',
                label: 'Option 121',
                title: 'Option 121',
              },
            ]
          },
        ],
      },
    ],
  }
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


//设置
let isOpenSetting = ref<boolean>(false);
const onOpenSetting = () => {
  isOpenSetting.value = !isOpenSetting.value;
}
const onCloseSetting = () => {
  isOpenSetting.value = false;
}

//整体风格设置
let themeNames: ('light' | 'dark' | 'realDark')[] = ['light', 'dark', 'realDark'];
// let themeNames:Array<('light'|'dark'|'realDark')> = ['light','dark','realDark'];
let currentTheme = ref<string>('dark');
const onChangeTheme = (item: 'light' | 'dark' | 'realDark'): void => {
  currentTheme.value = item;
}

//主题色
type Color = { name: string, value: string };
let basicColors: Color[] = [
  { name: '拂晓蓝', value: 'rgb(22, 119, 255)' },
  { name: '薄暮', value: 'rgb(245, 34, 45)' },
  { name: '火山', value: 'rgb(250, 84, 28)' },
  { name: '日暮', value: 'rgb(250, 173, 20)' },
  { name: '明青', value: 'rgb(19, 194, 194)' },
  { name: '极光绿', value: 'rgb(82, 196, 26)' },
  { name: '极光蓝', value: 'rgb(47, 84, 235)' },
  { name: '酱紫', value: 'rgb(114, 46, 209)' }
];
let currentColor = ref<Color>(basicColors[0]);
const onChangeThemeColor = (item: Color): void => {
  currentColor.value = item;
}

//导航模式
type SwitchItem = {
  /** 选项值 */
  value: boolean,
  /** 是否禁用 true-禁用 */
  disabled: boolean
}
type NavigationMode = {
  /** 名称 */
  name: string,
  /** 模式 */
  mode: string,
  /** 固定 Header */
  isFixedHeader: SwitchItem,
  /** 固定侧边菜单 */
  isFixedSideMenu: SwitchItem,
  /** 自动分割菜单 */
  isAutoSplitMenu: SwitchItem
}
let navigationModes: NavigationMode[] = [
  { name: '侧边菜单布局', mode: 'side-menu', isFixedHeader: { value: true, disabled: false }, isFixedSideMenu: { value: true, disabled: false }, isAutoSplitMenu: { value: false, disabled: true } },
  { name: '顶部菜单布局', mode: 'top-menu', isFixedHeader: { value: true, disabled: false }, isFixedSideMenu: { value: false, disabled: true }, isAutoSplitMenu: { value: false, disabled: true } },
  { name: '混合布局', mode: 'mixed', isFixedHeader: { value: true, disabled: true }, isFixedSideMenu: { value: true, disabled: false }, isAutoSplitMenu: { value: false, disabled: false } },
  { name: '左侧混合布局', mode: 'left-mixed', isFixedHeader: { value: true, disabled: false }, isFixedSideMenu: { value: true, disabled: false }, isAutoSplitMenu: { value: false, disabled: true } }
]
let currentNavigationMode = ref<NavigationMode>(navigationModes[0])
const onChangeNavigationMode = (item: NavigationMode) => {
  currentNavigationMode.value = item;
}

watch(
  () => currentNavigationMode,
  (_val, oldVal) => {
    if (_val.value.isAutoSplitMenu.value) {
      var arr = items2.value.map((mn: any) => {
        mn.tempChildren = mn.children;
        mn.children = [];
        return mn;
      });

      items3.value = items2.value[0]?.children ? items2.value[0]?.children : [] as any;
    } else {
      var arr = items2.value.map((mn: any) => {
        if (mn.tempChildren !== undefined) {
          mn.children = mn.tempChildren;
          mn.tempChildren = undefined;
        }
        return mn;
      });

      items3.value = JSON.parse(JSON.stringify(toRaw(items2.value)))
    }
  }, { deep: true }
);

const onMenuItemClick = (menu: any) => {
  if (currentNavigationMode.value.isAutoSplitMenu.value) {
    var mn: any = toRaw(menu);
    let c = mn.tempChildren || [];
    items3.value = c;
  }
}

//路由动画
let routeAnimations = [{ value: 'Null', label: 'Null' }, { value: 'Slide Up', label: 'Slide Up' }, { value: 'Slide Right', label: 'Slide Right' }, { value: 'Fade In', label: 'Fade In' }, { value: 'Zoom', label: 'Zoom' }]
let currentRouteAnimation = ref(routeAnimations[0])
//多标签
let isMultipleTags = ref<boolean>(false)
//固定多标签
let isFixedMultipleTags = ref<boolean>(false)

const onSwitch = (...options: any[]) => {
  console.log('options:', options)
  console.log('currentNavigationMode:', currentNavigationMode)
}

/** 固定左侧菜单栏 */
const layoutFixedLeftMenuStyle = computed(() => {
  if (currentNavigationMode.value.isFixedSideMenu.value) {
    return {
      overflow: 'auto',
      height: '100vh',
      position: 'fixed',
      left: 0,
      top: 0,
      bottom: 0
    };
  } else {
    return {};
  }
})

const leftMenuSiderExpandWidthNum = computed(() => {
  if (currentNavigationMode.value.mode === 'left-mixed')
    return 140;
  else
    return 200;
});
const leftMenuSiderExpandWidth = computed(() => {
  return leftMenuSiderExpandWidthNum.value + 'px';
});
const layoutFixedLeftMenuRightRegionStyle = computed(() => {
  if (currentNavigationMode.value.isFixedSideMenu.value) {
    if (!state.collapsed) {
      return {
        marginLeft: leftMenuSiderExpandWidth.value,
        transition: 'margin-left 0.2s'
      };
    } else {
      return {
        marginLeft: '48px',
        transition: 'margin-left 0.2s'
      };
    }
  } else {
    return {};
  }
})
</script>

<template>
  <a-layout style="height: 100%;">
    <a-layout-sider v-if="currentNavigationMode.mode !== 'top-menu'"
      @collapse="onCollapse" @breakpoint="onBreakpoint" v-model:collapsed="state.collapsed" :trigger="null"
      :class="{ 'sider-collapsed': state.collapsed }" collapsible :style="layoutFixedLeftMenuStyle" collapsed-width="48"
      :width="leftMenuSiderExpandWidthNum" :theme="currentNavigationMode.mode === 'mixed' ? 'light' : 'dark'">
      <div class="sider-logo">
        <div>
          <img src="@/assets/logo.jpg" />
          <h1 v-if="!state.collapsed">JiuDao Admin</h1>
        </div>
      </div>
      <!-- <a-menu v-model:openKeys="state.openKeys" v-model:selectedKeys="state.selectedKeys" mode="inline" theme="dark"
        :items="items"></a-menu> -->
      <jda-menu :menus="items3" :collapsed="state.collapsed"
        :theme="currentNavigationMode.mode === 'mixed' ? 'light' : 'dark'"></jda-menu>
    </a-layout-sider>
    <a-layout :style="layoutFixedLeftMenuRightRegionStyle">
      <a-layout-header v-if="currentNavigationMode.isFixedHeader.value"
        :style="{ padding: 0, lineHeight: '48px', height: '48px', width: '100%' }">
      </a-layout-header>
      <a-layout-header
        :class="{ 'header': true, 'layout-fixed-header-menu': currentNavigationMode.isFixedHeader.value }"
        v-if="currentNavigationMode.mode === 'top-menu' || currentNavigationMode.mode === 'mixed'"
        :style="{ padding: 0, lineHeight: '48px', height: '48px' }">
        <div class="header-main">
          <div class="header-left">
            <div class="header-logo">
              <div>
                <img src="@/assets/logo.jpg" />
                <h1>JiuDao Admin</h1>
              </div>
            </div>
          </div>
          <div class="header-middle">
            <div v-if="currentNavigationMode.mode === 'mixed' && !currentNavigationMode.isAutoSplitMenu.value">
              <menu-unfold-outlined v-if="state.collapsed" class="trigger" @click="state.collapsed = !state.collapsed"
                style="color:#fff;" />
              <menu-fold-outlined v-else class="trigger" @click="state.collapsed = !state.collapsed"
                style="color:#fff;" />
              <div style="display: inline-block;color:#fff;" class="heaer-menu">
                <a-tooltip title="刷新页面">
                  <ReloadOutlined />
                </a-tooltip>
              </div>
            </div>
            <jda-menu v-if="currentNavigationMode.mode === 'top-menu' || currentNavigationMode.isAutoSplitMenu.value"
              :menus="items2" :collapsed="state.collapsed" @on-menu-item-click="onMenuItemClick"
              mode="horizontal"></jda-menu>
          </div>
          <div class="header-right">
            <div class="header-menu-top-menu" style="display: inline-block;float:right;margin-right: 20px;">

              <!-- 头像昵称 -->
              <a-dropdown :trigger="['click']" class="heaer-menu heaer-menu-right" placement="bottomRight">
                <a @click.prevent>
                  <a-avatar v-if="true" size="small"
                    src="https://gw.alipayobjects.com/zos/antfincdn/XAosXuNZyF/BiazfanxmamNRoxxVxka.png" />
                  <a-avatar v-else size="small">
                    <template #icon>
                      <UserOutlined />
                    </template>
                  </a-avatar>
                  <span style="margin-left:5px;position: relative; top: 2px;">{{ nickName }}</span>
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
          </div>
        </div>
      </a-layout-header>
      <a-layout-header v-if="currentNavigationMode.mode === 'side-menu' || currentNavigationMode.mode === 'left-mixed'"
        :class="{ 'layout-fixed-header-menu_layout-left-menu': currentNavigationMode.isFixedHeader.value }"
        :style="{ backgroundColor: '#fff', padding: 0, lineHeight: '48px', height: '48px', left: state.collapsed ? '48px' : leftMenuSiderExpandWidth }">
        <!-- <menu-unfold-outlined v-if="state.collapsed" class="trigger" @click="toggleCollapsed" />
        <menu-fold-outlined v-else class="trigger" @click="toggleCollapsed" /> -->
        <menu-unfold-outlined v-if="state.collapsed" class="trigger" @click="state.collapsed = !state.collapsed" />
        <menu-fold-outlined v-else class="trigger" @click="state.collapsed = !state.collapsed" />
        <div style="display: inline-block;" class="heaer-menu">
          <a-tooltip title="刷新页面">
            <ReloadOutlined />
          </a-tooltip>
        </div>
        <div style="display: inline-block;float:right;margin-right: 20px;">

          <!-- 头像昵称 -->
          <a-dropdown :trigger="['click']" class="heaer-menu heaer-menu-right" placement="bottomRight">
            <a @click.prevent>
              <a-avatar v-if="true" size="small"
                src="https://gw.alipayobjects.com/zos/antfincdn/XAosXuNZyF/BiazfanxmamNRoxxVxka.png" />
              <a-avatar v-else size="small">
                <template #icon>
                  <UserOutlined />
                </template>
              </a-avatar>
              <span style="margin-left:5px;position: relative; top: 2px;">{{ nickName }}</span>
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
      <a-layout style="min-height: 100vh">
        <a-layout-sider v-if="true" collapsible>
          <div class="logo" />
          <jda-menu :menus="items3" :collapsed="state.collapsed"
            :theme="currentNavigationMode.mode === 'mixed' ? 'light' : 'dark'"></jda-menu>
        </a-layout-sider>
        <a-layout>
          <a-layout-content :style="{ margin: '24px', background: '#fff' }">
            <router-view />
          </a-layout-content>
          <a-layout-footer style="text-align: center">
            Copyright ©2024 Created by JiuDao
          </a-layout-footer>
        </a-layout>
      </a-layout>
    </a-layout>
  </a-layout>
  <div class="settings" @click="onOpenSetting" :class="{ 'settings-open': isOpenSetting }">
    <SettingOutlined />
  </div>
  <a-drawer title="" :open="isOpenSetting" @close="onCloseSetting" width="300">
    <div class="settings-content">
      <div class="settings-item">
        <h3>整体风格设置</h3>
        <div>
          <div class="settings-global-block">
            <div v-for="(theme, index) in themeNames" :key="theme" @click="onChangeTheme(theme)"
              :class="`settings-global-item settings-global-item-${theme}`">
              <div class="inner"></div>
              <CheckOutlined v-if="theme === currentTheme"
                :class="{ 'settings-global-item-select-icon': theme === currentTheme }" />
            </div>
          </div>
        </div>
      </div>
      <div class="settings-item">
        <h3>主题色</h3>
        <div>
          <div class="settings-theme-color-block clearfix">
            <div v-for="(color, index) in basicColors" :key="color.value" @click="onChangeThemeColor(color)"
              class="clearfix settings-theme-color" :style="{ 'background-color': color.value }">
              <CheckOutlined v-if="color.value === currentColor.value" />
            </div>
          </div>
        </div>
      </div>
      <a-divider />
      <div class="settings-item">
        <h3>导航模式</h3>
        <div>
          <div class="settings-global-block">
            <div v-for="(mode, index) in navigationModes" :key="mode.mode" @click="onChangeNavigationMode(mode)"
              :class="`settings-global-item settings-global-item-${mode.mode}`">
              <div class="inner"></div>
              <CheckOutlined v-if="mode.mode === currentNavigationMode.mode"
                :class="{ 'settings-global-item-select-icon': mode.mode === currentNavigationMode.mode }" />
            </div>
          </div>
        </div>
      </div>
      <div class="settings-item">
        <div class="settings-form">
          <div class="settings-form-item">
            <span>固定 Header</span>
            <div>
              <a-switch v-model:checked="currentNavigationMode.isFixedHeader.value" size="small" @change="onSwitch"
                :disabled="currentNavigationMode.isFixedHeader.disabled" />
            </div>
          </div>
          <div class="settings-form-item">
            <span>固定侧边菜单</span>
            <div>
              <a-switch v-model:checked="currentNavigationMode.isFixedSideMenu.value" size="small"
                :disabled="currentNavigationMode.isFixedSideMenu.disabled" />
            </div>
          </div>
          <div class="settings-form-item">
            <span>自动分割菜单</span>
            <div>
              <a-switch v-model:checked="currentNavigationMode.isAutoSplitMenu.value" size="small"
                :disabled="currentNavigationMode.isAutoSplitMenu.disabled" />
            </div>
          </div>
        </div>
      </div>
      <a-divider />
      <div class="settings-item">
        <h3>其他设置</h3>
        <div class="settings-form">
          <div class="settings-form-item">
            <span>路由动画</span>
            <div>
              <a-select size="small" v-model:value="currentRouteAnimation" style="width: 100px"
                :options="routeAnimations"></a-select>
            </div>
          </div>
          <div class="settings-form-item">
            <span>多标签</span>
            <div>
              <a-switch v-model:checked="isMultipleTags" size="small" />
            </div>
          </div>
          <div class="settings-form-item">
            <span>固定多标签</span>
            <div>
              <a-switch v-model:checked="isFixedMultipleTags" size="small" />
            </div>
          </div>
        </div>
      </div>
    </div>
  </a-drawer>
</template>

<style lang="scss" scoped>
.trigger {
  font-size: 18px;
  line-height: 54px;
  padding: 0 24px;
  cursor: pointer;
  transition: color 0.3s;
}

.trigger:hover {
  color: #1890ff;
}

.sider-logo {
  position: relative;
  display: flex;
  align-items: center;
  padding: 16px;
  line-height: 32px;
  cursor: pointer;

  div {
    display: flex;
    align-items: center;
    justify-content: center;
    min-height: 32px;
  }

  img {
    display: inline-block;
    height: 32px;
    vertical-align: middle;
    transition: height .2s;
  }

  h1 {
    display: inline-block;
    height: 32px;
    margin: 0 0 0 12px;
    overflow: hidden;
    color: #fff;
    font-weight: 600;
    font-size: 18px;
    line-height: 32px;
    vertical-align: middle;
    animation: fade-in;
    animation-duration: .2s;
  }
}

.sider-collapsed {
  .sider-logo {
    padding: 16px 8px;
  }
}


.header {

  .header-main {
    display: flex;
    flex-direction: row;
    padding-left: 16px;


    .header-left {
      display: flex;
      flex-direction: row;
      min-width: 192px;
    }

    .header-middle {
      flex: 1 1 0%;
      overflow: hidden;
    }

    .header-right {
      min-width: 208px;
    }
  }
}

.header-logo {
  position: relative;
  min-width: 165px;
  height: 100%;
  overflow: hidden;
  transition: all .3s;

  img {
    display: inline-block;
    height: 32px;
    vertical-align: middle;
  }

  h1 {
    display: inline-block;
    margin: 0 0 0 12px;
    color: #fff;
    font-weight: 400;
    font-size: 16px;
    vertical-align: top;
  }
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

.header-menu-top-menu {
  .heaer-menu {
    color: #fff;
  }
}

.heaer-menu-right {
  padding: 0 10px;

  &:hover {
    background: rgba(0, 0, 0, 0.025);
  }
}

.dropdown-menu {
  :deep(.ant-dropdown-menu-item) {
    color: rgba(0, 0, 0, 0.65);
  }
}

.settings {
  position: absolute;
  top: 240px;
  right: 0;
  z-index: 99999;
  display: -webkit-box;
  display: -ms-flexbox;
  display: flex;
  -webkit-box-align: center;
  -ms-flex-align: center;
  align-items: center;
  -webkit-box-pack: center;
  -ms-flex-pack: center;
  justify-content: center;
  width: 48px;
  height: 48px;
  font-size: 16px;
  text-align: center;
  background: #1890ff;
  border-radius: 4px 0 0 4px;
  cursor: pointer;
  pointer-events: auto;
  color: #fff;

  transition: right 0.3s ease;
  -ms-transition: right 0.3s ease;
  -webkit-transition: right 0.3s ease;
  -o-transition: right 0.3s ease;
  -moz-transition: right 0.3s ease;
}

.settings-open {
  right: 300px;
}

@keyframes settings-animation {
  from {
    right: 0;
  }

  to {
    right: 300px;
  }
}

@keyframes settings-animation2 {
  from {
    right: 300px;
  }

  to {
    right: 0;
  }
}

.settings-content {
  width: 100%;
  height: auto;

  .settings-item {
    width: 100%;
    margin-bottom: 24px;
  }
}

.settings-global-block {
  display: flex;

  .settings-global-item {
    position: relative;
    width: 44px;
    height: 36px;
    margin-right: 16px;
    overflow: hidden;
    background-color: #f0f2f5;
    border-radius: 4px;
    box-shadow: 0 1px 2.5px rgba(0, 0, 0, .18);
    cursor: pointer;

    .inner {
      display: none;
    }


    &:before {
      position: absolute;
      top: 0;
      left: 0;
      width: 33%;
      height: 100%;
      background-color: #fff;
      content: "";
    }

    &:after {
      position: absolute;
      top: 0;
      left: 0;
      width: 100%;
      height: 25%;
      background-color: #fff;
      content: "";
    }
  }

  .settings-global-item-select-icon {
    position: absolute;
    right: 6px;
    bottom: 4px;
    color: #1677ff;
    font-weight: 700;
    font-size: 14px;
    pointer-events: none;
  }

  /** 主题 */
  .settings-global-item-light {
    &:before {
      background-color: #fff;
      content: "";
    }

    &:after {
      background-color: #fff;
    }
  }

  .settings-global-item-dark {
    &:before {
      z-index: 1;
      background-color: #001529;
      content: "";
    }

    &:after {
      background-color: #fff;
    }
  }

  .settings-global-item-realDark {
    background-color: #001529d9;

    &:before {
      z-index: 1;
      background-color: #001529a6;
      content: "";
    }

    &:after {
      background-color: #001529d9;
    }
  }


  /** 导航模式 */
  .settings-global-item-side-menu {

    &:before {
      z-index: 1;
      background-color: #001529;
      content: "";
    }

    &:after {
      background-color: #fff;
    }
  }

  .settings-global-item-top-menu {

    &:before {
      position: absolute;
      top: 0;
      left: 0;
      width: 0;
      height: 0;
      background-color: #fff;
      content: "";
    }

    &:after {
      background-color: #001529;
    }
  }

  .settings-global-item-mixed {

    &:before {
      background-color: #fff;
      content: " ";
    }

    &:after {
      background-color: #001529;
    }
  }

  .settings-global-item-left-mixed {
    .inner {
      position: absolute;
      top: 0;
      left: 0;
      display: block;
      width: 33%;
      height: 100%;
      background-color: #fff;
      content: "";
    }

    &:before {
      z-index: 1;
      width: 16%;
      background-color: #001529;
      content: "";
    }

    &:after {}
  }
}

.settings-theme-color-block {
  margin-top: 16px;

  .settings-theme-color {
    display: flex;
    align-items: center;
    justify-content: center;
    float: left;
    width: 20px;
    height: 20px;
    margin-top: 8px;
    margin-right: 8px;
    color: #fff;
    font-weight: 700;
    text-align: center;
    border-radius: 2px;
    cursor: pointer;
  }
}

.settings-form {

  .settings-form-item {
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 12px 24px;
    color: rgba(0, 0, 0, 0.88);
    box-sizing: border-box;
  }
}

.clearfix {
  zoom: 1;

  &::before,
  &::after {
    display: table;
    content: ' ';
  }

  &::after {
    height: 0;
    clear: both;
    font-size: 0;
    visibility: hidden;
  }
}


/** 固定左侧菜单栏 */
/** 左侧菜单栏 */
.layout-fixed-left-menu {
  overflow: 'auto';
  height: '100vh';
  position: 'fixed';
  left: 0;
  top: 0;
  bottom: 0
}

/** 固定头部菜单栏 */
/** 头部菜单栏 */
.layout-fixed-header-menu {
  position: fixed;
  top: 0;
  transition: width 0.2s;
  left: 0;
  right: 0;
}

/** 头部菜单栏 -侧边菜单布局的头部菜单栏 */
.layout-fixed-header-menu_layout-left-menu {
  position: fixed;
  top: 0;
  left: 200px;
  right: 0;
  transition: left 0.2s ease;
  -ms-transition: left 0.2s ease;
  -webkit-transition: left 0.2s ease;
  -o-transition: left 0.2s ease;
  -moz-transition: left 0.2s ease;
}

/** 右侧内容区域 */
.layout-fixed-left-menu-right-region {
  margin-left: 200px;
  transition: margin-left 0.3s ease;
  -ms-transition: margin-left 0.3s ease;
  -webkit-transition: margin-left 0.3s ease;
  -o-transition: margin-left 0.3s ease;
  -moz-transition: margin-left 0.3s ease;
}
</style>
