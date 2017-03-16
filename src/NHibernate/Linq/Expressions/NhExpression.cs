using System.Linq.Expressions;
using NHibernate.Linq.Visitors;

namespace NHibernate.Linq.Expressions
{
	public abstract class NhExpression : Expression
	{
		protected sealed override Expression Accept(ExpressionVisitor visitor)
		{
			var nhVisitor = visitor as NhExpressionVisitor;
			if (nhVisitor != null)
				return Accept(nhVisitor);

			return base.Accept(visitor);
		}

		protected abstract Expression Accept(NhExpressionVisitor visitor);
	}
}
