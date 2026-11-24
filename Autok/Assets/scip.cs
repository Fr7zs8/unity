using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializedField] float sebesseg;
    [SerializedField] float fekezesiEro;
    [SerializedField] float maxFordSzog;

    [SerializedField] WheelCollider[] kerekUtkozo;
    [SerializedField] Transform[] kerekHalo;

    [SerializedField] float jelenlegiSebesseg = 0f;
    [SerializedField] float jelenlegiFekezesiEro = 0f;
    [SerializedField] float jelenlegiFordSzog = 0f;


    void Start()
    {
        sebesseg = 1000f;
        fekezesiEro = 1000f;
        maxFordSzog = 40f;

        for(int i= 0; i < kerekUtkozo.Length; i++){
            kerekUtkozo[i].transform.position = kerekHalo[i].position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetAxis("Vertical") != 0){
            jelenlegiSebesseg = sebesseg * Input.GetAxis("Vertical");
        }
        else{
            jelenlegiSebesseg = 0f;
        }

        if(Input.GetAxis("Horizontal") != 0){
            jelenlegiFordSzog = maxFordSzog * Input.GetAxis("Horizontal");
        }
        else{
            jelenlegiFordSzog = 0f;
        }

        kerekUtkozo[0].steerAngle = jelenlegiFordSzog;
        kerekUtkozo[1].steerAngle = jelenlegiFordSzog;
        kerekUtkozo[2].steerAngle = -jelenlegiFordSzog / 10f;
        kerekUtkozo[3].steerAngle = -jelenlegiFordSzog / 10f;

        if(Input.GetKey(KeyCode.Space)){
            jelenlegiFekezesiEro = fekezesiEro;
        }
        else{
            jelenlegiFekezesiEro = 0f;
        }

        kerekUtkozo[0].motorTorque = jelenlegiSebesseg;
        kerekUtkozo[1].motorTorque = jelenlegiSebesseg;

        for(int i = 0; i < kerekUtkozo.Length; i++){
            kerekUtkozo[i].brakeTorque = jelenlegiFekezesiEro;
        }

        for(int i = 0; i < kerekHalo.Length; i++){
            Vector3 kerekPozicio;
            Quaterion kerekForgas;
            kerekUtkozo[i].GetWorldPose(out kerekPozicio, out kerekForgas);

            kerekHalo[i].position = kerekPozicio;
            kerekHalo[i].rotation = kerekForgas;
        }

    }
}
