using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleric : Pawn
{
    // Start is called before the first frame update
    void Awake()
    {
        health = 6;
        attackPower = 1;
        magicPower = 3;
        speed = 3;
        range = 1;

        cost = 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
