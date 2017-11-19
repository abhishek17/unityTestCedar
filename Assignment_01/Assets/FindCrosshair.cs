using UnityEngine;
using System.Collections;

public class FindCrosshair : MonoBehaviour
{
    GameObject Target;
    GameObject Pursuer;
    PursueOrEvade scr_pursuer;
    Vector3 CrosshairPosition;
        
    void Start()
    {
        Target = GameObject.FindGameObjectsWithTag("Target")[0];
        Pursuer = GameObject.FindGameObjectsWithTag("Pursue")[0];
        scr_pursuer = (PursueOrEvade)Pursuer.gameObject.GetComponent(typeof(PursueOrEvade));
        CrosshairPosition = new Vector3();
    }
    void Update()
    {
        float DistanceBetweenTargetAndPursuer = Distance(Target.transform.position, Pursuer.transform.position);
        float EstimatedTime = DistanceBetweenTargetAndPursuer / (scr_pursuer.MaxSpeed);
        CrosshairPosition = Target.transform.position + EstimatedTime * Target.GetComponent<Rigidbody>().velocity;
        transform.position = CrosshairPosition;
    }
    float Distance(Vector3 first, Vector3 Second)
    {
        return (Mathf.Sqrt((first.x - Second.x) * (first.x - Second.x) + (first.y - Second.y) * (first.y - Second.y) + (first.z - Second.z) * (first.z - Second.z)));
    }
}


