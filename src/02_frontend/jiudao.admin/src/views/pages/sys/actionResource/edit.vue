<script setup lang="ts">
import type { Rule } from 'ant-design-vue/es/form';
import { reactive, ref, toRaw, watch, type UnwrapRef } from 'vue'
import { getEntityApi, saveActionResourceApi } from '@/apis/sys/actionResource'
import { getRouteTreeApi } from '@/apis/sys/routeResource'
import { useSysDic } from '@/hooks'
import { onMounted } from 'vue';
import type { TreeSelectProps } from 'ant-design-vue';
defineOptions({
    name: 'actionResource-edit'
})

const props = withDefaults(defineProps<{
    openCreateModal?: boolean,
    id: number | null
}>(), {
    openCreateModal: false,
    id: null
})

const emits = defineEmits(['update:openCreateModal'])

const { getDicItems } = useSysDic();
//性别字典
const sexDicItems = getDicItems('Sex');

// let openCreateModal = ref<boolean>(props.openCreateModal);


// watch(
//     () => props.openCreateModal,
//     (newVal, oloVal) => {
//         console.log(newVal);
//         openCreateModal.value = newVal;
//     },
//     { deep: true, immediate: true }
// )


const formRef = ref();
//表单对象-用户信息
let model = reactive({
    code: null,
    name: null,
    sort: 0,
    routeResourceId: null
})


// 表单验证规则
const rules: Record<string, Rule[]> = {
    code: [
        { required: true, message: '请输入账号名称', trigger: 'change' },
    ],
    name: [
        { required: true, message: '请输入姓名', trigger: 'change' }
    ]
};

//获取当前操作资源信息
const onGetCurrentModel = async () => {
    if (props.id && props.id > 0) {
        //根据id获取用户信息
        const res = await getEntityApi(props.id!);
        Object.assign(model, res);
    }
}
const menuTreeNodes = ref<any[]>([]);
//获取路由树
const onGetRouteTree = async () => {
    const res = await getRouteTreeApi({});
    menuTreeNodes.value = res as any;
    console.log(menuTreeNodes)
}

//获取当前操作资源信息
onGetCurrentModel();
onGetRouteTree();

let createConfirmLoading = ref<boolean>(false);
const handleOk = () => {
    formRef.value.validate()
        .then(async () => {
            createConfirmLoading.value = true;
            let sendData = toRaw(model);
            await saveActionResourceApi(sendData);
            createConfirmLoading.value = false;
            closeModal();
        })
        .catch((error: any) => {
            console.log('error', error);
        });
};
const closeModal = () => {
    createConfirmLoading.value = false;
    emits('update:openCreateModal', false);
}
const treeSelectValue = ref<string>('parent 1');
const treeData = ref<TreeSelectProps['treeData']>([
    {
        label: 'root 1',
        value: 'root 1',
        children: [
            {
                label: 'parent 1',
                value: 'parent 1',
                children: [
                    {
                        label: 'parent 1-0',
                        value: 'parent 1-0',
                        children: [
                            {
                                label: 'my leaf',
                                value: 'leaf1',
                            },
                            {
                                label: 'your leaf',
                                value: 'leaf2',
                            },
                        ],
                    },
                    {
                        label: 'parent 1-1',
                        value: 'parent 1-1',
                    },
                ],
            },
            {
                label: 'parent 2',
                value: 'parent 2',
            },
        ],
    },
]);

watch(() => treeSelectValue, (newVal, oldVal) => {
    console.log('oldVal', oldVal)
    console.log('newVal', newVal)
}, { deep: true, immediate: true })
</script>
<template>
    <!-- <jda-modal :width="800" v-model:open="openCreateModal" title="新建" :confirm-loading="createConfirmLoading"
        @ok="handleOk" @cancel="closeModal"> -->
    <a-card>
        <jda-form ref="formRef" :model="model" layout="horizontal" labelAlign="right" :rules="rules"
            :label-col="{ style: { width: '80px' } }">
            <a-row :gutter="48">
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="编码" name="code">
                        <a-input v-model:value="model.code" placeholder="请输入编码" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="名称" name="name">
                        <a-input v-model:value="model.name" placeholder="请输入名称" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="序号" name="sort">
                        <a-input v-model:value="model.sort" placeholder="请输入序号" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="路由资源" name="routeResourceId">
                        <jda-tree-select v-model:value="model.routeResourceId" placeholder="请选择路由资源"
                            :tree-data="menuTreeNodes" tree-node-filter-prop="name">
                        </jda-tree-select>
                    </a-form-item>
                </a-col>
            </a-row>
        </jda-form>
    </a-card>

    <jda-modal-footer :confirmLoading="createConfirmLoading" @ok="handleOk" @cancel="closeModal"></jda-modal-footer>
    <!-- </jda-modal> -->
</template>
<style lang="scss" scoped></style>