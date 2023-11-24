using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
	[SerializeField] private string nomeDoLevelDeJogo;
	[SerializeField] private GameObject painelMenuInicial;
	[SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject painelTutorial;
    [SerializeField] private GameObject painelSobre;
     
    private Dictionary<string, GameObject> paineis = new();

    private void Start()
    {
        paineis.Add("menu", painelMenuInicial);
        paineis.Add("opcoes", painelOpcoes);
        paineis.Add("tutorial", painelTutorial);
        paineis.Add("sobre", painelSobre);
    }

    public void Jogar()
	{
		SceneManager.LoadScene(nomeDoLevelDeJogo);
	}

	private void FecharTudo() {
		painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(false);
        painelTutorial.SetActive(false);
        painelSobre.SetActive(false);
    }

    public void AbrirPainel(string nome)
    {
        FecharTudo();
        paineis[nome] .SetActive(true);
    }

	public void SairJogo()
	{
		Debug.Log("Sair do Jogo");
		Application.Quit();
	}


}