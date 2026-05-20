using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int vida = 100;

    public bool stuneado;
    public float tiempoStun = 0.35f;

    public bool invulnerable;
    public float tiempoInvulnerable = 0.6f;

    public float fuerzaKnockback = 2f;

    public void RecibirGolpe(int dano)
    {
        if (invulnerable)
        {
            Debug.Log("Player esta invulnerable");
            return;
        }

        Bloqueo bloqueo = GetComponent<Bloqueo>();

        if (bloqueo != null && bloqueo.EstaBloqueando())
        {
            Debug.Log("Player bloqueo el golpe");
            return;
        }

        vida -= dano;

        Animator animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetTrigger("hit");
        }

        AplicarStun();
        AplicarInvulnerabilidad();
        AplicarKnockback();

        Debug.Log("Vida Player: " + vida);

        if (vida <= 0)
        {
            vida = 0;
            Morir();
        }
    }

    public void AplicarStun()
    {
        stuneado = true;
        CancelInvoke("QuitarStun");
        Invoke("QuitarStun", tiempoStun);
    }

    void QuitarStun()
    {
        stuneado = false;
    }

    public void AplicarInvulnerabilidad()
    {
        invulnerable = true;
        CancelInvoke("QuitarInvulnerabilidad");
        Invoke("QuitarInvulnerabilidad", tiempoInvulnerable);
    }

    void QuitarInvulnerabilidad()
    {
        invulnerable = false;
    }

    void AplicarKnockback()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (enemy == null)
        {
            return;
        }

        Vector2 direccion =
            (
                transform.position -
                enemy.transform.position
            ).normalized;

        PlayerKnockback knock = GetComponent<PlayerKnockback>();

        if (knock != null)
        {
            knock.fuerzaGolpe = fuerzaKnockback;
            knock.AplicarGolpe(direccion);
        }
    }

    void Morir()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.PerderVida();
        }
    }
}