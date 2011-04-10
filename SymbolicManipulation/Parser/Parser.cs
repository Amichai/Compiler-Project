using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SymbolicManipulation {
	class Parser {
		Expression rootExpression = new Expression();
		private static int openBraceCounter = 0;
		public void AddToken(Token token) {
			switch (token.TokenType) {
				case TokenType.arithmeticOp:
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
		private static int counter = 0;

		internal double Evaluate() {
			double evaluation = rootExpression.BuildParseTree().EvaluationValue;
			return evaluation;
		}
	}
}