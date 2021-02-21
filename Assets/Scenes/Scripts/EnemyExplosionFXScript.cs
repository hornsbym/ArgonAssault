using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosionFXScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Removes the explosion fx automatically after 2 secs.
        Destroy(gameObject, 2f);
    }
}
