using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SourceGeneratorUtils;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CSSL.Generators
{

    [Generator]
    public class ShaderSourceGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
//#if DEBUG
//            if (!Debugger.IsAttached)
//            {
//                Debugger.Launch();
//            }
//#endif
            // No initialization required for this one
        }


        public void Execute(GeneratorExecutionContext context)
        {
            // Code generation goes here

            var assembly = context.Compilation.Assembly;
            var shaderClasses = 
                GetAllTypes(assembly.GlobalNamespace)
                .Where(
                    t => t.BaseType.Name.Contains("CSShader")
                )
                .ToList();
            var initializer = SpirvMethodWriter.ModuleInitializer("SpirvModuleInitializer", shaderClasses);
            foreach(var c in shaderClasses)
            {
                var code = SpirvMethodWriter.Method(c.ContainingNamespace.OriginalDefinition.ToString(), c);
                context.AddSource($"{c.Name}.g.cs", code);
            }
        }


        public static void GenerateSpirv()
        {

        }


        
        private static IEnumerable<ITypeSymbol> GetAllTypes(INamespaceSymbol root)
        {
            foreach (var namespaceOrTypeSymbol in root.GetMembers())
            {
                if (namespaceOrTypeSymbol is INamespaceSymbol @namespace) foreach (var nested in GetAllTypes(@namespace)) yield return nested;

                else if (namespaceOrTypeSymbol is ITypeSymbol type)
                {
                    foreach (var t in type.GetTypeMembers())
                        yield return t;
                    yield return type;
                }
            }
        }

    }
    
}