using Remotion.Linq;

namespace NHibernate.Linq.Clauses
{
	public interface INhQueryModelVisitor: IQueryModelVisitor
	{
		void VisitNhJoinClause(NhJoinClause nhJoinClause, QueryModel queryModel, int index);
		void VisitNhWhereClause(NhWhereClause nhWhereClause, QueryModel queryModel, int index);
	}
}