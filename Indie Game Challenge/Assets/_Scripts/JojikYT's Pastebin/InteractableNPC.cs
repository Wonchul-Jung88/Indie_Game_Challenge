using UnityEngine;

namespace JojikYT//https://pastebin.com/u/JojikYT/1/4YYkmKJk
{
    public class InteractableNPC : Interactable
    {
        private Animator animator;

        public override void Start()
        {
            base.Start();
            animator = GetComponent<Animator>();
        }
        protected override void Interaction()
        {
            base.Interaction();
            print("Hello! Unfortunately I don't have a dialog system yet.");
            animator.SetTrigger("Wave");

            //Start Dialogue System
        }
    }
}