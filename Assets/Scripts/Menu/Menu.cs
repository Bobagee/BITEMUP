using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    //Start 
   public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Debug.Log("Saliendo");
        Application.Quit();
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
