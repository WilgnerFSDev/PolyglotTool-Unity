using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Polyglot;

public class FindTranslation : MonoBehaviour {

    public string nameId;
    public Text text;
    public PolyglotSave polyglot;
    public List<Translation> searchTranslations;

    private LanguageControl lc;

    // Use this for initialization
    void Start () {
        this.text = this.GetComponent<Text>();
        this.lc = GameObject.FindObjectOfType<LanguageControl>();
        if (polyglot == null)
            Debug.Log("Polyglot is null!");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public string GetSaveLocalPath()
    {
        return "Assets/Resources/Polyglot.asset";
    }

    public void SetText()
    {
        Translation t = polyglot.GetTranslationByName(nameId, lc.selectedLanguage);
        if (t != null)
            this.text.text = t.translation;
    }
}
