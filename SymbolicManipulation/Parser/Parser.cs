using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SymbolicManipulation {
	class Parser {
		Tokens tokens;
		int tokenIndex = 0;
		public Parser(Tokens tokens) {
			this.tokens = tokens;
		}

		private Expression setOperation(int value) {
			MathOperation operation = new MathOperation(new IntStmnt(value));
			tokenIndex++;
			operation.SetOperation(tokens.results[tokenIndex].TokenString);
			tokenIndex++;
			operation.SetParameter2(this.ParseToken());
			return new IntStmnt(operation.Evaluate());
		}

		private Expression setAddMethod() {
			AddMethod currentStatement = new AddMethod();
			tokenIndex++;
			while (tokenIndex < tokens.results.Count() - 1 && tokens.results[tokenIndex].TokenString != ")") {
				tokenIndex++;
				currentStatement.SetParameter(this.ParseToken());
			}
			currentStatement.Evaluate();
			return currentStatement;
		}

		private Expression setIntStatement(int value) {
			tokenIndex++;
			return new IntStmnt(value);
		}

		public Expression ParseToken() {
			Token currentToken = tokens.results[tokenIndex];
			TokenType nextToken = TokenType.empty;
			if(tokenIndex < tokens.results.Count())
				nextToken= tokens.results[tokenIndex + 1].TokenType;
			Expression statementToReturn = null;
			switch (currentToken.TokenType) {
				case TokenType.number:
					if (nextToken == TokenType.arithmeticOp) {
						statementToReturn = setOperation(currentToken.TokenNumValue);
					} else {
						statementToReturn = setIntStatement(currentToken.TokenNumValue);
					}
					break;
				case TokenType.function:
					//figure out where the method ends, test if the return will be part of another method
					switch (currentToken.TokenString) {
						case "Print":
							break;
						case "Add":
							statementToReturn = setAddMethod();
							if (tokenIndex < tokens.results.Count() - 1 && tokens.results[tokenIndex + 1].TokenType == TokenType.arithmeticOp) { }
							break;
						default:
							throw new Exception("Method not recognized");
					}
					break;
			}
			if (statementToReturn == null)
				throw new Exception("Null statement");
			if (tokenIndex == tokens.results.Count() || tokens.results[tokenIndex].TokenType == TokenType.syntaxChar)
				return statementToReturn;
			else {
				currentToken.TokenNumValue = statementToReturn.Value;
				tokenIndex++;
				ParseToken();
			}
			return null;
		}

		private Expression checkForNextOperation(int value) {
			if (tokenIndex < tokens.results.Count() - 1 && tokens.results[tokenIndex + 1].TokenType == TokenType.arithmeticOp) {
				return setOperation(value);
			} else return null;
		}

		
		HashSet<TokenType> expectation = new HashSet<TokenType>(){TokenType.function, TokenType.charString, TokenType.number, TokenType.syntaxChar};
		/// <summary>
		/// Check for input errors and build the parser tree
		/// </summary>
		public void TokenReview() {
			//Enforce sequence rules
			foreach (Token token in tokens.results) {
				switch (token.TokenType) {
					case TokenType.number:
						if (!expectation.Contains(TokenType.number))
							throw new Exception("Syntax error");
						//Next token must be a arithmetic op
						expectation = new HashSet<TokenType>(){TokenType.arithmeticOp, TokenType.function};
						break;
					case TokenType.arithmeticOp:
						if (!expectation.Contains(TokenType.arithmeticOp))
							throw new Exception("Syntax error");
						expectation = new HashSet<TokenType>() {TokenType.number};
						break;
					case TokenType.charString:
						if (!expectation.Contains(TokenType.charString))
							throw new Exception("Syntax error");
						expectation = new HashSet<TokenType>() { };
						break;
					case TokenType.empty:
						break;
					case TokenType.function:
						break;
					case TokenType.syntaxChar:
						break;

				}

			}
			//Build a parse tree to be populated

		}
	}
}
