using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data" , menuName = "level Data" , order = 1)]
public class LevelDataSO : ScriptableObject
{
    [SerializeField] private List<LevelSO> levels = new List<LevelSO>();
    [SerializeField] private LevelSO getLevel;
    [SerializeField] private int currentLevel;

    public int CurrentLevel => currentLevel;
    public void GetLevel(string levelName , Transform pos)
    {
        getLevel = levels.FirstOrDefault(x => x.levelName == levelName);
        if (getLevel != null)
        {
            GameObject LevelSelect = Instantiate(getLevel.levelPrefabs, pos.position, Quaternion.identity , pos);
        }
    }

    public void NextLevel(Transform pos)
    {
        if (getLevel == null) return;

        currentLevel = levels.IndexOf(getLevel);
        currentLevel++;
        getLevel = levels[currentLevel];
        PlayerPrefs.SetInt("Level" , currentLevel);
        
        if(getLevel != null)
        {
            GameObject levelSelect = Instantiate(getLevel.levelPrefabs, pos.position , Quaternion.identity , pos);
        }
    }

    public void LevelCompelete()
    {
        if (getLevel == null) return;

        getLevel.CompeleteLevel();

    }
}
