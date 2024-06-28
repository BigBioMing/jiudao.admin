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
import { reactive, ref, useSlots } from 'vue'

defineOptions({
    name: 'jda-table'
})


const columns = reactive([
    {
        title: '名称',
        dataIndex: 'name',
        key: 'name',
    },
    {
        title: '年龄',
        dataIndex: 'age',
        key: 'age',
    },
    {
        title: '家庭住址',
        dataIndex: 'address',
        key: 'address',
    },
    {
        title: '标签',
        key: 'tags',
        dataIndex: 'tags',
    },
    {
        title: '操作',
        key: 'action',
        fixed: 'right',
    },
]);

// 列展示/排序是否全选
let isColumnShowAndOrderCheckAll = ref<Boolean>(false);
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
    <div class="jda-table">
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
                            <a-popover placement="bottomRight" trigger="click">
                                <template #title>
                                    <div class="jda-table-column-settings-title">
                                        <a-checkbox v-model:checked="isColumnShowAndOrderCheckAll">列展示 / 排序</a-checkbox>
                                        <a>重置</a>
                                    </div>
                                </template>
                                <template #content>
                                    <vue-draggable ref="el" v-model="columns" class="jda-table-column-settings">
                                        <div class="jda-table-column-draggable-item" v-for="(item) in columns"
                                            :key="item.key">
                                            <span><font-awesome-icon icon="fas fa-list-ul" /></span>
                                            <a-checkbox v-model:checked="item.checked">{{ item.title }}</a-checkbox>
                                        </div>
                                    </vue-draggable>
                                </template>
                                <span>
                                    <font-awesome-icon icon="fas fa-list-ul" class="jda-table-toolbar-icon" />
                                </span>
                            </a-popover>
                        </div>
                        <div class="jda-table-toolbar-icon-btn">
                            <a-popover title="Title" placement="bottomRight" trigger="click">
                                <template #content>
                                    <vue-draggable ref="el" v-model="columns" class="jda-table-column-settings">
                                        <div class="jda-table-column-draggable-item" v-for="(item) in columns"
                                            :key="item.key">
                                            <span>
                                                <font-awesome-icon icon="far fa-calendar-check" />
                                            </span>
                                            <a-checkbox v-model:checked="item.checked">{{ item.title }}</a-checkbox>
                                        </div>
                                    </vue-draggable>
                                </template>
                                <span>
                                    <font-awesome-icon icon="fas fa-anchor" class="jda-table-toolbar-icon" />
                                </span>
                            </a-popover>
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
.jda-table {
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

.jda-table-column-settings-title{
    display: flex;
    flex-direction: row;
    justify-content: space-between;
}

.jda-table-column-settings {
    padding: 5px 10px;

    .jda-table-column-draggable-item {
        display: flex;
        align-items: center;
        width: 100%;
        padding: 4px 16px 4px 0;
        cursor: pointer;

        &>span {
            padding-right: 6px;
            cursor: move;
            position: relative;
            top: -1px;
        }

        :deep(.ant-checkbox-wrapper) {
            span:nth-child(2) {
                position: relative;
                top: -1px;
            }
        }
    }
}
</style>