using UnityEngine;
using System.Collections;

public class Winning : MonoBehaviour
{
    public GameController GameController;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        GameController.Win();
    }
}