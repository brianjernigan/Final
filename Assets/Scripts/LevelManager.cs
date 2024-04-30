using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Level
{
    One,
    Two,
    Three
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public LevelData CurrentLevelData { get; private set; }

    [SerializeField] private LevelData _levelOneData;
    [SerializeField] private LevelData _levelTwoData;
    [SerializeField] private LevelData _levelThreeData;

    public void LoadLevel(Level levelNumber)
    {
        switch (levelNumber)
        {
            case Level.One:
                CurrentLevelData = _levelOneData;
                break;
            case Level.Two:
                CurrentLevelData = _levelTwoData;
                break;
            case Level.Three:
                CurrentLevelData = _levelThreeData;
                break;
        }
    }
}
