using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SymbolicManipulation {
	class AllTokens {
		string allTokenStrings = string.Empty;
		List<Token> tokens = new List<Token>();

		public void Add(Token token) {
			testForOperationInference(token);
			tokens.Add(token);
			allTokenStrings += token.TokenType.ToString() + ": " + token.TokenString + " \n";
		}

		public PostFixedTokens ConvertToPostfix() {
			return new PostFixedTokens(tokens);
		}
		//TODO: Implement word functions like Add(1,2,3);

		public string Visualize() {
			return allTokenStrings;
		}

		private void testForOperationInference(Token tokenToAdd) {
			if (tokens.Count() > 0) {
				//When a minus or plus sign is read as a negative number, add a plus sign before the number
				if (tokenToAdd.TokenType == TokenType.number
					&& (tokens.Last().TokenType == TokenType.number || tokens.Last().TokenType == TokenType.closedBrace)
					&& (tokenToAdd.TokenString[0] == '-' || tokenToAdd.TokenString[0] == '+')) {
					tokens.Add(new Token("+", TokenType.arithmeticOp));
				}
				//Infer a multiplication sign between two sets of parenthesis
				if (tokenToAdd.TokenType == TokenType.openBrace && tokens.Last().TokenType == TokenType.closedBrace) {
					tokens.Add(new Token("*", TokenType.arithmeticOp));
				}
				//Infer a multiplication sign between parenthesis and a number (that doesn't start with a minus sign)
				if (tokenToAdd.TokenType == TokenType.openBrace && tokens.Last().TokenType == TokenType.number) {
					tokens.Add(new Token("*", TokenType.arithmeticOp));
				}
				if (tokenToAdd.TokenType == TokenType.number && tokens.Last().TokenType == TokenType.closedBrace && tokenToAdd.TokenString[0] != '-') {
					tokens.Add(new Token("*", TokenType.arithmeticOp));
				}
			}
		}
	}
}
