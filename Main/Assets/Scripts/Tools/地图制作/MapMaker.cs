using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EG;
using EG.Resource;
using System.IO;
using LitJson;

public class MapMaker : ViewController
{
    public static MapMaker Instance { get; private set; }
    #region 网格相关参数
    //网格边界
    public Bounds bounds;
    public int row = 7;
    public int columns = 12;
    public bool debug = false;
    public float GridWidth { get; private set; }
    public float GridHeight { get; private set; }
    #endregion

    #region 关卡数据
    public int bigLevelId;
    public int smallLevelId;
    private List<GridPoint> gridPointList = new List<GridPoint>();
    public List<GridPoint.GridIndex> monsterPathList = new List<GridPoint.GridIndex>();
    public List<Round.RoundInfo> roundInfoList = new List<Round.RoundInfo>();
    #endregion

    #region 计算网格大小
    void CalculateGrid()
    {
        float width = bounds.max.x - bounds.min.x;
        float height = bounds.max.y - bounds.min.y;
        GridWidth = width / columns;
        GridHeight = height / row;
    }
    #endregion

    #region 绘制调式网格
    private void OnDrawGizmos()
    {
        //绘制边界
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(bounds.center + transform.position, bounds.size);

        //绘制网格
        CalculateGrid();
        Gizmos.color = Color.red;
        Vector2 leftDown = bounds.min;
        Vector2 rightUp = bounds.max;
        for (int i = 0; i <= columns; i++)
        {
            Vector2 start = leftDown + new Vector2(i * GridWidth, 0);
            Vector2 end = leftDown + new Vector2(i * GridWidth, rightUp.y - leftDown.y);
            Gizmos.DrawLine(start, end);
        }
        for (int i = 0; i <= row; i++)
        {
            Vector2 start = leftDown + new Vector2(0, i * GridHeight);
            Vector2 end = leftDown + new Vector2(rightUp.x - leftDown.x, i * GridHeight);
            Gizmos.DrawLine(start, end);
        }
    }
    #endregion

    private GameObject mGridObj;

    private void Awake()
    {
        mGridObj = GetUtility<ResourcesManager>().Load<GameObject>("Assets/Prefabs/Game/Grid.prefab");
        Instance = this;
        ChangeBGAndRoad();
        CalculateGrid();
        CreateGrid();
    }

    void ChangeBGAndRoad()
    {
        SpriteRenderer bg = transform.Find("BG").GetComponent<SpriteRenderer>();
        SpriteRenderer road = transform.Find("Road").GetComponent<SpriteRenderer>();
        bg.sprite = GetUtility<ResourcesManager>().Load<Sprite>("Assets/Art/Pictures/NormalMordel/Game/" + bigLevelId + "/BG0.PNG");
        road.sprite = GetUtility<ResourcesManager>().Load<Sprite>("Assets/Art/Pictures/NormalMordel/Game/" + bigLevelId + "/Road" + smallLevelId + ".PNG");
    }

    #region 生成网格
    void CreateGrid()
    {
        GameObject parent = new GameObject("Grids");
        parent.transform.SetParent(transform);
        Vector2 point = bounds.min + new Vector3(GridWidth / 2, GridHeight / 2);
        for (int y = 0; y < row; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                var targetPoint = point + new Vector2(x * GridWidth, y * GridHeight);
                GameObject gridObj = Instantiate(mGridObj, targetPoint, transform.rotation);
                gridObj.transform.SetParent(parent.transform);
                var gridPoint = gridObj.GetComponent<GridPoint>();
                gridPointList.Add(gridPoint);
                gridPoint.gridIndex.X = x;
                gridPoint.gridIndex.Y = y;
            }
        }
    }
    #endregion

    #region 重置地图
    public void Reset()
    {
        foreach (var gridPoint in gridPointList)
        {
            gridPoint.Init();
            gridPoint.Reset();
        }
        monsterPathList.Clear();
        ChangeBGAndRoad();
    }
    #endregion

    #region 生成MapInfo对象用来保存文件
    public MapInfo CreateMapInfo()
    {
        MapInfo info = new MapInfo();
        info.bigLevelId = bigLevelId;
        info.smallLevelId = smallLevelId;
        info.gridStateList = new List<GridPoint.GridState>();
        foreach (var point in gridPointList)
        {
            info.gridStateList.Add(point.gridState);
        }
        info.monsterPath = new List<GridPoint.GridIndex>();
        foreach (var path in monsterPathList)
        {
            info.monsterPath.Add(path);
        }
        info.roundInfoList = new List<Round.RoundInfo>();
        foreach (var roundInfo in roundInfoList)
        {
            info.roundInfoList.Add(roundInfo);
        }
        return info;
    }
    #endregion

    #region 读取关卡数据
    public void LoadMap(MapInfo info)
    {
        Reset();
        bigLevelId = info.bigLevelId;
        smallLevelId = info.smallLevelId;
        ChangeBGAndRoad();
        for (int i = 0; i < gridPointList.Count; i++)
        {
            gridPointList[i].Init();
            gridPointList[i].Synchronize(info.gridStateList[i]);
        }
        monsterPathList.Clear();
        foreach (var path in info.monsterPath)
        {
            monsterPathList.Add(path);
        }
        roundInfoList.Clear();
        foreach (var roundInfo in info.roundInfoList)
        {
            roundInfoList.Add(roundInfo);
        }
    }
    #endregion
}
