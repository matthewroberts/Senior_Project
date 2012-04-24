using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMREC.Core.DependencyInjection;
using EMREC.Core.Domain.Interfaces;
using EMREC.Core.Domain.Models;

namespace EMREC.Web.qc
{
    public partial class Default1 : System.Web.UI.Page
    {
        private IDocumentService _documentService;
        private ISearchService _searchService;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeServices();
            if (Request.QueryString["success"] == "true")
                notificationsuccess.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "block");

            Search();
            if (Page.IsPostBack)
                return;
        }

        private void InitializeServices()
        {
            _documentService = DI.CreateDocumentService();
            _searchService = DI.CreateSearchService();
        }

        protected void Search()
        {
            List<Document> searchResults;
            try
            {
                searchResults = _documentService.GetOrphanedDocuments();
            }
            catch
            {
                return;
            }

            gvOrphan.DataSource = searchResults;
            gvOrphan.DataBind();
            Session["gvTable"] = ToDataTable(searchResults);

            if (gvOrphan.HeaderRow != null)
            {
                gvOrphan.UseAccessibleHeader = true;
                gvOrphan.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvOrphan.HeaderRow.CssClass = "sorting";
                gvOrphan.RowStyle.CssClass = "gradeC odd";
                gvOrphan.AlternatingRowStyle.CssClass = "gradeC even";
            }
        }

        protected void GridViewPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrphan.DataSource = Session["gvTable"] as DataTable;
            gvOrphan.PageIndex = e.NewPageIndex;
            gvOrphan.DataBind();
            if (gvOrphan.HeaderRow != null)
            {
                gvOrphan.UseAccessibleHeader = true;
                gvOrphan.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvOrphan.HeaderRow.CssClass = "sorting";
                gvOrphan.RowStyle.CssClass = "gradeC odd";
                gvOrphan.AlternatingRowStyle.CssClass = "gradeC even";
            }
        }

        protected void GridViewSorting(object sender, GridViewSortEventArgs e)
        {
            var dt = Session["gvTable"] as DataTable;
            if (dt != null)
            {
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);

                gvOrphan.DataSource = dt;
                gvOrphan.DataBind();
                Session["gvTable"] = dt;
                if (gvOrphan.HeaderRow != null)
                {
                    gvOrphan.UseAccessibleHeader = true;
                    gvOrphan.HeaderRow.TableSection = TableRowSection.TableHeader;
                    gvOrphan.HeaderRow.CssClass = "sorting";
                    gvOrphan.RowStyle.CssClass = "gradeC odd";
                    gvOrphan.AlternatingRowStyle.CssClass = "gradeC even";
                }
            }
        }

        private string GetSortDirection(string column)
        {
            var sortDirection = "ASC";
            var sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    var lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }

                }
            }
            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        public static DataTable ToDataTable<T>(IList<T> data)
        {
            var props = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            for (var i = 0; i < props.Count; i++)
            {
                var prop = props[i];
                var propType = prop.PropertyType;
                if (propType.IsGenericType &&
                    propType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    propType = Nullable.GetUnderlyingType(propType);
                }
                table.Columns.Add(prop.Name, propType);
            }
            var values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}