using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float timeOut = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("DestroyWhenTimeOut", timeOut);

    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);
    }

    void DestroyWhenTimeOut()
    {
        Destroy(this.gameObject);
    }
}
