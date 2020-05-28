using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPermanente : MonoBehaviour
{
    public int cherries = 0;
    public int health = 5;
    [SerializeField]public TextMeshProUGUI cherryText;
    [SerializeField]public Text healthAmount;

    public static UIPermanente perm;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (!perm)
        {
            perm = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Reset()
    {
        health = 5;
        healthAmount.text = health.ToString();
        cherries = 0;
        cherryText.text = cherries.ToString();
    }
}
