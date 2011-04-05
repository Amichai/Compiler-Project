using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SymbolicManipulation {
	class ParseTree {
		public double NodeValue;
		NodeType nodeType;
		public bool appendedToTree = false;

		public ParseTree(string function, int numOfChildren) {
			this.function = function;
			this.numberOfChildren = numOfChildren;
			nodeType = NodeType.function;
		}

		public ParseTree(int value) {
			NodeValue = value;
			nodeType = NodeType.number;
		}

		//This contains either the operator symbol or the function name
		string function = string.Empty;
		int numberOfChildren = int.MinValue;
		public List<ParseTree> children = new List<ParseTree>();

		public bool HasPrecedenceOver(ParseTree testFunction) {
			if (operatorPrecedence2.Contains(this.function) || operatorPrecedence1.Contains(testFunction.function)) {
				return true;
			} else return false;
		}

		enum NodeType { number, function, numericalOperator }
		static List<string> operatorPrecedence1 = new List<string>() { "+", "-" };
		static List<string> operatorPrecedence2 = new List<string>() { "*", "/", "%" };
		//TODO: Make this system more general and include "^"

		internal double Evaluate() {
			switch (function) {
				case "+":
					return children[0].Evaluate() + children[1].Evaluate();
				case "-":
					return children[0].Evaluate() - children[1].Evaluate();
				case "*":
					return children[0].Evaluate() * children[1].Evaluate();
				case "/":
					return children[0].Evaluate() / children[1].Evaluate();
				case "%":
					return children[0].Evaluate() % children[1].Evaluate();
				default:
					return NodeValue;
			}
		}
	}
}
