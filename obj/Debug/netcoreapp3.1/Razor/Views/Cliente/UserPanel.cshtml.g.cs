#pragma checksum "C:\Users\migue\source\repos\HipercorWeb\Views\Cliente\UserPanel.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c051711f9ce0acbaed34d868e9a119b0f282772a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cliente_UserPanel), @"mvc.1.0.view", @"/Views/Cliente/UserPanel.cshtml")]
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
#line 1 "C:\Users\migue\source\repos\HipercorWeb\Views\_ViewImports.cshtml"
using HipercorWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\migue\source\repos\HipercorWeb\Views\_ViewImports.cshtml"
using HipercorWeb.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c051711f9ce0acbaed34d868e9a119b0f282772a", @"/Views/Cliente/UserPanel.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"43aae5f7b0ea3f5cb5e4c86f658b490f1043798e", @"/Views/_ViewImports.cshtml")]
    public class Views_Cliente_UserPanel : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Cliente>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DatosPersonales", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-primary btn-block"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Direcciones", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 2 "C:\Users\migue\source\repos\HipercorWeb\Views\Cliente\UserPanel.cshtml"
  
    ViewData["Title"] = "Panel de usuario";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"card mx-auto\" style=\"width: 18rem;\">\r\n    <div class=\"card-body\">\r\n        <h5 class=\"card-title\">Datos de la cuenta</h5>\r\n        <div class=\"card-text\"><strong>Email: </strong>");
#nullable restore
#line 9 "C:\Users\migue\source\repos\HipercorWeb\Views\Cliente\UserPanel.cshtml"
                                                  Write(Model.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n    </div>\r\n</div>\r\n<div class=\"card mx-auto\" style=\"width: 18rem;\">\r\n    <div class=\"card-body\">\r\n        <h5 class=\"card-title\">Datos personales</h5>\r\n        <div class=\"card-text\"><strong>Nombre: </strong>");
#nullable restore
#line 15 "C:\Users\migue\source\repos\HipercorWeb\Views\Cliente\UserPanel.cshtml"
                                                   Write(Model.DatosPersonales.Nombre);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n        <div class=\"card-text\"><strong>Apellidos: </strong>");
#nullable restore
#line 16 "C:\Users\migue\source\repos\HipercorWeb\Views\Cliente\UserPanel.cshtml"
                                                      Write(Model.DatosPersonales.Apellidos);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n        <div class=\"card-text\"><strong>Movil: </strong>");
#nullable restore
#line 17 "C:\Users\migue\source\repos\HipercorWeb\Views\Cliente\UserPanel.cshtml"
                                                  Write(Model.DatosPersonales.Movil);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
#nullable restore
#line 18 "C:\Users\migue\source\repos\HipercorWeb\Views\Cliente\UserPanel.cshtml"
         if (@Model.DatosPersonales.Fijo != null)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"card-text\"><strong>Movil: </strong>");
#nullable restore
#line 20 "C:\Users\migue\source\repos\HipercorWeb\Views\Cliente\UserPanel.cshtml"
                                                      Write(Model.DatosPersonales.Fijo);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
#nullable restore
#line 21 "C:\Users\migue\source\repos\HipercorWeb\Views\Cliente\UserPanel.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c051711f9ce0acbaed34d868e9a119b0f282772a6733", async() => {
                WriteLiteral("Editar");
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
            WriteLiteral("\r\n    </div>\r\n</div>\r\n<div class=\"card mx-auto\" style=\"width: 18rem;\">\r\n    <div class=\"card-body\">\r\n        <h5 class=\"card-title\">Direcciones: ");
#nullable restore
#line 27 "C:\Users\migue\source\repos\HipercorWeb\Views\Cliente\UserPanel.cshtml"
                                       Write(Model.Direcciones.Count);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n        <div class=\"card-text\"><strong>Provincia: </strong>");
#nullable restore
#line 28 "C:\Users\migue\source\repos\HipercorWeb\Views\Cliente\UserPanel.cshtml"
                                                      Write(Model.Direcciones[0].Provincia.nm);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n        <div class=\"card-text\"><strong>Municipio: </strong>");
#nullable restore
#line 29 "C:\Users\migue\source\repos\HipercorWeb\Views\Cliente\UserPanel.cshtml"
                                                      Write(Model.Direcciones[0].Municipio.nm);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n        <div class=\"card-text\"><strong>Código postal: </strong>");
#nullable restore
#line 30 "C:\Users\migue\source\repos\HipercorWeb\Views\Cliente\UserPanel.cshtml"
                                                          Write(Model.Direcciones[0].CodigoPostal);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n        <div class=\"card-text\"><strong>Calle: </strong>");
#nullable restore
#line 31 "C:\Users\migue\source\repos\HipercorWeb\Views\Cliente\UserPanel.cshtml"
                                                  Write(Model.Direcciones[0].Calle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c051711f9ce0acbaed34d868e9a119b0f282772a9799", async() => {
                WriteLiteral("Editar");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Cliente> Html { get; private set; }
    }
}
#pragma warning restore 1591
