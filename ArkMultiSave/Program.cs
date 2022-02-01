using ArkMultiSave;
using ArkMultiSave.Menus;

var config = Init.DataFolder();
var profiles = Init.GetProfiles();
var activeProfile = Init.GetActiveProfile(profiles);
IFactory factory = new Factory();
new MainMenu(activeProfile, profiles, config, factory).Run();