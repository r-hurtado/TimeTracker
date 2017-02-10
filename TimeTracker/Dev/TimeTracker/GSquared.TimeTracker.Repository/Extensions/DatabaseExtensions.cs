using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace GSquared.TimeTracker.Repository.Extensions
{
    public static class DatabaseExtensions
    {
        /// <summary>
        ///     Execute Stored Procedure
        /// </summary>
        /// <typeparam name="TResult">The type of object that the stored procedure will return a list of</typeparam>
        /// <param name="database">The database context to execute the stored procedure against</param>
        /// <param name="procedure">The procedure object to build the sql statement from</param>
        /// <returns></returns>
        /// <remarks>Executes a stored procedure on the database based on the procedure object that is passed</remarks>
        public static IEnumerable<TResult> ExecuteStoredProcedure<TResult>(this Database database,
                                                                           IStoredProcedure<TResult> procedure)
        {
            var parameters = CreateSqlParametersFromProperties(procedure);
            var format = CreateSpCommand<TResult>(parameters, procedure.ProcedureName);

            return database.SqlQuery<TResult>(format, parameters.Cast<object>().ToArray());
        }

        /// <summary>
        ///     Creates a list of sql parameters based on the public properties of an object
        /// </summary>
        /// <param name="procedure">The procedure object that contains the public properties to build the sql parameters from</param>
        /// <returns></returns>
        /// <remarks>All public properties except the 'ProcedureName' property will be used to build up the sql parameter list</remarks>
        private static List<SqlParameter> CreateSqlParametersFromProperties<TResult>(IStoredProcedure<TResult> procedure)
        {
            var procedureType = procedure.GetType();
            var propertiesOfProcedure =
                procedureType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var parameters =
                propertiesOfProcedure.Select(propertyInfo => new SqlParameter(string.Format("@{0}", propertyInfo.Name),
                                                                              propertyInfo.GetValue(procedure,
                                                                                                    new object[] {}) ??
                                                                              DBNull.Value))
                                     .ToList();
            return
                parameters.Where(
                    p => !p.ParameterName.Equals("@ProcedureName", StringComparison.InvariantCultureIgnoreCase)).
                           ToList();
        }

        /// <summary>
        /// Creates the actual sql command to be executed
        /// </summary>
        /// <typeparam name="TResult">The type of the T result.</typeparam>
        /// <param name="parameters">A list of sql parameters required by the stored procedure</param>
        /// <param name="procedureName">The name of the database object stored procedure</param>
        /// <returns>A sql command string</returns>
        private static string CreateSpCommand<TResult>(List<SqlParameter> parameters, string procedureName)
        {
            var queryString = procedureName;
            parameters.ForEach(x => InlineAssignHelper(ref queryString, string.Format("{0} {1}={1},", queryString, x.ParameterName)));

            return queryString.TrimEnd(',');
        }

        /// <summary>
        /// Helper method to create sql string of parameters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static T InlineAssignHelper<T>(ref T target, T value)
        {
            target = value;
            return value;
        }

        /// <summary>
        /// Interface for creating stored procedure wrapper classes
        /// </summary>
        /// <typeparam name="TResult">The type of the T result.</typeparam>
        public interface IStoredProcedure<TResult>
        {
            string ProcedureName { get; }
        }
    }
}