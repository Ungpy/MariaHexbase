using System.Collections;
using System.Timers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
public class CharSprite : MonoBehaviour
{
    private Vector2 endPosition = new Vector2(7.27f, -2.2159f);
    private Vector2 startPosition = new Vector2(11f, -2.2159f);
    public GameObject character ;
    [SerializeField] private int posIndex;
    [SerializeField] private AnimationCurve animC;

    float currentPercent;
    bool isKey = false;
    void Start()
    {
        character.transform.position = startPosition;
    }

    void Update()
    { 
        //if (Keyboard.current.eKey.wasPressedThisFrame)
        //{
        //    StartCoroutine(SlideIn());
        //}
    }

    public void PlayAnimation(EDialogueSpriteAnim anim)
    {
        switch (anim)
        {
            case EDialogueSpriteAnim.SlideIn :
                StartCoroutine(Slide(startPosition, endPosition));
                break;

            case EDialogueSpriteAnim.SlideOut:
                StartCoroutine(Slide(endPosition, startPosition));
                break;

            default:
                break;
        }
    }

    private IEnumerator Slide(Vector2 start, Vector2 end)
    {
        float elapsedTime = 0f;
        float duration = 1f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            currentPercent = elapsedTime / duration;

            character.transform.position = Vector2.Lerp(start, end, animC.Evaluate(currentPercent));
            yield return null;
        }
        character.transform.position = end; 
    }

    
}
