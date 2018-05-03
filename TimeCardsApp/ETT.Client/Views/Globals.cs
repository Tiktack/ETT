using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Threading.Tasks;

namespace ETT.Web.Views
{
    public class Globals<TModel> : RazorPage<TModel>
    {
        public class Properties
        {
            public static string Logo = "ETT";
        }

        public class Design
        {
            public static string Color = "blue";
            public static string ColorAdmin = "black";
        }

        public override Task ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
