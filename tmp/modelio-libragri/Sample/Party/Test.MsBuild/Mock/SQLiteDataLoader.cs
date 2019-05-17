using System;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using System.IO;
using KBCsv;

namespace PartyDomain.Test.Mock
{
    public class SQLiteDataLoader
    {

        private const string ATTACHED_DB = "zxcvbnmInitialData";

        private SQLiteConnection _connection;
        private string _initialDataFilename;

        public SQLiteDataLoader(SQLiteConnection connection)
        {
            _connection = connection;
        }

        public void ImportFromDbFile(string initialDataFilename)
        {
            _initialDataFilename=initialDataFilename;
            DataTable dt = _connection.GetSchema(SQLiteMetaDataCollectionNames.Tables);
            var tableNames = (from DataRow R in dt.Rows
                             select (string)R["TABLE_NAME"]).ToArray();
            AttachDatabase();
            foreach (string tableName in tableNames)
            {
                CopyTableData(tableName);
            }
            DetachDatabase();
        }

        private void AttachDatabase()
        {
            SQLiteCommand cmd = new SQLiteCommand(_connection);
            cmd.CommandText = string.Format("ATTACH '{0}' AS {1}", _initialDataFilename, ATTACHED_DB);
            cmd.ExecuteNonQuery();
        }

        private void DetachDatabase()
        {
            SQLiteCommand cmd = new SQLiteCommand(_connection);
            cmd.CommandText = string.Format("DETACH {0}", ATTACHED_DB);
            cmd.ExecuteNonQuery();
        }

        private void CopyTableData(string TableName)
        {
            int rowsAffected;
            SQLiteCommand cmd = new SQLiteCommand(_connection);
            cmd.CommandText = string.Format("INSERT INTO {0} SELECT * FROM {1}.{0}", TableName, ATTACHED_DB );
            rowsAffected = cmd.ExecuteNonQuery();
        }


        public void ImportFromFileCSV(string pathFile)
        {
            string table = Path.GetFileNameWithoutExtension(pathFile);

            
            SQLiteCommand com = _connection.CreateCommand();
            string query = "INSERT INTO ["+table+"] VALUES(@values)";
            using (var fileStream = new FileStream(pathFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader streamReader = new StreamReader(fileStream))
            {
                using (CsvReader reader = new CsvReader(streamReader))
                {
                    reader.ValueSeparator =',';
                    reader.ReadHeaderRecord();
                    while (reader.HasMoreRecords)
                    {
                        string values="";
                        DataRecord record = reader.ReadDataRecord();
                        com.Parameters.Clear();
                        for (int i = 0; i < reader.HeaderRecord.Count; i++)
                        {
                            if(!string.IsNullOrEmpty(values))
                            {
                                values+=",";
                            }
                            values+="'"+record[i]+"'";
                        }
                        com.CommandText=query.Replace("@values",values);
                        com.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}