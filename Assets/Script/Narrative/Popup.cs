using System.Timers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
public class Popup : MonoBehaviour
{
    private Vector2 endPosition = new Vector2(6.82f, -2.35f);
    private Vector2 startPosition = new Vector2(10f, -2.35f);
    public GameObject character ;
    [SerializeField]
    private AnimationCurve animC;
    float elapsedTime;
    float duration = 1f;
    float currentPercent;
    bool isKey = false;
    void Start()
    {
        character.transform.position = startPosition;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        currentPercent = elapsedTime / duration;
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            isKey = true;
            Debug.Log("E Pressed");
            elapsedTime = 0f;
        }
        if (isKey)
        {
            //character.transform.position = Vector2.Lerp(startPosition, endPosition, Mathf.SmoothStep(0, 1, currentPercent));
            character.transform.position = Vector2.Lerp(startPosition, endPosition, animC.Evaluate(currentPercent));
        }
    }
}
