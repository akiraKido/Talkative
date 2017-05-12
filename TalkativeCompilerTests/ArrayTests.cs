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
    public class ArrayTests
    {


        [TestMethod]
        public void ArrayTest ()
        {
            var test = "#(10 5)";
            var expected = new List<Token>
            {
                new Token( TokenType.ArrayStart, "(" ),
                new Token( TokenType.Numeral, "10" ),
                new Token( TokenType.Numeral, "5" ),
                new Token( TokenType.ArrayEnd, ")" ),
            };
            TalkativeTests.TryTest( test, expected );

        }

        [TestMethod]
        public void ArrayTest2 ()
        {
            var test = "#(10 5).";
            var expected = new List<Token>
            {
                new Token( TokenType.ArrayStart, "(" ),
                new Token( TokenType.Numeral, "10" ),
                new Token( TokenType.Numeral, "5" ),
                new Token( TokenType.ArrayEnd, ")" ),
            };
            TalkativeTests.TryTest( test, expected );
        }

        [TestMethod]
        public void ArrayTest3 ()
        {
            var test = "x := #(10 5).";
            var expected = new List<Token>
            {
                new Token( TokenType.Identifier, "x" ),
                new Token( TokenType.AssignmentOperator, ":=" ),
                new Token( TokenType.ArrayStart, "(" ),
                new Token( TokenType.Numeral, "10" ),
                new Token( TokenType.Numeral, "5" ),
                new Token( TokenType.ArrayEnd, ")" ),
            };
            TalkativeTests.TryTest( test, expected );
        }

        [TestMethod]
        public void ArrayTest4 ()
        {
            var test = "x := #(10 'SmallTalk').";
            var expected = new List<Token>
            {
                new Token( TokenType.Identifier, "x" ),
                new Token( TokenType.AssignmentOperator, ":=" ),
                new Token( TokenType.ArrayStart, "(" ),
                new Token( TokenType.Numeral, "10" ),
                new Token( TokenType.StringLiteral, "SmallTalk" ),
                new Token( TokenType.ArrayEnd, ")" ),
            };
            TalkativeTests.TryTest( test, expected );


        }

        [TestMethod]
        public void ArrayTest5 ()
        {
            var test = "x := #(10 'SmallTalk' ).";
            var expected = new List<Token>
            {
                new Token( TokenType.Identifier, "x" ),
                new Token( TokenType.AssignmentOperator, ":=" ),
                new Token( TokenType.ArrayStart, "(" ),
                new Token( TokenType.Numeral, "10" ),
                new Token( TokenType.StringLiteral, "SmallTalk" ),
                new Token( TokenType.ArrayEnd, ")" ),
            };
            TalkativeTests.TryTest( test, expected );
        }

        [TestMethod]
        public void ArrayInsideArray()
        {
            var test = "x := #( #(1 10) 10 ).";
            var expected = new List<Token>
            {
                new Token( TokenType.Identifier, "x" ),
                new Token( TokenType.AssignmentOperator, ":=" ),
                new Token( TokenType.ArrayStart, "(" ),
                new Token( TokenType.ArrayStart, "(" ),
                new Token( TokenType.Numeral, "1" ),
                new Token( TokenType.Numeral, "10" ),
                new Token( TokenType.ArrayEnd, ")" ),
                new Token( TokenType.Numeral, "10" ),
                new Token( TokenType.ArrayEnd, ")" ),
            };
            TalkativeTests.TryTest( test, expected );

        }
    }
}
