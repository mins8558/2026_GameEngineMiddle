using UnityEngine;

public class ItemFloat : MonoBehaviour
{
    public float speed = 3f;
    public float height = 0.2f;
    Vector3 pos;

    void Start() { pos = transform.position; }

    void Update()
    {
        float newY = pos.y + Mathf.Sin(Time.time * speed) * height;
        transform.position = new Vector3(pos.x, newY, pos.z);
    }
}