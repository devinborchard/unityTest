using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Pawn
{
    // Start is called before the first frame update
    void Awake()
    {
        health = 5;
        attackPower = 2;
        magicPower = 0;
        speed = 3;
        range = 5;

        cost = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
