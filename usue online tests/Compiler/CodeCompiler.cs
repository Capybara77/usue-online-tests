using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Test_Wrapper;
using System.Drawing;

namespace usue_online_tests.Compiler;

public class CodeCompiler
{
    public object CompileCode(string code, ref bool success)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);
        // define other necessary objects for compilation
        string assemblyName = Path.GetRandomFileName();

        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        List<MetadataReference> references = new List<MetadataReference>();

        foreach (var a in assemblies)
        {
            try
            {
                references.Add(MetadataReference.CreateFromFile(a.Location));
            }
            catch
            {
                // ignored
            }
        }

        //references.Add(MetadataReference.CreateFromFile(typeof(Color).Assembly.Location));
        //references.Add(MetadataReference.CreateFromFile(typeof(ZedGraph.AlignH).Assembly.Location));
        //references.Add(MetadataReference.CreateFromFile(typeof(RectangleF).Assembly.Location));
        //references.Add(MetadataReference.CreateFromFile(typeof(Bitmap).Assembly.Location));

        // analyse and generate IL code from syntax tree
        CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName,
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        using var ms = new MemoryStream();
        // write IL code into memory
        EmitResult result = compilation.Emit(ms);

        success = result.Success;

        if (!result.Success)
        {
            // handle exceptions
            IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                diagnostic.IsWarningAsError ||
                diagnostic.Severity == DiagnosticSeverity.Error);

            foreach (Diagnostic diagnostic in failures)
            {
                Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
            }

            throw new BadHttpRequestException(string.Concat(failures.Select(diagnostic => diagnostic.GetMessage())));
        }

        ms.Seek(0, SeekOrigin.Begin);
        Assembly assembly = Assembly.Load(ms.ToArray());

        Type type = assembly.GetType("usue_online_tests.Tests.List.ZedGraphExample");
        ITestCreator obj = Activator.CreateInstance(type) as ITestCreator;
        return obj.CreateTest(14);

        return type.InvokeMember("CreateTest",
            BindingFlags.Default | BindingFlags.InvokeMethod,
            null, obj, new object[] { 15 });

        throw new BadHttpRequestException("code error", 400);
    }
}