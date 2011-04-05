using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SymbolicManipulation {
	class UserInterface {
		List<string> objectLog = new List<string>();
		public void AddToLog(string obj){
			objectLog.Add(obj);
		}

		public void Display(string textToDisplay) {
			Debug.Print(textToDisplay);
		}

		public void DisplayLog() {
			foreach(object obj in objectLog){
				Debug.Print(obj.ToString());
			}
		}
	}
}
