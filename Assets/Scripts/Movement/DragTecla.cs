using System.Net.Sockets;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class DragTecla : MonoBehaviour
{
    private bool isDragging = false;
    private bool isSnapped = false;
    private bool eventRemoveSocket = false;
    private SocketsTeclas closestSocket;
    private TeclaItem teclaItem;

    public float snapDistance = 1.0f;

    private void Start()
    {
        teclaItem = GetComponent<TeclaItem>();
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition;
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        isSnapped = false;

        if (!eventRemoveSocket)
        {
            eventRemoveSocket = true;
            RemoveFromSocket();
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        eventRemoveSocket = false;

        closestSocket = FindClosestSocket();

        string teclaValue = "";
        if(teclaItem is TeclaFlecha flecha)
        {
            teclaValue = flecha.GetDirection();
        }
        else
        {
            teclaValue = teclaItem.GetTeclaValue();
        }


        if (closestSocket != null && Vector3.Distance(transform.position, closestSocket.transform.position) <= snapDistance && closestSocket.CanSnap(teclaValue))
        {
            transform.position = closestSocket.transform.position;
            isSnapped = true;
            gameObject.transform.parent = closestSocket.transform;
            closestSocket.SnapTeclaToSocket(teclaItem);
        }
        else
        {
            ReturnToOriginalPosition();
        }
    }

    SocketsTeclas FindClosestSocket()
    {
        SocketsTeclas[] sockets = TeclasManager.Instance.GetSocketsTeclas();
        SocketsTeclas closest = null;
        float minDistance = Mathf.Infinity;

        foreach (SocketsTeclas socket in sockets)
        {
            if(socket.GetIsSocketTaken()) { continue; }
            float distance = Vector3.Distance(transform.position, socket.gameObject.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = socket;
            }
        }
        return closest;
    }

    public void ReturnToOriginalPosition()
    {
        closestSocket = null;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        TeclasManager.Instance.ConseguirTecla(teclaItem);
    }

    private void RemoveFromSocket()
    {
        if(closestSocket != null)
        {
            closestSocket.RemoveTeclaFromSocket();
            Debug.Log("REMOVENCION DEL SOCKET");
        }
        else
        {
            TeclasManager.Instance.LoseTecla(teclaItem);
            Debug.Log("REMOVENCION TOTAL");
        }
    }
}