using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIRacket : MonoBehaviour
{
    public Vector3 startPosition;
    [SerializeField] private float floatSpeed;
    [SerializeField] private float floatAmplitude;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Float();
    }

    private void Float() {
        // Calcula o movimento vertical usando uma função senoidal para suavidade.
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;

        // Aplica o movimento ao eixo Y.
        transform.position = startPosition + new Vector3(0, yOffset, 0);
    }
}
