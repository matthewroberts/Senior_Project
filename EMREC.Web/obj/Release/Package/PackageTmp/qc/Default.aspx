<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EMREC.Web.qc.Default1" %>


<asp:Content ID="Content3" ContentPlaceHolderID="Content" runat="Server">
<form id="form1" runat="server"  class="forms">
        <section>
            <section>
            <link rel="stylesheet" media="screen" href="../lib/datatables/css/vpad.css" />
            <script type="text/javascript" src="../js/jquery.min.js"></script>
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
            </script>
                <header>
                    <div class="container_12 clearfix">
                        <h1 class="grid_12">Orphan Documents</h1>
                    </div>
                </header>
                <section id="main-content" class="clearfix">
                <div class="container_12 clearfix leading">
                    
                <section id="notificationsuccess" style="display: none" runat="server">
                    <div class="message success">
   	                    <h3>Success!</h3>
   	                    <p> Document was updated.</p>
                    </div>
                </section><br/>
                    <div class="grid_12" id="view-tables.html">
                    <section>
                        <section class="clearfix" style="display: block">
                       <asp:GridView runat="server" PageSize="25" CssClass="display" ID="gvOrphan" AllowPaging="True" OnPageIndexChanging="GridViewPageIndexChanging" OnSorting="GridViewSorting"  AllowSorting="true" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField HeaderText="Document Name" DataField="Name" SortExpression="Name"/>
                                <asp:BoundField HeaderText="Date Scanned" DataField="LastUpdated" SortExpression="LastUpdated"/>
                                <asp:TemplateField HeaderText="PDF"><ItemTemplate><span class="<%#(Eval("Name").ToString().ToLower().EndsWith(".pdf"))?"inv":"" %>"><a target="_blank" href="<%#Eval("ServerPath") + "/" + Eval("Name")%>">View</a></span></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField><ItemTemplate><a href="UpdateDocument.aspx?id=<%#Eval("DocumentId")%>&name=<%#Eval("Name")%>">Update</a></ItemTemplate></asp:TemplateField>
                            </Columns>
                        </asp:GridView>  
                        </section>
                        </section> 
                    </div>
                    </div>  
                </section>
            </section>
        </section>
        </form>
</asp:Content>


