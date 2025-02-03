using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Pawn
{
    // Start is called before the first frame update
    void Awake()
    {
        health = 10;
        attackPower = 4;
        magicPower = 0;
        speed = 5;
        range = 1;

        cost = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
