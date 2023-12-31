﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;

public class DialogueTrigger : MonoBehaviour
{
    private VillagerDialogueManager ui;
    private PlayerInputManager _inputManager;
    private VillagerScript currentVillager;
    //private MovementInput movement;
    public CinemachineTargetGroup targetGroup;

    [Space]

    [Header("Post Processing")]
    public Volume dialogueDof;

    void Start()
    {
        ui = VillagerDialogueManager.instance;
        _inputManager = PlayerInputManager.Instance;
        //movement = GetComponent<MovementInput>();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && !ui.inDialogue && currentVillager != null)
        if (_inputManager.IsTalkPressed && !ui.inDialogue && currentVillager != null)
        {
            ui.playerStateMachine.ConversationStart();
            targetGroup.m_Targets[1].target = currentVillager.transform;
            //movement.active = false;
            ui.SetCharNameAndColor();
            ui.inDialogue = true;
            ui.CameraChange(true);
            ui.ClearText();
            ui.FadeUI(true, .2f, .65f);
            currentVillager.TurnToPlayer(transform.position);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Villager"))
        {
            currentVillager = other.GetComponent<VillagerScript>();
            ui.currentVillager = currentVillager;
            ui.playerStateMachine = GetComponent<PlayerStateMachine>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Villager"))
        {
            currentVillager = null;
            ui.currentVillager = currentVillager;
            ui.playerStateMachine = null;
        }
    }

}
