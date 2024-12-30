import { defineStore } from "pinia";

export const useCrumbStore= defineStore('crumb',{
    state:()=>{
        return{
            crumbs :[]
        }
    },
    getters:{
        currentCrumbs:()=>{
            
        }
    }
})