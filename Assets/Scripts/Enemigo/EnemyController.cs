using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator animator;

    public int vida = 3;

    public bool stuneado;
    public float tiempoStun = 0.2f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void RecibirGolpe(int dano)
    {
        vida -= dano;

        if (animator != null)
        {
            Debug.Log("Enemy triggers hit");
            animator.SetTrigger("hit");
        }
        else
        {
            Debug.Log("No animator");
        }

            AplicarStun(tiempoStun);

        Debug.Log("Vida enemigo: " + vida);

        if (vida <= 0)
        {
            Morir();
        }
    }

    public void AplicarStun(float duracion)
    {
        stuneado = true;

        CancelInvoke("QuitarStun");
        Invoke("QuitarStun", duracion);
    }

    void QuitarStun()
    {
        stuneado = false;
    }

    void Morir()
    {
        Destroy(gameObject);
    }
}