using TMPro;
using UnityEngine;

public class ShowUp : MonoBehaviour
{
    [SerializeField]
    private TMP_Text txtShowUPScore;
    public string showUpScore;

    // Start is called before the first frame update
    void Start()
    {
        txtShowUPScore.text = "+" + showUpScore;
        Destroy(gameObject, 1f);
    }
}
