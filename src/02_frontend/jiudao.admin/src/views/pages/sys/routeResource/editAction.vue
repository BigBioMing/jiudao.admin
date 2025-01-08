<script setup lang="ts">
import type { Rule } from 'ant-design-vue/es/form';
import { reactive, ref, toRaw, watch, type UnwrapRef } from 'vue'
import { getActionsApi, saveActionsApi } from '@/apis/sys/routeResource'
import { MinusCircleOutlined, PlusOutlined } from '@ant-design/icons-vue';
defineOptions({
    name: 'routeResource-editAction'
})

const props = withDefaults(defineProps<{
    openActionModal?: boolean,
    routeResourceId: number | null
}>(), {
    openActionModal: false,
    routeResourceId: null
})

const emits = defineEmits(['update:openActionModal','ok'])


const formRef = ref();
//表单对象
let model = reactive({
    actions: <any[]>[]
});


// 表单验证规则
const rules: Record<string, Rule[]> = {
    code: [
        { required: true, message: '请输入路由编码', trigger: 'change' },
    ],
    name: [
        { required: true, message: '请输入路由名称', trigger: 'change' }
    ]
};

//获取当前路由资源下的按钮集合
const onGetActions = async () => {
    if (props.routeResourceId && props.routeResourceId > 0) {
        //根据id获取用户信息
        const res = await getActionsApi({routeResourceId:props.routeResourceId});
        model.actions = res;
        if (model.actions.length == 0) {
            model.actions.push({
                code: null,
                name: null,
                sort: 1
            })
        }
    }
}

//获取当前路由资源下的按钮集合
onGetActions();

let createConfirmLoading = ref<boolean>(false);
const handleOk = () => {
    formRef.value.validate()
        .then(async () => {
            createConfirmLoading.value = true;
            let sendData = toRaw(model);
            await saveActionsApi({ routeResourceId: props.routeResourceId, actions: sendData.actions });
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
    emits('update:openActionModal', false);
}

const onAddAction = (action: any) => {
    const index = model.actions.indexOf(action);
    if (index > -1) {
        model.actions.splice(index, 0, { code: null, name: null, sort: 1 });
    } else {
        model.actions.push({ code: null, name: null, sort: 1 });
    }
}

const onRemoveAction = (action: any) => {
    const index = model.actions.indexOf(action);
    if (index > -1) {
        model.actions.splice(index, 1);
    }
}
</script>
<template>
    <a-card>
        <jda-form ref="formRef" :model="model" layout="horizontal" labelAlign="right">
            <a-row :gutter="48" v-for="(action, index) in model.actions" :key="index">
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="编码" :name="['actions', index, 'code']"
                        :rules="{ required: true, message: '请输入功能编码' }">
                        <a-input v-model:value="action.code" placeholder="请输入功能编码" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="名称" :name="['actions', index, 'name']"
                        :rules="{ required: true, message: '请输入功能名称' }">
                        <a-input v-model:value="action.name" placeholder="请输入功能名称" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label="序号" :name="['actions', index, 'sort']"
                        :rules="{ required: true, message: '请输入序号' }">
                        <a-input v-model:value="action.sort" placeholder="请输入序号" />
                    </a-form-item>
                </a-col>
                <a-col :md="12" :sm="24" :xs="24" :lg="12">
                    <a-form-item label=" " :colon="false" name="title">
                        <MinusCircleOutlined @click="onRemoveAction(action)" class="dynamic-delete-button"
                            style="margin-right: 10px;" />
                        <PlusCircleOutlined @click="onAddAction(action)" class="dynamic-delete-button">
                        </PlusCircleOutlined>
                    </a-form-item>
                </a-col>
            </a-row>
        </jda-form>
    </a-card>

    <jda-modal-footer :confirmLoading="createConfirmLoading" @ok="handleOk" @cancel="closeModal"></jda-modal-footer>
</template>
<style lang="scss" scoped>
.dynamic-delete-button {
    cursor: pointer;
    position: relative;
    top: 4px;
    font-size: 24px;
    color: #999;
    transition: all 0.3s;
}

.dynamic-delete-button:hover {
    color: #777;
}

.dynamic-delete-button[disabled] {
    cursor: not-allowed;
    opacity: 0.5;
}
</style>