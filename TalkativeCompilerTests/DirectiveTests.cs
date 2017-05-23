using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TalkativeCompiler;

namespace TalkativeCompilerTests
{
    [TestClass]
    public class DirectiveTests
    {
        [TestMethod]
        public void TestUsingDirective()
        {
            var test = "using System.";
            var expected = new List<Token>
            {
                new Token( TokenType.Predefined, "using" ),
                new Token( TokenType.Identifier, "System" )
            };
            TalkativeTests.TryTest( test, expected );
        }
    }
}
