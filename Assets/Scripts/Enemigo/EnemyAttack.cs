using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public EnemyHitbox hitbox;

    public float rangoAtaque = 1.3f;
    public float tiempoEntreAtaques = 1.2f;
    public float duracionHitbox = 0.2f;

    private Transform player;
    private bool atacando;
    private float ultimoAtaque;

    void Start()
    {
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");

        if (objPlayer != null)
        {
            player = objPlayer.transform;
        }
    }

    void Update()
    {
        IntentarAtacar();
    }

    void IntentarAtacar()
    {
        if (player == null)
        {
            return;
        }

        EnemyController controller = GetComponent<EnemyController>();

        if (controller != null && controller.stuneado)
        {
            return;
        }

        Bloqueo bloqueo = GetComponent<Bloqueo>();

        if (bloqueo != null && bloqueo.EstaBloqueando())
        {
            return;
        }

        float distancia = Vector2.Distance(
            transform.position,
            player.position
        );

        if (distancia <= rangoAtaque && !atacando)
        {
            if (Time.time - ultimoAtaque >= tiempoEntreAtaques)
            {
                StartCoroutine(Atacar());
            }
        }
    }

    IEnumerator Atacar()
    {
        atacando = true;
        ultimoAtaque = Time.time;

        Debug.Log("Enemy ataca");

        yield return new WaitForSeconds(0.15f);

        if (hitbox != null)
        {
            hitbox.Activar();
        }

        yield return new WaitForSeconds(duracionHitbox);

        if (hitbox != null)
        {
            hitbox.Desactivar();
        }

        atacando = false;
    }
}