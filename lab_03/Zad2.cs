/*
Zadanie 2
Stwórz nową scenę. Dodaj do niej obiekt typu Cube o wymiarach (2, 1, 1). Napisz skrypt, z publicznym polem speed (float), 
który będzie przemieszczał obiekt wzdłóż osi x o 10 jednostek i w momencie wykonania takiego przesunięcia będzie wykonywał 
ruch powrotny.
*/


using UnityEngine;

public class Zad2 : MonoBehaviour
{
    public float speed = 2.0f;

    private int direction = 1;
    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // tworzymy wektor prędkości
        Vector3 velocity = new Vector3(1, 0, 0);
        velocity = velocity.normalized * speed * Time.deltaTime;

        rb.MovePosition(transform.position + velocity * direction);
    }

    void FixedUpdate()
    {
        if (transform.position[0] >= 10)
        {
            direction = -1;
        }
        else if (transform.position[0] <= 0)
        {
            direction = 1;
        }
    }

}