import { createPinia } from "pinia";

const pinia = createPinia();
export default pinia;

export * from "./modules/global";
export * from "./modules/menus";
export * from "./modules/crumb";
export * from "./modules/loading";
