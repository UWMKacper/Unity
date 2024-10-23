/*
Zadanie 3
Do obiektu Cube z zadania 2 dodaj jakiś element, który będzie wskazywał na jej kierunek forward. 
Rozbuduj skrypt z zadania 2 (ale zapisz wszystko w nowym skrypcie), tak aby obiekt poruszał się 
'po kwadracie' o boku 10 jednostek i docierając do wierzchołka wykonywał obrót o 90 stopni w 
kierunku kolejnego ruchu.
*/


using UnityEngine;

public class Zad3 : MonoBehaviour
{
    public float speed = 2.0f;
    
    // Velocity vector parameters
    private int velocity_x = 1;
    private int velocity_y = 0;
    private int velocity_z = 0;

    // Target vector parameters
    private int target_x = 10;
    private float target_y = 0.5f;
    private int target_z = 0;

    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // tworzymy wektor prędkości
        Vector3 velocity = new Vector3(velocity_x, velocity_y, velocity_z);
        Vector3 target = new Vector3(10, 0, 0);
        velocity = velocity.normalized * speed * Time.deltaTime;

        rb.MovePosition(transform.position + velocity);
    }

    void FixedUpdate()
    {
        Vector3 rotationToAdd = new Vector3(0, 90, 0);
        if (velocity_x == 1 && transform.position[0] >= 10)
        {
            velocity_x = 0;
            velocity_z = 1;
            transform.Rotate(rotationToAdd);

            target_x = 10;
            target_z = 10;
        }
        else if (velocity_z == 1 && transform.position[2] >= 10)
        {
            velocity_x = -1;
            velocity_z = 0;
            transform.Rotate(rotationToAdd);

            target_x = 0;
            target_z = 10;
        }
        else if (velocity_x == -1 && transform.position[0] <= 0) 
        {
            velocity_x = 0;
            velocity_z = -1;
            transform.Rotate(rotationToAdd);

            target_x = 0;
            target_z = 0;
        }
        else if (velocity_z == -1 && transform.position[2] <= 0) 
        {
            velocity_x = 1;
            velocity_z = 0;
            transform.Rotate(rotationToAdd);

            target_x = 10;
            target_z = 0;
        }
    }

    void OnDrawGizmos()
    {
        Vector3 target = new Vector3(target_x, target_y, target_z);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, target);
    }
}