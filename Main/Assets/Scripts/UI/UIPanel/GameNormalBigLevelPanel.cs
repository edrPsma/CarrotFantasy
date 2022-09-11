using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG;
using EG.UI;
using EG.Resource;
using UnityEngine.UI;

public class GameNormalBigLevelPanel : BasePanel
{
    public SliderCanCoverScrollView LevelScrollView;
    private CanvasGroup mCanvasGroup;
    public void MoveToNextLevel()
    {
        LevelScrollView.MoveToNext();
    }

    public void MoveToLastLevel()
    {
        LevelScrollView.MoveToLast();
    }

    protected override void OnInit()
    {
        mCanvasGroup = GetComponent<CanvasGroup>();
        LoadingBigLevelTabs();
    }

    protected override void OnPause()
    {
        if (GetSystem<LevelSystem>().BigLevel != -1)
        {
            mCanvasGroup.alpha = 0;
            mCanvasGroup.blocksRaycasts = false;
        }
    }

    protected override void OnResume()
    {
        mCanvasGroup.alpha = 1;
        mCanvasGroup.blocksRaycasts = true;
    }

    void LoadingBigLevelTabs()
    {
        List<BigLevelInfo> bigLevelInfos = GetModel<LevelModel>().GetBigLevelInfos();
        GetComponentInChildren<SliderCanCoverScrollView>().ChangeItemCount(bigLevelInfos.Count);
        Transform content = transform.Find("Scroll View").Find("Viewport").Find("Content");
        for (int i = 0; i < bigLevelInfos.Count; i++)
        {
            GameObject bigLevelUIItem = GetUtility<ObjectPoolManager>().InstantiateObject("Assets/Prefabs/UI/BigLevelItem.prefab", content);
            bigLevelUIItem.name = i.ToString();
            bigLevelUIItem.GetComponent<Image>().sprite = bigLevelInfos[i].BigLevelBG;
            bigLevelUIItem.transform.Find("Img_Describe").GetComponent<Image>().sprite = bigLevelInfos[i].BigLevelTitle;
            bigLevelUIItem.GetComponent<Button>().onClick.AddListener(() =>
            {
                GetSystem<LevelSystem>().BigLevel = int.Parse(bigLevelUIItem.name);
                GetSystem<LevelSystem>().SmallLevel = -1;
                GetUtility<UIManager>().PushPanel("Assets/Prefabs/UIPanel/GameNormalLevelPanel.prefab");
            });
        }
    }
}
