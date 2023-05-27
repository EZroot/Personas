using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonaAI : MonoBehaviour
{
    [SerializeField] private PersonaMovement _personaMovement;
    [SerializeField] private Balance[] _balancers;
    [SerializeField] private bool _knockedOut = false;

    private bool _jumpInput = false;

    bool _lowerForce = false;
    bool _normalizeForce = false;
    void FixedUpdate()
    {
        var xDir = Input.GetAxisRaw("Horizontal");
        _personaMovement.SetMovement(xDir);
        if (_jumpInput)
        {
            _personaMovement.Jump(_jumpInput);
            _jumpInput = false;
        }

    //      // Check if the application is running on a mobile platform
    // if (Application.isMobilePlatform)
    // {
    //     // Get the current device acceleration
    //     Vector3 acceleration = Input.acceleration;

    //     // Create a Vector2 direction using the X and Y components of the acceleration
    //     Vector2 tiltDirection = new Vector2(acceleration.x, acceleration.y);

    //     // Use the magnitude of the tiltDirection as the tilt value
    //     float tiltMagnitude = tiltDirection.magnitude;

    //     // Do something with the tiltDirection and tiltMagnitude
    //     // For example, you can normalize the tiltDirection for use in movement or control
    //     tiltDirection.Normalize();
    //     Fling(tiltDirection);
    //     // Use the tiltDirection and tiltMagnitude as needed
    //     // For example, you can move an object based on the device tilt
    //     // Vector3 movement = new Vector3(tiltDirection.x, 0f, tiltDirection.y) * tiltMagnitude;
    //     // transform.Translate(movement);
    // }
    }

    bool kock = false;
    void Update()
    {
        _jumpInput = Input.GetKey(KeyCode.Space) || Input.GetKeyDown(KeyCode.W);

        LowerForce(Input.GetKeyDown(KeyCode.J));

        if (Input.GetKeyDown(KeyCode.K))
        {
            kock = !kock;
            KnockoutPersona(kock);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            //kock = !kock;
            Fling(Vector2.up * 500);
        }
    }

    public void Fling(Vector2 dir)
    {
        foreach (var balancer in _balancers)
        {
            balancer.AddImpulseForce(dir);
        }
    }

    public void KnockoutPersona(bool knockout)
    {
        foreach (var balancer in _balancers)
        {
            balancer.SetConciousness(knockout);
        }
    }

    float maxTimer = 0;
    public void LowerForce(bool lowerIt)
    {
        if (lowerIt == false)
            return;
        foreach (var balancer in _balancers)
        {
            balancer.force = 1f;
        }
    }
}