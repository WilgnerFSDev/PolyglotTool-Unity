using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Polyglot;

[CustomEditor(typeof(FindTranslation))]
public class FindTranslationEditor : Editor
{
    public SerializedProperty nameId;
    List<Translation> autoCompleteList = new List<Translation> ();

    private FindTranslation findTranslation;
    private SerializedObject script;

    public void OnEnable()
    {
        findTranslation = (FindTranslation)target;
        script = new SerializedObject(target);

        nameId = script.FindProperty ("nameId");
    }

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector ();

        script.Update();
        EditorGUI.BeginChangeCheck();

        this.findTranslation.polyglot = AssetDatabase.LoadAssetAtPath<PolyglotSave>(findTranslation.GetSaveLocalPath());

        EditorGUILayout.PropertyField(nameId);
        DrawSearch();

        if (EditorGUI.EndChangeCheck())
        {
            if (findTranslation.polyglot != null)
            {
                if (nameId != null)
                {
                    autoCompleteList = findTranslation.polyglot.AutoComplete(findTranslation.nameId);
                }
                else
                {
                    autoCompleteList.Clear();
                }
            }

            script.ApplyModifiedProperties();
        }
    }

    void DrawSearch()
    {
        GUILayout.BeginHorizontal("box");
        var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
        style.fontStyle = FontStyle.Bold;
        GUILayout.Label(string.Format("Auto Complete ({0})", autoCompleteList.Count), style, GUILayout.ExpandWidth(true));
        GUILayout.EndHorizontal();

        foreach(Translation t in autoCompleteList.ToArray())
        {
            if (GUILayout.Button(string.Format("{0} - {1}", t.nameID, t.categories.name)))
            {
                findTranslation.nameId = t.nameID;
                GUI.FocusControl(null);
            }

            if (t.nameID == findTranslation.nameId)
                autoCompleteList.Clear();
        }
    }
}