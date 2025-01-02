<script setup lang="ts">
import { reactive, ref } from 'vue'
import { getRouteAndOptionsApi } from '@/apis/sys/role'
import { onMounted } from 'vue';

defineOptions({
    name: 'role-auth'
})


const rowSelection = ref({
    selectedRowKeys: [],
    onChange: (selectedRowKeys: (string | number)[], selectedRows: any[]) => {
        console.log(`selectedRowKeys: ${selectedRowKeys}`, 'selectedRows: ', selectedRows);
    },
    onSelect: (record: any, selected: boolean, selectedRows: any[]) => {
        console.log(record, selected, selectedRows);
    },
    onSelectAll: (selected: boolean, selectedRows: any[], changeRows: any[]) => {
        console.log(selected, selectedRows, changeRows);
    },
});

const data: any[] = ref([]);
const getRouteAndOptions = async () => {
    const res = await getRouteAndOptionsApi({ roleId: 1 });
    data.value = res?.menuTreeNodes || [];
    rowSelection.value.selectedRowKeys = res?.selectMenuIds || [];
}

onMounted(() => {

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
</script>
<template>
    <jda-card>
        <a-table :columns="columns" :data-source="data" rowKey="id" :row-selection="rowSelection"
            childrenColumnName="childrens" bordered>
            <template #bodyCell="{ column, record }">
                <template v-if="column.key === 'actions'">
                    <template v-for="action in record.actions">
                        <a-checkbox v-model:checked="action.isCheck">{{ action.name }}</a-checkbox>
                    </template>
                </template>
            </template>
        </a-table>
    </jda-card>
</template>
<style lang="scss" scoped></style>