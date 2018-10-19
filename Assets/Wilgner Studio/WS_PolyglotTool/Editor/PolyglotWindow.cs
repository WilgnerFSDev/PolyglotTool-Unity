using UnityEditor;
using UnityEngine;

public class PolyglotWindow : EditorWindow {

    [MenuItem("Wilgner Studio/Polyglot Tool")]
    public static void ShowWindow()
    {
        GetWindow<PolyglotWindow>("Polyglot Tool");
    }

    private void OnGUI()
    {
        
    }
}
