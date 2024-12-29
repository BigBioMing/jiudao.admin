<script setup lang="ts">
import { computed, reactive, ref, watch, h, createVNode, markRaw, toRaw, onMounted } from 'vue';
import { ConfigProvider, theme } from 'ant-design-vue';
import { routeMenus } from '@/mock/menus';
import { useRouter, useRoute } from "vue-router";
import { useMenuStore,useGlobalStore } from "@/stores";
import router from "@/router";

defineOptions({
  name: 'basic-layout'
})

const menuStore = useMenuStore();
const globalStore = useGlobalStore();

import {
  MenuFoldOutlined,
  MenuUnfoldOutlined
} from '@ant-design/icons-vue';
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


//获取路由信息
const route = useRoute();
//面包屑数组
let crumbs = ref<{ name: string, path: string }[]>([]);
const routeMatched = route.matched || [];
for (let i = 0; i < routeMatched.length; i++) {
  let rmItem = routeMatched[i];
  crumbs.value.push({ name: rmItem.meta.name || '', path: rmItem.path });
}
console.log(crumbs)

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

const menus = menuStore.getMenus();
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
const onMenuItemClick = (pos: 'top-menu' | 'side-menu' | 'sub-side-menu', menu: any, isLeaf: boolean) => {
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
      // if (settingsConfig.value.currentNavigationMode.mode !== 'mixed')
      tabMenu = menu;
    }
    else if (pos === 'sub-side-menu')
      tabMenu = menu;

    if (tabMenu) {
      if (isLeaf) {
        addTab({ key: menu.key, title: menu.title });
      }
    }
  }

  if (isLeaf) {
    router.push({ path: menu.path });
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
  currentThemeStyle: { colorPrimary: { name: '拂晓蓝', value: 'rgb(22, 119, 255)' }, borderRadius: 6 },
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

  if (settingsConfig.value.currentNavigationMode.mode === 'left-mixed') {
    return 140;
  }
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

const tabLeft = computed(() => {
  if (!state.collapsed) {
    if (subSiderMenus.value.length) {
      return '300px';
    }
    else {
      if (settingsConfig.value.currentNavigationMode.mode === 'top-menu') {
        return 0;
      } else {
        return leftMenuSiderExpandWidth.value;
      }
    }
  } else {
    if (subSiderMenus.value.length) {
      return '210px';
    } else {
      return '48px';
    }
  }
})

const multipleTag = reactive<{
  activeKey: string,
  tabs: { title: string; key: string; closable?: boolean }[]
}>({
  activeKey: '',
  tabs: []
});
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
const removeTab = (targetKey: string) => {
  let lastIndex = 0;
  multipleTag.tabs.forEach((tab, i) => {
    if (tab.key === targetKey) {
      lastIndex = i - 1;
    }
  });
  multipleTag.tabs = multipleTag.tabs.filter(tab => tab.key !== targetKey);
  if (multipleTag.tabs.length && multipleTag.activeKey === targetKey) {
    if (lastIndex >= 0) {
      multipleTag.activeKey = multipleTag.tabs[lastIndex].key;
    } else {
      multipleTag.activeKey = multipleTag.tabs[0].key;
    }
  }
}
const onTabEdit = (targetKey: string | MouseEvent, action: string) => {
  if (action === 'add') {
  } else {
    removeTab(targetKey as string);
  }
};
// 将16进制颜色字符串转换为RGB对象
function hexToRgb(hex: any) {
  // 移除开头的#，如果有的话
  hex = hex.replace(/^#/, '');

  // 确保hex长度是6
  if (hex.length !== 6) {
    throw new Error('Invalid hex color code');
  }

  // 转换RGB
  let r = parseInt(hex.substr(0, 2), 16);
  let g = parseInt(hex.substr(2, 2), 16);
  let b = parseInt(hex.substr(4, 2), 16);

  return { r, g, b };
}

// RGB到HSL的转换函数
function rgbToHsl(r: any, g: any, b: any) {
  r /= 255;
  g /= 255;
  b /= 255;

  let max = Math.max(r, g, b), min = Math.min(r, g, b);
  let h, s, l = (max + min) / 2;

  if (max == min) {
    h = s = 0; // achromatic
  } else {
    let d = max - min;
    s = l > 0.5 ? d / (2 - max - min) : d / (max + min);

    switch (max) {
      case r: h = (g - b) / d + (g < b ? 6 : 0); break;
      case g: h = (b - r) / d + 2; break;
      case b: h = (r - g) / d + 4; break;
    }

    h /= 6;
  }

  return { h: h * 360, s: s * 100, l: l * 100 };
}

function rgbaToNumbers(rgbaString: string) {
  const match = rgbaString.match(/^rgba\((\d+), (\d+), (\d+), (\d+(?:\.\d+)?)\)$/);
  if (!match) {
    throw new Error('Invalid RGBA string');
  }
  return {
    r: parseInt(match[1], 10), // Red
    g: parseInt(match[2], 10), // Green
    b: parseInt(match[3], 10), // Blue
    a: parseFloat(match[4])    // Alpha
  }
}

function rgbToNumbers(rgbString: string) {
  const match = rgbString.match(/^rgb\((\d+),\s*(\d+),\s*(\d+)\)$/);
  if (!match) return null;
  return {
    r: parseInt(match[1], 10), // Red
    g: parseInt(match[2], 10), // Green
    b: parseInt(match[3], 10) // Blue
  }
}

// 辅助函数：HSL转RGB
function hslToRgbString({ h, s, l }: any) {
  console.log('h s l')
  console.log(h, s, l)
  let r, g, b;

  if (s === 0) {
    r = g = b = l; // 灰色
  } else {
    function hue2rgb(p: any, q: any, t: any) {
      if (t < 0) t += 1;
      if (t > 1) t -= 1;
      if (t < 1 / 6) return p + (q - p) * 6 * t;
      if (t < 1 / 2) return q;
      if (t < 2 / 3) return p + (q - p) * (2 / 3 - t) * 6;
      return p;
    }

    let q = l < 0.5 ? l * (1 + s) : l + s - l * s;
    let p = 2 * l - q;
    r = hue2rgb(p, q, h + 1 / 3);
    g = hue2rgb(p, q, h);
    b = hue2rgb(p, q, h - 1 / 3);
  }
  console.log(`rgb(${Math.round(r * 255)}, ${Math.round(g * 255)}, ${Math.round(b * 255)})`)
  return `rgb(${Math.round(r * 255)}, ${Math.round(g * 255)}, ${Math.round(b * 255)})`;
}
function hslToHex(h: any, s: any, l: any) {
  // 将HSL转换为RGB
  let r, g, b;
  function hue2Rgb(p: any, q: any, t: any) {
    if (t < 0) t += 1;
    if (t > 1) t -= 1;
    if (t < 1 / 6) return p + (q - p) * 6 * t;
    if (t < 1 / 2) return q;
    if (t < 2 / 3) return p + (q - p) * (2 / 3 - t) * 6;
    return p;
  }

  let q = l < 0.5 ? l * (1 + s) : l + s - l * s;
  let p = 2 * l - q;
  r = hue2Rgb(p, q, h + 1 / 3);
  g = hue2Rgb(p, q, h);
  b = hue2Rgb(p, q, h - 1 / 3);

  // 将RGB转换为HEX
  function rgbToHex(c: any) {
    return ('0' + Math.round(c * 255).toString(16)).slice(-2);
  }

  const hexR = rgbToHex(r);
  const hexG = rgbToHex(g);
  const hexB = rgbToHex(b);

  return `#${hexR}${hexG}${hexB}`;
}



// 获取hover和active状态的相近颜色
function getHoverAndActiveColors(color: any) {
  // 转换颜色到RGB
  let rgb: any = null;
  if (color.length <= 7) {
    rgb = hexToRgb(color)
  } else if (color.indexOf('rgba') > -1) {
    rgb = rgbaToNumbers(color)
  } else if (color.indexOf('rgb') > -1) {
    rgb = rgbToNumbers(color)
  } else {
    console.error('color参数错误：', color)
  }

  // 转换RGB到HSL
  let hsl = rgbToHsl(rgb.r, rgb.g, rgb.b);

  // 减少饱和度以生成hover颜色
  let hoverHsl = `hsl(${hsl.h.toFixed(0)}, ${Math.max(hsl.s - 20, 0)}%, ${hsl.l + 10}%)`;

  // 减少亮度以生成active颜色
  let activeHsl = `hsl(${hsl.h.toFixed(0)}, ${hsl.s}%, ${Math.max(hsl.l - 10, 0)}%)`;
  console.log({
    hover: hoverHsl,
    active: activeHsl
  });
  console.log('hslToHex:', hslToHex(hsl.h, hsl.s, hsl.l))
  return {
    // hover: hslToHex(hsl.h,Math.max(hsl.s - 20, 0),hsl.l + 10),
    // active: hslToHex(hsl.h,hsl.s,Math.max(hsl.l - 10, 0))
    // hover: hslToRgbString({h:hsl.h.toFixed(0),s:Math.max(hsl.s - 20, 0),l:hsl.l + 10}),
    // active: hslToRgbString({h:hsl.h.toFixed(0),s:hsl.s,l:Math.max(hsl.l - 10, 0)})
    hover: hoverHsl,
    active: activeHsl
  };
}

// // 使用示例
// let hexColor = '#ff8800'; // 你的基础颜色
// let { hover, active } = getHoverAndActiveColors(hexColor);

// console.log(hover); // 相近的hover颜色
// console.log(active); // 相近的active颜色
const { darkAlgorithm, compactAlgorithm } = theme;
const providerTheme = computed(() => {
  var primary = settingsConfig.value.currentThemeStyle.colorPrimary.value;
  console.log('primary:', primary)
  let cl = getHoverAndActiveColors(primary);
  console.log('cl:', cl)
  let pt: any = {
    algorithm: theme.defaultAlgorithm,
    token: {
      colorPrimary: settingsConfig.value.currentThemeStyle.colorPrimary.value,
      borderRadius: settingsConfig.value.currentThemeStyle.borderRadius,
      colorLink: settingsConfig.value.currentThemeStyle.colorPrimary.value,
      colorLinkActive: cl.active,
      colorLinkHover: cl.hover,
      colorIconHover: cl.hover,
    }
  };

  if (settingsConfig.value.currentThemeSkin === 'light') {
  }
  else if (settingsConfig.value.currentThemeSkin === 'dark') {
  }
  else if (settingsConfig.value.currentThemeSkin === 'realDark') {
    pt.algorithm = theme.darkAlgorithm;
    pt.colorText = 'rgba(229, 224, 216, 0.88)';
    pt.colorBgContainer = 'rgb(36, 37, 37)';
    pt.components = { Card: { colorBgContainer: 'rgb(36, 37, 37)' } };
  }

  return pt;
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

const onLogout=()=>{
  globalStore.clearToken();
  menuStore.reset();
  router.push('/login')
}

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
            @on-menu-item-click="(menu: any, isLeaf: boolean) => onMenuItemClick('side-menu', menu, isLeaf)"></jda-menu>
        </a-layout-sider>
        <a-layout :style="layoutFixedLeftMenuRightRegionStyle">
          <a-layout-header v-if="settingsConfig.currentNavigationMode.isFixedHeader.value" class="header"
            :style="{ padding: 0, lineHeight: '48px', height: '48px', width: '100%' }">
          </a-layout-header>
          <a-layout-header :class="{
    'header': true, 'header-dark': true, 'layout-fixed-header-menu': settingsConfig.currentNavigationMode.isFixedHeader.value
  }" v-if="!isMobile && (settingsConfig.currentNavigationMode.mode === 'top-menu' || settingsConfig.currentNavigationMode.mode === 'mixed')"
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
                  @on-menu-item-click="(menu: any, isLeaf: boolean) => onMenuItemClick('top-menu', menu, isLeaf)"
                  mode="horizontal" :theme="settingsConfig.currentThemeSkin === 'light' ? 'light' : 'dark'"></jda-menu>
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
                        <a-menu-item key="3" @click="onLogout">
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
  }" :style="{ padding: 0, lineHeight: '48px', height: '48px', left: isMobile ? 0 : (state.collapsed ? '48px' : leftMenuSiderExpandWidth) }"
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
                    <a-menu-item key="3" @click="onLogout">
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
                @on-menu-item-click="(menu: any, isLeaf: boolean) => onMenuItemClick('sub-side-menu', menu, isLeaf)"></jda-menu>
            </a-layout-sider>
            <a-layout :style="{ 'overflow-y': 'auto' }">
              <!-- 多标签 -->
              <div style="width: 100%;padding: 33px 0;"
                v-if="settingsConfig.isMultipleTags && settingsConfig.isFixedMultipleTags && multipleTag.tabs?.length">
              </div>
              <a-tabs :class="{ 'mutiltab': true, 'mutiltab-fixed': settingsConfig.isFixedMultipleTags }"
                v-if="settingsConfig.isMultipleTags && multipleTag.tabs?.length"
                v-model:activeKey="multipleTag.activeKey" hide-add type="editable-card" @edit="onTabEdit"
                :style="{ left: isMobile ? 0 : tabLeft, paddingRight: isMobile ? 0 : (settingsConfig.isFixedMultipleTags ? (state.collapsed ? '48px' : tabLeft) : '0') }">
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
                  <template v-for="(crumb, index) in crumbs">
                    <a-breadcrumb-item v-if="!crumb.path">{{ crumb.name }}</a-breadcrumb-item>
                    <a-breadcrumb-item v-else><a :href="crumb.path">{{ crumb.name }}</a></a-breadcrumb-item>
                  </template>
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
            @on-menu-item-click="(menu: any, isLeaf: boolean) => onMenuItemClick('side-menu', menu, isLeaf)"></jda-menu>
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
  // color: #1890ff;
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
