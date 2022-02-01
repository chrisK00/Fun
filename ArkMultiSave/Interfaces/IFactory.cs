namespace ArkMultiSave.Interfaces;
public interface IFactory
{
    ISaveService GetSaveService();
    ISaveFacade GetSaveFacade(Profile profile, Config config);
}
