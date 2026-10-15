using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovePj : MonoBehaviour
{
    [Header("Velocidad del jugador")]
    public float moveSpeed = 3f; // Puedes cambiarla desde el Inspector

    private Rigidbody2D rb;
    private Vector2 movement;


    void Start()
    {
        // Obtiene el Rigidbody2D del personaje
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Captura el input del teclado
        movement.x = Input.GetAxisRaw("Horizontal"); // A/D o flechas izquierda/derecha
        movement.y = Input.GetAxisRaw("Vertical");   // W/S o flechas arriba/abajo

        // Normaliza para que moverse diagonal no sea más rápido
        movement = movement.normalized;
        
       
    }

    void FixedUpdate()
    {
        // Mueve al personaje usando Rigidbody2D
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}