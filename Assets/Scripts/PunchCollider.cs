using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchCollider : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerMovement script;
    private Rigidbody2D rb;
    private GameObject enemy;
    private GameObject vida;
    public GameObject coracao;
    void Start()
    {
        script = player.GetComponent<PlayerMovement>();
        rb = player.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (script.dir<0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (script.dir>0)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        if (Input.GetKeyDown(KeyCode.G) && !script.hiding)
        {
            Destroy(enemy);
            enemy = null;
            if (vida != null)
            {
                Vector3 inst = new Vector3(vida.transform.position.x,vida.transform.position.y, vida.transform.position.z);
                Quaternion rot = Quaternion.identity;
                Destroy(vida);
                Debug.Log("alguem me mata");
                Instantiate(coracao, inst, rot);
                Destroy(vida);
                vida = null;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy = collision.gameObject;
        }
        if (collision.gameObject.CompareTag("Vida"))
        {
            vida = collision.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy = null;
        }
        if (collision.gameObject.CompareTag("Vida"))
        {
            vida = null;
        }
    }

}
