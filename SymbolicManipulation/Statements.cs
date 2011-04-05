using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SymbolicManipulation {
    public abstract class Expression {
        public int Value;
    }

    public class AddMethod : Expression {
        public List<Expression> Parameters = new List<Expression>();
        public void SetParameter(Expression parameter) {
            Parameters.Add(parameter);
        }
        public void Evaluate() {
            Value = Parameters.Sum(n => n.Value);
        }
    }

    public class IntStmnt : Expression {
        public IntStmnt(int val) {
            Value = val;
        }
    }

    public class MathOperation : Expression {
        public MathOperation(Expression val) {
            parameter1 = val;
        }
        public void SetParameter2(Expression val){
            parameter2 = val;
        }
        public void SetOperation(string val) {
            operation = val[0];
            if (val.Count() > 1)
                throw new Exception("Not a single character operation");
        }
        private Expression parameter1;
        private Expression parameter2;
        private char operation;

        internal int Evaluate() {
            switch (operation) {
                case '+':
                    return parameter1.Value + parameter2.Value;
                case '-':
                    return parameter1.Value - parameter2.Value;
                case '*':
                    return parameter1.Value * parameter2.Value;
                case '/':
                    return parameter1.Value / parameter2.Value;
                case '%':
                    return parameter1.Value % parameter2.Value;
            }
            throw new Exception("Operation unknown");
        }
    }

    public class AddOperation : Expression {
        public AddOperation(int val) {
            parameter1 = val;
        }
        public int parameter1;
        public int parameter2;
    }

    public class MultOp : Expression {
        public MultOp(int val) {
            parameter1 = val;
        }
        public int parameter1;
        public int parameter2;
    }
}
