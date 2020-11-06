using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TranslationSystem
{
    public class Lenguage
    {
        /// <summary>
        /// The lenaguage ID
        /// </summary>
        public string lenguageID;

        /// <summary>
        /// A dictionary with the lenguages traductions [key textID][Value traducted text]
        /// </summary>
        public Dictionary<string, string> lenguageTexts = new Dictionary<string, string>();


        public Lenguage(string _lenguageID)
        {
            lenguageID = _lenguageID;
        }

    }
}
