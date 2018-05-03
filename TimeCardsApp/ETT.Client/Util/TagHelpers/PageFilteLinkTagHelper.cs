using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETT.Web.Util.TagHelpers
{
    public class PageFilterLinkTagHelper : PageLinkTagHelper
    {
        public string PageFilter { get; set; }          // filter string

        public PageFilterLinkTagHelper(IUrlHelperFactory urlHelperFactory)
            : base(urlHelperFactory) { }

        protected override object GetValuesForAction(int page)
        {
            return new { page = page, PageFilter = PageFilter };
        }
    }
}
