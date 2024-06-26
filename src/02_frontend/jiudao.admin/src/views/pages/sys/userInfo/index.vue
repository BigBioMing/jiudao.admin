<script setup lang="ts">
import { ref } from 'vue';

const searchForm = ref({
  UserName: '',
  Account: '',
});
</script>
<template>
  <div class="jda-search-container">
    <a-form :model="searchForm" layout="horizontal" labelAlign="left" :label-col="{ style: { width: '60px' } }">
      <a-row :gutter="48">
        <a-col :md="8" :sm="24" :xs="24" :lg="7">
          <a-form-item label="用户名">
            <a-input v-model:value="searchForm.UserName" placeholder="input placeholder" />
          </a-form-item>
        </a-col>
        <a-col :md="8" :sm="24" :xs="24" :lg="7">
          <a-form-item label="账号">
            <a-input v-model:value="searchForm.Account" placeholder="input placeholder" />
          </a-form-item>
        </a-col>
        <a-col :md="8" :sm="24" :xs="24" :lg="7">
          <a-form-item label="账号">
            <a-input v-model:value="searchForm.Account" placeholder="input placeholder" />
          </a-form-item>
        </a-col>
        <a-col :md="8" :sm="24" :xs="24" :lg="7">
          <a-form-item label="账号">
            <a-input v-model:value="searchForm.Account" placeholder="input placeholder" />
          </a-form-item>
        </a-col>
        <a-col :md="8" :sm="24" :xs="24" :lg="7">
          <a-form-item label="账号">
            <a-input v-model:value="searchForm.Account" placeholder="input placeholder" />
          </a-form-item>
        </a-col>
        <a-col :md="8" :sm="24" :xs="24" :lg="7">
          <a-form-item>
            <a-button type="primary">查询</a-button>
          </a-form-item>
        </a-col>
      </a-row>
    </a-form>
  </div>
  <a-card>
    <p>card content</p>
    <p>card content</p>
    <p>card content</p>z-index: 13;
    <p>card content</p>z-index: 13;
    <p>card content</p>z-index: 13;
    <p>card content</p>z-index: 13;
    <p>card content</p>z-index: 13;
    <p>card content</p>z-index: 13;
    <p>card content</p>z-index: 13;
    <p>card content</p>z-index: 13;
    <p>card content</p>z-index: 13;
    <p>card content</p>z-index: 13;
    <p>card content</p>z-index: 13;
  </a-card>
</template>
<style lang="scss" scoped></style>