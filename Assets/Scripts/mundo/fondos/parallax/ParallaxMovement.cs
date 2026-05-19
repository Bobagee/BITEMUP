using UnityEngine;

public class SimpleParallax : MonoBehaviour
{
    public Transform cameraRig;

    [Range(0f, 1f)]
    public float parallaxStrength = 0.5f;

    private Vector3 startPos;
    private Vector3 camStartPos;

    void Start()
    {
        if (cameraRig == null)
        {
            GameObject rig = GameObject.Find("CameraRig");

            if (rig != null)
            {
                cameraRig = rig.transform;
            }
        }

        startPos = transform.position;

        if (cameraRig != null)
        {
            camStartPos = cameraRig.position;
        }
    }

    void LateUpdate()
    {
        if (cameraRig == null)
        {
            return;
        }

        float distancia =
            cameraRig.position.x - camStartPos.x;

        transform.position = new Vector3(
            startPos.x + (distancia * parallaxStrength),
            startPos.y,
            startPos.z
        );
    }
}