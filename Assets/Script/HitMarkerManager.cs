using UnityEngine;
using System.Collections;

public class HitMarkerManager : MonoBehaviour
{
    public static HitMarkerManager Instance;

    public GameObject hitMarker;
    public float showTime = 0.1f;

    void Awake()
    {
        Instance = this;
    }

    public void ShowHit()
    {
        StopAllCoroutines();
        StartCoroutine(ShowRoutine());
    }

    IEnumerator ShowRoutine()
    {
        hitMarker.SetActive(true);
        yield return new WaitForSeconds(showTime);
        hitMarker.SetActive(false);
    }
}