using System.Linq.Expressions;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ExpressionVisitors;

namespace NHibernate.Linq.Clauses
{
	public class NhWithClause : NhWhereClause
	{
		public NhWithClause(Expression predicate)
			: base(predicate)
		{
		}

		public override string ToString()
		{
			return "with " + Predicate;
		}
	}
}
