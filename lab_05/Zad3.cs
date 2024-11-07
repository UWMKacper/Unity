/*
Zadanie 3
Z przykładów z zajęć oraz zadania 1 przygotuj skrypt, który pozwoli na obsłużenie platformy, 
która może poruszać się dowolnie w przestrzeni od punktu do punktu. Punkty (w postaci obiektu Vector3)
 są przechowywane w dowolnej wybranej kolekcji. Wypróbuj możliwość dodawania kolejnych waypointów 
 poprzez panel Inspektor. Platforma porusza się od pierwszego do kolejnego punktu i jak dotrze do 
 ostatniego punktu, zawraca (czyli podąża tą samą drogą w przeciwnym kierunku).
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zad3 : MonoBehaviour
{
    /*
    Dodanie dla platformy funkcjonalności poruszania się od punktu początkowego do wielu punktów,
    w momencie gdy gracz na nią wskoczy.
    */

    public float speed = 2.0f;
    public List<Vector3> position_list = new List<Vector3>();

    private bool player_on_platform = false;
    private int item_index = 1;

    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Ustawienie początkowej i końcowej pozycji dla zakresu w jakim porusza się platforma.
        Vector3 basic_position = transform.position;

        position_list.Add(basic_position);
        position_list.Add(basic_position + new Vector3(10, 0, 0));

        // Ustawienie triggeru do platformy
        BoxCollider trigger_do_platformy = gameObject.AddComponent<BoxCollider>();
        trigger_do_platformy.isTrigger = true;
        trigger_do_platformy.center = new Vector3(0, 1, 0);
        trigger_do_platformy.size = new Vector3(1, 1, 1);
    }

    int select_item_index(int index)
    {
        int new_index = index + 1;
        Debug.Log("Nowy index: " + new_index);
        if (new_index > position_list.Count-1)
        {
            new_index = 0;
        }
        Debug.Log("Ostateczny index: " + new_index);

        return new_index;
    }

    void FixedUpdate()
    {
        // Jeżeli gracz nie jest na platfomie to jej nie poruszamy
        if (!player_on_platform)
        {
            return;
        }

        Vector3 target_position = position_list[item_index];
        transform.position = Vector3.MoveTowards(transform.position, target_position, speed * Time.deltaTime);

        // Sprawdzenie czy platforma doszła do wyznaczonego punktu
        if (Vector3.Distance(transform.position, target_position) < 0.01f)
        {
            item_index = select_item_index(item_index);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player wszedł na platformę.");
            player_on_platform = true;  

            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player zszedł z platformy.");
            player_on_platform = false;

            other.transform.SetParent(null);
        }
    }
}
