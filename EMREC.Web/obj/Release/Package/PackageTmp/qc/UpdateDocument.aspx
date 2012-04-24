<%@ Page Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateDocument.aspx.cs" Inherits="EMREC.Web.qc.UpdateDocument" %>

<asp:Content ID="Content4" ContentPlaceHolderID="Content" runat="Server">
        <form id="form1" runat="server" class="forms">
        <section>
            <section>
                <header>
                    <div class="container_12 clearfix">
                        <h1 class="grid_12">Update <asp:Label runat="server" ID="lblDocName"></asp:Label></h1>
                    </div>
                </header>
                <section id="main-content" class="clearfix">
                    <script type="text/javascript" src="http://code.jquery.com/jquery-1.7.min.js"></script>
                    <script type="text/javascript" src="../js/jquery.hashchange.min.js"></script>
                    <script type="text/javascript" src="../js/jquery.drilldownmenu.js"></script>
                    <script type="text/javascript" src="../js/jquery.itextclear.js"></script>
                    <script type="text/javascript">
                        $(document).ready(function () {
                            $(window).bind('load resize', function () {
                                var section = $('#wrapper > section > section');
                                if (section.css('position') == 'absolute' && section.css('left') != 0) {
                                    if (location.hash != '#menu') {
                                        section.css('left', 0);
                                    } else {
                                        section.css('left', '100%');
                                    }
                                } else {
                                    section.show();
                                }
                            });
                        });
                        $(window).bind('drilldown', function () {
                            $("input:checkbox,input:radio,select,input:file", "#main-content").uniform();
                        });
                        function Change(obj, evt) {
                            if (evt.type == "focus")
                                obj.style.borderColor = "yellow";
                            else if (evt.type == "blur")
                                obj.style.borderColor = "black";
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
                        <div class="form">
                            <div class="clearfix">
                                <label for="form-name" class="form-label">Barcode <em>*</em></label>
                                <div class="form-input"><asp:TextBox runat="server" ID="TextBox1" AccessKey="N" placeholder="Alt + N" TabIndex="1" OnTextChanged="LoadPatientData" AutoPostBack="true" required/></div> 
                                
                            </div>
                        </div>
                                <label><em>-OR-</em></label> 
                        <br/>
                        <div class="form">
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
                                <div class="form-input"><asp:DropDownList CssClass="selector" AccessKey="v" placeholder="Alt + V"  runat="server" ID="ddlVisitDate" AutoPostBack="false" TabIndex="3" required/></div>
                            </div>
                            <div class="clearfix">
                                <label for="form-name" class="form-label">Chart Id <em>*</em></label>
                                <div class="form-input"><asp:DropDownList CssClass="selector" AccessKey="c" placeholder="Alt + C"  runat="server" ID="ddlChartId" AutoPostBack="false" TabIndex="4" required/></div>
                            </div>
                            <div class="clearfix">
                                <label for="form-name" class="form-label">Document Type <em>*</em></label>
                                <div class="form-input"><asp:DropDownList CssClass="selector" AccessKey="t" placeholder="Alt + T"  runat="server" ID="ddlDocType" AutoPostBack="false" TabIndex="5" required/></div>
                            </div>
                            <div class="clearfix">
                                <label for="form-name" class="form-label">Document Description </label>
                                <div class="form-input"><asp:TextBox runat="server" AccessKey="G" placeholder="Alt + G"  ID="tbDocDescription" TabIndex="6"/></div>
                            </div>
                            <br/>
                            <div class="form-action clearfix">
                                <asp:Button runat="server" class="button" ID="bnext" Text="Save and Continue" OnClick="UpdateOrphanDocument" TabIndex="7"/>
                                <asp:Button runat="server" class="button" ID="bupdate" Text="Save and Exit" OnClick="UpdateOrphanDocument" TabIndex="8"/>
                            </div>
                        </div>
                        <br/>
                        <a href="#" style="float: right">Pop Out</a>
                        <div class="grid_12">
                            <div class="form">
                                <div class="clearfix">
                                <asp:PlaceHolder runat="server" ID="pdfViewer"></asp:PlaceHolder>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </section>
            </section>
        </section>  
        </form>
</asp:Content>
