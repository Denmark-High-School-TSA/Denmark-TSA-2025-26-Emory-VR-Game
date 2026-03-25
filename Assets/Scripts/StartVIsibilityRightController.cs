using UnityEngine;
using UnityEngine.InputSystem;

public class StartVisibility : MonoBehaviour
{
    [SerializeField] private GameObject[] Menu;
    public GameObject ResetPosMenu;
    public GameObject QuitMenu;
    public GameObject joke;
    public GameObject controller;
    public GameObject Paddle;
    public GameObject ray;
    public InputActionReference LeftMenuPress;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ray.SetActive(true);
        controller.SetActive(true);
        Menu[0].SetActive(true);
        Menu[1].SetActive(true);
        ResetPosMenu.SetActive(false);
        QuitMenu.SetActive(false);
        joke.SetActive(false);
        Paddle.SetActive(false);
    }

    private void Awake()
    {
        LeftMenuPress.action.Enable();
        LeftMenuPress.action.performed += ToggleMenu;
    }
    private void OnDestroy()
    {
        LeftMenuPress.action.Disable();
        LeftMenuPress.action.performed -= ToggleMenu;
    }
    void ToggleMenu(InputAction.CallbackContext context)
    {
        ray.SetActive(!ray.activeSelf);
        Menu[0].SetActive(!Menu[0].activeSelf);
        Menu[1].SetActive(!Menu[1].activeSelf);
        controller.SetActive(!controller.activeSelf);
        Paddle.SetActive(!Paddle.activeSelf);
    }
}
