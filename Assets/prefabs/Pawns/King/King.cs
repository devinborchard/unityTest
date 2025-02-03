using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Pawn
{
    // Start is called before the first frame update
    void Awake()
    {
        health = 15;
        attackPower = 0;
        magicPower = 0;
        speed = 3;
        range = 0;

        cost = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
