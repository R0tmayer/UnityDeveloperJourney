[System.Serializable]
public class PlayerProgressProvider
{
    public int crystalls;
    public int currentLevel;

    public PlayerProgressProvider()
    {
        crystalls = PlayerProgress.CRYSTALLS;
        currentLevel = PlayerProgress.CURRENT_LEVEL;
    }
}