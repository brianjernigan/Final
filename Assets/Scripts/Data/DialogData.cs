using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog/DialogData")]
public class DialogData : ScriptableObject
{
    public string DialogName;
    public List<string> Lines;
}
