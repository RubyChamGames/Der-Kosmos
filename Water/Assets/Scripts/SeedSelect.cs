using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSelect : MonoBehaviour
{
    public int seed = -100000;
    public int SeedSelector(){
        if(seed == -100000){
            seed = Random.Range(-1000, 1000);
        }
        return seed;
    }


}
