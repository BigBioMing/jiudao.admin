<script setup lang="ts">
defineOptions({
    name: 'jda-modal-footer'
})

const props = withDefaults(defineProps<{
    cancelText?: string,//取消按钮文字	
    okText?: string,//确认按钮文字
    okType?: string//确认按钮类型
    confirmLoading?: boolean
}>(), {
    cancelText: '取消',
    okText: '保存',
    okType: 'primary',
    confirmLoading: false
})

const emits = defineEmits(['ok', 'cancel'])

const handleOk = () => {
    emits('ok');
}
const handleCancel = () => {
    emits('cancel');
}
</script>
<template>
    <div class="jda-modal-footer">
        <slot>
            <a-space>
                <a-button @click="handleCancel">
                    {{ props.cancelText }}
                </a-button>
                <a-button @click="handleOk" :type="props.okType" :loading="props.confirmLoading">
                    {{ props.okText }}
                </a-button>
            </a-space>
        </slot>
    </div>
</template>
<style lang="scss" scoped>
.jda-modal-footer {
    text-align: end;
    background: transparent;
    margin-top: 18px;
}
</style>