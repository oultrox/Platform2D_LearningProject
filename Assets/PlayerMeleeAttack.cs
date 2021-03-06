﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour {

    [SerializeField] private float timeBetweenAttacks;
    private GameObject slashObject;
    private Transform slashChildren;
    private Animator animator;
    private SpriteRenderer playerOrientation;
    private PlatformMotor2D playerController;
    private float timeCounter = 0;
    

    //--------Métodos API--------
    private void Awake()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Slash")
            {
                slashObject = child.gameObject;
            }
        }
        animator = slashObject.GetComponent<Animator>();
        slashChildren = slashObject.transform;
        playerOrientation = this.GetComponentInParent<SpriteRenderer>();
        playerController = this.GetComponent<PlatformMotor2D>();
    }

    private void Start()
    {
        slashObject.SetActive(false);
    }

    private void Update()
    {
        timeCounter += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (timeCounter >= timeBetweenAttacks && !playerController.collisionInfo.isStickedToWall)
            {
                timeCounter = 0;
                StartCoroutine(Slash());
            }
        }
    }

    private float debuggerTime;
    IEnumerator Slash()
    {
        slashObject.SetActive(true);

        if (playerOrientation.flipX)
        {
            slashChildren.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            slashChildren.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        debuggerTime = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(debuggerTime);
        slashObject.SetActive(false);
    }
}
