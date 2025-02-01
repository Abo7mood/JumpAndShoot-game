using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] Enemy;
     Vector2 Randomization;
    void Start()
    {
        InvokeRepeating("InstanitateEnemies", 3, .5f);
    }

   public void InstanitateEnemies()
    {
        int rand = Random.Range(10, -10);
        int rand2 = Random.Range(0,2);
        Randomization = new Vector2(transform.position.x, rand);
        Instantiate(Enemy[rand2], Randomization, Quaternion.identity, null);
    }
}
