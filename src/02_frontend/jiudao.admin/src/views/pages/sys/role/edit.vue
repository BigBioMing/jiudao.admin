<script setup lang="ts">
import type { Rule } from 'ant-design-vue/es/form';
import { reactive, ref, toRaw, watch, type UnwrapRef } from 'vue'
import { getEntityApi, saveRoleApi } from '@/apis/sys/role'
import { useSysDic } from '@/hooks'
import { onMounted } from 'vue';
defineOptions({
    name: 'role-edit'
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
    description: null
})


// 表单验证规则
const rules: Record<string, Rule[]> = {
    code: [
        { required: true, message: '请输入编码', trigger: 'change' }
    ],
    name: [
        { required: true, message: '请输入角色名称', trigger: 'change' }
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
            await saveRoleApi(sendData);
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
    <jda-edit-card>
        <jda-form ref="formRef" :model="model" :rules="rules"
            :label-col="{ style: { width: '80px' } }">
            <a-row :gutter="48">
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="编码" name="code">
                        <a-input v-model:value="model.code" placeholder="请输入编码" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="角色名称" name="name">
                        <a-input v-model:value="model.name" placeholder="请输入角色名称" />
                    </a-form-item>
                </a-col>
                <a-col :md="24" :sm="24" :xs="24" :lg="24">
                    <a-form-item label="描述" name="description">
                        <a-textarea :rows="3" v-model:value="model.description" placeholder="描述" />
                    </a-form-item>
                </a-col>
            </a-row>
        </jda-form>
    </jda-edit-card>

    <jda-modal-footer :confirmLoading="createConfirmLoading" @ok="handleOk" @cancel="closeModal"></jda-modal-footer>
    <!-- </jda-modal> -->
</template>
<style lang="scss" scoped></style>