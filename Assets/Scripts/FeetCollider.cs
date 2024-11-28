using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCollider : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerMovement script;
    private BoxCollider2D col;
    void Start()
    {
        script = player.GetComponent<PlayerMovement>();
        col = gameObject.GetComponent<BoxCollider2D>();
    }


    void Update()
    {
        if (script.hiding)
        {
            col.enabled = false;
        }
        else if(!script.hiding) 
        { 
            col.enabled = true;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Vida"))
        {
            script.isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Vida"))
        {
            script.isGrounded = false;
        }
    }
}
