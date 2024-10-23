/*
Zadanie 4
Dodaj do skryptu LookAround ograniczenie obracania kamery do -90 i +90 stopni góra-dół (sprawdź metodę Mathf.Clamp 
(https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Mathf.Clamp.html) z API Unity).
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zad4x : MonoBehaviour
{
    // ruch wokół osi Y będzie wykonywany na obiekcie gracza, więc
    // potrzebna nam referencja do niego (konkretnie jego komponentu Transform)
    public Transform player;

    public float sensitivity = 200f;

    private float x_rotation = 0f;

    void Start()
    {
        // zablokowanie kursora na środku ekranu, oraz ukrycie kursora
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // pobieramy wartości dla obu osi ruchu myszy
        float mouseXMove = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseYMove = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // wykonujemy rotację wokół osi Y
        player.Rotate(Vector3.up * mouseXMove);

        // a dla osi X obracamy kamerę dla lokalnych koordynatów
        x_rotation -= mouseYMove;
        x_rotation = Mathf.Clamp(x_rotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(x_rotation, 0f, 0f);

    }
}