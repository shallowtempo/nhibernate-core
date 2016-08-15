using System.Collections;
using System.Collections.Generic;
using NHibernate.Proxy.DynamicProxy;

namespace NHibernate.Test.DynamicEntity
{
	public sealed class DataProxyHandler : Proxy.DynamicProxy.IInterceptor
	{
		private readonly IDictionary<string, object> data = new Dictionary<string, object>();
		private readonly string entityName;

		public DataProxyHandler(string entityName, object id)
		{
			this.entityName = entityName;
			data["Id"] = id;
		}

		public string EntityName
		{
			get { return entityName; }
		}

		public IDictionary<string, object> Data
		{
			get { return data; }
		}

		#region IInterceptor Members

		public object Intercept(InvocationInfo info)
		{
			string methodName = info.TargetMethod.Name;
			if ("get_DataHandler".Equals(methodName))
			{
				return this;
			}
			else if (methodName.StartsWith("set_"))
			{
				string propertyName = methodName.Substring(4);
				data[propertyName] = info.Arguments[0];
			}
			else if (methodName.StartsWith("get_"))
			{
				string propertyName = methodName.Substring(4);
				object value;
				return data.TryGetValue(propertyName, out value) ? value : null;
			}
			else if ("ToString".Equals(methodName))
			{
				return entityName + "#" + data["Id"];
			}
			else if ("GetHashCode".Equals(methodName))
			{
				return GetHashCode();
			}
			return null;
		}

		#endregion
	}
}
