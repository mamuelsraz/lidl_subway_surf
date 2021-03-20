using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDistance : MonoBehaviour
{
    public float distance;
    public Transform player;

    void Start()
    {
        
    }

    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) > distance) Destroy(gameObject);
    }
}
