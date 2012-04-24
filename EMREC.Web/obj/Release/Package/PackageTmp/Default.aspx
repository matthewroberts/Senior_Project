<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EMREC.Web.Default1" %>
<%@ Import Namespace="System.Activities.Expressions" %>

<asp:Content ID="Content3" ContentPlaceHolderID="Content" runat="Server">
<form id="form1" runat="server" class="forms">
        <section>
            <section>
                <header>
                    <div class="container_12 clearfix">
                        <h1 class="grid_12">Document Search</h1>
                    </div>
                </header>
                <section id="main-content" class="clearfix">
                <div class="container_12 clearfix leading">
                    <section id="notification" style="display: none" runat="server">
                        <div class="message error">
   	                        <h3>Error!</h3>
   	                        <p><asp:Label runat="server" ID="errorMsg"></asp:Label></p>
                        </div>
                    </section><br/>
                    <div class="grid_12" id="view-tables.html">
                        <div class="form">
                            <div class="clearfix">
                                <label for="form-name" class="form-label">Patient Id <em>*</em></label>
                                <div class="form-input"><asp:TextBox runat="server" ID="tbpatientid" required/></div>  
                            </div>
                            <div class="form-action clearfix"><asp:Button runat="server" ID="bsearch" Text="Search" OnClick="Search" /></div> 
                        </div>
                        <br/>
                        <div runat="server" id="reports" style="display: none; overflow: auto;" >
                            <div class="form" runat="server">
                                <div class="clearfix">
                                    <label for="form-name" class="form-label">Patient Name: </label>
                                    <div class="form-input"><asp:TextBox runat="server" ID="lblPatientName" Enabled="False" ReadOnly="True" BackColor="#f7f7f7" /></div>  
                                </div>
                                <div class="clearfix">
                                    <label for="form-name" class="form-label">Patient DOB: </label>
                                    <div class="form-input"><asp:TextBox runat="server" ID="lblDOB" Enabled="False" ReadOnly="True" BackColor="#f7f7f7" /></div>  
                                </div>
                                <div class="clearfix">
                                    <label for="form-name" class="form-label">Patient Address: </label>
                                    <div class="form-input"><asp:TextBox runat="server" ID="lblAddress" Enabled="False" ReadOnly="True" BackColor="#f7f7f7" /></div>  
                                </div>
                                <div class="clearfix">
                                    <label for="form-name" class="form-label">Patient Phone: </label>
                                    <div class="form-input"><asp:TextBox runat="server" ID="lblPhone" Enabled="False" ReadOnly="True" BackColor="#f7f7f7" /></div>  
                                </div>
                            </div>
                            <section class="tabs leading" id="tabs">
                                <ul class="clearfix">
                                    <li><a href="#ABN" class="tab" id="abnTab">ABN</a></li>
                                    <li><a href="#Labs" class="tab" id="labTab">Labs</a></li>
                                    <li><a href="#Referrals" class="tab" id="refTab">Referrals</a></li>
                                    <li><a href="#XRays" class="tab" id="xrayTab">X-Rays</a></li>
                                    <li><a href="#Other" class="tab" id="otherTab">Other</a></li>
                                </ul>
                                <section>
                                    <section class="clearfix tabsection" id="ABN">
                                        <header class="alpha omega" style="display: none" id="abnHead" runat="server">No Documents Found.</header>
                                        <div>
                                            <asp:GridView runat="server" CssClass="display" ID="gvABN" AllowPaging="True" OnPageIndexChanging="gridView_PageIndexChanging" OnSorting="gridView_Sorting"  AllowSorting="true" AutoGenerateColumns="false" >
                                                <Columns>
                                                    <asp:BoundField HeaderText="Document Id" DataField="DocumentId" SortExpression="DocumentId" />
                                                    <asp:BoundField HeaderText="Document Name" DataField="Name" SortExpression="Name"/>
                                                    <asp:BoundField HeaderText="Description" DataField="Description" SortExpression="Description"/>
                                                    <asp:BoundField HeaderText="Date" DataField="DocumentDate" SortExpression="DocumentDate"/>
                                                    <asp:TemplateField HeaderText="PDF"><ItemTemplate><span class="<%#(Eval("Name").ToString().ToLower().EndsWith(".pdf"))?"inv":"" %>"><a target="_blank" href="<%#Eval("ServerPath") + "/" + Eval("Name")%>">View</a></span></ItemTemplate></asp:TemplateField>
                                               </Columns>
                                            </asp:GridView>                
                                        </div>
                                    </section>
                                    <section class="clearfix tabsection" id="Labs">
                                        <header class="alpha omega" style="display: none" id="labsHead" runat="server">No Documents Found.</header>
                                        <div>
                                            <asp:GridView runat="server" CssClass="display" ID="gvLabs" AllowSorting="true" AutoGenerateColumns="false" AllowPaging="True" OnPageIndexChanging="gridView_PageIndexChanging" OnSorting="gridView_Sorting" >
                                                <Columns>
                                                    <asp:BoundField HeaderText="Document Id" DataField="DocumentId" SortExpression="DocumentId" />
                                                    <asp:BoundField HeaderText="Document Name" DataField="Name" SortExpression="Name"/>
                                                    <asp:BoundField HeaderText="Description" DataField="Description" SortExpression="Description"/>
                                                    <asp:BoundField HeaderText="Date" DataField="DocumentDate" SortExpression="DocumentDate"/> 
                                                    <asp:TemplateField HeaderText="PDF"><ItemTemplate><span class="<%#(Eval("Name").ToString().ToLower().EndsWith(".pdf"))?"inv":"" %>"><a target="_blank" href="<%#Eval("ServerPath") + "/" + Eval("Name")%>">View</a></span></ItemTemplate></asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>                
                                        </div>
                                    </section>
                                    <section class="clearfix tabsection" id="Referrals">
                                        <header class="alpha omega" style="display: none" id="refHead" runat="server">No Documents Found.</header>
                                        <div>
                                            <asp:GridView runat="server" CssClass="display" ID="gvReferrals" AllowSorting="true" AutoGenerateColumns="false"  AllowPaging="True" OnPageIndexChanging="gridView_PageIndexChanging" OnSorting="gridView_Sorting" >
                                                <Columns>
                                                    <asp:BoundField HeaderText="Document Id" DataField="DocumentId" SortExpression="DocumentId" />
                                                    <asp:BoundField HeaderText="Document Name" DataField="Name" SortExpression="Name"/>
                                                    <asp:BoundField HeaderText="Description" DataField="Description" SortExpression="Description"/>
                                                    <asp:BoundField HeaderText="Date" DataField="DocumentDate" SortExpression="DocumentDate"/>
                                                    <asp:TemplateField HeaderText="PDF"><ItemTemplate><span class="<%#(Eval("Name").ToString().ToLower().EndsWith(".pdf"))?"inv":"" %>"><a target="_blank" href="<%#Eval("ServerPath") + "/" + Eval("Name")%>">View</a></span></ItemTemplate></asp:TemplateField>
                                                </Columns>
                                            </asp:GridView> 
                                        </div>         
                                    </section>
                                    <section class="clearfix tabsection" id="XRays">
                                        <header class="alpha omega" style="display: none" id="xrayHead" runat="server">No Documents Found.</header>
                                        <div>
                                            <asp:GridView runat="server" CssClass="display" ID="gvXRays" AllowSorting="true" AutoGenerateColumns="false"  AllowPaging="True" OnPageIndexChanging="gridView_PageIndexChanging" OnSorting="gridView_Sorting">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Document Id" DataField="DocumentId" SortExpression="DocumentId" />
                                                    <asp:BoundField HeaderText="Document Name" DataField="Name" SortExpression="Name"/>
                                                    <asp:BoundField HeaderText="Description" DataField="Description" SortExpression="Description"/>
                                                    <asp:BoundField HeaderText="Date" DataField="DocumentDate" SortExpression="DocumentDate"/>
                                                    <asp:TemplateField HeaderText="PDF"><ItemTemplate><span class="<%#(Eval("Name").ToString().ToLower().EndsWith(".pdf"))?"inv":"" %>"><a target="_blank" href="<%#Eval("ServerPath") + "/" + Eval("Name")%>">View</a></span></ItemTemplate></asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>                
                                        </div>
                                    </section>
                                    <section class="clearfix tabsection" id="Other">
                                        <header class="alpha omega" style="display: none" id="otherHead" runat="server">No Documents Found.</header>
                                        <div>
                                            <asp:GridView runat="server" CssClass="display" ID="gvOther" AllowSorting="true" AutoGenerateColumns="false"  AllowPaging="True" OnPageIndexChanging="gridView_PageIndexChanging" OnSorting="gridView_Sorting">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Document Id" DataField="DocumentId" SortExpression="DocumentId" />
                                                    <asp:BoundField HeaderText="Document Name" DataField="Name" SortExpression="Name"/>
                                                    <asp:BoundField HeaderText="Description" DataField="Description" SortExpression="Description"/>
                                                    <asp:BoundField HeaderText="Date" DataField="DocumentDate" SortExpression="DocumentDate"/>
                                                    <asp:TemplateField HeaderText="PDF"><ItemTemplate><span class="<%#(Eval("Name").ToString().ToLower().EndsWith(".pdf"))?"inv":"" %>"><a target="_blank" href="<%#Eval("ServerPath") + "/" + Eval("Name")%>">View</a></span></ItemTemplate></asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>                
                                        </div>
                                    </section>
                                    <asp:HiddenField runat="server" ID="activeTab"/>
                                </section>
                            </section>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
                </section>
            </section>
        </section>
    
        <script type="text/javascript">
            $(document).ready(function () {
                if ($('#<%=activeTab.ClientID %>').val() == 'ABN') {
                    $(".tab").removeClass('current');
                    $('#abnTab').addClass('current');
                    $('.tabsection').hide();
                    $('#ABN').show();
                }
                else if ($('#<%=activeTab.ClientID %>').val() == 'REF') {
                    $(".tab").removeClass('current');
                    $('#refTab').addClass('current');
                    $('.tabsection').hide();
                    $('#Referrals').show();
                }
                else if ($('#<%=activeTab.ClientID %>').val() == 'XRAY') {
                    $(".tab").removeClass('current');
                    $('#xrayTab').addClass('current');
                    $('.tabsection').hide();
                    $('#XRays').show();
                }
                else if ($('#<%=activeTab.ClientID %>').val() == 'LAB') {
                    $(".tab").removeClass('current');
                    $('#labTab').addClass('current');
                    $('.tabsection').hide();
                    $('#Labs').show();
                }
                else if ($('#<%=activeTab.ClientID %>').val() == 'OTH') {
                    $(".tab").removeClass('current');
                    $('#otherTab').addClass('current');
                    $('.tabsection').hide();
                    $('#Other').show();
                }
                $('#abnTab').click(function () {
                    $(".tab").removeClass('current');
                    $('#abnTab').addClass('current');
                    $('.tabsection').hide();
                    $('#ABN').show();
                    $('#<%=activeTab.ClientID %>').val('ABN');
                });
                $('#labTab').click(function () {
                    $(".tab").removeClass('current');
                    $('#labTab').addClass('current');
                    $('.tabsection').hide();
                    $('#Labs').show();
                    $('#<%=activeTab.ClientID %>').val('LAB');
                });
                $('#refTab').click(function () {
                    $(".tab").removeClass('current');
                    $('#refTab').addClass('current');
                    $('.tabsection').hide();
                    $('#Referrals').show();
                    $('#<%=activeTab.ClientID %>').val('REF');
                });
                $('#xrayTab').click(function () {
                    $(".tab").removeClass('current');
                    $('#xrayTab').addClass('current');
                    $('.tabsection').hide();
                    $('#XRays').show();
                    $('#<%=activeTab.ClientID %>').val('XRAY');
                });
                $('#otherTab').click(function () {
                    $(".tab").removeClass('current');
                    $('#otherTab').addClass('current');
                    $('.tabsection').hide();
                    $('#Other').show();
                    $('#<%=activeTab.ClientID %>').val('OTH');
                });
                $('#main-content').bind('drilldown', function () {
                    $("input:checkbox,input:radio,select,input:file", "#main-content").uniform();
                });
            });
        </script>
        </form>
</asp:Content>


