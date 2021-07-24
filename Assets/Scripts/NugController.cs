using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NugController : Tool //INHERITENCE
{
    Transform toolTransform;

    Quaternion startRotation;
    Vector3 startPosition;      // 0.6, -0.5, 0.75

    Quaternion fromRotation;
    Vector3 fromPosition;

    Quaternion toRotation;
    Vector3 toPos;
    [SerializeField] Vector3[] toRotationEuler;
    [SerializeField] Vector3[] toPosition;

    [SerializeField] float duration;
    private int numMovements = 3;

    private bool inMotion;

    // POLYMORPHISM
    protected override void ActionOne(){
        if(inMotion){
            StopAllCoroutines();
        }

        inMotion = true;

        if(toolTransform == null){
            toolTransform = transform.parent.gameObject.transform;
            startRotation = toolTransform.localRotation;
            startPosition = toolTransform.localPosition;
        }

        StartCoroutine(Swat());
    }

    IEnumerator Swat(){
        float time = 0;
        Debug.Log("Swatting");

        for(int i = 0; i < numMovements; i++){
            Debug.Log($"for {i}");
            fromRotation = toolTransform.localRotation;
            fromPosition = toolTransform.localPosition;
            toRotation = i < numMovements - 1 ? Quaternion.Euler(toRotationEuler[i]) : startRotation;
            toPos = i < numMovements - 1 ? toPosition[i] : startPosition;
Debug.Log($"toRot: {toRotation} - toPos: {toPos}");
            while(time < (duration / (float)numMovements * (i + 1))){
                toolTransform.localRotation = Quaternion.Slerp(fromRotation, toRotation, time / (duration / (float)numMovements * (i + 1)));
                toolTransform.localPosition = Vector3.Lerp(fromPosition, toPos, time / (duration / (float)numMovements * (i + 1)));
                time += Time.deltaTime;
                yield return null;
            }
        }

        toolTransform.localRotation = startRotation;
        toolTransform.localPosition = startPosition;
        inMotion = false;
    }
}
