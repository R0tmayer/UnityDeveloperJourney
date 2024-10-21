using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerProgress : MonoBehaviour
{
    public static int CRYSTALLS;
    public static int CURRENT_LEVEL;

    private void Awake()
    {
        LoadData();

        var scene = SceneManager.GetActiveScene();

        if (scene.buildIndex == CURRENT_LEVEL)
        {
            return;
        }
        
        SceneManager.LoadSceneAsync(CURRENT_LEVEL);
    }

    public static void SaveData()
    {
        SaveSystem.SavePlayer();
    }

    public void LoadData()
    {
        PlayerProgressProvider data = SaveSystem.LoadPlayer();

        if (data == null)
        {
            SaveData();
        }
        else
        {
            CRYSTALLS = data.crystalls;
            CURRENT_LEVEL = data.currentLevel;
        }
    }
}
