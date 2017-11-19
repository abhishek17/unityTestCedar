using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    public float RangeMin;
    public float RangeMax;
    float DebugLineScale;
    LineRenderer LineVelocity;
    void Start()
    {
        Random.seed = (int)(System.DateTime.Now.Millisecond);
        transform.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(RangeMin, RangeMax), Random.Range(RangeMin, RangeMax), 0.0f);
        transform.position = new Vector3(Random.Range(-10.0f, 5.0f), Random.Range(-5.0f, 5.0f), 0.0f);
        DebugLineScale = 0.3f;
        LineVelocity = (LineRenderer)this.GetComponent(typeof(LineRenderer));
    }
    void Update()
    {
        LineVelocity.SetPosition(0, transform.position);
        LineVelocity.SetPosition(1, transform.position + GetComponent<Rigidbody>().velocity * DebugLineScale);
    }
}
