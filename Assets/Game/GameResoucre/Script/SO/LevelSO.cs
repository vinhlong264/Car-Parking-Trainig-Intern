using UnityEngine;

[CreateAssetMenu(fileName = "Level Game", menuName = "Level")]
public class LevelSO : ScriptableObject
{
    public string levelName;
    [SerializeField] private bool isCompelete;

    public GameObject levelPrefabs;

    public bool IsCompelete { get => isCompelete; }

    public void CompeleteLevel()
    {
        Debug.Log("Call");
        isCompelete = true;
    }
}
