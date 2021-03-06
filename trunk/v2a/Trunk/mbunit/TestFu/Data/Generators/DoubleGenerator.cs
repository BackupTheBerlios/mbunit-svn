using System;
using System.Data;

namespace TestFu.Data.Generators
{
	/// <summary>
	/// A random data generator for <see cref="double"/> values.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This <see cref="IDataGenerator"/> method generates double values in a range [<see cref="MinValue"/>, <see cref="MaxValue"/>].
	/// </para>
    /// </remarks>
	public class DoubleGenerator : DataGeneratorBase
	{
		private double minValue = 0;
		private double maxValue = 1;
		
		public DoubleGenerator(DataColumn column)
		:base(column)
		{}

		/// <summary>
		/// Gets the generated type
		/// </summary>
		/// <value>
		/// Generated type.
		/// </value>
		public override Type GeneratedType 
		{
			get
			{
				return typeof(System.Double);
			}
		}

		/// <summary>
		/// Gets or sets the minimum generated value
		/// </summary>
		/// <value>
		/// Minimum generated value. Default is <see cref="double.MinValue"/>
		/// </value>
		public double MinValue
		{
			get
			{
				return this.minValue;
			}
			set
			{
				this.minValue=value;
			}
		}
		
		/// <summary>
		/// Gets or sets the maximum generated value
		/// </summary>
		/// <value>
		/// Maximum generated value. Default is <see cref="double.MaxValue"/>
		/// </value>
		public double MaxValue
		{
			get
			{
				return this.maxValue;
			}
			set
			{
				this.maxValue=value;
			}
		}
		/// <summary>
		/// Generates a new value
		/// </summary>
		/// <returns>
		/// New random data.
		/// </returns>		
		public override void GenerateData(DataRow row)
		{
			if (this.FillNull(row))
				return;
			
			this.FillData(
			    row,
				this.MinValue + Rnd.NextDouble()* (this.MaxValue - this.MinValue)
				);
		}		
	}
}
