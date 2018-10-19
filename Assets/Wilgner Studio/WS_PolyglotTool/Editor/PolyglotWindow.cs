using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PolyglotWindow : EditorWindow {

    int selectedLanguage = 0;
    bool newLanguage = false;
    string nameLanguage = "New Language";
    List<string> languages = new List<string>();

    [MenuItem("Wilgner Studio/Polyglot Tool")]
    public static void ShowWindow()
    {
        GetWindow<PolyglotWindow>("Polyglot Tool");
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical("Box");
        GUILayout.Label("Supported Languages");
        GUILayout.BeginHorizontal();
        DrawLanguage();
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }

    void DrawLanguage()
    {
        if (newLanguage == true)
        {
            nameLanguage = GUILayout.TextField(nameLanguage);
            if (GUILayout.Button("Add", GUILayout.MaxWidth(150)))
            {
                AddLanguage(nameLanguage);
                ChangeNewLanguage();

            }
            if (GUILayout.Button("Cancel", GUILayout.MaxWidth(150)))
            {
                ChangeNewLanguage();
            }
        }
        else
        {
            selectedLanguage = EditorGUILayout.Popup(selectedLanguage, languages.ToArray());
            if (GUILayout.Button("Add New Language", GUILayout.MaxWidth(150)))
            {
                ChangeNewLanguage();
                nameLanguage = "New Language "+languages.Count;
            }
            if (GUILayout.Button("Delete Language", GUILayout.MaxWidth(150)))
            {
                if(EditorUtility.DisplayDialog("Delete Language", "Are you sure you want to delete the " + languages[selectedLanguage] + " language and all data together?", "Yes", "No")){
                    languages.RemoveAt(selectedLanguage);
                    selectedLanguage = languages.Count-1;
                }
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
