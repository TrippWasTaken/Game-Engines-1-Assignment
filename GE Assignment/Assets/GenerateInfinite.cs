using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Tile
{
    public GameObject theTile;
    public float creationTime;

    public Tile(GameObject tile, float time)
    {
        theTile = tile;
        creationTime = time;
    }
}
public class GenerateInfinite : MonoBehaviour
{

    public GameObject plane;
    public GameObject cam;

    public int planeSize = 10;
    public int halfTilesX = 5;
    public int halfTilesZ = 10;

    Vector3 startPos;
    Hashtable tiles = new Hashtable();

    void Start()
    {
        this.gameObject.transform.position = Vector3.zero;
        startPos = Vector3.zero;

        float updateTime = Time.realtimeSinceStartup;

        for (int x = -halfTilesX; x < halfTilesX; x++)
        {
            for (int z = -halfTilesZ; z < halfTilesZ; z++)
            {
                Vector3 pos = new Vector3((x * planeSize + startPos.x), 0, (z * planeSize + startPos.z));
                GameObject tileN = (GameObject)Instantiate(plane, pos, Quaternion.identity);

                string tilename = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();
                tileN.name = tilename;
                Tile tile = new Tile(tileN, updateTime);
                tiles.Add(tilename, tile);
            }
        }
    }


    void Update()
    {
        int zMove = (int)(cam.transform.position.z - startPos.z);

        if(Mathf.Abs(zMove) >= planeSize)
        {
            float updateTime = Time.realtimeSinceStartup;

            int camZ = (int)(Mathf.Floor(cam.transform.position.z / planeSize) * planeSize);

            for (int x = -halfTilesX; x < halfTilesX; x++)
            {
                for (int z = -halfTilesZ; z < halfTilesZ; z++)
                {
                    Vector3 pos = new Vector3((x * planeSize + startPos.x), 0, (z * planeSize + startPos.z));

                    string tilename = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();

                    if (!tiles.ContainsKey(tilename))
                    {
                        GameObject tileN = (GameObject)Instantiate(plane, pos, Quaternion.identity);

                        tileN.name = tilename;
                        Tile tile = new Tile(tileN, updateTime);
                        tiles.Add(tilename, tile);
                    }
                    else
                    {
                        (tiles[tilename] as Tile).creationTime = updateTime;
                    }
                   
                }
            }

            Hashtable newTerrain = new Hashtable();
            foreach (Tile t in tiles.Values)
            {
                if (t.creationTime != updateTime)
                {
                    Destroy(t.theTile);
                }
                else
                {
                    newTerrain.Add(t.theTile.name, t);
                }
            }
            tiles = newTerrain;

            startPos = cam.transform.position;
        }
    }
}
