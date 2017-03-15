using System.Linq.Expressions;
using Remotion.Linq.Clauses.Expressions;
using Remotion.Linq.Parsing;
using Remotion.Linq.Parsing.ExpressionVisitors;
using Remotion.Linq.Parsing.ExpressionVisitors.TreeEvaluation;

namespace NHibernate.Linq.Visitors
{
	internal class NhPartialEvaluatingExpressionTreeVisitor : RelinqExpressionVisitor, IPartialEvaluationExceptionExpressionVisitor
	{
		protected override Expression VisitConstant(ConstantExpression expression)
		{
			var value = expression.Value as Expression;
			if (value == null)
			{
				return base.VisitConstant(expression);
			}

			return EvaluateIndependentSubtrees(value);
		}

		public static Expression EvaluateIndependentSubtrees(Expression expression)
		{
			var evaluatedExpression = PartialEvaluatingExpressionVisitor.EvaluateIndependentSubtrees(expression, new NullEvaluatableExpressionFilter());
			return new NhPartialEvaluatingExpressionTreeVisitor().Visit(evaluatedExpression);
		}

		public Expression VisitPartialEvaluationException(PartialEvaluationExceptionExpression expression)
		{
			return Visit(expression.Reduce());
		}
	}

	internal class NullEvaluatableExpressionFilter : EvaluatableExpressionFilterBase
	{
	}
}
