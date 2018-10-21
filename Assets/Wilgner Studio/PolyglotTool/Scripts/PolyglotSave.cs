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

		/*
	    public PolyglotSave(int selectedLanguage, int selectedLanguageCategories, List<Translation> translations, List<string> languages, List<string> languagesCategories)
	    {
	        this.selectedLanguage = selectedLanguage;
	        this.selectedLanguageCategories = selectedLanguageCategories;
	        this.translations = translations;
	        this.languages = languages;
	        this.languagesCategories = languagesCategories;
	    }*/
	}
}