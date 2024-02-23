using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITweener : MonoBehaviour
{
    public GameObject[] aliens;
    public LeanTweenType easeType; 
    public LeanTweenType easeType2;

    void Start()
    {

        for (int i = 0; i < aliens.Length; i++)
        {
            LeanTween.moveY(aliens[i], 1300f - 150 * i, 2f).setDelay(1f + i / 2f).setEase(easeType);
            StartCoroutine(ShowPoints(3f + i / 2f, i));
        }

    }


    IEnumerator ShowPoints(float amount, int order)
    {
        yield return new WaitForSeconds(amount);
        GameObject go = aliens[order].transform.GetChild(0).gameObject;
        go.SetActive(true);
        LeanTween.scale(go, new Vector3(1f, 1f, 1f), 1f).setEase(easeType2);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("GameScene");
    }
}
