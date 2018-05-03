using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETT.Web.Models
{
    public class PageViewModel
    {
        public int PageNumber { get; private set; }     // active page
        public int TotalPages { get; private set; }     // amount of all pages

        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
             TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }

        public bool ExistPage(int pagenumber)
        {
            if (pagenumber < 1) return false;
            if (pagenumber > TotalPages) return false;
            return true;
        }
    }
}
