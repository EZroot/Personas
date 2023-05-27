using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonaAnimation : MonoBehaviour
{
    [Header("Animation")]
    public Animator anim;
    public Transform head;
    public Transform[] _bodyPartsToFlipUnsortedLayer;
    public SpriteRenderer[] _leftArmBodyPartsSpriteRenderer;
    public SpriteRenderer[] _leftLegBodyPartsSpriteRenderer;
    public SpriteRenderer[] _rightArmBodyPartsSpriteRenderer;
    public SpriteRenderer[] _rightLegBodyPartsSpriteRenderer;

    private int _onTopArmSortingLayer = 18;
    private int _onBottomArmSortingLayer = 8;

    private int _onTopLegSortingLayer = 1;
    private int _onBottomLegSortingLayer = 0;

    private void FixedUpdate()
    {
        Animation();
    }

    private void Animation()
    {
        float xDir = Input.GetAxisRaw("Horizontal");

        // animation

        if (xDir != 0)
        {
            head.localScale = new Vector3(xDir, 1f, 1f);
            for (var i = 0; i < _bodyPartsToFlipUnsortedLayer.Length; i++)
            {
                _bodyPartsToFlipUnsortedLayer[i].localScale = new Vector3(xDir, 1f, 1f);
            }
            //left body
            for (var i = 0; i < _leftArmBodyPartsSpriteRenderer.Length; i++)
            {
                _leftArmBodyPartsSpriteRenderer[i].transform.localScale = new Vector3(xDir, 1f, 1f);
                _leftArmBodyPartsSpriteRenderer[i].sortingOrder = xDir == 1 ? _onTopArmSortingLayer - i : _onBottomArmSortingLayer - i;
            }
            //left body
            for (var i = 0; i < _leftLegBodyPartsSpriteRenderer.Length; i++)
            {
                _leftLegBodyPartsSpriteRenderer[i].transform.localScale = new Vector3(xDir, 1f, 1f);
                _leftLegBodyPartsSpriteRenderer[i].sortingOrder = xDir == 1 ? _onTopLegSortingLayer - i : _onBottomLegSortingLayer - i;
            }
            //right body
            for (var i = 0; i < _rightArmBodyPartsSpriteRenderer.Length; i++)
            {
                _rightArmBodyPartsSpriteRenderer[i].transform.localScale = new Vector3(xDir, 1f, 1f);
                _rightArmBodyPartsSpriteRenderer[i].sortingOrder = xDir == 1 ? _onBottomArmSortingLayer - i : _onTopArmSortingLayer - i;
            }
            //right body
            for (var i = 0; i < _rightLegBodyPartsSpriteRenderer.Length; i++)
            {
                _rightLegBodyPartsSpriteRenderer[i].transform.localScale = new Vector3(xDir, 1f, 1f);
                _rightLegBodyPartsSpriteRenderer[i].sortingOrder = xDir == 1 ? _onBottomLegSortingLayer - i : _onTopLegSortingLayer - i;
            }
        }
        if (xDir > 0)
        {
            anim.SetBool("IsWalkingRight", true);
            anim.SetBool("IsWalkingLeft", false);
        }
        if (xDir < 0)
        {
            anim.SetBool("IsWalkingRight", false);
            anim.SetBool("IsWalkingLeft", true);
        }
        if (xDir == 0)
        {
            anim.SetBool("IsWalkingRight", false);
            anim.SetBool("IsWalkingLeft", false);
        }
    }
}