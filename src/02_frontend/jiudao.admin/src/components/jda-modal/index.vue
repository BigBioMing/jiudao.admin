<script setup lang="ts">
import { type VNode, reactive, ref } from 'vue'
defineOptions({
    name: 'jda-modal'
})

const props = withDefaults(defineProps<{
    footer?: string | VNode | Function | null
}>(), {
    footer: null
})
</script>
<template>
    <a-modal v-bind="$attrs" :footer="props.footer" wrap-class-name="jda-modal">
        <template v-for="(item, key, index) in $slots" :key="index" v-slot:[key]="scopeData">
            <slot :name="key" v-bind="scopeData || {}"></slot>
        </template>
    </a-modal>
</template>
<style lang="scss">
.jda-modal {
    .ant-modal-header {
        margin-bottom: 20px;
    }
}
</style>