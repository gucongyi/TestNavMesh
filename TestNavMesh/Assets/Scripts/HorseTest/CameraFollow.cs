using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float beginX = 1.5f;
    private float endX = -1.5f;

    private float beginY = 1.61f;
    private float endY = 3.3f;

    private float beginZ = 3.8f;
    private float endZ = 26f;

    public Transform FollowTarget;

    private float Smooth=800f;

    private float randomTime = 5f;

    private float randomX;
    private float randomY;
    private float randomZ;
    // Use this for initialization
    void Start () {
        randomX = -0.6400146f;
        randomY = 2.14f;
        randomZ = 3.550003f;
    }
	
	void LateUpdate ()
	{
	    randomTime -= Time.deltaTime;
	    if (randomTime <= 0f)
	    {
	        randomX = Random.Range(beginX, this.endX);
	        randomY = Random.Range(this.beginY, this.endY);
	        randomZ = Random.Range(this.beginZ, this.endZ);
	        randomTime = Random.Range(5f, 10f);
	    }
        float lerpX = Mathf.Lerp(this.transform.localPosition.x, randomX, Time.deltaTime * Smooth);
	    float lerpY = Mathf.Lerp(this.transform.localPosition.y, randomY, Time.deltaTime * Smooth);
	    float lerpZ = Mathf.Lerp(this.transform.localPosition.z, randomZ, Time.deltaTime * Smooth);
	    transform.localPosition= FollowTarget.transform.localPosition+new Vector3(lerpX, lerpY, lerpZ);
    }
}
