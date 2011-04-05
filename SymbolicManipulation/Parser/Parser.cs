using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SymbolicManipulation {
	class Parser {
		List<ParseTree> numberStack = new List<ParseTree>();
		List<ParseTree> operatorStack = new List<ParseTree>();

		public void AddToken(Token tokenToAdd) {
			if (tokenToAdd.TokenType == TokenType.number) {
				numberStack.Add(new ParseTree(tokenToAdd.TokenNumValue));
			} else if (tokenToAdd.TokenType == TokenType.arithmeticOp) {
				operatorStack.Add(new ParseTree(tokenToAdd.TokenString, 2));
			} else if (tokenToAdd.TokenType == TokenType.function) {
				operatorStack.Add(new ParseTree(tokenToAdd.TokenString, 1));
			} else if (tokenToAdd.TokenString == "(") {
				
			}
		}

		private int rootNode = 0;
		private ParseTree defineOperator(int num) {
			//For first parameter, either take the corresponding number
			//or the last defined operator
			if (operatorStack[num].children.Count() == 0) {
				if (!numberStack[num].appendedToTree) {
					operatorStack[num].children.Add(numberStack[num]);
					numberStack[num].appendedToTree = true;
				} else {
					if (operatorStack[rootNode].appendedToTree)
						throw new Exception("Trying to append an operator twice!");
					operatorStack[num].children.Add(operatorStack[rootNode]);
					rootNode = num;
				}
				return defineOperator(num);
			}
			//Not first operator parameter
			else {
				//determine precedence
				//If current operator has precedence over next operator or they are equivalent
				if (num + 1 == operatorStack.Count() || operatorStack[rootNode].HasPrecedenceOver(operatorStack[num + 1])) {
					operatorStack[num].children.Add(numberStack[num + 1]);
					numberStack[num + 1].appendedToTree = true;
					return operatorStack[num];
				}
				else {
					i++;
					ParseTree opToAdd = defineOperator(num + 1);
					operatorStack[num].children.Add(opToAdd);
					return operatorStack[num];
				}
			}
			throw new Exception("not all code paths return a value");
		}

		private int i=0;
		public ParseTree BuildParseTree() {
			ParseTree ParserTree = null;
			while (i < operatorStack.Count()) {
				ParserTree = defineOperator(i);
				i++;
			}
			return ParserTree;
		}
	}
}