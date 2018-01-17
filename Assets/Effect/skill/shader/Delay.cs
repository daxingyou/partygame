using UnityEngine;
using System.Collections;

public class Delay : MonoBehaviour {
	
	public float delayTime = 1.0f;
	
	// Use this for initialization
	void Start () {
        DeactivateChildren(gameObject, false);
        Invoke("DelayFunc", delayTime);
	}
	
	void DelayFunc()
	{
        Debug.Log("ok");
        DeactivateChildren(gameObject, true);

        // gameObject.SetActiveRecursively(true);
    }

    public void DeactivateChildren(GameObject go, bool state)
    {
        go.SetActive(state);

        foreach (Transform child in go.transform)
        {
            DeactivateChildren(child.gameObject, state);
        }
    }

}
