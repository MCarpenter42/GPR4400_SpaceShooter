using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls
{
    public Controls_Movement movement = new Controls_Movement();
    public Controls_Action action = new Controls_Action();
    public Controls_Menu menu = new Controls_Menu();
}

public class Controls_Movement
{
    public string forward = "w";
    public string back = "s";
    public string left = "a";
    public string right = "d";
    public string up = "space";
    public string down = "left ctrl";

    public string sprint = "left shift";
    public string lockRot = "x";
}

public class Controls_Action
{
    public string fire = "mouse 0";
    public string aim = "mouse 1";
}

public class Controls_Menu
{
    public string pause = "escape";
}
