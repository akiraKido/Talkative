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
    public class BlockTests
    {
        [TestMethod]
        public void BlockTest1()
        {

            var test = "block := [:x :y | x + y].";
            var expected = new List<Token>
            {
                new Token( TokenType.Identifier, "block" ),
                new Token( TokenType.AssignmentOperator, ":=" ),
                new Token( TokenType.BlockStart, "[" ),
                new Token( TokenType.BlockArgument, "x" ),
                new Token( TokenType.BlockArgument, "y" ),
                new Token( TokenType.BlockSeparator, "|" ),
                new Token( TokenType.Identifier, "x" ),
                new Token( TokenType.Identifier, "+" ),
                new Token( TokenType.Identifier, "y" ),
                new Token( TokenType.BlockEnd, "]" ),
            };
            TalkativeTests.TryTest( test, expected );
        }
    }
}
