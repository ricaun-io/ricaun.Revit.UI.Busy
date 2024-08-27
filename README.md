# ricaun.Revit.UI.Busy

[![Revit 2017](https://img.shields.io/badge/Revit-2017+-blue.svg)](https://github.com/ricaun-io/ricaun.Revit.UI.Busy)
[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](https://github.com/ricaun-io/ricaun.Revit.UI.Busy)
[![Nuke](https://img.shields.io/badge/Nuke-Build-blue)](https://nuke.build/)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Build](https://github.com/ricaun-io/ricaun.Revit.UI.Busy/actions/workflows/Build.yml/badge.svg)](https://github.com/ricaun-io/ricaun.Revit.UI.Busy/actions)

`ricaun.Revit.UI.Busy` package provides a static control to manage the busy state of Revit using the `Idling` event.

This project was generated by the [ricaun.AppLoader](https://ricaun.com/AppLoader/) Revit plugin.

## Exemple

### Initialize
Static initialization of the `ricaun.Revit.UI.Busy` control.
```c#
RevitBusyControl.Initialize(application);
```

### IsRevitBusy
To check if Revit is busy, use the `IsRevitBusy` property.
```c#
bool isRevitBuzy = RevitBusyControl.Control.IsRevitBusy;
```

### Binding

Binding to the `IsRevitBusy` property.
```xml
xmlns:busy="http://schemas.revit.busy.com/2024/xaml/presentation"
```
```xml
{Binding IsRevitBusy, Source={x:Static busy:RevitBusyControl.Control}}
```

## License

This project is [licensed](LICENSE) under the [MIT License](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](https://github.com/ricaun-io/ricaun.Revit.UI.Busy/stargazers)!