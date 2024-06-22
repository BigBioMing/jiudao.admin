<script setup lang="ts">
import { computed, reactive, ref, watch, h, createVNode, render, createCommentVNode, onMounted, type UnwrapRef, toRaw } from 'vue';

interface FormState {
  name: string;
  delivery: boolean;
  type: string[];
  resource: string;
  desc: string;
}
const formState: UnwrapRef<FormState> = reactive({
  name: '',
  delivery: false,
  type: [],
  resource: '',
  desc: '',
});
const onSubmit = () => {
  console.log('submit!', toRaw(formState));
};
const labelCol = { style: { width: '150px' } };
const wrapperCol = { span: 14 };
const items = reactive([
    {
        key: '1',
        // icon: () => h(PieChartOutlined),
        icon: 'far fa-square-caret-right',
        // icon:  (pa:any)=>{
        //   console.log(pa);
        //   return vn1
        // },
        label: 'Option 1',
        title: 'Option 1',
    },
    {
        key: '2',
        icon: 'fab fa-bandcamp',
        label: 'Option 2',
        title: 'Option 2',
    },
    {
        key: '3',
        icon: 'fas fa-leaf-maple',
        label: 'Option 3',
        title: 'Option 3',
    },
    {
        key: 'sub1',
        icon: 'far fa-square-caret-right',
        label: 'Navigation One',
        title: 'Navigation One',
        children: [
            {
                key: '5',
                icon: 'fas fa-plane-departure',
                label: 'Option 5',
                title: 'Option 5',
            },
            {
                key: '6',
                label: 'Option 6',
                title: 'Option 6',
            },
            {
                key: '7',
                label: 'Option 7',
                title: 'Option 7',
            },
            {
                key: '8',
                label: 'Option 8',
                title: 'Option 8',
            },
        ],
    },
    {
        key: 'sub2',
        icon: 'fas fa-umbrella-beach',
        label: 'Navigation Two',
        title: 'Navigation Two',
        children: [
            {
                key: '9',
                label: 'Option 9',
                title: 'Option 9',
            },
            {
                key: '10',
                label: 'Option 10',
                title: 'Option 10',
            },
            {
                key: 'sub3',
                label: 'Submenu',
                title: 'Submenu',
                children: [
                    {
                        key: '11',
                        label: 'Option 11',
                        title: 'Option 11',
                    },
                    {
                        key: '12',
                        label: 'Option 12',
                        title: 'Option 12',
                    },
                ],
            },
        ],
    },
    {
        key: 'sub21',
        icon: 'fas fa-cloud-sun',
        label: 'Navigation Two',
        title: 'Navigation Two',
        children: [
            {
                key: '91',
                label: 'Option 9',
                title: 'Option 9',
            },
            {
                key: '101',
                label: 'Option 10',
                title: 'Option 10',
            },
            {
                key: 'sub31',
                label: 'Submenu',
                title: 'Submenu',
                children: [
                    {
                        key: '111',
                        label: 'Option 111',
                        title: 'Option 111',
                    },
                    {
                        key: '121',
                        label: 'Option 121',
                        title: 'Option 121',
                        children: [
                            {
                                key: '1111',
                                label: 'Option 111',
                                title: 'Option 111',
                            },
                            {
                                key: '1211',
                                label: 'Option 121',
                                title: 'Option 121',
                            },
                        ]
                    },
                ],
            },
        ],
    }
]);
const open = ref<boolean>(false);

const showModal = () => {
  open.value = true;
};

const handleOk = (e: MouseEvent) => {
  console.log(e);
  open.value = false;
};
</script>
<template>
    <a-card title="Default size card">
        <h1>我是首页</h1>
        <a-space>
    <a-tooltip placement="topLeft" title="Prompt Text">
      <a-button>Align edge / 边缘对齐</a-button>
    </a-tooltip>
    <a-tooltip placement="topLeft" title="Prompt Text" arrow-point-at-center>
      <a-button>Arrow points to center / 箭头指向中心</a-button>
    </a-tooltip>
  </a-space>
  <div>
    <a-button type="primary" @click="showModal">Open Modal</a-button>
    <a-modal v-model:open="open" title="Basic Modal" @ok="handleOk">
      <p>Some contents...</p>
      <p>Some contents...</p>
      <p>Some contents...</p>
    </a-modal>
  </div>
        <div>
            <font-awesome-icon icon="calendar" style="color:green" />
            <font-awesome-icon icon="fas  fa-camera" />
            <font-awesome-icon :icon="['far', 'square']" />
            <font-awesome-icon icon="far fa-square" />
            <font-awesome-icon :icon="['far', 'square-caret-right']" />
            <font-awesome-icon icon="far fa-square-caret-right" />
            <font-awesome-icon icon="far fa-creative-commons-nc-eu" />
            <font-awesome-icon icon="fab fa-accusoft" />
            <font-awesome-icon icon="fas  fa-camera" />
        </div>
        <jda-menu :menus="items"></jda-menu>
        <a-form :model="formState" :label-col="labelCol" :wrapper-col="wrapperCol">
    <a-form-item label="Activity name">
      <a-input v-model:value="formState.name" />
    </a-form-item>
    <a-form-item label="Instant delivery">
      <a-switch v-model:checked="formState.delivery" />
    </a-form-item>
    <a-form-item label="Activity type">
      <a-checkbox-group v-model:value="formState.type">
        <a-checkbox value="1" name="type">Online</a-checkbox>
        <a-checkbox value="2" name="type">Promotion</a-checkbox>
        <a-checkbox value="3" name="type">Offline</a-checkbox>
      </a-checkbox-group>
    </a-form-item>
    <a-form-item label="Resources">
      <a-radio-group v-model:value="formState.resource">
        <a-radio value="1">Sponsor</a-radio>
        <a-radio value="2">Venue</a-radio>
      </a-radio-group>
    </a-form-item>
    <a-form-item label="Activity form">
      <a-textarea v-model:value="formState.desc" />
    </a-form-item>
    <a-form-item :wrapper-col="{ span: 14, offset: 4 }">
      <a-button type="primary" @click="onSubmit">Create</a-button>
      <a-button style="margin-left: 10px">Cancel</a-button>
    </a-form-item>
  </a-form>
  </a-card>
</template>
<style lang="scss" scoped></style>