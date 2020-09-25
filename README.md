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

### `idrop <bots>? <appid> <itemids>`

Triggers an item drop on one or more bots

- `idrop bot1,bot2,bot3 1234 122314` trigger on the passed bot names the item trigger
- `ìdrop 1234 122314` triggers on the bot which enter the command
