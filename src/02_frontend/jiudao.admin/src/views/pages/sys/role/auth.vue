<script setup lang="ts">
import { reactive, ref } from 'vue'
defineOptions({
    name: 'role-auth'
})

const columns = [
    {
        title: '页面',
        dataIndex: 'route',
        key: 'route',
    },
    {
        title: '操作',
        dataIndex: 'actions',
        key: 'actions',
        width: 'actions',
    }
];

const data: any[] = [];

const rowSelection = ref({
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
</script>
<template>
    <jda-card>
        <a-table :columns="columns" :data-source="data" :row-selection="rowSelection" bordered />
    </jda-card>
</template>
<style lang="scss" scoped></style>