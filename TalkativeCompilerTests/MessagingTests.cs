﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TalkativeCompiler;

namespace TalkativeCompilerTests
{
    [TestClass]
    public class MessagingTests
    {
        [TestMethod]
        public void MessagingTest1()
        {
            // #('one' 'two' 'three') last.

            var test = "#('one' 'two' 'three') last.";
            var expected = new List<Token>
            {
                new Token( TokenType.ArrayStart, "(" ),
                new Token( TokenType.StringLiteral, "one" ),
                new Token( TokenType.StringLiteral, "two" ),
                new Token( TokenType.StringLiteral, "three" ),
                new Token( TokenType.ArrayEnd, ")" ),
                new Token( TokenType.Identifier, "last" ),
            };
            TalkativeTests.TryTest( test, expected );
        }

        [TestMethod]
        public void BinaryMethodTest1()
        {
            var test = "4 + 1.";
            var expected = new List<Token>
            {
                new Token( TokenType.Numeral, "4" ),
                new Token( TokenType.Identifier, "+" ),
                new Token( TokenType.Numeral, "1" )
            };
            TalkativeTests.TryTest( test, expected );

        }

        // 10 / 2. "(10/2)という分数"
        [TestMethod]
        public void BinaryMethodTest2 ()
        {
            var test = "10 / 2. \"(10/2)という分数\"";
            var expected = new List<Token>
            {
                new Token( TokenType.Numeral, "10" ),
                new Token( TokenType.Identifier, "/" ),
                new Token( TokenType.Numeral, "2" )
            };
            TalkativeTests.TryTest( test, expected );

        }

        // 5 // 3. "商"
        [TestMethod]
        public void BinaryMethodTest3 ()
        {
            var test = "5 // 3. \"商\"";
            var expected = new List<Token>
            {
                new Token( TokenType.Numeral, "5" ),
                new Token( TokenType.Operand, "//" ),
                new Token( TokenType.Numeral, "3" )
            };
            TalkativeTests.TryTest( test, expected );

        }
    }
}
