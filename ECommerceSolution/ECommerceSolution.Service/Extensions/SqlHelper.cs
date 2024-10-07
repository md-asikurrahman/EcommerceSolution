using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace ECommerceSolution.Service.Extensions
{
    /// <summary>
    /// The SqlHelper class provides methods to interface with SQL
    /// through the .NET ADO.NET interface.
    /// </summary>
    public class SqlHelper
    {
        #region Fields      

        #endregion        

        #region ConvertStringValueToSqlDbType

        private static object ConvertStringValueToSqlDbType(string stringValue, SqlDbType sqlDbType)
        {
            object convertedValue = null;

            switch (sqlDbType)
            {
                case SqlDbType.Bit:
                case SqlDbType.TinyInt:
                    convertedValue = Byte.Parse(stringValue);
                    break;
                case SqlDbType.SmallInt:
                    convertedValue = Int16.Parse(stringValue);
                    break;
                case SqlDbType.Int:
                    convertedValue = Int32.Parse(stringValue);
                    break;
                case SqlDbType.BigInt:
                    convertedValue = Int64.Parse(stringValue);
                    break;
                case SqlDbType.Char:
                case SqlDbType.VarChar:
                case SqlDbType.Time:
                case SqlDbType.Date:
                case SqlDbType.DateTime:
                case SqlDbType.SmallDateTime:
                    //  Remove outer ' characters
                    convertedValue = stringValue.Substring(1, stringValue.Length - 2);
                    break;
                case SqlDbType.Decimal:
                case SqlDbType.Money:
                    convertedValue = float.Parse(stringValue);
                    break;
                case SqlDbType.SmallMoney:
                    //
                    //  This may be incorrect.....SmallMoney is a 4 byte float...what is the C# equivalent?
                    //
                    convertedValue = float.Parse(stringValue);
                    break;
                default:
                    throw new Exception(string.Format("Unsupported SqlDbType '{0}'.", sqlDbType));
            }

            return convertedValue;
        }

        #endregion

        #region ConvertValueIn

        private static object ConvertValueIn(SqlDbType ValueType, object Value)
        {
            if (Value == null)
                return DBNull.Value;

            switch (ValueType)
            {
                case SqlDbType.Bit:
                    return ConvertValueIn(typeof(bool), Value);

                default:
                    return Value;
            }
        }

        private static object ConvertValueIn(Type ValueType, object Value)
        {
            if (Value == null)
                return DBNull.Value;

            if (ValueType == typeof(bool))
            {
                if (Value is string)
                {
                    string strValue = (string)Value;

                    if (strValue == "0")
                        return 0;
                    else if (strValue == "1")
                        return 1;
                }

                return (Convert.ToBoolean(Value) == true) ? 1 : 0;
            }

            return Value;
        }

        #endregion

        #region ConvertValueOut

        private static object ConvertValueOut(SqlDbType ValueType, object Value)
        {
            if (Value == DBNull.Value)
                return null;

            switch (ValueType)
            {
                case SqlDbType.Bit:
                    return Convert.ToBoolean(Value);

                default:
                    return Value;
            }
        }

        private static object ConvertValueOut(Type ValueType, object Value)
        {
            if (Value == DBNull.Value)
                return null;

            if (ValueType == typeof(bool))
            {
                return Convert.ToBoolean(Value);
            }

            //
            //  For unboxing generic object types that can be converted
            //  but will not generically unbox on assignment because of
            //  casting 'object' to 'type' when the base type of the object
            //  is not the same type.  Example:
            //
            //      DOES NOT WORK:
            //          object value = (Int32)0;
            //          Int64 newValue = (Int64)value;
            //
            //      WORKS:
            //          object value = (Int32)0;
            //          Int64 newValue = Convert.ChangeType(typeof(Int64), value);
            //
            //  This handles convertion of byte to int, int to long, int to decimal, etc.
            //  when the type of a value returned in a query is not the same as, 
            //  but still convertible to, the target.
            //
            if (ValueType.IsGenericType)
            {
                Type genericType = ValueType.GetGenericTypeDefinition();

                if (genericType == typeof(Nullable<>))
                {
                    Type underlyingType = Nullable.GetUnderlyingType(ValueType);
                    return Convert.ChangeType(Value, underlyingType);
                }
                else
                    return Convert.ChangeType(Value, ValueType);
            }
            else
                return Convert.ChangeType(Value, ValueType);
        }

        #endregion

        #region ExecuteDataSet

        /// <summary>
        /// Executes the SQL Command and returns a data set containing one or more 
        /// tables returned by the specified Command.
        /// </summary>
        /// <param name="connection">
        /// The SqlConnection to use for the SQL Command.  This must already be open.
        /// </param>
        /// <param name="command">
        /// The SQL Command.
        /// </param>
        /// <param name="commandType">
        /// The CommandType of the SQL Command.
        /// </param>
        /// <param name="parameters">
        /// The ParameterList to be used for the SQL command.
        /// <br/>
        /// This list must be built using the Add(string, object, SqlDbType) or 
        /// Add(string, object, SqlDbType, SqlParameterDirection) overloads of ParameterList, 
        /// so that SQL data types and parameter directions are defined.
        /// </param>
        /// <returns>
        /// A DataSet containing one or more tables.  Each table will have the column 
        /// schema and all rows returned by the SQL command.
        /// <br/><br/>
        /// If no tables are returned by the specified Command, then the DataSet.Tables.Count will be 0.
        /// <br/><br/>
        /// If no rows are returned for a table, then the DataSet.Tables[].Count will be 0.
        /// </returns>
        /// <example>
        /// <code>
        /// ParameterList parameters = new ParameterList();
        /// parameters.Add("@Parameter1", 1, SqlDbType.BigInt);
        /// parameters.Add("@Parameter2", 1, SqlDbType.Int);
        /// parameters.Add("@Parameter3", 1, SqlDbType.SmallInt);
        /// parameters.Add("@Parameter4", DateTime.Now, SqlDbType.DateTime);
        /// parameters.Add("@Parameter5", isError, SqlDbType.Bit);
        /// parameters.Add("@OutputParameter", 0, SqlDbType.Int, ParameterDirection.Output);
        /// parameters.Add("@InputOutputParameter", 999, SqlDbType.Int, ParameterDirection.InputOutput);
        /// parameters.Add("@ReturnValue", 0, SqlDbType.Int, ParameterDirection.ReturnValue);
        /// 
        /// DataSet dataSet = SqlHelper.ExecuteDataSet(
        ///     _ProgramConnection, "MyQueryStoredProcedureName", CommandType.StoredProcedure, parameters);
        ///     
        /// int outputParameter = parameters.Get&lt;int&gt;("@OutputParameter");
        /// int inputOutputParameter = parameters.Get&lt;int&gt;("@InputOutputParameter");
        /// int returnValue = parameters.Get&lt;int&gt;("@ReturnValue");
        /// </code>
        /// </example>
        public static DataSet ExecuteDataSet(SqlConnection connection, string command, CommandType commandType, ParameterList parameters)
        {


            DataSet dataSet = new DataSet();

            SqlParameter[] sqlParameterList = GetSqlParameterList(parameters);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;
            sqlCommand.CommandText = command;
            sqlCommand.CommandType = commandType;
            sqlCommand.Parameters.AddRange(sqlParameterList);

            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
                do
                {
                    DataTable dataTable = new DataTable();

                    while (sqlDataReader.Read() == true)
                    {
                        if (dataTable.Columns.Count == 0)
                        {
                            for (int columnIndex = 0; (columnIndex < sqlDataReader.FieldCount); columnIndex++)
                            {
                                Type columnType = sqlDataReader.GetFieldType(columnIndex);
                                object value = sqlDataReader.GetProviderSpecificFieldType(columnIndex);
                                DataColumn column = new DataColumn();
                                column.ColumnName = sqlDataReader.GetName(columnIndex);
                                column.DataType = columnType;
                                dataTable.Columns.Add(column);
                            }
                        }

                        DataRow row = dataTable.NewRow();
                        for (int columnIndex = 0; (columnIndex < sqlDataReader.FieldCount); columnIndex++)
                        {
                            object value = sqlDataReader.GetValue(columnIndex);
                            row[columnIndex] = value;
                        }
                        dataTable.Rows.Add(row);
                    }

                    dataSet.Tables.Add(dataTable);
                } while (sqlDataReader.NextResult() == true);

                UpdateReturnedParameters(sqlParameterList, parameters);

                sqlDataReader.Close();
            }

            return dataSet;
        }

        /// <summary>
        /// Executes the SQL Command and returns a data set containing one or more 
        /// tables returned by the specified Command.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string to use for the SQL Command.
        /// </param>
        /// <param name="command">
        /// The SQL Command.
        /// </param>
        /// <param name="commandType">
        /// The CommandType of the SQL Command.
        /// </param>
        /// <param name="parameters">
        /// The ParameterList to be used for the SQL command.
        /// <br/>
        /// This list must be built using the Add(string, object, SqlDbType) or 
        /// Add(string, object, SqlDbType, SqlParameterDirection) overloads of ParameterList, 
        /// so that SQL data types and parameter directions are defined.
        /// </param>
        /// <returns>
        /// A DataSet containing one or more tables.  Each table will have the column 
        /// schema and all rows returned by the SQL command.
        /// <br/><br/>
        /// If no tables are returned by the specified Command, then the DataSet.Tables.Count will be 0.
        /// <br/><br/>
        /// If no rows are returned for a table, then the DataSet.Tables[].Count will be 0.
        /// </returns>
        /// <example>
        /// <code>
        /// ParameterList parameters = new ParameterList();
        /// parameters.Add("@Parameter1", 1, SqlDbType.BigInt);
        /// parameters.Add("@Parameter2", 1, SqlDbType.Int);
        /// parameters.Add("@Parameter3", 1, SqlDbType.SmallInt);
        /// parameters.Add("@Parameter4", DateTime.Now, SqlDbType.DateTime);
        /// parameters.Add("@Parameter5", isError, SqlDbType.Bit);
        /// parameters.Add("@OutputParameter", 0, SqlDbType.Int, ParameterDirection.Output);
        /// parameters.Add("@InputOutputParameter", 999, SqlDbType.Int, ParameterDirection.InputOutput);
        /// parameters.Add("@ReturnValue", 0, SqlDbType.Int, ParameterDirection.ReturnValue);
        /// 
        /// DataSet dataSet = SqlHelper.ExecuteDataSet(
        ///     _ProgramConnectionString, "MyQueryStoredProcedureName", CommandType.StoredProcedure, parameters);
        ///     
        /// int outputParameter = parameters.Get&lt;int&gt;("@OutputParameter");
        /// int inputOutputParameter = parameters.Get&lt;int&gt;("@InputOutputParameter");
        /// int returnValue = parameters.Get&lt;int&gt;("@ReturnValue");
        /// </code>
        /// </example>
        public static DataSet ExecuteDataSet(string connectionString, string command, CommandType commandType, ParameterList parameters)
        {


            DataSet dataSet = new DataSet();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if (!connection.State.Equals("Open"))
                {
                    connection.Open();
                }
                dataSet = ExecuteDataSet(connection, command, commandType, parameters);

                connection.Close();
            }

            return dataSet;
        }

        #endregion

        #region ExecuteDataTable

        /// <summary>
        /// Executes the SQL Command and returns a data table containing the first 
        /// table (presumably the only table) returned by the specified Command.
        /// </summary>
        /// <param name="connection">
        /// The SqlConnection to use for the SQL Command.  This must already be open.
        /// </param>
        /// <param name="command">
        /// The SQL Command.
        /// </param>
        /// <param name="commandType">
        /// The CommandType of the SQL Command.
        /// </param>
        /// <param name="parameters">
        /// The ParameterList to be used for the SQL command.
        /// <br/>
        /// This list must be built using the Add(string, object, SqlDbType) or 
        /// Add(string, object, SqlDbType, SqlParameterDirection) overloads of ParameterList, 
        /// so that SQL data types and parameter directions are defined.
        /// </param>
        /// <returns>
        /// A DataTable containing the column schema and all rows returned by the SQL command.
        /// <br/><br/>
        /// If no rows are returned, then the DataTable.Rows.Count will be 0.
        /// </returns>
        /// <example>
        /// <code>
        /// ParameterList parameters = new ParameterList();
        /// parameters.Add("@Parameter1", 1, SqlDbType.BigInt);
        /// parameters.Add("@Parameter2", 1, SqlDbType.Int);
        /// parameters.Add("@Parameter3", 1, SqlDbType.SmallInt);
        /// parameters.Add("@Parameter4", DateTime.Now, SqlDbType.DateTime);
        /// parameters.Add("@Parameter5", isError, SqlDbType.Bit);
        /// parameters.Add("@OutputParameter", 0, SqlDbType.Int, ParameterDirection.Output);
        /// parameters.Add("@InputOutputParameter", 999, SqlDbType.Int, ParameterDirection.InputOutput);
        /// parameters.Add("@ReturnValue", 0, SqlDbType.Int, ParameterDirection.ReturnValue);
        /// 
        /// DataTable dataTable = SqlHelper.ExecuteDataTable(
        ///     _ProgramConnection, "MyQueryStoredProcedureName", CommandType.StoredProcedure, parameters);
        ///     
        /// int outputParameter = parameters.Get&lt;int&gt;("@OutputParameter");
        /// int inputOutputParameter = parameters.Get&lt;int&gt;("@InputOutputParameter");
        /// int returnValue = parameters.Get&lt;int&gt;("@ReturnValue");
        /// </code>
        /// </example>
        public static DataTable ExecuteDataTable(SqlConnection connection, string command, CommandType commandType, ParameterList parameters)
        {
            DataSet dataSet = ExecuteDataSet(connection, command, commandType, parameters);
            return (dataSet.Tables.Count == 0) ? null : dataSet.Tables[0];
        }

        /// <summary>
        /// Executes the SQL Command and returns a data table containing the first 
        /// table (presumably the only table) returned by the specified Command.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string to use for the SQL Command.
        /// </param>
        /// <param name="command">
        /// The SQL Command.
        /// </param>
        /// <param name="commandType">
        /// The CommandType of the SQL Command.
        /// </param>
        /// <param name="parameters">
        /// The ParameterList to be used for the SQL command.
        /// <br/>
        /// This list must be built using the Add(string, object, SqlDbType) or 
        /// Add(string, object, SqlDbType, SqlParameterDirection) overloads of ParameterList, 
        /// so that SQL data types and parameter directions are defined.
        /// </param>
        /// <returns>
        /// A DataTable containing the column schema and all rows returned by the SQL command.
        /// <br/><br/>
        /// If no rows are returned, then the DataTable.Rows.Count will be 0.
        /// </returns>
        /// <example>
        /// <code>
        /// ParameterList parameters = new ParameterList();
        /// parameters.Add("@Parameter1", 1, SqlDbType.BigInt);
        /// parameters.Add("@Parameter2", 1, SqlDbType.Int);
        /// parameters.Add("@Parameter3", 1, SqlDbType.SmallInt);
        /// parameters.Add("@Parameter4", DateTime.Now, SqlDbType.DateTime);
        /// parameters.Add("@Parameter5", isError, SqlDbType.Bit);
        /// parameters.Add("@OutputParameter", 0, SqlDbType.Int, ParameterDirection.Output);
        /// parameters.Add("@InputOutputParameter", 999, SqlDbType.Int, ParameterDirection.InputOutput);
        /// parameters.Add("@ReturnValue", 0, SqlDbType.Int, ParameterDirection.ReturnValue);
        /// 
        /// DataTable dataTable = SqlHelper.ExecuteDataTable(
        ///     _ProgramConnectionString, "MyQueryStoredProcedureName", CommandType.StoredProcedure, parameters);
        ///     
        /// int outputParameter = parameters.Get&lt;int&gt;("@OutputParameter");
        /// int inputOutputParameter = parameters.Get&lt;int&gt;("@InputOutputParameter");
        /// int returnValue = parameters.Get&lt;int&gt;("@ReturnValue");
        /// </code>
        /// </example>
        public static DataTable ExecuteDataTable(string connectionString, string command, CommandType commandType, ParameterList parameters)
        {
            DataSet dataSet = ExecuteDataSet(connectionString, command, commandType, parameters);
            return (dataSet.Tables.Count == 0) ? null : dataSet.Tables[0];
        }

        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// Executes the specified SQL Command and returns the number of fows that were affected.
        /// </summary>
        /// <param name="connection">
        /// The SqlConnection to use for the SQL Command.  This must already be open.
        /// </param>
        /// <param name="command">
        /// The SQL command (query or stored procedure name).
        /// </param>
        /// <param name="commandType">
        /// The CommandType of the SQL Command.
        /// </param>
        /// <param name="parameters">
        /// The ParameterList to be used for the SQL command or NULL if there are no parameters.
        /// <br/>
        /// This list must be built using the Add(string, object, SqlDbType) or 
        /// Add(string, object, SqlDbType, SqlParameterDirection) overloads of ParameterList, 
        /// so that SQL data types and parameter directions are defined.
        /// </param>
        /// <param name="commandTimeoutInSeconds">
        /// The number of seconds before the SQL command will timeout.  
        /// If this value is less than or equal to 0, the default SqlCommand timeout of 30 seconds will be used.
        /// </param>
        /// <returns>
        /// The number of rows affected by the command.  Note - this return value is only valid for 
        /// CommandType = CommandType.Text.  Otherwise, the return value will be -1.
        /// </returns>
        /// <remarks>
        /// Once the command is executed, the SQL connection will remain open.
        /// <br/>
        /// If the Connection is using a transaction, then the command will be executed on the transaction.
        /// </remarks>
        /// <example>
        /// <code>
        /// using (SqlConnection connection = new SqlConnection("MyConnectionString"))
        /// {
        ///     ParameterList parameters = new ParameterList();
        ///     parameters.Add("@Parameter1", 1, SqlDbType.BigInt);
        ///     parameters.Add("@Parameter2", 1, SqlDbType.Int);
        ///     parameters.Add("@Parameter3", 1, SqlDbType.SmallInt);
        ///     parameters.Add("@Parameter4", DateTime.Now, SqlDbType.DateTime);
        ///     parameters.Add("@Parameter5", isError, SqlDbType.Bit);
        ///     parameters.Add("@OutputParameter", 0, SqlDbType.Int, ParameterDirection.Output);
        ///     parameters.Add("@InputOutputParameter", 999, SqlDbType.Int, ParameterDirection.InputOutput);
        ///     parameters.Add("@ReturnValue", 0, SqlDbType.Int, ParameterDirection.ReturnValue);
        /// 
        ///     Int32 commandTimeoutSeconds = 300;  // 5 minutes
        /// 
        ///     SqlHelper.ExecuteNonQuery(
        ///         connection, "MyStoredProcedureName", CommandType.StoredProcedure, parameters, commandTimeoutSeconds);
        ///    
        ///     Int32 outputValue1 = parameters.Get&lt;Int32&gt;("@OutputParameter");
        ///     Int32 outputValue2 = parameters.Get&lt;Int32&gt;("@InputOutputParameter");
        ///     Int32 returnValue = parameters.Get&lt;Int32&gt;("@ReturnValue");
        ///     
        ///     Int32 numberOfRowsAffected = SqlHelper.ExecuteNonQuery(
        ///         connection, "DELETE FROM MyTable WHERE [Type] = 1", CommandType.Text, null, timeoutSeconds);
        ///
        /// }   // &lt;---- connection closed here
        /// </code>
        /// </example>
        public static Int32 ExecuteNonQuery(SqlConnection connection, string command, CommandType commandType, ParameterList parameters, Int32 commandTimeoutInSeconds)
        {


            Int32 rowsAffected = 0;

            SqlParameter[] sqlParameterList = GetSqlParameterList(parameters);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;
            sqlCommand.CommandText = command;
            sqlCommand.CommandType = commandType;
            // sqlCommand.Transaction = GetCurrentTransaction(connection);

            if (commandTimeoutInSeconds > 0)
                sqlCommand.CommandTimeout = commandTimeoutInSeconds;

            sqlCommand.Parameters.AddRange(sqlParameterList);

            rowsAffected = sqlCommand.ExecuteNonQuery();

            UpdateReturnedParameters(sqlParameterList, parameters);

            if (commandType == CommandType.Text)
                return rowsAffected;
            else
                return -1;
        }

        /// <summary>
        /// Executes the specified SQL Command and returns the number of fows that were affected.
        /// </summary>
        /// <param name="connection">
        /// The SqlConnection to use for the SQL Command.  This must already be open.
        /// </param>
        /// <param name="command">
        /// The SQL command (query or stored procedure name).
        /// </param>
        /// <param name="commandType">
        /// The CommandType of the SQL Command.
        /// </param>
        /// <param name="parameters">
        /// The ParameterList to be used for the SQL command or NULL if there are no parameters.
        /// <br/>
        /// This list must be built using the Add(string, object, SqlDbType) or 
        /// Add(string, object, SqlDbType, SqlParameterDirection) overloads of ParameterList, 
        /// so that SQL data types and parameter directions are defined.
        /// </param>
        /// <returns>
        /// The number of rows affected by the command.  Note - this return value is only valid for 
        /// CommandType = CommandType.Text.  Otherwise, the return value will be -1.
        /// </returns>
        /// <remarks>
        /// Once the command is executed, the SQL connection will remain open.
        /// <br/>
        /// If the Connection is using a transaction, then the command will be executed on the transaction.
        /// <br/>
        /// The default SqlCommand timeout of 30 seconds will be used.
        /// </remarks>
        /// <example>
        /// <code>
        /// using (SqlConnection connection = new SqlConnection("MyConnectionString"))
        /// {
        ///     ParameterList parameters = new ParameterList();
        ///     parameters.Add("@Parameter1", 1, SqlDbType.BigInt);
        ///     parameters.Add("@Parameter2", 1, SqlDbType.Int);
        ///     parameters.Add("@Parameter3", 1, SqlDbType.SmallInt);
        ///     parameters.Add("@Parameter4", DateTime.Now, SqlDbType.DateTime);
        ///     parameters.Add("@Parameter5", isError, SqlDbType.Bit);
        ///     parameters.Add("@OutputParameter", 0, SqlDbType.Int, ParameterDirection.Output);
        ///     parameters.Add("@InputOutputParameter", 999, SqlDbType.Int, ParameterDirection.InputOutput);
        ///     parameters.Add("@ReturnValue", 0, SqlDbType.Int, ParameterDirection.ReturnValue);
        /// 
        ///     SqlHelper.ExecuteNonQuery(
        ///         connection, "MyStoredProcedureName", CommandType.StoredProcedure, parameters);
        ///    
        ///     Int32 outputValue1 = parameters.Get&lt;Int32&gt;("@OutputParameter");
        ///     Int32 outputValue2 = parameters.Get&lt;Int32&gt;("@InputOutputParameter");
        ///     Int32 returnValue = parameters.Get&lt;Int32&gt;("@ReturnValue");
        ///     
        ///     Int32 numberOfRowsAffected = SqlHelper.ExecuteNonQuery(
        ///         connection, "DELETE FROM MyTable WHERE [Type] = 1", CommandType.Text, null);
        ///
        /// }   // &lt;---- connection closed here
        /// </code>
        /// </example>
        public static Int32 ExecuteNonQuery(SqlConnection connection, string command, CommandType commandType, ParameterList parameters)
        {
            return ExecuteNonQuery(connection, command, commandType, parameters, 0);
        }

        /// <summary>
        /// Executes the specified SQL Command and returns the number of fows that were affected.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string to use for the SQL Command.
        /// </param>
        /// <param name="command">
        /// The SQL command (query or stored procedure name).
        /// </param>
        /// <param name="commandType">
        /// The CommandType of the SQL Command.
        /// </param>
        /// <param name="parameters">
        /// The ParameterList to be used for the SQL command or NULL if there are no parameters.
        /// <br/>
        /// This list must be built using the Add(string, object, SqlDbType) or 
        /// Add(string, object, SqlDbType, SqlParameterDirection) overloads of ParameterList, 
        /// so that SQL data types and parameter directions are defined.
        /// </param>
        /// <param name="commandTimeoutInSeconds">
        /// The number of seconds before the SQL command will timeout.  
        /// If this value is less than or equal to 0, the default SqlCommand timeout of 30 seconds will be used.
        /// </param>
        /// <returns>
        /// The number of rows affected by the command.  Note - this return value is only valid for 
        /// CommandType = CommandType.Text.  Otherwise, the return value will be -1.
        /// </returns>
        /// <remarks>
        /// The command will be executed on its own connection (no transaction support).
        /// </remarks>
        /// <example>
        /// <code>
        /// ParameterList parameters = new ParameterList();
        /// parameters.Add("@Parameter1", 1, SqlDbType.BigInt);
        /// parameters.Add("@Parameter2", 1, SqlDbType.Int);
        /// parameters.Add("@Parameter3", 1, SqlDbType.SmallInt);
        /// parameters.Add("@Parameter4", DateTime.Now, SqlDbType.DateTime);
        /// parameters.Add("@Parameter5", isError, SqlDbType.Bit);
        /// parameters.Add("@OutputParameter", 0, SqlDbType.Int, ParameterDirection.Output);
        /// parameters.Add("@InputOutputParameter", 999, SqlDbType.Int, ParameterDirection.InputOutput);
        /// parameters.Add("@ReturnValue", 0, SqlDbType.Int, ParameterDirection.ReturnValue);
        /// 
        /// Int32 timeoutSeconds = 300;  // 5 minutes
        /// 
        /// SqlHelper.ExecuteNonQuery(
        ///     "MyConnectionString", "MyStoredProcedureName", CommandType.StoredProcedure, parameters, timeoutSeconds);
        ///    
        /// Int32 outputValue1 = parameters.Get&lt;Int32&gt;("@OutputParameter");
        /// Int32 outputValue2 = parameters.Get&lt;Int32&gt;("@InputOutputParameter");
        /// Int32 returnValue = parameters.Get&lt;Int32&gt;("@ReturnValue");
        ///     
        /// Int32 numberOfRowsAffected = SqlHelper.ExecuteNonQuery(
        ///     "MyConnectionString", "DELETE FROM MyTable WHERE [Type] = 1", CommandType.Text, null, timeoutSeconds);
        /// </code>
        /// </example>
        public static Int32 ExecuteNonQuery(string connectionString, string command, CommandType commandType, ParameterList parameters, Int32 commandTimeoutInSeconds)
        {


            Int32 rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                rowsAffected = ExecuteNonQuery(connection, command, commandType, parameters, commandTimeoutInSeconds);

                connection.Close();
            }

            if (commandType == CommandType.Text)
                return rowsAffected;
            else
                return -1;
        }

        /// <summary>
        /// Executes the specified SQL Command and returns the number of fows that were affected.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string to use for the SQL Command.
        /// </param>
        /// <param name="command">
        /// The SQL command (query or stored procedure name).
        /// </param>
        /// <param name="commandType">
        /// The CommandType of the SQL Command.
        /// </param>
        /// <param name="parameters">
        /// The ParameterList to be used for the SQL command or NULL if there are no parameters.
        /// <br/>
        /// This list must be built using the Add(string, object, SqlDbType) or 
        /// Add(string, object, SqlDbType, SqlParameterDirection) overloads of ParameterList, 
        /// so that SQL data types and parameter directions are defined.
        /// </param>
        /// <returns>
        /// The number of rows affected by the command.  Note - this return value is only valid for 
        /// CommandType = CommandType.Text.  Otherwise, the return value will be -1.
        /// </returns>
        /// <remarks>
        /// The command will be executed on its own connection (no transaction support).
        /// <br/>
        /// The default SqlCommand timeout of 30 seconds will be used.
        /// </remarks>
        /// <example>
        /// <code>
        /// ParameterList parameters = new ParameterList();
        /// parameters.Add("@Parameter1", 1, SqlDbType.BigInt);
        /// parameters.Add("@Parameter2", 1, SqlDbType.Int);
        /// parameters.Add("@Parameter3", 1, SqlDbType.SmallInt);
        /// parameters.Add("@Parameter4", DateTime.Now, SqlDbType.DateTime);
        /// parameters.Add("@Parameter5", isError, SqlDbType.Bit);
        /// parameters.Add("@OutputParameter", 0, SqlDbType.Int, ParameterDirection.Output);
        /// parameters.Add("@InputOutputParameter", 999, SqlDbType.Int, ParameterDirection.InputOutput);
        /// parameters.Add("@ReturnValue", 0, SqlDbType.Int, ParameterDirection.ReturnValue);
        /// 
        /// SqlHelper.ExecuteNonQuery(
        ///     "MyConnectionString", "MyStoredProcedureName", CommandType.StoredProcedure, parameters);
        ///    
        /// Int32 outputValue1 = parameters.Get&lt;Int32&gt;("@OutputParameter");
        /// Int32 outputValue2 = parameters.Get&lt;Int32&gt;("@InputOutputParameter");
        /// Int32 returnValue = parameters.Get&lt;Int32&gt;("@ReturnValue");
        ///     
        /// Int32 numberOfRowsAffected = SqlHelper.ExecuteNonQuery(
        ///     "MyConnectionString", "DELETE FROM MyTable WHERE [Type] = 1", CommandType.Text, null);
        /// </code>
        /// </example>
        public static Int32 ExecuteNonQuery(string connectionString, string command, CommandType commandType, ParameterList parameters)
        {
            return ExecuteNonQuery(connectionString, command, commandType, parameters, 0);
        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// Executes the specified SQL Command and returns the first column of the first row in the 
        /// result set returned by the query. Additional columns or rows are ignored.
        /// </summary>
        /// <typeparam name="T">
        /// The type to which the value for the scalar value will be converted.
        /// </typeparam>
        /// <param name="connection">
        /// The SqlConnection to use for the SQL Command.  This must already be open.
        /// </param>
        /// <param name="command">
        /// The SQL command (query or stored procedure name).
        /// </param>
        /// <param name="commandType">
        /// The CommandType of the SQL Command.
        /// </param>
        /// <param name="parameters">
        /// The ParameterList to be used for the SQL command or NULL if there are no parameters.
        /// <br/>
        /// This list must be built using the Add(string, object, SqlDbType) or 
        /// Add(string, object, SqlDbType, SqlParameterDirection) overloads of ParameterList, 
        /// so that SQL data types and parameter directions are defined.
        /// </param>
        /// <returns>
        /// The number of rows affected by the command.  Note - this return value is only valid for 
        /// CommandType = CommandType.Text.  Otherwise, the return value will be -1.
        /// </returns>
        /// <remarks>
        /// Once the command is executed, the SQL connection will remain open.
        /// <br/>
        /// If the Connection is using a transaction, then the command will be executed on the transaction.
        /// <br/>
        /// The default SqlCommand timeout of 30 seconds will be used.
        /// </remarks>
        /// <example>
        /// <code>
        /// using (SqlConnection connection = new SqlConnection("MyConnectionString"))
        /// {
        ///     ParameterList parameters = new ParameterList();
        ///     parameters.Add("@Parameter1", 1, SqlDbType.BigInt);
        ///     parameters.Add("@Parameter2", 1, SqlDbType.Int);
        ///     parameters.Add("@Parameter3", 1, SqlDbType.SmallInt);
        ///     parameters.Add("@Parameter4", DateTime.Now, SqlDbType.DateTime);
        ///     parameters.Add("@Parameter5", isError, SqlDbType.Bit);
        ///     parameters.Add("@OutputParameter", 0, SqlDbType.Int, ParameterDirection.Output);
        ///     parameters.Add("@InputOutputParameter", 999, SqlDbType.Int, ParameterDirection.InputOutput);
        ///     parameters.Add("@ReturnValue", 0, SqlDbType.Int, ParameterDirection.ReturnValue);
        /// 
        ///     Int32 timeoutSeconds = 300;  // 5 minutes
        /// 
        ///     Int32 returnValue = SqlHelper.ExecuteScalar&lt;Int32&gt;(
        ///         _ProgramConnectionString, "MyStoredProcedureName", CommandType.StoredProcedure, parameters);
        ///    
        ///     Int32 outputValue1 = parameters.Get&lt;Int32&gt;("@OutputParameter");
        ///     Int32 outputValue2 = parameters.Get&lt;Int32&gt;("@InputOutputParameter");
        ///     Int32 returnValue = parameters.Get&lt;Int32&gt;("@ReturnValue");
        ///
        /// }   // &lt;---- connection closed here
        /// </code>
        /// </example>
        public static T ExecuteScalar<T>(SqlConnection connection, string command, CommandType commandType, ParameterList parameters)
        {


            SqlParameter[] sqlParameterList = GetSqlParameterList(parameters);
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;
            sqlCommand.CommandText = command;
            sqlCommand.CommandType = commandType;
            //sqlCommand.Transaction = GetCurrentTransaction(connection);

            sqlCommand.Parameters.AddRange(sqlParameterList);

            T returnValue = default(T);
            object value = sqlCommand.ExecuteScalar();

            if (value != null)
            {
                returnValue = (T)sqlCommand.ExecuteScalar();
            }

            UpdateReturnedParameters(sqlParameterList, parameters);

            return returnValue;
        }

        /// <summary>
        /// Executes the specified SQL Command and returns the first column of the first row in the 
        /// result set returned by the query. Additional columns or rows are ignored.
        /// </summary>
        /// <typeparam name="T">
        /// The type to which the value for the scalar value will be converted.
        /// </typeparam>
        /// <param name="connectionString">
        /// The connection string to use for the SQL Command.
        /// </param>
        /// <param name="command">
        /// The SQL command (query or stored procedure name).
        /// </param>
        /// <param name="commandType">
        /// The CommandType of the SQL Command.
        /// </param>
        /// <param name="parameters">
        /// The ParameterList to be used for the SQL command or NULL if there are no parameters.
        /// <br/>
        /// This list must be built using the Add(string, object, SqlDbType) or 
        /// Add(string, object, SqlDbType, SqlParameterDirection) overloads of ParameterList, 
        /// so that SQL data types and parameter directions are defined.
        /// </param>
        /// <returns>
        /// The number of rows affected by the command.  Note - this return value is only valid for 
        /// CommandType = CommandType.Text.  Otherwise, the return value will be -1.
        /// </returns>
        /// <remarks>
        /// The command will be executed on its own connection (no transaction support).
        /// <br/>
        /// The default SqlCommand timeout of 30 seconds will be used.
        /// </remarks>
        /// <example>
        /// <code>
        /// ParameterList parameters = new ParameterList();
        /// parameters.Add("@Parameter1", 1, SqlDbType.BigInt);
        /// parameters.Add("@Parameter2", 1, SqlDbType.Int);
        /// parameters.Add("@Parameter3", 1, SqlDbType.SmallInt);
        /// parameters.Add("@Parameter4", DateTime.Now, SqlDbType.DateTime);
        /// parameters.Add("@Parameter5", isError, SqlDbType.Bit);
        /// parameters.Add("@OutputParameter", 0, SqlDbType.Int, ParameterDirection.Output);
        /// parameters.Add("@InputOutputParameter", 999, SqlDbType.Int, ParameterDirection.InputOutput);
        /// parameters.Add("@ReturnValue", 0, SqlDbType.Int, ParameterDirection.ReturnValue);
        /// 
        /// Int32 timeoutSeconds = 300;  // 5 minutes
        /// 
        /// Int32 returnValue = SqlHelper.ExecuteScalar&lt;Int32&gt;(
        ///    "MyConnectionString", "MyStoredProcedureName", CommandType.StoredProcedure, parameters);
        ///    
        /// Int32 outputValue1 = parameters.Get&lt;Int32&gt;("@OutputParameter");
        /// Int32 outputValue2 = parameters.Get&lt;Int32&gt;("@InputOutputParameter");
        /// Int32 returnValue = parameters.Get&lt;Int32&gt;("@ReturnValue");
        /// </code>
        /// </example>
        public static T ExecuteScalar<T>(string connectionString, string command, CommandType commandType, ParameterList parameters)
        {


            SqlConnection connection = null;
            T returnValue = default(T);

            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                returnValue = ExecuteScalar<T>(connection, command, commandType, parameters);
                connection.Close();
            }

            return returnValue;
        }

        #endregion

        #region ExecuteReader

        private static IDataReader ExecuteReader(SqlConnection connection, string command, CommandType commandType, ParameterList parameters, bool AutoCloseConnectionOnReaderDispose, Int32 commandTimeoutInSeconds)
        {
            SqlDataReader reader = null;

            //*************************************************************************
            //
            //  For debugging only (comment out after using).
            //
            //StringBuilder infoMessages = new StringBuilder();
            //DateTime startTime = DateTime.Now;

            //connection.InfoMessage += delegate(object sender, SqlInfoMessageEventArgs e)
            //{
            //    infoMessages.AppendLine(e.Message);
            //};
            //*************************************************************************

            SqlParameter[] sqlParameterList = GetSqlParameterList(parameters);
            SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = command;
            sqlCommand.CommandType = commandType;
            sqlCommand.Parameters.AddRange(sqlParameterList);
            // sqlCommand.Transaction = GetCurrentTransaction(connection);

            if (commandTimeoutInSeconds != -1)
                sqlCommand.CommandTimeout = commandTimeoutInSeconds;

            if (AutoCloseConnectionOnReaderDispose == true)
                reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            else
                reader = sqlCommand.ExecuteReader();

            //*************************************************************************
            //
            //  For debugging only (comment out after using).
            //
            //DateTime endTime = DateTime.Now;
            //TimeSpan duration = endTime.Subtract(startTime);

            //if (duration.Seconds > 5)
            //{
            //    Thread.Sleep(2000);
            //    infoMessages.AppendLine(string.Format("The '{0}' query took {1} seconds - examine infoMessages", command, duration.Seconds));
            //}
            //*************************************************************************

            UpdateReturnedParameters(sqlParameterList, parameters);

            return reader;
        }

        /// <summary>
        /// Executes the specified SQL Command and returns a SqlDataReader.
        /// </summary>
        /// <param name="connection">
        /// The SqlConnection to use for the SQL Command.  This must already be open.
        /// </param>
        /// <param name="command">
        /// The SQL command (query or stored procedure name).
        /// </param>
        /// <param name="commandType">
        /// The CommandType of the SQL Command.
        /// </param>
        /// <param name="parameters">
        /// The ParameterList to be used for the SQL command or NULL if there are no parameters.
        /// <br/>
        /// This list must be built using the Add(string, object, SqlDbType) or 
        /// Add(string, object, SqlDbType, SqlParameterDirection) overloads of ParameterList, 
        /// so that SQL data types and parameter directions are defined.
        /// </param>
        /// <returns>
        /// A SqlDataReader that the caller can user to retrieve results of the query.
        /// <br/>
        /// The default SqlCommand timeout of 30 seconds will be used.
        /// </returns>
        /// <remarks>
        /// When the returned SqlDataReader is disposed, the SQL connection will remain open.
        /// <br/>
        /// The reader returned will be on the specified Connection).
        /// <br/>
        /// If the Connection is using a transaction, then the reader will be executed on the transaction.
        /// </remarks>
        /// <example>
        /// <code>
        /// using (SqlConnection connection = new SqlConnection("some connection string"))
        /// {
        ///     ParameterList parameters = new ParameterList();
        ///     parameters.Add("@Parameter1", 1, SqlDbType.BigInt);
        ///     parameters.Add("@Parameter2", 1, SqlDbType.Int);
        ///     parameters.Add("@Parameter3", 1, SqlDbType.SmallInt);
        ///     parameters.Add("@Parameter4", DateTime.Now, SqlDbType.DateTime);
        ///     parameters.Add("@Parameter5", isError, SqlDbType.Bit);
        ///     parameters.Add("@OutputParameter", 0, SqlDbType.Int, ParameterDirection.Output);
        ///     parameters.Add("@InputOutputParameter", 999, SqlDbType.Int, ParameterDirection.InputOutput);
        ///     parameters.Add("@ReturnValue", 0, SqlDbType.Int, ParameterDirection.ReturnValue);
        /// 
        ///     using (SqlDataReader reader = SqlHelper.ExecuteReader(
        ///         _ProgramConnectionString, "MyStoredProcedureName", CommandType.StoredProcedure, parameters))
        ///     {
        ///    
        ///         Int32 outputValue1 = parameters.Get&lt;Int32&gt;("@OutputParameter");
        ///         Int32 outputValue2 = parameters.Get&lt;Int32&gt;("@InputOutputParameter");
        ///         Int32 returnValue = parameters.Get&lt;Int32&gt;("@ReturnValue");
        /// 
        ///         //
        ///         //  Read from the reader.  Connection open at end of reader "using" block.
        ///         //
        ///     }
        ///         
        ///     //
        ///     //  Do something else with the connection.
        ///     //
        ///     
        /// }   // &lt;---- connection closed here
        /// </code>
        /// </example>
        public static IDataReader ExecuteReader(SqlConnection connection, string command, CommandType commandType, ParameterList parameters)
        {


            return ExecuteReader(connection, command, commandType, parameters, false, -1);
        }

        /// <summary>
        /// Executes the specified SQL Command and returns a SqlDataReader.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string to use for the SQL Command.
        /// </param>
        /// <param name="command">
        /// The SQL command (query or stored procedure name).
        /// </param>
        /// <param name="commandType">
        /// The CommandType of the SQL Command.
        /// </param>
        /// <param name="parameters">
        /// The ParameterList to be used for the SQL command or NULL if there are no parameters.
        /// <br/>
        /// This list must be built using the Add(string, object, SqlDbType) or 
        /// Add(string, object, SqlDbType, SqlParameterDirection) overloads of ParameterList, 
        /// so that SQL data types and parameter directions are defined.
        /// </param>
        /// <returns>
        /// A SqlDataReader that the caller can user to retrieve results of the query.
        /// </returns>
        /// <remarks>
        /// When the returned SqlDataReader is disposed, the SQL connection will be closed.
        /// <br/>
        /// The reader returned will be on its own connection (no transaction support).
        /// <br/>
        /// The default SqlCommand timeout of 30 seconds will be used.
        /// </remarks>
        /// <example>
        /// <code>
        /// ParameterList parameters = new ParameterList();
        /// parameters.Add("@Parameter1", 1, SqlDbType.BigInt);
        /// parameters.Add("@Parameter2", 1, SqlDbType.Int);
        /// parameters.Add("@Parameter3", 1, SqlDbType.SmallInt);
        /// parameters.Add("@Parameter4", DateTime.Now, SqlDbType.DateTime);
        /// parameters.Add("@Parameter5", isError, SqlDbType.Bit);
        /// parameters.Add("@OutputParameter", 0, SqlDbType.Int, ParameterDirection.Output);
        /// parameters.Add("@InputOutputParameter", 999, SqlDbType.Int, ParameterDirection.InputOutput);
        /// parameters.Add("@ReturnValue", 0, SqlDbType.Int, ParameterDirection.ReturnValue);
        /// 
        /// using (SqlDataReader reader = SqlHelper.ExecuteReader(
        ///     "MyConnectionString", "MyStoredProcedureName", CommandType.StoredProcedure, parameters))
        /// {
        ///     Int32 outputValue1 = parameters.Get&lt;Int32&gt;("@OutputParameter");
        ///     Int32 outputValue2 = parameters.Get&lt;Int32&gt;("@InputOutputParameter");
        ///     Int32 returnValue = parameters.Get&lt;Int32&gt;("@ReturnValue");
        ///     
        ///     //
        ///     //  Read from the reader.  Connection closed at end of reader "using" block.
        ///     //
        ///     
        /// }   &lt;---- connection closed here
        /// </code>
        /// </example>
        public static IDataReader ExecuteReader(string connectionString, string command, CommandType commandType, ParameterList parameters)
        {


            return ExecuteReader(connectionString, command, commandType, parameters, -1);
        }

        /// <summary>
        /// Executes the specified SQL Command and returns a SqlDataReader.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string to use for the SQL Command.
        /// </param>
        /// <param name="command">
        /// The SQL command (query or stored procedure name).
        /// </param>
        /// <param name="commandType">
        /// The CommandType of the SQL Command.
        /// </param>
        /// <param name="parameters">
        /// The ParameterList to be used for the SQL command or NULL if there are no parameters.
        /// <br/>
        /// This list must be built using the Add(string, object, SqlDbType) or 
        /// Add(string, object, SqlDbType, SqlParameterDirection) overloads of ParameterList, 
        /// so that SQL data types and parameter directions are defined.
        /// </param>
        /// <param name="commandTimeoutInSeconds">
        /// The number of seconds to wait for a command to process.
        /// If 0, there is no timeout (indefinite wait).
        /// If -1, the default SQL command timeout of 30 seconds will be used.
        /// </param>
        /// <returns>
        /// A SqlDataReader that the caller can user to retrieve results of the query.
        /// </returns>
        /// <remarks>
        /// When the returned SqlDataReader is disposed, the SQL connection will be closed.
        /// <br/>
        /// The reader returned will be on its own connection (no transaction support).
        /// </remarks>
        /// <example>
        /// <code>
        /// ParameterList parameters = new ParameterList();
        /// parameters.Add("@Parameter1", 1, SqlDbType.BigInt);
        /// parameters.Add("@Parameter2", 1, SqlDbType.Int);
        /// parameters.Add("@Parameter3", 1, SqlDbType.SmallInt);
        /// parameters.Add("@Parameter4", DateTime.Now, SqlDbType.DateTime);
        /// parameters.Add("@Parameter5", isError, SqlDbType.Bit);
        /// parameters.Add("@OutputParameter", 0, SqlDbType.Int, ParameterDirection.Output);
        /// parameters.Add("@InputOutputParameter", 999, SqlDbType.Int, ParameterDirection.InputOutput);
        /// parameters.Add("@ReturnValue", 0, SqlDbType.Int, ParameterDirection.ReturnValue);
        /// 
        /// Int32 commandTimeoutInSeconds = 300;  // 5 minutes
        /// 
        /// using (SqlDataReader reader = SqlHelper.ExecuteReader(
        ///     "MyConnectionString", "MyStoredProcedureName", CommandType.StoredProcedure, parameters, commandTimeoutInSeconds))
        /// {
        ///     Int32 outputValue1 = parameters.Get&lt;Int32&gt;("@OutputParameter");
        ///     Int32 outputValue2 = parameters.Get&lt;Int32&gt;("@InputOutputParameter");
        ///     Int32 returnValue = parameters.Get&lt;Int32&gt;("@ReturnValue");
        ///     
        ///     //
        ///     //  Read from the reader.  Connection closed at end of reader "using" block.
        ///     //
        ///     
        /// }   &lt;---- connection closed here
        /// </code>
        /// </example>
        public static IDataReader ExecuteReader(string connectionString, string command, CommandType commandType, ParameterList parameters, Int32 commandTimeoutInSeconds)
        {


            SqlConnection connection = null;
            IDataReader reader = null;

            //
            //  Cannot use "using" here because we want the connection to
            //  stay open in the returned reader (but we will close it in "catch"
            //  if something else goes wrong).
            //
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                reader = ExecuteReader(connection, command, commandType, parameters, true, commandTimeoutInSeconds);
            }
            catch
            {
                if ((connection != null) && (connection.State == ConnectionState.Open))
                    connection.Close();

                reader = null;

                throw;
            }

            return reader;
        }

        #endregion

        #region GetCurrentTransaction



        #endregion

        #region GetSqlDbType

        private static SqlDbType GetSqlDbType(string sqlDbType)
        {
            if (!String.IsNullOrEmpty(sqlDbType))
            {
                try
                {
                    string hintText = sqlDbType.Substring(sqlDbType.LastIndexOf(".") + 1);

                    SqlDbType sdt = (SqlDbType)System.Enum.Parse(typeof(SqlDbType), hintText, true);
                    return sdt;
                }
                catch (ArgumentException)
                {
                    throw new Exception(String.Format("{0} is an unknown SqlDbType.", sqlDbType));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            throw new Exception("Provided value for SqlDbType was null or empty.");
        }

        #endregion

        #region GetSqlParameterList

        /// <summary>
        /// Gets a list of SqlParameter objects initialized so that SqlParameter.ParameterName=Key,
        /// SqlParameter.Value=Value, SqlParameter.SqlDbType=SqlDbType, and 
        /// SqlParameter.Direction=SqlParameterDirection for each item in the specified Parameters list
        /// </summary>
        /// <param name="parameters">
        /// The ParameterList to be used for the SQL command or NULL if there are no parameters.
        /// <br/>
        /// This list must be built using the Add(string, object, SqlDbType) or 
        /// Add(string, object, SqlDbType, SqlParameterDirection) overloads of ParameterList, 
        /// so that SQL data types and parameter directions are defined.
        /// </param>
        /// <returns>
        /// A string list of all values.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// The parameter's SQL information is not set.
        /// Use the ParameterList.Add() overload to set SQL parameter information when creating the Parameters list.
        /// </exception>
        /// <example>
        /// <code>
        /// ParameterList parameters = new ParameterList();
        /// parameters.Add("@Parameter1", 1, SqlDbType.BigInt);
        /// parameters.Add("@Parameter2", 1, SqlDbType.Int);
        /// parameters.Add("@Parameter3", 1, SqlDbType.SmallInt);
        /// parameters.Add("@Parameter4", DateTime.Now, SqlDbType.DateTime);
        /// parameters.Add("@Parameter5", isError, SqlDbType.Bit);
        /// parameters.Add("@OutputParameter", 0, SqlDbType.Int, ParameterDirection.Output);
        /// parameters.Add("@InputOutputParameter", 999, SqlDbType.Int, ParameterDirection.InputOutput);
        /// parameters.Add("@ReturnValue", 0, SqlDbType.Int, ParameterDirection.ReturnValue);
        /// 
        /// SqlParameter[] sqlParameters = SqlHelper.GetSqlParameterList(parameters);
        /// do something with sqlParameters...
        /// </code>
        /// </example>
        public static SqlParameter[] GetSqlParameterList(ParameterList parameters)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();

            if (parameters != null)
            {
                foreach (ParameterItem item in parameters.Items)
                {
                    if (item._SqlParameterInfoSet == false)
                        throw new ArgumentException(
                            "The parameter's SQL information is not set. " +
                            "Use the ParameterList.Add() overload to set SQL parameter information when creating the Parameters list.");

                    SqlParameter p = new SqlParameter();
                    p.Direction = item.SqlParameterDirection;
                    p.ParameterName = item.Key;
                    p.SqlDbType = item.SqlDbType;

                    if (p.Direction == ParameterDirection.Output)
                    {
                        switch (p.SqlDbType)
                        {
                            case SqlDbType.NChar:
                            case SqlDbType.NText:
                            case SqlDbType.NVarChar:
                            case SqlDbType.Text:
                            case SqlDbType.VarChar:
                            case SqlDbType.VarBinary:
                                //case SqlDbType.Xml:     // ignored
                                p.Size = Int32.MaxValue;
                                break;
                        }
                    }

                    p.Value = ConvertValueIn(item.SqlDbType, item.Value);

                    sqlParameterList.Add(p);
                }
            }

            return sqlParameterList.ToArray();
        }

        #endregion

        #region GetStoredProcedureParameterList

        /// <summary>
        /// Gets a list of SqlParameter objects that represent all parameters to the specified storedProcedureName.
        /// </summary>
        /// <param name="connectionString">
        /// A connection string to the database where the storedProcedureName exists.
        /// </param>
        /// <param name="storedProcedureName">
        /// The name of the stored procedure for which parameters will be queried.
        /// </param>
        /// <param name="sqlParameterList">
        /// This <i>output</i> parameter returns a list of SqlParameter objects, one for each parameter to the stored procedure.
        /// </param>
        /// <returns>
        /// An ErrorStatus object that contains call success/failure/warning information.
        /// </returns>
        public static ErrorCode GetStoredProcedureParameterList(string connectionString, string storedProcedureName, out List<SqlParameter> sqlParameterList)
        {
            ErrorCode status = new ErrorCode();
            SqlCommand command = null;
            SqlConnection connection = null;
            SqlDataReader reader = null;
            string sql = string.Empty;

            sqlParameterList = new List<SqlParameter>();

            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();

                //
                //  Verify that the stored procedure exists.
                //
                sql =
                    "SELECT COUNT(*) " +
                    "FROM sys.procedures sp " +
                    "WHERE sp.name = '" + storedProcedureName + "'";

                command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                int count = (int)command.ExecuteScalar();

                if (count != 1)
                    throw new Exception("Stored procedure '" + storedProcedureName + "' could not be found.");

                //
                //  Get the parameter list.
                //
                sql = string.Format(
                    @" SELECT 
                    parm.name AS [Name] 
                    , typ.name AS [Type] 
                    , typ.max_length AS [Size] 
                    , typ.[precision] AS [Precision] 
                    , typ.scale AS [Scale] 
                    , parm.is_output AS [IsOutput] 
                    FROM sys.procedures sp 
                    JOIN sys.parameters parm ON sp.object_id = parm.object_id 
                    JOIN sys.types typ ON parm.system_type_id = typ.system_type_id 
                    WHERE sp.name = '{0}' 
                    ORDER BY parm.parameter_id", storedProcedureName);

                command = new SqlCommand(sql, connection);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SqlDataReaderHelper helper = new SqlDataReaderHelper(reader);

                    string name = reader.GetString(0);
                    Int32 size = reader.GetInt16(2);    // this is not a bug - 16bit in DB, 32 bit in SqlParameter
                    byte precision = reader.GetByte(3);
                    byte scale = reader.GetByte(4);

                    bool isOutput = helper.GetBoolean("IsOutput");

                    string type = helper.GetString("Type");
                    SqlDbType sqlDbType = GetSqlDbType(type);

                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.ParameterName = helper.GetString("Name");
                    sqlParameter.SqlDbType = sqlDbType;
                    sqlParameter.Direction = (isOutput) ? ParameterDirection.InputOutput : ParameterDirection.Input;
                    sqlParameter.Size = helper.GetInt16("Size");    // this is not a bug - 16bit in DB, 32 bit in SqlParameter
                    sqlParameter.Precision = helper.GetByte("Precision");
                    sqlParameter.Scale = helper.GetByte("Scale");
                    sqlParameter.IsNullable = true;
                    sqlParameter.Value = DBNull.Value;

                    sqlParameterList.Add(sqlParameter);
                }

                connection.Close();
                connection.Dispose();
            }

            return status;
        }

        #endregion

        #region GetStoredProcedureParameterListWithDefaultValues

        /// <summary>
        /// Gets a list of SqlParameter objects that represent all parameters to the specified storedProcedureName.
        /// Each returned list item's SqlParameter.Value property will be populated with the default value (if any) specified in the stored procedure.
        /// If a parameter does not have a default value, the list item SqlParameter.Value property will be NULL.
        /// </summary>
        /// <param name="connectionString">
        /// A connection string to the database where the storedProcedureName exists.
        /// </param>
        /// <param name="storedProcedureName">
        /// The name of the stored procedure for which parameters will be queried.
        /// </param>
        /// <param name="sqlParameterList">
        /// This <i>output</i> parameter returns a list of SqlParameter objects, one for each parameter to the stored procedure.
        /// Each returned list item's SqlParameter.Value property will be populated with the default value (if any) specified in the stored procedure.
        /// If a parameter does not have a default value, the list item SqlParameter.Value property will be NULL.
        /// </param>
        /// <returns>
        /// An ErrorStatus object that contains call success/failure/warning information.
        /// </returns>
        public static ErrorCode GetStoredProcedureParameterListWithDefaultValues(string connectionString, string storedProcedureName, out List<SqlParameter> sqlParameterList)
        {
            ErrorCode status = new ErrorCode();
            SqlCommand command = null;
            SqlConnection connection = null;
            string sql = string.Empty;
            string procedureText = string.Empty;

            sqlParameterList = new List<SqlParameter>();

            status = GetStoredProcedureParameterList(connectionString, storedProcedureName, out sqlParameterList);

            //if (status.Success != true)
            //{
            //    return status;
            //}

            try
            {
                using (connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    //
                    //  Get comments:
                    //      o   This was the only way I could find to get the default value
                    //          of stored procedure parameters.
                    //
                    sql =
                        "SELECT [text] " +
                        "FROM syscomments " +
                        "WHERE id = object_id('" + storedProcedureName + "')";

                    command = new SqlCommand(sql, connection);
                    command.CommandType = CommandType.Text;
                    procedureText = (string)command.ExecuteScalar();

                    connection.Close();
                    connection.Dispose();
                }

                //
                //  Get default values from the procedure text.
                //      o   This seems like a hack but there is no other way to do it.
                //      o   We want to get procedureText to look like this so we
                //          can reliably search for default values:
                //              @Parm1TYPE=defaultvalue,@Parm2TYPE,@Parm3TYPE=defaultvalue...,
                //      o   The last character must be ',' for the split()
                //      o   Any amount of white space is optional
                //      o   Extra ',' characters are OK (filtered out by the split()
                //
                if (sqlParameterList.Count > 0)
                {
                    SqlParameter lastSqlParameter = sqlParameterList[sqlParameterList.Count - 1];

                    //
                    //  Make sure all white space is spaces (no tabs)
                    //
                    procedureText = procedureText.Replace("\t", " ");           // convert tabs to spaces

                    //
                    //  Skip everything up to the end of the stored procedure name
                    //
                    int beginParameterListIndex = procedureText.IndexOf(storedProcedureName, 0, StringComparison.OrdinalIgnoreCase);

                    if (beginParameterListIndex < 0)
                        throw new Exception(string.Format("Unable to find stored procedure name '{0}'.", storedProcedureName));

                    beginParameterListIndex += storedProcedureName.Length;

                    while (procedureText[beginParameterListIndex] != '(')
                        ++beginParameterListIndex;

                    ++beginParameterListIndex;

                    //
                    //  Strip off everything up to the the end of last parameter:
                    //      o   Include the space to avoid finding another parameter
                    //          that is not the last.....like the following:
                    //              @LastParameterNotReally int
                    //              @LastParameter int
                    //
                    int endParameterListIndex = procedureText.IndexOf(lastSqlParameter.ParameterName + " ", beginParameterListIndex, StringComparison.OrdinalIgnoreCase);

                    if (endParameterListIndex < 0)
                        throw new Exception(string.Format("Unable to find last parameter '{0}'.", lastSqlParameter.ParameterName));

                    endParameterListIndex += lastSqlParameter.ParameterName.Length + 1;

                    //
                    //  We need to find the AS that marks the beginning of procedure code
                    //      o   We need to find the closing first "AS" after the last parameter
                    //      o   The characers "AS" can be in the parameter declaration
                    //      o   The default value of the last parameter may also contain 'AS'
                    //      o   The default value of the last parameter may also contain single quote characters
                    //      o   This strange code handles these scenarios
                    //

                    // Skip white space
                    while (char.IsWhiteSpace(procedureText[endParameterListIndex]))
                        endParameterListIndex += 1;

                    //  Skip the first optional "AS" before the data type
                    if (String.Compare(procedureText.Substring(endParameterListIndex, 2), "AS", true) == 0)
                        endParameterListIndex += 2;

                    //  Walk the string until we find the beginning of code "AS"
                    bool beginCodeFound = false;
                    bool blockCommentOpen = false;
                    bool lineCommentOpen = false;
                    int openQuoteCount = 0;
                    StringBuilder pureCode = new StringBuilder(procedureText.Substring(beginParameterListIndex, (endParameterListIndex - beginParameterListIndex)));

                    do
                    {
                        string current = procedureText.Substring(endParameterListIndex);

                        // Skip CR/LF
                        if (string.Compare(current.Substring(0, 2), "\r\n") == 0)
                        {
                            endParameterListIndex += 2;
                            lineCommentOpen = false;
                            continue;
                        }

                        // Detect begin single line comment
                        if (string.Compare(current.Substring(0, 2), "--") == 0)
                        {
                            lineCommentOpen = true;
                            endParameterListIndex += 2;
                            continue;
                        }

                        // Skip until end of single line comment
                        if (lineCommentOpen == true)
                        {
                            endParameterListIndex += 1;
                            continue;
                        }

                        // Detect begin block comment
                        if ((blockCommentOpen == false) && (string.Compare(current.Substring(0, 2), "/*") == 0))
                        {
                            blockCommentOpen = true;
                            endParameterListIndex += 2;
                            continue;
                        }

                        // Detect end block comment
                        if ((blockCommentOpen == true) && (string.Compare(current.Substring(0, 2), "*/") == 0))
                        {
                            blockCommentOpen = false;
                            endParameterListIndex += 2;
                            continue;
                        }

                        // Skip until end of block comment
                        if (blockCommentOpen == true)
                        {
                            endParameterListIndex += 1;
                            continue;
                        }

                        //  Keep track of open/close quotes (including nested quotes)
                        if (procedureText[endParameterListIndex] == '\'')
                        {
                            if (openQuoteCount > 0)
                                openQuoteCount -= 1;
                            else
                                openQuoteCount += 1;

                            pureCode.Append(procedureText[endParameterListIndex]);
                            endParameterListIndex += 1;
                            continue;
                        }

                        //  Skip all quoted content
                        if (openQuoteCount > 0)
                        {
                            pureCode.Append(procedureText[endParameterListIndex]);
                            endParameterListIndex += 1;
                            continue;
                        }

                        //  If we find "AS" we are done, otherwise keep searching
                        if (string.Compare(procedureText.Substring(endParameterListIndex, 2), "AS", true) == 0)
                            beginCodeFound = true;
                        else
                        {
                            pureCode.Append(procedureText[endParameterListIndex]);
                            endParameterListIndex += 1;
                        }
                    }
                    while (beginCodeFound == false);

                    //procedureText = procedureText.Substring(0, index) + ",";    // add "," for split()
                    pureCode = pureCode.Replace(" AS", "");           // get rid of " AS" in parameter declarations
                    pureCode = pureCode.Replace(" VARYING", "");      // get rid of " VARYING" in parameter declarations
                    pureCode = pureCode.Replace(" OUTPUT", "");       // get rid of " OUTPUT" in parameter declarations
                    pureCode = pureCode.Replace("\r\n", "");          // get rid of \r\n

                    //
                    //  Split() and extract default values if they exist.
                    //
                    string[] stringParameters = pureCode.ToString().Split(',');

                    foreach (SqlParameter sqlParameter in sqlParameterList)
                    {
                        foreach (string p in stringParameters)
                        {
                            string stringParameter = p.Trim();

                            //
                            //  If we have a default value for this parameter, extract the value
                            //      o   Include the space to avoid finding another parameter
                            //          that is not the right one.....like the following:
                            //              @Parameter123 int
                            //              @Parameter int
                            //
                            if (string.Compare(sqlParameter.ParameterName + " ", 0, stringParameter, 0, sqlParameter.ParameterName.Length + 1, true) == 0)
                            {
                                int valueIndex = stringParameter.IndexOf('=');

                                if (valueIndex > 0)
                                {
                                    string stringDefaultValue = stringParameter.Substring(valueIndex + 1).Replace("(", string.Empty).Replace(")", string.Empty).Trim();

                                    //  All parameters have already been defaulted to null.
                                    if (string.Compare(stringDefaultValue, "null", true) != 0)
                                    {
                                        sqlParameter.Value = ConvertStringValueToSqlDbType(stringDefaultValue, sqlParameter.SqlDbType);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                //
                //  If the parsing logic crashes for some reason, use the existing sqlParameterList.
                //
            }

            return status;
        }

        #endregion



        #region UpdateReturnedParameters

        /// <summary>
        /// Update return value and output parameters in the specified Parameters list from the SqlParameterList.
        /// <br/>
        /// This method should be called after every SQL query.
        /// </summary>
        /// <param name="sqlParameterList">
        /// The SqlParameter[] list supplied to the SQL query and containing the return/output parameters.
        /// </param>
        /// <param name="parameters">
        /// The original ParameterList that needs to be updated.
        /// </param>
        private static void UpdateReturnedParameters(SqlParameter[] sqlParameterList, ParameterList parameters)
        {
            foreach (SqlParameter p in sqlParameterList)
            {
                switch (p.Direction)
                {
                    case ParameterDirection.ReturnValue:
                    case ParameterDirection.InputOutput:
                    case ParameterDirection.Output:
                        {
                            parameters.Set(p.ParameterName, ConvertValueOut(p.SqlDbType, p.Value));
                            break;
                        }
                }
            }
        }

        #endregion
    }

    public class ErrorCode
    {
        public string ErrorID { get; set; }
        public string ErrorDescription { get; set; }
    }
}

