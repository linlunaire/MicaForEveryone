using MicaForEveryone.CoreUI;
using MicaForEveryone.Models;
using Microsoft.Windows.ApplicationModel.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Globalization;

namespace MicaForEveryone.App.Services;

public sealed class LocalizationService : ILocalizationService
{
    private readonly ResourceLoader resourceLoader = new();

    public IReadOnlyList<string> SupportedLanguages => ApplicationLanguages.ManifestLanguages;

    public string CurrentLanguage => ApplicationLanguages.PrimaryLanguageOverride;

    public void SetLanguage(string languageTag)
    {
        if (languageTag.Length != 0 && !SupportedLanguages.Contains(languageTag, StringComparer.OrdinalIgnoreCase))
            throw new ArgumentException("Unsupported language.", nameof(languageTag));

        ApplicationLanguages.PrimaryLanguageOverride = languageTag;
    }

    public string GetLocalizedBackdropType(BackdropType backdropType)
    {
        return backdropType switch
        {
            BackdropType.Default => GetLocalizedString("DefaultBackdropName"),
            BackdropType.None => GetLocalizedString("NoneBackdropName"),
            BackdropType.Mica => GetLocalizedString("MicaBackdropName"),
            BackdropType.Acrylic => GetLocalizedString("AcrylicBackdropName"),
            BackdropType.MicaAlt => GetLocalizedString("MicaAltBackdropName"),
            _ => throw new ArgumentException("Invalid backdrop type.", nameof(backdropType)),
        };
    }

    public string GetLocalizedCornerPreference(CornerPreference cornerPreference)
    {
        return cornerPreference switch {
            CornerPreference.Default => GetLocalizedString("DefaultCornerPreference"),
            CornerPreference.RoundedSmall => GetLocalizedString("RoundedSmallCornerPreference"),
            CornerPreference.Rounded => GetLocalizedString("RoundedCornerPreference"),
            CornerPreference.Square => GetLocalizedString("SquareCornerPreference"),
            _ => throw new ArgumentException("Invalid corner preference.", nameof(cornerPreference)),
        };
    }

    public string GetLocalizedString(string key)
    {
        return resourceLoader.GetString(key);
    }

    public string GetLocalizedTitleBarColor(TitleBarColorMode titleBarColorMode)
    {
        return titleBarColorMode switch {
            TitleBarColorMode.Default => GetLocalizedString("DefaultTitleBarColorMode"),
            TitleBarColorMode.Light => GetLocalizedString("LightTitleBarColorMode"),
            TitleBarColorMode.Dark => GetLocalizedString("DarkTitleBarColorMode"),
            TitleBarColorMode.System => GetLocalizedString("SystemTitleBarColorMode"),
            TitleBarColorMode.Custom => GetLocalizedString("CustomTitleBarColorMode"),
            _ => throw new ArgumentException("Invalid title bar color mode.", nameof(titleBarColorMode)),
        };
    }

    public string GetRuleName(Rule rule)
    {
        if (rule is GlobalRule)
            return GetLocalizedString("GlobalRuleName");
        if (rule is ProcessRule processRule)
            return processRule.ProcessName;
        if (rule is ClassRule classRule)
            return classRule.ClassName;
        throw new ArgumentException("Invalid rule type.", nameof(rule));
    }
}
