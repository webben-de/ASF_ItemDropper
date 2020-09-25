# ASF Item Dropper

# DISCLAIMER

This plugin is provided on AS-IS basis, without any guarantee at all. Author is not responsible for any harm, direct or indirect, that may be caused by using this plugin. You use this plugin at your own risk.

## Introduction

This plugin for [ASF](https://github.com/JustArchiNET/ArchiSteamFarm/) allows you to view, set and reset achievements in steam games, similar to [SAM](https://github.com/gibbed/SteamAchievementManager). Works only with ASF v4.0+ (make sure to check actual required version in release notes).

## Installation

- download .zip file from [latest release](https://github.com/webben-de/ASF_ItemDropper/releases/latest), in most cases you need `ASF-ItemDropper.zip`, but if you use ASF-generic-netf.zip (you really need a strong reason to do that) download `ASF-ItemDropper-netf.zip`.
- unpack downloaded .zip file to `plugins` folder inside your ASF folder.
- (re)start ASF, you should get a message indicating that plugin loaded successfully.

## Usage

After installation, you can use those commands (only for accounts with Master+ permissions):

### `istart <bots> <appids>`

### `istop`

### `icheck <bots> <appids>`

Displays current status of achievements in specified games on given bots. You can specify multiple bots and multiple appids.<br/>
Example of output:<br/>
![alist output example](https://i.imgur.com/IiRnH81.png)<br/>
Unlocked achievements are marked as ✅, still locked as ❌. If achievement has mark ⚠️ next to it - it means this achievement is server-side, and can't be switched with this plugin.<br/>
Examples:<br/>
`alist bot1 370910,730`<br/>
`alist bot1,bot2 370910`
