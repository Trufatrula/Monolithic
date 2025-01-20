using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TypingFinishedEvent : UnityEvent<string>
{ }

public class TextoEscribir : MonoBehaviour
{
    private TextMeshProUGUI textoAEscribir;
    private string texto;

    [SerializeField] private float delayEscribir = 0.05f;
    [SerializeField] private float waitText = 1f;
    [SerializeField] private float waitAutocompletable = 1f;

    [SerializeField] private TypingFinishedEvent OnTypingFinished;

    private void Awake()
    {
        textoAEscribir = GetComponent<TextMeshProUGUI>();
        texto = textoAEscribir.text;
    }

    private void OnEnable()
    {
        textoAEscribir.text = "";
        StartCoroutine(RevealText());
    }

    private IEnumerator RevealText()
    {
        textoAEscribir.text = "";

        yield return new WaitForSeconds(waitText);

        for (int i = 0; i < texto.Length; i++)
        {
            textoAEscribir.text += texto[i];
            textoAEscribir.ForceMeshUpdate();

            yield return new WaitForSeconds(delayEscribir);
        }

        yield return new WaitForSeconds(waitAutocompletable);
        completeSentence();
    }

    public void completeSentence()
    {
        textoAEscribir.text = texto;
        Debug.Log("Terminada");
        OnTypingFinished?.Invoke(texto);
    }
}