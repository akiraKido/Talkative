using System.Collections.Generic;

namespace TalkativeCompiler
{

    public class Talkative
    {
        public static IEnumerable<Token> GetTokens ( string sourceCode )
        {
            var lexer = new Lexer( sourceCode );
            return lexer.GenerateTokens();
        }

        public static IEnumerable<ASTBase> GenerateAst(string sourceCode)
        {
            var lexer = new Lexer( sourceCode );
            var tokens = lexer.GenerateTokens();
            return Parser.Parse( tokens );
        }
        
    }


    
}
