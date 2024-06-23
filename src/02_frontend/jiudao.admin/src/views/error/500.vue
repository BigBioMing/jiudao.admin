<template>
  <div class="farmer-error">
    <a-result status="500" title="500" sub-title="抱歉，服务器发生错误。">
      <template #extra>
        <a-button type="primary" @click="toBack">返回</a-button>
      </template>
    </a-result>
  </div>
</template>
<script lang="ts">
import { defineComponent } from "vue";
import { useRouter, useRoute } from "vue-router";

export default defineComponent({
  setup() {
    const router = useRouter();
    const route = useRoute();

    const toBack = () => {
      router.go(-1);
    };

    return{
        toBack
    }
  },
});
</script>
