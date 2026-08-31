namespace Duels.Save
{
    public interface ISaveService
    {
        SaveData Load();

        void Save(SaveData saveData);
    }
}