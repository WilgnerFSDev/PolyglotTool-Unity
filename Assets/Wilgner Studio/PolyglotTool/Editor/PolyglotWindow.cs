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
	    private int selectedLanguage = 0;
		private int selectedLanguageCategories = 0;

	    // New Language
		private bool newLanguage = false;
		private bool editLanguage = false;
		private string nameLanguage = "New Language";

	    // New Categories
		private bool newCategories = false;
		private bool editCategories = false;
		private string nameCategorie = "New Categorie";

	    // list for languages working

		private PolyglotSave polyglot;
		/* Replaced for PolyglotSave ScriptableObject
		private List<Translation> translations = new List<Translation>();
		private List<string> languages = new List<string>();
		private List<string> languagesCategories = new List<string>();
		*/

		private string menu = "List";

	    [MenuItem("Window/Polyglot Tool")]
	    public static void ShowWindow()
	    {
	        GetWindow<PolyglotWindow>("Polyglot Tool");

	    }

		public string GetSaveLocalPath()
		{
			return "Assets/Resources/Polyglot.asset";
		}

		private void OnEnable()
		{
			//This is a singleton
			this.polyglot = AssetDatabase.LoadAssetAtPath<PolyglotSave>(this.GetSaveLocalPath());
			if (this.polyglot == null)
			{
				polyglot = CreateInstance<PolyglotSave> ();
				this.SaveChanges ();
			}
		}

	    private void OnGUI()
	    {
	        #region Languages List CRUD
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
	                if (editLanguage == false)
	                    AddLanguage(nameLanguage);
	                else
						this.polyglot.languages[selectedLanguage] = nameLanguage;

	                ChangeNewLanguage();
	                menu = "List";
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
	                ChangeNewLanguage();
	                nameLanguage = polyglot.languages[selectedLanguage];
	                editLanguage = true;
	                menu = "Edit";

	                AddElementsForTest();
	            }
	            if (GUILayout.Button("Delete", GUILayout.MaxWidth(150)))
	            {
					if(EditorUtility.DisplayDialog("Delete Language", string.Format(@"Are you sure you want to delete the {0} language and all data together?", polyglot.languages[selectedLanguage]), "Yes", "No"))
					{
						polyglot.languages.RemoveAt(selectedLanguage);
						selectedLanguage = polyglot.languages.Count - 1;
	                }
	            }
	        }
	        #endregion
	    }

	    void AddElementsForTest()
	    {
			polyglot.translations.Clear();
			polyglot.languagesCategories.Clear();

	        Categories c0 = new Categories(0, "Game");
	        Categories c1 = new Categories(1, "Menu");
			polyglot.languagesCategories.Add("Game");
			polyglot.languagesCategories.Add("Menu");

	        Translation button0_En = new Translation(0, "Play Button", "Play", "Element0", c0);
	        Translation button0_Pt = new Translation(1, "Play Button", "Jogar", "Element0", c0);

	        Translation button1_En = new Translation(0, "Exit Button", "Exit", "Element1", c1);
	        Translation button1_Pt = new Translation(1, "Exit Button", "Sair", "Element1", c1);

			polyglot.translations.Add(button0_En);
			polyglot.translations.Add(button0_Pt);

			polyglot.translations.Add(button1_En);
			polyglot.translations.Add(button1_Pt);
	    }

	    void DrawCategoriesTranslations()
	    {
	        GUILayout.BeginHorizontal();
	        #region Add/Edit Language
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

	        #region List Languages
	        {
				selectedLanguageCategories = EditorGUILayout.Popup(selectedLanguageCategories, polyglot.languagesCategories.ToArray());
	            if (GUILayout.Button("New", GUILayout.MaxWidth(150)))
	            {
	                ChangeNewCategories();
					int count = polyglot.languagesCategories.Count + 1;
	                nameCategorie = "New Categorie " + count;
	                editCategories = false;
	                menu = "New";
	            }
	            if (GUILayout.Button("Edit", GUILayout.MaxWidth(150)))
	            {
	                ChangeNewCategories();
					nameCategorie = polyglot.languagesCategories[selectedLanguageCategories];
	                editCategories = true;
	                menu = "Edit";

	                AddElementsForTest();
	            }
	            if (GUILayout.Button("Delete", GUILayout.MaxWidth(150)))
	            {
					if(EditorUtility.DisplayDialog("Delete Language", string.Format(@"Are you sure you want to delete the {0} language and all data together?", polyglot.languages[selectedLanguage]), "Yes", "No"))
	                {
	                    RemoveCategorie();
	                }
	            }
	        }
	        #endregion
	        GUILayout.EndHorizontal();

	        GUILayout.BeginVertical("Box");
	        GUILayout.Label("Translation", EditorStyles.boldLabel);
	        GUILayout.BeginHorizontal("CN Box", GUILayout.MaxHeight(7));
	        GUILayout.Label("Name ID", EditorStyles.boldLabel);
	        GUILayout.FlexibleSpace();
	        GUILayout.Label("Translation", EditorStyles.boldLabel);
	        GUILayout.FlexibleSpace();
	        GUILayout.Label("Settings", EditorStyles.boldLabel);
	        GUILayout.EndHorizontal();

			for (int i = 0; i < polyglot.translations.Count; i++)
	        {
				Translation t = polyglot.translations [i];
	            if (t.indexLanguage == selectedLanguage)
	            {
	                if (selectedLanguageCategories == t.categories.index)
	                {
	                    float tam = position.width / 2;
	                    GUILayout.BeginHorizontal("Box", GUILayout.MaxHeight(20));
	                    t.nameID = GUILayout.TextField(t.nameID, GUILayout.MaxWidth(tam));
	                    t.translation = GUILayout.TextField(t.translation, GUILayout.MaxWidth(tam));

	                    Translation brotherElement = ChangeIdAnotherLanguage(t.idUniqueElements, t.nameID);

	                    if (GUILayout.Button("\u00D7", GUILayout.MaxWidth(30)))
	                    {
	                        if (EditorUtility.DisplayDialog("Delete Translation", "Are you sure you want to delete the " + t.nameID + " ?", "Yes", "No"))
	                        {
								polyglot.translations.RemoveAt(i);
	                            if (brotherElement != null)
									polyglot.translations.Remove(brotherElement);
	                        }
	                    }
	                    GUILayout.EndHorizontal();
	                    GUILayout.Space(2);
	                }
	            }
	        }

	        GUILayout.BeginVertical();
	        if (GUILayout.Button("Add New Translation"))
	        {
				Categories c = new Categories(selectedLanguageCategories, polyglot.languagesCategories[selectedLanguageCategories].ToString());
				for (int i = 0; i < polyglot.languages.Count; i++) {
					Translation element = new Translation(i, string.Format("Item Id {0}", i), "Translation here", string.Format("Element {0}", polyglot.translations.Count), c);
					polyglot.translations.Add(element);
				}
	        }
	        GUILayout.EndVertical();
	        GUILayout.EndVertical();
	    }

	    Translation ChangeIdAnotherLanguage(string idUniqueElements, string nameIDBrotherLanguage)
	    {
			foreach (Translation t in polyglot.translations)
	        {
	            if (t.indexLanguage != selectedLanguage)
	            {
	                if (selectedLanguageCategories == t.categories.index)
	                {
	                    if (t.idUniqueElements == idUniqueElements)
	                    {
	                        t.nameID = nameIDBrotherLanguage;
	                        return t;
	                    }
	                }
	            }
	        }
	        return null;
	    }

	    void ChangeLanguage()
	    {
			foreach (Translation t in polyglot.translations)
	        {
	            if (t.indexLanguage == selectedLanguage)
	            {
	                if(t.nameID == "Play Novo")
	                    GameObject.FindGameObjectWithTag("Respawn").GetComponent<Text>().text = t.translation;
	            }
	        }
	    }

	    void AddLanguage(string name)
	    {
			polyglot.languages.Add(name);
	    }

	    void AddCategories(string name)
	    {
			polyglot.languagesCategories.Add(name);
			selectedLanguageCategories = polyglot.languagesCategories.Count - 1;
	    }

	    void RemoveCategorie()
	    {
			foreach(Translation t in polyglot.translations.ToArray())
	        {
				string c = polyglot.languagesCategories[selectedLanguageCategories];
	            if (t.categories.name == c)
					polyglot.translations.Remove(t);
	        }
			polyglot.languagesCategories.RemoveAt(selectedLanguageCategories);
			selectedLanguageCategories = polyglot.languagesCategories.Count - 1;
	    }

	    void ChangeNewLanguage()
	    {
	        newLanguage = !newLanguage;
	    }

	    void ChangeNewCategories()
	    {
	        newCategories = !newCategories;
	    }

	    void SaveChanges()
		{
			polyglot.selectedLanguage = this.selectedLanguage;
			polyglot.selectedLanguageCategories = this.selectedLanguageCategories;
			AssetDatabase.CreateAsset(polyglot, this.GetSaveLocalPath());
	        AssetDatabase.SaveAssets();
	    }
	}
}
