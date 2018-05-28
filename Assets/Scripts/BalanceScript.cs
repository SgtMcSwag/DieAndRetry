using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceScript : MonoBehaviour
{
    private HingeJoint2D hinge;
    private JointMotor2D motor;
    // Use this for initialization
    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        motor = hinge.motor;
        motor.motorSpeed = -180f;
        hinge.motor = motor;

    }

    private float nextActionTime = 2.0f;
    public float period = 0.08f;

    void Update()
    {
        if (Time.time > nextActionTime)
        {
            Debug.Log(Time.time);
            nextActionTime ++;
            if(nextActionTime % 2 == 0)
            {
                motor.motorSpeed = -200;
                hinge.motor = motor;
            }
            else
            {
                motor.motorSpeed = 200;
                hinge.motor = motor;
            }
        }
    }
}
