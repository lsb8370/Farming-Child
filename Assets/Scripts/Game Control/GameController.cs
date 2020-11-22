using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameState
{
    None, Ready, Play
}

public class GameController : MonoBehaviour
{
    [Header("Play")]
    [SerializeField]
    private string m_playScenePrefix;
    [SerializeField]
    private string m_playSceneName;
    [SerializeField]
    private string m_playSceneRuleName;

    [Header ("UI")]
    [SerializeField]
    private string m_uiSceneName;
    [SerializeField]
    private string m_uiControllerName;

    private EGameState m_gameState;
    private bool m_isNewGame;
    private SceneController m_sceneController;
    private GridManager m_gridManager;
    private GameDataManager m_gameDataManager;
    private PlayerInput m_playerInput;
    private UIController m_uiController;
    private GameRule m_gameRule;

    public string PlayScenePrefix
    {
        get { return m_playScenePrefix; }
    }

    private void Awake()
    {
        m_gameState = EGameState.None;
    }

    private void Start()
    {
        Camera.main.clearFlags = CameraClearFlags.SolidColor;

        m_sceneController = GetComponent<SceneController>();
        m_gridManager = GetComponent<GridManager>();
        m_gameDataManager = GetComponent<GameDataManager>();

        m_playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();

        m_sceneController.AddLoadEventCallback(ESceneLoadEventType.Start, OnLoadSceneStart);
        m_sceneController.AddLoadEventCallback(ESceneLoadEventType.Complete, OnLoadSceneComplete);
        m_sceneController.LoadScene(m_uiSceneName);
    }

    private void Update()
    {
        if (m_gameState == EGameState.Ready)
        {
            if (Input.anyKey)
            {
                PlayGame();
            }
        }
        else if (m_gameState == EGameState.Play)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SaveGame();
            }
        }
    }

    public void StartNewGame ()
    {
        m_isNewGame = true;

        m_gameDataManager.ResetData();
        m_sceneController.LoadScene(m_playSceneName);
    }

    public void SaveGame ()
    {
        m_gameDataManager.Save("save.json");
    }

    public void ContinueGame ()
    {
        m_isNewGame = false;
        m_sceneController.LoadScene(m_playSceneName);
    }

    public void PlayGame ()
    {
        m_gameState = EGameState.Play;

        m_uiController.SetCanvasActive(EUICanvas.Loading, false);
        m_uiController.SetCanvasActive(EUICanvas.Play, true);

        m_playerInput.KeyboardEnabled = true;
        m_playerInput.MouseEnabled = true;
    }

    public void ExitGame ()
    {
        Application.Quit();
    }

    private void OnLoadSceneStart(string sceneName)
    {
        if (sceneName.StartsWith (m_playScenePrefix))
        {
            m_uiController.SetCanvasActive(EUICanvas.MainMenu, false);
            m_uiController.SetCanvasActive(EUICanvas.Loading, true);

            m_playerInput.KeyboardEnabled = false;
            m_playerInput.MouseEnabled = false;
        }
    }

    private void OnLoadSceneComplete(string sceneName)
    {
        if (sceneName == m_uiSceneName)
        {
            m_uiController = GameObject.Find(m_uiControllerName).GetComponent<UIController>();

            m_uiController.SetCanvasActive(EUICanvas.MainMenu, true);
            m_uiController.SetCanvasActive(EUICanvas.Play, false);
            m_uiController.SetCanvasActive(EUICanvas.Tutorial, false);

            m_uiController.AddButtonCallback(EUICanvas.MainMenu, "Button_Quit", ExitGame);
            m_uiController.AddButtonCallback(EUICanvas.MainMenu, "Button_New", StartNewGame);
            m_uiController.AddButtonCallback(EUICanvas.MainMenu, "Button_Continue", ContinueGame);

            Camera.main.clearFlags = CameraClearFlags.Skybox;
        }

        if (sceneName.StartsWith(m_playScenePrefix))
        {
            m_sceneController.ActiveScene = sceneName;
            m_gameRule = GameObject.Find(m_playSceneRuleName).GetComponent<GameRule>();
            
            if (m_isNewGame)
            {
                m_gridManager.CreateGrid(m_gameRule.GridSize);
            }
            else
            {
                if (m_gameDataManager.Load("save.json") == false)
                {
                    return;
                }
            }

            m_gameState = EGameState.Ready;
        }
    }
}
