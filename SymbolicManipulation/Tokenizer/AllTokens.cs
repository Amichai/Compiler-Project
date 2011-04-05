using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SymbolicManipulation {
	class AllTokens {
		string allTokenStrings = string.Empty;
		public Parser parseTree = new Parser();
		public void Add(Token token) {
			parseTree.AddToken(token);
			allTokenStrings += token.TokenString + " ";
			if (token.TokenType == TokenType.openBrace)
				openBracketCount++;
			if (token.TokenType == TokenType.closedBrace)
				openBracketCount--;
		}
		
		//TODO: Implemented a bracket order of operations system
		public int openBracketCount = 0;
	}
}
