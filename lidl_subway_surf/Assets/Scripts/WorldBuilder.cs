using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    //mapa
    public float world_tile_size;
    Object[] world_tiles;
    public float see_distance;
    int current_distance = 0;

    //clanky
    Object[] tiles;
    float current_tile_distance = 50;
    Vector3 last_tile = Vector3.one;

    public Transform player;

    void Start()
    {
        world_tiles = Resources.LoadAll("Map");
        tiles = Resources.LoadAll("Tiles");
    }

    void Update()
    {
        if (player.transform.position.z + see_distance > current_distance * world_tile_size)
        {
            Transform new_world_tile = Instantiate(world_tiles[Random.Range(0, world_tiles.Length)] as GameObject,Vector3.forward* current_distance * world_tile_size, transform.rotation).transform;
            new_world_tile.parent = transform;
            current_distance++;
            new_world_tile.gameObject.AddComponent<DestroyDistance>().distance = see_distance*1.5f;
            new_world_tile.gameObject.GetComponent<DestroyDistance>().player = player;
        }

        if (player.transform.position.z + see_distance >current_tile_distance)
        {
            Transform new_tile;
            while (true)
            {
                GameObject potential_tile = tiles[Random.Range(0, tiles.Length)] as GameObject;
                TileScript scr = potential_tile.GetComponent<TileScript>();
                if (scr.start.x * last_tile.x == 1 || scr.start.y * last_tile.y == 1 || scr.start.y * last_tile.y == 1)
                {
                    new_tile = Instantiate(potential_tile, Vector3.forward*(current_tile_distance+scr.distance/2), transform.rotation).transform;
                    break;
                }
            }
            new_tile.name = "_";
            new_tile.parent = transform;
            current_tile_distance+=new_tile.gameObject.GetComponent<TileScript>().distance;
           new_tile.gameObject.AddComponent<DestroyDistance>().distance = see_distance*1.5f;
            new_tile.gameObject.GetComponent<DestroyDistance>().player = player;
            last_tile = new_tile.gameObject.GetComponent<TileScript>().end;
        }
    }
}
