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
import type { NamePath } from 'ant-design-vue/es/form/interface';
import { computed, watch } from 'vue';
import { reactive, ref } from 'vue'
defineOptions({
    name: 'jda-form'
})

const props = withDefaults(defineProps<{
    labelAlign: 'left' | 'right'
}>(), {
    labelAlign: 'right'
})

const myFormRef = ref();

defineExpose({
    //表单验证
    validate: () => new Promise((resolve, reject) => {
        myFormRef.value.validate()
            .then((nameList?: NamePath[]) => {
                // console.log('nameList', nameList)
                resolve(nameList);
            })
            .catch((error: any) => {
                // console.log('error', error);
                reject(error);
            });
    })
})

</script>
<template>
    <a-form ref="myFormRef" layout="horizontal" v-bind="$attrs" :labelAlign="props.labelAlign">
        <!-- 通过遍历实现插槽透传 -->
        <template v-for="(item, key, index) in $slots" :key="index" v-slot:[key]="scopeData">
            <slot :name="key" v-bind="scopeData || {}"></slot>
        </template>
    </a-form>
</template>
<style lang="scss" scoped></style>