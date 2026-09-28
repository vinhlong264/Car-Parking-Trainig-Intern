using Unity.VisualScripting;
using UnityEngine;

public class SpawnLevel : MonoBehaviour
{
    [SerializeField] private LevelDataSO levelData;
    [SerializeField] private StringEvent eventData;
    [SerializeField] private string levelName;

    private void OnEnable()
    {
        levelName = eventData.LevelName;
        GameManager.Instance.OnYouWIn += YouWinHandler;
    }

    void Start()
    {
        levelData.GetLevel(levelName, this.transform);
    }

    private void YouWinHandler()
    {
        levelData.LevelCompelete();
        PlayerPrefs.SetInt("LevelGame" , levelData.CurrentLevel);
    }

    private void ResetLevel()
    {

    }

    public void NextLevel()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        levelData.NextLevel(this.transform);
    }
}
