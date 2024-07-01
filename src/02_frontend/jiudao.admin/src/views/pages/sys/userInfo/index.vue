<script setup lang="ts">
import type { Key } from 'ant-design-vue/es/table/interface';
import { computed, onMounted, reactive, ref } from 'vue';

const searchForm = ref({
  UserName: '',
  Account: '',
  Mobile: '',
  Email: ''
});

// table配置&数据
const columns = reactive([
  {
    title: '账号',
    dataIndex: 'Account',
    key: 'Account',
  },
  {
    title: '名称',
    dataIndex: 'Name',
    key: 'Name',
  },
  {
    title: '手机号码',
    dataIndex: 'Mobile',
    key: 'Mobile',
  },
  {
    title: '性别',
    key: 'Gender',
    dataIndex: 'Gender',
  },
  {
    title: '邮箱',
    key: 'Email',
    dataIndex: 'Email',
  },
  {
    title: 'Action',
    key: 'action',
    fixed: 'right',
  },
]);

const data: any[] = [];
for (let i = 0; i < 100; i++) {
  data.push({
    Id: i,
    Account: 'account' + i,
    Name: 'John Brown' + i,
    Mobile: 32,
    Gender: i % 2 == 0 ? 0 : 1,
    Email: 'Email' + i,
  })
}
let selectedRowKeys = ref<any[]>([]);
const onSelectChange = (selected: Key[]) => {
  selectedRowKeys.value = selected;
  console.log('selected:', selected)
};

let openCreateModal = ref<boolean>(false);
let createConfirmLoading = ref<boolean>(false);

const onGetTableDataSource = (opts) => {
  console.log(opts)
}
const onTableCreateClick = () => {
  openCreateModal.value = true;
  console.log('onTableCreateClick')
}
const onTableImportClick = () => {
  console.log('onTableImportClick')
}
const handleOk = () => {
  createConfirmLoading.value = true;
  setTimeout(() => {
    openCreateModal.value = false;
    createConfirmLoading.value = false;
  }, 1000);
};

//控制是否展开高级搜索
// let advanced = ref<boolean>(false)
</script>
<template>
  <jda-table-search :model="searchForm">
    <template v-slot="{advanced}">
      <a-col :md="12" :sm="24" :xs="24" :lg="8">
        <a-form-item label="用户名">
          <a-input v-model:value="searchForm.UserName" placeholder="请输入用户名" />
        </a-form-item>
      </a-col>
      <a-col :md="12" :sm="24" :xs="24" :lg="8">
        <a-form-item label="账号">
          <a-input v-model:value="searchForm.Account" placeholder="请输入账号" />
        </a-form-item>
      </a-col>
      <template v-if="advanced">
        <a-col :md="12" :sm="24" :xs="24" :lg="8">
          <a-form-item label="手机号码">
            <a-input v-model:value="searchForm.Mobile" placeholder="请输入手机号码" />
          </a-form-item>
        </a-col>
        <a-col :md="12" :sm="24" :xs="24" :lg="8">
          <a-form-item label="邮箱">
            <a-input v-model:value="searchForm.Email" placeholder="请输入邮箱" />
          </a-form-item>
        </a-col>
      </template>
    </template>
  </jda-table-search>
  <!-- <div class="jda-search-container">
    <a-form :model="searchForm" layout="horizontal" labelAlign="left" :label-col="{ style: { width: '70px' } }">
      <a-row :gutter="48">
        <a-col :md="12" :sm="24" :xs="24" :lg="8">
          <a-form-item label="用户名">
            <a-input v-model:value="searchForm.UserName" placeholder="请输入用户名" />
          </a-form-item>
        </a-col>
        <a-col :md="12" :sm="24" :xs="24" :lg="8">
          <a-form-item label="账号">
            <a-input v-model:value="searchForm.Account" placeholder="请输入账号" />
          </a-form-item>
        </a-col>
        <template v-if="advanced">
          <a-col :md="12" :sm="24" :xs="24" :lg="8">
            <a-form-item label="手机号码">
              <a-input v-model:value="searchForm.Mobile" placeholder="请输入手机号码" />
            </a-form-item>
          </a-col>
          <a-col :md="12" :sm="24" :xs="24" :lg="8">
            <a-form-item label="邮箱">
              <a-input v-model:value="searchForm.Email" placeholder="请输入邮箱" />
            </a-form-item>
          </a-col>
        </template>
        <a-col :md="12" :sm="24" :xs="24" :lg="8">
          <a-form-item>
            <a-button type="primary">查询</a-button>
            <a @click="advanced = !advanced" style="margin-left: 8px">
              {{ advanced ? '收起' : '展开' }}
              <font-awesome-icon v-if="advanced" icon="fas fa-angle-up" />
              <font-awesome-icon v-else icon="fas fa-angle-down" />
            </a>
          </a-form-item>
        </a-col>
      </a-row>
    </a-form>
  </div> -->
  <a-card class="j-card-table-wrapper">
    <jda-table class="ant-table-striped" :columns="columns" :data-source="data" :scroll="{ x: true }" bordered
      :total="100" :pagination="{
    showSizeChanger: true, pageSizeOptions: ['10', '20', '30', '50'],
    'show-total': (total: number) => `总共 ${total} 条数据`, buildOptionText: ({ value }: any) => `${value} 条/页`
  }" :rowClassName="(record: any, index: any) => (index % 2 === 1 ? 'table-striped' : null)"
      :row-selection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }"
      @get-table-data-source="onGetTableDataSource" @create="onTableCreateClick" @import="onTableImportClick">

      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'Account'">
          <a>
            {{ record.Account }}
          </a>
        </template>
        <template v-else-if="column.key === 'Gender'">
          <span>
            <a-tag :key="0" v-if="record.Gender === 0" color="green">
              女
            </a-tag>
            <a-tag :key="1" v-if="record.Gender === 1" color="geekblue">
              男
            </a-tag>
          </span>
        </template>
        <template v-else-if="column.key === 'action'">
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
      </template>
      <template #dd>
        <div>我是headerCell1</div>
      </template>
    </jda-table>
  </a-card>
  <a-modal :width="800" v-model:open="openCreateModal" title="新建" :confirm-loading="createConfirmLoading"
    @ok="handleOk">
    <a-card>
      <a-form layout="horizontal" labelAlign="left" :label-col="{ style: { width: '60px' } }">
        <a-row :gutter="48">
          <a-col :md="12" :sm="24" :xs="24" :lg="12">
            <a-form-item label="用户名">
              <a-input v-model:value="searchForm.UserName" placeholder="input placeholder" />
            </a-form-item>
          </a-col>
          <a-col :md="12" :sm="24" :xs="24" :lg="12">
            <a-form-item label="账号">
              <a-input v-model:value="searchForm.Account" placeholder="input placeholder" />
            </a-form-item>
          </a-col>
        </a-row>
      </a-form>
    </a-card>
  </a-modal>
</template>
<style lang="scss" scoped></style>