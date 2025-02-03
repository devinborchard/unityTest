using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject TerrainGenerator;
    public GameObject PawnPlacer;
    // Start is called before the first frame update
    void Start()
    {
        GameObject terrainGenerator = Instantiate(TerrainGenerator, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject pawnPlacer = Instantiate(PawnPlacer, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void BeginGame(){
        Debug.Log("Game starting");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
