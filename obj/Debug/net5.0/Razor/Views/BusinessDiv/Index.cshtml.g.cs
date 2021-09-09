#pragma checksum "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\BusinessDiv\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "851e6c32f5ce21bb4aa581661a354b1cab3cac1a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_BusinessDiv_Index), @"mvc.1.0.view", @"/Views/BusinessDiv/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\_ViewImports.cshtml"
using BLMS;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\_ViewImports.cshtml"
using BLMS.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"851e6c32f5ce21bb4aa581661a354b1cab3cac1a", @"/Views/BusinessDiv/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1935fafd0aa5610a1ac330bdda92ea4db0ded552", @"/Views/_ViewImports.cshtml")]
    public class Views_BusinessDiv_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<BLMS.Models.Admin.BusinessDiv>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-success btn-block text-white text-sm font-weight-bold"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\BusinessDiv\Index.cshtml"
  
    ViewData["Title"] = "Business Division";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<section class=""section align-baseline"">
    <!-- Grid row -->
    <div class=""row"">

        <!-- Grid column -->
        <div class=""col-md-12"">
            <div class=""card card-cascade cascading-admin-card user-card"">
                <!-- Card Data -->
                <div class=""admin-up d-flex justify-content-start"">
                    <i class=""fas fa fa-university info-color py-4 mr-3 z-depth-2""></i>
                    <div class=""data"">
                        <h5 class=""font-weight-bold dark-grey-text"">
                            Business Division
                        </h5>
                    </div>
                </div>
                <!-- Card Data -->
                <!-- Card content -->
                <div class=""card-body card-body-cascade"">
                    <!-- Grid row -->
                    <div class=""row"">
                        <div class=""col-lg-12"">
                            <div id=""alert"" class=""form-group"">
                                ");
#nullable restore
#line 30 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\BusinessDiv\Index.cshtml"
                           Write(Html.Raw(@ViewBag.Alert));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                            </div>

                            <div class=""table table-wrapper table-responsive-sm"">
                                <!--Table-->
                                <table id=""BusinessDiv"" class=""table table-hover mb-0"">
                                    <!-- Table head -->
                                    <thead>
                                        <tr>
                                            <th class=""text-sm font-weight-bold align-text-top col-md-10"">
                                                ");
#nullable restore
#line 40 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\BusinessDiv\Index.cshtml"
                                           Write(Html.DisplayNameFor(model => model.DivName));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                                            </th>
                                            <th class=""text-sm font-weight-bold align-text-top col-md-2 text-center"">Action</th>
                                        </tr>
                                    </thead>
                                    <!-- Table head -->
                                    <!-- Table body -->
                                    <tbody>
");
#nullable restore
#line 48 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\BusinessDiv\Index.cshtml"
                                         foreach (var item in Model)
                                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            <tr");
            BeginWriteAttribute("id", " id=\"", 2355, "\"", 2377, 2);
            WriteAttributeValue("", 2360, "row_", 2360, 4, true);
#nullable restore
#line 50 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\BusinessDiv\Index.cshtml"
WriteAttributeValue("", 2364, item.DivName, 2364, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                                <td scope=\"row\" class=\"text-sm align-text-top col-md-10\">\r\n                                                    ");
#nullable restore
#line 52 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\BusinessDiv\Index.cshtml"
                                               Write(Html.DisplayFor(modelItem => item.DivName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                </td>\r\n                                                <td class=\"text-center\">\r\n                                                    <a class=\"btn btn-outline-primary btn-rounded btn-sm px-2\" title=\"Edit\"");
            BeginWriteAttribute("href", " href=\"", 2837, "\"", 2899, 1);
#nullable restore
#line 55 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\BusinessDiv\Index.cshtml"
WriteAttributeValue("", 2844, Url.Action("Edit", "BusinessDiv", new {id=item.DivID}), 2844, 55, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                                                        <i class=""fas fa-pencil-alt mt-0""></i>
                                                    </a>
                                                    <a class=""btn btn-outline-danger btn-rounded btn-sm px-2"" title=""Delete"" href=""#""");
            BeginWriteAttribute("onclick", " onclick=\"", 3190, "\"", 3226, 3);
            WriteAttributeValue("", 3200, "ConfirmDelete(", 3200, 14, true);
#nullable restore
#line 58 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\BusinessDiv\Index.cshtml"
WriteAttributeValue("", 3214, item.DivID, 3214, 11, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 3225, ")", 3225, 1, true);
            EndWriteAttribute();
            WriteLiteral(@">
                                                        <i class=""fas fa-trash-alt mt-0""></i>
                                                    </a>
                                                </td>
                                            </tr>
");
#nullable restore
#line 63 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\BusinessDiv\Index.cshtml"
                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                    </tbody>
                                    <!-- Table body -->
                                </table>
                                <!-- Table -->
                                <!--Delete bootstrap confirmation box-->
                                <div class=""modal fade top"" id=""delete-conformation"" aria-labelledby=""delete-conformation"" aria-hidden=""true"" tabindex=""-1"" role=""dialog"">
                                    <div class=""modal-dialog modal-frame modal-top modal-notify modal-danger"">
                                        <div class=""modal-content"">
                                            <div class=""modal-header flex-column"">
                                                <div class=""icon-box"">
                                                    <i class=""material-icons"">&#xE5CD;</i>
                                                </div>
                                                <h4 class=""modal-title w-100 font-weight-bolder text");
            WriteLiteral(@"-center text-white"">DELETE BUSINESS DIVISION?</h4>
                                                <br />
                                                <p class=""mb-1 align-self-sm-center text-white"" style=""color: red;""><i class=""fas fa-exclamation-circle""></i> The saved data will be permanently deleted from BLMS database.</p>
                                                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-hidden=""true"">&times;</button>
                                            </div>
                                            <div class=""modal-footer justify-content-center"">
                                                <button type=""button"" class=""btn btn-outline-info waves-effect"" data-dismiss=""modal"">Cancel</button>
                                                <a href=""#"" class=""btn btn-outline-danger waves-effect"" onclick=""DeleteBusDiv()""><i class=""far fa fa-eraser ml-1 text-black""></i> Delete</a>
                                            </div>
         ");
            WriteLiteral("                               </div>\r\n                                    </div>\r\n                                </div>\r\n\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "851e6c32f5ce21bb4aa581661a354b1cab3cac1a12291", async() => {
                WriteLiteral("<i class=\"far fa fa-plus ml-1 text-black\"></i> Create Business Division");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n");
            WriteLiteral(@"                                <input type=""hidden"" id=""hidDivId"" />
                            </div>
                        </div>
                    </div>
                    <!-- Grid row -->
                </div>
                <!-- Card content -->
            </div>
        </div>
    </div>
</section>

");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>
        var ConfirmDelete = function (DivId) {

            $(""#hidDivId"").val(DivId);
            $(""#delete-conformation"").modal('show');
        }

        var DeleteBusDiv = function () {
            var DivId = $(""#hidDivId"").val();

            $.ajax({

                type: ""POST"",
                url: '");
#nullable restore
#line 118 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\BusinessDiv\Index.cshtml"
                 Write(Url.Action("Delete", "BusinessDiv"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"',
                dataType: ""json"",
                data: { Id: DivId },
                success: function (result) {
                    $(""#delete-conformation"").modal(""hide"");
                    $(""#row_"" + DivId).remove();
                    window.location.href =  '");
#nullable restore
#line 124 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\BusinessDiv\Index.cshtml"
                                        Write(Url.Action("Index", "BusinessDiv"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"'
                }
            })
        }
    </script>

    <script>
        $(document).ready(function () {
            $('#BusinessDiv').DataTable();
            $('.dataTables_length').addClass('bs-select');
        });

        //auto hide viewbag.alert
        $(document).ready(function () {
            setTimeout(function () {
                $(""#alert"").fadeOut();
            }, 3000);
        });
    </script>

");
#nullable restore
#line 144 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\BusinessDiv\Index.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<BLMS.Models.Admin.BusinessDiv>> Html { get; private set; }
    }
}
#pragma warning restore 1591
