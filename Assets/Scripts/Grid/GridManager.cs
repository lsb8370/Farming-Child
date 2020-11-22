using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField]
    private Vector3 m_gridPivot;

    [Header("Tile")]
    [SerializeField]
    private GameObject m_tilePrefab;
    [SerializeField]
    private Vector2 m_tileSize;
    public Vector2 TileSize { get { return m_tileSize; } }

    private Vector2Int m_gridSize;
    private List<List<GridTile>> m_gridTiles;
    private List<GridObject> m_gridObjects;

    private Vector3 StartPosition 
    {
        get
        {
            float x = m_gridPivot.x - (m_gridSize.x * m_tileSize.x * 0.5f) + (m_tileSize.x * 0.5f);
            float z = m_gridPivot.z - (m_gridSize.y * m_tileSize.y * 0.5f) + (m_tileSize.y * 0.5f);

            return new Vector3(x, m_gridPivot.y, z);
        }
    }

    private void Awake()
    {
        m_gridObjects = new List<GridObject>();
    }

    private void Start()
    {
        var gameDataManager = GetComponent<GameDataManager> ();
        gameDataManager.AddDataEventCallback(EGameDataEventType.OnSave, SaveGrid);
        gameDataManager.AddDataEventCallback(EGameDataEventType.OnLoad, LoadGrid);
    }

    public void CreateGrid(Vector2Int size)
    {
        m_gridSize = size;
        m_gridTiles = new List<List<GridTile>>();

        for (int x = 0; x < m_gridSize.x; x++)
        {
            m_gridTiles.Add (new List<GridTile>());

            for (int y = 0; y < m_gridSize.y; y++)
            {
                Vector3 tilePosition = GridPosToWorldPos(new Vector2Int(x, y));
                GridTile tile = Instantiate(m_tilePrefab, tilePosition, Quaternion.identity).GetComponent<GridTile> ();

                m_gridTiles[x].Add(tile);
            }
        }
    }

    public void SaveGrid (ref GameData gamedata)
    {
        gamedata.m_gridSize = m_gridSize;
        gamedata.m_gridTileData = new List<GameData.GridTileData>();
        gamedata.m_gridObjectData = new List<GameData.GridObjectData>();

        foreach (var gridTileColumn in m_gridTiles)
        {
            foreach (var gridTile in gridTileColumn)
            {
                var tileData = new GameData.GridTileData ();
                tileData.m_gridPosition = WorldPosToGridPos(gridTile.transform.position);
                tileData.m_type = gridTile.Type;

                gamedata.m_gridTileData.Add(tileData);
            }
        }

        foreach (var gridObject in m_gridObjects)
        {
            var objectData = new GameData.GridObjectData();
            objectData.m_gridPosition = WorldPosToGridPos(gridObject.transform.position);
            objectData.m_prefabName = gridObject.Type;
            objectData.m_angle = gridObject.transform.eulerAngles;

            gamedata.m_gridObjectData.Add(objectData);
        }
    }

    public void LoadGrid (ref GameData gamedata)
    {
        CreateGrid(gamedata.m_gridSize);

        foreach (var tileData in gamedata.m_gridTileData)
        {
            m_gridTiles[tileData.m_gridPosition.x][tileData.m_gridPosition.y].Type = tileData.m_type;
        }

        foreach (var objectData in gamedata.m_gridObjectData)
        {
            var gridObject = AddGridObject(objectData.m_prefabName);

            if (gridObject)
            {
                gridObject.transform.position = GridPosToWorldPos(objectData.m_gridPosition);
                gridObject.transform.eulerAngles = objectData.m_angle;
                gridObject.SetGridContactPoints(this);
            }
        }
    }

    public Vector3 GridPosToWorldPos(Vector2Int gridPos)
    {
        Vector3 offset = new Vector3(gridPos.x * m_tileSize.x, m_gridPivot.y, gridPos.y * m_tileSize.y);

        return StartPosition + offset;
    }

    public Vector2Int WorldPosToGridPos(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt((worldPos.x - StartPosition.x) / m_tileSize.x); // 몇 열
        int y = Mathf.RoundToInt((worldPos.z - StartPosition.z) / m_tileSize.y); // 몇 행

        return new Vector2Int(x, y);
    }

    public GridObject AddGridObject (string prefabName)
    {
        string prefabPath = Path.Combine("Prefabs", prefabName);
        GameObject prefab = Resources.Load<GameObject>(prefabPath);

        if (prefab == null)
        {
            return null;
        }

        var gridObject = Instantiate(prefab).GetComponent<GridObject>();
        gridObject.Type = prefabName;

        m_gridObjects.Add(gridObject);

        return gridObject;
    }

    public void AddGridObject(GridObject gridObject)
    {
        m_gridObjects.Add(gridObject);
    }

    public bool IsIntersect(GridObject gridObject)
    {
        Vector2Int objectPivot = WorldPosToGridPos(gridObject.transform.position);
        int angle = Mathf.RoundToInt(gridObject.transform.eulerAngles.y);

        foreach(GridObject obj in m_gridObjects)
        {
            foreach(Vector2Int cell in obj.ContactCells)
            {
                if (angle == 0)
                {
                    if ((objectPivot.x - (gridObject.Size.x - 1) <= cell.x && cell.x <= objectPivot.x)
                    && (objectPivot.y - (gridObject.Size.y - 1) <= cell.y && cell.y <= objectPivot.y))
                        return true;
                }
                else if (angle == 90)
                {
                    if ((objectPivot.x - (gridObject.Size.x - 1) <= cell.x && cell.x <= objectPivot.x)
                    && (objectPivot.y <= cell.y && cell.y <= objectPivot.y + (gridObject.Size.y - 1)))
                        return true;
                }
                else if (angle == 180)
                {
                    if ((objectPivot.x <= cell.x && cell.x <= objectPivot.x + (gridObject.Size.x - 1))
                    && (objectPivot.y <= cell.y && cell.y <= objectPivot.y + (gridObject.Size.y - 1)))
                        return true;
                }
                else if (angle == 270)
                {
                    if ((objectPivot.x <= cell.x && cell.x <= objectPivot.x + (gridObject.Size.x - 1))
                    && (objectPivot.y - (gridObject.Size.y - 1) <= cell.y && cell.y <= objectPivot.y))
                        return true;
                }
            }
        }

        return false;
    }

    public bool IsInArea(GridObject gridObject)
    {
        Vector2Int objectPivot = WorldPosToGridPos(gridObject.transform.position);
        int angle = Mathf.RoundToInt(gridObject.transform.eulerAngles.y);

        if (angle == 0)
        {
            if ((objectPivot.x - (gridObject.Size.x - 1) >= 0)
            && objectPivot.y - (gridObject.Size.y - 1) >= 0)
                return true;
        }
        else if (angle == 90)
        {
            if ((objectPivot.x - (gridObject.Size.x - 1) >= 0)
            && objectPivot.y + (gridObject.Size.y - 1) < m_gridSize.y)
                return true;
        }
        else if (angle == 180)
        {
            if ((objectPivot.x + (gridObject.Size.x - 1) < m_gridSize.x)
            && objectPivot.y + (gridObject.Size.y - 1) < m_gridSize.y)
                return true;
        }
        else if (angle == 270)
        {
            if ((objectPivot.x + (gridObject.Size.x - 1) < m_gridSize.x)
             && objectPivot.y - (gridObject.Size.y - 1) >= 0)
                return true;
        }

        return false;
    }
}
