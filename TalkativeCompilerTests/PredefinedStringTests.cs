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
    public class PredefinedStringTests
    {
        [TestMethod]
        public void PredefinedStringTest()
        {
            var test = "true";
            var expected = new List<Token>
            {
                new Token( TokenType.Predefined, "true" )
            };
            TalkativeTests.TryTest( test, expected );
        }

        [TestMethod]
        public void PredefinedStringInputTest ()
        {
            var test = "x := true";
            var expected = new List<Token>
            {
                new Token( TokenType.Identifier, "x" ),
                new Token( TokenType.AssignmentOperator, ":=" ),
                new Token( TokenType.Predefined, "true" )
            };
            TalkativeTests.TryTest( test, expected );
        }
    }
}
