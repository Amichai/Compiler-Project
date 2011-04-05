using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SymbolicManipulation {
	public enum LogType { input, output, token, parseTree, all }
	static class UI {
		static List<Tuple<string, LogType>> objectLog = new List<Tuple<string, LogType>>();
		static bool displayTokens = true;
		static bool displayInput = true;
		static bool displayOutput = true;

		public static string AddToLog(this string obj, LogType type){
			objectLog.Add(new Tuple<string, LogType>(type.ToString() + ": " + obj.ToString(), type));
			return obj;
		}

		public static double AddToLog(this double obj, LogType type) {
			objectLog.Add(new Tuple<string, LogType>(type.ToString() + ": " + obj.ToString(), type));
			return obj;
		}

		public static void Display(string textToDisplay) {
			Debug.Print(textToDisplay);
		}

		public static void DisplayLog(LogType logType) {
			foreach(Tuple<string, LogType> obj in objectLog){
				if(obj.Item2 == logType 
					//Check that we've selected to display this part of the log (from within this class)
					&& (!(obj.Item2 == LogType.token) || displayTokens)
					&& (!(obj.Item2 == LogType.input) || displayInput)
					&& (!(obj.Item2 == LogType.output) || displayOutput))
					Debug.Print(obj.Item1);
			}
		}
	}
}
