using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SymbolicManipulation {
	class Parser {
		public Parser(){}
		Expression rootExpression = new Expression();
		private static int openBraceCounter = 0;

		private string lastOperator = null;
		public void AddToken(Token token) {
			switch (token.TokenType) {
				case TokenType.arithmeticOp:
					//precedence precedence = precedenceOver(token.TokenString);
					//if (precedence == Parser.precedence.currentPrecedent) {						
					//}
					if (rootExpression.ExpressionStack.Count() > 0)
						rootExpression.ExpressionStack.Last().OperatorStack.Add(new Operation(token.TokenString));
					else
						rootExpression.OperatorStack.Add(new Operation(token.TokenString));
					break;
				case TokenType.number:
					if(rootExpression.ExpressionStack.Count() > 0)
						rootExpression.ExpressionStack.Last().NumberStack.Add(new Number(token.TokenNumValue));
					else
						rootExpression.NumberStack.Add(new Number(token.TokenNumValue));
					break;
				case TokenType.function:
					break;
				case TokenType.openBrace:
					openBraceCounter++;
					rootExpression.ExpressionStack.Add(new Expression());
					break;
				case TokenType.closedBrace:
					double evaluationValue = rootExpression.ExpressionStack.Last().BuildParseTree().EvaluationValue;
					rootExpression.ExpressionStack.RemoveAt(openBraceCounter-1);
					if (rootExpression.ExpressionStack.Count() > 0)
						rootExpression.ExpressionStack.Last().NumberStack.Add(new Number(evaluationValue));
					else
						rootExpression.NumberStack.Add(new Number(evaluationValue));
					openBraceCounter--;
					break;
			}
		}
		//TODO: Incorperate polymorphism into the token definition

		internal double Evaluate() {
			double evaluation = rootExpression.BuildParseTree().EvaluationValue;
			return evaluation;
		}

		private int getOperatorValue(string op) {
			int opValue = int.MinValue;
			if (op == "+" ||
				op == "-") {
				opValue = 1;
			}
			if (op == "*" ||
				op == "/") {
				opValue = 2;
			}
			if (op == "^") {
				opValue = 3;
			}
			if (opValue == int.MinValue)
				throw new Exception("Unable to evaluate operator value");
			return opValue;
		}

		private enum precedence {previousPrecedent, currentPrecedent, equalPrecedence}

		private precedence precedenceOver(string thisOperator) {
			int op1Value = getOperatorValue(lastOperator);
			int op2Value = getOperatorValue(thisOperator);
			if (op1Value < op2Value)
				return precedence.currentPrecedent;
			if (op1Value > op2Value)
				return precedence.previousPrecedent;
			if (op1Value == op2Value)
				return precedence.equalPrecedence;
			throw new Exception("Unhandled");
		}
	}
}