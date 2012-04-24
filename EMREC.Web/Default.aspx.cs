using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EMREC.Core.DependencyInjection;
using EMREC.Core.Domain.Interfaces;
using EMREC.Core.Domain.Models;
using EMREC.Core.Infrastructure.Mappings;

namespace EMREC.Web
{
    public partial class Default1 : System.Web.UI.Page
    {
        private IDocumentService _documentService;
        private ISearchService _searchService;

        protected void Page_Load(object sender, EventArgs e)
        {

            InitializeServices();
            if (!IsPostBack)
            {
                activeTab.Value = "ABN";
            }

        }

        private void InitializeServices()
        {
            _documentService = DI.CreateDocumentService();
            _searchService = DI.CreateSearchService();
        }

        protected void Search(object sender, EventArgs e)
        {
            notification.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
            reports.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
            abnHead.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
            labsHead.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
            xrayHead.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
            refHead.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
            otherHead.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");

            List<Document> searchResults;
            tblPatient patient = null;
            try
            {
                searchResults = _documentService.GetDocumentsByPatientId(Convert.ToInt32(tbpatientid.Text));
                patient = _searchService.GetPatient(Convert.ToInt32(tbpatientid.Text));
            }
            catch
            {
                errorMsg.Text = "An error occured, please check to make sure Patient Id is in the correct format.";
                notification.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "block");
                return;
            }
            if (patient == null)
            {
                notification.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "block");
                errorMsg.Text = "Patient Not found!";
                return;
            }

            lblPatientName.Text = patient.PatientFirstName + " " + patient.PatientLastName;
            lblDOB.Text = patient.DOB.ToShortDateString();
            lblAddress.Text = patient.Address1 + ", " + patient.City + ", " + patient.State + " " + patient.Zip;
            lblPhone.Text = patient.Phone;
            reports.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "block");
            gvABN.DataSource = searchResults.FindAll(d => d.DocumentType.TypeId == 0);
            gvABN.DataBind();
            SetTableStyle();
            
            gvLabs.DataSource = searchResults.FindAll(d => d.DocumentType.TypeId == 1);
            gvLabs.DataBind();
            SetTableStyle();
            
            gvXRays.DataSource = searchResults.FindAll(d => d.DocumentType.TypeId == 4);
            gvXRays.DataBind();
            SetTableStyle();
            
            gvReferrals.DataSource = searchResults.FindAll(d => d.DocumentType.TypeId == 3);
            gvReferrals.DataBind();
            SetTableStyle();

            gvOther.DataSource = searchResults.FindAll(d => d.DocumentType.TypeId == 5);
            gvOther.DataBind();
            SetTableStyle();

        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            var searchResults = _documentService.GetDocumentsByPatientId(Convert.ToInt32(tbpatientid.Text));
            if (sender.Equals(gvABN))
            {
                gvABN.PageIndex = e.NewPageIndex;
                gvABN.DataSource = searchResults.FindAll(d => d.DocumentType.TypeId == 0);
                gvABN.DataBind();
                SetTableStyle();
            }
            if (sender.Equals(gvLabs))
            {
                gvLabs.PageIndex = e.NewPageIndex;
                gvLabs.DataSource = searchResults.FindAll(d => d.DocumentType.TypeId == 1);
                gvLabs.DataBind();
                SetTableStyle();
            }
            if (sender.Equals(gvOther))
            {
                gvOther.PageIndex = e.NewPageIndex;
                gvOther.DataSource = searchResults.FindAll(d => d.DocumentType.TypeId == 5);
                gvOther.DataBind();
                SetTableStyle();
            }
            if (sender.Equals(gvReferrals))
            {
                gvReferrals.PageIndex = e.NewPageIndex;
                gvReferrals.DataSource = searchResults.FindAll(d => d.DocumentType.TypeId == 3);
                gvReferrals.DataBind();
                SetTableStyle();
            }
            if (sender.Equals(gvXRays))
            {
                gvXRays.PageIndex = e.NewPageIndex;
                gvXRays.DataSource = searchResults.FindAll(d => d.DocumentType.TypeId == 4);
                gvXRays.DataBind();
                SetTableStyle();
            }
        }

        protected void gridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            var searchResults = _documentService.GetDocumentsByPatientId(Convert.ToInt32(tbpatientid.Text));
            if (sender.Equals(gvABN))
            {
                var dataView = searchResults.FindAll(d => d.DocumentType.TypeId == 0);
                var dt = ToDataTable(dataView);
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);

                gvABN.DataSource = dt;
                gvABN.DataBind();
                SetTableStyle();
            }
            if (sender.Equals(gvLabs))
            {
                var dataView = searchResults.FindAll(d => d.DocumentType.TypeId == 1);
                var dt = ToDataTable(dataView);
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);

                gvLabs.DataSource = dt;
                gvLabs.DataBind();
                SetTableStyle();
            }
            if (sender.Equals(gvOther))
            {
                var dataView = searchResults.FindAll(d => d.DocumentType.TypeId == 5);
                var dt = ToDataTable(dataView);
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);

                gvOther.DataSource = dt;
                gvOther.DataBind();
                SetTableStyle();
            }
            if (sender.Equals(gvReferrals))
            {
                var dataView = searchResults.FindAll(d => d.DocumentType.TypeId == 3);
                var dt = ToDataTable(dataView);
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);

                gvReferrals.DataSource = dt;
                gvReferrals.DataBind();
                SetTableStyle();
            }
            if (sender.Equals(gvXRays))
            {
                var dataView = searchResults.FindAll(d => d.DocumentType.TypeId == 4);
                var dt = ToDataTable(dataView);
                dt.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);

                gvXRays.DataSource = dt;
                gvXRays.DataBind();
                SetTableStyle();
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

        public void SetTableStyle()
        {
            if (gvABN.HeaderRow != null)
            {
                gvABN.UseAccessibleHeader = true;
                gvABN.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvABN.HeaderRow.CssClass = "sorting";
                gvABN.RowStyle.CssClass = "gradeA odd";
                gvABN.AlternatingRowStyle.CssClass = "gradeA even";
            }
            else
            {
                abnHead.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "block");
            }
            
            if (gvLabs.HeaderRow != null)
            {
                gvLabs.UseAccessibleHeader = true;
                gvLabs.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvLabs.HeaderRow.CssClass = "sorting";
                gvLabs.RowStyle.CssClass = "gradeA odd";
                gvLabs.AlternatingRowStyle.CssClass = "gradeA even";
            }
            else
            {
                labsHead.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "block");
            }
            
            if (gvXRays.HeaderRow != null)
            {
                gvXRays.UseAccessibleHeader = true;
                gvXRays.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvXRays.HeaderRow.CssClass = "sorting";
                gvXRays.RowStyle.CssClass = "gradeA odd";
                gvXRays.AlternatingRowStyle.CssClass = "gradeA even";
            }
            else
            {
                xrayHead.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "block");
            }


            if (gvReferrals.HeaderRow != null)
            {
                gvReferrals.UseAccessibleHeader = true;
                gvReferrals.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvReferrals.HeaderRow.CssClass = "sorting";
                gvReferrals.RowStyle.CssClass = "gradeA odd";
                gvReferrals.AlternatingRowStyle.CssClass = "gradeA even";
            }
            else
            {
                refHead.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "block");
            }

            if (gvOther.HeaderRow != null)
            {
                gvOther.UseAccessibleHeader = true;
                gvOther.HeaderRow.TableSection = TableRowSection.TableHeader;
                gvOther.HeaderRow.CssClass = "sorting";
                gvOther.RowStyle.CssClass = "gradeA odd";
                gvOther.AlternatingRowStyle.CssClass = "gradeA even";
            }
            else
            {
                otherHead.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "block");
            }
            
        }

    }
}