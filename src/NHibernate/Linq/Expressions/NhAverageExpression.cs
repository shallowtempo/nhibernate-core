using System;
using System.Linq.Expressions;
using NHibernate.Util;

namespace NHibernate.Linq.Expressions
{
	public class NhAverageExpression : NhAggregatedExpression
	{
		public NhAverageExpression(Expression expression) : base(expression, CalculateAverageType(expression.Type), NhExpressionType.Average)
		{
		}

		private static System.Type CalculateAverageType(System.Type inputType)
		{
			var isNullable = false;

			if (inputType.IsNullable())
			{
				isNullable = true;
				inputType = inputType.NullableOf();
			}

			if (inputType == typeof(decimal))
				return isNullable ? typeof(decimal?) : typeof(decimal);
			if (inputType == typeof(short) || inputType == typeof(int) || inputType == typeof(long) || inputType == typeof(float) || inputType == typeof(double))
				return isNullable ? typeof(double?) : typeof(double);

			throw new NotSupportedException(inputType.FullName);
		}

		public override Expression CreateNew(Expression expression)
		{
			return new NhAverageExpression(expression);
		}
	}
}
