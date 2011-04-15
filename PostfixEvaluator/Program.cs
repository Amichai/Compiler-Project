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

}
