<script setup lang="ts">
import type { Key } from 'ant-design-vue/es/table/interface';
import { computed, createVNode, h, onMounted, reactive, ref } from 'vue';
import request from '@/utils/http'
import { message } from 'ant-design-vue';
import { useSysDic } from '@/hooks'
import Edit from './edit.vue'
import { Modal } from 'ant-design-vue'
import { ExclamationCircleOutlined } from '@ant-design/icons-vue';
import { getPageEntitiesApi, delActionResourceApi } from '@/apis/sys/actionResource';
import { getEntitiesApi, getRouteTreeApi } from '@/apis/sys/routeResource';
import type { PaginationChangeEvent } from '@/types/global';


const [messageApi, contextHolder] = message.useMessage();
const { dicItemName } = useSysDic();

const searchForm = ref({
  code: null,
  name: null,
  routeResourceId: null
});

// table配置&数据
const columns = reactive([
  {
    title: '编码',
    dataIndex: 'code',
    key: 'code',
  },
  {
    title: '名称',
    dataIndex: 'name',
    key: 'name',
  },
  {
    title: '所属菜单',
    key: 'routeResourceId',
    dataIndex: 'routeResourceId',
  },
  {
    title: '序号',
    dataIndex: 'sort',
    key: 'sort',
  },
  {
    title: '操作',
    key: 'action',
    fixed: 'right',
    width: 120
  },
]);


const menuTreeNodes = ref<any[]>([]);
//获取路由树
const onGetRouteTree = async () => {
  const res = await getRouteTreeApi({});
  menuTreeNodes.value = res as any;
  console.log(menuTreeNodes)
}

//表格数据源
let tableDataSource = ref<any[]>([]);
//获取表格数据源
const onGetTableDataSource = async (opts?: PaginationChangeEvent) => {
  console.log(opts)
  let pageIndex: number = opts?.page?.pageIndex || 1;
  let pageSize: number = opts?.page?.pageSize || 10;
  let searchParams = Object.assign({ pageIndex: pageIndex, pageSize: pageSize }, searchForm.value);
  var res = await getPageEntitiesApi(searchParams);
  tableDataSource.value = res?.items || [];
}
const onGetTableDataSource2 = async () => {
  try {
    await onGetTableDataSource();
  } catch (err) {
    console.log('ccccccccccccc', err)
  }
}
//导出
const onTableImportClick = (columns: any[]) => {
  console.log('onTableImportClick', columns)
}
//表格选中事件
let selectedRowKeys = ref<any[]>([]);
const onSelectChange = (selectedKeys: (string | number)[], selectedRows: any[]) => {
  selectedRowKeys.value = selectedRows.map(n => n.id);
};

const allRouteResources = ref<any[]>([])
const onGetAllRouteResources = async () => {
  const res = await getEntitiesApi({});
  allRouteResources.value = res;
}

onMounted(async () => {
  onGetTableDataSource2()
  onGetAllRouteResources()
  onGetRouteTree()
  // messageApi.error("网络暂时不可用，请检查下哦~11");
})
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
      let res = await delActionResourceApi(row.id);
      console.log(res)
    }
  });
}
</script>
<template>
  <context-holder />
  <jda-table-search :model="searchForm" @search="onGetTableDataSource" :advancedControl="false">

    <a-col :md="12" :sm="24" :xs="24" :lg="8" :xl="6">
      <a-form-item label="编码">
        <a-input v-model:value="searchForm.code" placeholder="请输入编码" />
      </a-form-item>
    </a-col>
    <a-col :md="12" :sm="24" :xs="24" :lg="8" :xl="6">
      <a-form-item label="名称">
        <a-input v-model:value="searchForm.name" placeholder="请输入名称" />
      </a-form-item>
    </a-col>
    <a-col :md="12" :sm="24" :xs="24" :lg="8" :xl="6">
      <a-form-item label="所属菜单">
        <jda-tree-select v-model:value="searchForm.routeResourceId" placeholder="请选择所属菜单" :tree-data="menuTreeNodes"
          tree-node-filter-prop="name">
        </jda-tree-select>
      </a-form-item>
    </a-col>

  </jda-table-search>
  <a-card class="j-card-table-wrapper">
    <jda-table class="ant-table-striped" rowKey="id" :columns="columns" :data-source="tableDataSource"
      :scroll="{ x: true }" bordered :total="100" :pagination="{
    showSizeChanger: true, pageSizeOptions: ['10', '20', '30', '50'],
    'show-total': (total: number) => `总共 ${total} 条数据`, buildOptionText: ({ value }: any) => `${value} 条/页`
  }" :rowClassName="(record: any, index: any) => (index % 2 === 1 ? 'table-striped' : null)"
      :row-selection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }"
      @get-table-data-source="onGetTableDataSource" @create="onEdit" @import="onTableImportClick">

      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'account'">
          <a>
            {{ record.account }}
          </a>
        </template>
        <template v-else-if="column.key === 'routeResourceId'">
          <span>
            {{ allRouteResources.find((n: any) => n.id === record.routeResourceId)?.name }}
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
  <jda-modal :width="800" v-model:open="openCreateModal.isOpen" title="新建" v-if="openCreateModal.isOpen">
    <Edit v-model:openCreateModal="openCreateModal.isOpen" :id="openCreateModal.id"></Edit>
  </jda-modal>
</template>
<style lang="scss" scoped></style>