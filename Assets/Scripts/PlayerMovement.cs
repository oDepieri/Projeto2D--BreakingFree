using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public int faseAtual;
    public float moveSpeed;
    public float jumpForce;
    private float jumpCurr;
    private bool jumping = false;
    private Rigidbody2D rb;
    public bool isGrounded;
    private Animator animator;
    public bool punchCol;
    public int dir;
    SpriteRenderer sprite;
    BoxCollider2D boxCol;
    public bool hiding;
    private GameObject caixa;
    public int vida = 3;
    [SerializeField] public GameObject textoGO;
    public TextMeshProUGUI textoVida;
    public Sprite[] vidaTexturas;
    public Image vidaUI;


    void Start()
    {
        textoVida = textoGO.GetComponent<TextMeshProUGUI>();
        textoVida.text = "VIDAS: " + vida.ToString();
        caixa = null;
        hiding = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        boxCol = gameObject.GetComponent<BoxCollider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        vidaUI.sprite = vidaTexturas[vida - 1];
    }

    void Update()
    {
        if (transform.position.y < -3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Y)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            
            if (!hiding && !jumping && caixa != null)
            {
                transform.position = caixa.transform.position;
                Debug.Log("apertou");
                hiding = true;
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
                boxCol.enabled = false;
                sprite.enabled = false;
            }
            else if (hiding && !jumping)
            {
                hiding = false;
                rb.constraints = ~RigidbodyConstraints2D.FreezePosition;
                boxCol.enabled = true;
                sprite.enabled = true;
            }
        }
        float moveInput = Input.GetAxis("Horizontal");
        if (!hiding)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
        if (rb.velocity.x > 0)
        {
            sprite.flipX = false;
            dir = 1;
        }
        else if (rb.velocity.x < 0)
        {
            sprite.flipX = true;
            dir = -1;
        }

        if (Input.GetKeyDown(KeyCode.F) && isGrounded && !hiding)
        {
            jumping = true;
            jumpCurr = jumpForce;
            rb.velocity = new Vector2(rb.velocity.x, jumpCurr);
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            jumping = false;
            jumpCurr = 0f;
        }
        if (Input.GetKey(KeyCode.F) && !isGrounded && jumping && rb.velocity.y>2.5f)
        {
            jumpCurr *= Mathf.Pow(.5f,Time.deltaTime);
            rb.velocity = new Vector2(rb.velocity.x, jumpCurr);
        }

        AnimationManager();
    }

    void AnimationManager()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            animator.SetTrigger("socando");
        }
        if (rb.velocity.y > 0)
        {
            animator.SetBool("pulando", true);
            animator.SetBool("caindo", false);
        }
        else if(rb.velocity.y < 0)
        {
            animator.SetBool("caindo", true);
            animator.SetBool("pulando", false);
        }
        else if ( Mathf.Abs(rb.velocity.x)>0.03)
        {
            animator.SetBool("correndo", true);
            animator.SetBool("caindo", false);
            animator.SetBool("pulando", false);
        }
        else
        {
            animator.SetBool("correndo", false);
            animator.SetBool("caindo", false);
            animator.SetBool("pulando", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Caixa"))
        {
            caixa = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Caixa"))
        {
            caixa = null;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Cocaina"))
        {
            vida = vida - 1;
            textoVida.text = "VIDAS: " + vida.ToString();
            if(vida>0) vidaUI.sprite = vidaTexturas[vida-1];
            if (vida < 1)
            {
                Debug.Log("xi");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        if (collision.gameObject.CompareTag("Chegada"))
        {
            SceneManager.LoadScene(faseAtual);
        }
        if (collision.gameObject.CompareTag("Coracao"))
        {
            if (vida < 3)
            {
                vida += 1;
                vidaUI.sprite = vidaTexturas[vida - 1];
                Destroy(collision.gameObject);
            }
            else Destroy(collision.gameObject);
        }
    }
}
