using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum EUICanvas
{
    MainMenu, Loading, Play, Tutorial
}

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject m_mainMenuCanvas;
    [SerializeField]
    private GameObject m_loadingCanvas;
    [SerializeField]
    private GameObject m_playCanvas;
    [SerializeField]
    private GameObject m_tutorialCanvas;

    [Header("Loading UI")]
    [SerializeField]
    private string m_loadingText;
    [SerializeField]
    private string m_loadingCompleteText;
    [SerializeField]
    private Text m_loadingTextUI;
    [SerializeField]
    private UITextFlicker m_loadingTextUiFlicker;

    private void Start()
    {
        GameController gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        SceneController sceneController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneController>();

        sceneController.AddLoadEventCallback(ESceneLoadEventType.Start, (sceneName) => {
            if (sceneName.StartsWith(gameController.PlayScenePrefix))
            {
                m_loadingTextUI.text = m_loadingText;
                m_loadingTextUiFlicker.enabled = true;
            }
        });

        sceneController.AddLoadEventCallback (ESceneLoadEventType.Complete, (sceneName) => {
            if (sceneName.StartsWith (gameController.PlayScenePrefix))
            {
                m_loadingTextUI.text = m_loadingCompleteText;
                m_loadingTextUiFlicker.enabled = false;
            }
        });
    }

    public void SetCanvasActive (EUICanvas canvas, bool isActive)
    {
        GameObject target = GetCanvas(canvas);
        
        if (target)
        {
            target.SetActive(isActive);
        }
    }

    public void AddButtonCallback (EUICanvas canvas, string buttonName, UnityAction callback)
    {
        GameObject target = GetCanvas(canvas);

        if (target)
        {
            foreach (var button in target.GetComponentsInChildren<Button> (true))
            {
                if (buttonName == button.gameObject.name)
                {
                    button.onClick.AddListener(callback);
                    break;
                }
            }
        }
    }

    private GameObject GetCanvas (EUICanvas canvas)
    {
        switch (canvas)
        {
            case EUICanvas.MainMenu:
                return m_mainMenuCanvas;

            case EUICanvas.Loading:
                return m_loadingCanvas;

            case EUICanvas.Play:
                return m_playCanvas;

            case EUICanvas.Tutorial:
                return m_tutorialCanvas;
        }

        return null;
    }
}
