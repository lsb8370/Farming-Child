using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECreationState
{
    Nothing,
    Placement
}

public class Player : MonoBehaviour
{
    [SerializeField]
    private LayerMask m_creationLayerMask;

    private PlayerInput m_input;
    private ECreationState m_creationState;
    private GridObject m_creatingObject;

    private GridManager m_gridManager;

    private void Awake()
    {
        m_creationState = ECreationState.Nothing;
        m_creatingObject = null;

        m_input = GetComponent<PlayerInput>(); 
    }

    private void Start()
    {
        m_input.AddMouseButtonDownCallback(EPlayerInputMouseButttonType.Left, FinishCreate);
        m_input.AddMouseButtonDownCallback(EPlayerInputMouseButttonType.Right, CancelCreate);
        m_input.AddKeyDownCallback(KeyCode.R, RotateObject);

        m_gridManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GridManager>();
    }

    private void Update()
    {
        if (m_creatingObject)
        {
            Vector2Int mouseGridPosition;

            if (RaycastMouse(out mouseGridPosition))
            {
                m_creatingObject.transform.position = m_gridManager.GridPosToWorldPos(mouseGridPosition);
            }
        }
    }

    public void StartCreate(string prefabName)
    {
        if (m_creationState != ECreationState.Nothing)
        {
            CancelCreate();
        }

        GameObject prefab = Resources.Load<GameObject>($"Prefabs/{prefabName}");

        if (prefab == null)
        {
            Debug.Log("This Prefab is Not Exist!");
            return;
        }

        m_creatingObject = Instantiate(prefab, Vector3.zero, Quaternion.identity).GetComponent<GridObject> ();
        m_creatingObject.Type = prefabName;

        Vector2Int mouseGridPosition;

        if (RaycastMouse (out mouseGridPosition))
        {
            m_creatingObject.transform.position = m_gridManager.GridPosToWorldPos(mouseGridPosition);
        }

        m_creationState = ECreationState.Placement;
    }

    private void FinishCreate ()
    {
        if (!m_creatingObject)
        {
            return;
        }

        // 그리드 영역을 벗어난 곳에는 배치할 수 없음
        if(!m_gridManager.IsInArea(m_creatingObject))
        {
            Debug.Log("그리드 영역을 벗어나서 배치할 수 없겠네요..");
            return;
        }
        
        // 다른 Object와 겹치면 배치할 수 없음
        if(m_gridManager.IsIntersect(m_creatingObject))
        {
            Debug.Log("다른 오브젝트와 겹쳐서 배치할 수 없겠네요..");
            return;
        }

        m_creatingObject.SetGridContactPoints(m_gridManager);
        m_gridManager.AddGridObject(m_creatingObject);

        m_creatingObject = null;
        m_creationState = ECreationState.Nothing;
    }

    private void CancelCreate()
    {
        if (!m_creatingObject)
        {
            return;
        }

        Destroy(m_creatingObject);

        m_creatingObject = null;
        m_creationState = ECreationState.Nothing;
    }

    private bool RaycastMouse (out Vector2Int mouseGridPosition)
    {
        mouseGridPosition = Vector2Int.zero;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.PositiveInfinity, m_creationLayerMask))
        {
            mouseGridPosition = m_gridManager.WorldPosToGridPos(hit.point);

            return true;
        }

        return false;
    }

    // R키를 누르면 Object가 회전하게 함
    private void RotateObject()
    {
        if(!m_creatingObject)
        {
            return;
        }

        m_creatingObject.Rotate();
    }
}
