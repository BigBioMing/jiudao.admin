<script setup lang="ts">
import type { Rule } from 'ant-design-vue/es/form';
import { reactive, ref, toRaw, watch, type UnwrapRef } from 'vue'
import { getEntityApi, saveRouteResourceApi } from '@/apis/sys/routeResource'
import { getRouteTreeApi } from '@/apis/sys/routeResource'
import { useSysDic } from '@/hooks'
import { onMounted } from 'vue';
defineOptions({
    name: 'routeResource-edit'
})

const props = withDefaults(defineProps<{
    openCreateModal?: boolean,
    id: number | null
}>(), {
    openCreateModal: false,
    id: null
})

const emits = defineEmits(['update:openCreateModal', 'ok'])

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
//表单对象
let model = reactive({
    id: null,
    code: null,
    name: null,
    title: null,
    url: null,
    redirect: null,
    component: null,
    icon: null,
    showInMenu: true,
    isThird: false,
    parentId: null,
    childrens: null
})


// 表单验证规则
const rules: Record<string, Rule[]> = {
    code: [
        { required: true, message: '请输入路由编码', trigger: 'change' },
    ],
    name: [
        { required: true, message: '请输入路由名称', trigger: 'change' }
    ]
};

//获取当前路由资源信息
const onGetCurrentModel = async () => {
    if (props.id && props.id > 0) {
        //根据id获取用户信息
        const res = await getEntityApi(props.id!);
        Object.assign(model, res);
        if (model.parentId == 0)
            model.parentId = null;
    }
}
const menuTreeNodes = ref<any[]>([]);
//获取路由树
const onGetRouteTree = async () => {
    const res = await getRouteTreeApi({});
    menuTreeNodes.value = res as any;
    console.log(menuTreeNodes)
}

//获取当前路由资源信息
onGetCurrentModel();
onGetRouteTree();

let createConfirmLoading = ref<boolean>(false);
const handleOk = () => {
    formRef.value.validate()
        .then(async () => {
            createConfirmLoading.value = true;
            let sendData = toRaw(model);
            await saveRouteResourceApi(sendData);
            createConfirmLoading.value = false;
            closeModal();
            emits('ok');
        })
        .catch((error: any) => {
            console.log('error', error);
        });
};
const closeModal = () => {
    createConfirmLoading.value = false;
    emits('update:openCreateModal', false);
}
</script>
<template>
    <div class="page-wrap page-wrap-slave">
    <!-- <jda-modal :width="800" v-model:open="openCreateModal" title="新建" :confirm-loading="createConfirmLoading"
        @ok="handleOk" @cancel="closeModal"> -->
    <a-card>
        <jda-form ref="formRef" :model="model" layout="horizontal" labelAlign="right" :rules="rules">
            <a-row :gutter="48">
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="编码" name="code">
                        <a-input v-model:value="model.code" placeholder="请输入路由编码" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="名称" name="name">
                        <a-input v-model:value="model.name" placeholder="请输入路由名称" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="标题" name="title">
                        <a-input v-model:value="model.title" placeholder="请输入路由标题" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="上级路由" name="parentId">
                        <jda-tree-select v-model:value="model.parentId" placeholder="请选择上级路由资源"
                            :tree-data="menuTreeNodes" tree-node-filter-prop="name">
                        </jda-tree-select>
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="Url" name="url">
                        <a-input v-model:value="model.url" placeholder="请输入路由路径" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="重定向路径" name="redirect">
                        <a-input v-model:value="model.redirect" placeholder="请输入重定向路径" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="组件路径" name="component">
                        <a-input v-model:value="model.component" placeholder="请输入组件路径" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="菜单图标" name="icon">
                        <!-- <a-input v-model:value="model.icon" placeholder="请输入菜单图标" /> -->
                        <a-input v-model:value="model.icon">
                            <template #addonAfter>
                                <font-awesome-icon v-if="model.icon" :icon="model.icon"></font-awesome-icon>
                            </template>
                        </a-input>
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="是否在菜单栏展示" name="showInMenu" :label-col="{ style: { width: '130px' } }">
                        <a-switch v-model:checked="model.showInMenu" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="是否第三方路由" name="isThird" :label-col="{ style: { width: '130px' } }">
                        <a-switch v-model:checked="model.isThird" />
                    </a-form-item>
                </a-col>
            </a-row>
        </jda-form>
    </a-card>

    <jda-modal-footer :confirmLoading="createConfirmLoading" @ok="handleOk" @cancel="closeModal"></jda-modal-footer>
    <!-- </jda-modal> -->
    </div>
</template>
<style lang="scss" scoped></style>