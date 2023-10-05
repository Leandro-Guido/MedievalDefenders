using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public Transform pause;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private string mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(pause.gameObject.activeSelf){
                pause.gameObject.SetActive(false);
                Time.timeScale = 1;
            }else{
                pause.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void RetornarGame(){
        pause.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void SairGame(){
        SceneManager.LoadScene(mainMenu);
    }

    public void OpcoesGame()
    {
        pause.gameObject.SetActive(false);
        painelOpcoes.SetActive(true);
    }
    public void FechaOpcoesGame()
    {
        painelOpcoes.SetActive(false);
        pause.gameObject.SetActive(true);
  
    }
}
