using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWand : MonoBehaviour
{
    public LayerMask layerMask;
    public OVRInput.RawButton ShootingButton;
    public LineRenderer LinePrefab;
    public GameObject RayImpactPrefab;
    public Transform ShootingPoint;
    public float MaxLineDistance = 5;
    public float LineShowTimer = 0.3f;
    public AudioSource ShootingSource;
    public AudioClip ShootingAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(ShootingButton))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        ShootingSource.PlayOneShot(ShootingAudioClip);

        Ray ray = new Ray(ShootingPoint.position,ShootingPoint.forward);
        bool bHasHit = Physics.Raycast(ray, out RaycastHit hit, MaxLineDistance, layerMask);

        Vector3 endPoint = Vector3.zero;

        //Use Dependency Injection in future
        if (bHasHit)
        {
            endPoint = hit.point;

            GoblinComponent goblin = hit.transform.GetComponentInParent<GoblinComponent>();

            if(goblin)
            {
                hit.collider.enabled = false;
                goblin.Die();
            }
            else
            {
                Quaternion rayImpactRotation = Quaternion.LookRotation(-hit.normal);

                GameObject rayImpact = Instantiate(RayImpactPrefab, hit.point, rayImpactRotation);
                Destroy(rayImpact, 1);
            }
        }
        else
        {
            endPoint = ShootingPoint.position + ShootingPoint.forward * MaxLineDistance;
        }

        LineRenderer line = Instantiate(LinePrefab);
        line.positionCount = 2;
        line.SetPosition(0, ShootingPoint.position); 
        line.SetPosition(1,endPoint);

        Destroy(line.gameObject, LineShowTimer);
    }
}
