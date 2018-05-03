using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETT.Web.Util.Expanders
{
    public class ProjectViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            // This is where we tell MVC where to look for our files.
            // This says to look for a file at "Views/Project/Controller/Action.cshtml"
            return viewLocations.Concat(new[] {
                "~/Views/Project/{1}/{0}.cshtml"
            });
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            // Nothing to do here.
        }
    }
}
