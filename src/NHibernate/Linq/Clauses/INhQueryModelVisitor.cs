using Remotion.Linq;

namespace NHibernate.Linq.Clauses
{
	public interface INhQueryModelVisitor: IQueryModelVisitor
	{
		void VisitNhJoinClause(NhJoinClause nhJoinClause, QueryModel queryModel, int index);

		void VisitNhWithClause(NhWithClause nhWhereClause, QueryModel queryModel, int index);

		void VisitNhHavingClause(NhHavingClause nhWhereClause, QueryModel queryModel, int index);
	}
}