using UnityEngine;
using System.Collections;

public class TeclaItem : MonoBehaviour
{
    [SerializeField] private string teclaValue;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 180f;
    [SerializeField] private bool presentacion = false;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    //private Transform targetTransform;
    protected Quaternion collectRotation;
    private TeclasManager teclasManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        teclasManager = TeclasManager.Instance;
        collectRotation = transform.rotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            EnableBoxCollider(false);
            collectRotation = transform.rotation;
            GetComponent<SpriteRenderer>().color = Color.yellow;

            if (presentacion)
            {
                presentacion = false;
                StartCoroutine(MoveSequence());
            }
            else
            {
                teclasManager.ConseguirTecla(this);
                //StartCoroutine(PlaceInSocket());
            }
        }
    }

    IEnumerator MoveSequence()
    {
        Vector3 screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        yield return StartCoroutine(MoveAndRotateToPosition(screenCenter));

        yield return new WaitForSeconds(2f);

        teclasManager.ConseguirTecla(this);
        //yield return StartCoroutine(PlaceInSocket());
    }

    IEnumerator MoveAndRotateToPosition(Vector3 targetPosition)
    {
        targetPosition.z = transform.position.z;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

            yield return null;
        }
        transform.position = targetPosition;
        transform.rotation = collectRotation;
    }

    public IEnumerator PlaceInSocket()
    {
        Vector3 targetPosition = new Vector3(0, 0, 0);
        targetPosition.z = transform.position.z;

        while (Vector3.Distance(transform.localPosition, targetPosition) > 0.1f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);

            transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

            yield return null;
        }
        transform.localPosition = targetPosition;
        transform.rotation = collectRotation;
        if (gameObject.GetComponent<DragTecla>() == null)
        {
            gameObject.AddComponent<DragTecla>();
        }
        EnableBoxCollider(true);
        boxCollider.isTrigger = true;
    }

    public void PlaceTeclaInSocket()
    {
        StartCoroutine(PlaceInSocket());
    }

    public void EnableBoxCollider(bool enable)
    {
        boxCollider.enabled = enable;
    }

    public string GetTeclaValue()
    {
        return teclaValue;
    }
}
