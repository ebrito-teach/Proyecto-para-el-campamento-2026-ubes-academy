using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //==== Variables publicas =====
   public static GameManager instance;

   [Header("sistema de vida")]
   public int lives = 3;
   [Header("sistema de puntuacion")]
   public int score = 0 ;

    //===variales privadas====

    bool isGameActive = true;

    void Awake()
    {
        //so mp existe instancia esta instancia valga la redundancia
        if (instance ==null)
        {
            instance = this;
        }
        else
        {
            //si existe ya otra, la destruye, ya que solo
            //puede haber una instanca
            Destroy(gameObject);
        }
    }
    void Start()
    {
        //aqui vamos a inicializar los valores
        isGameActive = true;
        lives = 3;
        score = 0;
        // esto es para ver el mensaje en la consola
        Debug.Log("GameManager Iniciado - vida - lives");
    }

      
    // Llamar cuando el jugador pierde una vida
    public void LoseLife()
    {
        // Solo procesar si el juego está activo
        if (!isGameActive) return;
        
        // Reducir vidas
        lives--;
        
        Debug.Log("¡Perdiste una vida! Vidas restantes: " + lives);
        
        // Verificar si se acabaron las vidas
        if (lives <= 0)
        {
            Debug.Log("¡Game Over!");
            GameOver();

        }
    }

      
    // Llamar cuando el jugador recoge una moneda
    public void AddScore(int points)
    {
        // Solo procesar si el juego está activo
        if (!isGameActive) return;
        
        // Sumar puntos
        score += points;
        
        Debug.Log("Puntuación: " + score);
    }
    // Terminar el juego
    void GameOver()
    {
        isGameActive = false;
        
        Debug.Log("=== GAME OVER ===");
        Debug.Log("Puntuación final: " + score);
        
        // Esperar 2 segundos y reiniciar
        Invoke("RestartGame", 2f);
    }
    
    // Reiniciar la escena
    void RestartGame()
    {
        // Obtener el nombre de la escena actual
        string currentScene = SceneManager.GetActiveScene().name;
        
        // Recargar la escena
        SceneManager.LoadScene(currentScene);
    }
     // ===== GETTERS =====
    
    // Obtener si el juego está activo
    public bool IsGameActive()
    {
        return isGameActive;
    }
}

   

