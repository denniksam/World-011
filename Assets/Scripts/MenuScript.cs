using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject menuContent;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Time.timeScale = menuContent.activeInHierarchy ? 1.0f : 0.0f;
            menuContent.SetActive( ! menuContent.activeInHierarchy);            
        }
    }
}
