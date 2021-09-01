/*
    Script that provents objects from destroying between scenes and ruining the dinner
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ddol : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
