using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PolyglotSave : ScriptableObject {

    /*
     * Se me chamar de javista ta morto
     * <3
     * */

    public class Categories
    {
        public int index;
        public string name = "";

        public Categories(int index, string name)
        {
            this.index = index;
            this.name = name;
        }

        public string List()
        {
            return "index: " + index + " | name: " + name;
        }
    }

    public class Translation
    {
        public int indexLanguage;
        public string nameID;
        public string translation;
        public string idUniqueElements;
        public Categories categories;

        public Translation(int indexLanguage, string nameID, string translation, string idUniqueElements, Categories categories)
        {
            this.indexLanguage = indexLanguage;
            this.nameID = nameID;
            this.translation = translation;
            this.categories = categories;
            this.idUniqueElements = idUniqueElements;
        }
    }

    public int selectedLanguage;
    public int selectedLanguageCategories;

    public List<Translation> translations = new List<Translation>();

    public List<string> languages = new List<string>();
    public List<string> languagesCategories = new List<string>();

    public PolyglotSave(int selectedLanguage, int selectedLanguageCategories, List<Translation> translations, List<string> languages, List<string> languagesCategories)
    {
        this.selectedLanguage = selectedLanguage;
        this.selectedLanguageCategories = selectedLanguageCategories;
        this.translations = translations;
        this.languages = languages;
        this.languagesCategories = languagesCategories;
    }
}
