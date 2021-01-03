using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

namespace TranslationSystem
{
    public class TranslationManager : Singleton<TranslationManager>
    {
        [HideInInspector]
        //The event called whenever the lenguage is changed
        public event Action<string> OnLenguageChanged;

        public event Action OnDataLoaded;

        [HideInInspector]
        //the dictionary of lenguages
        public Dictionary<string, Dictionary<string, string>> traductions = new Dictionary<string, Dictionary<string, string>>();
        
        public string currentLenguage;
        [HideInInspector]
        public bool loaded = false;
        
        protected override void Awake()
        {
            base.Awake();
            
            LoadLenguageData();
            StartCoroutine(SetSavedLenguage());

        }

        public IEnumerator SetSavedLenguage()
        {
            yield return new WaitUntil(() => loaded && SaveLoadManager.Instance.dataLoaded);

            string lenguage = ConfigurationManager.settings.lenguage;

            if(lenguage=="")
            {
                if(traductions.Count>0)
                {
                    lenguage = traductions.Keys.ElementAt(0);
                }
            }

            currentLenguage = lenguage;

            ChangeLenguage(currentLenguage);
        }

        public void ChangeLenguage(string lenguageID)
        {
            print("ChangeLenguage");
            currentLenguage = lenguageID;
            OnLenguageChanged?.Invoke(lenguageID);
        }

        private void LoadLenguageData()
        {
            string conn = "URI=file:" + Application.dataPath + "/TranslationSystem/Data/TranslationData.sqlite"; //Path to database.
            IDbConnection dbconn;
            dbconn = (IDbConnection)new SqliteConnection(conn);
            dbconn.Open(); //Open connection to the database.
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT * " + "FROM Lenguage";
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            List<Lenguage> lenguages = new List<Lenguage>();
            while (reader.Read())
            {
                string lenguageID = reader.GetString(0);

                lenguages.Add(new Lenguage(lenguageID));

            }
            reader.Close();
            reader = null;
            
            dbcmd.Dispose();
            dbcmd = null;

            dbconn.Close();
            dbconn = null;

            traductions = new Dictionary<string, Dictionary<string, string>>();

            foreach (Lenguage lenguage in lenguages)
            {
                conn = "URI=file:" + Application.dataPath + "/TranslationSystem/Data/TranslationData.sqlite"; //Path to database.

                dbconn = (IDbConnection)new SqliteConnection(conn);
                dbconn.Open(); //Open connection to the database.
                dbcmd = dbconn.CreateCommand();
                sqlQuery = "SELECT * FROM Text WHERE LenguageID=\"" + lenguage.lenguageID + "\"";
                dbcmd.CommandText = sqlQuery;
                reader = dbcmd.ExecuteReader();
                lenguage.lenguageTexts = new Dictionary<string, string>();
                while (reader.Read())
                {
                    byte[] contentInBytes = (byte[])reader["Content"];
                    string content = ASCIIExtended.ByteToString(contentInBytes);
                    

                    
                    string textID = reader.GetString(0);

                    lenguage.lenguageTexts.Add(textID, content);

                }

                traductions.Add(lenguage.lenguageID, lenguage.lenguageTexts);

                reader.Close();
                reader = null;
                
                dbcmd.Dispose();
                dbcmd = null;

                dbconn.Close();
                dbconn = null;
            }



            loaded = true;
            OnDataLoaded?.Invoke();
            print("final loadData");
        }
    }

    
}


