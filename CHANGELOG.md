# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.0.0] / 2023-08-27
### Features
- `RevitBusyService` and `RevitBusyControl` to check the busy state of Revit.
### Updated
- Rename `Revit.Busy` to `ricaun.Revit.UI.Busy`.

## [0.2.2] / 2023-08-14
### Features
- Add `XmlnsPrefix` and `XmlnsDefinition` to `Revit.Busy` namespace.
### Updated
- Update references to work with net code.
- Clear main project and move files to `Example`

## [0.2.1] / 2023-03-04
### Features
- Update `net8.0-windows` version for Revit 2025
- Update to `countIdling` insted of using timestamp.

## [0.2.0] / 2023-11-18
### Features
- Create `net7.0-windows` version for Revit 2025

## [0.1.2] / 2023-02-22
### Features
- `RevitBusyService` - start busy
### Changed
- `RevitBusyService` default `isRevitBusy = true`

## [0.1.1] / 2022-12-12
### Changed
- Change timer busy to 1 second
### Fixed
- Fix `OnPropertyChanged` only if changed

## [0.1.0] / 2022-11-29
### Features
- Add `RevitBusyControl` - static Busy control
- Add `RevitBusyService`

[vNext]: ../../compare/1.0.0...HEAD
[1.0.0]: ../../compare/0.2.2...1.0.0
[0.2.2]: ../../compare/0.2.1...0.2.2
[0.2.1]: ../../compare/0.2.0...0.2.1
[0.2.0]: ../../compare/0.1.2...0.2.0
[0.1.2]: ../../compare/0.1.1...0.1.2
[0.1.1]: ../../compare/0.1.0...0.1.1
[0.1.0]: ../../compare/0.1.0