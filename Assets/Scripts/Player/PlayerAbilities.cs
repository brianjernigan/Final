using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public bool HasPhotonBlast { get; set; }

    public event Action OnPhotonBlastAcquired;
}
