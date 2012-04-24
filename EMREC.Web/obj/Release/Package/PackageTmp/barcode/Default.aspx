<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EMREC.Web.barcode.Default1" %>


<asp:Content ID="Content3" ContentPlaceHolderID="Content" runat="Server">
        <form id="form1" runat="server"  class="forms">
        <section>
            <section>
                <header>
                    <div class="container_12 clearfix">
                        <h1 class="grid_12">Generate Barcode</h1>
                    </div>
                </header>
                <section id="main-content" class="clearfix">
                <script type="text/javascript" src="../js/jquery.min.js"></script> 
                <script type="text/javascript">
                    $(document).ready(function () {
                        $(window).bind('drilldown', function(){
                            $("input:checkbox,input:radio,select,input:file", "#main-content").uniform();
                        });
                    });
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
                                <label for="form-name" class="form-label">Patient Id <em>*</em></label>
                                <div class="form-input"><asp:TextBox runat="server" ID="tbpatientid" AccessKey="p" placeholder="Alt + P" TabIndex="1" OnTextChanged="LoadPatientData" AutoPostBack="true" required/></div>  
                            </div>
                            
                            <div class="clearfix">
                                <label></label>
                                <label><em>Populate fields below by inputting a patient Id and press TAB or ENTER.</em></label> 
                            </div>
                            <div class="clearfix">
                                <label for="form-name" class="form-label">Visit Date <em>*</em></label>
                                <div class="form-input"><asp:DropDownList CssClass="selector" runat="server" ID="ddlVisitDate" required AutoPostBack="false"/></div>
                            </div>
                            <div class="clearfix">
                                <label for="form-name" class="form-label">Chart Id <em>*</em></label>
                                <div class="form-input"><asp:DropDownList CssClass="selector" runat="server" ID="ddlChartId" required AutoPostBack="false"/></div>
                            </div>
                            <div class="clearfix">
                                <label for="form-name" class="form-label">Document Type <em>*</em></label>
                                <div class="form-input"><asp:DropDownList CssClass="selector" runat="server" ID="ddlDocType" required AutoPostBack="false"/></div>
                            </div>
                            <br/>
                            <div class="form-action clearfix">
                                <asp:Button runat="server" class="button" ID="bsearch" Text="Generate Barcode" OnClick="GenerateBarcode"/>
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


