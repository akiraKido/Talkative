using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        ArrayEnd = -10,

        BlockStart = -11,
        BlockEnd = -12,
        BlockSeparator = -13,
        BlockArgument = -14,

        Operand = -15,
        Return = -16,
        MessageKeyword = -17,

        Predefined = -99
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
        private static readonly string[] PredefinedStrings = { "true", "false", "nil", "self", "super", "thisContext", "using" };
        private static readonly string[] Operands = {"+", "-", "*", "/", "//", @"\\", ","};

        private string Input { get; }
        private int currentPosition;
        private int currentLine;
        private char lastChar = ' ';
        private const char EOF = '\0';
        private bool inBlock;

        internal Lexer ( string input )
        {
            Input = input;
        }

        internal IEnumerable<Token> GenerateTokens ()
        {
            var tokens = new List<Token>();

            //while ( currentPosition < Input.Length )
            while ( lastChar != '\0' )
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
                case '^':
                    ProceedToNextChar(); // ^ を消費
                    return new Token( TokenType.Return, "^" );
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
                        ProceedToNextChar();
                        return new Token( TokenType.Symbol, symbol );
                    }
                case ')':
                    while(lastChar == ')') ProceedToNextChar(); // ) を消費
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

                    if ( inBlock )
                    {
                        ProceedToNextChar(); // ':' を消費
                        string blockArgument = lastChar.ToString();
                        while ( char.IsLetterOrDigit( ProceedToNextCharWithWhiteSpaces() ) )
                        {
                            blockArgument += lastChar.ToString();
                        }
                        return new Token( TokenType.BlockArgument, blockArgument );
                    }

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
                                //ProceedToNextChar(); // "'" を消費
                                if ( lastChar == '\'' )
                                {
                                    ProceedToNextChar();
                                }
                                else
                                {
                                    // 純粋な文字列同士の結合でない場合コンマを戻す
                                    ReturnOneChar();
                                }
                            }
                            else
                            {
                                if ( PeekNextCharWithWhiteSpaces() != '\'' )
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
                // ブロック構文
                case '[':
                    ProceedToNextChar(); // [ を消費
                    inBlock = true;
                    return new Token( TokenType.BlockStart, "[" );
                case '|':
                    ProceedToNextChar(); // | を消費
                    return new Token( TokenType.BlockSeparator, "|" );
                case ']':
                    ProceedToNextChar(); // ] を消費
                    inBlock = false;
                    return new Token( TokenType.BlockEnd, "]" );
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
                    //if ( currentPosition < Input.Length )
                    //{
                    //    // 最終行は戻りすぎる為
                    //    ReturnOneChar();
                    //}
                    while(lastChar != '.') ReturnOneChar();
                    currentPosition++; // カーソル
                    numberString = numberString.Substring( 0, numberString.Length - 1 );
                }

                if ( lastChar == ')' && currentPosition == Input.Length ) currentPosition--;

                return new Token( TokenType.Numeral, numberString );
            }

            // 文字列
            string value = lastChar.ToString();
            var nextChar = ProceedToNextCharWithWhiteSpaces();
            while ( char.IsLetterOrDigit( nextChar ) )
            {
                value += lastChar;
                if ( currentPosition >= Input.Length ) break;
                nextChar = ProceedToNextCharWithWhiteSpaces();
            }
            
            if ( Operands.Contains( value + lastChar ) )
            {
                value += lastChar;
                ProceedToNextChar();
                return new Token( TokenType.Operand, value );
            }

            if ( PredefinedStrings.Contains( value ) )
            {
                return new Token( TokenType.Predefined, value );
            }

            if ( lastChar == ':' || PeekNextChar() == ':' )
            {
                ProceedToNextChar(); // : を消費
                if ( PeekNextChar() != '=' )
                {
                    return new Token( TokenType.MessageKeyword, value );
                }
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

        private char ReturnOneChar()
        {
            lastChar = Input[--currentPosition];
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

        private char PeekNextCharWithWhiteSpaces()
        {
            if ( currentPosition >= Input.Length ) return '\0';
            
            return Input[currentPosition];
        }
    }
}
