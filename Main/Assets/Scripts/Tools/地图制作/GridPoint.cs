using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG;
using EG.Resource;
using System.IO;

public class GridPoint : ViewController
{
    #region 组件
    private SpriteRenderer mSpriteRenderer;
    private Sprite mGridSprite;
    private Sprite mMonsterSprite;
    private List<GameObject> mItemObj = new List<GameObject>();
    #endregion

    #region 参数
    public GameObject CurItem { get; private set; }
    public GridState gridState = new GridState() { ItemId = -1, CanBuild = true };
    public GridIndex gridIndex = new GridIndex();
    #endregion

    #region 结构体定义
    public struct GridState
    {
        public bool CanBuild { get; set; }
        public bool IsMonsterPath { get; set; }
        public bool HasItem { get; set; }
        public int ItemId { get; set; }
    }

    [System.Serializable]
    public struct GridIndex
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    #endregion

    private void Awake()
    {
        mSpriteRenderer = GetComponent<SpriteRenderer>();
        Init();
    }

    #region 初始化
    public void Init()
    {
        int bigLevelId = MapMaker.Instance.bigLevelId;
        int smallLevelId = MapMaker.Instance.smallLevelId;
        mGridSprite = GetUtility<ResourcesManager>().Load<Sprite>("Assets/Art/Pictures/NormalMordel/Game/Grid.png");
        mMonsterSprite = GetUtility<ResourcesManager>().Load<Sprite>("Assets/Art/Pictures/NormalMordel/Game/1/Monster/6-1.PNG");
        string itemPath = "Assets/Prefabs/Game/" + bigLevelId + "/Item";
        if (Directory.Exists(itemPath))
        {
            DirectoryInfo infos = new DirectoryInfo(itemPath);
            int count = infos.GetFiles("*.prefab").Length;
            mItemObj.Clear();
            for (int i = 0; i < count; i++)
            {
                GameObject item = GetUtility<ResourcesManager>().Load<GameObject>(itemPath + "/" + i + ".prefab");
                mItemObj.Add(item);
            }
        }
    }
    #endregion

    #region 鼠标点击
    private void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.P))
        {
            if (!gridState.IsMonsterPath)
            {
                mSpriteRenderer.enabled = true;
                mSpriteRenderer.sprite = mMonsterSprite;
                gridState.CanBuild = false;
                gridState.IsMonsterPath = true;
                MapMaker.Instance.monsterPathList.Add(gridIndex);
            }
            else
            {
                gridState.IsMonsterPath = false;
                mSpriteRenderer.sprite = mGridSprite;
                gridState.CanBuild = true;
                MapMaker.Instance.monsterPathList.Remove(gridIndex);
            }
        }
        else if (Input.GetKey(KeyCode.I))
        {
            if (CurItem != null)
            {
                Destroy(CurItem);
            }
            gridState.ItemId++;
            if (gridState.ItemId >= mItemObj.Count)
            {
                gridState.ItemId = -1;
                gridState.HasItem = false;
                return;
            }
            gridState.HasItem = true;
            CurItem = CreateItem();
        }
        else
        {
            if (CurItem == null)
            {
                mSpriteRenderer.enabled = !mSpriteRenderer.enabled;
                gridState.CanBuild = !gridState.CanBuild;
            }
        }
    }
    #endregion

    #region 创建道具
    GameObject CreateItem()
    {
        GameObject result;
        ItemGridCount itemGridCount = mItemObj[gridState.ItemId].transform.GetComponent<ItemGridCount>();
        var targetPos = transform.position;
        if (itemGridCount.count == 4)
        {
            targetPos = transform.position + new Vector3(MapMaker.Instance.GridWidth / 2, MapMaker.Instance.GridHeight / 2);
            result = Instantiate(mItemObj[gridState.ItemId], targetPos, transform.rotation);
        }
        else if (itemGridCount.count == 2)
        {
            targetPos = transform.position + new Vector3(MapMaker.Instance.GridWidth / 2, 0);
            result = Instantiate(mItemObj[gridState.ItemId], targetPos, transform.rotation);
        }
        else
        {
            targetPos = transform.position;
            result = Instantiate(mItemObj[gridState.ItemId], targetPos, transform.rotation);
        }
        return result;
    }
    #endregion

    #region 重置
    public void Reset()
    {
        if (gridState.IsMonsterPath)
        {
            MapMaker.Instance.monsterPathList.Remove(gridIndex);
        }
        mSpriteRenderer.enabled = true;
        mSpriteRenderer.sprite = mGridSprite;
        gridState.IsMonsterPath = false;
        gridState.CanBuild = true;
        gridState.ItemId = -1;
        gridState.HasItem = false;
        if (CurItem != null)
        {
            Destroy(CurItem);
        }
    }
    #endregion

    #region 同步
    public void Synchronize(GridState state)
    {
        gridState.CanBuild = state.CanBuild;
        gridState.HasItem = state.HasItem;
        gridState.IsMonsterPath = state.IsMonsterPath;
        gridState.ItemId = state.ItemId;
        mSpriteRenderer.enabled = true;

        if (gridState.IsMonsterPath)
        {
            mSpriteRenderer.sprite = mMonsterSprite;
        }
        else if (gridState.HasItem)
        {
            if (CurItem != null)
            {
                Destroy(CurItem);
            }
            CurItem = CreateItem();
        }
        else if (!gridState.CanBuild)
        {
            mSpriteRenderer.enabled = false;
        }
    }
    #endregion
}
