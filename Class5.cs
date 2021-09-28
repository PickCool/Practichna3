using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace Practichna3
{
    public class Parser
    {
        private enum State
        {
            Initial = 0,
            LeftOperandParsed,
            OperationParsed,
            RightOperandParsed
        };

        private State state = State.Initial;
        private ParsedExpression parsedExpression = new ParsedExpression();
        private char[] OperationSigns = new char[] { '+', '-', '*' };
        public bool ProcessInput(string input)
        {
            if (state == State.RightOperandParsed)
                throw new Exception("Parser cannot receive data");
            State initialState = state;
            decimal initialLeftOperand = parsedExpression.LeftOperand;
            Operation initialOperation = parsedExpression.Operation;
            decimal initialRightOperand = parsedExpression.RightOperand;
            try
            {
                input = NormalizeInput(input);

                int position = 0;
                Token token = GetNextToken(input, ref position);
                while (token != null)
                {
                    ParseToken(token);
                    token = GetNextToken(input, ref position);
                }

                return state == State.RightOperandParsed;
            }
            catch
            {
                state = initialState;
                parsedExpression.LeftOperand = initialLeftOperand;
                parsedExpression.Operation = initialOperation;
                parsedExpression.RightOperand = initialRightOperand;
                throw;
            }
        }

        public ParsedExpression GetResult()
        {
            if (state != State.RightOperandParsed)
                throw new Exception("Parcer cannot give result yet");

            ParsedExpression result = parsedExpression;

            state = State.Initial;
            parsedExpression = new ParsedExpression();

            return result;
        }

        private string NormalizeInput(string input)
        {
            return input
                .Replace(" ", "")
                .Replace("=", "")
                .Replace(',', '.');
        }

        private Token GetNextToken(string input, ref int position)
        {
            string lexem = "";
            for (; position < input.Length; position++)
            {
                char ch = input[position];
                if (char.IsDigit(ch) || ch == '.')
                    lexem += ch;
                else if (OperationSigns.Contains(ch))
                {
                    if (lexem != "")
                        return new Token(lexem, TokenType.Number);
                    else
                    {
                        position++;
                        return new Token(ch.ToString(), TokenType.OperationSign);
                    }
                }
            }
            return lexem == "" ? null : new Token(lexem, TokenType.Number);
        }

        private void ParseToken(Token token)
        {
            switch (token.Type)
            {
                case TokenType.Number:
                    if (state == State.LeftOperandParsed)
                        throw new Exception("Operation sign is missed");

                    if (state == State.Initial)
                    {
                        parsedExpression.LeftOperand = ParseDecimal(token.Content);
                        state = State.LeftOperandParsed;
                    }
                    else if (state == State.OperationParsed)
                    {
                        parsedExpression.RightOperand = ParseDecimal(token.Content);
                        state = State.RightOperandParsed;
                    }
                    break;

                case TokenType.OperationSign:
                    if (state < State.LeftOperandParsed)
                        throw new Exception(
                            "Left operand is missed"
                        );

                    if (state >= State.OperationParsed)
                        throw new Exception(
                            "Too many signs of operations"
                        );

                    parsedExpression.Operation = ParseOperationSign(token.Content);
                    state = State.OperationParsed;
                    break;

                default: throw new Exception("Unknown token");
            }
        }
        private static decimal ParseDecimal(string s)
        {
            return decimal.Parse(s, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture);
        }
        private static Operation ParseOperationSign(string s)
        {
            switch (s)
            {
                case "+": return Operation.Addition;
                case "-": return Operation.Substraction;
                case "*": return Operation.Multiplication;
                default: throw new Exception("Unknown operation");
            }
        }
    }

}
