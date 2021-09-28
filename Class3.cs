using System;
using System.Collections.Generic;
using System.Text;

namespace Practichna3
{
    public class Calculator
    {
        public decimal Add(decimal a, decimal b)
        {
            return a + b;
        }
        public decimal Substract(decimal a, decimal b)
        {
            return a - b;
        }
        public decimal Multiply(decimal a, decimal b)
        {
            return a * b;
        }
        public decimal EvaluateExpression(ParsedExpression expression)
        {
            switch (expression.Operation)
            {
                case Operation.Addition:
                    return Add(expression.LeftOperand, expression.RightOperand);

                case Operation.Substraction:
                    return Substract(expression.LeftOperand, expression.RightOperand);

                case Operation.Multiplication:
                    return Multiply(expression.LeftOperand, expression.RightOperand);

                default:
                    throw new Exception("Unknown");
            }
        }
    }
}
