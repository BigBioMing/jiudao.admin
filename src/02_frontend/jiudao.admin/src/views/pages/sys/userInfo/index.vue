<script setup lang="ts">
import type { Key } from 'ant-design-vue/es/table/interface';
import { ref } from 'vue';

const searchForm = ref({
  UserName: '',
  Account: '',
});

// table配置&数据
const columns = [
  {
    dataIndex: 'name',
    key: 'name',
    slots: { title: 'customTitle', customRender: 'name' },
  },
  {
    title: 'Age',
    dataIndex: 'age',
    key: 'age',
  },
  {
    title: 'Address',
    dataIndex: 'address',
    key: 'address',
  },
  {
    title: 'Tags',
    key: 'tags',
    dataIndex: 'tags',
    slots: { customRender: 'tags' },
  },
  {
    title: 'Action',
    key: 'action',
    fixed: 'right',
    slots: { customRender: 'action' },
  },
];

const data = [
  {
    key: '1',
    name: 'John Brown',
    age: 32,
    address: 'New York No. 1 Lake Park',
    tags: ['nice', 'developer'],
  },
  {
    key: '2',
    name: 'Jim Green',
    age: 42,
    address: 'London No. 1 Lake Park',
    tags: ['loser'],
  },
  {
    key: '3',
    name: 'Joe Black',
    age: 32,
    address: 'Sidney No. 1 Lake Park',
    tags: ['cool', 'teacher'],
  },
  {
    key: '4',
    name: 'Joe Black1',
    age: 32,
    address: 'Sidney No1. 1 Lake Park',
    tags: ['cool', 'teacher'],
  },
];
let selectedRowKeys = ref<any[]>([]);
const onSelectChange = (selected: Key[]) => {
  selectedRowKeys.value = selected;
  console.log(selected)
};
</script>
<template>
  <div class="jda-search-container">
    <a-form :model="searchForm" layout="horizontal" labelAlign="left" :label-col="{ style: { width: '60px' } }">
      <a-row :gutter="48">
        <a-col :md="12" :sm="24" :xs="24" :lg="7">
          <a-form-item label="用户名">
            <a-input v-model:value="searchForm.UserName" placeholder="input placeholder" />
          </a-form-item>
        </a-col>
        <a-col :md="12" :sm="24" :xs="24" :lg="7">
          <a-form-item label="账号">
            <a-input v-model:value="searchForm.Account" placeholder="input placeholder" />
          </a-form-item>
        </a-col>
        <a-col :md="12" :sm="24" :xs="24" :lg="7">
          <a-form-item>
            <a-button type="primary">查询</a-button>
          </a-form-item>
        </a-col>
      </a-row>
    </a-form>
  </div>
  <a-card>
    <a-table class="ant-table-striped" :columns="columns" :data-source="data" :scroll="{ x: true }" bordered :total="20"
      :pagination="{ showSizeChanger: true, pageSizeOptions: ['10', '20', '30', '50'], 'show-total': (total: number) => `总共 ${total} 条数据`,buildOptionText:({value}:any) =>`${value} 条/页` }"
      :rowClassName="(record: any, index: any) => (index % 2 === 1 ? 'table-striped' : null)"
      :row-selection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }">
      <template #name="{ text }">
        <a>{{ text }}</a>
      </template>
      <template #customTitle>
        <span>
          <smile-outlined />
          Name
        </span>
      </template>
      <template #tags="{ text: tags }">
        <span>
          <a-tag v-for="tag in tags" :key="tag"
            :color="tag === 'loser' ? 'volcano' : tag.length > 5 ? 'geekblue' : 'green'">
            {{ tag.toUpperCase() }}
          </a-tag>
        </span>
      </template>
      <template #action="{ record }">
        <span>
          <a>Invite 一 {{ record.name }}</a>
          <a-divider type="vertical" />
          <a>Delete</a>
          <a-divider type="vertical" />
          <a class="ant-dropdown-link">
            More actions
            <down-outlined />
          </a>
        </span>
      </template>
    </a-table>
  </a-card>
</template>
<style lang="scss" scoped></style>