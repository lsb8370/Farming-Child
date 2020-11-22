using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public struct GameData
{
    [Serializable]
    public struct GridTileData
    {
        public Vector2Int m_gridPosition;
        public EGridTileType m_type;
    }

    [Serializable]
    public struct GridObjectData
    {
        public Vector2Int m_gridPosition;
        public string m_prefabName;
        public Vector3 m_angle;
    }

    public Vector2Int m_gridSize;
    public List<GridTileData> m_gridTileData;
    public List<GridObjectData> m_gridObjectData;
}

public delegate void GameDataEvent(ref GameData gameData);

public enum EGameDataEventType
{
    OnSave, OnLoad
}


public class GameDataManager : MonoBehaviour
{
    [SerializeField]
    private string m_saveDirectory;

    private GameData m_gameData;
    private Dictionary<EGameDataEventType, GameDataEvent> m_dataEvents;

    private void Awake()
    {
        m_dataEvents = new Dictionary<EGameDataEventType, GameDataEvent>();
    }

    public void ResetData ()
    {
        m_gameData = new GameData ();
    }

    public void Save(string saveFileName)
    {
        if (m_dataEvents.ContainsKey(EGameDataEventType.OnSave))
        {
            m_dataEvents[EGameDataEventType.OnSave].Invoke(ref m_gameData);
        }

        string path = Path.Combine(Application.dataPath, m_saveDirectory);

        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }

        string json = JsonUtility.ToJson(m_gameData);
        path = Path.Combine(path, saveFileName);

        File.WriteAllText(path, json);
    }

    public bool Load(string saveFileName)
    {
        string path = Path.Combine(Application.dataPath, m_saveDirectory, saveFileName);

        if (!File.Exists(path))
        {
            m_gameData = new GameData();
            return false;
        }

        string json = File.ReadAllText(path);
        m_gameData = JsonUtility.FromJson<GameData>(json);

        if (m_dataEvents.ContainsKey (EGameDataEventType.OnLoad))
        {
            m_dataEvents[EGameDataEventType.OnLoad].Invoke(ref m_gameData);
        }

        return true;
    }

    public void AddDataEventCallback(EGameDataEventType dataEventType, GameDataEvent callback)
    {
        if (m_dataEvents.ContainsKey(dataEventType))
        {
            m_dataEvents[dataEventType] += callback;
        }
        else
        {
            m_dataEvents.Add(dataEventType, callback);
        }
    }

    //public void Save (ref GameData saveData, string saveFileName)
    //{
    //    string path = Path.Combine(Application.dataPath, m_saveDirectory);

    //    if (Directory.Exists (path) == false)
    //    {
    //        Directory.CreateDirectory(path);
    //    }

    //    string json = JsonUtility.ToJson(saveData);
    //    path = Path.Combine(path, saveFileName);

    //    File.WriteAllText(path, json);
    //}

    //public bool Load(string saveFileName, out GameData saveData)
    //{
    //    string path = Path.Combine(Application.dataPath, m_saveDirectory, saveFileName);

    //    if (!File.Exists(path))
    //    {
    //        saveData = new GameData();
    //        return false;
    //    }

    //    string json = File.ReadAllText(path);
    //    saveData = JsonUtility.FromJson<GameData>(json);

    //    return true;
    //}
}
