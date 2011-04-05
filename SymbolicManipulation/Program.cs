using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace SymbolicManipulation {
	class Program {
		static void Main(string[] args) {
			UserInterface UI = new UserInterface();
			string input =
				"5 - 3 + 10 * 2 + 1 * 2";
			UI.Display("Program input: " + input);
			Tokens tokens = new Tokenizer(input).Scan();
			
			//Parser parser = new Parser(tokens);
			//parser.TokenReview();
			//parser.ParseToken().ExpressionEvaluator();
		}
	}

	class CheckAndOrderTokenList {
		int index = 0;
		public CheckAndOrderTokenList(Tokens tokens) {
			Tokens orderedTokens = new Tokens();
			Token currentToken = tokens.results[index];

			switch (currentToken.TokenType) {
				case TokenType.arithmeticOp:
					break;
				case TokenType.charString:
					break;
				case TokenType.function:
					break;
				case TokenType.number:
					break;
				case TokenType.syntaxChar:
					break;
				default:
					throw new Exception("Token type unknown");
			}

			if (tokens.results[index].TokenType == TokenType.number) {
				Operation op = new Operation(tokens.results[index]);
				op.getNextOperand();
			}
			orderedTokens.Add(currentToken);
		}
		private Token getNextNumber() {
			throw new Exception("Unimplemented");
		}
		private Token getNextOperation() {
			throw new Exception("Unimplemented");
		}
		private class Operation {
			public Operation(Token token) {

			}
			List<Token> operands = new List<Token>();
			public void getNextOperand() {

			}
		}
	}

	static class ExtensionMethods {
		public static ExpressionEvaluation ExpressionEvaluator(this Expression syntaxTree) {
			Debug.Print(syntaxTree.Value.ToString());
			return new ExpressionEvaluation();
		}
	}

	class ExpressionEvaluation { }
	class Tokens {
		string allTokenStrings = string.Empty;
		public ParseTree2 parseTree = new ParseTree2();
		public void Add(Token token) {
			results.Add(token);
			parseTree.AddToken(token);
			allTokenStrings += token.TokenString + " ";
			if(token.TokenType == TokenType.openBrace)
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