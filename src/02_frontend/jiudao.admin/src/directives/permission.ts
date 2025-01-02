import { useMenuStore } from "@/stores";
import { type App, type DirectiveBinding } from "vue";

const permissionDirective = (el: any, binding: DirectiveBinding) => {
  const menuStore = useMenuStore();

  const { value, modifiers } = binding;
  const actions = menuStore.getActions();

  let hasPermission = Array.isArray(value)
    ? value.some((action) => actions.includes(action))
    : actions.includes(value);

  if (!hasPermission) {
    if (modifiers.remove) {
      el.parentElement && el.parentElement.removeChild(el);
    } else if (modifiers.disabled) {
      el.disabled = true;
      //   el.classList.add("jda-disabled");
    } else if (modifiers.hidden) {
      el.style.display = "none";
    } else {
      el.parentElement && el.parentElement.removeChild(el);
    }
  }
};

export default {
  install(app: App) {
    app.directive("permission", permissionDirective);
  },
};
