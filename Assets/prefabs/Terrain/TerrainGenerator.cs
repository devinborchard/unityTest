using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject ground;
    private float tileWidth = 1f;  // Set based on your tile sprite width
    private float tileHeight = 0.5f; // Usually half the width for proper isometric effect
    private List<GameObject> tiles = new List<GameObject>();
    
    private int[,,] map = {
        {
            {1,1,1,1,1,1},
            {1,0,0,1,1,1},
            {1,1,1,1,1,1},
            {1,0,1,0,1,1},
            {1,1,1,0,1,0}
        },
        {
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
            {0,0,0,0,0,0},
        }
    };

        // private int[,,] map = {
        // {
        //     {1,1,1},
        //     {1,1,1},

        // },
        // {
        //     {0,0,0},
        //     {0,0,0}
 
        // }
    // };   

    void Start()
    {
        GenerateMap();
        Destroy(gameObject);
    }


    void GenerateMap()
    {
        int levels = map.GetLength(0);
        int rows = map.GetLength(1);
        int cols = map.GetLength(2);

        for (int level = 0; level < levels; level++)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (map[level, row, col] == 1)
                    {
                        float worldX = (col + row) * (tileWidth / 2.0f);
                        float worldY = (col - row) * (tileHeight / 2.0f) + level * tileHeight;

                        GameObject tile = Instantiate(ground, new Vector3(worldX, worldY, 0), Quaternion.identity);
                        tiles.Add(tile);

                        SpriteRenderer sr = tile.GetComponent<SpriteRenderer>();
                        if (sr != null)
                        {
                            sr.sortingOrder = -col;
                        }
                    }
                }
            }
        }
    }

}
