using System.Collections;
using UnityEngine;

public class Flasher : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float blinkDuration = 0.5f;
    [SerializeField] private float flashDuration = 0.5f;
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private int blinksBeforeFlash = 2;

    private bool flashing = false;
    private bool cubierto = false;
    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        StartCoroutine(BlinkAndFlashRoutine());
    }

    private IEnumerator BlinkAndFlashRoutine()
    {
        int blinkeadas = 0;

        while (true)
        {
            if (blinkeadas < blinksBeforeFlash)
            {
                yield return StartCoroutine(Blink());
                blinkeadas++;
            }
            else
            {
                yield return StartCoroutine(Flash());
                blinkeadas = 0;
            }
        }
    }

    private IEnumerator Blink()
    {
        float elapsedTime = 0;
        while (elapsedTime < blinkDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / blinkDuration);
            SetSpriteAlpha(alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0;
        while (elapsedTime < blinkDuration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / blinkDuration);
            SetSpriteAlpha(alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator Flash()
    {
        flashing = true;
        spriteRenderer.color = flashColor;

        float elapsedTime = 0;
        while (elapsedTime < flashDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / flashDuration);
            SetSpriteAlpha(alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0;
        while (elapsedTime < flashDuration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / flashDuration);
            SetSpriteAlpha(alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        flashing = false;
        spriteRenderer.color = originalColor;
    }

    private void SetSpriteAlpha(float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!flashing || cubierto) { return; }
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Chetun");
        }
    }

    public void SetCubierto(bool cubierto)
    {
        this.cubierto = cubierto;
    }


}