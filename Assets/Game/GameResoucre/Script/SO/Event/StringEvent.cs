using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Name" , menuName = "level Data")]
public class StringEvent : ScriptableObject
{
    [SerializeField] private string levelName;

    public string LevelName { get => levelName; }

    public void setUpLevel(string levelName)
    {
        this.levelName = levelName;
    }
}
