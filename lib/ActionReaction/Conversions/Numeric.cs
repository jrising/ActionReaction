using System;
using System.Collections;
using ActionReaction.Actions;
using ActionReaction.Interfaces;

namespace ActionReaction
{
	public class String2Numeric : UnitaryHandler
	{
		public String2Numeric ()
			: base("String2Numeric", "Convert a string to a number", new StringArgumentType(100, "-?\\d+\\.?\\d*", "3.14"),
				new AmbiguousArgumentType(new IArgumentType[2] {
					new RangedArgumentType<int>(int.MinValue, int.MaxValue, 1),
					new RangedArgumentType<double>(double.MinValue, double.MaxValue, 0.0)}), 1)
		{
		}

		public override object Handle(object args) {
			int integer;
			if (int.TryParse ((string) args, out integer))
				return integer;

			double floating;
			if (double.TryParse ((string) args, out floating))
				return floating;

			throw new ArgumentException ("Not a numeric value.");
		}
	}

	public class Numeric2String : UnitaryHandler
	{
		public Numeric2String ()
			: base("Numeric2String", "Convert a number to a string",
				new AmbiguousArgumentType(new IArgumentType[2] {
					new RangedArgumentType<int>(int.MinValue, int.MaxValue, 1),
					new RangedArgumentType<double>(double.MinValue, double.MaxValue, 0.0)}),
				new StringArgumentType(100, "-?\\d+\\.?\\d*", "3.14"), 1)
		{
		}

		public override object Handle(object args) {
			return args.ToString ();
		}
	}

	public class Summation : UnitaryHandler
	{
		public Summation ()
			:base("Summation", "Sum an enumeration of numerics.",
				new EnumerableArgumentType(100, new AmbiguousArgumentType(new IArgumentType[2] {
					new RangedArgumentType<int>(int.MinValue, int.MaxValue, 1),
					new RangedArgumentType<double>(double.MinValue, double.MaxValue, 0.0)})),
				new AmbiguousArgumentType(new IArgumentType[2] {
					new RangedArgumentType<int>(int.MinValue, int.MaxValue, 1),
					new RangedArgumentType<double>(double.MinValue, double.MaxValue, 0.0)}), 3)
		{
		}

		public override object Handle(object args) {
			bool allint = true;
			foreach (object elt in (IEnumerable)args)
				if (!(elt is int)) {
					allint = false;
					break;
				}

			if (allint) {
				int intsum = 0;
				foreach (object elt in (IEnumerable)args)
					intsum += (int)elt;
				return intsum;
			}

			double fltsum = 0;
			foreach (object elt in (IEnumerable)args)
				fltsum += (double)elt;
			return fltsum;
		}
	}
}

