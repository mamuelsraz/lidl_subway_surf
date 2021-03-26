using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldBuilder : MonoBehaviour
{
    //object pooling
    public int pool_size;
    GameObject[,] map_pool;
    GameObject[,] tile_pool;

    //mapa
    public float world_tile_size;
    public float see_distance;
    int current_distance = 0;

    //clanky
    float current_tile_distance = 50;
    Vector3 last_tile = Vector3.one;

    public Transform player;

    void Start()
    {
        GameObject[] map_types = Resources.LoadAll<GameObject>("Map");
        GameObject[] tile_types = Resources.LoadAll<GameObject>("Tiles");
        Inicialize_tiles(ref map_pool, map_types, pool_size);
        Inicialize_tiles(ref tile_pool, tile_types, pool_size);
    }

    /*void Update2()
    {
        if (player.transform.position.z + see_distance > current_distance * world_tile_size)
        {
            Transform new_world_tile = Instantiate(world_tiles[Random.Range(0, world_tiles.Length)],Vector3.forward* current_distance * world_tile_size, transform.rotation).transform;
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
                GameObject potential_tile = tiles[Random.Range(0, tiles.Length)];
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
    }*/

    private void Update()
    {
        if (player.transform.position.z + see_distance > current_distance * world_tile_size)
        {
            Spawn_map();
        }

        if (player.transform.position.z + see_distance > current_tile_distance)
        {
            //spawn tile
        }
    }

    void Spawn_map()
    {
        for (int i = 0; i < 200; i++)
        {
            GameObject potentional_tile = map_pool[Random.Range(0, map_pool.GetLength(0)), Random.Range(0, map_pool.GetLength(1))];

            if (!potentional_tile.activeSelf)
            {
                potentional_tile.transform.position = Vector3.forward * current_distance * world_tile_size;
                current_distance++;
                potentional_tile.SetActive(true);
                break;
            }
        }
    }

    void Inicialize_tiles(ref GameObject[,] pool, GameObject[] pool_parts, int pool_size)
    {
        pool = new GameObject[pool_parts.Length, pool_size];
        for (int i = 0; i < pool.GetLength(0); i++)
        {
            for (int ii = 0; ii < pool.GetLength(1); ii++)
            {
                GameObject obj = Instantiate(pool_parts[i], transform);
                pool[i, ii] = obj;
                Reset_tile(obj);
                if (obj.GetComponent<TileScript>() == null)
                {
                    TileScript script = obj.AddComponent<TileScript>();
                    script.player = player;
                    script.wb = this;
                    script.distance = see_distance * 1.5f;
                }
            }
        }
    }

    public void Reset_tile(GameObject obj)
    {
        obj.SetActive(false);
    }
}
