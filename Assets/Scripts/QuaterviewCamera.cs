using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ECameraMode
{
    Follow, // 선택한 Target을 따라가게 함
    ManualControl // 키보드 입력(WASD)에 따라 움직이게 함
}

public class QuaterviewCamera : MonoBehaviour
{
    [SerializeField]
    private ECameraMode m_mode;
    [SerializeField]
    private float m_moveSpeed = 4.0f;

    private Transform m_target;
    private Vector3 m_movementDirection;

    private void Awake()
    {
        m_movementDirection = Vector3.zero;
    }

    private void Start()
    {
        PlayerInput playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();

        playerInput.AddKeyCallback(KeyCode.W, MoveForward);
        playerInput.AddKeyCallback(KeyCode.S, MoveBackward);
        playerInput.AddKeyCallback(KeyCode.D, MoveRight);
        playerInput.AddKeyCallback(KeyCode.A, MoveLeft);
    }

    private void LateUpdate()
    {
        if (m_mode == ECameraMode.ManualControl)
        {
            Vector3 movement = new Vector3 (m_movementDirection.x, 0.0f, m_movementDirection.z).normalized;
            movement *= m_moveSpeed * Time.deltaTime;

            transform.Translate(movement, Space.World);

            m_movementDirection = Vector3.zero;
        }
        //else if (m_mode == ECameraMode.Follow)
        //{
        //    if (m_target == null)
        //        return;

        //    FollowTarget();
        //}
    }

    private void MoveForward ()
    {
        m_movementDirection += transform.forward;
    }

    private void MoveBackward()
    {
        m_movementDirection -= transform.forward;
    }

    private void MoveRight()
    {
        m_movementDirection += transform.right;
    }

    private void MoveLeft()
    {
        m_movementDirection -= transform.right;
    }

    //public void ChoiceTarget(Transform target)
    //{
    //    this.target = target;
    //    mode = CameraMode.Follow;
    //}

    //void ExitFollowMode(KeyEventType keyType)
    //{
    //    if(mode == CameraMode.Follow)
    //    {
    //        target = null;
    //        mode = CameraMode.ManualControl;
    //    }
    //}

    //void FollowTarget()
    //{
    //    transform.position = target.position + position;
    //    transform.localRotation = Quaternion.Euler(rotation);
    //}
}
