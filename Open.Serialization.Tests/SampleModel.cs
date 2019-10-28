using System.Collections.Generic;

namespace Open.Serialization.Tests
{
	public class SampleModel
	{
		public static IList<decimal?> DecimalList = new decimal?[] { 3.45678m, 45.6789m, null };

		public static IDictionary<string, decimal?> DecimalLookup { get; set; } = new SortedDictionary<string, decimal?> { { "NumberOne", 1.2345m }, { "NumberTwo", 2345.6m } };

		public static IDictionary<string, double?> DoubleLookup { get; set; } = new SortedDictionary<string, double?> { { "NumberOne", 1.2345 }, { "NumberTwo", 2345.6 } };

		public int IntegerValue { get; set; } = 11;

		public double DoubleValue { get; set; } = 1.2345678901234567891234;
		public decimal DecimalValue1 { get; set; } = 1.2345678901234567891234m;

		public decimal DecimalValue2 { get; set; } = 1.2345000m;
		public decimal DecimalValue3 { get; set; } = 11.000m;

		public string StringValue { get; set; } = "Hello";
		public string StringValueNull { get; set; } = null;

		public int? NullableIntegerValue { get; set; } = 11;
		public int? NullableIntegerNull { get; set; } = null;

		public double? NullableDoubleNull { get; set; } = null;

		public double? NullableDoubleValue { get; set; } = 1.2345678901234567891234;
		public decimal? NullableDecimalValue { get; set; } = 1.2345678901234567891234m;
		public decimal? NullableDecimalNull { get; set; } = null;

		public SampleModel Child { get; set; }

		public static readonly SampleModel Instance = new SampleModel() { Child = new SampleModel() };
	}
}
