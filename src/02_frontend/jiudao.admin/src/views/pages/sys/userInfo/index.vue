<script setup lang="ts">
import type { Key } from 'ant-design-vue/es/table/interface';
import { computed, createVNode, h, onMounted, reactive, ref } from 'vue';
import request from '@/utils/http'
import { message } from 'ant-design-vue';
import { useSysDic } from '@/hooks'
import Edit from './edit.vue'
import { Modal } from 'ant-design-vue'
import { ExclamationCircleOutlined } from '@ant-design/icons-vue';
import { getPageEntitiesApi, delUserApi, exportApi } from '@/apis/sys/userinfo';
import type { ImportDataFieldInputParams, PaginationChangeEvent } from '@/types/global';
import common from '@/utils/common';
import type { SysUserInfoGetPageEntitiesInputParams } from '@/types/sys/userinfo';


defineOptions({
  name: 'sysuserinfoindex'
})


const [messageApi, contextHolder] = message.useMessage();
const { dicItemName } = useSysDic();

const searchForm = ref<SysUserInfoGetPageEntitiesInputParams>({
  name: null,
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
    title: '姓名',
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
    width: 120
  },
]);

//表格数据源
let tableDataSource = ref<any[]>([]);
//获取表格数据源
const onGetTableDataSource = async (opts?: PaginationChangeEvent) => {
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
const onTableImportClick = async (columns: ImportDataFieldInputParams[], checkedColumns: ImportDataFieldInputParams[]) => {
  let searchParams = Object.assign({}, searchForm.value);
  const res = await exportApi(searchParams);
  const { data, headers } = res
  let filename = common.parseContentDisposition(headers);
  common.downloadFile(data, filename);
}
//表格选中事件
let selectedRowKeys = ref<any[]>([]);
const onSelectChange = (selectedKeys: (string | number)[], selectedRows: any[]) => {
  selectedRowKeys.value = selectedRows.map(n => n.id);
};

onMounted(async () => {
  onGetTableDataSource2()
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
      let res = await delUserApi(row.id);
      console.log(res)
    }
  });
}

</script>
<template>
  <div class="page-wrap page-wrap-master">
    <jda-table-search :model="searchForm" @search="onGetTableDataSource">
      <template v-slot="{ advanced }">
        <a-col :md="12" :sm="24" :xs="24" :lg="8" :xl="6">
          <a-form-item label="账号">
            <a-input v-model:value="searchForm.account" placeholder="请输入账号" />
          </a-form-item>
        </a-col>
        <a-col :md="12" :sm="24" :xs="24" :lg="8" :xl="6">
          <a-form-item label="姓名">
            <a-input v-model:value="searchForm.name" placeholder="请输入姓名" />
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
      <jda-table class="ant-table-striped" rowKey="id" :columns="columns" :data-source="tableDataSource"
        :scroll="{ x: true }" bordered :total="100" :pagination="{
      showSizeChanger: true, pageSizeOptions: ['10', '20', '30', '50'],
      'show-total': (total: number) => `总共 ${total} 条数据`, buildOptionText: ({ value }: any) => `${value} 条/页`
    }" :rowClassName="(record: any, index: any) => (index % 2 === 1 ? 'table-striped' : null)"
        :row-selection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }"
        @get-table-data-source="onGetTableDataSource" @create="onEdit" @export="onTableImportClick"
        :permission="{ create: 'sys.userinfo.index.add', export: 'sys.userinfo.index.export' }">

        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'account'">
            <a>
              {{ record.account }}
            </a>
          </template>
          <template v-else-if="column.key === 'gender'">
            <span>
              <a-tag :key="0" v-if="record.gender === 'Sex_Man'" color="green">
                {{ dicItemName('Sex', record.gender) }}
              </a-tag>
              <a-tag :key="1" v-if="record.gender === 'Sex_Woman'" color="geekblue">
                {{ dicItemName('Sex', record.gender) }}
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
    <jda-modal :width="800" v-model:open="openCreateModal.isOpen" title="新建" v-if="openCreateModal.isOpen">
      <Edit v-model:openCreateModal="openCreateModal.isOpen" :id="openCreateModal.id"></Edit>
    </jda-modal>
  </div>
</template>
<style lang="scss" scoped></style>