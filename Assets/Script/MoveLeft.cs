using UnityEngine;

// Este script mueve cualquier objeto hacia la izquierda
public class MoveLeft : MonoBehaviour
{
    // Velocidad de movimiento
    public float speed = 5f;

    // Se ejecuta cada frame
    void Update()
    {
        // Mover hacia la izquierda
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
