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
    public class MethodDefineTests
    {
        [TestMethod]
        public void ReturnTest()
        {
            var test = "^ 1 + 2.";
            var expected = new List<Token>
            {
                new Token( TokenType.Return, "^" ),
                new Token( TokenType.Numeral, "1" ),
                new Token( TokenType.Identifier, "+" ),
                new Token( TokenType.Numeral, "2" ),
            };
            TalkativeTests.TryTest( test, expected );
        }

        [TestMethod]
        public void MethodCallTest()
        {
            var test = "Object subclass: #john";
            var expected = new List<Token>
            {
                new Token( TokenType.Identifier, "Object" ),
                new Token( TokenType.MessageKeyword, "subclass" ),
                new Token( TokenType.Symbol, "john" )
            };
            TalkativeTests.TryTest( test, expected );

        }

        [TestMethod]
        public void MultipleKeywordTest()
        {
            var test = "hoge fuga: 10 moge: 'moge'.";
            var expected = new List<Token>
            {
                new Token( TokenType.Identifier, "hoge" ),
                new Token( TokenType.MessageKeyword, "fuga" ),
                new Token( TokenType.Numeral, "10" ),
                new Token( TokenType.MessageKeyword, "moge" ),
                new Token( TokenType.StringLiteral, "moge" )
            };
            TalkativeTests.TryTest( test, expected );


            test = "hoge fuga:10 moge: 'moge'.";
            TalkativeTests.TryTest( test, expected );


            test = "hoge fuga:10 moge:'moge'.";
            TalkativeTests.TryTest( test, expected );

            test = "hoge fuga:10 moge:'moge'";
            TalkativeTests.TryTest( test, expected );
        }
    }
}
