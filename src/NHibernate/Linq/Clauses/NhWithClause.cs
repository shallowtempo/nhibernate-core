using System;
using System.Linq.Expressions;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ExpressionVisitors;

namespace NHibernate.Linq.Clauses
{
	public class NhWithClause : NhWhereClause, IBodyClause, IClause
	{
		public NhWithClause(Expression predicate)
			: base(predicate)
		{
		}

		public override string ToString()
		{
			return "with " + Predicate;
		}

		/// <summary>
		/// Accepts the specified visitor by calling its <see cref="M:Remotion.Linq.IQueryModelVisitor.VisitWhereClause(Remotion.Linq.Clauses.WhereClause,Remotion.Linq.QueryModel,System.Int32)" /> method.
		/// </summary>
		/// <param name="visitor">The visitor to accept.</param>
		/// <param name="queryModel">The query model in whose context this clause is visited.</param>
		/// <param name="index">The index of this clause in the <paramref name="queryModel" />'s <see cref="P:Remotion.Linq.QueryModel.BodyClauses" /> collection.</param>
		public override void Accept(IQueryModelVisitor visitor, QueryModel queryModel, int index)
		{
			if (visitor == null) throw new ArgumentNullException(nameof(visitor));
			if (queryModel == null) throw new ArgumentNullException(nameof(queryModel));
			((INhQueryModelVisitor)visitor).VisitNhWithClause(this, queryModel, index);
		}

		IBodyClause IBodyClause.Clone(CloneContext cloneContext)
		{
			return Clone(cloneContext);
		}
	}
}
