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
import { reactive, ref } from 'vue'
import JdaCard from './index.vue'
defineOptions({
    name: 'jda-edit-card'
})
</script>
<template>
    <jda-card :bordered="false" class="jda-edit-card" v-bind="$attrs">
        <!-- 通过遍历实现插槽透传 -->
        <template v-for="(item, key, index) in $slots" :key="index" v-slot:[key]="scopeData">
            <slot :name="key" v-bind="scopeData || {}"></slot>
        </template>
    </jda-card>
</template>
<style lang="scss" scoped>
.jda-edit-card {
    box-shadow: none;

    :deep(.ant-card-body) {
        padding-bottom: 0;
        // padding-top: 30px;
    }
}
</style>