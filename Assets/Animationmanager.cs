using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animationmanager : MonoBehaviour {

    private static Animationmanager instance;

    private Animationmanager() { }

   

    public static Animationmanager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Animationmanager();
            }
            return instance;
        }
    }
}
