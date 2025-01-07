<!-- vue3禁止inheritAttrs
如果你使用了 <script setup>，你需要一个额外的 <script> 块来书写这个选项声明：  
-->
<script lang="ts">
// 使用普通的 <script> 来声明选项
export default {
    inheritAttrs: false
}
</script>
<script setup lang="ts">
import { computed, watch } from 'vue';
import { reactive, ref } from 'vue'
defineOptions({
    name: 'jda-card'
})

// const props = withDefaults(defineProps<{
//     boxShadow: boolean
// }>(), {
//     boxShadow: true
// })

// //card样式，控制box-shadow是否显示
// let cardStyle = ref({});
// watch(() => props.boxShadow, (newVal, oldVal) => {
//     if (newVal) {
//         cardStyle.value = {};
//     } else {
//         cardStyle.value = { boxShadow: 'none' };
//     }
// })

</script>
<template>
    <a-card v-bind="$attrs" style="overflow: hidden;">
        <!-- 通过遍历实现插槽透传 -->
        <template v-for="(item, key, index) in $slots" :key="index" v-slot:[key]="scopeData">
            <slot :name="key" v-bind="scopeData || {}"></slot>
        </template>
    </a-card>
</template>
<style lang="scss" scoped></style>