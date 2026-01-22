using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace ZadHolding.Data.Utilities
{
    /// <summary>
    /// DataHelper class provides a safe way to access value from DataRow and DataReader
    /// </summary>
    public class DataHelper
    {
        #region "Safe DataRow access functions"

        /// <summary>
        /// Retrive value from the specefied field as short
        /// </summary>
        /// <param name="row">data row</param>
        /// <param name="field">Field name</param>
        /// <param name="defaultValue">Value when the data row value is null</param>
        /// <returns>The value in the datarow or the default value if the datarow contains a NULL</returns
        static public short GetSafeShort(DataRow row, string field, short defaultValue)
        {           
            if (row == null || field == null || field.Length == 0)
                return defaultValue;

            return row.IsNull(field) ? defaultValue : (short)row[field];
        }

        /// <summary>
        /// Retrive value from the specefied field as integer
        /// </summary>
        /// <param name="row">data row</param>
        /// <param name="field">Field name</param>
        /// <param name="defaultValue">Value when the data row value is null</param>
        /// <returns>The value in the datarow or the default value if the datarow contains a NULL</returns
        static public int GetSafeInt(DataRow row, string field, int defaultValue)
        {
            if (row == null || field == null || field.Length == 0)
                return defaultValue;

            return row.IsNull(field) ? defaultValue : (int)row[field];
        }

        /// <summary>
        /// Retrive value from the specefied field as long   
        /// </summary>
        /// <param name="row">data row</param>
        /// <param name="field">Field name</param>
        /// <param name="defaultValue">Value when the data row value is null</param>
        /// <returns>The value in the datarow or the default value if the datarow contains a NULL</returns
        static public long GetSafeLong(DataRow row, string field, long defaultValue)
        {
            if (row == null || field == null || field.Length == 0)
                return defaultValue;

            return row.IsNull(field) ? defaultValue : (long) row[field];
        }

        /// <summary>
        /// Retrive value from the specefied field as float   
        /// </summary>
        /// <param name="row">data row</param>
        /// <param name="field">Field name</param>
        /// <param name="defaultValue">Value when the data row value is null</param>
        /// <returns>The value in the datarow or the default value if the datarow contains a NULL</returns
        static public float GetSafeFloat(DataRow row, string field, float defaultValue)
        {
            if (row == null || field == null || field.Length == 0)
                return defaultValue;

            return row.IsNull(field) ? defaultValue : (float)row[field];
        }
                
        /// <summary>
        /// Retrive value from the specefied field as double    
        /// </summary>
        /// <param name="row">data row</param>
        /// <param name="field">Field name</param>
        /// <param name="defaultValue">Value when the data row value is null</param>
        /// <returns>The value in the datarow or the default value if the datarow contains a NULL</returns
        static public double GetSafeDouble(DataRow row, string field, double defaultValue)
        {
            if (row == null || field == null || field.Length == 0)
                return defaultValue;

            return row.IsNull(field) ? defaultValue : (double)row[field];
        }

        /// <summary>
        /// Retrive value from the specefied field as decimal    
        /// </summary>
        /// <param name="row">data row</param>
        /// <param name="field">Field name</param>
        /// <param name="defaultValue">Value when the data row value is null</param>
        /// <returns>The value in the datarow or the default value if the datarow contains a NULL</returns
        static public decimal GetSafeDecimal(DataRow row, string field, decimal defaultValue)
        {
            if (row == null || field == null || field.Length == 0)
                return defaultValue;

            return row.IsNull(field) ? defaultValue : (decimal)row[field];
        }

        /// <summary>
        /// Retrive value from the specefied field as DateTime    
        /// </summary>
        /// <param name="row">data row</param>
        /// <param name="field">Field name</param>
        /// <param name="defaultValue">Value when the data row value is null</param>
        /// <returns>The value in the datarow or the default value if the datarow contains a NULL</returns
        static public DateTime GetSafeDateTime(DataRow row, string field, DateTime defaultValue)
        {
            if (row == null || field == null || field.Length == 0)
                return defaultValue;

            return row.IsNull(field) ? defaultValue : (DateTime)row[field];
        }

        /// <summary>
        /// Retrive value from the specefied field as byte    
        /// </summary>
        /// <param name="row">data row</param>
        /// <param name="field">Field name</param>
        /// <param name="defaultValue">Value when the data row value is null</param>
        /// <returns>The value in the datarow or the default value if the datarow contains a NULL</returns
        static public byte GetSafeByte(DataRow row, string field, byte defaultValue)
        {
            if (row == null || field == null || field.Length == 0)
                return defaultValue;

            return row.IsNull(field) ? defaultValue : (byte) row[field];
        }

        /// <summary>
        /// Retrive value from the specefied field as bool    
        /// </summary>
        /// <param name="row">data row</param>
        /// <param name="field">Field name</param>
        /// <param name="defaultValue">Value when the data row value is null</param>
        /// <returns>The value in the datarow or the default value if the datarow contains a NULL</returns
        static public bool GetSafeBool(DataRow row, string field, bool defaultValue)
        {
            if (row == null || field == null || field.Length == 0)
                return defaultValue;

            return row.IsNull(field) ? defaultValue : (bool)row[field];
        }

        #endregion

        #region "Safe DataReader access functions"
        /// <summary>
        /// Retrive value from the specefied field as short
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public short GetSafeShort(IDataReader reader, string field, short defaultValue)
        {
            if (reader == null || field == null || field.Length == 0)
                return defaultValue;

            return GetSafeShort(reader, reader.GetOrdinal(field), defaultValue);
        }

        /// <summary>
        /// Retrive value from the field at the specified ordinal as short
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public short GetSafeShort(IDataReader reader, int ordinal, short defaultValue)
        {
            if (reader == null || ordinal < 0)
                return defaultValue;

            try
            {
                return (reader.IsDBNull(ordinal) ? defaultValue : reader.GetInt16(ordinal));
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Retrive value from the specefied field as integer 
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public int GetSafeInt(IDataReader reader, string field, int defaultValue)
        {
            if (reader == null || field == null || field.Length == 0)
                return defaultValue;

            return GetSafeInt(reader, reader.GetOrdinal(field), defaultValue);
        }

        /// <summary>
        /// Retrive value from the field at the specified ordinal as short
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public int GetSafeInt(IDataReader reader, int ordinal, int defaultValue)
        {
            if (reader == null || ordinal < 0)
                return defaultValue;

            try
            {
                return (reader.IsDBNull(ordinal) ? defaultValue : reader.GetInt32(ordinal));
            }
            catch
            {
                return defaultValue;
            }
        }
        
        /// <summary>
        /// Retrive value from the specefied field as long 
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public long GetSafeLong(IDataReader reader, string field, long defaultValue)
        {
            if (reader == null || field == null || field.Length == 0)
                return defaultValue;

            return GetSafeLong(reader, reader.GetOrdinal(field), defaultValue);
        }

        /// <summary>
        /// Retrive value from the field at the specified ordinal as short
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public long GetSafeLong(IDataReader reader, int ordinal, long defaultValue)
        {
            if (reader == null || ordinal < 0)
                return defaultValue;

            try
            {
                return (reader.IsDBNull(ordinal) ? defaultValue : reader.GetInt64(ordinal));
            }
            catch
            {
                return defaultValue;
            }
        }


        /// <summary>
        /// Retrive value from the specefied field as byte
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public byte GetSafeByte(IDataReader reader, string field, byte defaultValue)
        {
            if (reader == null || field == null || field.Length == 0)
                return defaultValue;

            return GetSafeByte(reader, reader.GetOrdinal(field), defaultValue);
        }

        /// <summary>
        /// Retrive value from the field at the specified ordinal as byte
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public byte GetSafeByte(IDataReader reader, int ordinal, byte defaultValue)
        {
            if (reader == null || ordinal < 0)
                return defaultValue;

            try
            {
                return (reader.IsDBNull(ordinal) ? defaultValue : reader.GetByte(ordinal));
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Retrive value from the specefied field as float
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public float GetSafeFloat(IDataReader reader, string field, float defaultValue)
        {
            if (reader == null || field == null || field.Length == 0)
                return defaultValue;

            return GetSafeFloat(reader, reader.GetOrdinal(field), defaultValue);
        }

        /// <summary>
        /// Retrive value from the field at the specified ordinal as float
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public float GetSafeFloat(IDataReader reader, int ordinal, float defaultValue)
        {
            if (reader == null || ordinal < 0)
                return defaultValue;

            try
            {
                return (reader.IsDBNull(ordinal) ? defaultValue : reader.GetFloat(ordinal));
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Retrive value from the specefied field as Double
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public double GetSafeDouble(IDataReader reader, string field, double defaultValue)
        {
            if (reader == null || field == null || field.Length == 0)
                return defaultValue;

            return GetSafeDouble(reader, reader.GetOrdinal(field), defaultValue);
        }

        /// <summary>
        /// Retrive value from the field at the specified ordinal as Double
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public double GetSafeDouble(IDataReader reader, int ordinal, double defaultValue)
        {
            if (reader == null || ordinal < 0)
                return defaultValue;

            try
            {
                return (reader.IsDBNull(ordinal) ? defaultValue : reader.GetDouble(ordinal));
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Retrive value from the specefied field as decimal
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public decimal GetSafeDecimal(IDataReader reader, string field, decimal defaultValue)
        {
            if (reader == null || field == null || field.Length == 0)
                return defaultValue;

            return GetSafeDecimal(reader, reader.GetOrdinal(field), defaultValue);
        }

        /// <summary>
        /// Retrive value from the field at the specified ordinal as decimal
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public decimal GetSafeDecimal(IDataReader reader, int ordinal, decimal defaultValue)
        {
            if (reader == null || ordinal < 0)
                return defaultValue;

            try
            {
                return (reader.IsDBNull(ordinal) ? defaultValue : reader.GetDecimal(ordinal));
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Retrive value from the specefied field as bool
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public bool GetSafeBool(IDataReader reader, string field, bool defaultValue)
        {
            if (reader == null || field == null || field.Length == 0)
                return defaultValue;

            return GetSafeBool(reader, reader.GetOrdinal(field), defaultValue);
        }

        /// <summary>
        /// Retrive value from the field at the specified ordinal as bool
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public bool GetSafeBool(IDataReader reader, int ordinal, bool defaultValue)
        {
            if (reader == null || ordinal < 0)
                return defaultValue;

            try
            {
                return (reader.IsDBNull(ordinal) ? defaultValue : reader.GetBoolean(ordinal));
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Retrive value from the specefied field as DateTime
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public DateTime GetSafeDateTime(IDataReader reader, string field, DateTime defaultValue)
        {
            if (reader == null || field == null || field.Length == 0)
                return defaultValue;

            return GetSafeDateTime(reader, reader.GetOrdinal(field), defaultValue);
        }

        /// <summary>
        /// Retrive value from the field at the specified ordinal as DateTime
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public DateTime GetSafeDateTime(IDataReader reader, int ordinal, DateTime defaultValue)
        {
            if (reader == null || ordinal < 0)
                return defaultValue;

            try
            {
                return (reader.IsDBNull(ordinal) ? defaultValue : reader.GetDateTime(ordinal));
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Retrive value from the specefied field as string
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public string GetSafeString(IDataReader reader, string field, string defaultValue)
        {
            if (reader == null || field == null || field.Length == 0)
                return defaultValue;

            return GetSafeString(reader, reader.GetOrdinal(field), defaultValue);
        }

        /// <summary>
        /// Retrive value from the field at the specified ordinal as string
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public string GetSafeString(IDataReader reader, int ordinal, string defaultValue)
        {
            if (reader == null || ordinal < 0)
                return defaultValue;

            try
            {
                return (reader.IsDBNull(ordinal) ? defaultValue : reader.GetString(ordinal));
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Retrive value from the specefied field as GUID
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public Guid GetSafeGuid(IDataReader reader, string field, Guid defaultValue)
        {
            if (reader == null || field == null || field.Length == 0)
                return defaultValue;

            return GetSafeGuid(reader, reader.GetOrdinal(field), defaultValue);
        }

        /// <summary>
        /// Retrive value from the field at the specified ordinal as GUID
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public Guid GetSafeGuid(IDataReader reader, int ordinal, Guid defaultValue)
        {
            if (reader == null || ordinal < 0)
                return defaultValue;

            try
            {
                return (reader.IsDBNull(ordinal) ? defaultValue : reader.GetGuid(ordinal));
            }
            catch
            {
                return defaultValue;
            }
        }





        /// <summary>
        /// Retrive value from the specefied field as byte[]
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public byte[] GetSafeImage(IDataReader reader, string field, byte[] defaultValue)
        {
            if (reader == null || field == null || field.Length == 0)
                return defaultValue;

            return GetSafeImage(reader, reader.GetOrdinal(field), defaultValue);
        }

        /// <summary>
        /// Retrive value from the field at the specified ordinal as byte[]
        /// </summary>
        /// <param name="reader">datareader </param>
        /// <param name="ordinal">Field ordinal</param>
        /// <param name="defaultValue">Value when the reader value is null</param>
        /// <returns>The value in the reader or the default value if the reader contains a NULL</returns>
        static public byte[] GetSafeImage(IDataReader reader, int ordinal, byte[] defaultValue)
        {
            if (reader == null || ordinal < 0)
                return defaultValue;

            try
            {
                return (reader.IsDBNull(ordinal) ? defaultValue : reader.GetValue(ordinal) as byte[]);
            }
            catch
            {
                return defaultValue;
            }
        }


        


        #endregion
    }
}
