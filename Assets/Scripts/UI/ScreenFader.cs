using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    //private Animator anim;
    private Image image;
    [SerializeField] private float fadeDuration = 10f;

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
            
            //暂停协程的执行，让出当前帧的控制权，并在下一帧的 Update 方法执行完毕后恢复协程
            yield return null;
        }
    }
}
