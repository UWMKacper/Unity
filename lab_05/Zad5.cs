/*
Zadanie 5
(Zadanie dotyczy poziomu z lab 04)
Stwórz nowy obiekt na scenie imitujący płytę naciskową. Po wejściu na nią (kolizja ?) gracz powinien zostać 
wyrzucony w powietrze z trzykrotnie większą "siłą" niż w przypadku skoku.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zad5 : MonoBehaviour
{
    /*
    Gracz po wejściu na platformę zostanie wyrzucony w górę z 3 krotną siłą skoku.
    */

    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Zad5Player Zad5Player = other.gameObject.GetComponent<Zad5Player>();
            if (Zad5Player == null)
            {
                Debug.Log("Gracz musi mieć podpięty kontroler ze skryptu Zad5_player, aby obsłużyć potrójny skok na platformie.");
                return;
            }
            Zad5Player.should_tripple_jump = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Zad5Player Zad5Player = other.gameObject.GetComponent<Zad5Player>();
            if (Zad5Player == null)
            {
                Debug.Log("Gracz musi mieć podpięty kontroler ze skryptu Zad5_player, aby obsłużyć potrójny skok na platformie.");
                return;
            }
            Zad5Player.should_tripple_jump = false;
        }
    }
}
