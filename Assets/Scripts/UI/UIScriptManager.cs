using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScriptManager : MonoBehaviour
{

    [SerializeField]private GameObject welcomeMenu;
    [SerializeField] private GameObject instructionMenu;
    [SerializeField] private GameObject gameScene;
    [SerializeField] private GameObject rightPointer;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject slapperIntro;
    void OnEnable()
    {
        StartCoroutine(DestroyWelcomeMenu());
        instructionMenu.SetActive(false);
        gameScene.SetActive(false);
        leftHand.SetActive(false);
        rightHand.SetActive(false);
    } 

    public void CloseWelcomeMenu()
    {
        //Debug.Log($"<b> I NEED THIS TO WORK </b>");
        welcomeMenu.SetActive(false);
        instructionMenu.SetActive(true);
        rightPointer.SetActive(true);
    }
    public void StartGame()
    {
        instructionMenu.SetActive(false);
        gameScene.SetActive(true);
        slapperIntro.SetActive(false);
        leftHand.SetActive(true);
        rightHand.SetActive(true);
        rightPointer.SetActive(false );
    }
    IEnumerator DestroyWelcomeMenu()
    {   //First enable welcome
        yield return new WaitForSeconds(5f);
        //Disable welcome and enable instruction
        //welcomeMenu.SetActive(false);
        //rightPointer.SetActive(true);
        //instructionMenu.SetActive(true);
        //Another return to destroy the Canvas or disable the cnavas 
        //Then enable canvas to display final score after the boss is defeated.
    }

    //
}
