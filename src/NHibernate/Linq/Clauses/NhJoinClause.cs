using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using NHibernate.Linq.Visitors;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.Expressions;

namespace NHibernate.Linq.Clauses
{
	/// <summary>
	/// All joins are created as outer joins. An optimization in <see cref="WhereJoinDetector"/> finds
	/// joins that may be inner joined and calls <see cref="MakeInner"/> on them.
	/// <see cref="QueryModelVisitor"/>'s <see cref="QueryModelVisitor.VisitAdditionalFromClause"/> will
	/// then emit the correct HQL join.
	/// </summary>
	public class NhJoinClause : NhFromClauseBase, IBodyClause, IClause
	{
		public NhJoinClause(string itemName, System.Type itemType, Expression fromExpression)
			: this(itemName, itemType, fromExpression, new NhWithClause[0])
		{
		}

		public NhJoinClause(string itemName, System.Type itemType, Expression fromExpression, IEnumerable<NhWithClause> restrictions)
			: base(itemName, itemType, fromExpression)
		{
			Restrictions = new ObservableCollection<NhWithClause>();
			foreach (var withClause in restrictions)
				Restrictions.Add(withClause);
			IsInner = false;
		}

		public ObservableCollection<NhWithClause> Restrictions { get; private set; }

		public bool IsInner { get; private set; }

		public NhJoinClause Clone(CloneContext cloneContext)
		{
			var joinClause = new NhJoinClause(ItemName, ItemType, FromExpression);
			foreach (var withClause in Restrictions)
			{
				var withClause2 = new NhWithClause(withClause.Predicate);
				joinClause.Restrictions.Add(withClause2);
			}

			cloneContext.QuerySourceMapping.AddMapping(this, new QuerySourceReferenceExpression(joinClause));
			return joinClause;
		}

		public void MakeInner()
		{
			IsInner = true;
		}

		public override void TransformExpressions(Func<Expression, Expression> transformation)
		{
			foreach (var withClause in Restrictions)
				withClause.TransformExpressions(transformation);
			base.TransformExpressions(transformation);
		}

		/// <summary>
		/// Accepts the specified visitor by calling its <see cref="M:Remotion.Linq.IQueryModelVisitor.VisitAdditionalFromClause(Remotion.Linq.Clauses.AdditionalFromClause,Remotion.Linq.QueryModel,System.Int32)" /> method.
		/// </summary>
		/// <param name="visitor">The visitor to accept.</param>
		/// <param name="queryModel">The query model in whose context this clause is visited.</param>
		/// <param name="index">The index of this clause in the <paramref name="queryModel" />'s <see cref="P:Remotion.Linq.QueryModel.BodyClauses" /> collection.</param>
		public void Accept(IQueryModelVisitor visitor, QueryModel queryModel, int index)
		{
			if (visitor == null) throw new ArgumentNullException(nameof(visitor));
			if (queryModel == null) throw new ArgumentNullException(nameof(queryModel));
			var nhVisitor = visitor as INhQueryModelVisitor;
			if (nhVisitor == null) throw new ArgumentException("Expect visitor to implement INhQueryModelVisitor", nameof(visitor));
			nhVisitor.VisitNhJoinClause(this, queryModel, index);
		}

		IBodyClause IBodyClause.Clone(CloneContext cloneContext)
		{
			return Clone(cloneContext);
		}
	}

	public abstract class NhFromClauseBase : IFromClause, IClause, IQuerySource
	{
		private string _itemName;
		private System.Type _itemType;
		private Expression _fromExpression;

		/// <summary>
		/// Gets or sets a name describing the items generated by this from clause.
		/// </summary>
		/// <remarks>
		/// Item names are inferred when a query expression is parsed, and they usually correspond to the variable names present in that expression.
		/// However, note that names are not necessarily unique within a <see cref="T:Remotion.Linq.QueryModel" />. Use names only for readability and debugging, not for
		/// uniquely identifying <see cref="T:Remotion.Linq.Clauses.IQuerySource" /> objects. To match an <see cref="T:Remotion.Linq.Clauses.IQuerySource" /> with its references, use the
		/// <see cref="P:Remotion.Linq.Clauses.Expressions.QuerySourceReferenceExpression.ReferencedQuerySource" /> property rather than the <see cref="P:Remotion.Linq.Clauses.NhFromClauseBase.ItemName" />.
		/// </remarks>
		public string ItemName
		{
			get { return _itemName; }
			set
			{
				if (string.IsNullOrEmpty(value)) throw new ArgumentException("Value cannot be null or empty.", nameof(value));
				_itemName = value;
			}
		}

		/// <summary>
		/// Gets or sets the type of the items generated by this from clause.
		/// </summary>
		/// <note type="warning">
		/// Changing the <see cref="P:Remotion.Linq.Clauses.NhFromClauseBase.ItemType" /> of a <see cref="T:Remotion.Linq.Clauses.IQuerySource" /> can make all <see cref="T:Remotion.Linq.Clauses.Expressions.QuerySourceReferenceExpression" /> objects that
		/// point to that <see cref="T:Remotion.Linq.Clauses.IQuerySource" /> invalid, so the property setter should be used with care.
		/// </note>
		public System.Type ItemType
		{
			get { return _itemType; }
			set
			{
				if (value == null) throw new ArgumentNullException(nameof(value));
				_itemType = value;
			}
		}

		/// <summary>
		/// The expression generating the data items for this from clause.
		/// </summary>
		public Expression FromExpression
		{
			get { return _fromExpression; }
			set
			{
				if (value == null) throw new ArgumentNullException(nameof(value));
				_fromExpression = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Remotion.Linq.Clauses.NhFromClauseBase" /> class.
		/// </summary>
		/// <param name="itemName">A name describing the items generated by the from clause.</param>
		/// <param name="itemType">The type of the items generated by the from clause.</param>
		/// <param name="fromExpression">The <see cref="T:System.Linq.Expressions.Expression" /> generating data items for this from clause.</param>
		internal NhFromClauseBase(string itemName, System.Type itemType, Expression fromExpression)
		{
			if (string.IsNullOrEmpty(itemName)) throw new ArgumentException("Value cannot be null or empty.", nameof(itemName));
			if (itemType == null) throw new ArgumentNullException(nameof(itemType));
			if (fromExpression == null) throw new ArgumentNullException(nameof(fromExpression));
			_itemName = itemName;
			_itemType = itemType;
			_fromExpression = fromExpression;
		}

		public virtual void CopyFromSource(IFromClause source)
		{
			if (source == null) throw new ArgumentNullException(nameof(source));
			FromExpression = source.FromExpression;
			ItemName = source.ItemName;
			ItemType = source.ItemType;
		}

		/// <summary>
		/// Transforms all the expressions in this clause and its child objects via the given <paramref name="transformation" /> delegate.
		/// </summary>
		/// <param name="transformation">The transformation object. This delegate is called for each <see cref="T:System.Linq.Expressions.Expression" /> within this
		/// clause, and those expressions will be replaced with what the delegate returns.</param>
		public virtual void TransformExpressions(Func<Expression, Expression> transformation)
		{
			if (transformation == null) throw new ArgumentNullException(nameof(transformation));
			FromExpression = transformation(FromExpression);
		}

		public override string ToString()
		{
			return string.Format("from {0} {1} in {2}", ItemType.Name, ItemName, FromExpression);
		}
	}
}