<script setup lang="ts">
import type { Key } from 'ant-design-vue/es/table/interface';
import { computed, createVNode, h, onMounted, reactive, ref } from 'vue';
import request from '@/utils/http'
import { message } from 'ant-design-vue';
import { useSysDic } from '@/hooks'
import Edit from './edit.vue'
import { Modal } from 'ant-design-vue'
import { ExclamationCircleOutlined } from '@ant-design/icons-vue';
import { getPageEntitiesApi,delUserApi } from '@/apis/sys/userinfo';
import type { PaginationChangeEvent } from '@/types/global';


const [messageApi, contextHolder] = message.useMessage();
const { dicItemName } = useSysDic();

const searchForm = ref({
  userName: null,
  account: null,
  mobile: null,
  email: null
});

// table配置&数据
const columns = reactive([
  {
    title: '账号',
    dataIndex: 'account',
    key: 'account',
  },
  {
    title: '名称',
    dataIndex: 'name',
    key: 'name',
  },
  {
    title: '手机号码',
    dataIndex: 'mobile',
    key: 'mobile',
  },
  {
    title: '性别',
    key: 'gender',
    dataIndex: 'gender',
  },
  {
    title: '邮箱',
    key: 'email',
    dataIndex: 'email',
  },
  {
    title: '操作',
    key: 'action',
    fixed: 'right',
    width: 60
  },
]);

const data: any[] = [];
for (let i = 0; i < 100; i++) {
  data.push({
    key: i,
    id: i,
    account: 'account' + i,
    name: 'John Brown' + i,
    mobile: 32,
    gender: i % 2 == 0 ? 'Sex_Man' : 'Sex_Woman',
    email: 'Email' + i,
  })
}
//表格选中事件
let selectedRowKeys = ref<any[]>([]);
const onSelectChange = (selectedKeys: (string | number)[], selectedRows: any[]) => {
  selectedRowKeys.value = selectedRows.map(n => n.Id);
  console.log('selected:', selectedKeys, selectedRows)
};
//获取表格数据源
const onGetTableDataSource = async (opts?: PaginationChangeEvent) => {
  console.log(opts)
  let pageIndex: number = opts?.page?.pageIndex || 1;
  let pageSize: number = opts?.page?.pageSize || 10;
  var res = await getPageEntitiesApi({ pageIndex: pageIndex, pageSize: pageSize });
}
//导出
const onTableImportClick = (columns: any[]) => {
  console.log('onTableImportClick', columns)
}

onMounted(() => {
  onGetTableDataSource();
  // messageApi.error("网络暂时不可用，请检查下哦~11");
})
//控制是否展开高级搜索
// let advanced = ref<boolean>(false)

//创建用户-打开创建用户弹窗
let openCreateModal = ref<boolean>(false);
const onEdit = (row?: any) => {
  openCreateModal.value = true;
}

//删除用户
const onDelete = (row: any) => {
  Modal.confirm({
    title: '删除提示',
    icon: createVNode(ExclamationCircleOutlined),
    content: `确定要删除用户【${row.name}】吗？`,
    okText: '确认',
    cancelText: '取消',
    async onOk(e){
      console.log(e)
     let res =  await delUserApi(row.id);
      console.log(res)
    }
  });
}
</script>
<template>
  <context-holder />
  <jda-table-search :model="searchForm" @search="onGetTableDataSource">
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
      @get-table-data-source="onGetTableDataSource" @create="onEdit" @import="onTableImportClick">

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
            <a-button type="link" @click="onEdit(record)">
              <span class="jda-table-action-btn-text">修改</span>
              <template #icon>
                <font-awesome-icon icon="fas fa-edit" />
              </template>
            </a-button>
            <a-divider type="vertical" />
            <a-button danger type="link" @click="onDelete(record)">
              <span class="jda-table-action-btn-text">删除</span>
              <template #icon>
                <font-awesome-icon icon="fas fa-trash-alt" />
              </template>
            </a-button>
          </span>
        </template>
      </template>
    </jda-table>
  </a-card>
  <jda-modal :width="800" v-model:open="openCreateModal" title="新建">
    <Edit v-model:openCreateModal="openCreateModal"></Edit>
  </jda-modal>
</template>
<style lang="scss" scoped></style>