using UnityEngine;

public class EnemyDefense : MonoBehaviour
{
    public Animator animator;

    public float probabilidadBloqueo = 0.25f;
    public float duracionBloqueo = 3.5f;

    private Bloqueo bloqueo;

    void Start()
    {
        bloqueo = GetComponent<Bloqueo>();
        animator = GetComponent<Animator>();
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

            if (animator != null)
            {
                animator.SetBool("isBlocking", true);
            }

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

            if (animator != null)
            {
                animator.SetBool("isBlocking", false);
            }

            Debug.Log("Enemy dejo de bloquear");
        }
    }
}