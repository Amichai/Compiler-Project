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
			allTokenStrings += token.TokenType.ToString() + ": " + token.TokenString + " \n";
		}
		
		//TODO: Allow for negated numbers with a space like: "- 3"
		//TODO: Implement word functions like Add(1,2,3);

		public string Visualize() {
			return allTokenStrings;
		}
	}
}
