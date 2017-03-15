using System.Linq.Expressions;

namespace NHibernate.Linq.Expressions
{
	public abstract class NhAggregatedExpression : Expression
	{
		protected NhAggregatedExpression(Expression expression, NhExpressionType nodeType)
			: this(expression, expression.Type, nodeType)
		{
		}

		protected NhAggregatedExpression(Expression expression, System.Type type, NhExpressionType nodeType)
		{
			Expression = expression;
			NodeType = (ExpressionType) nodeType;
			Type = type;
		}

		public override ExpressionType NodeType { get; }

		public override System.Type Type { get; }

		public Expression Expression { get; }

		protected override Expression VisitChildren(ExpressionVisitor visitor)
		{
			var newExpression = visitor.Visit(Expression);

			return newExpression != Expression
				? CreateNew(newExpression)
				: this;
		}

		public abstract Expression CreateNew(Expression expression);
	}
}
