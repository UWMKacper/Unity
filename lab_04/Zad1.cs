/*
Zadanie 1
Wykorzystaj kod z listingu 1 i zmodyfikuj go tak, aby możliwe było:

określenie w inspektorze ilości obiektów losowych do wegenerowania,
pozycje x oraz z były pobierane adekwatnie dla obiektu platformy, do której skrypt jest dołączany (zakładamy, że tak będzie),
Wskazówka: https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Bounds.html

dodaj do swojego projektu nowe materiały, tak, aby było 5 różnych do wykorzystania i przydzielaj losowo materiał w trakcie tworzenia nowego obiektu.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Zad1 : MonoBehaviour
{
    List<Vector3> positions = new List<Vector3>();
    int objectCounter = 0;
    System.Random system_random = new System.Random();

    // Public
    public GameObject block;
    public float delay = 2f;
    public int number_objects_to_generate = 5;
    public Material[] materials;

    void Start()
    {
        // Ustal bounds dla plane
        MeshRenderer plane_mesh_renderer = GetComponent<MeshRenderer>();
        Bounds plane_bounds = plane_mesh_renderer.bounds;

        // Wyznaczenie granic dla plane bounds
        float plane_bounds_min_x = plane_bounds.min[0];
        float plane_bounds_max_x = plane_bounds.max[0];
        float plane_bounds_min_z = plane_bounds.min[2];
        float plane_bounds_max_z = plane_bounds.max[2];

        // Wygenerowanie lisy dla poprawnych x i z
        List<float> pozycje_x = Enumerable.Range(0, number_objects_to_generate)
                                  .Select(_ => (float)(system_random.NextDouble() * (plane_bounds_max_x - plane_bounds_min_x) + plane_bounds_min_x))
                                  .OrderBy(x => Guid.NewGuid())
                                  .ToList();
        List<float> pozycje_z = Enumerable.Range(0, number_objects_to_generate)
                                  .Select(_ => (float)(system_random.NextDouble() * (plane_bounds_max_z - plane_bounds_min_z) + plane_bounds_min_z))
                                  .OrderBy(x => Guid.NewGuid())
                                  .ToList();
        
        for(int i=0; i<number_objects_to_generate; i++)
        {
            this.positions.Add(
                new Vector3(pozycje_x[i], 
                2,
                pozycje_z[i]));
        }

        // uruchamiamy coroutine
        StartCoroutine(GenerujObiekt());
    }

    void Update()
    {
        
    }

    IEnumerator GenerujObiekt()
    {
        foreach(Vector3 pos in positions)
        {
            // Utworzenie nowego obiektu game object
            GameObject new_game_object =Instantiate(this.block, this.positions.ElementAt(this.objectCounter++), Quaternion.identity);
            // Dodaj losowy materiał do nowo utworzonego obiektu
            Renderer new_game_object_renderer = new_game_object.GetComponent<Renderer>();
            if (materials.Length > 0){
                new_game_object_renderer.material = materials[UnityEngine.Random.Range(0, materials.Length)];
            }

            yield return new WaitForSeconds(this.delay);
        }
        // zatrzymujemy coroutine
        StopCoroutine(GenerujObiekt());
    }
}