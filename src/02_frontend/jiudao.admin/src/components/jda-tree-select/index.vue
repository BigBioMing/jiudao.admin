<script setup lang="ts">
import { watch } from 'vue';
import { reactive, ref } from 'vue'
defineOptions({
    name: 'jda-tree-select'
})

const props = withDefaults(defineProps<{
    treeLine?: boolean,
    treeData: any[]
}>(), {
    treeLine: true,
    treeData: <any>[]
});
const showLeafIcon = ref(false);
</script>
<template>
    <a-tree-select placeholder="Please select" :tree-line="props.treeLine && { showLeafIcon }"
        :tree-data="props.treeData" tree-node-filter-prop="name" v-bind="$attrs" :allowClear="true"
        :field-names="{
        children: 'childrens',
        label: 'name',
        value: 'id',
    }">
        <template v-for="(item, key, index) in $slots" :key="index" v-slot:[key]="scopeData">
            <slot :name="key" v-bind="scopeData || {}"></slot>
        </template>
    </a-tree-select>
</template>
<style lang="scss" scoped></style>