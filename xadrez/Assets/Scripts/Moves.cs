using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves
{
    public static Moves Instance { set; get; }
    public int x;
    public int y;

    public PeçaDefault peçaDoMove;


    public Moves(int x, int y, PeçaDefault peçaDoMove)
    {
        this.x = x;
        this.y = y;
        this.peçaDoMove = peçaDoMove;
    }

    void Start()
    {
        Instance = this;
    }
}