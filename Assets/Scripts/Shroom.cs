using UnityEngine;
using System.Collections;

public class Shroom : MonoBehaviour
{
    // The offset of the shroom to hide it
    private Vector2 startPosition = new Vector2(0f, -1f);
    private Vector2 endPosition = Vector2.zero;
    // How long it takes to show a shroom
    private float showDuration = 0.5f;
    private float duration = 1f;
    private SpriteRenderer spriteRenderer;
    private bool hittable = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ShowHide(startPosition, endPosition));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ShowHide(Vector2 start, Vector2 end)
    {
        transform.localPosition = start;
        
        // show the shroom
        float elapsed = 0f;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = end;

        yield return new WaitForSeconds(duration);

        // hide the shroom
        elapsed = 0f;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = start;
    }

    private void OnMouseDown()
    {
        if (hittable)
        {
            spriteRenderer.color = new Color (200, 20, 0, 1);
            StopAllCoroutines();
            StartCoroutine(QuickHide());
            hittable = false;
        }
    }

    private IEnumerator QuickHide()
    {
        yield return new WaitForSeconds(0.25f);
        if (!hittable)
        {
            Hide();
        }
    }

    public void Hide()
    {
        transform.localPosition = startPosition;
    }
}
