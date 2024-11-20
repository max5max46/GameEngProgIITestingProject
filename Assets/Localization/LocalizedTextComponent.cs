using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocalizedTextComponent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string tableReference;
    [SerializeField] private string localizationKey;

    private LocalizedString localizedStr;
    public Text textComponent;

    void Start()
    {
        textComponent = GetComponent<Text>();

        localizedStr = new LocalizedString { TableReference = tableReference, TableEntryReference = localizationKey };

        LocalizationSettings.SelectedLocaleChanged += UpdateText;

        //var frenchLocale = LocalizationSettings.AvailableLocales.GetLocale("fr");

        //LocalizationSettings.SelectedLocale = frenchLocale;

        //UpdateText(frenchLocale);
    }

    private void OnDestroy()
    {
        LocalizationSettings.SelectedLocaleChanged -= UpdateText;
    }

    // Update is called once per frame
    void UpdateText(Locale locale)
    {
        textComponent.text = localizedStr.GetLocalizedString();
    }
}
