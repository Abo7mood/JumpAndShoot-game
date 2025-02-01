using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Vector2 enemyspeed;
 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-enemyspeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Die"))
        {
            Destroy(gameObject);
           
        }
            
    }
}
