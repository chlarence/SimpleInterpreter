using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterpreter.Grammar
{
    public class Token
    {
        public TokenType Type { get; }
        public string Value { get; }

        public Token(TokenType type, string value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"({Type}, {Value})";
        }
    }

    // TokenType is an enumeration that defines the different types of tokens.
    public enum TokenType
    {
        Integer,
        Plus,
        Minus,
        Multiply,
        Divide,
        LParen,
        RParen,
        EOF
    }

    // The Lexer class takes an input string and converts it into a sequence of tokens.
    public class Lexer
    {
        private static readonly Dictionary<char, TokenType> Operators = new Dictionary<char, TokenType>
        {
            { '+', TokenType.Plus },
            { '-', TokenType.Minus },
            { '*', TokenType.Multiply },
            { '/', TokenType.Divide },
            { '(', TokenType.LParen },
            { ')', TokenType.RParen }
        };

        private readonly string _input;
        private int _position;

        public Lexer(string input)
        {
            _input = input;
            _position = 0;
        }

        public List<Token> Tokenize()
        {
            List<Token> tokens = new List<Token>();

            while (true)
            {
                Token token = NextToken();
                tokens.Add(token);

                if (token.Type == TokenType.EOF)
                {
                    break;
                }
            }

            return tokens;
        }

        private Token NextToken()
        {
            if (_position >= _input.Length)
            {
                return new Token(TokenType.EOF, "");
            }

            char currentChar = _input[_position];

            if (char.IsDigit(currentChar))
            {
                return ReadInteger();
            }

            if (Operators.TryGetValue(currentChar, out TokenType type))
            {
                _position++;
                return new Token(type, currentChar.ToString());
            }

            throw new InvalidProgramException($"Invalid character: {currentChar}");
        }

        private Token ReadInteger()
        {
            string result = "";

            while (_position < _input.Length && char.IsDigit(_input[_position]))
            {
                result += _input[_position];
                _position++;
            }

            return new Token(TokenType.Integer, result);
        }
    }
}
