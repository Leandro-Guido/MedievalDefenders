using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Pause : MonoBehaviour
{
    [NonSerialized] public static Pause main;

    public GameObject pause;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private string mainMenu;

    private void Awake()
    {
        main = this;    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause.activeSelf)
            {
                RetornarGame();
            }
            else
            {
                if (painelOpcoes.activeSelf)
                {
                    FechaOpcoesGame();
                }
                else 
                {
                    PausarGame();
                }
            }
        }
    }

    public void PausarGame()
    {
        pause.SetActive(true);
        Time.timeScale = 0;
    }

    public void RetornarGame()
    {

        pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void SairGame()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void OpcoesGame()
    {
        pause.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void FechaOpcoesGame()
    {
        painelOpcoes.SetActive(false);
        pause.SetActive(true);
    }

    public bool IsPaused ()
    {
        return pause.activeSelf || painelOpcoes.activeSelf;
    }
}
