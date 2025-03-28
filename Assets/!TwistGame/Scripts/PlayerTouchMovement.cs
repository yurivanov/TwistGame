using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerTouchMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 JoystickSize = new Vector2(200, 200);
    [SerializeField]
    private FloatingJoystickS Joystick;
    [SerializeField]
    private NavMeshAgent Player;

    public bool ShootWeapon = false;

    private Finger MovementFinger;
    private Vector2 MovementAmount;
    private CharacterHealth characterHealth;
    private Animator anim; // our animator

    void Start()
    {
        characterHealth = GetComponent<CharacterHealth>();
        anim = GetComponent<Animator>(); // get the animator component
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleLoseFinger;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleLoseFinger;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void HandleFingerMove(Finger MovedFinger)
    {
        if (MovedFinger == MovementFinger)
        {
            Vector2 knobPosition;
            float maxMovement = JoystickSize.x / 2f;
            ETouch.Touch currentTouch = MovedFinger.currentTouch;

            if (Vector2.Distance(
                    currentTouch.screenPosition,
                    Joystick.RectTransform.anchoredPosition
                ) > maxMovement)
            {
                knobPosition = (
                    currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition
                    ).normalized
                    * maxMovement;
            }
            else
            {
                knobPosition = currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition;
            }

            Joystick.Knob.anchoredPosition = knobPosition;
            MovementAmount = knobPosition / maxMovement;
        }
    }

    private void HandleLoseFinger(Finger LostFinger)
    {
        if (LostFinger == MovementFinger)
        {
            MovementFinger = null;
            Joystick.Knob.anchoredPosition = Vector2.zero;
            Joystick.gameObject.SetActive(false);
            MovementAmount = Vector2.zero;
        }
    }

    private void HandleFingerDown(Finger TouchedFinger)
    {
        if (MovementFinger == null && TouchedFinger.screenPosition.x <= Screen.width / 2f)
        {
            MovementFinger = TouchedFinger;
            MovementAmount = Vector2.zero;
            Joystick.gameObject.SetActive(true);
            Joystick.RectTransform.sizeDelta = JoystickSize;
            Joystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
        }
    }

    private Vector2 ClampStartPosition(Vector2 StartPosition)
    {
        if (StartPosition.x < JoystickSize.x / 2)
        {
            StartPosition.x = JoystickSize.x / 2;
        }

        if (StartPosition.y < JoystickSize.y / 2)
        {
            StartPosition.y = JoystickSize.y / 2;
        }
        else if (StartPosition.y > Screen.height - JoystickSize.y / 2)
        {
            StartPosition.y = Screen.height - JoystickSize.y / 2;
        }

        return StartPosition;
    }

    private void Update()
    {
        // Check if the player is dead.
        if (characterHealth.health <= 0)
        {
            anim.enabled = false;
           // anim.Play("DeadCharacter"); // if player is dead, use idle animation
            return; // Exit the method early if the player is dead.
        }

        Vector3 scaledMovement = Player.speed * Time.deltaTime * new Vector3( MovementAmount.x, 0, MovementAmount.y);

        Player.transform.LookAt(Player.transform.position + scaledMovement, Vector3.up);

        // check if the player is moving or standing still
        if (scaledMovement != Vector3.zero)
        {
            Player.Move(scaledMovement);
                        
            anim.Play("WalkCharacter"); // if player is moving, use walk animation
                                       // anim.SetBool("IdleCharacter", false);
           
        
        }

        else
        {
                      
                // anim.SetBool("WalkCharacter", false); // if player is not moving, use idle animation
                anim.Play("IdleCharacter");
            
        }


        

        
    }
}
