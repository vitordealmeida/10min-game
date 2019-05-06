using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float angularSpeed;

    // Start is called before the first frame update
    void Start()
    {
        angularSpeed = Random.Range(0, 100f);

        transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, angularSpeed * Time.deltaTime);
    }
}
