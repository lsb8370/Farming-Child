using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICreateButton : MonoBehaviour
{
    [SerializeField]
    private string m_name;

    private void Start()
    {
        Button button = GetComponent<Button>();
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

        button.onClick.AddListener(() => {
            player.StartCreate(m_name);
        });
    }
}
