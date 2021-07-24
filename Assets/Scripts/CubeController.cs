using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : Tool  //INHERITENCE
{
    Transform toolTransform;
    Quaternion startRotation;
    Quaternion from;
    Quaternion to;

    [SerializeField] float duration = 3.0f;
    [SerializeField] int numShakes = 10;
    private float range = 20.0f;

    private bool inMotion;

    // POLYMORPHISM
    protected override void ActionOne()
    {
        if(inMotion){
            StopAllCoroutines();
        }

        inMotion = true;

        if(toolTransform == null){
            toolTransform = transform.parent.gameObject.transform;
            startRotation = toolTransform.localRotation;
        }

        from = startRotation;

        StartCoroutine(ShakeIt());
    }

    IEnumerator ShakeIt(){
        float time = 0;

        for(int i = 0; i < numShakes; i++){
            from = toolTransform.localRotation;
            to = Quaternion.Euler(Random.Range(-range, range) + startRotation.x, Random.Range(-range, range) + startRotation.y, Random.Range(-range, range) + startRotation.z);

            while (time < (duration / (float)(numShakes) * (i + 1))){
                toolTransform.localRotation = Quaternion.Slerp(from, to, time / (duration / (float)(numShakes) * (i + 1)));
                time += Time.deltaTime;
                yield return null;
            }
        }

        toolTransform.localRotation = startRotation;
        inMotion = false;
    }
}
