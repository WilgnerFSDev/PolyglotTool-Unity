using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
/*
[System.Serializable]
public class Categories
{
    public int index;
    public string name = "";

    public Categories (int index, string name)
    {
        this.index = index;
        this.name = name;
    }

    public string List()
    {
        return "index: " + index + " | name: " + name;
    }
}

[System.Serializable]
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
*/
public class PolyglotWindow : EditorWindow {

    /*
     * Se me chamar de javista ta morto
     * <3
     * */
    public PolyglotSave polyglotSave;

    int selectedLanguage = 0;
    int selectedLanguageCategories = 0;

    // New Language
    bool newLanguage = false;
    bool editLanguage = false;
    string nameLanguage = "New Language";

    // New Categories
    bool newCategories = false;
    bool editCategories = false;
    string nameCategorie = "New Categorie";

    // list for languages working
    List<polyglotSave.> translations = new List<Translation>();
    
    List<string> languages = new List<string>();
    List<string> languagesCategories = new List<string>();

    string menu = "List";

    [MenuItem("Wilgner Studio/Polyglot Tool")]
    public static void ShowWindow()
    {
        GetWindow<PolyglotWindow>("Polyglot Tool");
    }

    private void OnGUI()
    {
        #region Languages List CRUD
        GUILayout.BeginVertical("Box");
        GUILayout.Label("Supported Languages: "+menu, EditorStyles.boldLabel);
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
            string buttonName = "Add";
            nameLanguage = GUILayout.TextField(nameLanguage);
            if (editLanguage == true)
                buttonName = "Confirm";
            if (GUILayout.Button(buttonName, GUILayout.MaxWidth(150)))
            {
                if (editLanguage == false)
                    AddLanguage(nameLanguage);
                else
                    languages[selectedLanguage] = nameLanguage;

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
            selectedLanguage = EditorGUILayout.Popup(selectedLanguage, languages.ToArray());
            if (GUILayout.Button("New", GUILayout.MaxWidth(150)))
            {
                ChangeNewLanguage();
                int count = languages.Count + 1;
                nameLanguage = "New Language "+count;
                editLanguage = false;
                menu = "New";
            }
            if (GUILayout.Button("Edit", GUILayout.MaxWidth(150)))
            {
                ChangeNewLanguage();
                nameLanguage = languages[selectedLanguage];
                editLanguage = true;
                menu = "Edit";

                AddElementsForTest();
            }
            if (GUILayout.Button("Delete", GUILayout.MaxWidth(150)))
            {
                if(EditorUtility.DisplayDialog("Delete Language", "Are you sure you want to delete the " + languages[selectedLanguage] + " language and all data together?", "Yes", "No")){
                    languages.RemoveAt(selectedLanguage);
                    selectedLanguage = languages.Count-1;
                }
            }
        }
        #endregion
    }

    void AddElementsForTest()
    {
        translations.Clear();
        languagesCategories.Clear();

        Categories c0 = new Categories(0, "Game");
        Categories c1 = new Categories(1, "Menu");
        languagesCategories.Add("Game");
        languagesCategories.Add("Menu");

        Translation button0_En = new Translation(0, "Play Button", "Play", "Element0", c0);
        Translation button0_Pt = new Translation(1, "Play Button", "Jogar", "Element0", c0);

        Translation button1_En = new Translation(0, "Exit Button", "Exit", "Element1", c1);
        Translation button1_Pt = new Translation(1, "Exit Button", "Sair", "Element1", c1);

        translations.Add(button0_En);
        translations.Add(button0_Pt);

        translations.Add(button1_En);
        translations.Add(button1_Pt);
    }

    void DrawCategoriesTranslations()
    {
        GUILayout.BeginHorizontal();
        #region Add/Edit Language
        if (newCategories == true)
        {
            string buttonName = "Add";
            nameCategorie = GUILayout.TextField(nameCategorie);
            if (editCategories == true)
                buttonName = "Confirm";
            if (GUILayout.Button(buttonName, GUILayout.MaxWidth(150)))
            {
                if (editCategories == false)
                    AddCategories(nameCategorie);
                else
                    languagesCategories[selectedLanguageCategories] = nameCategorie;

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
            selectedLanguageCategories = EditorGUILayout.Popup(selectedLanguageCategories, languagesCategories.ToArray());
            if (GUILayout.Button("New", GUILayout.MaxWidth(150)))
            {
                ChangeNewCategories();
                int count = languagesCategories.Count + 1;
                nameCategorie = "New Categorie " + count;
                editCategories = false;
                menu = "New";
            }
            if (GUILayout.Button("Edit", GUILayout.MaxWidth(150)))
            {
                ChangeNewCategories();
                nameCategorie = languagesCategories[selectedLanguageCategories];
                editCategories = true;
                menu = "Edit";

                AddElementsForTest();
            }
            if (GUILayout.Button("Delete", GUILayout.MaxWidth(150)))
            {
                if (EditorUtility.DisplayDialog("Delete Categories", "Are you sure you want to delete the " + languagesCategories[selectedLanguageCategories] + " categories ?", "Yes", "No"))
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

        int j = 0;
        foreach (Translation t in translations.ToArray())
        {
            if (t.indexLanguage == selectedLanguage)
            {
                if (selectedLanguageCategories == t.categories.index)
                {
                    float tam = position.width / 2;
                    GUILayout.BeginHorizontal("Box", GUILayout.MaxHeight(20));
                    t.nameID = GUILayout.TextField(t.nameID, GUILayout.MaxWidth(tam));
                    t.translation = GUILayout.TextField(t.translation, GUILayout.MaxWidth(tam));

                    Translation brotherElement = ChangeIdAnotherLanguage(t.idUniqueElements, t.nameID);

                    if (GUILayout.Button("X", GUILayout.MaxWidth(30)))
                    {
                        if (EditorUtility.DisplayDialog("Delete Translation", "Are you sure you want to delete the " + t.nameID + " ?", "Yes", "No"))
                        {
                            translations.Remove(t);

                            if (brotherElement != null)
                                translations.Remove(brotherElement);
                        }
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.Space(2);
                }
            }
            j++;
        }

        GUILayout.BeginVertical();
        if (GUILayout.Button("Add New Translation"))
        {
            Categories c = new Categories(selectedLanguageCategories, languagesCategories[selectedLanguageCategories].ToString());
            int i = 0;
            int e = 0;
            foreach (Translation t in translations) { e++; }
            foreach (string l in languages)
            {
                Translation element = new Translation(i, "Item Id "+i, "Translation here", "Element"+e, c);
                translations.Add(element);
                i++;
            }  
        }
        GUILayout.EndVertical();
        GUILayout.EndVertical();
    }

    Translation ChangeIdAnotherLanguage(string idUniqueElements, string nameIDBrotherLanguage)
    {
        foreach (Translation t in translations)
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
        foreach (Translation t in translations)
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
        languages.Add(name);
    }

    void AddCategories(string name)
    {
        languagesCategories.Add(name);
        selectedLanguageCategories = languagesCategories.Count - 1;
    }

    void RemoveCategorie()
    {
        foreach(Translation t in translations.ToArray())
        {
            string c = languagesCategories[selectedLanguageCategories];
            if (t.categories.name == c)
            {
                translations.Remove(t);
            }
        }
        languagesCategories.RemoveAt(selectedLanguageCategories);
        selectedLanguageCategories = languagesCategories.Count - 1;
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
        string nameFile ="myfile.asset";
        PolyglotSave polyglotSave = new PolyglotSave(selectedLanguage, selectedLanguageCategories, translations, languages, languagesCategories);
        AssetDatabase.CreateAsset(polyglotSave, "Assets/WS_USQLFramework/Resources/DataBase/"+nameFile);
        AssetDatabase.SaveAssets();
    }
}
