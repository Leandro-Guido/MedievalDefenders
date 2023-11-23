using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticesHolder : MonoBehaviour
{
    public List<Transform> lista_vertices = new List<Transform>();
    public List<float> lista_heuristica = new List<float>();
    public static VerticesHolder instance;

    public void Start()
    {
        foreach (Transform t in lista_vertices)
        {
            lista_heuristica.Add(Vector2.Distance(t.position, lista_vertices[13].position));
        }
        SingletonExample.Instance.heuristics = lista_heuristica;
    }
}
