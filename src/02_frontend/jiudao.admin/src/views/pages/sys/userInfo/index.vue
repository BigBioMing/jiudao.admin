<script setup lang="ts">
import type { Key } from 'ant-design-vue/es/table/interface';
import { computed, onMounted, reactive, ref } from 'vue';
import request from '@/utils/http'
import { message } from 'ant-design-vue';
import { useGlobalStore } from '@/stores'
import { getDicItem } from '@/utils/sysdicitem'
import { useSysDic } from '@/hooks'

const [messageApi, contextHolder] = message.useMessage();
const globalStore = useGlobalStore();
const { dicItemName } = useSysDic();

const dics: any = globalStore.getDics();
console.log(dics)
const onTest = async () => {
  await request({
    'url': '/api/Sys/SysDictionaryDefine/GetDictionaryTree',
    method: 'get',
    data: { aaa: 1, bbb: '33' }
  })
}

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
    Gender: i % 2 == 0 ? 'Sex_Man' : 'Sex_Woman',
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

onMounted(() => {

  messageApi.error("网络暂时不可用，请检查下哦~11");
})

let model = reactive({
  Account: null,
  Password: null,
  UserName: null,
  Mobile: null,
  Email: null,
  Gender: null
})
//控制是否展开高级搜索
// let advanced = ref<boolean>(false)
</script>
<template>
  <context-holder />
  <jda-table-search :model="searchForm" @search="onTest">
    <template v-slot="{ advanced }">
      <a-col :md="12" :sm="24" :xs="24" :lg="8" :xl="6">
        <a-form-item label="用户名">
          <a-input v-model:value="searchForm.UserName" placeholder="请输入用户名" />
        </a-form-item>
      </a-col>
      <a-col :md="12" :sm="24" :xs="24" :lg="8" :xl="6">
        <a-form-item label="账号">
          <a-input v-model:value="searchForm.Account" placeholder="请输入账号" />
        </a-form-item>
      </a-col>
      <template v-if="advanced">
        <a-col :md="12" :sm="24" :xs="24" :lg="8" :xl="6">
          <a-form-item label="手机号码">
            <a-input v-model:value="searchForm.Mobile" placeholder="请输入手机号码" />
          </a-form-item>
        </a-col>
        <a-col :md="12" :sm="24" :xs="24" :lg="8" :xl="6">
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
            <a-tag :key="0" v-if="record.Gender === 'Sex_Man'" color="green">
              {{ dicItemName('Sex', record.Gender) }}
            </a-tag>
            <a-tag :key="1" v-if="record.Gender === 'Sex_Woman'" color="geekblue">
              {{ dicItemName('Sex', record.Gender) }}
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
      <a-form :model="model" layout="horizontal" labelAlign="left" :label-col="{ style: { width: '60px' } }">
        <a-row :gutter="48">
          <a-col :md="12" :sm="24" :xs="24" :lg="12">
            <a-form-item label="账号">
              <a-input v-model:value="model.Account" placeholder="input placeholder" />
            </a-form-item>
          </a-col>
          <a-col :md="12" :sm="24" :xs="24" :lg="12">
            <a-form-item label="密码">
              <a-input v-model:value="model.Password" placeholder="input placeholder" />
            </a-form-item>
          </a-col>
          <a-col :md="12" :sm="24" :xs="24" :lg="12">
            <a-form-item label="名称">
              <a-input v-model:value="model.UserName" placeholder="input placeholder" />
            </a-form-item>
          </a-col>
          <a-col :md="12" :sm="24" :xs="24" :lg="12">
            <a-form-item label="手机号">
              <a-input v-model:value="model.Mobile" placeholder="input placeholder" />
            </a-form-item>
          </a-col>
          <a-col :md="12" :sm="24" :xs="24" :lg="12">
            <a-form-item label="性别">
              <a-input v-model:value="model.Gender" placeholder="input placeholder" />
            </a-form-item>
          </a-col>
          <a-col :md="12" :sm="24" :xs="24" :lg="12">
            <a-form-item label="邮箱">
              <a-input v-model:value="model.Email" placeholder="input placeholder" />
            </a-form-item>
          </a-col>
        </a-row>
      </a-form>
    </a-card>
  </a-modal>
</template>
<style lang="scss" scoped></style>