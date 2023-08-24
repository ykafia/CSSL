// See https://aka.ms/new-console-template for more information

using CSSL.Experiments;
using SoftTouch.Spirv;

ParentShader.Emit();
MyShader.Emit();
OtherShader.Emit();

Console.WriteLine(
    MixinSourceProvider.Get("ParentShader").Disassemble()
);