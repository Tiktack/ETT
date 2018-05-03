using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ETT.Web.Models;
using ETT.Web.Models.Users;
using Microsoft.AspNetCore.Html;

// https://metanit.com/sharp/aspnet5/12.8.php
// http://materializecss.com/pagination.html

namespace ETT.Web.Util.TagHelpers
{
    public class PageLinkTagHelper : TagHelper
    {
        #region private fields
        private enum Arrows
        {
            left,
            leftff,
            right,
            rightff,
            nope
        }

        private int anchorCount;

        private IUrlHelperFactory urlHelperFactory;
        #endregion

        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
            string html = "<div class=\"sda\">";
            HtmlString str;
        }

        #region properties
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PageViewModel PageModel { get; set; }    // model for pagination

        public string PageAction { get; set; }          // action        

        public int AnchorCount                          // amount of pagination elements
        {
            get { return anchorCount; }
            set
            {
                if (value < 3)
                {
                    anchorCount = 3;
                    return;
                }

                if (value > 20)
                {
                    anchorCount = 20;
                    return;
                }

                anchorCount = value;
            }
        }
        #endregion

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";

            // create <ul>
            TagBuilder tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");

            if (anchorCount > PageModel.TotalPages) anchorCount = PageModel.TotalPages;

            // left fast forward arrow
            tag.InnerHtml.AppendHtml(CreateArrowTag(PageModel.PageNumber - AnchorCount, urlHelper, Arrows.leftff));

            // left arrow
            tag.InnerHtml.AppendHtml(CreateArrowTag(PageModel.PageNumber - 1, urlHelper, Arrows.left));

            // numbers
            int aCount = anchorCount / 2;
            int actualEndPage = (
                PageModel.ExistPage(PageModel.PageNumber + aCount)
                ? PageModel.PageNumber + aCount
                : PageModel.TotalPages);
            int actualStartPage = actualEndPage - aCount * 2;

            while (!PageModel.ExistPage(actualStartPage))
            {
                actualStartPage++;
                if (PageModel.ExistPage(actualEndPage + 1)) actualEndPage++;
            }
            for (int i = actualStartPage; i <= actualEndPage; i++)
            {
                TagBuilder currentItem = CreateTag(i, urlHelper);
                tag.InnerHtml.AppendHtml(currentItem);
            }

            // right arrow
            tag.InnerHtml.AppendHtml(CreateArrowTag(PageModel.PageNumber + 1, urlHelper, Arrows.right));

            // right fast forward arrow
            tag.InnerHtml.AppendHtml(CreateArrowTag(PageModel.PageNumber + AnchorCount, urlHelper, Arrows.rightff));

            output.Content.AppendHtml(tag);
        }

        /// <summary>
        /// Сreates the object necessary for URL parameters
        /// </summary>
        /// <param name="page">Page to create a link</param>
        /// <returns>Parameters of URL</returns>
        protected virtual object GetValuesForAction(int page)
        {
            return new { page = page };
        }

        private TagBuilder CreateArrowTag(int pageNumber, IUrlHelper urlHelper, Arrows arrow)
        {
            TagBuilder tag = null;
            if (PageModel.ExistPage(pageNumber))
            {
                tag = CreateTag(pageNumber, urlHelper, arrow);
            }
            else
            {
                tag = CreateTag(0, urlHelper, arrow, false);
            }

            return tag;
        }

        private TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper, Arrows arrows = Arrows.nope, bool Enabled = true)
        {
            // create <li> and <a>
            TagBuilder item = new TagBuilder("li");
            item.AddCssClass("waves-effect");
            TagBuilder link = new TagBuilder("a");
            if (pageNumber == this.PageModel.PageNumber)
            {
                item.AddCssClass("active");
            }
            else
            {
                link.Attributes["href"] = urlHelper.Action(PageAction, GetValuesForAction(pageNumber));
            }

            // arrow
            TagBuilder icon1 = null;
            TagBuilder icon2 = null;
            if (arrows != Arrows.nope)
            {
                icon1 = new TagBuilder("i");
                icon2 = new TagBuilder("i");
                icon1.AddCssClass("material-icons");
                icon2.AddCssClass("material-icons");
                icon1.Attributes["style"] = "margin-left:-10px; margin-right:-10px";
                icon2.Attributes["style"] = "margin-left:-13px; margin-right:-10px";

                switch (arrows)
                {
                    case Arrows.left:
                        icon1.InnerHtml.Append("chevron_left");
                        break;
                    case Arrows.leftff:
                        icon1.InnerHtml.Append("chevron_left");
                        icon2.InnerHtml.Append("chevron_left");
                        break;
                    case Arrows.right:
                        icon1.InnerHtml.Append("chevron_right");
                        break;
                    case Arrows.rightff:
                        icon1.InnerHtml.Append("chevron_right");
                        icon2.InnerHtml.Append("chevron_right");
                        break;
                }

                link.InnerHtml.AppendHtml(icon1);
                if (arrows == Arrows.leftff || arrows == Arrows.rightff)
                    link.InnerHtml.AppendHtml(icon2);
            }

            // enable or not
            if (Enabled)
            {
                link.InnerHtml.Append(arrows == Arrows.nope ? pageNumber.ToString() : "");
                item.InnerHtml.AppendHtml(link);
            }
            else
            {
                link.Attributes["href"] = "#!";
                item.AddCssClass("disabled");
                item.InnerHtml.AppendHtml(link);
            }

            return item;
        }
    }
}
