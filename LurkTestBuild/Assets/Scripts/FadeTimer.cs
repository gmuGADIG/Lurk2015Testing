using UnityEngine;
using System.Collections;

public class FadeTimer : MonoBehaviour
{
    private float start = 0;    // Start time (time when created)
    private float duration = 0;     // Amount of time to last
    private Renderer r;
    private Color c;

    public bool Setup(float d)
    {
        start = Time.time;
        duration = d;
        r = this.GetComponentInChildren<Renderer>();
        if (r == null)
        {
            Debug.LogError(name + " FadeTimer did not find a SpriteRenderer!");
            return false;
        }
        c = r.material.color;
        return true;
    }

    void Update()
    {
        if (duration == 0)
            Destroy(gameObject);

        c.a = 1 - (Time.time - start) / duration;

        if (c.a <= 0)
            Destroy(gameObject);

        if (r != null || Setup(duration))
            r.material.color = c;
    }
}
