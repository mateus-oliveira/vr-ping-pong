using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string mode;
    [SerializeField] private GameObject ui;

    // service practice mode
    [SerializeField] private GameObject balls;
    [SerializeField] private GameObject bouncingWall;

    // reflexes practice mode
    [SerializeField] private GameObject ballsShooter;
    // Start is called before the first frame update
    void Start()
    {
        mode = "menu";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SetMode("menu");
        }
    }

    public void SetMode(string mode) {
        mode = mode.ToLower();
        ui.SetActive(mode == "menu");

        if (mode == "service") {
            balls.SetActive(true);
            bouncingWall.SetActive(true);

            ballsShooter.SetActive(false);
        } else if (mode == "reflexes") {
            balls.SetActive(false);
            bouncingWall.SetActive(false);

            ballsShooter.SetActive(true);
        }
    }
}
