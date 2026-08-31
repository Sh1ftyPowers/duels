using UnityEngine;

namespace Duels.Save
{
    public class SaveService : ISaveService
    {
        private const string SaveKey = "PlayerSave";

        public SaveData Load()
        {
            if (!PlayerPrefs.HasKey(SaveKey))
            {
                Debug.Log("LOAD: Save not found");
                return null;
            }

            string json = PlayerPrefs.GetString(SaveKey);

            Debug.Log($"LOAD: {json}");

            return JsonUtility.FromJson<SaveData>(json);
        }

        public void Save(SaveData saveData)
        {
            string json = JsonUtility.ToJson(saveData);

            Debug.Log($"SAVE: {json}");

            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }
    }
}