using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Map에 배치할 수 있는 오브젝트들만!
public class GridObject : MonoBehaviour
{
    // Object의 Size
    [SerializeField]
    private Vector2Int m_size = Vector2Int.zero;
    public Vector2Int Size { get { return m_size; } }

    private string m_type;
    public string Type { get { return m_type; } set { m_type = value; } }

    // Object의 pivot
    private Vector2Int pivotCell;

    [SerializeField]
    private List<Vector2Int> contactCells = new List<Vector2Int>();
    public List<Vector2Int> ContactCells { get { return contactCells; } }

    public void Rotate()
    {
        Vector3 angle = transform.eulerAngles;

        angle.y += 90;
        angle.y %= 360;

        transform.eulerAngles = angle;

        int x = m_size.x;
        int y = m_size.y;

        m_size.x = y;
        m_size.y = x;
    }

    public void SetGridContactPoints(GridManager gridManager)
    {
        float cellSize = gridManager.TileSize.x;

        Vector2Int pivotToGridCellPos = gridManager.WorldPosToGridPos(transform.position);

        int angle = Mathf.RoundToInt(transform.eulerAngles.y);

        for (int row = 0; row < m_size.y; row++)
        {
            for (int column = 0; column < m_size.x; column++)
            {
                if (angle == 0)
                    contactCells.Add(new Vector2Int(pivotToGridCellPos.x - column, pivotToGridCellPos.y - row));

                else if (angle == 90)
                    contactCells.Add(new Vector2Int(pivotToGridCellPos.x - column, pivotToGridCellPos.y + row));

                else if (angle == 180)
                    contactCells.Add(new Vector2Int(pivotToGridCellPos.x + column, pivotToGridCellPos.y + row));

                else if (angle == 270)
                    contactCells.Add(new Vector2Int(pivotToGridCellPos.x + column, pivotToGridCellPos.y - row));
            }
        }
    }
}
