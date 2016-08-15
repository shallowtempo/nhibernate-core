using System.Collections;
using System.Collections.Generic;

namespace NHibernate.Test.NHSpecificTest.NH2664
{
	public class Product
	{
		public virtual string ProductId { get; set; }

		private IDictionary _properties;

		public virtual IDictionary Properties
		{
			get
			{
				if (_properties == null)
					_properties = new Dictionary<object, object>();

				return _properties;
			}
			set { _properties = value; }
		}
	}
}
