using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    public float world_tile_size;
    Object[] world_tiles;
    public float see_distance;
    int current_distance = 0;

    public Transform player;

    void Start()
    {
        world_tiles = Resources.LoadAll("map");
    }

    void Update()
    {
        if (player.transform.position.z + see_distance > current_distance * world_tile_size)
        {
            Transform new_world_tile = Instantiate(world_tiles[Random.Range(0, world_tiles.Length)] as GameObject,Vector3.forward* current_distance * world_tile_size, transform.rotation).transform;
            new_world_tile.parent = transform;
            current_distance++;
            new_world_tile.gameObject.AddComponent<DestroyDistance>().distance = see_distance;
            new_world_tile.gameObject.GetComponent<DestroyDistance>().player = player;
        }
    }
}
