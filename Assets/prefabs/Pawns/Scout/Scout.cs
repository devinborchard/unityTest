using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : Pawn
{
    // Start is called before the first frame update
    void Awake()
    {
        health = 4;
        attackPower = 1;
        magicPower = 0;
        speed = 10;
        range = 1;

        cost = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
