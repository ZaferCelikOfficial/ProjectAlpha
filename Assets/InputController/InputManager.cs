using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion

    private InputController PlayerInputController;
    private Camera MainCamera;
    private void Awake()
    {
        PlayerInputController = new InputController();
        MainCamera = Camera.main;
    }
    private void OnEnable()
    {
        PlayerInputController.Enable();
    }
    private void OnDisable()
    {
        PlayerInputController.Disable();
    }
    void Start()
    {
        PlayerInputController.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        PlayerInputController.Touch.PrimaryContact.canceled+= ctx => EndTouchPrimary(ctx);
        
    }
    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(MainCamera,PlayerInputController.Touch.PrimaryPosition.ReadValue<Vector2>()),(float)context.startTime);
    }
    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(MainCamera, PlayerInputController.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
    }
    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(MainCamera, PlayerInputController.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}
