<template>
    <a-config-provider :theme="providerTheme">
      <a-watermark content="JiuDao Admin" style="height:100%;width:100%;">
            <router-view />
          </a-watermark>
    </a-config-provider>
</template>

<script setup lang="ts">
import { computed, reactive, ref, watch, h, createVNode, markRaw, toRaw, onMounted } from 'vue';
import { ConfigProvider, theme } from 'ant-design-vue';

defineOptions({
    name: 'blank-layout'
})

const onChangeThemeStyle = (item: 'light' | 'dark' | 'realDark'): void => {
    settingsConfig.value.currentThemeSkin = item;
    document.body.className = themeStyleClass.value;
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
      token: {colorText:'rgba(229, 224, 216, 0.88)',colorBgContainer:'rgb(36, 37, 37)', colorPrimary: settingsConfig.value.currentThemeStyle.colorPrimary.value, borderRadius: settingsConfig.value.currentThemeStyle.borderRadius },
      components: {Card:{colorBgContainer: 'rgb(36, 37, 37)'}}
    }
  }

  return {
    algorithm: theme.defaultAlgorithm,
    token: { colorPrimary: settingsConfig.value.currentThemeStyle.colorPrimary.value, borderRadius: settingsConfig.value.currentThemeStyle.borderRadius },
  }
});


const themeStyleClass = computed(() => {
  if (settingsConfig.value.currentThemeSkin === 'light')
    return 'theme-style-light';
  else if (settingsConfig.value.currentThemeSkin === 'dark')
    return 'theme-style-dark';
  else if (settingsConfig.value.currentThemeSkin === 'realDark')
    return 'theme-style-real-dark';

  return 'theme-style-light';
})

// onMounted(() => {
    let scstr = localStorage.getItem('settings-config');
    if (scstr) {
        let sc = JSON.parse(scstr);
        settingsConfig.value = sc;
        onChangeThemeStyle(settingsConfig.value.currentThemeSkin as any);
    }
// })
</script>

<style scoped></style>