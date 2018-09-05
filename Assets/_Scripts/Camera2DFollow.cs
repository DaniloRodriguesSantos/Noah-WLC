using System;
using UnityEngine;


public class Camera2DFollow : MonoBehaviour
{
    #region General Variables
    private Vector3 targetPos;
    private Transform followTarget_Player;
    private float moveSpeed = 2f;
    private Transform trans;
    private Camera main_Camera;
    //public Camera mapCamera;
    #endregion

    #region Camera Boundaries
    public bool bounds;
    private bool followTarget = true;
    [HideInInspector] public Vector3 minCameraPos;
    [HideInInspector] public Vector3 maxCameraPos;
    private PlatformerCharacter2D platformerCharacter2D_Script;
    [HideInInspector] public float halfHeight;
    [HideInInspector] public float halfWidth;
    [HideInInspector] public float clampedX;
    [HideInInspector] public float clampedY;

    [Space(10)]
    [Header("Minigames")]
    public GameObject miniG_Waves_BG;
    public GameObject miniG_Boss_BG;
    #endregion

    void Awake ()
	{
		trans = GetComponent<Transform> ();
        main_Camera = GetComponent<Camera>();
        platformerCharacter2D_Script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformerCharacter2D>();
        followTarget_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

	// Use this for initialization
	private void Start ()
	{
		trans.parent = null;
	}


	// Update is called once per frame
	private void Update ()
	{
        if (followTarget)
        {
            targetPos = new Vector3(followTarget_Player.position.x, followTarget_Player.position.y, trans.position.z);
            trans.position = Vector3.Lerp(trans.position, targetPos, moveSpeed * Time.deltaTime);
        }

        if (bounds)
        {
            clampedX = Mathf.Clamp(trans.position.x, minCameraPos.x + halfWidth, maxCameraPos.x - halfWidth);
            clampedY = Mathf.Clamp(trans.position.y, minCameraPos.y + halfHeight, maxCameraPos.y - halfHeight);
            trans.position = new Vector3(clampedX, clampedY, trans.position.z);
        }
    }

    public void changeCamera_MiniGWaves()
    {
        bounds = false;
        followTarget = false;
        platformerCharacter2D_Script.changeCameraSize = false;
        main_Camera.orthographicSize = 5f;
        trans.parent = miniG_Waves_BG.transform;
        trans.localPosition = new Vector3(0, 0, -10f);
    }

    public void changeCamera_MiniGBoss()
    {
        bounds = false;
        followTarget = false;
        platformerCharacter2D_Script.changeCameraSize = false;
        main_Camera.orthographicSize = 5f;
        trans.parent = miniG_Boss_BG.transform;
        trans.localPosition = new Vector3(0, 0, -10f);
    }

    public void changeCamera_ToPlayer()
    {
        bounds = true;
        followTarget = true;
        platformerCharacter2D_Script.changeCameraSize = true;
    }
}

