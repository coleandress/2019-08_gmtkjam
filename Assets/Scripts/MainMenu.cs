using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _byline;
    [SerializeField] private GameObject _instructions;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            if (_menu.activeInHierarchy)
            {
                _menu.SetActive(false);
                _byline.SetActive(false);

                _instructions.SetActive(true);
            }
            else
            {
                _menu.SetActive(true);
                _byline.SetActive(true);

                _instructions.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && _menu.activeInHierarchy)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
