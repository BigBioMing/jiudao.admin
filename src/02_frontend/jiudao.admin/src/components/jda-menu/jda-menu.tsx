import {
  defineComponent,
  reactive,
  toRaw,
  watch,
  type SetupContext,
} from "vue";

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
    menus: {
      type: Array<any>,
      default: () => [],
    },
    collapsed: {
      type: Boolean,
      default: false,
    },
    mode: {
      type: String,
      default: "inline",
    },
    theme: {
      type: String,
      default: "dark",
    },
  },
  emits: ["on-menu-item-click"],
  setup(props, context) {
    const state = reactive({
      menus: toRaw(props.menus),
      collapsed: props.collapsed,
      selectedKeys: [],
      openKeys: [],
      preOpenKeys: [],
      mode: props.mode,
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
    watch(
      () => props.menus,
      (_val, oldVal) => {
        state.menus = toRaw(_val);
      }
    );

    /**
     * 菜单点击事件
     * @param menu 菜单对象
     * @param isLeaf 是否叶子节点 true-是
     */
    const onMenuItemClick = (menu: any, isLeaf: boolean) => {
      context.emit("on-menu-item-click", menu, isLeaf);
    };

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
            <a-sub-menu
              v-model:key={menu.key}
              v-slots={slot}
              onClick={() => onMenuItemClick(menu, false)}
            >
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
              <a-menu-item
                v-model:key={menu.key}
                v-slots={slot}
                onClick={() => onMenuItemClick(menu, true)}
              >
                {menu.label}
              </a-menu-item>
            );
          } else {
            arr.push(
              <a-menu-item
                v-model:key={menu.key}
                onClick={() => onMenuItemClick(menu, true)}
              >
                {menu.label}
              </a-menu-item>
            );
          }
        }
      }

      return arr;
    };

    let els = creMenu(state.menus);
    return () => (
      <a-menu
        v-model:openKeys={state.openKeys}
        v-model:selectedKeys={state.selectedKeys}
        v-model:mode={state.mode}
        v-model:theme={props.theme}
      >
        {creMenu(state.menus)}
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
