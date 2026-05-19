using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake instance;

    private Vector3 posicionLocalOriginal;

    void Awake()
    {
        instance = this;
        posicionLocalOriginal = transform.localPosition;
    }

    public void Shake(float duracion, float intensidad)
    {
        StopAllCoroutines();
        StartCoroutine(HacerShake(duracion, intensidad));
    }

    IEnumerator HacerShake(float duracion, float intensidad)
    {
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            float x = Random.Range(-1f, 1f) * intensidad;
            float y = Random.Range(-1f, 1f) * intensidad;

            transform.localPosition =
                posicionLocalOriginal + new Vector3(x, y, 0f);

            tiempo += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = posicionLocalOriginal;
    }
}