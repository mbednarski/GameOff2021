using NUnit.Framework;
using Instructions;

namespace Instructions.Tests
{
    public class Test0op
    {
        private InstructionParser parser;

        [SetUp]
        public void Setup()
        {
            parser = new InstructionParser();
        }

        [TestCase("")]
        [TestCase("    ")]
        [TestCase("   \t   ")]
        public void ErrorOnEmpty(string value)
        {
            var (instruction, error) = parser.Parse(value);
            Assert.AreEqual(ParseError.EMPTY_INSTRUCTION, error);
        }
    }
}