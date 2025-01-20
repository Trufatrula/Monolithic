using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketsTeclas : MonoBehaviour
{
    [SerializeField] private bool locker;
    [SerializeField] private bool proximity;
    [SerializeField] private bool timer;
    [SerializeField] private GameObject cadenas;

    [SerializeField] private List<SocketComponent> components;
    [SerializeField] private List<string> posibleTeclas;

    private TeclaFlecha socketedTecla;
    private bool isTaken = false;
    [SerializeField] private bool isBloqued = false;
    
    public void SnapTeclaToSocket(TeclaFlecha tecla)
    {
        socketedTecla = tecla;
        isTaken = true;
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
            Lock(true);
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
        isTaken = false;
    }

    public void RemoveAndReturnTecla()
    {
        if(isTaken)
        {
            socketedTecla.GetComponent<DragTecla>().ReturnToOriginalPosition();
            RemoveTeclaFromSocket();
        }
    }

    public bool GetIsSocketTaken()
    {
        return isTaken || isBloqued;
    }

    public void SetBloqued(bool isBloqued)
    {
        this.isBloqued = isBloqued;
    }

    public void Lock(bool lockear)
    {
        lockear = lockear || locker;
        socketedTecla.EnableBoxCollider(!lockear);
        cadenas.SetActive(lockear);
    }

    public bool CanSnap(string teclaValue)
    {
        if(posibleTeclas.Contains(teclaValue))
        {
            return true;
        }
        return false;
    }
}
