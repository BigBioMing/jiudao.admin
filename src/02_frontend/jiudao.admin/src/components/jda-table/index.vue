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
import { computed, reactive, ref, useSlots, useAttrs, watch } from 'vue'

//vue3中访问$attrs
// import { useAttrs } from 'vue'
// const attrs = useAttrs()
defineOptions({
    name: 'jda-table'
})

let props = defineProps<{
    columns: any[],
    pagination: any
}>()

//'get-table-data-source'-获取表格数据源
//'create'-点击新建按钮
//'get-table-data-source'-点击导出按钮
const emit = defineEmits(['get-table-data-source', 'create', 'import'])

//分页配置
let pagination = {
    defaultPageSize: 10,
    pageSize: 10,
    showSizeChanger: true
    , pageSizeOptions: ['10', '20', '30', '50']
    , showTotal: (total: number) => `总共 ${total} 条数据`
    , buildOptionText: ({ value }: any) => `${value} 条/页`
    , onChange: (page: number, pageSize: number) => {
        //根据 当前每页数量 判断时候改变了每页条数，如果改变了，则pageIndex=1
        let pageIndex = page;
        if (currentPageSize !== pageSize) {
            pageIndex = 1;
            currentPageSize = pageSize;
        }
        pagination.pageSize = pageSize;
        emit('get-table-data-source', { page: { pageIndex: pageIndex, pageSize: pageSize } })
    }
    // , onShowSizeChange: (current: number, size: number) => {
    //     emit('get-table-data-source', { page: { pageIndex: 1, pageSize: size } })
    // }
}
//当前每页数量
let currentPageSize = pagination.defaultPageSize || pagination.pageSize;
watch(
    () => props.pagination,
    (newVal, oldVal) => {
        Object.assign(pagination, newVal);
    },
    { deep: true, immediate: true }
)


const onCreate = () => {
    emit('create')
}
const onImport = () => {
    emit('import')
}

//对象克隆
const clone: any = (obj: any) => {
    if (!obj) return obj;
    return JSON.parse(JSON.stringify(obj));
}

//初始化innerColumns
const initInnerColumns = (columns: any[]) => {
    let arr = [...columns];
    for (let i = 0; i < arr.length; i++) {
        let item = arr[i];
        item.originalFixed = item.fixed;
        item.originalChecked = true;
        item.checked = true;
        item.index = i + 1;
    }
    innerColumns.value = arr;
}
//column可以排序、隐藏、固定，使用新的变量存储
let innerColumns = ref<any[]>([]);
watch(
    () => props.columns,
    (newVal, oldVal) => {
        initInnerColumns(clone(newVal));
    },
    { deep: true, immediate: true }
);
watch(
    () => innerColumns,
    (newVal, oldVal) => {
        let checkedSelectCount = newVal.value.filter(n => n.checked).length;
        let fixedSelectCount = newVal.value.filter(n => n.fixed).length;
        let len = newVal.value.length;
        if (checkedSelectCount === len)
            columnSetting.showAndOrder.checkAll = true;
        else
            columnSetting.showAndOrder.checkAll = false;
        if (fixedSelectCount === len)
            columnSetting.fixed.checkAll = true;
        else
            columnSetting.fixed.checkAll = false;
    },
    { deep: true }
);

//表格用的列，根据需要展示的列及排列顺序呈现
let computedColumns = computed(() => {
    return innerColumns.value.filter(n => n.checked);
})

// 表格配置项
let columnSetting = reactive({
    // 列展示/排序
    showAndOrder: {
        // 列展示/排序是否全选
        checkAll: true,//是否全选
        indeterminate: computed(() => {
            //选中的列数量
            let selectCount = innerColumns.value.filter(n => n.checked).length;
            let len = innerColumns.value.length;
            return selectCount > 0 && selectCount < len;
        }),
        onCheckAllChange: (e: Event) => {
            columnSetting.showAndOrder.onCheckAll(e.target?.checked)
        },
        onCheckAll: (checked: boolean) => {
            for (let i = 0; i < innerColumns.value.length; i++) {
                var innerColumn = innerColumns.value[i];
                innerColumn.checked = checked;
            }
        },
        onChange: (e: Event) => {
        },
        onReset: () => {
            initInnerColumns(clone(props.columns));
        }
    },
    // 列固定
    fixed: {
        // 列固定是否全选
        checkAll: false,// 是否全选
        indeterminate: computed(() => {
            //选中的列数量
            let selectCount = innerColumns.value.filter(n => n.fixed).length;
            let len = innerColumns.value.length;
            return selectCount > 0 && selectCount < len;
        }),
        onCheckAllChange: (e: Event) => {
            columnSetting.fixed.onCheckAll(e.target?.checked)
        },
        onCheckAll: (checked: boolean) => {
            if (checked) {
                for (let i = 0; i < innerColumns.value.length; i++) {
                    var innerColumn = innerColumns.value[i];
                    innerColumn.fixed = 'right';
                }
            } else {
                for (let i = 0; i < innerColumns.value.length; i++) {
                    var innerColumn = innerColumns.value[i];
                    innerColumn.fixed = undefined;
                }
            }
        },
        onChange: (e: Event, column: any) => {
            //选择列固定模式后进行排序，取消的放在最后，left排在前面，right放在后面
            let leftArr = innerColumns.value.filter(n => n.fixed === 'left');
            let rightArr = innerColumns.value.filter(n => n.fixed === 'right');
            let cancelArr = innerColumns.value.filter(n => !n.fixed);
            arrayOrder(leftArr, 'index');
            arrayOrder(rightArr, 'index');
            arrayOrder(cancelArr, 'index');
            innerColumns.value = [...leftArr, ...cancelArr, ...rightArr]
        },
        onReset: () => {
            initInnerColumns(clone(props.columns));
        }
    }
})

//冒泡排序
const arrayOrder = (arr: any[], field: string) => {
    var len = arr.length;
    for (var i = 0; i < len - 1; i++) {
        for (var j = 0; j < len - 1 - i; j++) {
            let curItem = arr[j];//当前元素
            let nextItem = arr[j + 1];//相邻的下个元素
            if (curItem[field] > nextItem[field]) {        // 相邻元素两两对比
                var temp = nextItem;        // 元素交换
                arr[j + 1] = curItem;
                arr[j] = temp;
            }
        }
    }
    return arr;
}

const onChoose = (/**Event*/evt: any) => {
}
const onUnchoose = (/**Event*/evt: any) => {
}
const onStart = (/**Event*/evt: any) => {
}
const onEnd = (/**Event*/evt: any) => {
    //拖拽后重新排序
    for (let i = 0; i < innerColumns.value.length; i++) {
        innerColumns.value[i].index = i + 1;
    }
}
const onUpdate = (/**Event*/evt: any) => {
}
const onMove = (/**Event*/evt: any) => {
}
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
                            <a-button type="primary" @click="onCreate">
                                <template #icon>
                                    <font-awesome-icon icon="fas fa-plus" />
                                </template>
                                新建</a-button>
                            <a-button type="primary" @click="onImport">
                                <template #icon>
                                    <font-awesome-icon icon="fas fa-arrow-circle-down" />
                                </template>
                                导出</a-button>
                        </a-space>
                    </div>
                    <div class="jda-table-toolbar-divider" >
                        <a-divider type="vertical" />
                    </div>
                    <div class="jda-table-toolbar-icon-btns">
                        <div class="jda-table-toolbar-icon-btn">
                            <a-popover placement="bottomRight" trigger="click">
                                <template #title>
                                    <div class="jda-table-column-settings-title">
                                        <a-checkbox v-model:checked="columnSetting.showAndOrder.checkAll"
                                            :indeterminate="columnSetting.showAndOrder.indeterminate"
                                            @change="columnSetting.showAndOrder.onCheckAllChange">列展示 /
                                            排序</a-checkbox>
                                        <a @click="columnSetting.showAndOrder.onReset">重置</a>
                                    </div>
                                </template>
                                <template #content>
                                    <vue-draggable ref="el" v-model="innerColumns" class="jda-table-column-settings"
                                        @choose="onChoose" @unchoose="onUnchoose" @start="onStart" @end="onEnd"
                                        @update="onUpdate" @move="onMove">
                                        <div class="jda-table-column-draggable-item" v-for="(item) in innerColumns"
                                            :key="item.key">
                                            <span><font-awesome-icon icon="fas fa-list-ul" /></span>
                                            <a-checkbox v-model:checked="item.checked"
                                                @change="columnSetting.showAndOrder.onChange">{{ item.title
                                                }}</a-checkbox>
                                        </div>
                                    </vue-draggable>
                                </template>
                                <span>
                                    <font-awesome-icon icon="fas fa-list-ul" class="jda-table-toolbar-icon" />
                                </span>
                            </a-popover>
                        </div>
                        <div class="jda-table-toolbar-icon-btn">
                            <a-popover placement="bottomRight" trigger="click">
                                <template #title>
                                    <div class="jda-table-column-settings-title">
                                        <a-checkbox v-model:checked="columnSetting.fixed.checkAll"
                                            :indeterminate="columnSetting.fixed.indeterminate"
                                            @change="columnSetting.fixed.onCheckAllChange">列固定</a-checkbox>
                                        <a @click="columnSetting.fixed.onReset">重置</a>
                                    </div>
                                </template>
                                <template #content>
                                    <div class="jda-table-column-settings">
                                        <div class="jda-table-column-fixed-item" v-for="(item) in innerColumns"
                                            :key="item.key">
                                            <div>
                                                <span><font-awesome-icon icon="fas fa-anchor" /></span>
                                                <span>{{ item.title }}</span>
                                            </div>
                                            <a-radio-group v-model:value="item.fixed" button-style="solid" size="small"
                                                @change="columnSetting.fixed.onChange($event, item)">
                                                <a-radio-button value="left">左侧</a-radio-button>
                                                <a-radio-button value="right">右侧</a-radio-button>
                                                <a-radio-button>取消</a-radio-button>
                                            </a-radio-group>
                                        </div>
                                    </div>
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
        <a-table v-bind="$attrs" :columns="computedColumns" :pagination="pagination">
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
            min-height: 72px;
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

.jda-table-column-settings-title {
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

    .jda-table-column-fixed-item {
        display: flex;
        justify-content: space-between;
        width: 100%;
        padding: 4px 16px 4px 0;
        cursor: pointer;

        &>div {
            &:nth-child(1) {
                margin-right: 20px;
            }

            span {
                &:nth-child(1) {
                    margin-right: 5px;
                }
            }
        }
    }
}
</style>