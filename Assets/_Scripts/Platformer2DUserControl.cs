using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
    public enum FSMState { MoveState, InteractingState };
    public FSMState state = FSMState.MoveState;

    private PlatformerCharacter2D m_Character;
    private bool m_Jump;
    public bool iNeverFreeze;
    //Rigidbody2D rb;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public bool isTeleporting;


    [SerializeField] private Animator m_Anim;
    private void Awake()
    {
        m_Character = GetComponent<PlatformerCharacter2D>();
        rb = GetComponent<Rigidbody2D>();
        m_Anim = GetComponentInChildren<Animator>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }


    }


    private void FixedUpdate()
    {
        if (iNeverFreeze)
        {
            
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            //if (!m_Character.activateStair)
            //{
                m_Character.Move(h/*, crouch*/, m_Jump);
            //}
            m_Jump = false;
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
            m_Anim.SetFloat("Speed", 0);
        }

    }

}
