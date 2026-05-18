using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int vida = 3;

    public bool stuneado;
    public float tiempoStun = 0.2f;

    public void RecibirGolpe(int dano)
    {
        vida -= dano;

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