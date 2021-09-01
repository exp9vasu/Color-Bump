using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject RestartPanel;
    public GameObject WinPanel;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
                
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowWinPanel()
    {
        WinPanel.SetActive(true);
    }

    public void ShowRetryPanel()
    {
        RestartPanel.SetActive(true);
    }
}
