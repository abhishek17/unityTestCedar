using UnityEngine;
using System.Collections;

public class PursueOrEvade : MonoBehaviour
{
    GameObject TargetPoint;
    public float MaxSpeed;
    public bool Pursuer;
    float DebugLineScale;
    public float RangeMin;
    public float RangeMax;
    Vector3 CheckVel = new Vector3();
    Vector3 Steering;
    float delayTime;
    float TimeSinceCollision;
    static int TimeNow = (int)(System.DateTime.Now.Millisecond);
    LineRenderer LineVelocity;
    LineRenderer LineSteer;
    bool Collided;
    void Start()
    {
        TargetPoint = GameObject.FindGameObjectsWithTag("TargetPoint")[0];
        Random.seed = TimeNow;
        transform.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(RangeMin, RangeMax), Random.Range(RangeMin, RangeMax), 0.0f);
        transform.position = new Vector3(Random.Range(-20.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0.0f);
        CheckVel = transform.GetComponent<Rigidbody>().velocity;
        DebugLineScale = 0.3f;
        Steering = new Vector3();
        delayTime = 1.0f;
        Collided = false;
        LineVelocity = (LineRenderer)this.GetComponent(typeof(LineRenderer));
        if (Pursuer)
            LineSteer = (LineRenderer)GameObject.Find("LineRendererChildPursuer").GetComponent((typeof(LineRenderer)));
        else
            LineSteer = (LineRenderer)GameObject.Find("LineRendererChildEvader").GetComponent((typeof(LineRenderer)));
    }

    void Update()
    {
        if (Collided)
            TimeSinceCollision += Time.deltaTime;

        Steering = (TargetPoint.transform.position - transform.position).normalized * MaxSpeed;
        Steering = Steering - GetComponent<Rigidbody>().velocity;

        if (!Pursuer)
            Steering = -Steering;

        CheckVel += Steering * Time.deltaTime;

        if (CheckVel.magnitude <= MaxSpeed)
            GetComponent<Rigidbody>().velocity = CheckVel;
        else
            GetComponent<Rigidbody>().velocity = CheckVel.normalized * MaxSpeed;

        CheckVel = GetComponent<Rigidbody>().velocity;
        if (TimeSinceCollision > delayTime)
            Application.LoadLevel(0);
       
        LineVelocity.SetPosition(0, transform.position);
        LineVelocity.SetPosition(1, transform.position + GetComponent<Rigidbody>().velocity * DebugLineScale);

        LineSteer.SetColors(Color.blue, Color.blue);
        LineSteer.SetPosition(0, transform.position);
        LineSteer.SetPosition(1, transform.position + Steering.normalized);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TargetPoint" && Pursuer)
        {
            TimeNow = (int)(System.DateTime.Now.Millisecond);
            Collided = true;
            other.GetComponent<Renderer>().enabled = false;
            Application.LoadLevel(0);
        }
    }
}
