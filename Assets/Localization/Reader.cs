using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.HostingServices;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class Language
{
    public string lang;
    public string title;
    public string play;
    public string quit;
    public string options;
    public string credits;
}

public class LanguageData
{
    public Language[] languages;
}

public class Reader : MonoBehaviour
{
    private bool isCurrentLangEng;

    public Text buttonText;

    public TextAsset json;
    public string currentLanguage;
    private LanguageData languageData;

    public Text titleText;
    public Text playText;
    public Text optionsText;
    public Text quitText;
    public Text creditsText;

    private void Start()
    {
        isCurrentLangEng = true;
        buttonText.text = "English";
        languageData = JsonUtility.FromJson<LanguageData>(json.text);
        SetLanguage(currentLanguage);
    }

    public void SwapLanguage()
    {
        if (isCurrentLangEng)
        {
            isCurrentLangEng = false;
            buttonText.text = "Français";
            SetLanguage("fr");
        }
        else
        {
            isCurrentLangEng = true;
            buttonText.text = "English";
            SetLanguage("en");
        }
    }

    public void SetLanguage(string newLanguage)
    {
        foreach (Language lang in languageData.languages)
        {
            if (lang.lang.ToLower() == newLanguage.ToLower())
            {
                titleText.text = lang.title;
                playText.text = lang.play;
                optionsText.text = lang.options;
                quitText.text = lang.quit;
                creditsText.text = lang.credits;
                return;
            }
        }
    }
}
