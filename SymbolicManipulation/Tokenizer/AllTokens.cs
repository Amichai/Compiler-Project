using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SymbolicManipulation {
	class AllTokens {
		string allTokenStrings = string.Empty;
		public Parser parseTree = new Parser();
		public void Add(Token token) {
			results.Add(token);
			parseTree.AddToken(token);
			allTokenStrings += token.TokenString + " ";
			if (token.TokenType == TokenType.openBrace)
				openBracketCount++;
			if (token.TokenType == TokenType.closedBrace)
				openBracketCount--;
		}
		//TODO: Replace this with the parse tree (which can be evaluated)
		public List<Token> results = new List<Token>();

		//TODO: test the input for coherence
		//Build the parse tree
		//State of the token set:
		public int openBracketCount = 0;
	}
}
