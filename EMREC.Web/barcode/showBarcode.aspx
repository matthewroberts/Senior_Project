<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="showBarcode.aspx.cs" Inherits="EMREC.Web.barcode.showBarcode" %>


<asp:Content ID="Content3" ContentPlaceHolderID="Content" runat="Server">
        <form id="form2" runat="server"  class="forms">
        <section>
            <section>
                <header>
                    <div class="container_12 clearfix">
                        <h1 class="grid_12">Print Barcode</h1>
                    </div>
                </header>
                <section id="main-content" class="clearfix">
                <script type="text/javascript">
                    var directory = '/barcode/Temp/<%=Request.QueryString["file"]%>.gif';
                    function ClientSidePrint() {
                        var printWindow = window.open(directory, "mywindow", "width=200,height=100");
                        printWindow.document.close();
                        printWindow.focus();
                        setTimeout(function () { printWindow.print(); }, 1000);
                        setTimeout(function() { printWindow.close(); }, 15000);
                    }
                </script>
                <div class="container_12 clearfix leading">
                <section id="notificationerror" style="display: none" runat="server">
                    <div class="message error">
   	                    <h3>Error!</h3>
   	                    <p> <asp:Label runat="server" ID="lblError"></asp:Label></p>
                    </div>
                </section><br/>
                    <div class="grid_12">
                        <div class="form" runat="server" id="searchDiv">
                            <div class="clearfix">
                                <div id="imgDiv">
                                    <asp:Image runat="server" ID="imgBarcode"/><br/>
                                </div>
                            </div>
                            <div class="form-action clearfix">
                                <asp:Button runat="server" ID="btnPrint" Text="Print" OnClientClick="ClientSidePrint();" OnClick="BtnDeleteClick"/>
                            </div>
                        </div>
                        <br/>
                    </div>
                </div>
                </section>
            </section>
        </section>
        </form>
</asp:Content>
