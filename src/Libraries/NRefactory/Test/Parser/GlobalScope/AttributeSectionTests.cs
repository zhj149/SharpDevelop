﻿// <file>
//     <copyright see="prj:///doc/copyright.txt">2002-2005 AlphaSierraPapa</copyright>
//     <license see="prj:///doc/license.txt">GNU General Public License</license>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.IO;
using NUnit.Framework;
using ICSharpCode.NRefactory.Parser;
using ICSharpCode.NRefactory.Parser.VB;
using ICSharpCode.NRefactory.Parser.AST;

namespace ICSharpCode.NRefactory.Tests.AST
{
	[TestFixture]
	public class AttributeSectionTests
	{
		[Test]
		public void AttributeOnStructure()
		{
			string program = @"
<StructLayout( LayoutKind.Explicit )> _
Public Structure MyUnion

	<FieldOffset( 0 )> Public i As Integer
	< FieldOffset( 0 )> Public d As Double
	
End Structure 'MyUnion
";
			TypeDeclaration decl = (TypeDeclaration)ParseUtilVBNet.ParseGlobal(program, typeof(TypeDeclaration));
			Assert.AreEqual("StructLayout", decl.Attributes[0].Attributes[0].Name);
		}
		
		[Test]
		public void AttributeOnModule()
		{
			string program = @"
<HideModule> _
Public Module MyExtra

	Public i As Integer
	Public d As Double
	
End Module
";
			TypeDeclaration decl = (TypeDeclaration)ParseUtilVBNet.ParseGlobal(program, typeof(TypeDeclaration));
			Assert.AreEqual("HideModule", decl.Attributes[0].Attributes[0].Name);
		}
		
		[Test]
		public void GlobalAttributeVB()
		{
			string program = @"<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class Form1
	
End Class";
			TypeDeclaration decl = (TypeDeclaration)ParseUtilVBNet.ParseGlobal(program, typeof(TypeDeclaration));
			Assert.AreEqual("Microsoft.VisualBasic.CompilerServices.DesignerGenerated", decl.Attributes[0].Attributes[0].Name);
		}
		
		[Test]
		public void GlobalAttributeCSharp()
		{
			string program = @"[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
[someprefix::DesignerGenerated()]
public class Form1 {
}";
			TypeDeclaration decl = (TypeDeclaration)ParseUtilCSharp.ParseGlobal(program, typeof(TypeDeclaration));
			Assert.AreEqual("Microsoft.VisualBasic.CompilerServices.DesignerGenerated", decl.Attributes[0].Attributes[0].Name);
			Assert.AreEqual("someprefix.DesignerGenerated", decl.Attributes[1].Attributes[0].Name);
		}
	}
}
