using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Polyglot;

public class LanguageControl : MonoBehaviour {

    public PolyglotSave polyglot;
    public UnityEvent LanguageChanged = new UnityEvent();
    [SerializeField]
    private int selectedLanguage;
    public int previousSelectedLanguage = -1;

    // Use this for initialization
    void Start () {
        LanguageChanged.AddListener(UpdateTextTranslation);
    }
	
	// Update is called once per frame
	void Update () {
        if (selectedLanguage != previousSelectedLanguage)
            LanguageChanged.Invoke();
    }

    public void UpdateTextTranslation()
    {
        FindTranslation[] findTranslation = GameObject.FindObjectsOfType<FindTranslation> ();
        for(int i=0; i < findTranslation.Length; i++)
        {
            findTranslation[i].SetText();
        }
        previousSelectedLanguage = selectedLanguage;
    }

    public string GetSaveLocalPath()
    {
        return "Assets/Wilgner Studio/PolyglotTool/DataBase/Polyglot.asset";
    }

    public int GetSelectedLanguage() {
        return this.selectedLanguage;
    }

    public void SetSelectedLanguage(int value) {
        this.selectedLanguage = value;
    }
}
