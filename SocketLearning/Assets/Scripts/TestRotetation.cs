using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotetation : MonoBehaviour
{
    TweenRotation tr, mSecondTr;

    // Use this for initialization
    void Start()
    {
        tr = transform.GetChild(0).GetComponent<TweenRotation>();
        mSecondTr = transform.GetChild(1).GetComponent<TweenRotation>();
        tr.enabled = false;
        mSecondTr.enabled = false;
        tr.from = new Vector3(0, 0, 0);
        tr.to = new Vector3(0, 90, 0);
        tr.ResetToBeginning();
        tr.AddOnFinished(() =>
        {
            mSecondTr.Play(true);
            Debug.Log("mSecondTr.Play(true);");
        });
        mSecondTr.enabled = false;
        mSecondTr.from = new Vector3(0, 90, 0);
        mSecondTr.to = new Vector3(0, 0, 0);
        mSecondTr.ResetToBeginning();
        tr.Play(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
