using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace SymbolicManipulation {
	class Program {
		static HashSet<char> someOps = new HashSet<char> { '(', '^', '*', '/', '+', '-' };
		public static string AddParenthesis(string input) {
			string withParenthesis = string.Empty;
			withParenthesis += "((((";
			for(int i=0; i != input.Count(); i++){
				char c = input[i];
				switch (c) {
					case '(': withParenthesis+= "(((("; continue;
					case ')': withParenthesis+= "))))"; continue;
					case '^': withParenthesis+= ")^("; continue;
					case '*': withParenthesis+= "))*(("; continue;
					case '/': withParenthesis+= "))/(("; continue;
					case '+':
						if (i == 1 || withParenthesis.Any(a => someOps.Contains(a)))
							withParenthesis+="+";
						else
							withParenthesis+=")))+(((";
						continue;
					case '-':
						if (i == 1 || withParenthesis.Any(a => someOps.Contains(a)))
							withParenthesis += "-";
						else
							withParenthesis += ")))-(((";
						continue;
				}
				withParenthesis += c;
			}
			withParenthesis += "))))";
			return withParenthesis;
		}

		static void Main(string[] args) {
			string input =
			"5 + 3/(2 / ((4 - 1) * 2))"	.AddToLog(LogType.input);

			input = AddParenthesis(input);
			Debug.Print(input);

			//TODO: Teach the tokenizer to handle "^"
			//TODO: BUG:
			//   "5 + 3/2 / (5 - 1) - 2/-7 + (3 - 2 / -5) + -1"
			// is being read as:
			//	"5 + 3/(2 / (5 - 1)) - 2/-7 + (3 - 2 / -5) + -1"
			// I don't know why

			new Tokenizer(input)
					.Scan().AddToLog(LogType.allTokens)
					.parseTree
					.Evaluate().AddToLog(LogType.output);
										
										UI.DisplayLog(LogType.input);
										UI.DisplayLog(LogType.parseTree);
										UI.DisplayLog(LogType.allTokens);
										UI.DisplayLog(LogType.output);
		}
	}
}