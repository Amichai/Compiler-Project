using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SymbolicManipulation {
	class PostFixedTokens {
		private void handleOperator(Token token) {
			//Test precedence
			//If the current op has higher precedence, add to the stack
			//true if the last operator on the stack has precedence over the current operator
			while (operatorStack.Count() > 0 && operatorStack.First().TokenType == TokenType.arithmeticOp
				&& precedenceTest(operatorStack.First().TokenString, token.TokenString)) {
				tokens.Add(operatorStack.Pop());
			}
			operatorStack.Push(token);
		}

		Stack<Token> operatorStack = new Stack<Token>();

		public PostFixedTokens(List<Token> inputTokens) {
			foreach (Token token in inputTokens) {
				if (token.TokenType == TokenType.number) {
					tokens.Add(token);
				}
				if (token.TokenType == TokenType.function) {
					operatorStack.Push(token);
				}
				if (token.TokenType == TokenType.syntaxChar) {
					while (operatorStack.First().TokenType != TokenType.openBrace)
						tokens.Add(operatorStack.Pop());
				}
				if (token.TokenType == TokenType.arithmeticOp) {
					handleOperator(token);
				}
				if (token.TokenType == TokenType.openBrace) {
					operatorStack.Push(token);
				}
				if (token.TokenType == TokenType.closedBrace) {
					while (operatorStack.First().TokenType != TokenType.openBrace) {
						tokens.Add(operatorStack.Pop());
						//If no parenthesis found, mismatched parenthesis exception
					}
					operatorStack.Pop();
				}
			}
			while (operatorStack.Count() > 0)
				tokens.Add(operatorStack.Pop());
		}

		private int getOperatorValue(string op) {
			int opValue = int.MinValue;
			if (op == "(") {
				opValue = 0;
			}

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

		List<string> rightAssociative = new List<string>() { "^" };

		private bool precedenceTest(string op1, string op2) {
			int op1Value = getOperatorValue(op1);
			int op2Value = getOperatorValue(op2);
			if (op1Value < op2Value)
				return false;
			if (op1Value > op2Value)
				return true;
			if (op1Value == op2Value){
				if (rightAssociative.Contains(op2))
					return false;
				else
					return true;
			}
			throw new Exception("Unhandled");
		}

		List<Token> tokens = new List<Token>();
		string allTokenStrings = string.Empty;

		public void Add(Token token) {
			allTokenStrings += token.TokenType.ToString() + ": " + token.TokenString + " \n";
			tokens.Add(token);
		}

		Stack<double> evalStack = new Stack<double>();
		public double Evaluate() {
			foreach (Token token in tokens) {
				if (token.TokenType == TokenType.number) {
					evalStack.Push(token.TokenNumValue);
				}
				if (token.TokenType == TokenType.arithmeticOp) {
					double val1, val2;
					switch (token.TokenString) {
						case "+":
							evalStack.Push(evalStack.Pop() + evalStack.Pop());
							break;
						case "-":
							evalStack.Push(-evalStack.Pop() + evalStack.Pop());
							break;
						case "*":
							evalStack.Push(evalStack.Pop() * evalStack.Pop());
							break;
						case "/":
							val1 = evalStack.Pop();
							val2 = evalStack.Pop();
							evalStack.Push(val2 / val1);
							break;
						case "%":
							evalStack.Push(evalStack.Pop() % evalStack.Pop());
							break;
						case "^":
							val1 = evalStack.Pop();
							val2 = evalStack.Pop();
							evalStack.Push(Math.Pow(val2, val1));
							break;
						default:
							throw new Exception("unknown operator");
					}
				}
			}

			if (evalStack.Count() != 1)
				throw new Exception("Parser evaluation error");
			return evalStack.Pop();
		}

		public string Visualize() {
			return allTokenStrings;
		}
	}
}
