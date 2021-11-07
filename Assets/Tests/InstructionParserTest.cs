using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InstructionParserTest
{
    [Test]
    public void TestWhitespaceString()
    {
        var parser = new InstructionParser();   
        var text = "";
        var (instruction, error) = parser.Parse(text);

        Assert.NotNull(error);
    }
    [Test]
    public void TestNOOP()
    {
        var parser = new InstructionParser();   
        var text = "  NOOP ";
        var (instruction, error) = parser.Parse(text);

        Assert.IsNull(error);
        Assert.AreEqual("NOOP", instruction.mnemonic);
    }

    [Test]
    public void Test0argUnknown()
    {
        var parser = new InstructionParser();   
        var text = "  xD ";
        var (instruction, error) = parser.Parse(text);

        Assert.IsNull(instruction);
        Assert.NotNull(error);
    }

    [Test]
    public void Test1argRegistry()
    {
        var parser = new InstructionParser();   
        var text = "JMP AX";
        var (instruction, error) = parser.Parse(text);

        Assert.Null(error);
        Assert.NotNull(instruction);
        Assert.AreEqual("JMP", instruction.mnemonic);
        Assert.AreEqual("AX", instruction.operand1);
    }
    [Test]
    public void Test1argDecimal()
    {
        var parser = new InstructionParser();   
        var text = "JMP   17 ";
        var (instruction, error) = parser.Parse(text);

        Assert.IsNull(error);
        Assert.NotNull(instruction);
        Assert.AreEqual("JMP", instruction.mnemonic);
        Assert.AreEqual("17", instruction.operand1);
    }
    [Test]
    public void Test1argHex()
    {
        var parser = new InstructionParser();   
        var text = "JMP 0xAF";
        var (instruction, error) = parser.Parse(text);

        Assert.IsNull(error);
        Assert.NotNull(instruction);
        Assert.AreEqual("JMP", instruction.mnemonic);
        Assert.AreEqual("0xAF", instruction.operand1);
    }
    
    [Test]
    public void Test1argIncorrectArg()
    {
        var parser = new InstructionParser();   
        var text = "JMP asdf";
        var (instruction, error) = parser.Parse(text);

        Assert.NotNull(error);
        Assert.IsNull(instruction);
    }

    [Test]
    public void Test1argUnknown()
    {
        var parser = new InstructionParser();   
        var text = "JMP asdf";
        var (instruction, error) = parser.Parse(text);

        Assert.NotNull(error);
        Assert.IsNull(instruction);
    }

    [Test]
    public void Test2argUnknown()
    {
        var parser = new InstructionParser();   
        var text = "DD AX AX";
        var (instruction, error) = parser.Parse(text);

        Assert.NotNull(error);
        Assert.IsNull(instruction);
    }
    [Test]
    public void Test2argRegReg()
    {
        var parser = new InstructionParser();   
        var text = "MOV AX BX";
        var (instruction, error) = parser.Parse(text);

        Assert.IsNull(error);
        Assert.NotNull(instruction);

        Assert.AreEqual("MOV", instruction.mnemonic);
        Assert.AreEqual("AX", instruction.operand1);
        Assert.AreEqual("BX", instruction.operand2);
    }
    [Test]
    public void Test2argRegDec()
    {
        var parser = new InstructionParser();   
        var text = "MOV AX 42";
        var (instruction, error) = parser.Parse(text);

        Assert.IsNull(error);
        Assert.NotNull(instruction);

        Assert.AreEqual("MOV", instruction.mnemonic);
        Assert.AreEqual("AX", instruction.operand1);
        Assert.AreEqual("42", instruction.operand2);
    }
    [Test]
    public void Test2argRegHex()
    {
        var parser = new InstructionParser();   
        var text = "MOV IP 0xcc";
        var (instruction, error) = parser.Parse(text);

        Assert.IsNull(error);
        Assert.NotNull(instruction);

        Assert.AreEqual("MOV", instruction.mnemonic);
        Assert.AreEqual("IP", instruction.operand1);
        Assert.AreEqual("0xcc", instruction.operand2);
    }
    [Test]
    public void Test3arg()
    {
        var parser = new InstructionParser();   
        var text = "MOV XX XX XX";
        var (instruction, error) = parser.Parse(text);

        Assert.NotNull(error);
        Assert.IsNull(instruction);
    }

}
