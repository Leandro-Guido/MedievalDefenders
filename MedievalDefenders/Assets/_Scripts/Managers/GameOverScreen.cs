using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{    
    [SerializeField] private string nomeDoNivel;
    public void Restart()
	{
		SceneManager.LoadScene(nomeDoNivel);
	}

    public void Creditos()
	{
		SceneManager.LoadScene("Creditos");
	}
}
