#if FEATURE_WEB_SESSION_CONTEXT

using System;
using System.Collections.Generic;
using NHibernate.Engine;

namespace NHibernate.Context
{
	/// <summary>
	/// Provides a <see cref="ISessionFactory.GetCurrentSession()">current session</see>
	/// for each System.Web.HttpContext. Works only with web applications.
	/// </summary>
	[Serializable]
	public class WebSessionContext : MapBasedSessionContext
	{
		private const string SessionFactoryMapKey = "NHibernate.Context.WebSessionContext.SessionFactoryMapKey";

		public WebSessionContext(ISessionFactoryImplementor factory) : base(factory) {}

		protected override IDictionary<ISessionFactoryImplementor, ISession> GetMap()
		{
			return ReflectiveHttpContext.HttpContextCurrentItems[SessionFactoryMapKey] as IDictionary<ISessionFactoryImplementor, ISession>;
		}

		protected override void SetMap(IDictionary<ISessionFactoryImplementor, ISession> value)
		{
			ReflectiveHttpContext.HttpContextCurrentItems[SessionFactoryMapKey] = value;
		}
	}
}

#endif
