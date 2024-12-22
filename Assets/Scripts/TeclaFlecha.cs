using UnityEngine;
using System.Collections;

public class TeclaFlecha : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 180f;
    [SerializeField] private bool presentacion = false;
    
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Transform targetTransform;
    private Quaternion originalRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            boxCollider.enabled = false;
            originalRotation = transform.rotation;

            TeclasManager.Instance.ConseguirFlecha(this);

            if(presentacion)
            {
                presentacion = false;
                StartCoroutine(MoveSequence());
            } else
            {
                StartCoroutine(MoveAndRotateToPosition(targetTransform.position));
            }
        }
    }

    IEnumerator MoveSequence()
    {
        Vector3 screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        yield return StartCoroutine(MoveAndRotateToPosition(screenCenter));

        yield return new WaitForSeconds(2f);

        if (targetTransform != null)
        {
            yield return StartCoroutine(MoveAndRotateToPosition(targetTransform.position));
        }
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
        transform.rotation = originalRotation;
        Debug.Log(originalRotation);
    }

    public void SetTargetTransform(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
    }

    public string GetDirection()
    {
        Vector3 direction = originalRotation * Vector3.up;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            return direction.x > 0 ? "Down" : "Up";
        }
        else
        {
            return direction.y > 0 ? "Right" : "Left";
        }
    }
}
