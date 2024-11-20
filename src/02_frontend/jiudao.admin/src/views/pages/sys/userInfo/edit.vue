<script setup lang="ts">
import type { Rule } from 'ant-design-vue/es/form';
import { reactive, ref, toRaw, watch, type UnwrapRef } from 'vue'
import { getEntityApi, saveUserApi } from '@/apis/sys/userinfo'
import { useSysDic } from '@/hooks'
import { onMounted } from 'vue';
defineOptions({
    name: 'userinfo-edit'
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
    account: null,
    password: null,
    name: null,
    mobile: null,
    email: null,
    gender: null
})


// 表单验证规则
const rules: Record<string, Rule[]> = {
    account: [
        { required: true, message: '请输入账号名称', trigger: 'change' },
        { min: 3, max: 20, message: '长度应该是3-20', trigger: 'blur' },
    ],
    password: [
        { required: true, message: '请输入密码', trigger: 'change' },
        { min: 6, max: 20, message: '长度应该是6-20', trigger: 'blur' }
    ],
    name: [
        { required: true, message: '请输入姓名', trigger: 'change' }
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
            await saveUserApi(sendData);
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
    <!-- <jda-modal :width="800" v-model:open="openCreateModal" title="新建" :confirm-loading="createConfirmLoading"
        @ok="handleOk" @cancel="closeModal"> -->
    <a-card>
        <jda-form ref="formRef" :model="model" layout="horizontal" labelAlign="right" :rules="rules"
            :label-col="{ style: { width: '60px' } }">
            <a-row :gutter="48">
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="账号" name="account">
                        <a-input v-model:value="model.account" placeholder="请输入账号" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="密码" name="password">
                        <a-input v-model:value="model.password" placeholder="请输入密码" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="名称" name="name">
                        <a-input v-model:value="model.name" placeholder="请输入姓名" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="手机号" name="mobile">
                        <a-input v-model:value="model.mobile" placeholder="请输入手机号码" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="性别" name="gender">
                        <!-- <a-input v-model:value="model.gender" placeholder="请输入性别" /> -->
                        <a-select v-model:value="model.gender" allowClear>
                            <a-select-option v-for="(item, index) in sexDicItems" :value="item.id">{{ item.name
                                }}</a-select-option>
                        </a-select>
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="邮箱" name="email">
                        <a-input v-model:value="model.email" placeholder="请输入邮箱" />
                    </a-form-item>
                </a-col>
            </a-row>
        </jda-form>
    </a-card>

    <jda-modal-footer :confirmLoading="createConfirmLoading" @ok="handleOk" @cancel="closeModal"></jda-modal-footer>
    <!-- </jda-modal> -->
</template>
<style lang="scss" scoped></style>