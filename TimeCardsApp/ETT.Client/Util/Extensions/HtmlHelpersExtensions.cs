using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ETT.Web.Util.Extensions
{
    public static class HtmlHelpersExtensions
    {

        /// <summary>
        /// Generate correct materialize checkbox for single boolean value
        /// </summary>
        /// <param name="title">Title of the checkbox</param>
        /// <param name="nameOfItem">The name of the property to use checkbox</param>
        /// <param name="isChecked">Parameter characterizing the state of chekbox</param>
        /// <returns></returns>
        public static HtmlString CheckBoxOnceMaterialize(this IHtmlHelper html, string title, string nameOfItem, bool isChecked = false)
        {
            TagBuilder label = GetLabel(title, nameOfItem, new string[] { "black-text" });
            TagBuilder checkbox = GetCheckBox(nameOfItem, nameOfItem, isChecked, false, new string[] { "filled-in" });

            return new HtmlString(GetHtmlString(checkbox, label));
        }

        /// <summary>
        ///  Generate correct materialize checkbox for list of items
        /// </summary>
        /// <param name="title">Title of the checkbox</param>
        /// <param name="nameOfItem">The name of the property to use checkbox</param>
        /// <param name="groupName">The name of the group</param>
        /// <param name="isChecked">Parameter characterizing the state of chekbox</param>
        /// <returns></returns>
        public static HtmlString CheckBoxMultiMaterialize(this IHtmlHelper html, string title, string nameOfItem, string groupName, bool isChecked = false)
        {
            TagBuilder label = GetLabel(title, nameOfItem, new string[] { "black-text" });
            TagBuilder checkbox = GetCheckBox(nameOfItem, groupName, isChecked, true, new string[] { "filled-in" });

            return new HtmlString(GetHtmlString(checkbox, label));
        }

        /// <summary>
        /// Generate correct materialize checkbox for single boolean value
        /// </summary>
        /// <param name="title">Title of the checkbox</param>
        /// <param name="nameOfItem">The name of the property to use checkbox</param>
        /// <param name="isChecked">Parameter characterizing the state of chekbox</param>
        /// <param name="stylesForLabel"></param>
        /// <param name="stylesForCheckbox"></param>
        /// <returns></returns>
        public static HtmlString CheckBoxOnceMaterialize(this IHtmlHelper html, string title, string nameOfItem, bool isChecked, string[] stylesForLabel, string[] stylesForCheckbox)
        {
            TagBuilder label = GetLabel(title, nameOfItem, stylesForLabel);
            TagBuilder checkbox = GetCheckBox(nameOfItem, nameOfItem, isChecked, false, stylesForCheckbox);

            return new HtmlString(GetHtmlString(checkbox, label));
        }

        /// <summary>
        /// Generate correct materialize checkbox for list of items
        /// </summary>
        /// <param name="title">Title of the checkbox</param>
        /// <param name="nameOfItem">The name of the property to use checkbox</param>
        /// <param name="groupName">The name of the group</param>
        /// <param name="isChecked">Parameter characterizing the state of chekbox</param>
        /// <param name="stylesForLabel"></param>
        /// <param name="stylesForCheckbox"></param>
        /// <returns></returns>
        public static HtmlString CheckBoxMultiMaterialize(this IHtmlHelper html, string title, string nameOfItem, string groupName, bool isChecked, string[] stylesForLabel, string[] stylesForCheckbox)
        {
            TagBuilder label = GetLabel(title, nameOfItem, stylesForLabel);
            TagBuilder checkbox = GetCheckBox(nameOfItem, groupName, isChecked, true, stylesForCheckbox);

            return new HtmlString(GetHtmlString(checkbox, label));
        }

        private static TagBuilder GetLabel(string title, string nameOfItem, params string[] styles)
        {
            TagBuilder titl = new TagBuilder("label");
            titl.InnerHtml.Append(title);
            titl.Attributes.Add("for", nameOfItem);
            foreach (var item in styles)
            {
                titl.AddCssClass(item);
            }
            return titl;
        }

        private static TagBuilder GetCheckBox(string nameOfItem, string groupName, bool isChecked = false, bool exValue = false, params string[] styles)
        {
            TagBuilder input = new TagBuilder("input");
            input.Attributes.Add("type", "checkbox");
            input.Attributes.Add("name", groupName);
            input.Attributes.Add("id", nameOfItem);
            if (!exValue)
            {
                input.Attributes.Add("value", "true");
            }
            else
            {
                input.Attributes.Add("value", nameOfItem);
            }
            foreach (var item in styles)
            {
                input.AddCssClass(item);
            }
            if (isChecked)
            {
                input.Attributes.Add("checked", "");
            }
            return input;
        }

        private static string GetHtmlString(params TagBuilder[] tagBuilders)
        {
            var writer = new System.IO.StringWriter();
            foreach (var item in tagBuilders)
            {
                item.WriteTo(writer, HtmlEncoder.Default);
            }
            return writer.ToString();
        }

    }
}
