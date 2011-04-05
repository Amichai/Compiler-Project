using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SymbolicManipulation {
	//Can be a function or a value
	class ParseTree {
		List<ParseTree> children = new List<ParseTree>();
		string functionName = string.Empty;
		int evaluationValue = int.MinValue;
		int numberOfChildern = int.MinValue;
		//This is the tree that is under construction
		ParseTree currentTreeState = new ParseTree();
		
		ParseTree currentNode = new ParseTree();
		//The current node can be appended to the currentTree
		//In one of two ways:
		//1-The current tree is a parameter in the current Node operator
		//2-The currentNode is a parameter in the current Tree
		//This decision is made according to order of opertations

		public void AddToken(Token tokenToAdd) {
			//Depending on the nature of this token
			//Add the root to the last token then add the current token
			//Or add the current token to this token (to add the root later)
			
		

			//If the current opertaion does **not** have precedence
			currentNode.children.Add(currentTreeState);
			currentTreeState = currentNode;
			//If the current operation *does* have precedence 
			


			//Structure the tree according tokenToAdd.TokenType
			switch (tokenToAdd.TokenType) {
				case TokenType.number:
					currentNode.evaluationValue = tokenToAdd.TokenNumValue;
					break;
				case TokenType.arithmeticOp:
					//Option 1:
					

					currentNode.numberOfChildern = 2;
					currentNode.functionName = tokenToAdd.TokenString;

					break;
			}
		}

		//Operation parameter stack
		//Make an ordered list of directions to nodes waiting for parameters to fill

		//Hash the location of the live node 
		List<int> directionsToLiveNode = new List<int>();
		private void AddToMostRecentNode() {

		}
	}

	
	//Build a stack of numerical values
	//Build a stack of functions acting on these numerical values
	class ParseTree2 {
		static List<string> operatorPrecedence1 = new List<string>() { "+", "-" };
		static List<string> operatorPrecedence2 = new List<string>() { "*", "/", "%" };

		private class TreeNode {
			int operatorValue;
			NodeType nodeType;
			public bool appendedToTree = false;

			public TreeNode(string function, int numOfChildren) {
				this.function = function;
				this.numberOfChildren = numOfChildren;
				nodeType = NodeType.function;
			}

			public TreeNode(int value) {
				operatorValue = value;
				nodeType = NodeType.number;
			}

			//This contains either the operator symbol or the function name
			string function = string.Empty;
			int numberOfChildren = int.MinValue;
			public List<TreeNode> children = new List<TreeNode>();

			public bool HasPrecedenceOver(TreeNode testFunction) {
				if (operatorPrecedence2.Contains(this.function) || operatorPrecedence1.Contains(testFunction.function)) {
					return true;
				} else return false;
			}

			

			enum NodeType { number, function, numericalOperator }
		}

		List<TreeNode> numberStack = new List<TreeNode>();
		List<TreeNode> operatorStack = new List<TreeNode>();

		public void AddToken(Token tokenToAdd) {
			if (tokenToAdd.TokenType == TokenType.number) {
				numberStack.Add(new TreeNode(tokenToAdd.TokenNumValue));
			} else if (tokenToAdd.TokenType == TokenType.arithmeticOp) {
				operatorStack.Add(new TreeNode(tokenToAdd.TokenString, 2));
			} else if (tokenToAdd.TokenType == TokenType.function) {
				//
			}
		}


		private int rootNode = 0;
		private TreeNode defineOperator(int num) {
			//For first parameter, either take the corresponding number
			//or the last defined operator
			if (operatorStack[num].children.Count() == 0) {
				if (!numberStack[num].appendedToTree) {
					operatorStack[num].children.Add(numberStack[num]);
					numberStack[num].appendedToTree = true;
				} else {
					//if (operatorStack[num - 1].appendedToTree)
					if (operatorStack[rootNode].appendedToTree)
						throw new Exception("Trying to append an operator twice!");
					//operatorStack[num].children.Add(operatorStack[num - 1]);
					operatorStack[num].children.Add(operatorStack[rootNode]);
					rootNode++;
				}
				return defineOperator(num);
			}

			//Not first operator parameter
			else {
				//determine precedence
				//If current operator has precedence over next operator or they are equivalent
				if (num + 1 == operatorStack.Count() || operatorStack[num].HasPrecedenceOver(operatorStack[num + 1])) {
					operatorStack[num].children.Add(numberStack[num + 1]);
					numberStack[num + 1].appendedToTree = true;
					//defineOperator(num + 1);
					return operatorStack[num];
				}
				else {
					i++;
					TreeNode opToAdd = defineOperator(num + 1);
					operatorStack[num].children.Add(opToAdd);
					return operatorStack[num];
				}
			}
			throw new Exception("not all code paths return a value");
		}

		private int i=0;
		public void BuildParseTree() {
			TreeNode ParserTree;
			while (i < operatorStack.Count()) {
				ParserTree = defineOperator(i);
				i++;
			}
		}
	}
}