using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public int health = 1;
    public int attackPower = 1;
    public int magicPower = 1;
    public int speed = 1;
    public int range = 1;

    public int cost = 1;

    public GameObject tileOccupying;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setTile(GameObject tile){
        tileOccupying = tile;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
