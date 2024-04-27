using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog/DialogSO")]
public class DialogScriptableObject : ScriptableObject
{
    public DialogLines dialogData;
}
