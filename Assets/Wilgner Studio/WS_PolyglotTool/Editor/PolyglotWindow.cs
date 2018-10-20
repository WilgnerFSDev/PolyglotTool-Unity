using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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

public class PolyglotWindow : EditorWindow {

    // mostra(0) > mostraCategoria(0) 

    int selectedLanguage = 0;
    int selectedLanguageCategories = 0;

    bool newLanguage = false;
    bool editLanguage = false;
    string nameLanguage = "New Language";

    // list for languages working
    //Dictionary<string, string> languagesSC = new Dictionary<string, string>();
    List<Translation> lista = new List<Translation>();
    

    List<string> languages = new List<string>();
    List<string> languagesCategories = new List<string>();
    List<string> languagesSubCategories = new List<string>();

    List<List<string>> languagesTranslation = new List<List<string>>();

    List<List<List<string>>> languages2 = new List<List<List<string>>>();

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

        #region Language Categories
        /*
        GUILayout.BeginVertical("Box");
        GUILayout.Label("Categories", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        DrawCategories();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        */
        #endregion

        #region Language Element
        GUILayout.BeginVertical("Box");
        GUILayout.Label("Categorioes", EditorStyles.boldLabel);
        DrawCategories();
        GUILayout.EndVertical();
        #endregion
    }

    void ListLanguages()
    {
        int i = 0;
        foreach(string l in languages)
        {
            Debug.Log(l);
            int j = 0;
            foreach (string lc in languagesCategories)
            {
                Debug.Log(lc);
                int k = 0;
                foreach (string lsc in languagesSubCategories)
                {
                    Debug.Log(lsc);
                    k++;
                }
                j++;
            }
            i++;
        }
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
            if (GUILayout.Button("Add New Language", GUILayout.MaxWidth(150)))
            {
                ChangeNewLanguage();
                int count = languages.Count + 1;
                nameLanguage = "New Language "+count;
                editLanguage = false;
                menu = "New";
            }
            if (GUILayout.Button("Edit Name", GUILayout.MaxWidth(150)))
            {
                ChangeNewLanguage();
                nameLanguage = languages[selectedLanguage];
                editLanguage = true;
                menu = "Edit";

                AddElementsForTest();
            }
            if (GUILayout.Button("Delete Language", GUILayout.MaxWidth(150)))
            {
                if(EditorUtility.DisplayDialog("Delete Language", "Are you sure you want to delete the " + languages[selectedLanguage] + " language and all data together?", "Yes", "No")){
                    languages.RemoveAt(selectedLanguage);
                    selectedLanguage = languages.Count-1;
                }
            }
            if(GUILayout.Button("Test Language", GUILayout.MaxWidth(150)))
            {
                ChangeLanguage();
            }
        }
        #endregion
    }

    void AddElementsForTest()
    {
        lista.Clear();
        languagesCategories.Clear();

        Categories c0 = new Categories(0, "Game");
        Categories c1 = new Categories(1, "Menu");
        languagesCategories.Add("Game");
        languagesCategories.Add("Menu");

        Translation button0_En = new Translation(0, "Play Button", "Play", "Element0", c0);
        Translation button0_Pt = new Translation(1, "Play Button", "Jogar", "Element0", c0);

        Translation button1_En = new Translation(0, "Exit Button", "Exit", "Element1", c1);
        Translation button1_Pt = new Translation(1, "Exit Button", "Sair", "Element1", c1);

        lista.Add(button0_En);
        lista.Add(button0_Pt);

        lista.Add(button1_En);
        lista.Add(button1_Pt);
    }

    void DrawCategories()
    {
        selectedLanguageCategories = EditorGUILayout.Popup(selectedLanguageCategories, languagesCategories.ToArray());
        GUILayout.BeginVertical("Box");
        GUILayout.Label("Translation", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Name ID");
        GUILayout.Label("Translation");
        GUILayout.EndHorizontal();

        foreach (Translation t in lista)
        {
            if (t.indexLanguage == selectedLanguage)
            {
                if (selectedLanguageCategories == t.categories.index)
                {
                    GUILayout.BeginHorizontal();
                    t.nameID = GUILayout.TextField(t.nameID);
                    t.translation = GUILayout.TextField(t.translation);
                    GUILayout.EndHorizontal();

                    ChangeIdAnotherLanguage(t.idUniqueElements, t.nameID);
                }
            }
        }

        GUILayout.BeginVertical();
        if (GUILayout.Button("Add New Translation"))
        {
            Categories c = new Categories(selectedLanguageCategories, languagesCategories[selectedLanguageCategories].ToString());
            int i = 0;
            int e = 0;
            foreach (Translation t in lista) { e++; }
            foreach (string l in languages)
            {
                Translation element = new Translation(i, "Item Id "+i, "Translation here", "Element"+e, c);
                lista.Add(element);
                i++;
            }  
        }
        GUILayout.EndVertical();
        GUILayout.EndVertical();
    }

    void ChangeIdAnotherLanguage(string idUniqueElements, string nameIDBrotherLanguage)
    {
        foreach (Translation t in lista)
            if (t.indexLanguage != selectedLanguage)
                if (selectedLanguageCategories == t.categories.index)
                    if (t.idUniqueElements == idUniqueElements)
                        t.nameID = nameIDBrotherLanguage;
    }

    void ChangeLanguage()
    {
        foreach (Translation t in lista)
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

    void ChangeNewLanguage()
    {
        newLanguage = !newLanguage;
    }
}
