using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JongJin
{
    public class GameManager
    {
        private float[] playersMoveDistance = { 0, 0, 0, 0 };
        public float dinosaurMoveDistance = -3;
        
        public float DinosaurMoveDistance { get { return dinosaurMoveDistance; } set { dinosaurMoveDistance = value; } }
    }
}
