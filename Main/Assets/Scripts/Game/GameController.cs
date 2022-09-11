using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG;
using EG.Resource;

public class GameController : ViewController
{
    public static GameController Instance { get; set; }
    public Level level;
    public int[] monsterIDList;
    public int monsterIDIndex;
    public LevelStage currentStage;
    private void Awake()
    {
        Instance = this;

    }
}
