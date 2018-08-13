using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

    [HideInInspector] public Vector3 nextPos;
    [SerializeField] private float speed;
    public Transform PlatformTrans;
    private Vector3 posIni = new Vector3(0, 0, 0);
	
	// Update is called once per frame
	void Update () {
        if(nextPos != posIni && PlatformTrans.localPosition != nextPos)
        {
            Move();
        }

	}

    private void Move()
    {
        if(nextPos != posIni)
        {
            PlatformTrans.localPosition = Vector3.MoveTowards(PlatformTrans.localPosition, nextPos, speed * Time.deltaTime);
        }
    }
}
