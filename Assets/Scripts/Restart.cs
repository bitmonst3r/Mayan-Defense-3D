using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public int level = 0;
    public Button restartButton;
    public GameObject restart;
    // Start is called before the first frame update
    void Start()
    {
        restart = GameObject.Find("Restart Manager");
    }

    // Update is called once per frame
    void Update()
    {
        Button button = restartButton.GetComponent<Button>();
        button.onClick.AddListener(RestartButton_onClick2);
    }

    public void RestartButton_onClick2()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
