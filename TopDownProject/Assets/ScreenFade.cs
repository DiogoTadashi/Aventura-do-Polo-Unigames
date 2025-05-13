using UnityEngine;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    public CanvasGroup blackScreen; // Referência ao CanvasGroup da tela preta

    void Start()
    {
        blackScreen.alpha = 0; // Começa invisível
        blackScreen.gameObject.SetActive(false);
    }

    public IEnumerator FadeIn(float duration)
    {
        blackScreen.gameObject.SetActive(true);
        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            blackScreen.alpha = Mathf.Lerp(0, 1, counter / duration); // Faz o fade-in
            yield return null;
        }
    }

    public IEnumerator FadeOut(float duration)
    {
        float counter = 0;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            blackScreen.alpha = Mathf.Lerp(1, 0, counter / duration); // Faz o fade-out
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        blackScreen.gameObject.SetActive(false);
    }
}