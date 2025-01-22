using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ClassIsland.Core;
using ClassIsland.ThemeLoader.Models;
using ClassIsland.ThemeLoader.Views;
using ControlzEx.Theming;
using Microsoft.Extensions.Logging;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ClassIsland.ThemeLoader.Services;

public class XamlThemeService
{
    public ILogger<XamlThemeService> Logger { get; }
    public Plugin Plugin { get; }
    private ResourceDictionary RootResourceDictionary { get; } = new();

    private Window MainWindow { get; } = AppBase.Current.MainWindow!;

    public string ThemesPath => Path.Combine(Plugin.PluginConfigFolder, "Themes");

    public ObservableCollection<ThemeInfo> Themes { get; } = [];

    public XamlThemeService(ILogger<XamlThemeService> logger, Plugin plugin)
    {
        Logger = logger;
        Plugin = plugin;
        var resourceBoarder = VisualTreeUtils.FindChildVisualByName<Border>(MainWindow, "ResourceLoaderBorder");
        if (!Directory.Exists(ThemesPath))
        {
            Directory.CreateDirectory(ThemesPath);
        }
        resourceBoarder?.Resources.MergedDictionaries.Add(RootResourceDictionary);
        LoadAllThemes();
    }

    public void LoadAllThemes()
    {
        RootResourceDictionary.MergedDictionaries.Clear();
        Themes.Clear();
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .IgnoreUnmatchedProperties()
            .Build();
        foreach (var i in Directory.GetDirectories(ThemesPath))
        {
            var themeInfo = new ThemeInfo
            {
                Name = Path.GetFileName(i),
                Path = Path.GetFullPath(i)
            };
            try
            {
                if (File.Exists(Path.Combine(i, "manifest.yml")))
                {
                    var yaml = File.ReadAllText(Path.Combine(i, "manifest.yml"));
                    themeInfo = deserializer.Deserialize<ThemeInfo>(yaml);
                }

                themeInfo.Path = Path.GetFullPath(i);
                if (themeInfo.IsEnabled)
                {
                    LoadTheme(Path.Combine(i, "Theme.xaml"));
                    themeInfo.IsLoaded = true;
                }
            }
            catch (Exception e)
            {
                themeInfo.IsError = true;
                themeInfo.Error = e;
                Logger.LogError(e, "无法加载主题 {}", i);
            }
            Themes.Add(themeInfo);
        }
    }

    public void LoadTheme(string themePath)
    {
        Logger.LogInformation("正在加载主题 {}", themePath);
        var themeResourceDictionary = new ResourceDictionary
        {
            Source = new Uri(Path.GetFullPath(themePath))
        };
        RootResourceDictionary.MergedDictionaries.Add(themeResourceDictionary);
    }
}