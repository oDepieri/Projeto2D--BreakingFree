using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDir : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private EnemyMovement script;
    void Start()
    {
        script = enemy.GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Caixa") && !collision.gameObject.CompareTag("Player"))
        {
            script.dir = -1;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Caixa") && !collision.gameObject.CompareTag("Player"))
        {
            script.dir = -1;
        }
    }
}
