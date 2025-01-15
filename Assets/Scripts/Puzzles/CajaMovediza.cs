using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CajaMovediza : MonoBehaviour
{
    [SerializeField] private SocketsTeclas socketBox;
    [SerializeField] private float snapPos = 0f;

    private bool isPlaced = false;
    float distanceCaer = 0f;

    private IEnumerator MoveBox()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y - distanceCaer);
        float velCaer = 5f;
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, velCaer * Time.deltaTime);
            velCaer += 0.5f;
            yield return null;
        }
        transform.position = targetPosition;
        transform.localPosition = new Vector3(snapPos, transform.position.y);
    }
    public void StartMoving(float distanceCaer)
    {
        this.distanceCaer = distanceCaer;
        socketBox.RemoveAndReturnTecla();
        SetPlaced(true);
        StartCoroutine(MoveBox());
    }

    public void SetPlaced(bool place)
    {
        if(!place) { return; }
        isPlaced = true;
        gameObject.tag = "Untagged";
        socketBox.SetBloqued(true);
        socketBox.GetComponent<SpriteRenderer>().enabled = false;
    }

    public bool GetPlaced()
    {
        return isPlaced;
    }

}
