using System.Data.Common;

namespace NHibernate.Driver
{
	public class SQLiteDriver : DriverBase
	{
		public override bool UseNamedPrefixInSql => true;

		public override bool UseNamedPrefixInParameter => true;

		public override string NamedPrefix => "@";

		public override bool SupportsMultipleOpenReaders => false;

		public override bool SupportsMultipleQueries => true;

		public override DbConnection CreateConnection()
		{
			return new System.Data.SQLite.SQLiteConnection();
		}

		public override DbCommand CreateCommand()
		{
			return new System.Data.SQLite.SQLiteCommand();
		}

		public override IResultSetsCommand GetResultSetsCommand(Engine.ISessionImplementor session)
		{
			return new BasicResultSetsCommand(session);
		}
	}
}
