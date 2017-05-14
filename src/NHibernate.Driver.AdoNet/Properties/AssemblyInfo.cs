using System;
using System.Reflection;
using System.Security;

[assembly: CLSCompliantAttribute(true)]
[assembly: AssemblyTitleAttribute("NHibernate.Driver.ReflectionBased")]
[assembly: AssemblyDescriptionAttribute("Driver for all ADO.Net data providers not covered by other NuGet packages. To be used with NHibernate 5.  " 
	+ "Includes: CsharpSqliteDriver, DB2400Driver, DB2Driver, DotConnectMySqlDriver, IfxDriver, IngresDriver, OracleClientDriver, OracleDataClientDriver, OracleLiteDataClientDriver, SybaseAsaClientDriver, SybaseAseClientDriver, SybaseSQLAnywhereDotNet4Driver, and SybaseSQLAnywhereDriver.  "
	+ "The following are also included, but try to use the dedicated NuGet package: FirebirdClientDriver, MySqlDataDriver, NpgsqlDriver, OracleManagedDataClientDriver, SQLite20Driver, SqlServerCeDriver.")]
[assembly: AssemblyCompanyAttribute("NHibernate.info")]
[assembly: AssemblyProductAttribute("NHibernate.Driver.ReflectionBased")]
[assembly: AssemblyCopyrightAttribute("Licensed under LGPL.")]
[assembly: AssemblyDelaySignAttribute(false)]
[assembly: AllowPartiallyTrustedCallersAttribute()]
[assembly: SecurityRulesAttribute(SecurityRuleSet.Level1)]
