using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClassIsland.Core.Attributes;
using ClassIsland.ThemeLoader.Models;
using ClassIsland.ThemeLoader.Services;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;

namespace ClassIsland.ThemeLoader.Views;

/// <summary>
/// ThemesSettingsPage.xaml 的交互逻辑
/// </summary>
[SettingsPageInfo("classisland.themeLoader.theme", "主题", PackIconKind.FileCodeOutline, PackIconKind.FileCode)]
public partial class ThemesSettingsPage
{
    public XamlThemeService XamlThemeService { get; }

    public ThemesSettingsPage(XamlThemeService xamlThemeService)
    {
        XamlThemeService = xamlThemeService;
        InitializeComponent();
        DataContext = this;
    }

    private void ButtonLoadThemes_OnClick(object sender, RoutedEventArgs e)
    {
        XamlThemeService.LoadAllThemes();
    }

    [RelayCommand]
    private void OpenFolder(ThemeInfo info)
    {
        Process.Start(new ProcessStartInfo()
        {
            FileName = System.IO.Path.GetFullPath(info.Path),
            UseShellExecute = true
        });
    }

    [RelayCommand]
    private void ShowErrors(ThemeInfo info)
    {
        OpenDrawer("ErrorInfoDrawer", dataContext:info.Error?.ToString());
    }

    private void ButtonOpenThemeFolder_OnClick(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo()
        {
            FileName = XamlThemeService.ThemesPath,
            UseShellExecute = true
        });
    }
}