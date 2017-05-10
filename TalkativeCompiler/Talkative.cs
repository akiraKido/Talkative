using System.Collections.Generic;

namespace TalkativeCompiler
{

    public class Talkative
    {
        public static IEnumerable<Token> Parse ( string sourceCode )
        {
            var lexer = new Lexer( sourceCode );
            return lexer.GetTokens();
        }
    }


    
}
