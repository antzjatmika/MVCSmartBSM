using System;
using DevExpress.Data.Filtering;
using System.Collections.Generic;

namespace MVCSmartClient01.Models
{
    public class CustomCriteriaToLinqWhereParser : ICriteriaVisitor, IClientCriteriaVisitor
    {
        public static string Process(CriteriaOperator op)
        {
            return op.Accept(new CustomCriteriaToLinqWhereParser()).ToString();
        }

        static string GetFunctionName(FunctionOperatorType operatorType)
        {
            switch (operatorType)
            {
                case FunctionOperatorType.IsNull:
                    return "is null";
                case FunctionOperatorType.StartsWith:
                    return "StartsWith";
                case FunctionOperatorType.EndsWith:
                    return "EndsWith";
                case FunctionOperatorType.Contains:
                    return "Contains";
                default:
                    return operatorType.ToString();
            }
        }

        static string GetUnaryOperationName(UnaryOperatorType operatorType)
        {
            switch (operatorType)
            {
                case UnaryOperatorType.BitwiseNot:
                    return "not";
                case UnaryOperatorType.IsNull:
                    return "is null";
                case UnaryOperatorType.Minus:
                    return "minus";
                case UnaryOperatorType.Not:
                    return "not";
                case UnaryOperatorType.Plus:
                    return "plus";
                default:
                    return string.Empty;
            }
        }

        static string GetBinaryOperationName(BinaryOperatorType operatorType)
        {
            switch (operatorType)
            {
                case BinaryOperatorType.Equal:
                    return "==";
                case BinaryOperatorType.Less:
                    return "<";
                case BinaryOperatorType.LessOrEqual:
                    return "<=";
                case BinaryOperatorType.Greater:
                    return ">";
                case BinaryOperatorType.GreaterOrEqual:
                    return ">=";
                case BinaryOperatorType.NotEqual:
                    return "!=";
                default:
                    return operatorType.ToString();
            }
        }


        object ICriteriaVisitor.Visit(FunctionOperator theOperator)
        {
            string OperandFormat = "{2}";
            switch (theOperator.OperatorType)
            {
                case FunctionOperatorType.StartsWith:
                    OperandFormat = ".ToLower().StartsWith(\"{2}\".ToLower())";
                    break;
                case FunctionOperatorType.EndsWith:
                    OperandFormat = ".ToLower().EndsWith(\"{2}\".ToLower())";
                    break;
                case FunctionOperatorType.Contains:
                    OperandFormat = ".ToLower().Contains(\"{2}\".ToLower())";
                    break;
            }
            List<string> parameters = new List<string>();
            foreach (CriteriaOperator operand in theOperator.Operands)
                parameters.Add(operand.Accept(this).ToString());
            parameters.Insert(1, GetFunctionName(theOperator.OperatorType));
            return string.Format("{0}" + OperandFormat, parameters.ToArray());
        }

        object ICriteriaVisitor.Visit(OperandValue theOperand)
        {
            return theOperand.Value == null ? string.Empty : theOperand.Value.ToString();
        }

        object ICriteriaVisitor.Visit(GroupOperator theOperator)
        {
            List<string> operands = new List<string>();
            foreach (CriteriaOperator operand in theOperator.Operands)
                operands.Add(string.Format("({0})", operand.Accept(this)));
            return string.Join(string.Format(" {0} ", theOperator.OperatorType.ToString()),
                operands.ToArray());
        }

        object ICriteriaVisitor.Visit(InOperator theOperator)
        {
            List<string> operands = new List<string>();
            foreach (CriteriaOperator operand in theOperator.Operands)
                operands.Add(operand.Accept(this).ToString());
            return string.Format("{0} in {1}", theOperator.LeftOperand.Accept(this).ToString(),
                string.Join(", ", operands.ToArray()));
        }

        object ICriteriaVisitor.Visit(UnaryOperator theOperator)
        {
            return string.Format("{0} {1}", GetUnaryOperationName(theOperator.OperatorType),
                theOperator.Operand.Accept(this).ToString());
        }
        object ICriteriaVisitor.Visit(BinaryOperator theOperator)
        {
            string left = theOperator.LeftOperand.Accept(this).ToString();
            string right = theOperator.RightOperand.Accept(this).ToString();
            float output;
            if (float.TryParse(right, out output))
            {
                return string.Format("{0} {1} {2}", left, GetBinaryOperationName(theOperator.OperatorType),
                    right);
            }
            else
            {
                return string.Format("{0}.ToLower() {1} \"{2}\".ToLower()", left, GetBinaryOperationName(theOperator.OperatorType),
                 right);
            }
        }

        object ICriteriaVisitor.Visit(BetweenOperator theOperator)
        {
            return string.Format("{0} is between {1} and {2}", theOperator.TestExpression.Accept(this).ToString(), theOperator.BeginExpression.Accept(this).ToString(), theOperator.EndExpression.Accept(this).ToString());
        }

        object IClientCriteriaVisitor.Visit(OperandProperty theOperand)
        {
            return string.Format("{0}", theOperand.PropertyName);
        }

        object IClientCriteriaVisitor.Visit(AggregateOperand theOperand)
        {
            throw new NotImplementedException();
        }

        object IClientCriteriaVisitor.Visit(JoinOperand theOperand)
        {
            throw new NotImplementedException();
        }

    }
}