using System;
using System.Collections.Generic;
using ActionReaction;
using ActionReaction.Interfaces;

namespace Commander
{
	class MainClass : IMessageReceiver
	{
		public static void Main (string[] argv)
		{
			MainClass instance = new MainClass();
			instance.Run(argv);
		}

		public void Run(string[] argv) {
			ActionEnvironment actenv = new ActionEnvironment (this);
			actenv.AddAction (new String2Numeric ());
			actenv.AddAction (new String2Enumerable ());
			actenv.AddAction (new Summation ());
			actenv.AddAction (new Numeric2String ());
			actenv.AddAction (new Enumerable2NumericEnumerable ());

			object result = actenv.ImmediateConvertTo (argv, new StringArgumentType(1000, ".+", "result"), 10, 1000);
			if (result is Exception) {
				Console.WriteLine (((Exception)result).Message);
				Console.WriteLine (((Exception)result).StackTrace);
			} else {
				Console.WriteLine (result);
			}
		}

		public bool Receive(string message, object reference) {
			Console.WriteLine ("[" + message + "]");
			return true;
		}
	}
}
