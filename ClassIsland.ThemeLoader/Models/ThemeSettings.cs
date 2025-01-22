using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using YamlDotNet.Serialization;

namespace ClassIsland.ThemeLoader.Models;

public class ThemeInfo : ObservableRecipient
{
    private string _name = "";
    private string _author = "";
    private string _url = "";
    private bool _isLoaded = false;
    private bool _isError = false;
    private Exception? _error;
    private string _path = "";

    public string Name
    {
        get => _name;
        set
        {
            if (value == _name) return;
            _name = value;
            OnPropertyChanged();
        }
    }

    public string Author
    {
        get => _author;
        set
        {
            if (value == _author) return;
            _author = value;
            OnPropertyChanged();
        }
    }

    public string Url
    {
        get => _url;
        set
        {
            if (value == _url) return;
            _url = value;
            OnPropertyChanged();
        }
    }

    [YamlIgnore]
    public string Path
    {
        get => _path;
        set
        {
            if (value == _path) return;
            _path = value;
            OnPropertyChanged();
        }
    }

    [YamlIgnore]
    public bool IsLoaded
    {
        get => _isLoaded;
        set
        {
            if (value == _isLoaded) return;
            _isLoaded = value;
            OnPropertyChanged();
        }
    }

    [YamlIgnore]
    public bool IsError
    {
        get => _isError;
        set
        {
            if (value == _isError) return;
            _isError = value;
            OnPropertyChanged();
        }
    }

    [YamlIgnore]
    public Exception? Error
    {
        get => _error;
        set
        {
            if (Equals(value, _error)) return;
            _error = value;
            OnPropertyChanged();
        }
    }

    [YamlIgnore]
    public bool IsEnabled
    {
        get => File.Exists(System.IO.Path.Combine(Path, ".enabled"));
        set
        {
            try
            {
                if (value)
                {
                    File.WriteAllText(System.IO.Path.Combine(Path, ".enabled"), "");
                }
                else
                {
                    File.Delete(System.IO.Path.Combine(Path, ".enabled"));
                }
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}