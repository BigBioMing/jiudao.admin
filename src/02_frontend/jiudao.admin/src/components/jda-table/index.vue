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
import { useSlots } from 'vue'

defineOptions({
    name: 'jda-table'
})

const slots = useSlots()
console.log('---------------slots s---------------')
console.log(slots);
for (let key in slots) {
    console.log('key:' + key, slots[key]);
}


//vue3中访问$attrs
// import { useAttrs } from 'vue'
// const attrs = useAttrs()


/**
// 插槽


// 普通插槽
// $slots可以拿到父组件所传递过来的所有插槽,它是一个对象，key 名对应着插槽名。
<!-- 子组件 -->
<template>
  <el-button v-bind="$attrs">
    <!-- 通过遍历实现插槽透传 -->
    <template v-for="(item, key, index) in $slots" :key="index" v-slot:[key]>
      <slot :name="key"></slot>
    </template>
  </el-button>
</template>
<!-- 父组件 -->
<template>
  <MyButton type="primary">
    <template #default>按钮</template>
    <template #icon>111</template>
    <template #footer>ceshi</template>
  </MyButton>
</template>

// 作用域插槽
// 如果插槽是一个作用域插槽，传递给该插槽函数的参数可以作为插槽的 prop 提供给插槽。
<!-- 子组件 -->
<template>
  <el-button v-bind="$attrs">
    <!-- 通过便利实现插槽透传 -->
    <!-- v-slot:[key] 绑定到对应的插槽中 -->
    <template v-for="(item, key, index) in $slots" :key="index" v-slot:[key]>
      <slot :name="key" v-if="key === 'icon'" :count="99"></slot>
      <slot :name="key" v-else></slot>
    </template>
  </el-button>
</template>
<!-- 父组件 -->
<template>
  <MyButton type="primary">
    <template #default>按钮</template>
    <template #icon="iconProps">11{{ iconProps.count }}</template>
    <template #footer>ceshi</template>
  </MyButton>
</template>
*/
</script>
<template>
    <div class="jds-table">
        <div class="jda-table-toolbar">
            <div class="jda-table-toolbar-wrapper">
                <div class="jda-table-toolbar-list">
                    <div class="jda-table-toolbar-btns">
                        <a-space>
                            <a-button type="primary">
                                <template #icon>
                                    <font-awesome-icon icon="fas fa-plus" />
                                </template>
                                新建</a-button>
                            <a-button type="primary">
                                <template #icon>
                                    <font-awesome-icon icon="fas fa-arrow-circle-down" />
                                </template>
                                导出</a-button>
                        </a-space>
                    </div>
                    <div class="jda-table-toolbar-divider">
                        <a-divider type="vertical" />
                    </div>
                    <div class="jda-table-toolbar-icon-btns">
                        <div class="jda-table-toolbar-icon-btn">
                            <font-awesome-icon icon="fas fa-list-ul" class="jda-table-toolbar-icon" />
                        </div>
                        <div class="jda-table-toolbar-icon-btn">
                            <font-awesome-icon icon="fas fa-anchor" class="jda-table-toolbar-icon" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <a-table v-bind="$attrs">
            <!-- 通过遍历实现插槽透传 -->
            <!-- <template v-for="(value, name) in $slots" #[name]="scopeData">
                <slot :name="name" v-bind="scopeData || {}"></slot>
            </template> -->
            <template v-for="(item, key, index) in $slots" :key="index" v-slot:[key]="scopeData">
                <slot :name="key" v-bind="scopeData || {}"></slot>
            </template>
        </a-table>
    </div>
</template>
<style lang="scss" scoped>
.jds-table {
    .jda-table-toolbar {
        display: flex;
        flex-direction: row;
        justify-content: right;

        .jda-table-toolbar-wrapper {
            padding: 0 24px;
            height: 72px;
            line-height: 72px;

            .jda-table-toolbar-list {
                .jda-table-toolbar-icon {
                    font-size: 16px;
                    cursor: pointer;
                }

                :deep(.ant-btn) {
                    svg {
                        margin-right: 5px;
                    }
                }

                .jda-table-toolbar-divider,
                .jda-table-toolbar-btns,
                .jda-table-toolbar-icon-btns,
                .jda-table-toolbar-icon-btn {
                    display: inline-block;
                }

                .jda-table-toolbar-divider {
                    padding: 0 8px;
                }

                .jda-table-toolbar-btns {
                    font-size: 16px;
                }

                .jda-table-toolbar-icon-btns {
                    font-size: 16px;

                    &:first-child {
                        margin-left: 0;
                    }

                    .jda-table-toolbar-icon-btn {
                        margin-left: 16px;

                        &:first-child {
                            margin-left: 0;
                        }
                    }
                }
            }
        }
    }
}
</style>