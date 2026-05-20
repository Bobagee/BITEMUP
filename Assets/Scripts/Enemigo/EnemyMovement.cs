using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    public bool activo;

    public float velocidad = 2.3f;
    public float distanciaDetenerse = 1.15f;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (player == null)
        {
            GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");

            if (objPlayer != null)
            {
                player = objPlayer.transform;
            }
        }
    }

    void Update()
    {
        if (!activo)
        {
            if (animator != null)
            {
                animator.SetBool("isMoving", false);
            }

            return;
        }

        EnemyController controller = GetComponent<EnemyController>();

        if (controller != null && controller.stuneado)
        {
            if (animator != null)
            {
                animator.SetBool("isMoving", false);
            }

            return;
        }

        Bloqueo bloqueo = GetComponent<Bloqueo>();

        if (bloqueo != null && bloqueo.EstaBloqueando())
        {
            if (animator != null)
            {
                animator.SetBool("isMoving", false);
            }

            return;
        }

        PerseguirJugador();
    }

    public void ActivarEnemigo()
    {
        activo = true;
    }

    public void PerseguirJugador()
    {
        if (player == null)
        {
            return;
        }

        float distancia = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distancia > distanciaDetenerse)
        {
            Vector2 direccion =
                player.position - transform.position;

            direccion = direccion.normalized;

            transform.position +=
                (Vector3)(direccion * velocidad * Time.deltaTime);

            if (animator != null)
            {
                animator.SetBool("isMoving", true);
            }

            if (direccion.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (direccion.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        else
        {
            if (animator != null)
            {
                animator.SetBool("isMoving", false);
            }
        }
    }
}