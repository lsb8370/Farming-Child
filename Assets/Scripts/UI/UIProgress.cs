using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIProgress : MonoBehaviour
{
    [SerializeField]
    private Image m_image;

    private SceneController m_sceneController;

    private void Start()
    {
        m_sceneController = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneController>();
    }

    private void Update()
    {
        m_image.fillAmount = m_sceneController.IsLoading ? m_sceneController.Progress : 1.0f;
    }
}
