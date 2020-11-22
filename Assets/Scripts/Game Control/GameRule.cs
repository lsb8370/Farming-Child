using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRule : MonoBehaviour
{
    [SerializeField]
    private Vector2Int m_gridSize;
    [SerializeField]
    private int m_numOfArea; // Area의 개수 (수정)

    public Vector2Int GridSize
    {
        get { return m_gridSize; }
    }

    public int NumOfArea
    {
        get { return m_numOfArea; }
    }
}
