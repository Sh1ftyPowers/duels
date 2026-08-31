namespace Duels.Core
{
    public interface ISaveService
    {
        SaveData Load();

        void Save(SaveData saveData);
    }
}