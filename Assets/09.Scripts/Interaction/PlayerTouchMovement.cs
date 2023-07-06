using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerTouchMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 JoystickSize = new Vector2(300, 300);
    [SerializeField]
    private FloatingJoystick Joystick;
    
    [SerializeField]
    private GameObject Player;
    //private object movescript;

    private Finger MovementFinger;
    [SerializeField]
    private Vector2 MovementAmount;
    
    private Rigidbody rigidbody;
    public float rotSpeed;
    public float moveSpeed;
    private Vector3 dir = Vector3.zero;

    [SerializeField]
    private GameObject Model;
    private Animator ModelAnimator;
    
    private Finger buttonClickFinger;
    
    public bool isInteractionButtonOn=false;
    
    [SerializeField]
    public GameObject InteractionButton;
    private RectTransform buttonRectTransform;
        
    public bool isGaming=true;

    [SerializeField] private GameObject GameManagerObject;
    private GameManager _gameManager;
    private float buttonOnStart = 0.0f;
    private float buttonOnEnd;
    
    [SerializeField] private GameObject PlayerCollider;
    
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
            Joystick.gameObject.SetActive(false);
            Joystick.Knob.anchoredPosition = Vector2.zero;
            MovementAmount = Vector2.zero;
        }
        else if (LostFinger == buttonClickFinger)
        {
            buttonClickFinger = null; 
            InteractionButton.gameObject.SetActive(false);
            Joystick.RectTransform.anchoredPosition = Vector2.zero;
        }
    }

    private void HandleFingerDown(Finger TouchedFinger)
    {
        if (MovementFinger == null && isGaming && TouchedFinger.screenPosition.x <= Screen.width / 2f)
        {
            MovementFinger = TouchedFinger;
            MovementAmount = Vector2.zero;
            Joystick.gameObject.SetActive(true);
            Joystick.RectTransform.sizeDelta = JoystickSize;//위치고정
            Joystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
        }
        else if (buttonClickFinger == null && isGaming && TouchedFinger.screenPosition.x > Screen.width / 2f)
        {
            buttonOnStart = _gameManager.playTime;
            buttonClickFinger = TouchedFinger;
            InteractionButton.gameObject.SetActive(true);
            buttonRectTransform.position = TouchedFinger.screenPosition;
            isInteractionButtonOn = true;
        }
    }

    private Vector2 ClampStartPosition(Vector2 StartPosition)
    {
        if (StartPosition.x < JoystickSize.x / 2)
        {
            StartPosition.x = JoystickSize.x / 2;
        }
        
        if (StartPosition.y < JoystickSize.y / 2)//joystick
        {
            StartPosition.y = JoystickSize.y / 2;
        }
        else if (StartPosition.y > Screen.height - JoystickSize.y / 2) //joystick
        {
            StartPosition.y = Screen.height - JoystickSize.y / 2;
        }

        return StartPosition;
    }

    private void Awake()
    {
        _gameManager = GameManagerObject.GetComponent<GameManager>();
        ModelAnimator=Model.GetComponent<Animator>();
    }
    private void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        buttonRectTransform = InteractionButton.GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        Vector3 scaledMovement = moveSpeed * Time.deltaTime * new Vector3(MovementAmount.x, 0, MovementAmount.y);
        UpdatePlayerWalking();
        Player.transform.LookAt(Player.transform.position + scaledMovement, Vector3.up);
        // Player.Move(scaledMovement);
        rigidbody.MovePosition(Player.gameObject.transform.position + scaledMovement);

        if (isInteractionButtonOn && (_gameManager.playTime-buttonOnStart)>0.2f)
        {
            buttonOnStart = 0.0f;
            HandleLoseFinger(buttonClickFinger);
        }
        //PlayerCollider.GetComponent<Grab>().GetFishCount = 0;
    }
    
    public void onPointerClickEventTrigger()
    {
        print("Button Clicked");
    }
    private void UpdatePlayerWalking()
    {
        ModelAnimator.SetBool("isWalking",MovementAmount.magnitude > 0);
    }
    
    /*private void OnGUI()
    {
        GUIStyle labelStyle = new GUIStyle()
        {
            fontSize = 40,
            normal = new GUIStyleState()
            {
                textColor = Color.white
            }
        };
        if (MovementFinger != null)
        {
            GUI.Label(new Rect(20, 45, 500, 20), $"Finger Start Position: {MovementFinger.currentTouch.startScreenPosition}", labelStyle);
            GUI.Label(new Rect(20, 80, 500, 20), $"Finger Current Position: {MovementFinger.currentTouch.screenPosition}", labelStyle);
        }
        else
        {
            GUI.Label(new Rect(20, 45, 500, 20), "No Current Movement Touch", labelStyle);
        }

        GUI.Label(new Rect(20, 10, 500, 20), $"Screen Size ({Screen.width}, {Screen.height})", labelStyle);
    }*/
}