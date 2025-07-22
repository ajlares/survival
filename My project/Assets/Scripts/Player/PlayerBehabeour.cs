using Mirror;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehabeour : NetworkBehaviour
{
  #region variables
    [Header(" basics ")]
    //private InfoPlayer _usernamePanel;
    [SerializeField] private CharacterController CC;
    [SerializeField] private CinemachineCamera playerCamera;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject attackZone;
    [SerializeField] private bool isAttacking;
    [SerializeField] private GameObject interactZone;
    [SerializeField] private bool isInteracting;
    [SerializeField] private Animator anim;
    [SerializeField] private string currentAnim = " ";
    [Header("Stats")] 
    [SyncVar (hook = nameof(HealthChanged))]
    [SerializeField] private float actualLife;
    [SerializeField] private float maxLife;
    [SerializeField] private bool isTakingDamage;

    [SerializeField] private float damage;
    
    [Header("Nametag")]
    //[SerializeField] private TextMeshPro nametagObject;
    //[SyncVar(hook = nameof(NameChanged))]
    private string username;
    
    [Header("movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private Vector3 movementInput;
    [SerializeField] private float  turnSmoothVelocity;
    [SerializeField] private float turnSmoothTime = .1F;
    [SerializeField] private Transform cam;
    [SerializeField] private float jumpForce;
    #endregion
    #region syncvars
    private void HealthChanged(float oldHealth, float newHealth)
    {
        actualLife = newHealth;
    }
    [Command]
    private void CommandChangeName(string maiName)
    {
        username = maiName;
    }
    private void NameChanged(string oldName, string newName)
    {
        //nametagObject.text = newName;
        name = newName;
    }
    #endregion
    #region movements

    public void Move(InputAction.CallbackContext context)
    {
        if(!isLocalPlayer) return;
        movementInput = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!isLocalPlayer) return;
        if (context.action.IsPressed())
        {
            CC.Move(Vector3.up * jumpForce);
        }
    }
    #endregion
    
    #region attacks and interacts

    public void attack(InputAction.CallbackContext context)
    {
        if (!isLocalPlayer) return;
        if(isAttacking || isInteracting || isTakingDamage) return;
        if (context.action.IsPressed())
        {
            attackZone.gameObject.SetActive(true);
            isAttacking = true;
            changeAnim("PunchLeft");
        // PunchLeft
        }
    }

    public void interact(InputAction.CallbackContext context)
    {
        if (!isLocalPlayer) return;
        if(isAttacking || isInteracting || isTakingDamage) return;
        if (context.action.IsPressed())
        {
            interactZone.gameObject.SetActive(true);
            isInteracting = true;
            changeAnim("Gathering");
        }
    }
    public float getPlayerDamage()
    {
        return damage;
    }

    public void TakeDamage()
    {
        changeAnim("GetHit");
        isTakingDamage = true;
    }

    public void deathPlayer()
    {
        Debug.Log("deathPlayer");
    }
    public void stopTakeDamage()
    {
        
        isTakingDamage = false;
    }

    public void StopAttacking()
    {
        attackZone.gameObject.SetActive(false);
        isAttacking = false;
    }

    public void stopInteractting()
    {
        interactZone.gameObject.SetActive(false);
        isInteracting = false;
    }
    #endregion
    
    #region anims

    private void checkAnimation()
    {
        Vector2 checkmove = new Vector2(movementInput.x, movementInput.z);
        if (checkmove.y > 0)
            changeAnim("RunForward");
        else if (checkmove.y < 0)
            changeAnim("RunBackward");
        else if (checkmove.x == 1)
            changeAnim("StrafeRight");
        else if (checkmove.x == -1)
            changeAnim("StrafeLeft");
        else
            changeAnim("Idle");
        
    }
    private void changeAnim(string newAnim, float crossfade = 0f)
    {
        if (currentAnim != newAnim)
        {
            currentAnim = newAnim;
            anim.CrossFade(newAnim,crossfade,0);
        }
    }
    
    
    #endregion
    
    #region Unity basics

    private void Start()
    {
        if (!isLocalPlayer) anim.applyRootMotion = false; return;
        actualLife = maxLife;
        CC = GetComponent<CharacterController>();
        changeAnim("Idle");
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        if(isAttacking || isInteracting || isTakingDamage )return;
        checkAnimation();
    }

    private void FixedUpdate()
    {
        if(!isLocalPlayer) return;
        if(isAttacking || isInteracting || isTakingDamage )return;
        if (movementInput.magnitude > 0)
        {
            float targetAngle = Mathf.Atan2(movementInput.x, movementInput.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            if (movementInput.z > 0)
            {
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle, ref turnSmoothVelocity,turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            CC.Move(moveDir.normalized * movementSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }
    #endregion
    
    #region miror callbaks
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        if(!isLocalPlayer) return;
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera.gameObject.SetActive(true);
        playerInput.enabled = true;
        interactZone.gameObject.SetActive(false);
        attackZone.gameObject.SetActive(false);
        isAttacking = false;
        isInteracting = false;
        //_usernamePanel = GameObject.FindGameObjectWithTag("Username").GetComponent<InfoPlayer>();
        //CommandChangeName(_usernamePanel.PideUsuario());
        //_usernamePanel.gameObject.SetActive(false);
    }
    #endregion
}
