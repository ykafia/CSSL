using Microsoft.CodeAnalysis;
using System.Diagnostics;

namespace CSSL.Generators
{

    [Generator]
    public class ShaderSourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            // Code generation goes here
            context.AddSource("MyClass.g.cs", "namespace CSSL.Experiments{ public record class HelloWorld(); }");
        }

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
    }
}