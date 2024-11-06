using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDA.Core.ModelBinds
{
    public class CustomModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            ExpandoObject @params = null;
            var qs = bindingContext?.HttpContext?.Request?.QueryString.Value;

            if (string.Equals(bindingContext.HttpContext.Request.Method, "Get", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrWhiteSpace(qs))
            {
                var paramses = qs.Split("&");

                foreach (var item in paramses)
                {
                    var items = item.Split("=");
                    string key = items[0];
                    object? value = null;
                    if (item.Length > 1) value = items[1];

                    var childKeys = key.Split(".");
                    if (childKeys[0] == "params" || childKeys[0] == "Params")
                    {
                        if (value == null) continue;
                        if (childKeys.Length > 1)
                        {
                            if (@params == null) @params = new ExpandoObject();
                            for (int i = 1; i < childKeys.Length; i++)
                            {
                                @params.TryAdd(childKeys[1], value);
                            }
                        }
                    }
                }
            }

            if (@params != null)
            {
                bindingContext.Model = @params;
                bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            }

            return Task.CompletedTask;
        }
    }
}
