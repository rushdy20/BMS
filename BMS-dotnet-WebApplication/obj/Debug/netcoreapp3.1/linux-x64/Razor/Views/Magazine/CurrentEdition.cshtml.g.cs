#pragma checksum "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "366441483fa07d629a99bf461225e84149448cff"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Magazine_CurrentEdition), @"mvc.1.0.view", @"/Views/Magazine/CurrentEdition.cshtml")]
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
#nullable restore
#line 1 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
using BMS_dotnet_WebApplication.Models.Shared;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"366441483fa07d629a99bf461225e84149448cff", @"/Views/Magazine/CurrentEdition.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d3c4039b0fdcc97212a713b1acd2623d6a084b9a", @"/Views/_ViewImports.cshtml")]
    public class Views_Magazine_CurrentEdition : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BMS_dotnet_WebApplication.Models.MagazineVM.CurrentEditionVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("nav-link text-dark"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Magazine", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/imgs/feedback.png"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_ValidationScriptsPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n");
#nullable restore
#line 5 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
  
    ViewData["Title"] = "CurrentEdition";
    var currentHeading = string.Empty;
   
    var parentFolder = "magazine";
    var image = $"{"http://d2nxbo7tljjo4u.cloudfront.net/"}{parentFolder}/{Model.Magazine.FolderName}/{Model.Magazine.Image}";
    var contentBody = $"{Model.Magazine.Name}";
    var downloadFilePath = "";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("Meta", async() => {
                WriteLiteral("\r\n    <meta property=\"og:type\" content=\"article\" />\r\n    <meta property=\"og:title\" name=\"title\"");
                BeginWriteAttribute("content", " content=\"", 574, "\"", 596, 1);
#nullable restore
#line 18 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
WriteAttributeValue("", 584, contentBody, 584, 12, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n    <meta property=\"og:description\" name=\"description\"");
                BeginWriteAttribute("content", " content=\"", 656, "\"", 678, 1);
#nullable restore
#line 19 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
WriteAttributeValue("", 666, contentBody, 666, 12, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n    <meta property=\"og:image\" name=\"image\"");
                BeginWriteAttribute("content", " content=\"", 726, "\"", 742, 1);
#nullable restore
#line 20 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
WriteAttributeValue("", 736, image, 736, 6, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n\r\n");
            }
            );
            WriteLiteral("\r\n<script>\r\n");
            WriteLiteral(@"    (function(d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) return;
        js = d.createElement(s);
        js.id = id;
        js.src = ""//connect.facebook.net/en_US/all.js#xfbml=1&appId=239242753967982"";
        fjs.parentNode.insertBefore(js, fjs);
    }(document, 'script', 'facebook-jssdk'));
</script>


<h1 style=""font-family: fangsong"">");
#nullable restore
#line 42 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
                             Write(Model.Magazine.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</h1>

<nav class=""navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-magazine-menu  mb-3"" style=""padding-left: 0px"">
    <div class=""container"" style=""padding-left: 0px"">
        <button class=""navbar-toggler"" type=""button"" data-toggle=""collapse"" data-target="".magazie-collapse"" aria-controls=""navbarSupportedContent""
                aria-expanded=""false"" aria-label=""Toggle navigation"">
            <span class=""navbar-toggler-icon""></span>
        </button>
        <div class=""navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse magazie-collapse"">
            <ul class=""navbar-nav flex-grow-1"">
");
#nullable restore
#line 52 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
                 foreach (var category in Model.ContentCategories)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <li class=\"nav-item magazine-nav-item\">\r\n                        <a class=\"nav-link\"");
            BeginWriteAttribute("href", " href=\"", 2325, "\"", 2353, 2);
            WriteAttributeValue("", 2332, "#", 2332, 1, true);
#nullable restore
#line 55 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
WriteAttributeValue("", 2333, category.CategoryId, 2333, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                            ");
#nullable restore
#line 56 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
                       Write(category.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </a>\r\n                    </li>\r\n");
#nullable restore
#line 59 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li class=\"nav-item magazine-nav-item\">\r\n                    <a class=\"nav-link\" href=\"#feedback\">\r\n                        Leave feedback\r\n                    </a>\r\n                </li>\r\n\r\n");
#nullable restore
#line 66 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
                 if (Model.IsAdmin)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <li class=\"nav-item magazine-nav-item\">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "366441483fa07d629a99bf461225e84149448cff10934", async() => {
                WriteLiteral("Magazine Manager");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </li>\r\n");
#nullable restore
#line 71 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </ul>\r\n        </div>\r\n    </div>\r\n</nav>\r\n");
            WriteLiteral("\r\n<div class=\"form-group \">\r\n");
#nullable restore
#line 83 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
     foreach (var category in Model.ContentCategories)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"row NewNewsFeedSeperator\"");
            BeginWriteAttribute("id", " id=\"", 3404, "\"", 3429, 1);
#nullable restore
#line 85 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
WriteAttributeValue("", 3409, category.CategoryId, 3409, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n            <img");
            BeginWriteAttribute("src", " src=\"", 3449, "\"", 3551, 1);
#nullable restore
#line 86 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
WriteAttributeValue("", 3455, Url.Action("GetImage", new {filename =  @category.IconImage, section = "magazinecategoryicon"}), 3455, 96, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 3552, "\"", 3573, 1);
#nullable restore
#line 86 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
WriteAttributeValue(" ", 3558, category.Name, 3559, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n\r\n        </div>\r\n");
            WriteLiteral("        <div class=\"content-group\">\r\n\r\n");
#nullable restore
#line 92 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
             foreach (var content in Model.Magazine.Contents.Where(c => c.Category.CategoryId == category.CategoryId))
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"row float-left content-card\">\r\n                    ");
#nullable restore
#line 95 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
               Write(Html.Partial("_MagazineCard", content));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n");
#nullable restore
#line 97 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n");
#nullable restore
#line 99 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n\r\n\r\n<div class=\"row NewNewsFeedSeperator\" id=\"feedback\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "366441483fa07d629a99bf461225e84149448cff15725", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n</div>\r\n<div class=\"form-group \">\r\n    ");
#nullable restore
#line 108 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Magazine\CurrentEdition.cshtml"
Write(Html.Partial("_Feedback", new FeedbackModel { FeedbackOn = @Model.Magazine.Name }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n</div>\r\n\r\n<div id=\"fb-root\"></div>\r\n\r\n\r\n\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "366441483fa07d629a99bf461225e84149448cff17255", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BMS_dotnet_WebApplication.Models.MagazineVM.CurrentEditionVM> Html { get; private set; }
    }
}
#pragma warning restore 1591
