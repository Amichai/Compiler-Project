using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostfixEvaluator {
	class Program {
		static void Main(string[] args) {
			string input =
			"5 + 3/(2 / ((4 - 1) * 2))";

			input.ConvertToPostfixNotation()
				.Evaluate();
		}
	}

	static class ExtensionMethods {
		public static string ConvertToPostfixNotation(this string input){
			string postfix = string.Empty;
			char[] stack;
			foreach (char c in input) {
				if (char.IsDigit(c)) {
					postfix += c;
				}
				if (char.IsLetter(c) || c == '_') {
					
				}
			}
			return postfix;
		}

		public static
			double Evaluate(this string inputInPostfixNotation) {
			return 0;
		}

	}
}
