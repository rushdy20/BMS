#pragma checksum "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "723c0eb9a1971d92a51c2a38b9a45f19af05b495"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__BookCard), @"mvc.1.0.view", @"/Views/Shared/_BookCard.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"723c0eb9a1971d92a51c2a38b9a45f19af05b495", @"/Views/Shared/_BookCard.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d3c4039b0fdcc97212a713b1acd2623d6a084b9a", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__BookCard : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BMS.BooksLibrary.BusinessLayer.Models.BookModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"card card-book\">\r\n    <div class=\"book-card-img\">\r\n        <img");
            BeginWriteAttribute("src", " src=\"", 133, "\"", 218, 1);
#nullable restore
#line 5 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
WriteAttributeValue("", 139, Url.Action("GetImage", new {filename = "booksimg/" + Model.MainImageFileName}), 139, 79, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" style=\"height: 125px;\" alt=\"...\">\r\n    </div>\r\n   \r\n    <div class=\"card-body\"");
            BeginWriteAttribute("id", " id=\"", 298, "\"", 317, 1);
#nullable restore
#line 8 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
WriteAttributeValue("", 303, Model.Barcode, 303, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n        <h5 class=\"card-title\"> ");
#nullable restore
#line 9 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
                           Write(Model.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n");
#nullable restore
#line 10 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
         if (Model.BookCategory != null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <p class=\"card-text\">Category : ");
#nullable restore
#line 12 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
                                       Write(Model.BookCategory.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 13 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p class=\"card-text\">Barcode : ");
#nullable restore
#line 14 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
                                  Write(Model.Barcode);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 15 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
         if (!string.IsNullOrEmpty(Model.Description))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <p class=\"card-text\">Description : ");
#nullable restore
#line 17 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
                                          Write(Model.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 18 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
         if (!string.IsNullOrEmpty(Model.Comments))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <p class=\"card-text\">Comment : ");
#nullable restore
#line 21 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
                                      Write(Model.Comments);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 22 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 23 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
         if (!string.IsNullOrEmpty(Model.Publisher))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <p class=\"card-text\">Publisher : ");
#nullable restore
#line 25 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
                                        Write(Model.Publisher);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 26 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 28 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
         if (Model.IsAvailable)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <a href=\"#\" class=\"btn btn-primary\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1093, "\"", 1130, 3);
            WriteAttributeValue("", 1103, "addToBucket(", 1103, 12, true);
#nullable restore
#line 30 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
WriteAttributeValue("", 1115, Model.Barcode, 1115, 14, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1129, ")", 1129, 1, true);
            EndWriteAttribute();
            WriteLiteral(">Add to Basket</a>\r\n");
            WriteLiteral("            <input type=\"hidden\"");
            BeginWriteAttribute("id", " id=\"", 1185, "\"", 1215, 2);
            WriteAttributeValue("", 1190, "_BookTitle_", 1190, 11, true);
#nullable restore
#line 32 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
WriteAttributeValue("", 1201, Model.Barcode, 1201, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("name", " name=\"", 1216, "\"", 1248, 2);
            WriteAttributeValue("", 1223, "_BookTitle_", 1223, 11, true);
#nullable restore
#line 32 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
WriteAttributeValue("", 1234, Model.Barcode, 1234, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 1249, "\"", 1269, 1);
#nullable restore
#line 32 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
WriteAttributeValue("", 1257, Model.Title, 1257, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("/>\r\n            <input type=\"hidden\"");
            BeginWriteAttribute("id", " id=\"", 1306, "\"", 1338, 2);
            WriteAttributeValue("", 1311, "_BookBarcode_", 1311, 13, true);
#nullable restore
#line 33 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
WriteAttributeValue("", 1324, Model.Barcode, 1324, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("name", " name=\"", 1339, "\"", 1373, 2);
            WriteAttributeValue("", 1346, "_BookBarcode_", 1346, 13, true);
#nullable restore
#line 33 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
WriteAttributeValue("", 1359, Model.Barcode, 1359, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 1374, "\"", 1396, 1);
#nullable restore
#line 33 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
WriteAttributeValue("", 1382, Model.Barcode, 1382, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("/>\r\n            <input type=\"hidden\"");
            BeginWriteAttribute("id", " id=\"", 1433, "\"", 1463, 2);
            WriteAttributeValue("", 1438, "_BookImage_", 1438, 11, true);
#nullable restore
#line 34 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
WriteAttributeValue("", 1449, Model.Barcode, 1449, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("name", " name=\"", 1464, "\"", 1496, 2);
            WriteAttributeValue("", 1471, "_BookImage_", 1471, 11, true);
#nullable restore
#line 34 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
WriteAttributeValue("", 1482, Model.Barcode, 1482, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 1497, "\"", 1529, 1);
#nullable restore
#line 34 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
WriteAttributeValue("", 1505, Model.MainImageFileName, 1505, 24, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("/>\r\n");
#nullable restore
#line 35 "C:\Learning\BMS\BMS-dotnet-WebApplication\Views\Shared\_BookCard.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    </div>
</div>

<script>
    function addToBucket(barcode) {
        $.post(""/Library/AddBooksToCart"",
            {
                Title: $('#_BookTitle_' + barcode).val(),
                Barcode: $('#_BookBarcode_' + barcode).val(),
                MainImageFileName: $('#_BookImage_' + barcode).val()
            },
            function(data) {

                $("".booksCount"").text(data);
                $(""#divBookSelected"").css(""display"", ""block"");;

            });
    };

</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BMS.BooksLibrary.BusinessLayer.Models.BookModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
