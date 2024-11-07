/*
Zadanie 1
Przygotuj skrypt i przykład platformy poruszającej się horyzontalnie w momencie, w którym gracz na nią wejdzie.
Platforma ma ustalony punkt docelowy i po dotarciu do niego powinna wrócić do miejsca początkowego.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zad1 : MonoBehaviour
{
    /*
    Dodanie dla platformy funkcjonalności poruszania się od punktu początkowego do punktu końcowego,
    w momencie gdy gracz na nią wskoczy.
    */

    public float speed = 2.0f;

    private int direction = 1;
    private bool player_on_platform = false;
    private Vector3 basic_position;
    private Vector3 target_position;

    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Ustawienie początkowej i końcowej pozycji dla zakresu w jakim porusza się platforma.
        basic_position = transform.position;
        target_position = basic_position + new Vector3(10, 0, 0);

        // Ustawienie triggeru do platformy
        BoxCollider trigger_do_platformy = gameObject.AddComponent<BoxCollider>();
        trigger_do_platformy.isTrigger = true;
        trigger_do_platformy.center = new Vector3(0, 1, 0);
        trigger_do_platformy.size = new Vector3(1, 1, 1);
    }

    void FixedUpdate()
    {
        // Ustalenie kierunku poruszania się platformy
        if (transform.position[0] >= target_position[0])
        {
            direction = -1;
        }
        else if (transform.position[0] <= basic_position[0])
        {
            direction = 1;
        }

        // Jeżeli gracz znajduje się na platformie to zaczyna się ona poruszać
        if (player_on_platform)
        {
            Vector3 velocity = new Vector3(1, 0, 0) * speed * Time.deltaTime * direction;
            transform.position = transform.position + velocity.normalized * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Platform - on trigger enter");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player wszedł na windę.");
            player_on_platform = true;  
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Platform - on trigger exit");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player zszedł z windy.");
            player_on_platform = false;
            other.transform.SetParent(null);
        }
    }
}
