using Microsoft.CodeAnalysis;
using SourceGeneratorUtils;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CSSL.Generators;

internal static class SpirvMethodWriter
{
    public static string Method(string nspace, ITypeSymbol classSymbol)
    {
        var writer = new SourceWriter();
        writer.WriteLine($"namespace {nspace};")
            .WriteEmptyLines(2)
            .WriteLine("using CSSL.Core;")
            .WriteLine("using SoftTouch.Spirv;")
            .WriteLine("using SoftTouch.Spirv.Core;")
            .WriteLine("using SoftTouch.Spirv.Core.Buffers;")
            .WriteEmptyLines(2)
            .WriteLine($"{classSymbol.DeclaredAccessibility.ToString().ToLower()} partial class {classSymbol.Name} : ISpirvEmitter")
            .OpenBlock()
            .WriteLine("public static void Emit()")
            .OpenBlock()
            .WriteLine($"var mixer = new Mixer(\"{classSymbol.Name}\").WithType(\"int\").Build();")
            .CloseAllBlocks();
        return writer.ToString();
    }
    
    public static string ModuleInitializer(string nspace, List<ITypeSymbol> classes)
    {
        var writer = new SourceWriter();
        writer.WriteLine($"namespace {nspace};")
            .WriteLine("using System.Runtime.CompilerServices;");
        foreach (var c in classes)
            writer.WriteLine($"using {c.ContainingNamespace};");

        writer
            .WriteLine("internal static class ModuleInitializer")
            .OpenBlock()
            .WriteLine("[ModuleInitializer]")
            .WriteLine("internal static void InitializeSpirvCodes()")
            .OpenBlock();
        foreach(var c  in classes)
        {
            writer.WriteLine($"{c.Name}.Emit();");
        }
        return writer.CloseBlock().ToString();
    }

}
