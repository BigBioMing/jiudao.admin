<template>
  <div class="farmer-error farmer-404">
    <a-result status="404" title="404" sub-title="抱歉，您访问的页面不存在。">
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
  name: "error.404",
  setup() {
    const router = useRouter();
    const route = useRoute();

    const toBack = () => {
      router.go(-1);
    };

    return {
      toBack,
    };
  },
});
</script>
