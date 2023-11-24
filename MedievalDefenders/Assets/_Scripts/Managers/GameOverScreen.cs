using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{    
    [SerializeField] private string nomeDoNivel;
    [SerializeField] private TextMeshProUGUI score;

    void OnEnable()
    {
        int waves = PlayerPrefs.GetInt("waves");
        score.text = $"Sobreviveu {waves} rodadas";
    }

    public void Restart()
	{
		SceneManager.LoadScene(nomeDoNivel);
	}
}
