using UnityEngine;
using System.Collections.Generic;

public class EnemyHitbox : MonoBehaviour
{
    public int dano = 1;

    private BoxCollider2D col;

    private List<PlayerHealth> golpeados =
        new List<PlayerHealth>();

    void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
    }

    public void Activar()
    {
        golpeados.Clear();
        col.enabled = true;
    }

    public void Desactivar()
    {
        col.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        RevisarGolpe(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        RevisarGolpe(other);
    }

    void RevisarGolpe(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();

            if (player != null && !golpeados.Contains(player))
            {
                golpeados.Add(player);
                player.RecibirGolpe(dano);

                Debug.Log("Enemy golpeo al player");
            }
        }
    }
}