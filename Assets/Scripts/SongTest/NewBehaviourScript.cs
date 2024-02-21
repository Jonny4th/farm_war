using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public class Player
    {
        public float experience;
        public float h;

        public Player(int exp)
        {
            experience = exp;
        }

        // การนิยาม implicit operator สำหรับการแปลง Player เป็น Score
        public static implicit operator int(Player player)
        {
            // สร้าง Score โดยใช้ค่า experience ของ Player

            return (int)player.experience;
        }

    }

    public class Score
    {
        public int value;
        public int g = 0;
        public Score(int val)
        {
            value = val;
        }
    }
    public class Score1
    {
        public int value;
        public int g = 0;
        public Score1(int val)
        {
            value = val;
        }
    }
    void Start()
    {
        Player player = new Player(100);
        int g = player;
        // Score score = player;
        // Implicit conversion from Player to Score
        Debug.Log("value: " + g);
        Debug.Log("type: " + g.GetType());
    }

    // Update is called once per frame
    void Update()
    {

    }

}
