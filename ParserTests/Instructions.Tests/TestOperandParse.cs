using NUnit.Framework;
using Instructions;

namespace Instructions.Tests
{
    public class TestOperandParse
    {
        private InstructionParser parser;

        [SetUp]
        public void Setup()
        {
            parser = new InstructionParser();
        }

        [TestCase("0x45", 0x45)]
        [TestCase("0x00", 0)]
        [TestCase("0xff", 255)]
        public void TestHexNumber(string operandText, int parsedValue)
        {
            var (operand, error) = parser.RecognizeOperand(operandText);
            Assert.AreEqual(ParseError.NO_ERROR, error);
            Assert.AreEqual(parsedValue, operand.value);
        }

        [TestCase("0xrr", ParseError.INVALID_NUMBER)]
        [TestCase("0xffq", ParseError.INVALID_NUMBER)]
        [TestCase("0x100", ParseError.NUMBER_OUT_OF_RANGE)]
        [TestCase("0x-1", ParseError.INVALID_NUMBER)]
        public void TestHexIncorectNumber(string operandText, ParseError expectedError)
        {
            var (operand, error) = parser.RecognizeOperand(operandText);
            Assert.AreEqual(expectedError, error);
        }
    }
}