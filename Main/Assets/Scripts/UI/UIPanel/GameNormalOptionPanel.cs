using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG;
using EG.UI;
using EG.Scene;

public class GameNormalOptionPanel : BasePanel
{
    public void BackToPrevious()
    {
        if (GetSystem<LevelSystem>().BigLevel == -1)
        {
            GetUtility<SceneManager>().LoadScene<MainSceneState>();
        }
        else
        {
            GetUtility<UIManager>().PopPanel();
            GetSystem<LevelSystem>().ResetLevel();
        }
    }
    protected override void OnInit()
    {
        GetSystem<LevelSystem>().ResetLevel();
    }

    public void OpenHelpPage()
    {
        GetUtility<UIManager>().PushPanel("Assets/Prefabs/UIPanel/HelpPanel.prefab").transform.SetAsLastSibling();
    }
}
