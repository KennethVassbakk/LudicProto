using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class affectFrost : MonoBehaviour
{
    FrostEffect frost;
    private float stdSeeThrough;
    public float freezingSeeThrough = 2f;

    private bool isFreezing = false;
    private float frostFadeDuration = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        frost = GetComponent<FrostEffect>();
        stdSeeThrough = frost.seethroughness;
    }

    public void setFreezing(bool state) {
        if(state) {
            StartCoroutine(fadeFreeze(frost, freezingSeeThrough, frostFadeDuration));
        } else {
            StartCoroutine(fadeFreeze(frost, stdSeeThrough, frostFadeDuration));
        }
    }

    public static IEnumerator fadeFreeze(FrostEffect frost, float target, float duration) {
        float currentTime = 0;
        float currentEffect = frost.seethroughness;
        float startEffect = frost.seethroughness;


        while (currentTime < duration) {
            currentTime += Time.deltaTime;
            float newVal = Mathf.Lerp(currentEffect, target, currentTime / duration);
            frost.seethroughness = newVal;

            if (currentTime >= duration) {
                frost.seethroughness = target;
            }
            yield return null;
        }

        yield break;
    }
}
