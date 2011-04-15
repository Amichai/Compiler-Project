using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SymbolicManipulation {
	class AllTokens {
		string allTokenStrings = string.Empty;
		List<Token> tokens = new List<Token>();

		//TODO: Remove parse tree in favor of the postfix notation
		public Parser parseTree = new Parser();
		public void Add(Token token) {
			parseTree.AddToken(token);
			tokens.Add(token);
			allTokenStrings += token.TokenType.ToString() + ": " + token.TokenString + " \n";
		}
		////

		public AllTokens ConvertToPostfix() {
			return new ConvertToPostfix(tokens).postFixedTokens;
		}
		//Refactor this mess
		public double Evaluate() {
			return 0;
		}
		
		//TODO: Allow for negated numbers with a space like: "- 3"
		//TODO: Implement word functions like Add(1,2,3);

		public string Visualize() {
			return allTokenStrings;
		}
	}

	class ConvertToPostfix {
		public AllTokens postFixedTokens = new AllTokens();
		private TokenType lastTokenType = TokenType.empty;

		private void handleOperator(Token token) {
			//Test precedence
			//If the current op has higher precedence, add to the stack
			//true if the last operator on the stack has precedence over the current operator
			while (operatorStack.Count() > 0 && precedenceTest(operatorStack.First().TokenString, token.TokenString)) {
				postFixedTokens.Add(operatorStack.Pop());
			}
			operatorStack.Push(token);
		}

		Stack<Token> operatorStack = new Stack<Token>();
		public ConvertToPostfix(List<Token> tokens) {
			foreach (Token token in tokens) {

				if (token.TokenType == TokenType.number) {
					if (lastTokenType == TokenType.number) {
						handleOperator(new Token("+", TokenType.arithmeticOp));
					}
					postFixedTokens.Add(token);
					lastTokenType = TokenType.number;
				}
				if (token.TokenType == TokenType.function) {
					operatorStack.Push(token);
				}
				if (token.TokenType == TokenType.syntaxChar) {
					while (operatorStack.First().TokenType != TokenType.openBrace)
						postFixedTokens.Add(operatorStack.Pop());
				}
				if (token.TokenType == TokenType.arithmeticOp) {
					handleOperator(token);
					lastTokenType = TokenType.arithmeticOp;
				}
				if (token.TokenType == TokenType.openBrace) {
					operatorStack.Push(token);
				}
				if (token.TokenType == TokenType.closedBrace) {
					while (operatorStack.First().TokenType != TokenType.openBrace) {
						postFixedTokens.Add(operatorStack.Pop());
						//If no parenthesis found, mismatched parenthesis exception
					}
					operatorStack.Pop();
				}
			}
			while(operatorStack.Count() > 0)
				postFixedTokens.Add(operatorStack.Pop());
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
	}
}
