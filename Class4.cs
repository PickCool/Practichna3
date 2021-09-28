using System;
using System.Collections.Generic;
using System.Text;

namespace Practichna3
{
    public class ParsedExpression
    {
        public decimal LeftOperand { get; set; } = 0;
        public Operation Operation { get; set; } = Operation.Addition;
        public decimal RightOperand { get; set; } = 0;
        public ParsedExpression() { }
        public ParsedExpression(decimal leftOperand, Operation operation, decimal rightOperand)
        {
            LeftOperand = leftOperand;
            Operation = operation;
            RightOperand = rightOperand;
        }
    }
}
