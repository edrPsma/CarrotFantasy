using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG.Scene;
using EG.UI;
using DG.Tweening;

public class StartLoadSceneState : BaseSceneState
{
    protected override string BindScene()
    {
        return "Assets/Scenes/StartLoadScene.unity";
    }

    protected override void OnEnterScene()
    {
        GetUtility<UIManager>().PushPanel("Assets/Prefabs/UIPanel/StartLoadPanel.prefab");
    }
}
