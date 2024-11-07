/*
Zadanie 2
Przygotuj prosty model drzwi przesuwanych poziomo, które będą otwierane jeżeli gracz znajdzie się odpowiednio blisko jednej ze stron drzwi.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zad2 : MonoBehaviour
{
    /*
    Dodanie dla drzwi wunkcjonalności automatycznego otwierania i zamykania. Jeżeli gracz znajdzie się w
    ich pobliżu to dzwi się otworzą. Jeżeli odejdzie to dzwi się zamknął.
    */

    public float speed = 2.0f;

    private bool player_is_close = false;
    private Vector3 basic_position;
    private Vector3 target_position;

    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Ustawienie początkowej i końcowej pozycji dla zakresu w jakim porusza się platforma.
        basic_position = transform.position;
        target_position = basic_position + new Vector3(1, 0, 0);

        // Ustawienie triggeru do drzwi
        BoxCollider trigger_do_drzwi = gameObject.AddComponent<BoxCollider>();
        trigger_do_drzwi.isTrigger = true;
        trigger_do_drzwi.size = new Vector3(5, 1, 10);
    }

    void FixedUpdate()
    {
        // Jeżeli gracz znajduje się na platformie to zaczyna się ona poruszać
        if (player_is_close)
        {
            Vector3 velocity = new Vector3(1, 0, 0) * speed * Time.deltaTime;

            if (transform.position[0] <= target_position[0])
            {
                transform.position = transform.position + velocity.normalized * speed * Time.deltaTime;
            }
        }
        else
        {
            Vector3 velocity = new Vector3(1, 0, 0) * speed * Time.deltaTime * -1;

            if (transform.position[0] >= basic_position[0])
            {
                transform.position = transform.position + velocity.normalized * speed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player podszedł do drzwi.");
            player_is_close = true;  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player odszedł od drzwi.");
            player_is_close = false;
        }
    }
}
