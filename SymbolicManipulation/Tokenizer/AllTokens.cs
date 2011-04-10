using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SymbolicManipulation {
	class AllTokens {
		string allTokenStrings = string.Empty;
		public Parser2 parseTree = new Parser2();
		public void Add(Token token) {
			parseTree.AddToken(token);
			allTokenStrings += token.TokenType.ToString() + ": " + token.TokenString + " \n";
			if (token.TokenType == TokenType.openBrace)
				openBracketCount++;
			if (token.TokenType == TokenType.closedBrace)
				openBracketCount--;
		}
		
		
		//TODO: Implemented a bracket order of operations system
		//TODO: Implement word functions like Add(1,2,3);
		public int openBracketCount = 0;

		public string Visualize() {
			return allTokenStrings;
		}
	}
}
