using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace Calculator.XUnitTests
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class ExcelDataAttribute : DataAttribute
    {
        private static readonly string connectionTemplate =
       "Provider=Microsoft.ACE.OLEDB.12.0; Data Source={0}; Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';";

        public ExcelDataAttribute(string fileName, string queryString)
        {
            FileName = fileName;
            QueryString = queryString;
        }

        public string FileName { get; }
        public string QueryString { get; }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod == null)
                throw new ArgumentNullException(nameof(testMethod));

            var parameters = testMethod.GetParameters();
            return DataSource(FileName, QueryString, parameters.Select(p => p.ParameterType).ToArray());
        }

        private IEnumerable<object[]> DataSource(string fileName, string selectString, Type[] parameterTypes)
        {
            var connectionString = string.Format(connectionTemplate, GetFullFilename(fileName));

            using (var adapter = new OleDbDataAdapter(selectString, connectionString))
            {
                var dataSet = new DataSet();
                adapter.Fill(dataSet);

                foreach (DataRow row in dataSet.Tables[0].Rows)
                    yield return ConvertParameters(row.ItemArray, parameterTypes);
            }
        }

        private static string GetFullFilename(string filename)
        {
            var executable = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(executable), filename));
        }

        private static object[] ConvertParameters(object[] values, Type[] parameterTypes)
        {
            var result = new object[values.Length];

            for (int i = 0; i < values.Length; i++)
                result[i] = ConvertParameter(values[i], i >= parameterTypes.Length ? null : parameterTypes[i]);

            return result;
        }

        /// <summary>
        /// Converts a parameter to its destination parameter type, if necessary.
        /// </summary>
        /// <param name="parameter">The parameter value</param>
        /// <param name="parameterType">The destination parameter type (null if not known)</param>
        /// <returns>The converted parameter value</returns>
        private static object ConvertParameter(object parameter, Type parameterType)
        {
            if ((parameter is double || parameter is float) &&
                (parameterType == typeof(int) || parameterType == typeof(int?)))
            {
                int intValue;
                var floatValueAsString = parameter.ToString();

                if (int.TryParse(floatValueAsString, out intValue))
                    return intValue;
            }

            return parameter;
        }

    }
}
