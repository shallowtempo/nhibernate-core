using System.Linq.Expressions;

namespace NHibernate.Linq.Expressions
{
	public class NhStarExpression : Expression
	{
		public NhStarExpression(Expression expression)
		{
			Expression = expression;
		}

		public Expression Expression { get; }

		public override ExpressionType NodeType => (ExpressionType) NhExpressionType.Star;

		public override System.Type Type => Expression.Type;

		protected override Expression VisitChildren(ExpressionVisitor visitor)
		{
			var newExpression = visitor.Visit(Expression);

			return newExpression != Expression
				? new NhStarExpression(newExpression)
				: this;
		}
	}
}