<script setup lang="ts">
import { computed, reactive, ref, watch, h, createVNode, markRaw, toRaw, onMounted } from 'vue';
import { ConfigProvider, theme } from 'ant-design-vue';

defineOptions({
  name: 'basic-layout'
})

import {
  MenuFoldOutlined,
  MenuUnfoldOutlined
} from '@ant-design/icons-vue';
import { VALUE_SPLIT } from 'ant-design-vue/es/vc-cascader/utils/commonUtil';
console.log(import.meta.env)

let isMobile = ref<Boolean>(false);
const onWindowResize = () => {
  if (window.matchMedia('(max-width: 800px)').matches) {
    isMobile.value = true;
  } else {
    isMobile.value = false;
  }
}

let isOpenMobileSiderMenu = ref<boolean>(false);
const onChangeMobileSiderMenu = () => {
  isOpenMobileSiderMenu.value = !isOpenMobileSiderMenu.value;
}

let mobileSiderMenuStyle = computed(() => {
  if (settingsConfig.value.currentThemeSkin === 'realDark') {
    return {
      'background-color': '#001529'
    }
  } else if (settingsConfig.value.currentThemeSkin === 'light') {
    return {
      'background-color': '#ffffff'
    }
  } else if (settingsConfig.value.currentThemeSkin === 'dark') {
    return {
      'background-color': '#001529'
    }
  } else {
    return {
      'background-color': '#001529'
    }
  }
})

//监听窗口变化，变换左侧菜单栏
onWindowResize();
window.addEventListener('resize', () => {
  onWindowResize();
})

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

const menus = [
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
];
// const menus = ref(noRefMenus);
// const subMenus = ref([]);
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
let themeStyleNames: ('light' | 'dark' | 'realDark')[] = ['light', 'dark', 'realDark'];
// let themeStyleNames:Array<('light'|'dark'|'realDark')> = ['light','dark','realDark'];
const onChangeThemeStyle = (item: 'light' | 'dark' | 'realDark'): void => {
  settingsConfig.value.currentThemeSkin = item;
  document.body.className = themeStyleClass.value;
}

const themeStyleClass = computed(() => {
  if (settingsConfig.value.currentThemeSkin === 'light')
    return 'theme-style-light';
  else if (settingsConfig.value.currentThemeSkin === 'dark')
    return 'theme-style-dark';
  else if (settingsConfig.value.currentThemeSkin === 'realDark')
    return 'theme-style-real-dark';

  return 'theme-style-light';
})

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
const onChangeThemeColor = (item: Color): void => {
  settingsConfig.value.currentThemeStyle.colorPrimary = item;
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
const onChangeNavigationMode = (item: NavigationMode) => {
  settingsConfig.value.currentNavigationMode = item;
}



//当前点击的header菜单
let currentMenuItemHeader = ref();
//当前点击的侧边栏菜单
let currentMenuItemSider = ref();
//当前点击的子侧边栏菜单
let currentMenuItemSubSider = ref();
const onMenuItemClick = (pos: 'top-menu' | 'side-menu' | 'sub-side-menu', menu: any) => {
  if (pos === 'top-menu')
    currentMenuItemHeader.value = menu;
  else if (pos === 'side-menu')
    currentMenuItemSider.value = menu;
  else if (pos === 'sub-side-menu')
    currentMenuItemSubSider.value = menu;

  if (settingsConfig.value.isMultipleTags) {
    //添加tab
    let tabMenu = null;
    if (pos === 'top-menu') {
      if (!settingsConfig.value.currentNavigationMode.isAutoSplitMenu.value)
        tabMenu = menu;
    }
    else if (pos === 'side-menu') {
      if (settingsConfig.value.currentNavigationMode.mode !== 'mixed')
        tabMenu = menu;
    }
    else if (pos === 'sub-side-menu')
      tabMenu = menu;

    if (tabMenu) {
      addTab({ key: menu.key, title: menu.title });
    }
  }
}

const getTopMenusNoRef = (menus: any[]) => {
  let topMenus = [];
  for (let i = 0; i < menus.length; i++) {
    var menu = menus[i];
    var newMenu = { ...menu };
    newMenu.children = [];
    newMenu.tempChildren = menu.children;
    topMenus.push(newMenu)
  }
  return topMenus;
}

/** 头部菜单栏 */
const headerMenus = computed(() => {
  let hMenus = [];
  if (isMobile.value) {
    return [];
  }

  //根据布局类型设置头部菜单栏
  if (settingsConfig.value.currentNavigationMode.mode === 'side-menu') {
    hMenus = [];
  } else if (settingsConfig.value.currentNavigationMode.mode === 'top-menu') {
    hMenus = menus;
  } else if (settingsConfig.value.currentNavigationMode.mode === 'mixed') {
    hMenus = [];
  } else if (settingsConfig.value.currentNavigationMode.mode === 'left-mixed') {
    hMenus = [];
  }

  //如果分割了菜单
  if (settingsConfig.value.currentNavigationMode.isAutoSplitMenu.value) {
    hMenus = getTopMenusNoRef(menus);
  }

  return hMenus;
})

/** 侧边菜单栏 */
const siderMenus = computed(() => {
  let sMenus = [];
  if (isMobile.value) {
    return menus;
  }

  //根据布局类型设置侧边栏
  if (settingsConfig.value.currentNavigationMode.mode === 'side-menu') {
    sMenus = menus;
  } else if (settingsConfig.value.currentNavigationMode.mode === 'top-menu') {
    sMenus = [];
  } else if (settingsConfig.value.currentNavigationMode.mode === 'mixed') {
    sMenus = menus;
  } else if (settingsConfig.value.currentNavigationMode.mode === 'left-mixed') {
    sMenus = getTopMenusNoRef(menus);
  }

  //如果分割了菜单，则侧边栏设置为当前点击的头部菜单的子菜单
  if (settingsConfig.value.currentNavigationMode.isAutoSplitMenu.value) {
    sMenus = currentMenuItemHeader?.value?.tempChildren || [];
  }

  return sMenus;
})

/** 子侧边菜单栏 */
const subSiderMenus = computed(() => {
  let ssMenus = [];

  //根据布局类型设置子侧边栏
  if (settingsConfig.value.currentNavigationMode.mode === 'left-mixed') {
    //将子侧边栏设置为当前点击的侧边栏菜单的子菜单
    ssMenus = currentMenuItemSider?.value?.tempChildren || [];
  }

  return ssMenus;
})

//路由动画
let routeAnimations = [{ value: 'Null', label: 'Null' }, { value: 'Slide Up', label: 'Slide Up' }, { value: 'Slide Right', label: 'Slide Right' }, { value: 'Fade In', label: 'Fade In' }, { value: 'Zoom', label: 'Zoom' }]


//设置
let settingsConfig = ref({
  //主题风格
  currentThemeSkin: 'dark',
  //主题样式
  currentThemeStyle: { colorPrimary: { name: '拂晓蓝', value: 'rgb(22, 119, 255)' }, borderRadius: '5px' },
  //导航模式
  currentNavigationMode: navigationModes[0],
  //多标签
  isMultipleTags: false,
  //固定多标签
  isFixedMultipleTags: false,
  //路由动画
  currentRouteAnimation: routeAnimations[0]
});

watch(
  () => settingsConfig,
  (_val, oldVal) => {
    console.log(_val, oldVal);
    let sc = toRaw(_val.value);
    localStorage.setItem('settings-config', JSON.stringify(sc));
  }, { deep: true }//,{deep: true,immediate: true}
);

const onSwitch = (...options: any[]) => {
  console.log('options:', options)
}

/** 固定左侧菜单栏 */
const layoutFixedLeftMenuStyle = computed(() => {
  if (settingsConfig.value.currentNavigationMode.isFixedSideMenu.value) {
    return {
      overflow: 'auto',
      height: '100vh',
      position: 'fixed',
      left: 0,
      top: 0,
      bottom: 0,
      zIndex: 9
    };
  } else {
    return {};
  }
})

const leftMenuSiderExpandWidthNum = computed(() => {
  if (isMobile.value)
    return 200;

  if (settingsConfig.value.currentNavigationMode.mode === 'left-mixed')
    return 140;
  else
    return 200;
});
const leftMenuSiderExpandWidth = computed(() => {
  return leftMenuSiderExpandWidthNum.value + 'px';
});
const layoutFixedLeftMenuRightRegionStyle = computed(() => {

  //如果是移动端，则margin-left=0
  if (isMobile.value) {
    return {
      marginLeft: 0,
      transition: 'margin-left 0.2s'
    };
  }

  if (settingsConfig.value.currentNavigationMode.isFixedSideMenu.value) {
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

const multipleTag = reactive<{
  activeKey: string,
  tabs: { title: string; key: string; closable?: boolean }[]
}>({
  activeKey: '',
  tabs: []
});
const onEdit = (targetKey: string) => {

};
const tabMenuOptions = [
  "关闭其他", "刷新当前页"
]
const addTab = (tab: { title: string; key: string; closable?: boolean }) => {
  let exists = multipleTag.tabs.some(n => n.key === tab.key);
  if (!exists) {
    multipleTag.activeKey = tab.key;
    multipleTag.tabs.push(tab);
  }
}

const { darkAlgorithm, compactAlgorithm } = theme;
const providerTheme = computed(() => {
  let token = { colorPrimary: settingsConfig.value.currentThemeStyle.colorPrimary.value, borderRadius: settingsConfig.value.currentThemeStyle.borderRadius };

  if (settingsConfig.value.currentThemeSkin === 'light') {
    return {
      algorithm: theme.defaultAlgorithm,
      token: { colorPrimary: settingsConfig.value.currentThemeStyle.colorPrimary.value, borderRadius: settingsConfig.value.currentThemeStyle.borderRadius }
    }
  }
  else if (settingsConfig.value.currentThemeSkin === 'dark') {
    return {
      algorithm: theme.defaultAlgorithm,
      token: { colorPrimary: settingsConfig.value.currentThemeStyle.colorPrimary.value, borderRadius: settingsConfig.value.currentThemeStyle.borderRadius }
    }
  }
  else if (settingsConfig.value.currentThemeSkin === 'realDark') {
    return {
      algorithm: theme.darkAlgorithm,
      token: { colorText: 'rgba(229, 224, 216, 0.88)', colorBgContainer: 'rgb(36, 37, 37)', colorPrimary: settingsConfig.value.currentThemeStyle.colorPrimary.value, borderRadius: settingsConfig.value.currentThemeStyle.borderRadius },
      components: { Card: { colorBgContainer: 'rgb(36, 37, 37)' } }
    }
  }

  return {
    algorithm: theme.defaultAlgorithm,
    token: { colorPrimary: settingsConfig.value.currentThemeStyle.colorPrimary.value, borderRadius: settingsConfig.value.currentThemeStyle.borderRadius },
  }
});

let headerMenuControlStyle = computed(() => {
  if (settingsConfig.value.currentThemeSkin === 'light' && settingsConfig.value.currentNavigationMode.mode === 'mixed') {
    return {
      color: 'color: rgba(0, 0, 0, 0.65);'
    }
  } else {
    return {
      color: '#fff'
    }
  }
})

// onMounted(() => {
let scstr = localStorage.getItem('settings-config');
if (scstr) {
  let sc = JSON.parse(scstr);
  settingsConfig.value = sc;
  onChangeThemeStyle(settingsConfig.value.currentThemeSkin as any);
}
// });
</script>

<template>
  <a-config-provider :theme="providerTheme">
    <a-watermark content="JiuDao Admin" style="height:100%;width:100%;"
      :font="{ color: 'rgba(0,0,0,.3)', fontSize: 14 }">
      <a-layout style="height: 100%;" :class="themeStyleClass">
        <a-layout-sider v-if="!isMobile && settingsConfig.currentNavigationMode.mode !== 'top-menu'"
          @collapse="onCollapse" @breakpoint="onBreakpoint" v-model:collapsed="state.collapsed" :trigger="null"
          :class="{ 'sider-collapsed': state.collapsed }" collapsible :style="layoutFixedLeftMenuStyle"
          collapsed-width="48" :width="leftMenuSiderExpandWidthNum"
          :theme="(settingsConfig.currentNavigationMode.mode === 'mixed' || settingsConfig.currentThemeSkin === 'light') ? 'light' : 'dark'">
          <div
            :class="{ 'sider-logo': true, 'sider-logo-dark': settingsConfig.currentNavigationMode.mode === 'mixed' }">
            <div>
              <img src="@/assets/logo.jpg" />
              <h1 v-if="!state.collapsed">JiuDao Admin</h1>
            </div>
          </div>
          <!-- <a-menu v-model:openKeys="state.openKeys" v-model:selectedKeys="state.selectedKeys" mode="inline" theme="dark"
        :items="items"></a-menu> -->
          <jda-menu :menus="siderMenus" :collapsed="state.collapsed"
            :theme="(settingsConfig.currentNavigationMode.mode === 'mixed' || settingsConfig.currentThemeSkin === 'light') ? 'light' : 'dark'"
            @on-menu-item-click="(menu: any) => onMenuItemClick('side-menu', menu)"></jda-menu>
        </a-layout-sider>
        <a-layout :style="layoutFixedLeftMenuRightRegionStyle">
          <a-layout-header v-if="settingsConfig.currentNavigationMode.isFixedHeader.value" class="header"
            :style="{ padding: 0, lineHeight: '48px', height: '48px', width: '100%' }">
          </a-layout-header>
          <a-layout-header :class="{
            'header': true, 'header-dark': true, 'layout-fixed-header-menu': settingsConfig.currentNavigationMode.isFixedHeader.value
          }"
            v-if="!isMobile && (settingsConfig.currentNavigationMode.mode === 'top-menu' || settingsConfig.currentNavigationMode.mode === 'mixed')"
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
                <div
                  v-if="settingsConfig.currentNavigationMode.mode === 'mixed' && !settingsConfig.currentNavigationMode.isAutoSplitMenu.value">
                  <menu-unfold-outlined v-if="state.collapsed" class="trigger"
                    @click="state.collapsed = !state.collapsed" :style="headerMenuControlStyle" />
                  <menu-fold-outlined v-else class="trigger" @click="state.collapsed = !state.collapsed"
                    :style="headerMenuControlStyle" />
                  <div style="display: inline-block;" :style="headerMenuControlStyle" class="heaer-menu">
                    <a-tooltip title="刷新页面">
                      <ReloadOutlined />
                    </a-tooltip>
                  </div>
                </div>
                <jda-menu
                  v-if="settingsConfig.currentNavigationMode.mode === 'top-menu' || settingsConfig.currentNavigationMode.isAutoSplitMenu.value"
                  :menus="headerMenus" :collapsed="state.collapsed"
                  @on-menu-item-click="(menu: any) => onMenuItemClick('top-menu', menu)" mode="horizontal"
                  :theme="settingsConfig.currentThemeSkin === 'light' ? 'light' : 'dark'"></jda-menu>
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
          <a-layout-header
            v-if="isMobile || (settingsConfig.currentNavigationMode.mode === 'side-menu' || settingsConfig.currentNavigationMode.mode === 'left-mixed')"
            :class="{
              'header': true, 'header-light': true, 'layout-fixed-header-menu_layout-left-menu': settingsConfig.currentNavigationMode.isFixedHeader.value
            }"
            :style="{ padding: 0, lineHeight: '48px', height: '48px', left: isMobile ? 0 : (state.collapsed ? '48px' : leftMenuSiderExpandWidth) }"
            theme="light">
            <!-- <menu-unfold-outlined v-if="state.collapsed" class="trigger" @click="toggleCollapsed" />
        <menu-fold-outlined v-else class="trigger" @click="toggleCollapsed" /> -->
            <menu-unfold-outlined v-if="!isMobile && state.collapsed" class="trigger"
              @click="state.collapsed = !state.collapsed" />
            <menu-fold-outlined v-if="!isMobile && !state.collapsed" class="trigger"
              @click="state.collapsed = !state.collapsed" />
            <menu-unfold-outlined v-if="isMobile && isOpenMobileSiderMenu" class="trigger"
              @click="onChangeMobileSiderMenu" />
            <menu-fold-outlined v-if="isMobile && !isOpenMobileSiderMenu" class="trigger"
              @click="onChangeMobileSiderMenu" />
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
          <a-layout>
            <a-layout-sider
              v-if="!isMobile && settingsConfig.currentNavigationMode.mode === 'left-mixed' && subSiderMenus.length"
              collapsible width="160" :theme="'light'">
              <div class="logo" />
              <jda-menu :menus="subSiderMenus" :collapsed="state.collapsed" :theme="'light'"
                @on-menu-item-click="(menu: any) => onMenuItemClick('sub-side-menu', menu)"></jda-menu>
            </a-layout-sider>
            <a-layout :style="{ 'overflow-y': 'auto' }">
              <!-- 多标签 -->
              <div style="width: 100%;padding: 33px 0;"
                v-if="settingsConfig.isMultipleTags && settingsConfig.isFixedMultipleTags && multipleTag.tabs?.length">
              </div>
              <a-tabs :class="{ 'mutiltab': true, 'mutiltab-fixed': settingsConfig.isFixedMultipleTags }"
                v-if="settingsConfig.isMultipleTags && multipleTag.tabs?.length"
                v-model:activeKey="multipleTag.activeKey" hide-add type="editable-card" @edit="onEdit"
                :style="{ left: isMobile ? 0 : (state.collapsed ? '48px' : leftMenuSiderExpandWidth), paddingRight: isMobile ? 0 : (settingsConfig.isFixedMultipleTags ? (state.collapsed ? '48px' : leftMenuSiderExpandWidth) : '0') }">
                <a-tab-pane v-for="tab in multipleTag.tabs" :key="tab.key" :closable="tab.closable">
                  <template #tab>
                    {{ tab.title }}
                    <!-- <ReloadOutlined class="mutiltab-tab-btn" /> -->
                  </template>
                </a-tab-pane>
                <template #rightExtra>
                  <a-dropdown :trigger="['click']" placement="bottomRight">
                    <a @click.prevent class="mutiltab-dropdown-menu-btn">
                      <MoreOutlined />
                    </a>
                    <template #overlay>
                      <a-menu style="width:120px;">
                        <a-menu-item v-for="(item, index) in tabMenuOptions" :key="index">
                          <span>{{ item }}</span>
                        </a-menu-item>
                      </a-menu>
                    </template>
                  </a-dropdown>
                </template>
              </a-tabs>
              <!-- 面包屑 -->
              <div class="jda-container">
                <a-breadcrumb>
                  <a-breadcrumb-item>Home</a-breadcrumb-item>
                  <a-breadcrumb-item><a href="">Application Center</a></a-breadcrumb-item>
                  <a-breadcrumb-item><a href="">Application List</a></a-breadcrumb-item>
                  <a-breadcrumb-item>An Application</a-breadcrumb-item>
                </a-breadcrumb>
              </div>
              <a-layout-content :style="{ margin: '24px' }">
                <router-view />
              </a-layout-content class="full-screen">
              <!-- <a-layout-footer style="text-align: center;z-index:10">
              Copyright ©2024 Created by JiuDao
            </a-layout-footer> -->
            </a-layout>
          </a-layout>
        </a-layout>
      </a-layout>
      <a-button type="primary" class="settings" :class="{ 'settings-open': isOpenSetting }" @click="onOpenSetting">
        <SettingOutlined />
      </a-button>
      <!-- <div class="settings" @click="onOpenSetting" :class="{ 'settings-open': isOpenSetting }">
      <SettingOutlined />
    </div> -->
      <a-drawer title="" :open="isOpenSetting" @close="onCloseSetting" width="300" :class="themeStyleClass">
        <div class="settings-content">
          <div class="settings-item">
            <h3>整体风格设置</h3>
            <div>
              <div class="settings-global-block">
                <div v-for="(theme, index) in themeStyleNames" :key="theme" @click="onChangeThemeStyle(theme)"
                  :class="`settings-global-item settings-global-item-${theme}`">
                  <div class="inner"></div>
                  <CheckOutlined v-if="theme === settingsConfig.currentThemeSkin"
                    :class="{ 'settings-global-item-select-icon': theme === settingsConfig.currentThemeSkin }" />
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
                  <CheckOutlined v-if="color.value === settingsConfig.currentThemeStyle.colorPrimary.value" />
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
                  <CheckOutlined v-if="mode.mode === settingsConfig.currentNavigationMode.mode"
                    :class="{ 'settings-global-item-select-icon': mode.mode === settingsConfig.currentNavigationMode.mode }" />
                </div>
              </div>
            </div>
          </div>
          <div class="settings-item">
            <div class="settings-form">
              <div class="settings-form-item">
                <span>固定 Header</span>
                <div>
                  <a-switch v-model:checked="settingsConfig.currentNavigationMode.isFixedHeader.value" size="small"
                    @change="onSwitch" :disabled="settingsConfig.currentNavigationMode.isFixedHeader.disabled" />
                </div>
              </div>
              <div class="settings-form-item">
                <span>固定侧边菜单</span>
                <div>
                  <a-switch v-model:checked="settingsConfig.currentNavigationMode.isFixedSideMenu.value" size="small"
                    :disabled="settingsConfig.currentNavigationMode.isFixedSideMenu.disabled" />
                </div>
              </div>
              <div class="settings-form-item">
                <span>自动分割菜单</span>
                <div>
                  <a-switch v-model:checked="settingsConfig.currentNavigationMode.isAutoSplitMenu.value" size="small"
                    :disabled="settingsConfig.currentNavigationMode.isAutoSplitMenu.disabled" />
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
                  <a-select size="small" v-model:value="settingsConfig.currentRouteAnimation" style="width: 100px"
                    :options="routeAnimations"></a-select>
                </div>
              </div>
              <div class="settings-form-item">
                <span>多标签</span>
                <div>
                  <a-switch v-model:checked="settingsConfig.isMultipleTags" size="small" />
                </div>
              </div>
              <div class="settings-form-item">
                <span>固定多标签</span>
                <div>
                  <a-switch v-model:checked="settingsConfig.isFixedMultipleTags" size="small" />
                </div>
              </div>
            </div>
          </div>
        </div>
      </a-drawer>

      <!-- 移动端左侧菜单栏 -->
      <a-drawer v-if="isMobile" title="" :open="isOpenMobileSiderMenu" :closable="false"
        @close="onChangeMobileSiderMenu" width="auto" placement="left" class="mobile-sider-menu-wrapper"
        :style="mobileSiderMenuStyle" :bodyStyle="{ padding: 0, 'overflow-x': 'hidden' }">
        <a-layout-sider style="padding:0;" trigger="null" collapsed-width="48" :width="leftMenuSiderExpandWidthNum"
          :theme="(settingsConfig.currentNavigationMode.mode === 'mixed' || settingsConfig.currentThemeSkin === 'light') ? 'light' : 'dark'">
          <div
            :class="{ 'sider-logo': true, 'sider-logo-dark': settingsConfig.currentNavigationMode.mode === 'mixed' }">
            <div>
              <img src="@/assets/logo.jpg" />
              <h1>JiuDao Admin</h1>
            </div>
          </div>
          <!-- <a-menu v-model:openKeys="state.openKeys" v-model:selectedKeys="state.selectedKeys" mode="inline" theme="dark"
        :items="items"></a-menu> -->
          <jda-menu :menus="siderMenus" :theme="(settingsConfig.currentThemeSkin === 'light') ? 'light' : 'dark'"
            @on-menu-item-click="(menu: any) => onMenuItemClick('side-menu', menu)"></jda-menu>
        </a-layout-sider>
      </a-drawer>
    </a-watermark>
  </a-config-provider>
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
    padding: 16px 11px;
  }
}


.header {
  box-shadow: 0 1px 4px rgba(0, 21, 41, .08);
  z-index: 10;

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
  z-index: 1001;
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
  // background: #1890ff;
  border-radius: 4px 0 0 4px;
  cursor: pointer;
  pointer-events: auto;
  //color: #fff;

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


/** 多标签 */
.mutiltab {
  margin: 0px;
  padding-top: 10px;
  width: 100%;
  border-radius: 8px 8px 0 0;
  background: #ffffff;

  :deep(.ant-tabs-nav) {
    padding-left: 16px;
    margin-bottom: 16px;

    .ant-tabs-tab {
      border-radius: 8px 8px 0 0;
    }
  }

  .mutiltab-dropdown-menu-btn {
    margin-right: 8px;
    padding: 12px;
    font-size: 16px;
    cursor: pointer;
  }

  .mutiltab-tab-btn {
    margin-right: 0;
    margin-left: 8px;
    color: rgba(0, 0, 0, 0.65);
    font-size: 12px;
  }

  :deep(.ant-tabs-tab-remove) {
    padding: 0;
    margin-top: 2px;
  }
}

.mutiltab-fixed {
  position: fixed;
  top: 48px;
  right: 0;
  z-index: 9;
  transition: left 0.2s ease;
  -ms-transition: left 0.2s ease;
  -webkit-transition: left 0.2s ease;
  -o-transition: left 0.2s ease;
  -moz-transition: left 0.2s ease;
}



/************ 主题色 start *************/
.theme-style-light {
  .ant-layout {

    .ant-layout-header {
      // color: rgba(229, 224, 216, 0.88);
      background-color: #ffffff;
    }
  }

  .header-menu-top-menu {
    .heaer-menu {
      color: rgba(0, 0, 0, 0.65);
    }
  }

  .sider-logo {
    background-color: #ffffff;

    h1 {
      color: rgba(0, 0, 0, 0.65);
    }
  }

  .header-logo {
    h1 {
      color: rgba(0, 0, 0, 1);
    }
  }
}

.theme-style-dark {
  .header-light {
    background: #ffffff;
  }
}


.theme-style-real-dark {
  color: rgba(229, 224, 216, 0.88);

  .dropdown-menu {
    :deep(.ant-dropdown-menu-item) {
      color: rgba(229, 224, 216, 0.88);
    }
  }

  :deep(.ant-layout) {
    background-image: initial;
    background-color: rgb(41, 42, 42);


    .ant-layout-footer {
      color: rgba(229, 224, 216, 0.88);
      background-image: initial;
      background-color: rgb(41, 42, 42);
    }

    .ant-layout-header {
      color: rgba(229, 224, 216, 0.88);
      background-image: initial;
      background: #242525;
      background-color: rgb(15, 28, 41);
    }
  }

  .header {
    color: rgba(229, 224, 216, 0.88);
    background: #242525;
    background-color: rgb(15, 28, 41);
    box-shadow: rgba(15, 28, 41, 0.08) 0px 1px 4px;

    .heaer-menu {
      color: rgba(229, 224, 216, 0.88);
    }
  }

  .header-dark {

    .ant-menu {
      background-image: initial;
      background-color: transparent;
    }

    .ant-menu {
      background: transparent;
    }
  }

  .header-light {
    background: #242525 !important;
  }

  .sider-logo-dark {
    color: rgba(255, 255, 255, 0.65);
    background: #001529;
    padding: 8px;
  }


  .mutiltab {
    background: #242525;
    color: rgba(229, 224, 216, 0.88);

    :deep(.ant-tabs-nav) {
      a {
        color: rgba(229, 224, 216, 0.88);
        list-style-image: initial;
      }
    }

    :deep(.ant-tabs-tab) {
      background-image: initial;
      background-color: rgba(13, 13, 13, 0.02);
      border-color: rgba(141, 131, 116, 0.06);
    }
  }

  :deep(.ant-drawer) {
    .ant-drawer-content {
      background-image: initial;
      background-color: rgb(36, 37, 37);
    }
  }

  .settings-item {
    h3 {
      color: #e5e0d8;
    }

    .settings-global-item-side-menu {
      &:after {
        background-color: rgb(36, 37, 37);
      }
    }

    .settings-global-item-mixed {
      &:before {
        background-color: rgb(36, 37, 37);
      }
    }

    .settings-global-item-left-mixed {
      &:before {
        background-color: #0f1c29;
      }

      .inner {
        background-color: rgb(36, 37, 37);
      }

      &:after {
        background-color: rgb(36, 37, 37);
      }
    }

    .settings-global-item {
      background-color: rgb(42, 44, 44);
      box-shadow: rgba(13, 13, 13, 0.18) 0px 1px 2.5px;
    }

    .settings-global-item-light {
      background-color: rgb(42, 44, 44);

      &:before {
        background-color: rgb(36, 37, 37);
      }

      &:after {
        background-color: rgb(36, 37, 37);
      }
    }

    .settings-global-item-dark {
      background-color: rgb(42, 44, 44);

      &:before {
        background-color: #0f1c29;
      }

      &:after {
        background-color: rgb(36, 37, 37);
      }
    }

    .settings-global-item-realDark {
      background-color: rgba(15, 28, 41, 0.65);

      &:before {
        background-color: rgba(15, 28, 41, 0.65);
      }

      &:after {
        background-color: rgba(15, 28, 41, 0.85);
      }
    }
  }

  .settings-form {
    .settings-form-item {
      color: rgba(229, 224, 216, 0.88);
    }
  }

  :deep(.ant-menu-light) {
    color: rgba(229, 224, 216, 0.88);
  }

  :deep(.ant-layout-sider-light) {
    background-color: #242525;
    box-shadow: rgba(35, 39, 42, 0.05) 2px 0px 8px;
    border-right-color: initial;

    .ant-menu-light {
      border-right-color: transparent;
      color: rgba(229, 224, 216, 0.88);
      background-image: initial;
      background-color: rgb(36, 37, 37);

      .ant-menu-sub.ant-menu-inline {
        background: transparent;
      }

      .ant-menu-item-selected {
        // background-color: rgb(42, 44, 44);
      }
    }
  }
}

/************ 主题色 end *************/
</style>
