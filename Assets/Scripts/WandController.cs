using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : Tool
{
    Transform toolTransform;
    Quaternion startRotation;
    Quaternion from;
    Quaternion to;

    [SerializeField] float duration = 3.0f;

    private bool inMotion;

    protected override void ActionOne()
    {
        if(inMotion){   // resets motion if it was already going
            StopAllCoroutines();
        }

        inMotion = true;

        if(toolTransform == null){
            toolTransform = transform.parent.gameObject.transform;
            startRotation = toolTransform.localRotation;
        }
        
        from = startRotation;
        to = Quaternion.Euler(50, -20, 0);
        
        StartCoroutine(SwishAndFlick());
    }

    /// <summary>
    /// This did not turn out how I wanted but time to move on
    /// </summary>
    /// <returns></returns>
    IEnumerator SwishAndFlick(){
        float time = 0f; 

        while(time < duration * 0.25f){
            toolTransform.localRotation = Quaternion.Slerp(from, to, time / (duration * 0.25f));
            time += Time.deltaTime;
            yield return null;
        }

        SetStep(70, -40);
        while(time < duration * 0.5f){
            toolTransform.localRotation = Quaternion.Slerp(from, to, time / (duration * 0.5f));
            time += Time.deltaTime;
            yield return null;
        }

        SetStep(90, -20);
        while(time < duration * 0.75f){
            toolTransform.localRotation = Quaternion.Slerp(from, to, time / (duration * 0.75f));
            time += Time.deltaTime;
            yield return null;
        }

        while(time < duration){
            toolTransform.localRotation = Quaternion.Slerp(to, startRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        toolTransform.localRotation = startRotation;
        inMotion = false;
    }

    private void SetStep(float x, float y){
        from = to;
        to = Quaternion.Euler(x, y, 0);
    }
}
