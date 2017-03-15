using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Remotion.Linq.Clauses.Expressions;
using Remotion.Linq.Parsing;

namespace NHibernate.Linq
{
	public class ExtensionExpression : Expression
	{
		public ExtensionExpression(System.Type type, ExpressionType nodeType) 
			: base(nodeType, type)
		{
		}

		public ExtensionExpression()
		{
		}
	}

	public class ExpressionTreeVisitor : RelinqExpressionVisitor
	{
		public override Expression Visit(Expression expression)
		{
			return base.Visit(expression);
		}

		protected override Expression VisitSubQuery(SubQueryExpression expression)
		{
			return base.VisitSubQuery(expression);
		}

		protected override Expression VisitQuerySourceReference(QuerySourceReferenceExpression expression)
		{
			return base.VisitQuerySourceReference(expression);
		}

		protected override Expression VisitMember(MemberExpression expression)
		{
			return base.VisitMember(expression);
		}

		protected override Expression VisitNewArray(NewArrayExpression expression)
		{
			return base.VisitNewArray(expression);
		}

		protected override Expression VisitNew(NewExpression expression)
		{
			return base.VisitNew(expression);
		}

		protected override Expression VisitBinary(BinaryExpression expression)
		{
			return base.VisitBinary(expression);
		}

		protected override Expression VisitConstant(ConstantExpression expression)
		{
			return base.VisitConstant(expression);
		}

		protected override Expression VisitUnary(UnaryExpression expression)
		{
			return base.VisitUnary(expression);
		}

		protected override Expression VisitConditional(ConditionalExpression expression)
		{
			return base.VisitConditional(expression);
		}

		protected override Expression VisitMethodCall(MethodCallExpression expression)
		{
			return base.VisitMethodCall(expression);
		}

		protected override Expression VisitLambda<T>(Expression<T> expression)
		{
			return base.VisitLambda(expression);
		}

		protected override Expression VisitParameter(ParameterExpression expression)
		{
			return base.VisitParameter(expression);
		}

		protected override Expression VisitTypeBinary(TypeBinaryExpression expression)
		{
			throw new NotImplementedException();
		}

		protected virtual Expression VisitExtension(ExtensionExpression expression)
		{
			throw new NotImplementedException();
		}

		protected static void Visit<T>(ReadOnlyCollection<T> expressionArguments, Func<T, T> appendCommas)
		{
			ExpressionVisitor.Visit(expressionArguments, appendCommas);
		}
	}

	class ArgumentUtility
	{
		public static void CheckNotNull<T>(string name, T arg)
		{
			if (arg == null) throw new ArgumentNullException(name);
		}
	}
}