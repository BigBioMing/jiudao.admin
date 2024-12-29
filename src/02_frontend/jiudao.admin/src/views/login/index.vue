<template>
  <div class="login-container">
    <div class="login-form">
      <div class="login-header">
        <span class="login-title">
          <span class="login-title-name">JiuDao</span>
          <span class="login-title-suffix">后台管理系统</span>
        </span>
      </div>
      <a-form size="large" :model="form" :label-col="{ span: 3 }">
        <a-form-item>
          <a-input size="large" v-model:value="form.name" autocomplete>
            <template #prefix>
              <UserOutlined />
            </template>
          </a-input>
        </a-form-item>
        <a-form-item>
          <a-input size="large" type="password" v-model:value="form.pwd" autocomplete>
            <template #prefix>
              <LockOutlined />
            </template>
          </a-input>
        </a-form-item>
        <a-form-item>
          <a-button v-model:loading="loading" size="large" type="primary" style="width: 100%"
            @click="onSubmit">登录</a-button>
        </a-form-item>
      </a-form>
      <a-dropdown>
        <a class="ant-dropdown-link" @click.prevent>
          Hover me
          <DownOutlined />
        </a>
        <template #overlay>
          <a-menu>
            <a-menu-item key="0">
              <a target="_blank" rel="noopener noreferrer" href="http://www.alipay.com/">
                1st menu item
              </a>
            </a-menu-item>
            <a-menu-item key="1">
              <a target="_blank" rel="noopener noreferrer" href="http://www.taobao.com/">
                2nd menu item
              </a>
            </a-menu-item>
            <a-menu-divider />
            <a-menu-item key="3" disabled>3rd menu item（disabled）</a-menu-item>
          </a-menu>
        </template>
      </a-dropdown>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, reactive, toRefs, onMounted } from "vue";
import router from '@/router';
import { useGlobalStore } from "@/stores";
import { loginApi } from '@/apis/login/login'
// import { useStore } from "vuex";
// import { login } from "@/api/entry/entry";
// import { PageStyleUtils } from "@/core/index";

export default defineComponent({
  setup() {

    const globalStore = useGlobalStore();
    let form = reactive({
      name: "admin",
      pwd: "123456",
    });
    let loading = ref<Boolean>(false);

    // let store = useStore();

    // onMounted(() => {
    //   var theme: any = PageStyleUtils.InitTheme();
    //   PageStyleUtils.UpdateThemeColor(theme.color);
    // });

    return {
      form,
      loading,
      onSubmit: () => {
        loading.value = true;
        loginApi({ UserName: form.name, Password: form.pwd }).then((res: any) => {
          globalStore.setToken(res.token, res.expireTime);
          router.push("/");
        }).finally(() => {
          loading.value = false;
        })
      },
    };
  },
});
</script>
<style scoped lang="scss">
.login-container {
  //   background-image: url("../../../assets/login/login_bg.svg");
  background-color: rgba(0, 0, 0, $alpha: 0.6);
  height: 100%;
  width: 100%;
  overflow: hidden;

  .login-form {
    position: relative;
    top: 50%;
    width: 400px;
    margin: 0 auto;
    margin-top: -165px;

    .login-header {
      margin-bottom: 30px;
      text-align: center;

      .login-title {
        font-size: 38px;
        color: #fff;
        font-weight: 900;

        .login-title-name {
          margin-right: 10px;
        }

        .login-title-suffix {
          letter-spacing: 5px;
        }
      }
    }
  }
}
</style>