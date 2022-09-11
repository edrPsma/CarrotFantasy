using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG;

public class ResetRecordCmd : AbstractCommand
{
    protected override void OnExecute()
    {
        var storageSystem = GetSystem<StorageSystem>();
        storageSystem.AdventureModelNum.Value = 0;
        storageSystem.BurriedLevelNum.Value = 0;
        storageSystem.BossModelNum.Value = 0;
        storageSystem.Coin.Value = 0;
        storageSystem.KillMonsterNum.Value = 0;
        storageSystem.KillBossNum.Value = 0;
        storageSystem.ClearItemNum.Value = 0;
    }
}
