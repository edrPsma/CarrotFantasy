using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG.Scene;
using EG.UI;

public class BossGameOptionSceneState : BaseSceneState
{
    protected override string BindScene()
    {
        return "Assets/Scenes/DynamicScene/BossOptionScene.unity";
    }

    protected override void OnLoadStart()
    {
        GetUtility<UIManager>().PushPanel("Assets/Prefabs/UIPanel/GameLoadPanel.prefab");
    }

    protected override void OnEnterScene()
    {
        GetUtility<UIManager>().PushPanel("Assets/Prefabs/UIPanel/MainPanel.prefab");
    }
}
