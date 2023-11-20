using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorSom : MonoBehaviour
{
    [SerializeField] private AudioSource fundoMusical;
    [SerializeField] private Slider sliderVolume;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("VolumeMusical"))
        {
            float volumeSalvo = PlayerPrefs.GetFloat("VolumeMusical");
            fundoMusical.volume = volumeSalvo;
            sliderVolume.value = volumeSalvo;
        }
    }

    public void VolumeMusical(float value)
    {
        fundoMusical.volume = value;

        PlayerPrefs.SetFloat("VolumeMusical", value);
        PlayerPrefs.Save();
    }
}
