using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandController : MonoBehaviour
{
    private static float[] playerSpeeds = { 0, 0 };
    public static float bandZ = 0.0f;

    private void Start()
    {
        
    }

    private void Update()
    {
        CurPosX();
        transform.position = new Vector3(transform.position.x, transform.position.y, bandZ + 1f);
    }

    public static void SetX(int index, float z)
    {
        playerSpeeds[index] = z;
    }

    private void CurPosX()
    {
        bandZ = Mathf.Max(playerSpeeds[0], playerSpeeds[1]);
    }
}
