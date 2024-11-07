/*
Zadanie 6
(Zadanie dotyczy poziomu z lab 04)
Stwórz nowy obiekt, który będzie obiektem przeszkodą, dodaj do niego tag. Stwórz z tego obiektu prefabrykat i
dodaj kilka instancji prefabrykatu do sceny w różnych miejscach poziomu. Dodaj do obiektu gracza skrypt, który
będzie zawierał kod sprawdzający czy doszło do kontaktu pomiędzy graczem a przeszkodą (można wyszukiwać obiekty
za pomocą tagu). Wyświetlaj komunikat o rozpoczęciu kontaktu w konsoli.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zad6 : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 8.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private float pushPower = 2.0f;

    private void Start()
    {
        // zakładamy, że komponent CharacterController jest już podpięty pod obiekt
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
        
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        // zgodnie ze wzorem y = (1/2 * g) * t-kwadrat, ale jednak w trybie play
        // okazuje się, że jest to zbyt wolne opadanie, więc zastosowano g * t-kwadrat
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    private void did_player_hit_obstacle(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Przeszkoda"))
        {
            Debug.Log("Gracz uderzył w obiekt z tagiem 'Przeszkoda'."); 
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        did_player_hit_obstacle(hit);

        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }
}