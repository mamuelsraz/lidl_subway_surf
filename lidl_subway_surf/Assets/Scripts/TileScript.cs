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
        if (Vector3.Distance(player.position, transform.position) > distance) wb.Reset_tile(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(15, 1, distance));
    }
}
