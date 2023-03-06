using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    public string _name;
    public int _score;

    public User(string name, int score)
    {
        _name = name;
        _score = score;
    }
}
