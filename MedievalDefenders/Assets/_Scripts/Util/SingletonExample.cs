using System.Collections.Generic;
using UnityEngine;

public class SingletonExample : MonoBehaviour
{
    // Static instance of the singleton
    private static SingletonExample _instance;
    //variavel publica array
    public List <float> heuristics = new List<float>();
    // Public property to access the singleton instance
    public static SingletonExample Instance
    {
        get
        {
            // If the instance doesn't exist, create it
            if (_instance == null)
            {
                GameObject singletonObject = new GameObject("SingletonExample");
                _instance = singletonObject.AddComponent<SingletonExample>();
            }

            return _instance;
        }
    }

    // Optional: Add other variables and methods as needed

    private void Awake()
    {
        // Ensure there is only one instance of the singleton
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Optional: Add other MonoBehaviour methods or custom methods as needed
}