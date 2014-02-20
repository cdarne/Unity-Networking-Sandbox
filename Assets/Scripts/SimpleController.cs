using UnityEngine;
using System.Collections;

public class SimpleController : MonoBehaviour
{
    public float speed = 20f;

    void Awake()
    {
        enabled = networkView.isMine;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(h, v, 0) * speed * Time.deltaTime);
    }
}
