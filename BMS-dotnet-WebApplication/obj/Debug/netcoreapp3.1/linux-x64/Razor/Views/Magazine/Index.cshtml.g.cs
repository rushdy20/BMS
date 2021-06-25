#pragma checksum "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d1edce38695d3202bcac0881d730a42bc36522a5"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Magazine_Index), @"mvc.1.0.view", @"/Views/Magazine/Index.cshtml")]
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
#line 1 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\_ViewImports.cshtml"
using BMS_dotnet_WebApplication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\_ViewImports.cshtml"
using BMS_dotnet_WebApplication.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d1edce38695d3202bcac0881d730a42bc36522a5", @"/Views/Magazine/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d3c4039b0fdcc97212a713b1acd2623d6a084b9a", @"/Views/_ViewImports.cshtml")]
    public class Views_Magazine_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<BMS_dotnet_WebApplication.Models.MagazineVM.MagazineIndexVM>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Magazine Manager</h1>\r\n\r\n<div class=\"form-group row container custom-box\">\r\n    ");
#nullable restore
#line 9 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\Index.cshtml"
Write(Html.ActionLink("Magazine Category", "CreateCategory", "Magazine", null, new {@class = "btn btn-secondary"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n\r\n<div class=\"form-group row container custom-box\">\r\n    ");
#nullable restore
#line 13 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\Index.cshtml"
Write(Html.ActionLink("Create Magazine", "CreateMagazine", "Magazine", null, new {@class = "btn btn-secondary"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n\r\n");
#nullable restore
#line 16 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\Index.cshtml"
 foreach (var magazine in Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"form-group\">\r\n        <div class=\"card\" style=\"width: 18rem; margin: 20px;\">\r\n\r\n            <div class=\"card-center\">\r\n                <a");
            BeginWriteAttribute("href", " href=\'", 686, "\'", 765, 1);
#nullable restore
#line 22 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\Index.cshtml"
WriteAttributeValue("", 693, Url.Action("CurrentEdition", "Magazine", new {Id=@magazine.MagazineId}), 693, 72, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                    <img");
            BeginWriteAttribute("src", " src=\"", 793, "\"", 891, 1);
#nullable restore
#line 23 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\Index.cshtml"
WriteAttributeValue("", 799, Url.Action("GetImage", new {filename = magazine.CurrentEditionImage, section = "magazine"}), 799, 92, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" alt=\"...\">\r\n                </a>\r\n            </div>\r\n\r\n            <div class=\"card-body\">\r\n                <h5 class=\"card-title\"> ");
#nullable restore
#line 28 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\Index.cshtml"
                                   Write(magazine.CurrentEditionName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n");
            WriteLiteral("\r\n");
#nullable restore
#line 37 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\Index.cshtml"
                 if (!magazine.IsLive)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 39 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\Index.cshtml"
               Write(Html.ActionLink("Add Content", "CreateContent", "Magazine", new { Id = magazine.MagazineId }, new {@class = "btn btn-primary"}));

#line default
#line hidden
#nullable disable
#nullable restore
#line 41 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\Index.cshtml"
               Write(Html.ActionLink("Remove Content", "RemoveContent", "Magazine", new { Id = magazine.MagazineId }, new {@class = "btn btn-danger"}));

#line default
#line hidden
#nullable disable
#nullable restore
#line 41 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\Index.cshtml"
                                                                                                                                                      

                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 45 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\Index.cshtml"
                 if (magazine.IsLive)
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 47 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\Index.cshtml"
               Write(Html.ActionLink("Off Live", "StatusUpdate", "Magazine", new {status = false, Id = magazine.MagazineId}, new {@class = "btn btn-danger"}));

#line default
#line hidden
#nullable disable
#nullable restore
#line 47 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\Index.cshtml"
                                                                                                                                                             
                }
                else
                {
                    

#line default
#line hidden
#nullable disable
#nullable restore
#line 51 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\Index.cshtml"
               Write(Html.ActionLink("Go Live", "StatusUpdate", "Magazine", new {status = true, Id = magazine.MagazineId}, new {@class = "btn btn-primary"}));

#line default
#line hidden
#nullable disable
#nullable restore
#line 51 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\Index.cshtml"
                                                                                                                                                            
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 58 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\Index.cshtml"

}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<BMS_dotnet_WebApplication.Models.MagazineVM.MagazineIndexVM>> Html { get; private set; }
    }
}
#pragma warning restore 1591
