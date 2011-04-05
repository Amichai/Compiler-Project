using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SymbolicManipulation {
	class Token {
		private static readonly HashSet<string> functionLibrary = new HashSet<string>() { "Print", "Add" };
		public TokenType TokenType;
		public string TokenString;
		public int TokenNumValue;

		public Token(string tokenString, TokenType tokenType) {
			TokenString = tokenString;
			TokenType = tokenType;
			if (TokenType == TokenType.number) {
				TokenNumValue = int.Parse(TokenString);
			}
			if (TokenType == TokenType.charString) {
				if (functionLibrary.Contains(TokenString)) {
					TokenType = TokenType.function;
				}
			}
		}
	}
}
