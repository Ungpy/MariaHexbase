using System.Collections;
using System.Timers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
public class CharSprite : MonoBehaviour
{
    private Vector2 endPosition0 = new Vector2(7.27f, -2.2f);
    private Vector2 startPosition0 = new Vector2(11f, -2.2f);

    private Vector2 endPosition1 = new Vector2(-6.9f, -2.2f);
    private Vector2 startPosition1 = new Vector2(-10.6f, -2.2f);
    public GameObject character ;
    [SerializeField] private int posIndex = 0;
    [SerializeField] private AnimationCurve animC;

    float currentPercent;
    bool isKey = false;
    void Start()
    {
        switch (posIndex)
        {
            case 0:
                character.transform.position = startPosition0;
                break;
            case 1:
                character.transform.position = startPosition1;
                break;
            default:
                character.transform.position = new Vector2();
                break;
        }
        //character.transform.position = startPosition;
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
        Vector2 startPosition;
        Vector2 endPosition;


        switch (posIndex)
        {
            case 0:
                startPosition = startPosition0;
                endPosition = endPosition0;
                break;
            case 1:
                startPosition = startPosition1;
                endPosition = endPosition1;
                break;
            default:
                startPosition = new Vector2();
                endPosition = new Vector2();
                break;
        }

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
