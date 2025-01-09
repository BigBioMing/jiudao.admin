<script setup lang="ts">
import type { Rule } from 'ant-design-vue/es/form';
import { reactive, ref, toRaw, watch, type UnwrapRef } from 'vue'
import { getEntityApi, saveApi } from '@/apis/sys/dictionaryDefine'
defineOptions({
    name: 'dictionaryDefine-edit'
})

const props = withDefaults(defineProps<{
    openCreateModal?: boolean,
    id: number | null
}>(), {
    openCreateModal: false,
    id: null
})

const emits = defineEmits(['update:openCreateModal'])


const formRef = ref();
//表单对象-用户信息
let model = reactive({
    code: null,
    name: null,
    enabled: null,
    description: null
})


// 表单验证规则
const rules: Record<string, Rule[]> = {
    code: [
        { required: true, message: '请输入编码', trigger: 'change' }
    ],
    name: [
        { required: true, message: '请输入名称', trigger: 'change' }
    ]
};

//获取用户信息
const onGetUser = async () => {
    if (props.id && props.id > 0) {
        //根据id获取用户信息
        const res = await getEntityApi(props.id!);
        Object.assign(model, res);
    }
}

//获取用户信息
onGetUser();

let createConfirmLoading = ref<boolean>(false);
const handleOk = () => {
    formRef.value.validate()
        .then(async () => {
            createConfirmLoading.value = true;
            let sendData = toRaw(model);
            await saveApi(sendData);
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
</script>
<template>
    <a-card>
        <jda-form ref="formRef" :model="model" layout="horizontal" labelAlign="right" :rules="rules">
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
                <a-col :md="24" :sm="24" :xs="24" :lg="24">
                    <a-form-item label="启用/禁用" name="mobile" :label-col="{ style: { width: '80px' } }">
                        <a-switch v-model:checked="model.enabled" checked-children="启用" un-checked-children="禁用" />
                    </a-form-item>
                </a-col>
                <a-col :md="24" :sm="24" :xs="24" :lg="24">
                    <a-form-item label="描述" name="description">
                        <a-textarea v-model:value="model.description" placeholder="请输入描述"
                            :autosize="{ minRows: 3, maxRows: 3 }" allow-clear />
                    </a-form-item>
                </a-col>
            </a-row>
        </jda-form>
    </a-card>

    <jda-modal-footer :confirmLoading="createConfirmLoading" @ok="handleOk" @cancel="closeModal"></jda-modal-footer>
</template>
<style lang="scss" scoped></style>