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

			string input = //Console.ReadLine();
				"5 - 3 + 10 * 2 + 1 * 2";
			UI.Display("Program input: " + input);
			double returnVal = new Tokenizer(input).Scan().parseTree.BuildParseTree().Evaluate();
			UI.Display(returnVal.ToString());
		}
	}
}