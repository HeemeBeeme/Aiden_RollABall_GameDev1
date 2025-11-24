using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;

    public Vector3 InitialPosition;
    public Vector3 ShakePosition;

    public bool shake = false;
    private float shakeMagnitude = 0.1f;

    void Start()
    {
        offset = transform.position - player.transform.position;
        InitialPosition = transform.localPosition;
    }
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;

        if (shake == true)
        {
            ShakePosition = Random.insideUnitSphere * shakeMagnitude;
            transform.localPosition += ShakePosition;
        }
    }
}
