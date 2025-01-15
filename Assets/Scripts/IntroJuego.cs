using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroJuego : MonoBehaviour
{
    [SerializeField] Animator mano;
    [SerializeField] Animator playerMentira;
    [SerializeField] Animator flecha;
    [SerializeField] Transform playerLugar;
    [SerializeField] Rigidbody2D flechaTecla;
    [SerializeField] GameObject cajaCamara;

    public void StartIntro()
    {
        StartCoroutine(StartIntroCutscene());
    }

    private IEnumerator StartIntroCutscene()
    {
        yield return new WaitForSeconds(3f);
        mano.SetTrigger("Intro");
        yield return new WaitForSeconds(6f);
        playerMentira.SetTrigger("Intro");
        yield return new WaitForSeconds(3f);
        GameManager.Instance.InstantiatePlayer(playerLugar);
        playerMentira.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.85f);
        flecha.SetTrigger("Intro");
        yield return new WaitForSeconds(1f);
        flechaTecla.transform.parent = null;
        flechaTecla.isKinematic = false;
        cajaCamara.SetActive(true);
    }

}

