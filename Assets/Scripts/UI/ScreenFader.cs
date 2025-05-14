using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    //private Animator anim;
    private Image image;
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        //anim = GetComponent<Animator>();
        image = GetComponent<Image>();
    }

    public IEnumerator FadeOut()
    {
        //anim.SetTrigger("FadeOut");

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
           float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration); 
           image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
           elapsed += Time.deltaTime;
           yield return null;
        }
    }
    
    public IEnumerator FadeIn()
    {
        //anim.SetTrigger("FadeOut");

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration); 
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
