#pragma checksum "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_Feedback.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3aa194eb7dcf8996ac6e2963764d7be04684439b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__Feedback), @"mvc.1.0.view", @"/Views/Shared/_Feedback.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3aa194eb7dcf8996ac6e2963764d7be04684439b", @"/Views/Shared/_Feedback.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d3c4039b0fdcc97212a713b1acd2623d6a084b9a", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__Feedback : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BMS_dotnet_WebApplication.Models.Shared.FeedbackModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 4 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_Feedback.cshtml"
 using (Html.BeginForm("Feedback", "Magazine", FormMethod.Post))
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""feedback"">
        <div class=""feedback-info"">
            Your feedback helps us give you our best content. Please take a moment to leave your feedback, suggestions and ideas that you would like us to include in our future issues. <br />
            To get in touch, we can be reached via editor-magazine@britanniaislamiccentre.org  <br />
            Thank You!
        </div>
        <div>
            ");
#nullable restore
#line 13 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_Feedback.cshtml"
       Write(Html.ValidationSummary(false, "", new { @class = "text-danger" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n\r\n        ");
#nullable restore
#line 16 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_Feedback.cshtml"
   Write(Html.HiddenFor(m => m.FeedbackOn));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 19 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_Feedback.cshtml"
       Write(Html.LabelFor(m => m.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 20 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_Feedback.cshtml"
       Write(Html.TextBoxFor(m => m.Name, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 21 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_Feedback.cshtml"
       Write(Html.ValidationMessageFor(m => m.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 24 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_Feedback.cshtml"
       Write(Html.LabelFor(m => m.EmailAddress));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 25 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_Feedback.cshtml"
       Write(Html.TextBoxFor(m => m.EmailAddress, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 26 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_Feedback.cshtml"
       Write(Html.ValidationMessageFor(m => m.EmailAddress));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 29 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_Feedback.cshtml"
       Write(Html.LabelFor(m => m.FeedBack));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 30 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_Feedback.cshtml"
       Write(Html.TextAreaFor(m => m.FeedBack, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 31 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_Feedback.cshtml"
       Write(Html.ValidationMessageFor(m => m.FeedBack));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            <button type=\"submit\" id=\"btnSubmit\" class=\"btn btn-primary\">Submit</button>\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 38 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_Feedback.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BMS_dotnet_WebApplication.Models.Shared.FeedbackModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
