using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    public float restingAngle = 0f;
    public float force = 750f;
    private float _prevForce;
    private Rigidbody2D rb;

    bool _knockedOut = false;

    [SerializeField] PersonaAI _personaAI;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //_personaAI = gameObject.transform.parent.GetComponent<PersonaAI>();
        if (_personaAI == null)
            Debug.Log("COULD NOT FIND PERSONA AI");
    }

    private void FixedUpdate()
    {
        if (!_knockedOut)
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, restingAngle, force * Time.deltaTime));
    }

    public void SetConciousness(bool knockedOut)
    {
        _knockedOut = knockedOut;
    }

    public void AddImpulseForce(Vector2 dir)
    {
        rb.AddForce(dir, ForceMode2D.Impulse);
    }

    public void SetBalanceForce(float force)
    {
        this.force = force;
    }
}