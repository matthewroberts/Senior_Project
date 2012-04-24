using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EMREC.Core.DependencyInjection;
using EMREC.Core.Domain.Interfaces;
using EMREC.Core.Domain.Models;
using EMREC.Core.Infrastructure.Mappings;

namespace EMREC.Web.qc
{
    public partial class UpdateDocument : System.Web.UI.Page
    {
        private string _documentId;
        private string _documentName;
        private ISearchService _searchService;
        private IQCService _qcService;
        private IDocumentService _documentService;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeServices();
            _documentId = Request.QueryString["id"];
            _documentName = Request.QueryString["name"];
            tbpatientid.Focus();
            LoadViewer();
            if (Page.IsPostBack)
                return;

            lblDocName.Text = _documentName;
            LoadDocumentTypes(sender, e);
        }

        private void InitializeServices()
        {
            _searchService = DI.CreateSearchService();
            _qcService = DI.CreateQCService();
            _documentService = DI.CreateDocumentService();
        }

        protected void UpdateOrphanDocument(object sender, EventArgs e)
        {
            if (ddlDocType.SelectedIndex == 0 || tbpatientid.Text == "")
            {
                lblError.Text = "You must fill in all the required fields.";
                notificationerror.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "block");
                return;
            }

            var visit = new Visit{Date = ddlVisitDate.SelectedItem.Text,VisitId = Convert.ToInt32(ddlVisitDate.SelectedValue)};
            var type = new DocumentType {Type = ddlDocType.SelectedItem.Text, TypeId = Convert.ToInt32(ddlDocType.SelectedValue)};
            try
            {
                _qcService.UpdateOrphanDocument(Convert.ToInt32(_documentId), visit, ddlChartId.SelectedItem.Text, type, tbDocDescription.Text);

            }
            catch (Exception ex)
            {
                lblError.Text = "An error occured, please contact an administrator.  Error: " + ex.Message;
                notificationerror.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "block");
                return;
            }

            if (sender == bupdate)
            {
                Response.Redirect("Default.aspx?success=true");
            }
            else
            {
                UpdateOrphanDocumentAndGoToNext(sender, e);
            }

        }

        private void LoadDocumentTypes(object sender, EventArgs eventArgs)
        {
            var documentTypes = _searchService.GetDocumentTypes();
            documentTypes.Add(new DocumentType { Type = "", TypeId = -1 });
            ddlDocType.DataTextField = "Type";
            ddlDocType.DataValueField = "TypeId";
            ddlDocType.DataSource = documentTypes.OrderBy(p => p.Type).ToList();
            ddlDocType.DataBind();
        }

        protected void LoadPatientData(object sender, EventArgs e)
        {
            ddlVisitDate.Items.Clear();
            ddlChartId.Items.Clear();

            tblPatient patient = null;
            try
            {
                patient = DI.CreateSearchService().GetPatient(Convert.ToInt32(tbpatientid.Text));  
            }
            catch
            {
                lblError.Text = "An error occured, please check to make sure Patient Id is in the correct format.";
                notificationerror.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "block");
                return;
            }

            if (patient == null)
            {
                lblError.Text = "Patient not found!";
                notificationerror.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "block");
                return;
            }

            var visits = _searchService.GetPatientVisits(patient.PatientID);
            var chartId = _searchService.GetPatientChart(tbpatientid.Text);

            ddlVisitDate.DataValueField = "VisitId";
            ddlVisitDate.DataTextField = "Date";
            ddlVisitDate.DataSource = visits.OrderBy(v => v.Date).ToList();
            ddlVisitDate.DataBind();

            ddlChartId.Items.Add(chartId);
        }
    
        protected void LoadViewer()
        {
            var viewer = _documentService.GetDocumentById(Convert.ToInt32(_documentId));
            var host = HttpContext.Current.Request.Url.Host;
            var port = HttpContext.Current.Request.Url.Port;

            pdfViewer.Controls.Add(new LiteralControl("<iframe src='http://" + host + ":" + port + viewer.ServerPath + "/" + viewer.Name +"' width='100%' height='400px'></iframe>"));
            
        }

        protected void UpdateOrphanDocumentAndGoToNext(object sender, EventArgs e)
        {
            var searchResults = DI.CreateDocumentService().GetOrphanedDocuments();
            if (searchResults.Count == 0)
            {
                Response.Redirect("Default.aspx?success=true");
            }
            else
            {
                var random = new Random(0);
                var index = random.Next(0, 5);

                Response.Redirect("UpdateDocument.aspx?id=" + searchResults[index].DocumentId + "&name=" + searchResults[index].Name);
            }
        }
    }
}