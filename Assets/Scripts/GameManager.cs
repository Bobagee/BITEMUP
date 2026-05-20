using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int vidas = 3;

    public string escenaInicial = "Escena1";
    public string escenaGameOver = "GameOver";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PerderVida()
    {
        vidas--;

        Debug.Log("Vidas restantes: " + vidas);

        if (vidas > 0)
        {
            SceneManager.LoadScene(escenaInicial);
        }
        else
        {
            SceneManager.LoadScene(escenaGameOver);
        }
    }
}