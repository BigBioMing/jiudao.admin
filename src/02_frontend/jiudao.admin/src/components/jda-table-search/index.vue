<script setup lang="ts">
import { reactive, ref } from 'vue'
defineOptions({
    name: 'jda-table-search'
})

//'search'-点击查询按钮
const emits = defineEmits(['search'])

let props = withDefaults(defineProps<{
    resetControl?: boolean,//控制重置按钮是否显示
    advancedControl?: boolean,//控制高级搜索按钮是否显示
    model: any,
    formProps?: any,//a-form组件的属性
    rowProps?: any,//a-rowProps组件的属性
}>(), {
    resetControl: true,
    advancedControl: true,
    formProps: { layout: "horizontal", labelAlign: 'left', labelCol: { style: { width: '70px' } } },
    rowProps: { gutter: 48 }
});

// 重置搜索条件
const originalModel = JSON.parse(JSON.stringify(props.model));
const onReset = () => {
    const om = JSON.parse(JSON.stringify(originalModel));
    Object.assign(props.model, om);
}

// 查询
const onSearch = () => {
    emits('search')
}

// 控制是否展开高级搜索
let advanced = ref<boolean>(false)
const onAdvancedToggle = () => {
    advanced.value = !advanced.value;
}

// // 将 advanced 暴露出去，让父组件可以做额外的控制
// defineExpose({
//     advanced,
//     onAdvancedToggle
// })
</script>
<template>
    <div class="jda-search-container">
        <a-form :model="props.model" v-bind="props.formProps">
            <a-row v-bind="props.rowProps">
                <slot :advanced="advanced"></slot>
                <slot name="action">
                    <a-col :sm="24" :xs="24" :md="!advanced && 12 || 24" :lg="!advanced && 8 || 24" :xl="!advanced && 6 || 24">
                        <a-form-item>
                            <span :style="advanced && { float: 'right', overflow: 'hidden' } || {} ">
                            <a-button type="primary" @click="onSearch">查询</a-button>
                            <a-button style="margin-left:8px;" @click="onReset" v-if="props.resetControl">重置</a-button>
                            <a @click="onAdvancedToggle" style="margin-left: 8px" v-if="props.advancedControl">
                                {{ advanced ? '收起' : '展开' }}
                                <font-awesome-icon v-if="advanced" icon="fas fa-angle-up" />
                                <font-awesome-icon v-else icon="fas fa-angle-down" />
                            </a></span>
                        </a-form-item>
                    </a-col>
                </slot>
            </a-row>
        </a-form>
    </div>
</template>
<style lang="scss" scoped></style>