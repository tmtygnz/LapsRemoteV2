# Laps Remote 2 [![Laps Remote Stats](https://circleci.com/gh/jostimian/LapsRemoteV2.svg?style=svg)](https://circleci.com/gh/jostimian/LapsRemoteV2) [![](https://tokei.rs/b1/github/jostimian/LapsRemoteV2)](https://github.com/jostimian/LapsRemoteV2)


<img width="100" height="100" src="./img/newico.ico" align ="right">

Laps Remote 2 is a complete rewrite of Laps Remote to a newer framework.
Laps Remote is a project that will try to eliminate cross contamination among humans
by using telemedice

## Currently Working On
- [x] Implementing MVVM
- [x] Adding Vitals Monitor
- [x] Vital Recorder
- [x] Vital Record Reader
- [ ] Adding video and audio check up call [in progress]
- [ ] Better User Interface

## Build And Installation
**Note**
If you are using older version of windows (Older than windows 10/11) some ui components might not work properly.

**Prerequisite**
- [Dotnet 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
- Visual Studio 2019 with .`NET desktop development` installed
- [Inno Setup](https://jrsoftware.org/isinfo.php) (Optional)

**Steps**
- Clone The Repository with `https://github.com/jostimian/LapsRemoteV2.git`
- Run The `build.ps1` script with powershell. You can also use Visual Studio to build the application

Use this command in powershell to clone and build the project.
```bash
git clone https://github.com/jostimian/LapsRemoteV2.git; .\LapsRemoteV2\build.ps1
```
## Credits
- [Mah Apps Metro](https://github.com/MahApps/MahApps.Metro) A toolkit for creating modern WPF applications.
- [Mah Apps Metro Icons](https://github.com/MahApps/MahApps.Metro.IconPacks) Awesome icon packs for WPF and UWP in one library.
- [Newtonsoft Json](https://github.com/JamesNK/Newtonsoft.Json) high-performance JSON framework for .NET.
- [WPF Extended Toolkit](https://github.com/xceedsoftware/wpftoolkit) All the controls missing in WPF.
- [Live Charts](https://github.com/Live-Charts/Live-Charts) Simple, flexible, interactive & powerful charts, maps and gauges for .Net.
- [Prism](https://github.com/PrismLibrary/Prism) Prism is a framework for building loosely coupled, maintainable, and testable XAML applications in WPF, Xamarin Forms, and Uno / Win UI Applications.
- [Xaml Behaviors](https://github.com/microsoft/XamlBehaviorsWpf) Easily add interactivity to your apps using XAML Behaviors for WPF.
