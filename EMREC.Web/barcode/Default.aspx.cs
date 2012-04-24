using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI;
using EMREC.Core.DependencyInjection;
using EMREC.Core.Domain.Interfaces;
using EMREC.Core.Domain.Models;
using EMREC.Core.Infrastructure.Mappings;
using iTextSharp.text.pdf;

namespace EMREC.Web.barcode
{
    public partial class Default1 : System.Web.UI.Page
    {
        private ISearchService _searchService;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeServices();
            if (Page.IsPostBack)
                return;

            LoadDocumentTypes(sender, e);
        }

        private void LoadDocumentTypes(object sender, EventArgs eventArgs)
        {
            var documentTypes = _searchService.GetDocumentTypes();
            documentTypes.Add(new DocumentType{Type = "",TypeId = -1});
            ddlDocType.DataTextField = "Type";
            ddlDocType.DataValueField = "TypeId";
            ddlDocType.DataSource = documentTypes.OrderBy(p => p.Type).ToList();
            ddlDocType.DataBind();
        }

        private void InitializeServices()
        {
            _searchService = DI.CreateSearchService();
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

        protected void GenerateBarcode(object sender, EventArgs e)
        {
            if (ddlDocType.SelectedIndex ==0 || tbpatientid.Text == "")
            {
                lblError.Text = "You must fill in all the required fields.";
                notificationerror.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "block");
                return;
            }

            var bc39 = new Barcode39();
            var visitId = ddlVisitDate.SelectedItem.Value;
            var type = ddlDocType.SelectedItem.Value;
            var chartId = ddlChartId.SelectedItem.Text;

            bc39.Code = visitId + "-" + type + "-" + chartId;
            var bc = BarcodeLib.Barcode.DoEncode(BarcodeLib.TYPE.CODE39, bc39.Code, true, Color.Black, Color.White, 200, 50);
            
            using (var ms = new MemoryStream())
            {
                bc.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                ms.WriteTo(Response.OutputStream);
            }
            var fileName = "Temp_" + DateTime.Now.Ticks;
            var bcDirectory = Server.MapPath("~/barcode/Temp/" + fileName + ".gif");
            bc.Save(bcDirectory, System.Drawing.Imaging.ImageFormat.Gif);
            
            Response.Redirect("showBarcode.aspx?file=" + fileName);
        }
    }
}