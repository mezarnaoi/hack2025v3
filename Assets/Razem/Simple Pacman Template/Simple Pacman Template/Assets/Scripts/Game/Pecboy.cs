using System;
using System.Collections;
using UnityEngine;

public class Pecboy : MonoBehaviour
{
    [Space(10)]
    [Header("Stats")]
    [SerializeField, ReadOnly] private Vector2 horizontal;
    [SerializeField, ReadOnly] private Vector2 vertical;
    [SerializeField, ReadOnly] private bool up, down, right, left, move;
    [SerializeField, ReadOnly] private Vector2 direction;
    [SerializeField, ReadOnly] private CameraShake cameraShake;
    [SerializeField, ReadOnly] private Rigidbody2D body;
    
    
    [Space(10)]
    [Header("Settings")]
    [SerializeField] private float speed;

    [SerializeField] private GameObject graphic;
    [SerializeField] private GameObject scoreUI;
    [SerializeField] private GameObject vfxExplode;
    [SerializeField] private SpriteRenderer head;

    [Space(10)]
    [Header("Audios")]
    [SerializeField] private AudioSource fxAudioSource;
    [SerializeField] private AudioClip coinAudio;

    private void OnDisable()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void Update()
    {
        horizontal.x = Input.GetAxisRaw("Horizontal");
        vertical.y = Input.GetAxisRaw("Vertical");
        if (GameController.instance.game != Game.GAMEPLAY)
            return;
        Vector2 originUp = new Vector2(transform.position.x, transform.position.y + 0.5f);
        Vector2 originDown = new Vector2(transform.position.x, transform.position.y - 0.5f);
        Vector2 originRight = new Vector2(transform.position.x + 0.5f, transform.position.y);
        Vector2 originLeft = new Vector2(transform.position.x - 0.5f, transform.position.y);


        RaycastHit2D upHit = Physics2D.Raycast(originUp, Vector2.up, 0.1f);
        RaycastHit2D downHit = Physics2D.Raycast(originDown, Vector2.down, 0.1f);
        RaycastHit2D rightHit = Physics2D.Raycast(originRight, Vector2.right, 0.1f);
        RaycastHit2D leftHit = Physics2D.Raycast(originLeft, Vector2.left, 0.1f);

        Vector2 dir = ((Vector3)direction / 2) + transform.position;
        RaycastHit2D frontHit = Physics2D.Raycast(dir, direction, 0.1f);

        if (frontHit.collider != null && frontHit.collider.tag == "Wall")
        {
            Time.timeScale = 0.15f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
        }


        float posX = Math.Abs(Math.Abs(transform.position.x) - Math.Abs(Mathf.RoundToInt(transform.position.x)));
        float posY = Math.Abs(Math.Abs(transform.position.y) - Math.Abs(Mathf.RoundToInt(transform.position.y)));

        if (posX <= 0.25f && posY <= 0.25f)
            move = true;
        else
            move = false;

        if (upHit.collider != null && upHit.collider.tag == "Wall")
            up = false;
        else
            up = true;

        if (downHit.collider != null && downHit.collider.tag == "Wall")
            down = false;
        else
            down = true;

        if (rightHit.collider != null && rightHit.collider.tag == "Wall")
            right = false;
        else
            right = true;

        if (leftHit.collider != null && leftHit.collider.tag == "Wall")
            left = false;
        else
            left = true;

        if (move)
        {
            if (Input.GetKey(KeyCode.UpArrow) && up)
            {
                graphic.transform.eulerAngles = new Vector3(0, 0, 90);
                direction = Vector2.up;
            }
            if (Input.GetKey(KeyCode.RightArrow) && right)
            {
                graphic.transform.eulerAngles = new Vector3(0, 0, 0);
                direction = Vector2.right;
            }
            if (Input.GetKey(KeyCode.DownArrow) && down)
            {
                graphic.transform.eulerAngles = new Vector3(0, 0, -90);
                direction = Vector2.down;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && left)
            {
                graphic.transform.eulerAngles = new Vector3(0, 0, 180);
                direction = Vector2.left;
            }
        }

        if (transform.position.x < -9)
            transform.position = new Vector2(9, transform.position.y);
        if (transform.position.x > 9)
            transform.position = new Vector2(-9, transform.position.y);

    }

    void FixedUpdate()
    {
        body.linearVelocity = direction * speed;
    }

    public void SetHeadItem(Sprite sprite)
    {
        head.sprite = sprite;
    }

    public void ToInitialPosition()
    {
        direction = Vector2.zero;
        StartCoroutine(MovePecboyToPosition(GameController.instance.level.initialPecboyPosition));
    }

    public void InitialPosition()
    {
        direction = Vector2.zero;
        transform.position = GameController.instance.level.initialPecboyPosition.position;
    }

    private IEnumerator MovePecboyToPosition(Transform target)
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
        direction = Vector2.zero;
        float step = (5f / (transform.position - Vector3.zero).magnitude * Time.fixedDeltaTime);
        float t = 0;
        while (t <= 0.5f)
        {
            t += step;
            transform.position = Vector3.Lerp(transform.position, target.position, t);
            yield return new WaitForSeconds(0.01f);
        }
        transform.position = target.position;
        transform.eulerAngles = new Vector3(0, 0, 0);
        GetComponent<CircleCollider2D>().isTrigger = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameController.instance.game != Game.GAMEPLAY)
            return;

        if (collision.tag == "Point")
        {
            GameController.instance.AddPoint();
            Instantiate(scoreUI, transform.position, Quaternion.identity);
            collision.gameObject.SetActive(false);
        }

        if (collision.tag == "Coin")
        {
            cameraShake.DoShake(0.1f);
            collision.gameObject.SetActive(false);
            GameController.instance.AddCoin();
            fxAudioSource.PlayOneShot(coinAudio);
        }

        if (collision.tag == "Enemy")
        {
            if (GameController.instance.invicible)
            {
                cameraShake.DoShake(0.15f);
                collision.GetComponent<Ghost>().ToCenter();
                collision.GetComponent<Ghost>().GhostReturn(3f);
            }
            else
            {
                cameraShake.DoShake(3f);
                Instantiate(vfxExplode, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                GameController.instance.Gameover();
            }
        }
    }
}
