using System;	
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SymbolicManipulation {
	public class Expression {
		public Expression() {
		}
		public static int counter = 0;
		public List<Expression> Children = new List<Expression>();
		public double EvaluationValue = double.MinValue;
		public bool appendedToTree = false;

		public List<Number> NumberStack = new List<Number>();
		public List<Operation> OperatorStack = new List<Operation>();
		public List<Expression> ExpressionStack = new List<Expression>();

		public virtual double Evaluate() { return int.MinValue; }
		private int rootNode = 0;
		private Operation defineOperator(int num) {
			//For first parameter, either take the corresponding number
			//or the last defined operator
			if (OperatorStack[num].Children.Count() == 0) {
				if (!NumberStack[num].appendedToTree) {
					OperatorStack[num].Children.Add(NumberStack[num]);
					NumberStack[num].appendedToTree = true;
				} else {
					if (OperatorStack[rootNode].appendedToTree)
						throw new Exception("Trying to append an operator twice!");
					OperatorStack[num].Children.Add(OperatorStack[rootNode]);
					rootNode = num;
				}
				return defineOperator(num);
			}
				//Not first operator parameter
			else {
				//determine precedence
				//If current operator has precedence over next operator or they are equivalent
				if (num + 1 == OperatorStack.Count() || OperatorStack[rootNode].HasPrecedenceOver(OperatorStack[num + 1])) {
					OperatorStack[num].Children.Add(NumberStack[num + 1]);
					NumberStack[num + 1].appendedToTree = true;
					return OperatorStack[num];
				} else {
					i++;
					Expression opToAdd = defineOperator(num + 1);
					OperatorStack[num].Children.Add(opToAdd);
					return OperatorStack[num];
				}
			}
			throw new Exception("not all code paths return a value");
		}

		private int i = 0;
		public Expression BuildParseTree() {
			Operation ParserTree = null;
			while (i < OperatorStack.Count()) {
				ParserTree = defineOperator(i);
				i++;
			}
			ParserTree.EvaluationValue = ParserTree.Evaluate();
			return ParserTree;
		}
	}

	public class Number : Expression {
		public Number(double num) {
			EvaluationValue = num;
		}
		public override double Evaluate() {
			return EvaluationValue;
		}
	}

	public class Operation : Expression {
		public string operationType;
		public Operation(string c) {
			operationType = c;
		}

		public override double Evaluate() {
			switch (operationType) {
				case "+":
					return Children[0].Evaluate() + Children[1].Evaluate();
				case "-":
					return Children[0].Evaluate() - Children[1].Evaluate();
				case "*":
					return Children[0].Evaluate() * Children[1].Evaluate();
				case "/":
					return Children[0].Evaluate() / Children[1].Evaluate();
				case "%":
					return Children[0].Evaluate() % Children[1].Evaluate();
				default:
					throw new Exception("unknown operator");
			}
		}

		private int getOperatorValue(string op) {
			int opValue = int.MinValue;
			if (op == "+" ||
				op == "-") {
				opValue = 1;
			}
			if (op == "*" ||
				op == "/") {
				opValue = 2;
			}
			if (op == "^") {
				opValue = 3;
			}
			if (opValue == int.MinValue)
				throw new Exception("Unable to evaluate operator value");
			return opValue;
		}

		public bool HasPrecedenceOver(Operation testFunction) {
			int op1Value = getOperatorValue(this.operationType);
			int op2Value = getOperatorValue(testFunction.operationType);
			if (op1Value < op2Value)
				return false;
			else return true;
		}
	}

	public class Function : Expression {
		public Function(string functionName) {
		}
	}

}
