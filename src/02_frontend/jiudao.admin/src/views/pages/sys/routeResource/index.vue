<script setup lang="ts">
import type { Key } from 'ant-design-vue/es/table/interface';
import { computed, createVNode, h, onMounted, reactive, ref, type Ref } from 'vue';
import request from '@/utils/http'
import { message } from 'ant-design-vue';
import { useSysDic } from '@/hooks'
import Edit from './edit.vue'
import EditAction from './editAction.vue'
import { Modal } from 'ant-design-vue'
import { ExclamationCircleOutlined } from '@ant-design/icons-vue';
import { getPageEntitiesApi, delRouteResourceApi, getRouteAndActionsApi } from '@/apis/sys/routeResource';
import type { PaginationChangeEvent } from '@/types/global';


const [messageApi, contextHolder] = message.useMessage();
const { dicItemName } = useSysDic();

const searchForm = ref({
  userName: null,
  account: null,
  mobile: null,
  email: null
});



const tableDataSource: Ref<any[]> = ref([]);
const onGetRouteAndActions = async () => {
  const res: any = await getRouteAndActionsApi({});
  tableDataSource.value = res || [];
}

onMounted(() => {
  onGetRouteAndActions();
})

const columns = [
  {
    title: '路由编码',
    dataIndex: 'code',
    key: 'code',
  },
  {
    title: '路由名称',
    dataIndex: 'name',
    key: 'name',
  },
  {
    title: '路由标题',
    dataIndex: 'title',
    key: 'title',
  },
  {
    title: '路由路径',
    dataIndex: 'url',
    key: 'url',
  },
  {
    title: '重定向路径',
    dataIndex: 'redirect',
    key: 'redirect',
  },
  {
    title: '组件路径',
    dataIndex: 'component',
    key: 'component',
  },
  {
    title: '菜单图标',
    dataIndex: 'icon',
    key: 'icon',
  },
  {
    title: '是否在菜单栏展示',
    dataIndex: 'showInMenu',
    key: 'showInMenu',
  },
  {
    title: '是否第三方路由',
    dataIndex: 'isThird',
    key: 'isThird',
  },
  {
    title: '功能',
    dataIndex: 'actions',
    key: 'actions',
  },
  {
    title: '操作',
    dataIndex: 'action',
    key: 'action',
    fixed: 'right',
  }
];


const rowSelection: Ref<any> = ref({
  selectedRowKeys: [],
  onChange: (selectedRowKeys: (string | number)[], selectedRows: any[]) => {
    // console.log('table onChange');
    // console.log(`selectedRowKeys: ${selectedRowKeys}`, 'selectedRows: ', selectedRows);
    rowSelection.value.selectedRowKeys = selectedRowKeys;
  },
  onSelect: (record: any, selected: boolean, selectedRows: any[]) => {
    // console.log('table onSelect');
    // console.log(record, selected, selectedRows);
  },
  onSelectAll: (selected: boolean, selectedRows: any[], changeRows: any[]) => {
    // console.log('table onSelectAll');
    // console.log(selected, selectedRows, changeRows);
  },
});

//导出
const onTableImportClick = (columns: any[]) => {
  console.log('onTableImportClick', columns)
}
//表格选中事件
let selectedRowKeys = ref<any[]>([]);
const onSelectChange = (selectedKeys: (string | number)[], selectedRows: any[]) => {
  selectedRowKeys.value = selectedRows.map(n => n.id);
};

//控制是否展开高级搜索
// let advanced = ref<boolean>(false)

//创建用户-打开创建用户弹窗
let openCreateModal = reactive({ isOpen: false, id: null })
// let isOpenCreateModal = ref<boolean>(false);
const onEdit = (row?: any) => {
  // isOpenCreateModal.value = true;
  openCreateModal.isOpen = true;
  openCreateModal.id = row?.id;
}

//配置按钮功能-打开弹窗
let openActionModal = reactive({ isOpen: false, id: null })
const onAction = (row?: any) => {
  openActionModal.isOpen = true;
  openActionModal.id = row?.id;
}
//删除用户
const onDelete = (row: any) => {
  Modal.confirm({
    title: '删除提示',
    icon: createVNode(ExclamationCircleOutlined),
    content: `确定要删除用户【${row.name}】吗？`,
    okText: '确认',
    cancelText: '取消',
    async onOk(e) {
      console.log(e)
      let res = await delRouteResourceApi(row.id);
      console.log(res)
    }
  });
}
</script>
<template>
  <div class="page-wrap page-wrap-master">
  <jda-table-search :model="searchForm" @search="onGetRouteAndActions">
    <template v-slot="{ advanced }">
      <a-col :md="12" :sm="24" :xs="24" :lg="8" :xl="6">
        <a-form-item label="用户名">
          <a-input v-model:value="searchForm.userName" placeholder="请输入用户名" />
        </a-form-item>
      </a-col>
      <a-col :md="12" :sm="24" :xs="24" :lg="8" :xl="6">
        <a-form-item label="账号">
          <a-input v-model:value="searchForm.account" placeholder="请输入账号" />
        </a-form-item>
      </a-col>
      <template v-if="advanced">
        <a-col :md="12" :sm="24" :xs="24" :lg="8" :xl="6">
          <a-form-item label="手机号码">
            <a-input v-model:value="searchForm.mobile" placeholder="请输入手机号码" />
          </a-form-item>
        </a-col>
        <a-col :md="12" :sm="24" :xs="24" :lg="8" :xl="6">
          <a-form-item label="邮箱">
            <a-input v-model:value="searchForm.email" placeholder="请输入邮箱" />
          </a-form-item>
        </a-col>
      </template>
    </template>
  </jda-table-search>
  <jda-card >
    <a-table :columns="columns" :data-source="tableDataSource" rowKey="id" :row-selection="rowSelection"
      childrenColumnName="childrens" bordered :scroll="{ x: true }" :pagination="false">
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'icon'">
          <font-awesome-icon v-if="record.icon" :icon="record.icon" />
        </template>
        <template v-else-if="column.key === 'showInMenu'">
            <a-tag v-if="record.showInMenu != true">不展示</a-tag>
            <a-tag color="green" v-else-if="record.showInMenu == true">展示</a-tag>
        </template>
        <template v-else-if="column.key === 'isThird'">
            <a-tag color="green" v-if="record.isThird != true">否</a-tag>
            <a-tag color="pink" v-else-if="record.isThird == true">是</a-tag>
        </template>
        <template v-else-if="column.key === 'actions'">
          <template v-for="action in record.actions">
            <a-tag color="pink">{{ action.name }}</a-tag>
          </template>
        </template>
        <template v-else-if="column.key === 'action'">
            <a-button type="link" @click="onEdit(record)">
              <span class="jda-table-action-btn-text">修改</span>
              <template #icon>
                <font-awesome-icon icon="fas fa-edit" />
              </template>
            </a-button>
            <a-button type="link" @click="onAction(record)">
              <span class="jda-table-action-btn-text">配置按钮功能</span>
              <template #icon>
                <font-awesome-icon icon="fas fa-edit" />
              </template>
            </a-button>
            <a-button danger type="link" @click="onDelete(record)">
              <span class="jda-table-action-btn-text">删除</span>
              <template #icon>
                <font-awesome-icon icon="fas fa-trash-alt" />
              </template>
            </a-button>
        </template>
      </template>
    </a-table>
  </jda-card>
  <jda-modal :width="800" v-model:open="openCreateModal.isOpen" title="新建" v-if="openCreateModal.isOpen">
    <Edit v-model:openCreateModal="openCreateModal.isOpen" :id="openCreateModal.id" @ok="onGetRouteAndActions"></Edit>
  </jda-modal>
  <jda-modal :width="800" v-model:open="openActionModal.isOpen" title="配置按钮功能" v-if="openActionModal.isOpen">
    <EditAction v-model:openActionModal="openActionModal.isOpen" :routeResourceId="openActionModal.id" @ok="onGetRouteAndActions"></EditAction>
  </jda-modal>
  </div>
</template>
<style lang="scss" scoped></style>