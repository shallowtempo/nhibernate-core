using System.Collections.Generic;
using NHibernate.Engine;

namespace NHibernate.Context
{
	public abstract class MapBasedSessionContext : CurrentSessionContext
	{
		private readonly ISessionFactoryImplementor _factory;

		protected MapBasedSessionContext(ISessionFactoryImplementor factory)
		{
			_factory = factory;
		}

		/// <summary>
		/// Gets or sets the currently bound session.
		/// </summary>
		protected override ISession Session
		{
			get
			{
				IDictionary<ISessionFactoryImplementor, ISession> map = GetMap();
				if (map == null)
				{
					return null;
				}
				else
				{
					ISession value;
					return map.TryGetValue(_factory, out value) ? value : null;
				}
			}
			set
			{
				IDictionary<ISessionFactoryImplementor, ISession> map = GetMap();
				if (map == null)
				{
					map = new Dictionary<ISessionFactoryImplementor, ISession>();
					SetMap(map);
				}
				map[_factory] = value;
			}
		}

		/// <summary>
		/// Get the dictionary mapping session factory to its current session.
		/// </summary>
		protected abstract IDictionary<ISessionFactoryImplementor, ISession> GetMap();

		/// <summary>
		/// Set the map mapping session factory to its current session.
		/// </summary>
		protected abstract void SetMap(IDictionary<ISessionFactoryImplementor, ISession> value);
	}
}
