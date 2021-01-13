using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using System.Linq;

namespace TranslationSystem
{
    [System.Serializable]
    [SerializeField]
    public class TranslationData 
    {

        public int prueba = 0;

        public List<LenguageTableData> lenguages;
        public List<TextTableData> translations;



        public Dictionary<string, Dictionary<string, string>> DataToDictionary()
        {
            Dictionary<string, Dictionary<string, string>> traductions = new Dictionary<string, Dictionary<string, string>>();

            foreach(LenguageTableData lenguage in lenguages)
            {

                List<TextTableData> lenguageTexts;
                lenguageTexts = translations.Where((x) => x.lenguageID == lenguage.lenguageID).ToList();

                Dictionary<string, string> lenguageTranslations = new Dictionary<string, string>();

                foreach(TextTableData textData in lenguageTexts)
                {
                    lenguageTranslations.Add(textData.textID, textData.translatedText);
                }

                traductions.Add(lenguage.lenguageID, lenguageTranslations);
            }
            
            return traductions;
        }

        public void LoadLenguageData()
        {

            lenguages = new List<LenguageTableData>();
            translations = new List<TextTableData>();

            string conn = "URI=file:" + Application.dataPath + "/TranslationSystem/Data/TranslationData.sqlite"; //Path to database.
            IDbConnection dbconn;
            dbconn = (IDbConnection)new SqliteConnection(conn);
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT * " + "FROM Lenguage";
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            lenguages = new List<LenguageTableData>();
            while (reader.Read())
            {
                string lenguageID = reader.GetString(0);

                lenguages.Add(new LenguageTableData(lenguageID));

            }
            reader.Close();
            reader = null;

            dbcmd.Dispose();
            dbcmd = null;

            dbconn.Close();
            dbconn = null;

            translations = new List<TextTableData>();

            foreach (LenguageTableData lenguage in lenguages)
            {
                conn = "URI=file:" + Application.dataPath + "/TranslationSystem/Data/TranslationData.sqlite"; //Path to database.

                dbconn = (IDbConnection)new SqliteConnection(conn);
                dbconn.Open(); //Open connection to the database.
                dbcmd = dbconn.CreateCommand();
                sqlQuery = "SELECT * FROM Text WHERE LenguageID=\"" + lenguage.lenguageID + "\"";
                dbcmd.CommandText = sqlQuery;
                reader = dbcmd.ExecuteReader();
                while (reader.Read())
                {
                    byte[] contentInBytes = (byte[])reader["Content"];
                    string translation = ASCIIExtended.ByteToString(contentInBytes);



                    string textID = reader.GetString(0);

                    translations.Add(new TextTableData(textID, lenguage.lenguageID,translation));

                }
                

                reader.Close();
                reader = null;

                dbcmd.Dispose();
                dbcmd = null;

                dbconn.Close();
                dbconn = null;
            }


            prueba = 8;
            
        }

        

    }

    [System.Serializable]
    public class LenguageTableData
    {
        public string lenguageID;

        public LenguageTableData(string _lenguageID)
        {
            lenguageID = _lenguageID;
        }

        public override string ToString()
        {
            return lenguageID;
        }
    }

    [System.Serializable]
    public class TextTableData
    {
        public string textID;
        public string lenguageID;
        public string translatedText;

        public TextTableData(string _textID, string _lenguageID, string _translation)
        {
            textID = _textID;
            lenguageID = _lenguageID;
            translatedText = _translation;
        }

        public override string ToString()
        {
            return lenguageID + " - " + textID + " => " + translatedText;
        }
    }
}

