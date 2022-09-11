using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG;

[System]
public class LevelSystem : AbstractSystem
{
    public int BigLevel { get; set; }
    public int SmallLevel { get; set; }
    public void ResetLevel()
    {
        BigLevel = -1;
        SmallLevel = -1;
    }
}
