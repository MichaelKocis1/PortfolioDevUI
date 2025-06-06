
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed;
    public float groundDrag;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 movementDirection;

    Rigidbody rb;

    private PlayerInputActions inputActions;

    [Header("Interactable")]
    public LayerMask Interactable;
    bool lookingAtInteractable;
    bool interactableActive;
    public GameObject interactableIcon;

    [Header("Pause")]
    public GameObject pauseMenu;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        rb.drag = groundDrag;

        interactableIcon.SetActive(false);
    }

    private void Update() {
        lookingAtInteractable = Physics.Raycast(transform.position, orientation.forward, 10f, Interactable);

        MyInput();
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void MyInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer() {
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(movementDirection.normalized * movementSpeed * 10f, ForceMode.Force);
    }

    private void Awake() {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable() {
        inputActions.Player.Enable();

        inputActions.Player.Interact.performed += _ => Interact();
        inputActions.Player.Pause.performed += _ => PauseGame();
    }

    private void OnDisable() {
        inputActions.Player.Disable();
    }

    private void Interact() {
        if (lookingAtInteractable && !interactableActive) {
            interactableIcon.SetActive(true);
            interactableActive = true;
            StartCoroutine(HideInteractableAfterDelay());
        }
    }

    private IEnumerator HideInteractableAfterDelay() {
        yield return new WaitForSeconds(3f);
        interactableIcon.SetActive(false);
        interactableActive = false;
    }

    private void PauseGame() {
        if (pauseMenu.activeSelf) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        } else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        
    }
}
