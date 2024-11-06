<script setup lang="ts">
import { reactive, ref, watch } from 'vue'
defineOptions({
    name: 'userinfo-edit'
})

const props = withDefaults(defineProps<{
    openCreateModal?: boolean
}>(), {
    openCreateModal: false
})

const emits = defineEmits(['update:openCreateModal'])

// let openCreateModal = ref<boolean>(props.openCreateModal);


// watch(
//     () => props.openCreateModal,
//     (newVal, oloVal) => {
//         console.log(newVal);
//         openCreateModal.value = newVal;
//     },
//     { deep: true, immediate: true }
// )

let model = reactive({
    Account: null,
    Password: null,
    UserName: null,
    Mobile: null,
    Email: null,
    Gender: null
})
let createConfirmLoading = ref<boolean>(false);
const handleOk = () => {
    createConfirmLoading.value = true;
    setTimeout(() => {
        closeModal();
        // props.openCreateModal = false;
        createConfirmLoading.value = false;
    }, 1000);
};
const closeModal = () => {
    emits('update:openCreateModal', false);
}
</script>
<template>
    <!-- <jda-modal :width="800" v-model:open="openCreateModal" title="新建" :confirm-loading="createConfirmLoading"
        @ok="handleOk" @cancel="closeModal"> -->
    <a-card>
        <a-form :model="model" layout="horizontal" labelAlign="left" :label-col="{ style: { width: '60px' } }">
            <a-row :gutter="48">
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="账号">
                        <a-input v-model:value="model.Account" placeholder="请输入账号" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="密码">
                        <a-input v-model:value="model.Password" placeholder="请输入密码" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="名称">
                        <a-input v-model:value="model.UserName" placeholder="请输入姓名" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="手机号">
                        <a-input v-model:value="model.Mobile" placeholder="请输入手机号码" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="性别">
                        <a-input v-model:value="model.Gender" placeholder="请输入性别" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="邮箱">
                        <a-input v-model:value="model.Email" placeholder="请输入邮箱" />
                    </a-form-item>
                </a-col>
            </a-row>
        </a-form>
    </a-card>

    <jda-modal-footer :confirmLoading="createConfirmLoading" @ok="handleOk" @cancel="closeModal"></jda-modal-footer>
    <!-- </jda-modal> -->
</template>
<style lang="scss" scoped></style>