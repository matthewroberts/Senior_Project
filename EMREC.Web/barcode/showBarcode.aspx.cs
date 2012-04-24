using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Org.BouncyCastle.Asn1.Ocsp;

namespace EMREC.Web.barcode
{
    public partial class showBarcode : System.Web.UI.Page
    {
        private string _fileName;

        protected void Page_Load(object sender, EventArgs e)
        {
            _fileName = Request.QueryString["file"] + ".gif";
            imgBarcode.ImageUrl = "~/barcode/Temp/" + _fileName;
        }

        protected void BtnDeleteClick(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
            File.Delete(Server.MapPath(imgBarcode.ImageUrl));
            Response.Redirect("Default.aspx");
        }
    }
}