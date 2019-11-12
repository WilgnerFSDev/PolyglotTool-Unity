/*
* Author: Wilgner Fábio
* Contributors: N0BODE
*/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Polyglot;

namespace Polyglot.Editor
{
	public class PolyglotWindow : EditorWindow 
	{
        // Which languages and categories are selected respectively
        private int selectedLanguage = 0;
		private int selectedLanguageCategories = 0;

        private Vector2 scrollTranslations;

        #region Languages Variables
        private bool newLanguage = false; // Is New Languge
		private bool editLanguage = false; // Edit mode
		private string nameLanguage = "New Language"; // Name of new or edit language
        #endregion

        #region Categories Variables
        private bool newCategories = false; // Is New Categories
        private bool editCategories = false; // Edit mode
        private string nameCategorie = "New Categorie"; // Name of new or edit categorie
        #endregion

        // Save/Load data in ScriptableObject (PolyglotSave)
        private PolyglotSave polyglot;

        // Name in inspector (List, New or Edit)
		private string menu = "List";

        #region Creates a window if it does not exist
        [MenuItem("Window/Polyglot Tool")]
	    public static void ShowWindow()
	    {
	        GetWindow<PolyglotWindow>("Polyglot Tool");
	    }
        #endregion

        #region Path where data is saved
        public string GetSaveLocalPath()
		{
			return "Assets/Wilgner Studio/PolyglotTool/DataBase/Polyglot.asset";
		}
        #endregion


        private void OnEnable()
		{
            #region Attempts to load data from the ScriptableObject if it exists, if there is no create a new
            this.polyglot = AssetDatabase.LoadAssetAtPath<PolyglotSave>(this.GetSaveLocalPath());
			if (this.polyglot == null)
			{
                PolyglotSave asset = ScriptableObject.CreateInstance<PolyglotSave>();

                AssetDatabase.CreateAsset(asset, this.GetSaveLocalPath());
                AssetDatabase.SaveAssets();

                this.polyglot = AssetDatabase.LoadAssetAtPath<PolyglotSave>(this.GetSaveLocalPath());
                this.SaveChanges ();
			}
            #endregion
        }

        private void OnDisable() {
            EditorUtility.SetDirty(polyglot);
            AssetDatabase.SaveAssets();
        }


        private void OnGUI()
	    {
            #region Icon and Social Links
            GUILayout.Space(10);
            Texture2D logo = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Wilgner Studio/PolyglotTool/Images/Editor/pgt-logo.png", typeof(Texture2D));
            var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
            GUILayout.Label(logo, style, GUILayout.ExpandWidth(true), GUILayout.Height(70));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            int buttonSize = 40;

            Texture2D githubicon = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Wilgner Studio/PolyglotTool/Images/Editor/github-icon.png", typeof(Texture2D));
            if (GUILayout.Button(githubicon, GUILayout.MaxWidth(buttonSize), GUILayout.MaxHeight(buttonSize)))
            {
                Application.OpenURL("https://github.com/WilgnerFSDev/PolyglotTool-Unity");
            }

            Texture2D wsicon = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Wilgner Studio/PolyglotTool/Images/Editor/ws-icon.png", typeof(Texture2D));
            if (GUILayout.Button(wsicon, GUILayout.MaxWidth(buttonSize), GUILayout.MaxHeight(buttonSize)))
            {
                Application.OpenURL("http://wilgnerstudio.com");
            }
            /*
            if (GUILayout.Button("A", GUILayout.MaxWidth(buttonSize), GUILayout.MaxHeight(buttonSize)))
            {
                // Asset Store
                //Application.OpenURL("http://unity3d.com/");
            }
            */

            Texture2D ppicon = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Wilgner Studio/PolyglotTool/Images/Editor/pp-icon.png", typeof(Texture2D));
            if (GUILayout.Button(ppicon, GUILayout.MaxWidth(buttonSize), GUILayout.MaxHeight(buttonSize)))
            {
                // Donate - Buy me a coffee?
                Application.OpenURL("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=NVH5N8ALD8R7C");
            }
            GUILayout.EndHorizontal();
            #endregion

            #region Languages CRUD
            GUILayout.BeginVertical("Box");
			GUILayout.Label(string.Format("Supported Languages: {0}", menu), EditorStyles.boldLabel);
	        GUILayout.BeginHorizontal();
	        DrawLanguage();
	        GUILayout.EndHorizontal();
	        GUILayout.EndVertical();
	        #endregion

	        #region Language Categories/Translations
	        GUILayout.BeginVertical("Box");
	        GUILayout.Label("Categorioes", EditorStyles.boldLabel);
	        DrawCategoriesTranslations();
	        GUILayout.EndVertical();
            #endregion

        }

	    void DrawLanguage()
	    {
	        #region Add/Edit Language
	        if (newLanguage == true)
	        {
				string buttonName = (editLanguage ? "Confirm" : "Add");
	            nameLanguage = GUILayout.TextField(nameLanguage);
				if (GUILayout.Button(buttonName, GUILayout.MaxWidth(150)))
	            {
                    bool exitsLanguage = false;

	                if (editLanguage == false) {
	                    exitsLanguage = AddLanguage(nameLanguage);
                        int indexLanguage = FindIndexLanguageByName(nameLanguage);
                        if(indexLanguage != -1) {
                            // 
                            foreach (Translation t in polyglot.translations.ToArray()) {
                                if (t.indexLanguage == 0) {
                                    Translation newTranslation = new Translation(indexLanguage, t.nameID, "Translation here", t.idUniqueElements, t.categories);
                                    polyglot.translations.Add(newTranslation);
                                }
                            }
                        }
                        else {
                            Debug.LogError("Unable to add existing translations for new language!");
                        }            
                    }
                    else {
                        this.polyglot.languages[selectedLanguage] = nameLanguage;
                    }

                    if (exitsLanguage == false)
                    {
                        ChangeNewLanguage();
                        menu = "List";
                    }
	            }
	            if (GUILayout.Button("Cancel", GUILayout.MaxWidth(150)))
	            {
	                ChangeNewLanguage();
	                menu = "List";
	            }
	        }else
	        #endregion

	        #region List Languages
	        {
				selectedLanguage = EditorGUILayout.Popup(selectedLanguage, polyglot.languages.ToArray());
	            if (GUILayout.Button("New", GUILayout.MaxWidth(150)))
	            {
                    ChangeNewLanguage();
					int count = polyglot.languages.Count + 1;
	                nameLanguage = "New Language "+count;
	                editLanguage = false;
	                menu = "New";
	            }
	            if (GUILayout.Button("Edit", GUILayout.MaxWidth(150)))
	            {
                    if (polyglot.languages.Count == 0) return;
                    ChangeNewLanguage();
	                nameLanguage = polyglot.languages[selectedLanguage];
	                editLanguage = true;
	                menu = "Edit";
	            }
	            if (GUILayout.Button("Delete", GUILayout.MaxWidth(150)))
	            {
                    if (polyglot.languages.Count == 0) return;
                    if (EditorUtility.DisplayDialog("Delete Language", string.Format(@"Are you sure you want to delete the {0} language and all data together?", polyglot.languages[selectedLanguage]), "Yes", "No")) {
                        polyglot.languages.RemoveAt(selectedLanguage);
                        selectedLanguage = polyglot.languages.Count - 1;
                    }
                    
	            }
	        }
	        #endregion
	    }

	    void DrawCategoriesTranslations()
	    {
            #region Add/Edit Categories
            GUILayout.BeginHorizontal();
            if (newCategories == true)
	        {
				string buttonName = (editLanguage ? "Confirm" : "Add");
	            nameCategorie = GUILayout.TextField(nameCategorie);
	            if (GUILayout.Button(buttonName, GUILayout.MaxWidth(150)))
	            {
	                if (editCategories == false)
	                    AddCategories(nameCategorie);
	                else
						polyglot.languagesCategories[selectedLanguageCategories] = nameCategorie;

	                ChangeNewCategories();
	                menu = "List";
	            }
	            if (GUILayout.Button("Cancel", GUILayout.MaxWidth(150)))
	            {
	                ChangeNewCategories();
	                menu = "List";
	            }
	        }
	        else
            #endregion

            #region Categories CRUD
            {
				selectedLanguageCategories = EditorGUILayout.Popup(selectedLanguageCategories, polyglot.languagesCategories.ToArray());
	            if (GUILayout.Button("New", GUILayout.MaxWidth(150)))
	            {
                    if (polyglot.languages.Count == 0) return;
                    ChangeNewCategories();
					int count = polyglot.languagesCategories.Count + 1;
	                nameCategorie = "New Categorie " + count;
	                editCategories = false;
	                menu = "New";
	            }
	            if (GUILayout.Button("Edit", GUILayout.MaxWidth(150)))
	            {
                    if (polyglot.languagesCategories.Count == 0) return;
                    ChangeNewCategories();
					nameCategorie = polyglot.languagesCategories[selectedLanguageCategories];
	                editCategories = true;
	                menu = "Edit";
	            }
	            if (GUILayout.Button("Delete", GUILayout.MaxWidth(150)))
	            {
                    if (polyglot.languagesCategories.Count == 0) return;
                    if (EditorUtility.DisplayDialog("Delete Language", string.Format(@"Are you sure you want to delete the {0} language and all data together?", polyglot.languages[selectedLanguage]), "Yes", "No"))
	                {
	                    RemoveCategorie();
	                }
	            }
	        }
            GUILayout.EndHorizontal();
            #endregion

            #region List Translations HEADER
            GUILayout.BeginVertical("Box");
	        GUILayout.Label("Translation", EditorStyles.boldLabel);
	        GUILayout.BeginHorizontal("CN Box", GUILayout.MaxHeight(7));
	        GUILayout.Label("Name ID", EditorStyles.boldLabel);
	        GUILayout.FlexibleSpace();
	        GUILayout.Label("Translation", EditorStyles.boldLabel);
	        GUILayout.FlexibleSpace();
	        GUILayout.Label("Settings", EditorStyles.boldLabel);
	        GUILayout.EndHorizontal();
            #endregion

            #region List Elements
            scrollTranslations = EditorGUILayout.BeginScrollView(scrollTranslations, GUILayout.Width(position.width*0.977f), GUILayout.Height(140));
            // Browse a list of translations
            for (int i = 0; i < polyglot.translations.Count; i++)
	        {
				Translation t = polyglot.translations [i];
                // Check if the translation is part of the selected language
                if (t.indexLanguage == selectedLanguage)
	            {
                    // If part of the selected category
                    if (selectedLanguageCategories == t.categories.index)
	                {
	                    float tam = position.width / 2 - 30;
	                    GUILayout.BeginHorizontal("Box", GUILayout.MaxHeight(20));
	                    t.nameID = GUILayout.TextField(t.nameID, GUILayout.MaxWidth(tam));
	                    t.translation = GUILayout.TextField(t.translation, GUILayout.MaxWidth(tam));

                        // Changes the brother element in the other languages and get the brother
                        List<Translation> brotherElements = ChangeIdAnotherLanguage(t.idUniqueElements, t.nameID);

                        // If you click the button to delete the translation
                        if (GUILayout.Button("\u00D7", GUILayout.MaxWidth(30)))
	                    {
                            // Case YES
                            if (EditorUtility.DisplayDialog("Delete Translation", "Are you sure you want to delete the " + t.nameID + " ?", "Yes", "No"))
	                        {
								polyglot.translations.RemoveAt(i); // Remove translation
                                polyglot.DisableIdElement(t.idUniqueElements); // Disable id brother
                                if (brotherElements != null) {
                                    foreach(Translation tb in brotherElements) {
                                        polyglot.translations.Remove(tb); // Delete brother element
                                    }
                                }
                                   
	                        }
	                    }
	                    GUILayout.EndHorizontal();
	                    GUILayout.Space(2);
	                }
	            }
	        }
            EditorGUILayout.EndScrollView();
            #endregion

            #region Add New Translation
            GUILayout.BeginVertical();
            // If you click the button to add new translation
            if (GUILayout.Button("Add New Translation"))
	        {
                // A language and category is required to create a translation
                string error = "";
                if (polyglot.languages.Count == 0) error += "Language is required\n";
                if (polyglot.languagesCategories.Count == 0) error += "Category is required";

                if (error != "") {
                    Debug.Log(error);
                    return;
                }

                // Create new categories (selected)
                Categories c = new Categories(selectedLanguageCategories, polyglot.languagesCategories[selectedLanguageCategories].ToString());

                // Try get a shared id between brothers translations
                Vector2Int idE = GetIdElements();

                // If there is no shared id available
                if (idE.y == 0)
                {
                    // Create new shared id
                    IdElements idElement = new IdElements(true, polyglot.idElements.Count);
                    polyglot.idElements.Add(idElement);
                }

                // idE.x = id available (Line: 260)
                // Creates the new translation in all available languages
                for (int i = 0; i < polyglot.languages.Count; i++) {
                    Translation element = new Translation(i, "Item Id", "Translation here", idE.x, c);
					polyglot.translations.Add(element);
				}
	        }
	        GUILayout.EndVertical();
            #endregion

            GUILayout.EndVertical(); // End Begin Vertical in Categories HEADER (Line: 147)
	    }

        // Changes the brother element in the other languages and get the brother
        List<Translation> ChangeIdAnotherLanguage(int idUniqueElements, string nameIDBrotherLanguage)
	    {
            List<Translation> brotherElementsList = new List<Translation>();
            // Scroll through the translations list
            foreach (Translation t in polyglot.translations)
	        {
                // Check if the translation is from the language selected
                if (t.indexLanguage != selectedLanguage)
	            {
                    // Check if the translation is from the selected category
                    if (selectedLanguageCategories == t.categories.index)
	                {
                        // Check if id's are brothers
                        if (t.idUniqueElements == idUniqueElements)
	                    {
                            // Change the name of the brother element
                            t.nameID = nameIDBrotherLanguage;
                            brotherElementsList.Add(t);

                        }
	                }
	            }
	        }
            // Return all brothers
            return brotherElementsList;
	    }

        // Check if a unique id(Compatibility) is free
        Vector2Int GetIdElements(){
            // Browse the list of shared ids
            foreach (IdElements ie in polyglot.idElements)
            {
                // If it is not in use (no translation is using it)
                if (ie.inUse == false)
                {
                    // Set In Use true
                    ie.inUse = true;
                    // Returns the id and id is available (no need to create a new one)
                    return new Vector2Int(ie.id, 1);
                }
            }
            // Returns an id that does not exist
            return new Vector2Int(polyglot.idElements.Count, 0);
        }

        // Add a language if it does not exist
        bool AddLanguage(string name)
	    {
            bool exits = false;
            // Browsing all the words in the languages list
            foreach (string l in polyglot.languages)
            {
                // If the language exists
                if (l == name)
                    exits = true;
            }

            // If language does not exist
            if (exits == false)
                polyglot.languages.Add(name); // Add new language
            else
                Debug.Log(name + " language already exists");

            return exits;
        }

        // Add a categories if it does not exist
        void AddCategories(string name)
	    {
            bool exits = false;
            // Browsing all the words in the categories list
            foreach (string c in polyglot.languagesCategories)
            {
                // If the categories exists
                if (c == name)
                    exits = true;
            }

            // If categories does not exist
            if (exits == false)
            {
                polyglot.languagesCategories.Add(name); // Add new categorie
                selectedLanguageCategories = polyglot.languagesCategories.Count - 1; // Select the last categories
            }
            else
            {
                Debug.Log(name + " categories already exists");
            }
	    }

        // Remove categorie
	    void RemoveCategorie()
	    {
            // Browsing all the words in the languages list
            foreach (Translation t in polyglot.translations.ToArray())
	        {
                // Get selected categorie
				string c = polyglot.languagesCategories[selectedLanguageCategories];

                // If the translation of the category is equal to the selected category
                if (t.categories.name == c)
					polyglot.translations.Remove(t); // Removes all related translations
            }
            // Remove categories
			polyglot.languagesCategories.RemoveAt(selectedLanguageCategories);
			selectedLanguageCategories = polyglot.languagesCategories.Count - 1; // Select the last categories
        }

        // New, editing language or not
        void ChangeNewLanguage()
	    {
	        newLanguage = !newLanguage;
	    }

        // New, editing categorie or not
        void ChangeNewCategories()
	    {
	        newCategories = !newCategories;
	    }

        // Save all data of Windows in ScriptableObject set in the Path
	    void SaveChanges()
		{
			polyglot.selectedLanguage = this.selectedLanguage;
			polyglot.selectedLanguageCategories = this.selectedLanguageCategories;
	    }
        
        int FindIndexLanguageByName(string name) {
            int i = 0;
            foreach(string l in polyglot.languages) {
                if(l == name)
                    return i;

                i++;
            }

            return -1;
        }
        
    }
}
