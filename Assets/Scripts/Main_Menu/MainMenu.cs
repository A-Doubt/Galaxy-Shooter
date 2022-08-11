using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            SceneManager.LoadScene(1);
        }
    }
}
