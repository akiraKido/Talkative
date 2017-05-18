using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TalkativeCompiler;

namespace TalkativeCompilerTests
{
    [TestClass()]
    public class TalkativeTests
    {
        [TestMethod()]
        public void AssignmentOperatorTest ()
        {
            var sourceCode = "x := 1.";

            var expected = new List<Token>
            {
                new Token( TokenType.Identifier, "x" ),
                new Token( TokenType.AssignmentOperator, ":=" ),
                new Token( TokenType.Numeral, "1" )
            };
            
            TryTest( sourceCode, expected );
        }

        [TestMethod]
        public void NumberLiteralTests()
        {
            var sourceCode = "10";
            var expected = new List<Token> {new Token( TokenType.Numeral, "10" )};
            TryTest( sourceCode,expected );

            sourceCode = "-10";
            expected = new List<Token> {new Token( TokenType.Numeral, "-10" )};
            TryTest( sourceCode, expected );

            sourceCode = "x := -10";
            expected = new List<Token>
            {
                new Token( TokenType.Identifier, "x" ),
                new Token( TokenType.AssignmentOperator, ":=" ),
                new Token( TokenType.Numeral, "-10" )
            };
            TryTest( sourceCode, expected );
        }

        [TestMethod]
        public void StringLiteralTest()
        {
            const string test1 = "'Smalltalk'";

            var expected1 = new List<Token>
            {
                new Token( TokenType.StringLiteral, "Smalltalk" )
            };

            TryTest( test1, expected1 );

            const string test2 = "'I''m a Smalltalker.'";
            var expected2 = new List<Token>
            {
                new Token( TokenType.StringLiteral, "I'm a Smalltalker." )
            };

            TryTest( test2, expected2 );
        }

        [TestMethod]
        public void StringConnectionTest()
        {
            const string test = "'Small','talk'";
            var expected = new List<Token>
            {
                new Token( TokenType.StringLiteral, "Smalltalk" )
            };
            TryTest( test, expected );
        }

        [TestMethod]
        public void CommentOutTest()
        {
            const string test = "\"This is a test\"";
            var expected = new List<Token>();
            TryTest( test, expected );
        }

        [TestMethod]
        public void SymbolTest()
        {
            var test = "#hoge";
            var expected = new List<Token> { new Token( TokenType.Symbol, "hoge" ) };
            TryTest( test, expected );

            test = "#hoge#fuga";
            expected = new List<Token>
            {
                new Token( TokenType.Symbol, "hoge" ),
                new Token( TokenType.Symbol, "fuga" ),
            };
            TryTest( test, expected );

            test = "#hoge" + Environment.NewLine + "#fuga";
            expected = new List<Token>
            {
                new Token( TokenType.Symbol, "hoge" ),
                new Token( TokenType.Symbol, "fuga" ),
            };
            TryTest( test, expected );

        }

        [TestMethod]
        public void TotalTest1()
        {
            string test = "\"this is a test\"" + Environment.NewLine +
                          "x := 10." + Environment.NewLine +
                          "y := 'Small' , 'Talker'.";
            var expected = new List<Token>
            {
                new Token( TokenType.Identifier, "x" ),
                new Token( TokenType.AssignmentOperator, ":=" ),
                new Token( TokenType.Numeral, "10" ),
                new Token( TokenType.LineBreak, "." ),
                new Token( TokenType.Identifier, "y" ),
                new Token( TokenType.AssignmentOperator, ":=" ),
                new Token( TokenType.StringLiteral, "SmallTalker" )
            };

            TryTest( test, expected );
        }

        internal static void TryTest(string sourceCode, List<Token> expected)
        {
            var actual = Talkative.GetTokens( sourceCode );
            AssertEqual( expected, actual );

        }

        private static void AssertEqual( List<Token> expected, IEnumerable<Token> actual )
        {
            Assert.AreEqual( expected.Count, actual.Count() );

            int i = 0;
            foreach ( Token token in actual )
            {
                Debug.WriteLine( $"actual token: {token.Value}" );
                Assert.AreEqual( expected[i].Type, token.Type );
                Assert.AreEqual( expected[i].Value, token.Value );
                i++;
            }
        }
    }
}