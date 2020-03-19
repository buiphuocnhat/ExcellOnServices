using AutoMapper;
using SaleECS.Models;
using SaleECS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;


namespace SaleECS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Mapper.Initialize(map =>
            {
                map.CreateMap<EmployeeUpdateViewModel, Employee>();
                map.CreateMap<Employee, EmployeeUpdateViewModel>();
                map.CreateMap<ChangePasswordViewModel, Employee>().ForMember(dich => dich.Password, tuyChon => tuyChon.MapFrom(nguon => nguon.ConfirmNewPassword));
                map.CreateMap<Employee, ChangePasswordViewModel>();
                map.CreateMap<RegisterViewModel, Employee>();
                map.CreateMap<Employee, RegisterViewModel>();
            });
        }
    }
}
