using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvocarBoss : MonoBehaviour
{
    [SerializeField] GameObject mano;
    [SerializeField] Animator barreraEntrada;
    [SerializeField] Animator barreraSalida;
    [SerializeField] SocketsTeclas socketIzq;
    [SerializeField] SocketsTeclas socketDrc;
    [SerializeField] Slider vidaBoss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ComenzarBoss();
        }
    }

    private void ComenzarBoss()
    {
        Debug.Log("ComienzaElBoss");
        BarreraEntradaAnimacion(false);
        mano.gameObject.SetActive(true);
        socketIzq.SetBloqued(false);
        socketIzq.GetComponent<SpriteRenderer>().enabled = true;
        socketDrc.SetBloqued(false);
        socketDrc.GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = false;
    }


    public void RespawnearBoss()
    {
        socketIzq.RemoveAndReturnTecla();
        socketIzq.SetBloqued(true);
        socketIzq.GetComponent<SpriteRenderer>().enabled = false;
        socketDrc.RemoveAndReturnTecla();
        socketDrc.SetBloqued(true);
        socketDrc.GetComponent<SpriteRenderer>().enabled = false;
        FillVida();
        GetComponent<BoxCollider2D>().enabled = true;
        BarreraEntradaAnimacion(true);
    }

    //private IEnumerator AparecerMuro(SpriteRenderer sprite)
    //{
    //    float duration = 2f;
    //    float elapsedTime = 0f;
    //    Color color = sprite.color;

    //    while (elapsedTime < duration)
    //    {
    //        float alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
    //        sprite.color = new Color(color.r, color.g, color.b, alpha);
    //        elapsedTime += Time.deltaTime;
    //        yield return null; 
    //    }
    //    sprite.color = new Color(color.r, color.g, color.b, 1f);
    //}

    public void FinalizarBoss()
    {
        BarreraEntradaAnimacion(true);
        BarreraSalidaAnimacion(true);
        gameObject.SetActive(false);
    }

    public void PerderVida()
    {
        vidaBoss.value--;
    }

    public void FillVida()
    {
        StartCoroutine(FillBarraVida());
    }

    private IEnumerator FillBarraVida()
    {
        vidaBoss.value = 0;
        yield return new WaitForSeconds(1f);
        while (vidaBoss.value < 4)
        {
            vidaBoss.value += 0.01f;
            yield return null;
        }
        ComenzarBoss();
    }
    public void BarreraEntradaAnimacion(bool abrir)
    {
        barreraEntrada.SetBool("Abrir", abrir);
    }

    public void BarreraSalidaAnimacion(bool abrir)
    {
        barreraSalida.SetBool("Abrir", abrir);
    }
}
