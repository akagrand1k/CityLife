using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CityLife.WebApp.Models.Abstract
{
    public abstract class AbstractViewModel
    {
        public AbstractViewModel()
        {            
            CountOnPage = 30;
            NumPage = 1;
            FieldOrderBy = "Id";
            IsAscending = true;
            IsDelete = false;

        }

        public int? Id { get; set; }

        public bool IsDelete { get; set; }

        public bool IsActive { get; set; }

        public int CountTotal { get; set; }

        public int CountOnPage { get; set; }

        public int NumPage { get; set; }

        public string FieldOrderBy { get; set; }

        public bool IsAscending { get; set; }

        public Func<string, bool> IsInRole { get; set; }


        #region DataTable

        public string[] сolumnName { get; set; }

        public int start { get; set; }

        public int length { get; set; }

        public int draw { get; set; }


        public virtual void InitSortingData()
        {
            var httpContext = HttpContext.Current;
            if (httpContext != null)
            {
                if (httpContext.Request.Params["order[0][column]"] != null && httpContext.Request.Params["order[0][dir]"] != null)
                {
                    Int32 indexColumn = -1;
                    Int32.TryParse(httpContext.Request.Params["order[0][column]"], out indexColumn);
                    if (indexColumn > -1 && сolumnName != null && сolumnName.Length > indexColumn
                        && !String.IsNullOrEmpty(сolumnName[indexColumn]))
                    {
                        FieldOrderBy = сolumnName[indexColumn];
                        IsAscending = httpContext.Request.Params["order[0][dir]"] != null && httpContext.Request.Params["order[0][dir]"] == "asc" ? true : false;
                    }

                }

                if (length != 0)
                {
                    NumPage = (length + start) / length;
                    CountOnPage = length;
                }
                else
                {
                    NumPage = 1;
                    CountOnPage = 100;
                }

            }

        }

        #endregion

    }
}
