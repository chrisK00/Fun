using ArkMultiSave.Services;

namespace ArkMultiSave.Factories;
public class Factory : IFactory
{
    public ISaveService GetSaveService() => new SaveService();
    public ISaveFacade GetSaveFacade(Profile profile, Config config) => new SaveFacade(profile, config, GetSaveService());
}
