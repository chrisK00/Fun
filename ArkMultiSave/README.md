
## V 1.0.0

[![wtgif.gif](https://i.postimg.cc/dtqhB1sL/sf-UHAgoi-WT.gif)](https://postimg.cc/py6VVPnH)

[![wtimg.png](https://i.postimg.cc/wTshC3KF/Windows-Terminal-J61-GEA9-CCK.png)](https://postimg.cc/WhTFrN1J)

## Why?
In Ark you can only have one game per map and there is no way to switch between different ones. That means you cannot have two saves that are on TheIsland, even if you just wanted to try something out. If you like to play alone in one world, with one of your friends in another and another world with another friend then you have to manually rename multiple folders everytime. I created this tool so that I can quickly switch between worlds/create new ones and I hope someone else also finds it usefull.

## How to start
1. You need to get the full folder path to where Ark stores all your saves which is usually: `C:\SteamLibrary\steamapps\common\ARK\ShooterGame\Saved`
2. When you start the tool for the first time it will create a config file and profiles file inside of `%AppData%` in a folder called ArkMultiSave. Then it prompts you to enter the path mentioned above. You can always directly edit the config files otherwise and it's mentioned how below.
3. After that you will enter a name for your first profile and it auto adds TheIsland to it.
4. If you are playing SinglePLayer now but your friend asks you to play with them you can simply switch to that profile by selecting "Load Profile" or New Profile to create one

## How to install as a CLI tool
1. Open your terminal while inside the project folder and Run `dotnet pack`
2. Install the tool globally `dotnet tool install -g --add-source ./nupkg ArkMultiSave`
3. Now you can run it from the terminal anytime by typing `ark`

## Configuration files
1. Head to `YourDrive\Users\YourUsername\AppData\Roaming` and create/open a folder called ArkMultiSave
2. Create a `config.json` and a `profiles.json` file
3. The config currently only holds the path to where the ark saves should exist but later on I might add the ability to add default maps that will always be added to a new profile (currently TheIsland aka SavedArksLocal is always added) if wanted. You need to escape the backslashes like this `\\`

```
{
  "SavedFolderPath": "C:\\SteamLibrary\\steamapps\\common\\ARK\\ShooterGame\\Saved"
}
```

4. The profiles.json file should have a list of profiles (objects). If you manually add a map like Valguero you have to add SavedArksLocal to the end. You can also set a profile to become the active one which you are currently playing on. Remember that there can only be one active profile

```
[
  {
    "Name": "SE",
    "GameFolders": [
      "SavedArksLocal",
      "RagnarokSavedArksLocal"
    ],
    "IsActive": true
  },
  {
    "Name": "Multiplayer1",
    "GameFolders": [
      "SavedArksLocal"
    ],
    "IsActive": false
  }
]
```

## Remove existing tool
`dotnet tool uninstall packageName -g`

## List tools
`dotnet tool list -g`

## Tool/Ark Info
- SavedArksLocal is the default folder and contains TheIsland
- SaveGames contains your mod configurations for all maps (f.e DinoTracker, Waypoints...)
- A profile is like an environment (f.e SinglePLayer, Friends, Test). A profile can have many maps but only one of each. (TheIsland, Ragnarok, Valguero....)
- ConsoleTables and SharPrompt are used for the UI part of the app
