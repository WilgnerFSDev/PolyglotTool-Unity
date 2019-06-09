using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Polyglot;
#if TMP
using TMPro;
#endif

public class FindTranslation : MonoBehaviour {

    public string nameId;
    public Text text;
#if TMP
    public TMP_Text textP;
#endif
    public PolyglotSave polyglot;
    public List<Translation> searchTranslations;

    private LanguageControl lc;

    // Use this for initialization
    void Start () {
        this.text = this.GetComponent<Text>();

#if TMP
        this.textP = this.GetComponent<TMP_Text>();
#endif

        this.lc = GameObject.FindObjectOfType<LanguageControl>();
        if (polyglot == null)
            Debug.Log("Polyglot is null!");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public string GetSaveLocalPath()
    {
        return "Assets/Wilgner Studio/PolyglotTool/DataBase/Polyglot.asset";
    }

    public void SetText()
    {
        Translation t = polyglot.GetTranslationByName(nameId, lc.GetSelectedLanguage());
        if (t != null)
        {
#if TMP
            if(textP != null)
                this.textP.text = t.translation;
#endif

            if (text != null)
                this.text.text = t.translation;
        }
    }
}
