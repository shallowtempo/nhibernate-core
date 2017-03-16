using System.Linq.Expressions;

namespace NHibernate.Linq.Expressions
{
	/// <summary>
	///     Represents an expression that has been nominated for direct inclusion in the SELECT clause.
	///     This bypasses the standard nomination process and assumes that the expression can be converted
	///     directly to SQL.
	/// </summary>
	/// <remarks>
	///     Used in the nomination of GroupBy key expressions to ensure that matching select clauses
	///     are generated the same way.
	/// </remarks>
	class NhNominatedExpression : Expression
	{
		public NhNominatedExpression(Expression expression)
		{
			Expression = expression;
		}

		public override ExpressionType NodeType => (ExpressionType) NhExpressionType.Nominator;

		public override System.Type Type => Expression.Type;

		public Expression Expression { get; }

		protected override Expression VisitChildren(ExpressionVisitor visitor)
		{
			var newExpression = visitor.Visit(Expression);

			return newExpression != Expression
				? new NhNominatedExpression(newExpression)
				: this;
		}
	}
}
