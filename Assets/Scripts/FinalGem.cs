using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalGem : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
