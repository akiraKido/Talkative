using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkativeCompiler
{
    internal class Parser
    {
        internal static IEnumerable<ASTBase> Parse( IEnumerable<Token> tokens )
        {
            List<Token> tokenList = tokens.ToList();
            for ( int i = 0; i < tokenList.Count; i++ )
            {
                switch ( tokenList[i].Type )
                {
                    case TokenType.EOF:
                        break;
                    case TokenType.Identifier:
                        break;
                    case TokenType.AssignmentOperator:
                        break;
                    case TokenType.Numeral:
                        break;
                    case TokenType.LineBreak:
                        break;
                    case TokenType.StringLiteral:
                        break;
                    case TokenType.StringConnector:
                        break;
                    case TokenType.Symbol:
                        break;
                    case TokenType.ArrayStart:
                        break;
                    case TokenType.ArrayEnd:
                        break;
                    case TokenType.BlockStart:
                        break;
                    case TokenType.BlockEnd:
                        break;
                    case TokenType.BlockSeparator:
                        break;
                    case TokenType.BlockArgument:
                        break;
                    case TokenType.Operand:
                        break;
                    case TokenType.Return:
                        break;
                    case TokenType.Predefined:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            throw new NotImplementedException();
        }
    }
}
