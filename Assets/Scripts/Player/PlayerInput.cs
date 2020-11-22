using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerInputMouseButttonType
{
    Left, Right, Middle
}

public class PlayerInput : MonoBehaviour
{
    private bool m_isKeyboardEnabled;
    private bool m_isMouseEnabled;

    private Dictionary<KeyCode, Action> m_onKeyUpEventMap;
    private Dictionary<KeyCode, Action> m_onKeyDownEventMap;
    private Dictionary<KeyCode, Action> m_onKeyEventMap;

    private Dictionary<EPlayerInputMouseButttonType, Action> m_onMouseButtonUpEventMap;
    private Dictionary<EPlayerInputMouseButttonType, Action> m_onMouseButtonDownEventMap;
    private Dictionary<EPlayerInputMouseButttonType, Action> m_onMouseButtonEventMap;

    private Action<Vector3> m_onMouseMove;
    private Action<Vector2> m_onMouseWheel;

    public bool KeyboardEnabled
    {
        get { return m_isKeyboardEnabled; }
        set { m_isKeyboardEnabled = value; }
    }

    public bool MouseEnabled
    {
        get { return m_isMouseEnabled; }
        set { m_isMouseEnabled = value; }
    }

    private void Awake()
    {
        m_isKeyboardEnabled = true;
        m_isMouseEnabled = true;

        m_onKeyUpEventMap = new Dictionary<KeyCode, Action>();
        m_onKeyDownEventMap = new Dictionary<KeyCode, Action>();
        m_onKeyEventMap = new Dictionary<KeyCode, Action>();

        m_onMouseButtonUpEventMap = new Dictionary<EPlayerInputMouseButttonType, Action>();
        m_onMouseButtonDownEventMap = new Dictionary<EPlayerInputMouseButttonType, Action>();
        m_onMouseButtonEventMap = new Dictionary<EPlayerInputMouseButttonType, Action>();
    }

    private void Update()
    {
        if (m_isKeyboardEnabled)
        {
            foreach (var onKeyUp in m_onKeyUpEventMap)
            {
                if (Input.GetKeyUp(onKeyUp.Key))
                {
                    onKeyUp.Value.Invoke();
                }
            }

            foreach (var onKeyDown in m_onKeyDownEventMap)
            {
                if (Input.GetKeyDown(onKeyDown.Key))
                {
                    onKeyDown.Value.Invoke();
                }
            }

            foreach (var onKey in m_onKeyEventMap)
            {
                if (Input.GetKey(onKey.Key))
                {
                    onKey.Value.Invoke();
                }
            }
        }

        if (m_isMouseEnabled)
        {
            foreach (var onMouseButtonUp in m_onMouseButtonUpEventMap)
            {
                if (onMouseButtonUp.Key == EPlayerInputMouseButttonType.Left)
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        onMouseButtonUp.Value.Invoke();
                    }
                }
                else if (onMouseButtonUp.Key == EPlayerInputMouseButttonType.Right)
                {
                    if (Input.GetMouseButtonUp(1))
                    {
                        onMouseButtonUp.Value.Invoke();
                    }
                }
                else if (onMouseButtonUp.Key == EPlayerInputMouseButttonType.Middle)
                {
                    if (Input.GetMouseButtonUp(2))
                    {
                        onMouseButtonUp.Value.Invoke();
                    }
                }
            }

            foreach (var onMouseButtonDown in m_onMouseButtonDownEventMap)
            {
                if (onMouseButtonDown.Key == EPlayerInputMouseButttonType.Left)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        onMouseButtonDown.Value.Invoke();
                    }
                }
                else if (onMouseButtonDown.Key == EPlayerInputMouseButttonType.Right)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        onMouseButtonDown.Value.Invoke();
                    }
                }
                else if (onMouseButtonDown.Key == EPlayerInputMouseButttonType.Middle)
                {
                    if (Input.GetMouseButtonDown(2))
                    {
                        onMouseButtonDown.Value.Invoke();
                    }
                }
            }

            foreach (var onMouseButton in m_onMouseButtonEventMap)
            {
                if (onMouseButton.Key == EPlayerInputMouseButttonType.Left)
                {
                    if (Input.GetMouseButton(0))
                    {
                        onMouseButton.Value.Invoke();
                    }
                }
                else if (onMouseButton.Key == EPlayerInputMouseButttonType.Right)
                {
                    if (Input.GetMouseButton(1))
                    {
                        onMouseButton.Value.Invoke();
                    }
                }
                else if (onMouseButton.Key == EPlayerInputMouseButttonType.Middle)
                {
                    if (Input.GetMouseButton(2))
                    {
                        onMouseButton.Value.Invoke();
                    }
                }
            }

            if (m_onMouseMove != null)
            {
                m_onMouseMove.Invoke(Input.mousePosition);
            }


            if (m_onMouseWheel != null)
            {
                m_onMouseWheel.Invoke(Input.mouseScrollDelta);
            }
        }
    }

    public void AddKeyUpCallback(KeyCode key, Action callback)
    {
        if (m_onKeyUpEventMap.ContainsKey (key))
        {
            m_onKeyUpEventMap[key] += callback;
        }
        else
        {
            m_onKeyUpEventMap.Add(key, callback);
        }
    }

    public void AddKeyDownCallback(KeyCode key, Action callback)
    {
        if (m_onKeyDownEventMap.ContainsKey(key))
        {
            m_onKeyDownEventMap[key] += callback;
        }
        else
        {
            m_onKeyDownEventMap.Add(key, callback);
        }
    }

    public void AddKeyCallback(KeyCode key, Action callback)
    {
        if (m_onKeyEventMap.ContainsKey(key))
        {
            m_onKeyEventMap[key] += callback;
        }
        else
        {
            m_onKeyEventMap.Add(key, callback);
        }
    }

    public void AddMouseButtonUpCallback(EPlayerInputMouseButttonType mouseButon, Action callback)
    {
        if (m_onMouseButtonUpEventMap.ContainsKey(mouseButon))
        {
            m_onMouseButtonUpEventMap[mouseButon] += callback;
        }
        else
        {
            m_onMouseButtonUpEventMap.Add(mouseButon, callback);
        }
    }

    public void AddMouseButtonDownCallback(EPlayerInputMouseButttonType mouseButon, Action callback)
    {
        if (m_onMouseButtonDownEventMap.ContainsKey(mouseButon))
        {
            m_onMouseButtonDownEventMap[mouseButon] += callback;
        }
        else
        {
            m_onMouseButtonDownEventMap.Add(mouseButon, callback);
        }
    }

    public void AddMouseButtonCallback(EPlayerInputMouseButttonType mouseButon, Action callback)
    {
        if (m_onMouseButtonEventMap.ContainsKey(mouseButon))
        {
            m_onMouseButtonEventMap[mouseButon] += callback;
        }
        else
        {
            m_onMouseButtonEventMap.Add(mouseButon, callback);
        }
    }

    public void AddOnMouseMoveCallback (Action<Vector3> callback)
    {
        if (m_onMouseMove == null)
        {
            m_onMouseMove = callback;
        }
        else
        {
            m_onMouseMove += callback;
        }
    }

    public void AddOnMouseWheelCallback(Action<Vector2> callback)
    {
        if (m_onMouseWheel == null)
        {
            m_onMouseWheel = callback;
        }
        else
        {
            m_onMouseWheel += callback;
        }
    }
}
