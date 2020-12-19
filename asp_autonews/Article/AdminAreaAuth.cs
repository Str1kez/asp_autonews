using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace asp_autonews.Article
{
    public class AdminAreaAuth : IControllerModelConvention
    {
        private readonly string area;
        private readonly string policy;

        public AdminAreaAuth(string area, string policy)
        {
            this.area = area;
            this.policy = policy;
        }

        public void Apply(ControllerModel controller)
        {
            // здесь проверяем атрибуты контроллера
            // Если есть area атрибут, то отправляем пользователя на авторизацию
            if (controller.Attributes.Any(a =>
                    a is AreaAttribute && (a as AreaAttribute).RouteValue.Equals(area, StringComparison.OrdinalIgnoreCase))
                || controller.RouteValues.Any(m => m.Key.Equals("area", StringComparison.OrdinalIgnoreCase)
                                                   && m.Value.Equals(area, StringComparison.OrdinalIgnoreCase)))
            {
                controller.Filters.Add(new AuthorizeFilter(policy));
            }
        }
    }
}