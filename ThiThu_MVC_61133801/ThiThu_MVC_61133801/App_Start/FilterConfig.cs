﻿using System.Web;
using System.Web.Mvc;

namespace ThiThu_MVC_61133801
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}