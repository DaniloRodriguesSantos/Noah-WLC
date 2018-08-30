using System;
using UnityEngine;


public class Camera2DFollow : MonoBehaviour
{
	public Transform target;
	public float damping = 1;
	public float lookAheadFactor = 3;
	public float lookAheadReturnSpeed = 0.5f;
	public float lookAheadMoveThreshold = 0.1f;

	private float m_OffsetZ;
	private Vector3 m_LastTargetPosition;
	private Vector3 m_CurrentVelocity;
	private Vector3 m_LookAheadPos;

    private Camera main_Camera;
    #region Camera Boundaries

    public bool bounds;
    private bool followTarget = true;
    public Camera mapCamera;
    [Space(10)]
    [Header("BGs")]
    public GameObject Papelaria_BG;
    public GameObject Quarto_Noah_BG;
    public GameObject SalaDeEstar_Noah_BG;
    public GameObject Portaria_Manha_BG;
    public GameObject Portaria_Tarde_BG;
    public GameObject PortariaMetro_BG;
    public GameObject MetroEscola_BG;
    public GameObject PapelariaMercado_BG;
    public GameObject Escola_BG;
    public GameObject Refeitorio_BG;
    public GameObject SalaDeAula_BG;
    public GameObject Pensamento_Tipo2_BG;

    [Space(10)]
    public GameObject ParteCima_MetroEscola_BG;
    public GameObject ParteBaixo_MetroEscola_BG;
    [Space(10)]
    public GameObject ParteCima_PortariaMetro_BG;
    public GameObject ParteBaixo_PortariaMetro_BG;
    [Space(10)]
    public GameObject Nivel1_PapelariaMercado_BG;
    public GameObject Nivel2_PapelariaMercado_BG;
    public GameObject Nivel3_PapelariaMercado_BG;

    [Space(10)]
    [Header("Papelaria")]
    public Vector3 Papelaria_minCam_Pos = new Vector3(0, 0, -10);
    public Vector3 Papelaria_maxCam_Pos = new Vector3(0, 0, -10);
    [Space(10)]
    [Header("Quarto Noah")]
    public Vector3 QuartoNoah_minCam_Pos = new Vector3(0, 0, -10);
    public Vector3 QuartoNoah_maxCam_Pos = new Vector3(0, 0, -10);
    [Space(10)]
    [Header("Sala de Estar")]
    public Vector3 SalaDeEstar_Noah_minCam_Pos = new Vector3(0, 0, -10);
    public Vector3 SalaDeEstar_Noah__maxCam_Pos = new Vector3(0, 0, -10);
    [Space(10)]
    [Header("Portaria")]
    public Vector3 Portaria_minCam_Pos = new Vector3(0, 0, -10);
    public Vector3 Portaria_maxCam_Pos = new Vector3(0, 0, -10);
    [Space(10)]
    [Header("Escola")]
    public Vector3 Escola_minCam_Pos = new Vector3(0, 0, -10);
    public Vector3 Escola_maxCam_Pos = new Vector3(0, 0, -10);
    [Space(10)]
    [Header("Refeitorio")]
    public Vector3 Refeitorio_minCam_Pos = new Vector3(0, 0, -10);
    public Vector3 Refeitorio_maxCam_Pos = new Vector3(0, 0, -10);
    [Space(10)]
    [Header("Sala de Aula")]
    public Vector3 SalaDeAula_minCam_Pos = new Vector3(0, 0, -10);
    public Vector3 SalaDeAula_maxCam_Pos = new Vector3(0, 0, -10);
    [Space(10)]
    [Header("Pensamento Tipo 2")]
    public Vector3 Pensamento_Tipo2_minCam_Pos = new Vector3(0, 0, -10);
    public Vector3 Pensamento_Tipo2_maxCam_Pos = new Vector3(0, 0, -10);

    [Space(10)]
    [Header("Metro Escola")]
    public Vector3 MetroEscola_minCam_Pos = new Vector3(0, 0, -10);
    public Vector3 MetroEscola_maxCam_Pos = new Vector3(0, 0, -10);
    [Space(10)]
    public Vector3 ParteCima_MetroEscola_minCam_Pos = new Vector3(0, 0, -10);
    public Vector3 ParteCima_MetroEscola_maxCam_Pos = new Vector3(0, 0, -10);
    public Vector3 ParteBaixo_MetroEscola_minCam_Pos = new Vector3(0, 0, -10);
    public Vector3 ParteBaixo_MetroEscola_maxCam_Pos = new Vector3(0, 0, -10);

    [Space(10)]
    [Header("Portaria Metro")]
    public Vector3 PortariaMetro_minCam_Pos = new Vector3(0, 0, -10);
    public Vector3 PortariaMetro_maxCam_Pos = new Vector3(0, 0, -10);
    [Space(10)]
    public Vector3 ParteCima_PortariaMetro_minCam_Pos = new Vector3(0, 0, -10);
    public Vector3 ParteCima_PortariaMetro_maxCam_Pos = new Vector3(0, 0, -10);
    public Vector3 ParteBaixo_PortariaMetro_minCam_Pos = new Vector3(0, 0, -10);
    public Vector3 ParteBaixo_PortariaMetro_maxCam_Pos = new Vector3(0, 0, -10);

    [Space(10)]
    [Header("Papelaria Mercado")]
    public Vector3 PapelariaMercado_minCam_Pos = new Vector3(0, 0, -10);
    public Vector3 PapelariaMercado_maxCam_Pos = new Vector3(0, 0, -10);
    [Space(10)]
    public Vector3 Nivel1_PapelariaMercado_minCam_Pos = new Vector3(0, 0, -10);
    public Vector3 Nivel1_PapelariaMercado_maxCam_Pos = new Vector3(0, 0, -10);
    public Vector3 Nivel2_PapelariaMercado_minCam_Pos = new Vector3(0, 0, -10);
    public Vector3 Nivel2_PapelariaMercado_maxCam_Pos = new Vector3(0, 0, -10);
    public Vector3 Nivel3_PapelariaMercado_minCam_Pos = new Vector3(0, 0, -10);
    public Vector3 Nivel3_PapelariaMercado_maxCam_Pos = new Vector3(0, 0, -10);

    [Space(10)]
    [Header("Minigames")]
    public GameObject miniG_Waves_BG;
    public GameObject miniG_Boss_BG;


    [HideInInspector] public Vector3 minCameraPos;
    [HideInInspector] public Vector3 maxCameraPos;
    private Transform trans;

    private PlatformerCharacter2D platformerCharacter2D_Script;
    #endregion

    void Awake ()
	{
		trans = GetComponent<Transform> ();
        main_Camera = GetComponent<Camera>();
        platformerCharacter2D_Script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlatformerCharacter2D>();
    }

	// Use this for initialization
	private void Start ()
	{
		m_LastTargetPosition = target.position;
		m_OffsetZ = (transform.position - target.position).z;
		trans.parent = null;
	}


	// Update is called once per frame
	private void Update ()
	{
        if (followTarget)
        {
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(trans.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            trans.position = newPos;

            m_LastTargetPosition = target.position;
        }

		if (bounds) {
			trans.position = new Vector3 (Mathf.Clamp (trans.position.x, minCameraPos.x, maxCameraPos.x), Mathf.Clamp (trans.position.y, minCameraPos.y, maxCameraPos.y), -10f); 
		}

        if (Input.GetKeyDown(KeyCode.H))
        {
            changeCamera_MiniGBoss();
        }

        if (Input.GetKey(KeyCode.B))
        {
            PlayerPrefs.SetString("canRetry", "true");
            print("You can now play forever");
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

