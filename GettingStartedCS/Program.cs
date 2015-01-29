using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace GettingStartedCS
{
    class Program
    {
        private static void Main(string[] args)
        {
            var tree = CSharpSyntaxTree.ParseText(
                @"using System;
                using System.Collections;
                using System.Linq;
                using System.Text;

                namespace HelloWorld
                {
                    class Program
                    {
                        static void Main(string[] args)
                        {
                            Console.WriteLine(""Hello, world!"");
                        }
                    }
                }");
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var firstMember = root.Members[0];
            var helloWorldDeclaration = (NamespaceDeclarationSyntax) firstMember;
            var programDeclaration = (ClassDeclarationSyntax) helloWorldDeclaration.Members[0];
            var mainDeclaration = (MethodDeclarationSyntax) programDeclaration.Members[0];
            var argsParameter = mainDeclaration.ParameterList.Parameters[0];
            var firstParameters = from methodDeclaration in root.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                where methodDeclaration.Identifier.ValueText == "main"
                select methodDeclaration.ParameterList.Parameters.First();
            var argsParameter2 = firstParameters.Single();
        }
    }
}
