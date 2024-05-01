using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Data", menuName = "Level/Level Data")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public int levelIndex;
    public string[] triggerNames;
    public string[] npcNames;
    public string[] objectNames;
    public DialogData[] dialogs;
}
