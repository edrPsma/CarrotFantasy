using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG;
using EG.UI;
using UnityEngine.UI;
using EG.Resource;
using TMPro;
using EG.Scene;

public class GameNormalLevelPanel : BasePanel
{
    private CanvasGroup mCanvasGroup;
    public SliderCanCoverScrollView LevelScrollView;
    public Image DecorateLeft;
    public Image DecorateRight;
    public TMP_Text Waves;
    public Transform TowerContent;
    Transform mContent;
    List<GameObject> mLoadSmallLevelItems = new List<GameObject>();
    List<GameObject> mTowerItems = new List<GameObject>();
    protected override void OnInit()
    {
        mCanvasGroup = GetComponent<CanvasGroup>();
        mContent = transform.Find("Scroll View").Find("Viewport").Find("Content");
        BindProperty();
    }

    protected override void OnEnter()
    {
        mLoadSmallLevelItems.Clear();
        mTowerItems.Clear();
        mCanvasGroup.alpha = 1;
        mCanvasGroup.blocksRaycasts = true;
        LevelScrollView.Reset();
        LoadingSmallLevelTabs();
    }

    protected override void OnExit()
    {
        mCanvasGroup.alpha = 0;
        mCanvasGroup.blocksRaycasts = false;
        ClearSmallLevelItemAndTowerItem();
    }

    #region 加载小关卡
    void LoadingSmallLevelTabs()
    {
        LevelDecorate decorate = GetModel<LevelModel>().GetBigLevelDecorateById(GetSystem<LevelSystem>().BigLevel);
        DecorateLeft.sprite = decorate.DecorateLeft;
        DecorateRight.sprite = decorate.DecorateRight;

        List<SmallLevelInfo> smallInfoList = GetModel<LevelModel>().GetSmallLevelInfosById(GetSystem<LevelSystem>().BigLevel);
        LevelScrollView.ChangeItemCount(smallInfoList.Count);
        for (int i = 0; i < smallInfoList.Count; i++)
        {
            GameObject item = GetUtility<ObjectPoolManager>().InstantiateObject("Assets/Prefabs/UI/SmallLevelItem.prefab", mContent);
            mLoadSmallLevelItems.Add(item);
            item.GetComponent<Image>().sprite = smallInfoList[i].LevelMap;
        }
        LevelScrollView.CurrentIndex.Synchronization();
    }
    #endregion

    #region 属性绑定
    void BindProperty()
    {
        LevelScrollView.CurrentIndex.OnValueChanged += value =>
        {
            for (int i = mTowerItems.Count - 1; i >= 0; i--)
            {
                GetUtility<ObjectPoolManager>().UnLoad(mTowerItems[i]);
            }
            mTowerItems.Clear();
            List<SmallLevelInfo> smallInfoList = GetModel<LevelModel>().GetSmallLevelInfosById(GetSystem<LevelSystem>().BigLevel);
            int index = value - 1;
            Waves.text = smallInfoList[index].Waves.ToString();
            foreach (var towerItem in smallInfoList[index].TowerList)
            {
                GameObject tower = GetUtility<ObjectPoolManager>().InstantiateObject("Assets/Prefabs/UI/TowerItem.prefab", TowerContent);
                mTowerItems.Add(tower);
                tower.GetComponent<Image>().sprite = towerItem;
            }
        };
    }
    #endregion

    #region 清除加载的所有小关卡和塔列表
    void ClearSmallLevelItemAndTowerItem()
    {
        for (int i = mLoadSmallLevelItems.Count - 1; i >= 0; i--)
        {
            GetUtility<ObjectPoolManager>().UnLoad(mLoadSmallLevelItems[i]);
        }

        for (int i = mTowerItems.Count - 1; i >= 0; i--)
        {
            GetUtility<ObjectPoolManager>().UnLoad(mTowerItems[i]);
        }
    }
    #endregion

    #region 进入到游戏场景
    public void MoveToGameScene()
    {
        LevelSystem levelSystem = GetSystem<LevelSystem>();
        GetSystem<LevelSystem>().SmallLevel = LevelScrollView.CurrentIndex.Value - 1;
        GetUtility<SceneManager>().LoadScene<NormalModelSceneState>(false);
    }
    #endregion
}
