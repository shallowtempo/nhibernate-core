using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using NHibernate.Dialect;
using NHibernate.SqlCommand;
using NHibernate.SqlTypes;
using NHibernate.Util;

namespace NHibernate.Driver
{
	public class FirebirdClientDriver : DriverBase
	{
		private const string SELECT_CLAUSE_EXP = @"(?<=\bselect|\bwhere).*";
		private const string CAST_PARAMS_EXP = @"(?<![=<>]\s?|first\s?|skip\s?|between\s|between\s@\bp\w+\b\sand\s)@\bp\w+\b(?!\s?[=<>])";
		private readonly Regex _statementRegEx = new Regex(SELECT_CLAUSE_EXP, RegexOptions.IgnoreCase);
		private readonly Regex _castCandidateRegEx = new Regex(CAST_PARAMS_EXP, RegexOptions.IgnoreCase);
		private readonly FirebirdDialect _fbDialect = new FirebirdDialect();

		public override bool UseNamedPrefixInSql => true;

		public override bool UseNamedPrefixInParameter => true;

		public override string NamedPrefix => "@";

		public override DbConnection CreateConnection()
		{
			return new FirebirdSql.Data.FirebirdClient.FbConnection();
		}

		public override DbCommand CreateCommand()
		{
			return new FirebirdSql.Data.FirebirdClient.FbCommand();
		}

		public override DbCommand GenerateCommand(CommandType type, SqlString sqlString, SqlType[] parameterTypes)
		{
			var command = base.GenerateCommand(type, sqlString, parameterTypes);

			var expWithParams = GetStatementsWithCastCandidates(command.CommandText);
			if (!string.IsNullOrWhiteSpace(expWithParams))
			{
				var candidates = GetCastCandidates(expWithParams);
				var castParams = from DbParameter p in command.Parameters
				                 where candidates.Contains(p.ParameterName)
				                 select p;
				foreach (var param in castParams)
				{
					TypeCastParam(param, command);
				}
			}

			return command;
		}

		protected override void InitializeParameter(DbParameter dbParam, string name, SqlType sqlType)
		{
			var convertedSqlType = sqlType;
			if (convertedSqlType.DbType == DbType.Currency)
				convertedSqlType = new SqlType(DbType.Decimal);

			base.InitializeParameter(dbParam, name, convertedSqlType);
		}

		private string GetStatementsWithCastCandidates(string commandText)
		{
			return _statementRegEx.Match(commandText).Value;
		}

		private HashSet<string> GetCastCandidates(string statement)
		{
			var candidates =
				_castCandidateRegEx
					.Matches(statement)
					.Cast<Match>()
					.Select(match => match.Value);
			return new HashSet<string>(candidates);
		}

		private void TypeCastParam(DbParameter param, DbCommand command)
		{
			var castType = GetFbTypeFromDbType(param.DbType);
			command.CommandText = command.CommandText.ReplaceWholeWord(param.ParameterName, string.Format("cast({0} as {1})", param.ParameterName, castType));
		}

		private string GetFbTypeFromDbType(DbType dbType)
		{
			return _fbDialect.GetCastTypeName(new SqlType(dbType));
		}
	}
}
