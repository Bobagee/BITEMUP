using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLevelExit : MonoBehaviour
{
    public string siguienteEscena;

    private EnemyController boss;

    void Start()
    {
        boss = GetComponent<EnemyController>();
    }

    void Update()
    {
        if (boss == null)
        {
            SceneManager.LoadScene(siguienteEscena);
        }
    }
}