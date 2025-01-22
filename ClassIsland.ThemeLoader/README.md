# ClassIsland 主题加载器

本插件支持向 ClassIsland 主界面注入自定义的 XAML 资源。

## 开始使用

首先，检查您的设备满足以下条件：

- ClassIsland 1.5.3.1 或以上版本

如果您的设备满足以上需求，可以按照以下步骤安装本插件：

1. 在插件市场下载并安装插件
2. 打开[【应用设置】->【主题】](classisland://app/settings)页面，即可开始使用本插件。

## 安装主题

主题一般存储在`应用数据目录\Config\Plugins\classIsland.themeLoader\Themes`目录下，您可以在主题设置页面点击【打开主题文件夹】按钮打开本目录。您可以将主题文件解压到这个目录下。

您可以先从这些[示例主题](https://github.com/ClassIsland/ClassIsland.ThemeLoader/blob/master/examples)开始，也可以在这些主题的基础上进行进一步的修改，满足您的个性化需要。

## 制作主题

一个主题通常具有以下的目录结构：

``` plaintext
你的主题包
  - Theme.xaml      # 主题资源文件
  - manifest.xaml   # 主题清单文件（可选）
  - 其它文件…
```

其中 Theme.xaml 是存储您要注入的资源的文件。Theme.xaml 的顶级元素是 `ResourceDictionary`，存储了您要注入的资源和样式。您可以直接将下面的内容复制到这个文件中以开始：

``` xml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                    >
</ResourceDictionary>
```

## 致谢

本项目使用了以下第三方库：

- [ClassIsland.PluginSdk](https://github.com/ClassIsland/ClassIsland)

## 许可证

本项目基于 [MIT License](https://github.com/ClassIsland/ClassIsland.ThemeLoader/blob/master/LICENSE.txt) 许可。
