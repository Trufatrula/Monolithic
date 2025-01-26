using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaloActivar : MonoBehaviour
{
    [SerializeField] private bool canActivar = false;
    [SerializeField] private bool needTwoActivar = false;
    [SerializeField] private Transform puntoIr;
    [SerializeField] private List<PaloActivar> palosUnlocked;
    [SerializeField] private SocketsTeclas socket;
    [SerializeField] private string direction;

    [SerializeField] private MinijuegoPalos minijuego;

    private bool activado = false;

    private void Update()
    {
        if (activado) return;
        if(Input.GetKey(KeyCode.LeftArrow) && direction.Equals("Left"))
        {
            ActivarPalo();
        }
        else if(Input.GetKey(KeyCode.RightArrow) && direction.Equals("Right"))
        {
            ActivarPalo();
        }
        else if (Input.GetKey(KeyCode.UpArrow) && direction.Equals("Up"))
        {
            ActivarPalo();
        }
        else if (Input.GetKey(KeyCode.DownArrow) && direction.Equals("Down"))
        {
            ActivarPalo();
        }

    }


    public void ActivarPalo()
    {
        activado = true;
        if (canActivar)
        {
            socket.SetBloqued(true);
            minijuego.Puntuar();
            StartCoroutine(SalePalo());
        }
        else
        {
            activado = false;
            socket.RemoveAndReturnTecla();
        }
    }

    private IEnumerator SalePalo()
    {
        while(transform.position != puntoIr.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, puntoIr.transform.position, 5f * Time.deltaTime);
            yield return null;
        }
        foreach(PaloActivar paloActivar in palosUnlocked)
        {
            paloActivar.ActivarActivacion();
        }
        GetComponent<SpriteRenderer>().enabled = false;
        socket.RemoveAndReturnTecla();
    }

    public void ActivarActivacion()
    {
        if(needTwoActivar)
        {
            needTwoActivar = false;
            return;
        }
        canActivar = true;
    }
}
