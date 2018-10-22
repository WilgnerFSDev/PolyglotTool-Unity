/*
* Author: Wilgner Fábio
* Contributors: N0BODE
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polyglot{
	[System.Serializable]
	public class PolyglotSave : ScriptableObject {
	    public int selectedLanguage;
	    public int selectedLanguageCategories;

		public List<Translation> translations = new List<Translation>();
        public List<string> languages = new List<string>();
	    public List<string> languagesCategories = new List<string>();
        public List<IdElements> idElements = new List<IdElements>();

        public void DisableIdElement(int idD)
        {
            foreach (IdElements ie in idElements)
                if (ie.id == idD)
                    ie.inUse = false;
        }

        public List<Translation> AutoComplete(string name)
        {
            List<Translation> elementsFind = new List<Translation> ();
            foreach(Translation t in translations)
            {
                if (t.nameID.Contains(name) && t.indexLanguage == 0 && t.nameID != name)
                    elementsFind.Add(t);
            }
            return elementsFind;
        }

        public Translation GetTranslationByName(string name, int currentLang)
        {
            foreach (Translation t in translations)
            {
                if (t.nameID == name && t.indexLanguage == currentLang)
                    return t;
            }

            return null;
        }
    }
}