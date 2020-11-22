using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Grass, // 잔디 타일
    Soil // 흙 타일
}

public enum EGridTileType
{
    Grass, Soil
}

public class GridTile : MonoBehaviour
{
    [SerializeField]
    private EGridTileType m_defaultType;
    [SerializeField]
    private GameObject m_grassTile;
    [SerializeField]
    private GameObject m_soilTile;

    private EGridTileType m_type;
    private GameObject m_lastTile;

    public EGridTileType Type
    {
        get { return m_type; }
        set
        {
            m_type = value;

            if (m_lastTile)
            {
                m_lastTile.SetActive(false);
            }

            switch (m_type)
            {
                case EGridTileType.Grass:
                    m_grassTile.SetActive(true);
                    m_lastTile = m_grassTile;

                    break;

                case EGridTileType.Soil:
                    m_soilTile.SetActive(true);
                    m_lastTile = m_soilTile;

                    break;
            }
        }
    }

    private void Start()
    {
        Type = m_defaultType;
    }

    //[SerializeField]
    //Vector2Int position;
    //[SerializeField]
    //TileType type;

    //[SerializeField] 
    //bool placed; // 해당 타일에 배치된 object가 있는가?
    //public bool Placed { get { return placed; } set { placed = value; } }

    //public Field field;

    //public Vector2Int Position { get { return position; } set { position = value; } }
    //public TileType Type { get { return type; } set { type = value; } }

    //void Start()
    //{
    //    field = transform.GetChild(1).GetComponent<Field>();
    //}

    //private void Update()
    //{
    //    GameObject menu = GameObject.Find("UI_Menu");
    //    if (menu == null)
    //        return;

    //    UIManager ui_manager = menu.GetComponent<UIManager>();
    //    if (ui_manager == null)
    //        return;

    //    if(placed)
    //    {
    //        if (ui_manager.ActivedType == UIType.Placement)
    //        {
    //            transform.GetChild(3).GetComponent<MeshRenderer>().materials[0].color = new Color(255, 0, 0, 255);
    //        }
    //        else
    //        {
    //            transform.GetChild(3).GetComponent<MeshRenderer>().materials[0].color = new Color(255, 0, 0, 0);
    //        }
    //    }
    //    else
    //    {
    //        transform.GetChild(3).GetComponent<MeshRenderer>().materials[0].color = new Color(0, 255, 0, 0);
    //    }
    //}

    //public void OnPlaced()
    //{
    //    placed = true;
    //}

    //public void OnNotPlaced()
    //{
    //    placed = false;
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.tag == "Plow")
    //    {
    //        if (collision.transform.parent.GetComponent<NPC>().State == NPCState.Plowing)
    //        {
    //            if (type == TileType.Grass)
    //            {
    //                type = TileType.Soil;
    //                transform.GetChild(0).gameObject.SetActive(false);
    //                transform.GetChild(1).gameObject.SetActive(true);

    //                placed = true;
    //            }
    //        }
    //    }
    //}

    //public Vector3 GetWorldPos()
    //{
    //    GridTilesManager manager = GameObject.Find("GridManager").GetComponent<GridTilesManager>();

    //    return manager.GridPosToWorldPos(position);
    //}
}
