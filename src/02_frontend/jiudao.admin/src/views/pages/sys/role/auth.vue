<script setup lang="ts">
import { reactive, ref } from 'vue'
import { getRouteAndOptionsApi, empowerApi } from '@/apis/sys/role'
import { onMounted } from 'vue';
import type { Ref } from 'vue';
import { useRoute } from "vue-router";
import { onActivated } from 'vue';

defineOptions({
    name: 'role-auth'
})

// const props = defineProps({
//   id: Number
// })

// console.log('roid:',props.id);


//获取路由信息
const route = useRoute();

let actionIds: number[] = [];
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

const data: Ref<any[]> = ref([]);
const getRouteAndOptions = async () => {
    const roleId = route.query.id;
    const res: any = await getRouteAndOptionsApi({ roleId: roleId });
    data.value = res?.menuTreeNodes || [];
    rowSelection.value.selectedRowKeys = res?.selectMenuIds || [];
    actionIds = res?.selectActionIds || [];
}

const onEmpower = async () => {
    const roleId = route.query.id;
    await empowerApi({ roleId, routeResourceIds: rowSelection.value.selectedRowKeys, actionResourceIds: actionIds });
}

// onMounted(() => {
//     getRouteAndOptions();
// })
onActivated(() => {
    getRouteAndOptions();
})

const columns = [
    {
        title: '页面',
        dataIndex: 'name',
        key: 'name',
    },
    {
        title: '功能',
        dataIndex: 'actions',
        key: 'actions',
        width: 'actions',
    }
];


const onActionSelect = (evt: Event, action: any) => {
    if (evt.target.checked) {//如果选中，则向按钮id集合中添加该按钮权限
        const index = actionIds.indexOf(action.id);
        if (index <= -1) {
            actionIds.push(action.id);
        }
    } else {//如果取消选中，则从按钮id集合中移除该按钮权限
        const index = actionIds.indexOf(action.id);
        if (index > -1) {
            actionIds.splice(index, 1);
        }
    }

    // console.log('action:',actionIds)
}
</script>
<template>
    <div class="page-wrap page-wrap-slave">
        <jda-card>
            <a-button type="primary" @click="onEmpower()">保存</a-button>
            <a-table :columns="columns" :data-source="data" rowKey="id" :row-selection="rowSelection"
                childrenColumnName="childrens" bordered>
                <template #bodyCell="{ column, record }">
                    <template v-if="column.key === 'actions'">
                        <template v-for="action in record.actions">
                            <a-checkbox v-model:checked="action.isCheck" @change="onActionSelect($event, action)">{{
                action.name }}</a-checkbox>
                        </template>
                    </template>
                </template>
            </a-table>
        </jda-card>
    </div>
</template>
<style lang="scss" scoped></style>