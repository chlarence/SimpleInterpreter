using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInterpreter.Grammar
{
    internal class Parser
    {
        private readonly List<Token> _tokens;
        private int _position;

        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
        }
        public int Parse()
        {
            int result = Expr();

            if (CurrentToken().Type != TokenType.EOF)
            {
                throw new InvalidProgramException($"Unexpected token: {CurrentToken()}");
            }

            return result;
        }

        private int Expr()
        {
            int result = Term();

            while (CurrentToken().Type == TokenType.Plus || CurrentToken().Type == TokenType.Minus)
            {
                Token token = NextToken();

                if (token.Type == TokenType.Plus)
                {
                    result += Term();
                }
                else if (token.Type == TokenType.Minus)
                {
                    result -= Term();
                }
            }

            return result;
        }

        private int Term()
        {
            int result = Factor();

            while (CurrentToken().Type == TokenType.Multiply || CurrentToken().Type == TokenType.Divide)
            {
                Token token = NextToken();

                if (token.Type == TokenType.Multiply)
                {
                    result *= Factor();
                }
                else if (token.Type == TokenType.Divide)
                {
                    result /= Factor();
                }
            }

            return result;
        }

        private int Factor()
        {
            Token token = NextToken();

            if (token.Type == TokenType.Integer)
            {
                return int.Parse(token.Value);
            }
            else if (token.Type == TokenType.LParen)
            {
                int result = Expr();

                if (CurrentToken().Type != TokenType.RParen)
                {
                    throw new InvalidProgramException($"Expected ) but found {CurrentToken()}");
                }

                NextToken();

                return result;
            }

            throw new InvalidProgramException($"Unexpected token: {token}");
        }

        private Token CurrentToken()
        {
            if (_position >= _tokens.Count)
            {
                return new Token(TokenType.EOF, "");
            }

            return _tokens[_position];
        }

        private Token NextToken()
        {
            _position++;
            return CurrentToken();
        }
    }
}
