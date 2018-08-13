using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformChooseController : MonoBehaviour
{

    Audio_MainScript script_Audio_MainScript;

    //private Animator animator;
    private float t;
    [SerializeField] private float timeToReachMaxSpeed;
    #region Generic Variable
    public float speed;
    public float max_Speed;
    private Transform trans;
    #endregion

    #region Waypoints
    public GameObject path_Parent;
    public WaypointControl[] waypoints;
    public float distanceToChangeWaypoint;
    private int currentWaypoint;
    #endregion

    #region ChooseType
    [SerializeField] private bool isChooseObj;
    [SerializeField] private bool isBreakIce;
    #endregion

    #region ChooseObj
    [Space(10)]
    [Header("Choose OBJ")]
    [SerializeField] private SpriteRenderer objChooseImg;
    private float time;
    [SerializeField] private float timeToAppear;
    private bool appearImg = false;
    #endregion

    #region Break Ice 
    [Space(10)]
    [Header("Break Ice")]
    [SerializeField] private GameObject objBreakIce_Sprite;
    [SerializeField] private GameObject objBreakIce_Anim;
    private Animator breakIce_Anim;
    private PensamentoController pensamentoController_Script;
    private bool platformSelected = false;
    #endregion

    private void Awake()
    {
        script_Audio_MainScript = GameObject.Find("Main Camera").GetComponent<Audio_MainScript>();
        trans = GetComponent<Transform>();
        waypoints = path_Parent.GetComponentsInChildren<WaypointControl>();
        pensamentoController_Script = GameObject.Find("PensamentoController").GetComponent<PensamentoController>();
    }

    private void Start()
    {
        if (this.objChooseImg != null)
        {
            this.objChooseImg.color = new Color(1f, 1f, 1f, 0);
        }

        objBreakIce_Sprite.SetActive(true);
        objBreakIce_Anim.SetActive(false);
    }

    private void FixedUpdate()
    {
        platformChase();
    }

    private void Update()
    {
        t += Time.deltaTime / timeToReachMaxSpeed;
        speed = Mathf.Lerp(1f, max_Speed, t);

        this.time += Time.deltaTime / timeToAppear;

        if(this.objChooseImg != null)
        {
            if (this.appearImg)
            {
                this.objChooseImg.color = new Color(1f, 1f, 1f, Mathf.Lerp(0f, 1f, time));
            }
            else
            {
                this.objChooseImg.color = new Color(1f, 1f, 1f, Mathf.Lerp(1f, 0f, time));
            }
        }
    }

    private void platformChase()
    {
        Vector3 wpDir = waypoints[currentWaypoint].transform.position - trans.position;
        if (wpDir.magnitude <= distanceToChangeWaypoint)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
                currentWaypoint = 0;

        }
        else
        {
            trans.position = Vector3.MoveTowards(trans.position, waypoints[currentWaypoint].transform.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isChooseObj)
            {
                if (this.objChooseImg.color.a == 0)
                {
                    this.time = 0;
                    this.appearImg = true;
                }
            }

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isChooseObj)
            {
                if (this.objChooseImg != null)
                {
                    this.appearImg = true;
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    pensamentoController_Script.voltaPosicao = true;
                }
            }
            if (isBreakIce)
            {
                if (!platformSelected)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        script_Audio_MainScript.tocar_GeloQuebrado();
                        objBreakIce_Sprite.SetActive(false);
                        objBreakIce_Anim.SetActive(true);
                        StartCoroutine(ReturnNoahToRealLife(1.5f));
                        platformSelected = true;
                    }
                }
            }
        }
    }

    private IEnumerator ReturnNoahToRealLife(float seconds)
    {
        yield return new WaitForSecondsRealtime(seconds);
        platformSelected = false;
        pensamentoController_Script.voltaPosicao = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isChooseObj)
            {
                if (this.objChooseImg != null)
                {
                    this.time = 0;
                    this.appearImg = false;
                }
            }
        }
    }
}
