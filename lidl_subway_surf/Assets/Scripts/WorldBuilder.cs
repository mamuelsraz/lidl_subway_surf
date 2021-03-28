using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldBuilder : MonoBehaviour
{
    //object pooling
    public int pool_size;
    TileScript[,] map_pool;
    TileScript[,] tile_pool;

    //mapa
    public float world_tile_size;
    public float see_distance;
    float current_distance = 0;

    //clanky
    float current_tile_distance = 70;
    Vector3 last_tile = Vector3.one;

    public Transform player;

    void Start()
    {
        GameObject[] map_types = Resources.LoadAll<GameObject>("Map");
        GameObject[] tile_types = Resources.LoadAll<GameObject>("Tiles");
        map_pool = Inicialize_tiles(map_types, pool_size);
        tile_pool = Inicialize_tiles(tile_types, pool_size);
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
        if (player.transform.position.z + see_distance > current_distance)
        {
            Vector3 troughput = Vector3.one;
            Spawn(map_pool, ref troughput, ref current_distance);
        }

        if (player.transform.position.z + see_distance > current_tile_distance)
        {
            Spawn(tile_pool, ref last_tile, ref current_tile_distance);
        }
    }

    void Spawn(TileScript[,] pool, ref Vector3 last_tile, ref float distance)
    {
        for (int i = 0; i < 200; i++)
        {
            TileScript potentional_tile = pool[Random.Range(0, pool.GetLength(0)), Random.Range(0, pool.GetLength(1))];
            Vector3 tile_pass = potentional_tile.start;
            if (!potentional_tile.gameObject.activeSelf && (tile_pass.x * last_tile.x == 1 || tile_pass.y * last_tile.y == 1 || tile_pass.y * last_tile.y == 1))
            {
                potentional_tile.transform.position = Vector3.forward * (distance + potentional_tile.distance/2);
                distance += potentional_tile.distance;
                potentional_tile.gameObject.SetActive(true);
                last_tile = potentional_tile.end;
                break;
            }
        }
    }

    TileScript[,] Inicialize_tiles(GameObject[] pool_parts, int pool_size)
    {
        TileScript[,] pool = new TileScript[pool_parts.Length, pool_size];
        for (int i = 0; i < pool.GetLength(0); i++)
        {
            for (int ii = 0; ii < pool.GetLength(1); ii++)
            {
                pool[i, ii] = Inicialize_tile(Instantiate(pool_parts[i], transform));
            }
        }
        return pool;
    }
    TileScript Inicialize_tile(GameObject new_tile)
    {
        TileScript script = new_tile.GetComponent<TileScript>();
        if (script == null)
        {
            script = new_tile.AddComponent<TileScript>();
            script.distance = 200;
            script.end = Vector3.one;
            script.start = Vector3.one;
        }
        script.player = player;
        script.wb = this;

        Reset_tile(script);
        return script;
    }

    public void Reset_tile(TileScript obj)
    {
        obj.gameObject.SetActive(false);
        foreach (Transform item in obj.transform)
        {
            item.gameObject.SetActive(true);
        }
    }
}
