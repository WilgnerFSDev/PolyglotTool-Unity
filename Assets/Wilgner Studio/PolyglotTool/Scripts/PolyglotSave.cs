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
	}
}