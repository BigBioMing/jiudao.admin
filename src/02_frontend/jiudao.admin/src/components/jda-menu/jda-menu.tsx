import { defineComponent, reactive, watch, type SetupContext } from "vue";

type JdaMenuPropsType = {
  menus: any[];
};

// /** 写法一 */
// export default function(){
//     return(
//         <div>我是自定义组件</div>
//     )
// }

/** 写法二 */
export default defineComponent({
  props: {
    menus: Array<any>,
    collapsed: {
      type: Boolean,
      default: false,
    },
    mode:{
        type:String,
        default:'inline'
    }
  },
  setup(props, context) {
    console.log("props:", props);
    console.log("props:", props.menus);
    console.log("context:", context);
    const state = reactive({
      collapsed: props.collapsed,
      selectedKeys: [],
      openKeys: [],
      preOpenKeys: [],
      mode: props.mode
    });
    // const toggleCollapsed = () => {
    //   state.collapsed = !state.collapsed;
    //   state.openKeys = state.collapsed ? [] : state.preOpenKeys;
    // };
    watch(
      () => state.openKeys,
      (_val, oldVal) => {
        state.preOpenKeys = oldVal;
      }
    );
    watch(
      () => props.collapsed,
      (_val, oldVal) => {
        state.collapsed = _val;
        state.openKeys = state.collapsed ? [] : state.preOpenKeys;
      }
    );
    watch(
      () => props.mode,
      (_val, oldVal) => {
        state.mode = _val;
      }
    );
    const creMenu = (subMenus: any[]) => {
      let menus = subMenus || [];
      let len = menus?.length || 0;
      let arr = [];
      for (let i = 0; i < len; i++) {
        let menu = menus[i];
        let children = menu.children || [];
        if (children.length > 0) {
          let subEl = creMenu(children);
          const slot = {
            title: () => {
              if (menu.icon) {
                return (
                  <>
                    <font-awesome-icon
                      v-model:icon={menu.icon}
                      class="anticon ant-menu-item-icon"
                    />
                    <span>{menu.label}</span>
                  </>
                );
              } else {
                return (
                  <>
                    <span>{menu.label}</span>
                  </>
                );
              }
            },
          };
          arr.push(
            <a-sub-menu v-model:key={menu.key} v-slots={slot}>
              {subEl}
            </a-sub-menu>
          );
        } else {
          if (menu.icon) {
            const slot = {
              icon: () => {
                return (
                  <>
                    <font-awesome-icon
                      v-model:icon={menu.icon}
                      class="anticon ant-menu-item-icon"
                    />
                  </>
                );
              },
            };
            arr.push(
              <a-menu-item v-model:key={menu.key} v-slots={slot}>
                {menu.label}
              </a-menu-item>
            );
          } else {
            arr.push(
              <a-menu-item v-model:key={menu.key}>{menu.label}</a-menu-item>
            );
          }
        }
      }

      return arr;
    };

    let menus = props.menus || [];
    let els = creMenu(menus);
    console.log("els:", els);
    let elstr = els.join();
    console.log("elstr:", elstr);
    return () => (
      <a-menu
        v-model:openKeys={state.openKeys}
        v-model:selectedKeys={state.selectedKeys}
        v-model:mode={state.mode}
        theme="dark"
      >
        {els}
      </a-menu>
    );
  },
});

// /** 写法三 */
// const renderDom = (props:JdaMenuPropsType)=>{
//     console.log("props:",props)
//     return(<>
//     <div>我是自定义组件</div>
//     </>)
// }
// export default renderDom
