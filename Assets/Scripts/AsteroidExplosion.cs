using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("ExplosionSound").GetComponent<AudioSource>().Play();
        Destroy(this.gameObject, 3.0f);
    }

}
