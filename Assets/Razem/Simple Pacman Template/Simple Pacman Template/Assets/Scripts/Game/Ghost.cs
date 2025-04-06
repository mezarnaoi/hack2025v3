using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [Space(10)]
    [Header("Stats")]
    [SerializeField, ReadOnly] private Vector2 direction;
    [SerializeField, ReadOnly] private bool up, down, right, left;
    [SerializeField, ReadOnly] private List<Vector2> directionAllowed;
    [SerializeField, ReadOnly] private Rigidbody2D body;
    [SerializeField, ReadOnly] private float countCollision;
    [SerializeField, ReadOnly] private float returnToGameCount;
    [SerializeField, ReadOnly] private bool moveToCenter;
    [SerializeField, ReadOnly] private bool returnToGame;

    [Space(10)]
    [Header("Settings")]
    [SerializeField] private bool randomMovement;
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite fearSprite;
    [SerializeField] private GameObject trail;
    private Sprite originalSprite;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        originalSprite = spriteRenderer.sprite;
    }

    public void ToCenter()
    {
        StartCoroutine(MoveGhostToPosition(GameController.instance.level.center));
    }

    private void ToCenterWithNoReturn()
    {
        StartCoroutine(MoveGhostToPosition(GameController.instance.level.center));
    }

    public void GhostReturn(float time)
    {
        returnToGame = true;
        returnToGameCount = time;
    }

    public void GhostStopReturn()
    {
        returnToGame = false;
    }

    private IEnumerator MoveGhostToPosition(Transform target)
    {
        trail.SetActive(true);
        direction = Vector2.zero;
        moveToCenter = true;
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
        moveToCenter = false;
        trail.SetActive(false);

    }

    private IEnumerator WaitToGhostReturn()
    {
        yield return new WaitForSeconds(3f);
        if(GameController.instance.game == Game.GAMEPLAY)
            StartCoroutine(MoveGhostToPosition(GameController.instance.level.initialGhostPosition));
    }

    private void Update()
    {
        if (GameController.instance.game == Game.GAMEOVER)
            target = GameController.instance.pecboy.transform;
        else if (GameController.instance.game != Game.GAMEPLAY)
        {
            return;
        }
        if (!moveToCenter)
        {
            RaycastHit2D centerHit = Physics2D.Raycast(transform.position, Vector2.zero, 0.1f, LayerMask.GetMask("Point"));

            if (centerHit.collider != null && centerHit.collider.tag == "PointEnemy" && countCollision <= 0)
            {
                transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
                Move();
                countCollision = 0.1f;
            }
        }

        if (returnToGame)
        {
            returnToGameCount -= Time.deltaTime;
            if(returnToGameCount <= 0)
            {
                StartCoroutine(MoveGhostToPosition(GameController.instance.level.initialGhostPosition));
                returnToGame = false;
                returnToGameCount = 3f;
            }
        }

        if (countCollision > 0)
            countCollision -= Time.deltaTime;
        if (transform.position.x < -9)
            transform.position = new Vector2(9, transform.position.y);
        if (transform.position.x > 9)
            transform.position = new Vector2(-9, transform.position.y);
    }

    private void FixedUpdate()
    {
        body.linearVelocity = direction * speed;
    }

    private void ValidMovement()
    {
        Vector2 originUp = new Vector2(transform.position.x, transform.position.y + 0.52f);
        Vector2 originDown = new Vector2(transform.position.x, transform.position.y - 0.52f);
        Vector2 originRight = new Vector2(transform.position.x + 0.52f, transform.position.y);
        Vector2 originLeft = new Vector2(transform.position.x - 0.52f, transform.position.y);

        RaycastHit2D upHit = Physics2D.Raycast(originUp, Vector2.up, 0.1f, LayerMask.GetMask("Wall"));
        RaycastHit2D downHit = Physics2D.Raycast(originDown, Vector2.down, 0.1f, LayerMask.GetMask("Wall"));
        RaycastHit2D rightHit = Physics2D.Raycast(originRight, Vector2.right, 0.1f, LayerMask.GetMask("Wall"));
        RaycastHit2D leftHit = Physics2D.Raycast(originLeft, Vector2.left, 0.1f, LayerMask.GetMask("Wall"));


        if (upHit.collider != null && upHit.collider.tag == "Wall")
            up = false;
        else
        {
            up = true;
        }

        if (downHit.collider != null && downHit.collider.tag == "Wall")
            down = false;
        else
        {
            down = true;
        }

        if (rightHit.collider != null && rightHit.collider.tag == "Wall")
            right = false;
        else
        {
            right = true;
        }

        if (leftHit.collider != null && leftHit.collider.tag == "Wall")
            left = false;
        else
        {
            left = true;
        }

       
    }

    public void Move()
    {
        ValidMovement();
        directionAllowed.Clear();

        if (target.position.x > transform.position.x && target.position.y > transform.position.y && up)
            directionAllowed.Add(Vector2.up);
        if (target.position.x > transform.position.x && target.position.y > transform.position.y && right)
            directionAllowed.Add(Vector2.right);

        if (target.position.x > transform.position.x && target.position.y < transform.position.y && down)
            directionAllowed.Add(Vector2.down);
        if (target.position.x > transform.position.x && target.position.y < transform.position.y && right)
            directionAllowed.Add(Vector2.right);

        if (target.position.x < transform.position.x && target.position.y < transform.position.y && down)
            directionAllowed.Add(Vector2.down);
        if (target.position.x < transform.position.x && target.position.y < transform.position.y && left)
            directionAllowed.Add(Vector2.left);

        if (target.position.x < transform.position.x && target.position.y > transform.position.y && up)
            directionAllowed.Add(Vector2.up);
        if (target.position.x < transform.position.x && target.position.y > transform.position.y && left)
            directionAllowed.Add(Vector2.left);

        if (target.position.x == transform.position.x && target.position.y > transform.position.y && up)
            directionAllowed.Add(Vector2.up);
        if (target.position.x == transform.position.x && target.position.y < transform.position.y && down)
            directionAllowed.Add(Vector2.down);
        if (target.position.x > transform.position.x && target.position.y == transform.position.y && right)
            directionAllowed.Add(Vector2.right);
        if (target.position.x < transform.position.x && target.position.y == transform.position.y && left)
            directionAllowed.Add(Vector2.left);

        if (!randomMovement)
        {
            if (directionAllowed.Count > 0)
            {
                direction = directionAllowed[Random.Range(0, directionAllowed.Count)];
            }
            else
            {
                if (up)
                    directionAllowed.Add(Vector2.up);
                if (down)
                    directionAllowed.Add(Vector2.down);
                if (right)
                    directionAllowed.Add(Vector2.right);
                if (left)
                    directionAllowed.Add(Vector2.left);
                direction = directionAllowed[Random.Range(0, directionAllowed.Count)];
            }
        }
        else
        {
            directionAllowed.Clear();
            if (up)
                directionAllowed.Add(Vector2.up);
            if (down)
                directionAllowed.Add(Vector2.down);
            if (right)
                directionAllowed.Add(Vector2.right);
            if (left)
                directionAllowed.Add(Vector2.left);
            direction = directionAllowed[Random.Range(0, directionAllowed.Count)];
        }
    }

    public void GhostFear()
    {
        randomMovement = true;
        spriteRenderer.sprite = fearSprite;
    }

    public void GhostNormal()
    {
        randomMovement = false;
        spriteRenderer.sprite = originalSprite;
    }
}
