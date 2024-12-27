using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketsTeclas : MonoBehaviour
{
    [SerializeField] private bool locker;
    [SerializeField] private bool proximity;
    [SerializeField] private bool timer;

    [SerializeField] private List<SocketComponent> components;

    private TeclaFlecha socketedTecla; 

    public void SnapTeclaToSocket(TeclaFlecha tecla)
    {
        socketedTecla = tecla;
        string teclaValue = socketedTecla.GetDirection();
        foreach(SocketComponent socketComponent in components)
        {
            if(socketComponent.nameTecla.Equals(teclaValue))
            {
                socketComponent.component.enabled = true;
            }
        }

        if (locker)
        {
            socketedTecla.EnableBoxCollider(false);
        }
    }

    public void RemoveTeclaFromSocket()
    {
        string teclaValue = socketedTecla.GetDirection();
        foreach (SocketComponent socketComponent in components)
        {
            if (socketComponent.nameTecla.Equals(teclaValue))
            {
                socketComponent.component.enabled = false;
            }
        }
        socketedTecla = null;
    }
}
