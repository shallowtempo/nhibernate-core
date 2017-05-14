using System.Data;
using System.Data.Common;

namespace NHibernate.Driver
{
	public class NpgsqlDriver : DriverBase
	{
		public override bool UseNamedPrefixInSql => true;

		public override bool UseNamedPrefixInParameter => true;

		public override string NamedPrefix => ":";

		public override bool SupportsMultipleOpenReaders => false;

		protected override bool SupportsPreparingCommands => true;

		public override bool SupportsMultipleQueries => true;

		public override DbConnection CreateConnection()
		{
			return new Npgsql.NpgsqlConnection();
		}

		public override DbCommand CreateCommand()
		{
			return new Npgsql.NpgsqlCommand();
		}

		public override IResultSetsCommand GetResultSetsCommand(Engine.ISessionImplementor session)
		{
			return new BasicResultSetsCommand(session);
		}

		protected override void InitializeParameter(DbParameter dbParam, string name, SqlTypes.SqlType sqlType)
		{
			base.InitializeParameter(dbParam, name, sqlType);

			// Since the .NET currency type has 4 decimal places, we use a decimal type in PostgreSQL instead of its native 2 decimal currency type.
			if (sqlType.DbType == DbType.Currency)
				dbParam.DbType = DbType.Decimal;
		}
	}
}
