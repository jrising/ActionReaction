using System;
using NUnit.Framework;
using ActionReaction.Actions;
using ActionReaction.Interfaces;
using ActionReaction;

namespace ActionReaction
{
	public class PlusHandler : ArgumentsHandler
	{
		public static SetArgumentTreeArgumentType SummationResultType =
			new SetArgumentTreeArgumentType("sum", new RangedArgumentType<double>(double.MinValue, double.MaxValue, 0.0));

		public PlusHandler()
			: base("Plus", "Add together two numbers.",
				new string[] { "one", "two" },
				new string[] { "first number", "second number" },
				new IArgumentType[] {
					new RangedArgumentType<double>(double.MinValue, double.MaxValue, 0.0),
					new RangedArgumentType<double>(double.MinValue, double.MaxValue, 0.0)},
				new string[] { null, null },
				new bool[] { true, true },
				SummationResultType, 1) {
		}

		public override ArgumentTree Handle(ArgumentTree arg)
		{
			double one = 0, two = 0;
			object value;
			Console.WriteLine (arg);

			if (arg.TryGetValue("one", out value))
				one = (double) value;
			if (arg.TryGetValue("two", out value))
				two = (double) value;

			return new ArgumentTree(one + two);
		}
	}

	[TestFixture]
	class MainClass : IMessageReceiver
	{
		public static void Main (string[] args)
		{
			MainClass instance = new MainClass();
			instance.Run();
		}

		[Test]
		public void Run() {
			Console.WriteLine("Running tests...");

			ActionEnvironment actenv = new ActionEnvironment (this);
			actenv.AddAction (new String2Numeric ());
			actenv.AddAction (new PlusHandler ());

			ArgumentTree sumargs = new ArgumentTree ();
			sumargs.Add("one", (double) 3);
			sumargs.Add("two", "5");

			object result = actenv.ImmediateConvertTo (sumargs, PlusHandler.SummationResultType, 10, 1000);
			Assert.AreEqual(result, 8.0);
		}

		public bool Receive(string message, object reference) {
			Console.WriteLine ("[" + message + "]");
			return true;
		}
	}
}
