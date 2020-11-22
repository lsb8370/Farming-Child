using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ESceneLoadEventType
{
    Start, Complete, Loading
}

public class SceneController : MonoBehaviour
{
    private bool m_isLoading;
    private float m_progress;

    private Action<string> m_onLoadStart;
    private Action<string> m_onLoadComplete;
    private Action<string> m_onLoading;

    public bool IsLoading
    {
        get { return m_isLoading; }
    }

    public float Progress
    {
        get { return m_progress; }
    }

    public string ActiveScene
    {
        get { return SceneManager.GetActiveScene().name; }
        set { SceneManager.SetActiveScene(SceneManager.GetSceneByName(value)); }
    }

    private void Awake()
    {
        m_isLoading = false;
        m_progress = 0.0f;
    }

    public void AddLoadEventCallback(ESceneLoadEventType type, Action<string> callback)
    {
        switch (type)
        {
            case ESceneLoadEventType.Start:
                if (m_onLoadStart != null)
                {
                    m_onLoadStart += callback;
                }
                else
                {
                    m_onLoadStart = callback;
                }
                break;

            case ESceneLoadEventType.Complete:
                if (m_onLoadComplete != null)
                {
                    m_onLoadComplete += callback;
                }
                else
                {
                    m_onLoadComplete = callback;
                }
                break;

            case ESceneLoadEventType.Loading:
                if (m_onLoading != null)
                {
                    m_onLoading += callback;
                }
                else
                {
                    m_onLoading = callback;
                }
                break;
        }
    }

    public void LoadScene (string name)
    {
        if (m_isLoading)
        {
            return;
        }

        m_isLoading = true;
        m_progress = 0.0f;

        StartCoroutine(LoadSceneCoroutine(name));
    }

    private IEnumerator LoadSceneCoroutine (string name)
    {
        var loadAsync = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

        if (m_onLoadStart != null)
        {
            m_onLoadStart.Invoke (name);
        }

        while (loadAsync.isDone == false)
        {
            m_progress = loadAsync.progress;

            if (m_onLoading != null)
            {
                m_onLoading.Invoke(name);
            }

            yield return null;
        }

        if (m_onLoadComplete != null)
        {
            m_onLoadComplete.Invoke(name);
        }

        m_isLoading = false;
    }
}
