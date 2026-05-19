using UnityEngine;

public class EnemyDefense : MonoBehaviour
{

    public float probabilidadBloqueo = 0.25f;
    public float duracionBloqueo = 3.5f;

    private Bloqueo bloqueo;

    void Start()
    {
        bloqueo = GetComponent<Bloqueo>();
    }

    public void IntentarBloquear()
    {
        if (bloqueo == null)
        {
            return;
        }

        if (bloqueo.EstaBloqueando())
        {
            return;
        }

        float suerte = Random.value;

        if (suerte <= probabilidadBloqueo)
        {
            bloqueo.ActivarBloqueo();

            Debug.Log("Enemy decidio bloquear");

            CancelInvoke("QuitarBloqueo");
            Invoke("QuitarBloqueo", duracionBloqueo);
        }
    }

    void QuitarBloqueo()
    {
        if (bloqueo != null)
        {
            bloqueo.DesactivarBloqueo();

            Debug.Log("Enemy dejo de bloquear");
        }
    }
}