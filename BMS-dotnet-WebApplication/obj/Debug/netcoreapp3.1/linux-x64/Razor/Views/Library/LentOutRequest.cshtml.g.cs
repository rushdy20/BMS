#pragma checksum "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cf5b388f9421de5834898147d7ab0467adf177d0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Library_LentOutRequest), @"mvc.1.0.view", @"/Views/Library/LentOutRequest.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cf5b388f9421de5834898147d7ab0467adf177d0", @"/Views/Library/LentOutRequest.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d3c4039b0fdcc97212a713b1acd2623d6a084b9a", @"/Views/_ViewImports.cshtml")]
    public class Views_Library_LentOutRequest : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<BMS.BusinessLayer.Library.Models.LendingRequestModel>>
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
#line 3 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
  
    var isRequest =(bool)ViewBag.isRequest;
    var Id = 0;
    var DueNow = "";


#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>Books lent out request</h1>\r\n<div id=\"accordion\">\r\n");
#nullable restore
#line 11 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
     foreach (var bookRequest in Model)
    {
        Id = Id + 1;
        DueNow = DateTime.Today > bookRequest.LentOn?.AddDays(14) ? "DueNow" : "";


#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"card\">\r\n            <div class=\"card-header\" id=\"headingOne\">\r\n                <h5 class=\"mb-0\">\r\n                    <button class=\"btn btn-link accordClick \"");
            BeginWriteAttribute("id", " id=", 552, "", 566, 1);
#nullable restore
#line 19 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
WriteAttributeValue("", 556, $"{Id}", 556, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" data-toggle=\"collapse\" data-target=");
#nullable restore
#line 19 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                                                                                                           Write($"{"#collapse"}{Id}");

#line default
#line hidden
#nullable disable
            WriteLiteral(" aria-expanded=\"true\"");
            BeginWriteAttribute("aria-controls", " aria-controls=", 646, "", 683, 1);
#nullable restore
#line 19 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
WriteAttributeValue("", 661, $"{"collapse"}{Id}", 661, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n");
#nullable restore
#line 20 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                        if (isRequest)
                       {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <span> ");
#nullable restore
#line 22 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                          Write(bookRequest.RequestedBy);

#line default
#line hidden
#nullable disable
            WriteLiteral(" has requested for ");
#nullable restore
#line 22 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                                                                     Write(bookRequest.BooksLent.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Books</span>\r\n");
#nullable restore
#line 23 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                       }
                       else
                       {

#line default
#line hidden
#nullable disable
            WriteLiteral("                          <span");
            BeginWriteAttribute("class", " class=\"", 981, "\"", 996, 1);
#nullable restore
#line 26 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
WriteAttributeValue("", 989, DueNow, 989, 7, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">  ");
#nullable restore
#line 26 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                                             Write(bookRequest.BooksLent.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Books have been lent to ");
#nullable restore
#line 26 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                                                                                                  Write(bookRequest.RequestedBy);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 27 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                       }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </button>\r\n                </h5>\r\n            </div>\r\n\r\n            <div");
            BeginWriteAttribute("id", " id=", 1206, "", 1232, 1);
#nullable restore
#line 33 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
WriteAttributeValue("", 1210, $"{"collapse"}{Id}", 1210, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"collapse\" aria-labelledby=\"headingOne\" data-parent=\"#accordion\">\r\n                <div class=\"card-body\">\r\n");
#nullable restore
#line 35 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                     using (Html.BeginForm("LentOut", "Library", FormMethod.Post))
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div class=\"form-group row\">\r\n");
#nullable restore
#line 38 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                             foreach (var t in bookRequest.BooksLent)
                            {


#line default
#line hidden
#nullable disable
            WriteLiteral("                                <div class=\"float-left\">\r\n                                    ");
#nullable restore
#line 42 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                               Write(Html.Partial("_BookCard", t));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </div>\r\n");
#nullable restore
#line 44 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"

                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </div>\r\n");
            WriteLiteral("                        <div class=\"form-group row\">\r\n                            <div class=\"col-md-4\">\r\n                                ");
#nullable restore
#line 50 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                           Write(Html.Label($"LentOn{Id}", "Lent On"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                ");
#nullable restore
#line 51 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                           Write(Html.TextBox($"LentOn{Id}", GetDate(bookRequest.LentOn), new { @class = $"form-control LentOn", autocomplete = "off" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                            </div>\r\n                            <div class=\"col-md-4\">\r\n                                ");
#nullable restore
#line 55 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                           Write(Html.Label($"ReturnedOn{Id}", "Returned On"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                ");
#nullable restore
#line 56 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                           Write(Html.TextBox($"ReturnedOn{Id}", "", new { @class = $"form-control ReturnedOn", autocomplete = "off" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                            </div>\r\n                        </div>\r\n                        <div class=\"form-check\">\r\n                            ");
#nullable restore
#line 61 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                       Write(Html.CheckBox("IsReadyToCollect",@bookRequest.IsReadyToCollect, new { @class = "form-check-input" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                           <label class=\"form-check-label\" for=\"IsReadyToCollect\">Ready to Collect</label>\r\n                        </div>\r\n                        <div class=\"form-group\">\r\n                            ");
#nullable restore
#line 65 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                       Write(Html.Label("Note", "Note"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            ");
#nullable restore
#line 66 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                       Write(Html.TextArea("Note", @bookRequest.Note, new {@class = "form-control"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </div>\r\n");
#nullable restore
#line 68 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                   Write(Html.Hidden("RequestedBy", bookRequest.RequestedBy));

#line default
#line hidden
#nullable disable
#nullable restore
#line 69 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                   Write(Html.Hidden("RequestedBy", bookRequest.RequestedBy));

#line default
#line hidden
#nullable disable
#nullable restore
#line 70 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                   Write(Html.Hidden("RequestedEmail", bookRequest.RequestedEmail));

#line default
#line hidden
#nullable disable
#nullable restore
#line 71 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                   Write(Html.Hidden("LendingRequestId", bookRequest.LendingRequestId));

#line default
#line hidden
#nullable disable
#nullable restore
#line 72 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                   Write(Html.Hidden("LentOn", ""));

#line default
#line hidden
#nullable disable
#nullable restore
#line 73 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                   Write(Html.Hidden("ReturnedOn", ""));

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <button type=\"submit\" class=\"btn btn-primary submit\">Register</button>\r\n");
#nullable restore
#line 76 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </div>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 80 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "cf5b388f9421de5834898147d7ab0467adf177d014799", async() => {
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
                WriteLiteral(@"
    <link rel=""stylesheet"" href=""https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"">
    <script src=""https://code.jquery.com/jquery-1.12.4.js""></script>
    <script src=""https://code.jquery.com/ui/1.12.1/jquery-ui.js""></script>
    <script>
       

        $('.accordClick').click(function() {
           
            var id = $(this).attr(""id"");

            $(""#LentOn"" + id).datepicker({
                dateFormat: 'dd-M-yy'
            });

            $(""#ReturnedOn"" + id).datepicker({
                dateFormat: 'dd-M-yy'
            });

        });

        $('.submit').click(function() {
            $(this).parent().find(""#LentOn"").val($(this).parent().find('.LentOn').val());
            $(this).parent().find(""#ReturnedOn"").val($(this).parent().find('.ReturnedOn').val());
        });
    </script>
");
            }
            );
            WriteLiteral("\r\n");
        }
        #pragma warning restore 1998
#nullable restore
#line 113 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Library\LentOutRequest.cshtml"
           

    private string GetDate(DateTime? date)
    {
        if (date == null)
            return null;

        var date1 = (DateTime) date;
        return date1.ToString("dd-M-yy");

        
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<BMS.BusinessLayer.Library.Models.LendingRequestModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
