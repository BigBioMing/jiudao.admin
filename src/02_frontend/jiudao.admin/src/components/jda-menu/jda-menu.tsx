import { defineComponent, reactive, type SetupContext } from "vue"


type JdaMenuPropsType={
    menus:any[]
}


// /** 写法一 */
// export default function(){
//     return(
//         <div>我是自定义组件</div>
//     )
// }

/** 写法二 */
export default defineComponent({
    props:{
        menus:Array<any>
    },
    setup(props,context) {
        console.log("props:",props)
        console.log("props:",props.menus)
        console.log("context:",context)
        const state = reactive({
          collapsed: false,
          selectedKeys: ['1'],
          openKeys: ['sub1'],
          preOpenKeys: ['sub1'],
        });
const a = (menu:any)=>{
    return menu.icon
}
        const creMenu=(subMenus:any[])=>{
            let menus = subMenus||[]
            let len = menus?.length||0
            let arr = [];
            for(let i=0;i<len;i++){
                let menu = menus[i]
                let children = menu.children||[]
                if(children.length>0){
                    let subEl = creMenu(children)
                    const slot = {
                        title:()=>{
                            return(
                                <>
            <span>
              {a(menu)}
              <span>{menu.label}</span>
            </span>
                                </>
                            )
                        }
                    }
                    arr.push(<a-sub-menu v-model:key={menu.key} v-slots={slot}>
                        
          {subEl}
                    </a-sub-menu>)
                }else{
                    arr.push(<a-menu-item v-model:key={menu.key}>{menu.label}</a-menu-item>)
                }
            }

            return arr
        }

        let menus = props.menus||[]
        let els = creMenu(menus)
        console.log('els:',els)
        let elstr = els.join()
        console.log('elstr:',elstr)
        return () => (<a-menu v-model:openKeys={state.openKeys} v-model:selectedKeys={state.selectedKeys} mode="inline" theme="dark"
        >{els}</a-menu>)
    }
})



// /** 写法三 */
// const renderDom = (props:JdaMenuPropsType)=>{
//     console.log("props:",props)
//     return(<>
//     <div>我是自定义组件</div>
//     </>)
// }
// export default renderDom