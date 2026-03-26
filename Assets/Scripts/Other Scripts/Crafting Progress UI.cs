using UnityEngine;
using UnityEngine.UI;

public class CraftingProgressUI : MonoBehaviour
{
    public GameObject fillbar;

    private RectTransform rect;
    private float maxWidth;

    private float frame = 0;
    private bool wait = false;
    private float waitFrame = 0;
    private const float waitDuration = 1f;

    public bool startCraft = false;
    public float durationCraft;

    void Start()
    {
        rect = fillbar.GetComponent<RectTransform>();
        maxWidth = rect.sizeDelta.x;
    }

    void Update()
    {
        if (startCraft && !wait)
        {
            frame += Time.deltaTime;

            float progress = Mathf.Clamp01(frame / durationCraft);
            float width = progress * maxWidth;

            rect.sizeDelta = new Vector2(width, rect.sizeDelta.y);

            if (progress == 1)
            {
                wait = true;
                frame = 0;
            }
        } else if (wait)
        {
            waitFrame += Time.deltaTime;
            if (waitFrame >= waitDuration)
            {
                rect.sizeDelta = new Vector2(-maxWidth, rect.sizeDelta.y);
                waitFrame = 0;
                wait = false;
                startCraft = false;
            }
        }
        else
        {
            frame = 0;
            rect.sizeDelta = new Vector2(0, rect.sizeDelta.y);
        }
    }
}