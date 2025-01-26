using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzleFinal : MonoBehaviour
{
    [SerializeField] private List<SocketsTeclas> sockets;
    private List<string> codigo = new List<string> { "k", "e", "r", "o", "n", "t", "a", "s" };
    private List<string> playerCodigo = new List<string> ();

    public void ComprobarFinal()
    {
        playerCodigo.Clear ();
        foreach (SocketsTeclas socket in sockets)
        {
            playerCodigo.Add(socket.GetTeclaSocket());
        }
        if(codigo == playerCodigo)
        {
            Debug.Log("ES IGUAAAAAAL");
            foreach (SocketsTeclas socket in sockets)
            {
                socket.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        else
        {
            Debug.Log("NO");
            foreach (SocketsTeclas socket in sockets)
            {
                socket.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }
    }
}
