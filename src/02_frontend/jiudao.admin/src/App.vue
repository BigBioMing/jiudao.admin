<script setup lang="ts">
import { getDicApi } from '@/apis/page-init';
import { useGlobalStore } from '@/stores'

const globalStore = useGlobalStore();
const onInit = async () => {
  const res = await getDicApi();
  const dics = res.data.data;
  globalStore.setDics(dics);
}


onInit();
</script>

<template>
  <router-view />
</template>

<style lang="scss" scoped></style>
