using System.Data;

namespace ECommerceSolution.Service.Extensions
{
    /// <summary>
    /// The SqlDataReaderHelper class provides methods to read data
    /// from a SqlDataReader.
    /// </summary>
    public class SqlDataReaderHelper
    {
        #region Constructor

        /// <summary>
        /// Creates a new instance of SqlDataReaderHelper.
        /// </summary>
        /// <param name="dataReader">
        /// The data reader that will be read from.
        /// </param>
        public SqlDataReaderHelper(IDataReader dataReader)
        {
            _Reader = dataReader;
        }

        /// <summary>
        /// Creates a new instance of SqlDataReaderHelper.
        /// </summary>
        /// <param name="record">
        /// The data record that will be read from.
        /// </param>
        public SqlDataReaderHelper(IDataRecord record)
        {
            _Reader = record as IDataReader;
        }

        #endregion

        #region Constants

        private const Int32 READ_BUFFER_SIZE = (64 * 1024);

        #endregion

        #region Fields

        private IDataReader _Reader;

        #endregion

        #region IsDBNull

        /// <summary>
        /// Gets a value that indicates whether the columnName value is DBNull.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <returns>
        /// <b>true</b> if the columnName value is DBNull, otherwise <b>false</b>.
        /// </returns>
        public bool IsDBNull(string columnName)
        {
            int ordinal = _Reader.GetOrdinal(columnName);
            return _Reader.IsDBNull(ordinal);
        }

        #endregion

        #region GetValue

        /// <summary>
        /// Gets a value by colum name from the SQL data reader.
        /// If the column is DbNull, then default(T) will be returned.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the data to be read.
        /// </typeparam>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <returns>
        /// Data from the reader cast to the specified type &lt;T&gt;.
        /// </returns>
        public T GetValue<T>(string columnName)
        {
            int ordinal = -1;

            try
            {
                ordinal = _Reader.GetOrdinal(columnName);
            }
            catch (Exception ex)
            {
                if (ex is IndexOutOfRangeException)
                {
                    string newMessage = string.Format("The column name '{0}' does not exist.", ex.Message);
                    ex = new IndexOutOfRangeException(newMessage, ex);
                    throw ex;
                }

                throw;
            }

            T value;

            if (_Reader.GetValue(ordinal).GetType() == typeof(T))
                value = (T)_Reader.GetValue(ordinal);
            else
            {
                try
                {
                    value = default(T); //DataHelper.ConvertToType<T>(_Reader.GetValue(ordinal));
                }
                catch
                {
                    value = default(T);
                }
            }

            return value;
        }

        #endregion

        #region GetBoolean

        /// <summary>
        /// Gets a Boolean value from the specified columnName.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <returns>
        /// A Boolean value from the specified columnName.  
        /// If the column value is null, then <b>false</b> will be returned.
        /// </returns>
        public Boolean GetBoolean(string columnName)
        {
            return GetValue<Boolean>(columnName);
        }

        #endregion

        #region GetByte

        /// <summary>
        /// Gets a Byte value from the specified columnName.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <returns>
        /// A Byte value from the specified columnName.  
        /// If the column value is null, then (byte)0 will be returned.
        /// </returns>
        public Byte GetByte(string columnName)
        {
            return GetValue<Byte>(columnName);
        }

        #endregion

        #region GetBytes

        /// <summary>
        /// Reads bytes from the specified columnName into a buffer.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <param name="dataIndex">
        /// The starting index from which data will be read (from columnName).
        /// </param>
        /// <param name="buffer">
        /// The buffer into which bytes will be read.
        /// </param>
        /// <param name="bufferIndex">
        /// The starting index into which bytes will be read (in buffer).
        /// </param>
        /// <param name="length">
        /// The number of bytes to read.
        /// </param>
        /// <returns>
        /// The number of bytes read into the specified buffer.
        /// If the column value is null, then 0 will be returned.
        /// </returns>
        public Int64 GetBytes(string columnName, Int64 dataIndex, Byte[] buffer, Int32 bufferIndex, Int32 length)
        {
            int ordinal = _Reader.GetOrdinal(columnName);

            if (_Reader.IsDBNull(ordinal))
                return 0;
            else
                return _Reader.GetBytes(ordinal, dataIndex, buffer, bufferIndex, length);
        }

        /// <summary>
        /// Reads all data from the specified columnName and return as a byte array..
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <returns>
        /// All data in the column as a byte array.
        /// </returns>
        /// <remarks>
        /// If the column is NULL or contains no data, an empty byte array will be returned.
        /// </remarks>
        public byte[] GetBytes(string columnName)
        {
            int ordinal = _Reader.GetOrdinal(columnName);

            if (_Reader.IsDBNull(ordinal))
                return new byte[0];
            else
            {
                MemoryStream returnData = new MemoryStream();
                Int64 totalBytesRead = 0;
                Int64 actualBytesRead;
                Int64 dataIndex = 0;
                byte[] buffer = new byte[READ_BUFFER_SIZE];

                do
                {
                    actualBytesRead = _Reader.GetBytes(ordinal, dataIndex, buffer, 0, READ_BUFFER_SIZE);

                    if (actualBytesRead > 0)
                    {
                        dataIndex += actualBytesRead;
                        totalBytesRead += actualBytesRead;
                        returnData.Write(buffer, 0, (Int32)actualBytesRead);
                    }
                }
                while (actualBytesRead > 0);

                returnData.Capacity = (Int32)totalBytesRead;

                return returnData.GetBuffer();
            }
        }

        #endregion

        #region GetBytesLength

        /// <summary>
        /// Gets the length of a varbinary column data.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <returns>
        /// The length, in bytes, of the data in the specified columnName.
        /// </returns>
        public Int64 GetBytesLength(string columnName)
        {
            int ordinal = _Reader.GetOrdinal(columnName);

            if (_Reader.IsDBNull(ordinal))
                return 0;
            else
            {
                return _Reader.GetBytes(ordinal, 0, null, 0, Int32.MaxValue);
            }
        }

        #endregion

        #region GetChars

        /// <summary>
        /// Reads characters from the specified columnName into a buffer.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <param name="dataIndex">
        /// The starting index from which characters will be read (from columnName).
        /// </param>
        /// <param name="buffer">
        /// The buffer into which characters will be read.
        /// </param>
        /// <param name="bufferIndex">
        /// The starting index into which characters will be read (in buffer).
        /// </param>
        /// <param name="length">
        /// The number of characters to read.
        /// </param>
        /// <returns>
        /// The number of characters read into the specified buffer.
        /// If the column value is null, then 0 will be returned.
        /// </returns>
        public Int64 GetChars(string columnName, Int64 dataIndex, Char[] buffer, Int32 bufferIndex, Int32 length)
        {
            int ordinal = _Reader.GetOrdinal(columnName);

            if (_Reader.IsDBNull(ordinal))
                return 0;
            else
                return _Reader.GetChars(ordinal, dataIndex, buffer, bufferIndex, length);
        }

        #endregion

        #region GetDateTime

        /// <summary>
        /// Gets a DateTime value from the specified columnName.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <returns>
        /// A DateTime value from the specified columnName.  
        /// If the column value is null, then DateTime.MinValue will be returned.
        /// </returns>
        public DateTime GetDateTime(string columnName)
        {
            return GetValue<DateTime>(columnName);
        }

        #endregion

        #region GetDateTimeUTC

        /// <summary>
        /// Gets a DateTime value from the specified columnName.
        /// Guarantees that the DateTimeKind is DateTimeKind.UTC for the returned value.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <returns>
        /// A DateTime value from the specified columnName.  
        /// If the column value is null, then DateTime.MinValue will be returned.
        /// </returns>
        public DateTime GetDateTimeUTC(string columnName)
        {
            DateTime dateTime = GetValue<DateTime>(columnName);

            dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

            return dateTime;
        }

        #endregion

        #region GetDecimal

        /// <summary>
        /// Gets a Decimal value from the specified columnName.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <returns>
        /// A Decimal value from the specified columnName.  
        /// If the column value is null, then 0.0 will be returned.
        /// </returns>
        public Decimal GetDecimal(string columnName)
        {
            return GetValue<Decimal>(columnName);
        }

        #endregion

        #region GetDouble

        /// <summary>
        /// Gets a Double value from the specified columnName.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <returns>
        /// A Double value from the specified columnName.  
        /// If the column value is null, then 0.0 will be returned.
        /// </returns>
        public Double GetDouble(string columnName)
        {
            return GetValue<Double>(columnName);
        }

        #endregion

        #region GetGuid

        /// <summary>
        /// Gets a Guid value from the specified columnName.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <returns>
        /// A Guid value from the specified columnName.  
        /// If the column value is null, then Guid.Empty will be returned.
        /// </returns>
        public Guid GetGuid(string columnName)
        {
            return GetValue<Guid>(columnName);
        }

        #endregion

        #region GetInt16

        /// <summary>
        /// Gets an Int16 value from the specified columnName.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <returns>
        /// An Int16 value from the specified columnName.  
        /// If the column value is null, then 0 will be returned.
        /// </returns>
        public Int16 GetInt16(string columnName)
        {
            return GetValue<Int16>(columnName);
        }

        #endregion

        #region GetInt32

        /// <summary>
        /// Gets an Int32 value from the specified columnName.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <returns>
        /// An Int32 value from the specified columnName.  
        /// If the column value is null, then 0 will be returned.
        /// </returns>
        public Int32 GetInt32(string columnName)
        {
            return GetValue<Int32>(columnName);
        }

        #endregion

        #region GetInt64

        /// <summary>
        /// Gets an Int64 value from the specified columnName.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <returns>
        /// An Int64 value from the specified columnName.  
        /// If the column value is null, then 0 will be returned.
        /// </returns>
        public Int64 GetInt64(string columnName)
        {
            return GetValue<Int64>(columnName);
        }

        #endregion

        #region GetSingle

        /// <summary>
        /// Gets a Single value from the specified columnName.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <returns>
        /// A Single value from the specified columnName.  
        /// If the column value is null, then 0.0 will be returned.
        /// </returns>
        public Single GetSingle(string columnName)
        {
            return GetValue<Single>(columnName);
        }

        #endregion

        #region GetString

        /// <summary>
        /// Gets a String value from the specified columnName.
        /// </summary>
        /// <param name="columnName">
        /// The name of the column.
        /// </param>
        /// <returns>
        /// A String value from the specified columnName.  
        /// If the column value is null, then String.Empty will be returned.
        /// </returns>
        public String GetString(string columnName)
        {
            return GetValue<String>(columnName);
        }

        #endregion
    }
}
