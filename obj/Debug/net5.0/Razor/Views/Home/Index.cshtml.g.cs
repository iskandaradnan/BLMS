#pragma checksum "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "507966dc95cac5bfeb7aab55da36d05c4c97138b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"507966dc95cac5bfeb7aab55da36d05c4c97138b", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1935fafd0aa5610a1ac330bdda92ea4db0ded552", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BLMS.Models.User>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\Home\Index.cshtml"
  
    ViewData["Title"] = "My Profile";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<section class=\"section team-section\">\r\n");
#nullable restore
#line 8 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\Home\Index.cshtml"
     if (User.Identity.IsAuthenticated)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <div class=""row text-center"">

            <!-- Grid column -->
            <div class=""col-md-8 mb-4"">

                <!-- Card -->
                <div class=""card card-cascade cascading-admin-card user-card"">

                    <!-- Card Data -->
                    <div class=""admin-up d-flex justify-content-start"">
                        <i class=""fas fa-users info-color py-4 mr-3 z-depth-2""></i>
                        <div class=""data"">
                            <h5 class=""font-weight-bold dark-grey-text"">
                                My Profile
                            </h5>
                        </div>
                    </div>
                    <!-- Card Data -->
                    <!-- Card content -->
                    <div class=""card-body card-body-cascade"">
                        <!-- Grid row -->
                        <div class=""row"">
                            <!-- Grid column -->
                            <div class=""col-md-12"">
   ");
            WriteLiteral("                             <div class=\"md-form form-sm mb-0\">\r\n                                    <input type=\"text\" id=\"form5\" class=\"form-control disabled\"");
            BeginWriteAttribute("value", " value=\"", 1347, "\"", 1374, 1);
#nullable restore
#line 35 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\Home\Index.cshtml"
WriteAttributeValue("", 1355, User.Identity.Name, 1355, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    <label for=\"form5\"");
            BeginWriteAttribute("class", " class=\"", 1432, "\"", 1440, 0);
            EndWriteAttribute();
            WriteLiteral(@">Staff Name</label>
                                </div>
                            </div>
                            <!-- Grid column -->
                        </div>
                        <!-- Grid row -->
                        <!-- Grid row -->
                        <div class=""row"">
                            <!-- Grid column -->
                            <div class=""col-md-12"">
                                <div class=""md-form form-sm mb-0"">
                                    <input type=""text"" id=""form6"" class=""form-control disabled""");
            BeginWriteAttribute("value", " value=\"", 2015, "\"", 2038, 1);
#nullable restore
#line 47 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\Home\Index.cshtml"
WriteAttributeValue("", 2023, Model.STAFF_NO, 2023, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    <label for=\"form6\"");
            BeginWriteAttribute("class", " class=\"", 2096, "\"", 2104, 0);
            EndWriteAttribute();
            WriteLiteral(@">Staff No</label>
                                </div>
                            </div>
                            <!-- Grid column -->
                        </div>
                        <!-- Grid row -->
                        <!-- Grid row -->
                        <div class=""row"">
                            <!-- Grid column -->
                            <div class=""col-md-12"">
                                <div class=""md-form form-sm mb-0"">
                                    <input type=""text"" id=""form6"" class=""form-control disabled""");
            BeginWriteAttribute("value", " value=\"", 2677, "\"", 2703, 1);
#nullable restore
#line 59 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\Home\Index.cshtml"
WriteAttributeValue("", 2685, Model.STAFF_EMAIL, 2685, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    <label for=\"form6\"");
            BeginWriteAttribute("class", " class=\"", 2761, "\"", 2769, 0);
            EndWriteAttribute();
            WriteLiteral(@">Staff Email</label>
                                </div>
                            </div>
                            <!-- Grid column -->
                        </div>
                        <!-- Grid row -->
                        <!-- Grid row -->
                        <div class=""row"">
                            <!-- Grid column -->
                            <div class=""col-md-6"">
                                <div class=""md-form form-sm mb-0"">
                                    <input type=""text"" id=""form6"" class=""form-control disabled""");
            BeginWriteAttribute("value", " value=\"", 3344, "\"", 3363, 1);
#nullable restore
#line 71 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\Home\Index.cshtml"
WriteAttributeValue("", 3352, Model.ROLE, 3352, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    <label for=\"form6\"");
            BeginWriteAttribute("class", " class=\"", 3421, "\"", 3429, 0);
            EndWriteAttribute();
            WriteLiteral(@">Staff Role</label>
                                </div>
                            </div>
                            <!-- Grid column -->
                            <!-- Grid column -->
                            <div class=""col-md-6"">
                                <div class=""md-form form-sm mb-0"">
                                    <input type=""text"" id=""form6"" class=""form-control disabled""");
            BeginWriteAttribute("value", " value=\"", 3842, "\"", 3869, 1);
#nullable restore
#line 79 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\Home\Index.cshtml"
WriteAttributeValue("", 3850, Model.ACCESS_LEVEL, 3850, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                                    <label for=\"form6\"");
            BeginWriteAttribute("class", " class=\"", 3927, "\"", 3935, 0);
            EndWriteAttribute();
            WriteLiteral(@">Access Level</label>
                                </div>
                            </div>
                            <!-- Grid column -->
                        </div>
                        <!-- Grid row -->
                    </div>
                    <!-- Card content -->
                </div>
                <!-- Card -->
            </div>
            <div class=""col-md-4 mb-4"">
                <!-- Card -->
                <div class=""card profile-card"">
                    <!-- Avatar -->
                    <div class=""avatar z-depth-1-half mb-4"">
                        <img src=""https://mdbootstrap.com/img/Photos/Avatars/img%20(10).jpg"" class=""rounded-circle"" alt=""First sample avatar image"">
                    </div>
                    <div class=""card-body pt-0 mt-0"">
                        <!-- Name -->
                        <h3 class=""mb-3 font-weight-bold""><strong>");
#nullable restore
#line 100 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\Home\Index.cshtml"
                                                             Write(User.Identity.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</strong></h3>\r\n                        <h6 class=\"font-weight-bold cyan-text mb-4\">");
#nullable restore
#line 101 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\Home\Index.cshtml"
                                                               Write(Model.ROLE);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n                    </div>\r\n                </div>\r\n                <!-- Card -->\r\n            </div>\r\n            <!-- Grid column -->\r\n        </div>\r\n");
#nullable restore
#line 108 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\Home\Index.cshtml"
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"row\">\r\n            <div class=\"col-lg-4 col-md-4 col-sm-4\">\r\n                <div>\r\n");
#nullable restore
#line 114 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\Home\Index.cshtml"
                     using (Html.BeginForm("LoginUser", "Home", FormMethod.Post, new { role = "form" }))
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div>\r\n                            ");
#nullable restore
#line 117 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\Home\Index.cshtml"
                       Write(Html.AntiForgeryToken());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            <div>\r\n                                <label>EMAIL</label><br />\r\n                            </div>\r\n                            <div>\r\n                                ");
#nullable restore
#line 122 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\Home\Index.cshtml"
                           Write(Html.TextBoxFor(m => m.STAFF_EMAIL, new { @class = "form-control txtbox" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                            </div>
                        </div>
                        <div style=""padding-left:35%;width:40%;"">
                            <div style=""padding-top:20px;"">
                                <input class=""btn btn-primary btn-lg rph-login-button"" type=""submit"" value=""Login"" id=""login"" />
                            </div>
                        </div>
");
#nullable restore
#line 130 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\Home\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 134 "C:\Users\iskandar.adnan\Documents\GitHub\blmscloud\BLMS\Views\Home\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</section>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    <script>\r\n        $(document).ready(function () {\r\n            $(\"#login\").trigger(\'click\');\r\n        });\r\n    </script>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BLMS.Models.User> Html { get; private set; }
    }
}
#pragma warning restore 1591
