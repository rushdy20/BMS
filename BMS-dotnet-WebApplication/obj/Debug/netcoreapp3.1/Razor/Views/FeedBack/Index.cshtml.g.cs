#pragma checksum "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\FeedBack\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a06e15a6125cc1065a2bdff5a3a27d68195497a6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_FeedBack_Index), @"mvc.1.0.view", @"/Views/FeedBack/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a06e15a6125cc1065a2bdff5a3a27d68195497a6", @"/Views/FeedBack/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d3c4039b0fdcc97212a713b1acd2623d6a084b9a", @"/Views/_ViewImports.cshtml")]
    public class Views_FeedBack_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<BMS.BusinessLayer.Models.FeedbackModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\FeedBack\Index.cshtml"
 foreach (var feedback in Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"form-group row\">\r\n        <div class=\"feedback\">\r\n            <p>\r\n                <span>Name: </span><b>");
#nullable restore
#line 10 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\FeedBack\Index.cshtml"
                                 Write(feedback.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b>\r\n            </p>\r\n            <p>\r\n                <span>Email: </span><b>");
#nullable restore
#line 13 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\FeedBack\Index.cshtml"
                                  Write(feedback.EmailAddress);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b>\r\n            </p>\r\n            <p>\r\n                <span>Feedback: </span><b>");
#nullable restore
#line 16 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\FeedBack\Index.cshtml"
                                     Write(feedback.FeedBack);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b>\r\n            </p>\r\n            <p>\r\n                <span>FeedbackOn: </span><b>");
#nullable restore
#line 19 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\FeedBack\Index.cshtml"
                                       Write(feedback.FeedbackOn);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b>\r\n            </p>\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 23 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\FeedBack\Index.cshtml"
    
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<BMS.BusinessLayer.Models.FeedbackModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591