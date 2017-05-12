using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkativeCompiler
{
    public enum TokenType
    {
        EOF = -1,

        Identifier = -2,
        AssignmentOperator = -3,
        Numeral = -4,
        LineBreak = -5,
        StringLiteral = -6,
        StringConnector = -7,
        Symbol = -8,
        ArrayStart = -9,
        ArrayEnd = -10
    }

    public class Token
    {
        public Token ( TokenType type, string value )
        {
            this.Type = type;
            this.Value = value;
        }

        public TokenType Type { get; }
        public string Value { get; }
    }

    internal class Lexer
    {
        private string Input { get; }
        private int currentPosition;
        private int currentLine;
        private char lastChar = ' ';
        private const char EOF = '\0';

        internal Lexer ( string input )
        {
            Input = input;
        }

        internal IEnumerable<Token> GetTokens ()
        {
            var tokens = new List<Token>();

            while ( currentPosition < Input.Length )
            {
                Token token = GetNextToken();
                if(token != null) tokens.Add( token );
            }

            // 最後のピリオドは無視
            if ( tokens.LastOrDefault()?.Value == "." )
            {
                tokens.RemoveAt( tokens.Count - 1 );
            }

            return tokens;
        }

        private Token GetNextToken()
        {
            if ( char.IsWhiteSpace( lastChar ) ) ProceedToNextChar();

            Token token;

            // コメントアウト
            switch ( lastChar )
            {
                case '\0':
                    // 何もなし
                    return null;
                case '"':
                    // コメントアウト
                    while ( currentPosition < Input.Length )
                    {
                        ProceedToNextChar();
                        if ( currentPosition >= Input.Length
                             || lastChar == '"' && PeekNextChar() != '"' )
                        {
                            break;
                        }
                    }
                    ProceedToNextChar(); // " を消費
                    return GetNextToken();
                case '#':
                    // シンボル
                    ProceedToNextChar(); // # を消費
                    if ( lastChar == '(' )
                    {
                        // 配列開始
                        ProceedToNextChar(); // ( を消費
                        return new Token( TokenType.ArrayStart, "(" );
                    }
                    else
                    {
                        // 通常シンボル
                        string symbol = string.Empty;
                        while ( char.IsLetter( lastChar ) )
                        {
                            symbol += lastChar.ToString();
                            if ( currentPosition >= Input.Length ) break;
                            ProceedToNextChar();
                        }
                        return new Token( TokenType.Symbol, symbol );
                    }
                case ')':
                    ProceedToNextChar(); // ) を消費
                    return new Token( TokenType.ArrayEnd, ")" );
                case ':':
                    // 代入
                    if ( PeekNextChar() == '=' )
                    {
                        token = new Token( TokenType.AssignmentOperator, ":=" );
                        currentPosition++; // :を消費
                        ProceedToNextChar(); // = を消費
                        return token;
                    }

                    // TODO: コロン単体は呼び出し
                    throw new NotImplementedException();
                case '.':
                    // 行区切り
                    token = new Token( TokenType.LineBreak, "." );
                    ProceedToNextChar(); // '.' を消費
                    return token;
                case '\'':
                    // 文字列リテラル
                    string stringValue = string.Empty;
                    while ( true )
                    {
                        ProceedToNextCharWithWhiteSpaces();
                        if ( lastChar == '\'' )
                        {
                            if ( PeekNextChar() == ',' )
                            {
                                // 結合文字列だった場合
                                ProceedToNextChar(); // "'" を消費
                                ProceedToNextChar(); // "," を消費
                                ProceedToNextChar(); // "'" を消費
                            }
                            else
                            {
                                if ( currentPosition >= Input.Length || PeekNextChar() != '\'' )
                                {
                                    break;
                                }

                                currentPosition++; // " '' " だった場合、一個進める
                            }
                        }
                        stringValue += lastChar.ToString();
                    }

                    if ( currentPosition < Input.Length )
                    {
                        ProceedToNextChar(); // ' を消費
                    }
                    return new Token( TokenType.StringLiteral, stringValue );
            }
           
            // 数字リテラル
            if ( char.IsDigit( lastChar ) || lastChar == '-' && char.IsDigit( PeekNextChar() ) )
            {
                // マイナスの後に文字が続けばOK
                string numberString = string.Empty;

                do
                {
                    numberString += lastChar;
                    if ( currentPosition >= Input.Length ) break;
                    // 次が数字の場合があるので
                    ProceedToNextCharWithWhiteSpaces();
                } while ( char.IsDigit( lastChar ) || lastChar == '.' );

                // 行区切りの場合
                if ( numberString[numberString.Length - 1] == '.' )
                {
                    if ( currentPosition < Input.Length )
                    {
                        // 最終行は戻りすぎる為
                        currentPosition--;
                    }
                    currentPosition--;
                    numberString = numberString.Substring( 0, numberString.Length - 1 );
                }

                if ( lastChar == ')' && currentPosition == Input.Length ) currentPosition--;

                return new Token( TokenType.Numeral, numberString );
            }

            // 文字列
            string value = lastChar.ToString();
            while ( char.IsLetterOrDigit( ProceedToNextChar() ) )
            {
                value += lastChar;
                if ( currentPosition >= Input.Length ) break;
            }
            return new Token( TokenType.Identifier, value );
        }

        private char ProceedToNextChar()
        {
            if ( currentPosition >= Input.Length )
            {
                lastChar = '\0';
            }
            else
            {
                do
                {
                    lastChar = Input[currentPosition++];
                    if ( lastChar.Equals( '\n' ) )
                    {
                        currentLine++;
                    }

                    if ( currentPosition >= Input.Length ) break;
                } while ( char.IsWhiteSpace( lastChar ) );
            }
            return lastChar;
        }

        private char ProceedToNextCharWithWhiteSpaces()
        {
            lastChar = currentPosition >= Input.Length
                ? '\0' 
                : Input[currentPosition++];
            return lastChar;
        }

        private char PeekNextChar()
        {
            if ( currentPosition >= Input.Length ) return '\0';

            int localCurrent = currentPosition;

            char _lastChar;
            do
            {
                _lastChar = Input[localCurrent++];
                if ( localCurrent >= Input.Length ) break;
            } while ( char.IsWhiteSpace( _lastChar ) );

            return _lastChar;
        }
    }
}
