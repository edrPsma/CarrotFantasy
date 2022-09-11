using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using LitJson;

[CustomEditor(typeof(MapMaker))]
public class MapMakerEditor : Editor
{
    private MapMaker MapMaker => MapMaker.Instance;
    private List<FileInfo> mFileList = new List<FileInfo>();
    private string[] mFileNameArray;
    private int mSelectIndex = -1;

    #region 界面
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (Application.isPlaying)
        {
            mFileNameArray = GetFileNames(mFileList);
            EditorGUILayout.BeginHorizontal();
            int currentIndex = EditorGUILayout.Popup(mSelectIndex, mFileNameArray);
            if (currentIndex != mSelectIndex)
            {
                mSelectIndex = currentIndex;

                MapMaker.LoadMap(LoadMapInfoFile(mFileNameArray[currentIndex]));
            }

            if (GUILayout.Button("读取关卡列表"))
            {
                LoadLevelFile();
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("重置地图"))
            {
                MapMaker.Reset();
            }

            if (GUILayout.Button("保存Json文件"))
            {
                SaveJson();
            }
        }
    }
    #endregion

    #region 清除文件列表
    void ClearFileList()
    {
        mFileList.Clear();
        mSelectIndex = -1;
    }
    #endregion

    #region 加载关卡数据文件
    void LoadLevelFile()
    {
        ClearFileList();

        mFileList = GetLevelFileInfos();
        mFileNameArray = GetFileNames(mFileList);
    }
    #endregion

    #region 读取关卡文件列表
    List<FileInfo> GetLevelFileInfos()
    {
        string[] files = Directory.GetFiles(Application.dataPath + "/Json/Maps/", "*.json");

        List<FileInfo> list = new List<FileInfo>();
        for (int i = 0; i < files.Length; i++)
        {
            FileInfo file = new FileInfo(files[i]);
            list.Add(file);
        }
        return list;
    }
    #endregion

    #region 获取关卡文件名
    string[] GetFileNames(List<FileInfo> files)
    {
        List<string> names = new List<string>();
        foreach (var item in files)
        {
            names.Add(item.Name);
        }
        return names.ToArray();
    }
    #endregion

    #region 保存Json文件
    public void SaveJson()
    {
        MapInfo info = MapMaker.CreateMapInfo();
        string path = Application.dataPath + "/Json/Maps/Level_" + MapMaker.bigLevelId + "_" + MapMaker.smallLevelId + ".json";
        string jsonStr = JsonMapper.ToJson(info);
        StreamWriter sw = new StreamWriter(path);
        sw.Write(jsonStr);
        sw.Close();
        Debug.Log("保存成功");
    }
    #endregion

    #region 读取Json并转化为MapInfo对象
    public MapInfo LoadMapInfoFile(string fileName)
    {
        string filePath = Application.dataPath + "/Json/Maps/" + fileName;
        if (!File.Exists(filePath))
        {
            Debug.LogError("文件加载失败,Path" + filePath);
            return null;
        }
        StreamReader sr = new StreamReader(filePath);
        string jsonStr = sr.ReadToEnd();
        sr.Close();
        MapInfo info = JsonMapper.ToObject<MapInfo>(jsonStr);
        return info;
    }
    #endregion
}
