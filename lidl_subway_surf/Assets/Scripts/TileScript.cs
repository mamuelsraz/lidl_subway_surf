using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public float distance = 200;
    public Vector3 start = Vector3.one;
    public Vector3 end = Vector3.one;

    public WorldBuilder wb;
    public Transform player;

    void Update()
    {
        if (transform.position.z + distance < player.position.z) wb.Reset_tile(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(15, 1, distance));
    }
}
