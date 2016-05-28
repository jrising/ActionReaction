using System;
using System.Collections.Generic;
using ActionReaction.Actions;
using ActionReaction.Interfaces;
using ActionReaction.Evaluations;

namespace ActionReaction
{
	public class String2Enumerable : UnitaryHandler
	{
		public String2Enumerable ()
			:base("String2Enumerable", "Parse a space-separated list of tokens.",
				new StringArgumentType(1000, ".+", "a bc def ghij"),
				new EnumerableArgumentType(100, new StringArgumentType(100, ".+", "token")), 2)
		{
		}

		public override object Handle(object args) {
			return args.ToString ().Split (new char[] { ' ', '\t', '\n', ',', ';' });
		}
	}
		
	public class Enumerable2NumericEnumerable : UnitaryHandler
	{
		public Enumerable2NumericEnumerable ()
			: base("Enumerable2NumericEnumerable", "Convert each element into a number.",
				new EnumerableArgumentType (100, new StringArgumentType (100, "-?\\d+\\.?\\d*", "3.14")),
				new EnumerableArgumentType(100, new AmbiguousArgumentType(new IArgumentType[2] {
					new RangedArgumentType<int>(int.MinValue, int.MaxValue, 1),
					new RangedArgumentType<double>(double.MinValue, double.MaxValue, 0.0)})), 4)
		{
		}
			
		public override object Handle(object args) {
			List<double> result = new List<double>();
			foreach (string elt in (IEnumerable<string>) args) {
				result.Add (double.Parse (elt));
			}

			return result;
		}
	}
}

