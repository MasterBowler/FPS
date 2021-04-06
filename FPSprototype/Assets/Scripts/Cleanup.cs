using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanup : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            Destroy(gameObject);
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            Destroy(gameObject);
    }
}
