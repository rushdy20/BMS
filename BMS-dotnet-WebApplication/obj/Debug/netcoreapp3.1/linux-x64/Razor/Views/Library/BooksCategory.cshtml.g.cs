#pragma checksum "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\BooksCategory.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "76f595dd217a6fbcf65e1c66f93ba37ed5fac8eb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Library_BooksCategory), @"mvc.1.0.view", @"/Views/Library/BooksCategory.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"76f595dd217a6fbcf65e1c66f93ba37ed5fac8eb", @"/Views/Library/BooksCategory.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d3c4039b0fdcc97212a713b1acd2623d6a084b9a", @"/Views/_ViewImports.cshtml")]
    public class Views_Library_BooksCategory : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BMS_dotnet_WebApplication.Models.LibraryVM.AdminBooksCategoryVM>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_ValidationScriptsPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\BooksCategory.cshtml"
  
    ViewData["Title"] = "Books Genre";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Register new Books Genre</h1>\r\n\r\n");
#nullable restore
#line 9 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\BooksCategory.cshtml"
 using (Html.BeginForm("AddCategory", "Library", FormMethod.Post, new { @id = "partialform" }))
{
   

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 13 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\BooksCategory.cshtml"
       Write(Html.LabelFor(m => m.BooksCategory.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 14 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\BooksCategory.cshtml"
       Write(Html.TextBoxFor(m => m.BooksCategory.Name, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 15 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\BooksCategory.cshtml"
       Write(Html.ValidationMessageFor(m => m.BooksCategory.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 18 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\BooksCategory.cshtml"
       Write(Html.LabelFor(m => m.BooksCategory.Comment));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 19 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\BooksCategory.cshtml"
       Write(Html.TextBoxFor(m => m.BooksCategory.Comment, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            \r\n        </div>\r\n        <div class=\"form-group \">\r\n            <button type=\"submit\" class=\"btn btn-primary\">Add</button>\r\n        </div>\r\n");
            WriteLiteral("        <div class=\"col-sm-12\">\r\n            <ul>\r\n                <li>\r\n                    Books Categories\r\n                    <ul>\r\n");
#nullable restore
#line 33 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\BooksCategory.cshtml"
                         foreach (var category in Model.ExistingCategories)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <li>\r\n                                ");
#nullable restore
#line 36 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\BooksCategory.cshtml"
                           Write(category.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" -- ");
#nullable restore
#line 36 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\BooksCategory.cshtml"
                                             Write(category.Comment);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </li>\r\n");
#nullable restore
#line 38 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\BooksCategory.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </ul>\r\n                </li>\r\n            </ul>\r\n        </div>\r\n        <div class=\"form-group\">\r\n            ");
#nullable restore
#line 44 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\BooksCategory.cshtml"
       Write(Html.ActionLink("Save", "Save", "Library", null, new { @class = "btn btn-success" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n");
#nullable restore
#line 46 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\BooksCategory.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "76f595dd217a6fbcf65e1c66f93ba37ed5fac8eb7763", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BMS_dotnet_WebApplication.Models.LibraryVM.AdminBooksCategoryVM> Html { get; private set; }
    }
}
#pragma warning restore 1591
