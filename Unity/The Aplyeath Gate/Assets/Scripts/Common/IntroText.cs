using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroText : MonoBehaviour
{

	public string[] dialogue;
	public string dialogueTarget;
	public string curDialogue;
	public int curLetter = 0;
	public int curText = 0;
	Text txt;
	public bool isSlow = true;
	public GameObject yesNo;
	public bool ending = false;
	public GameObject talkerParent;

	void Start ()
	{
		txt = transform.GetComponent<Text> ();
	}

	void Update ()
	{
		if (isSlow == true) {
			dialogueTarget = dialogue [curText];
			txt.text = curDialogue;
			if (curDialogue.Length != dialogueTarget.Length) {
				curDialogue += dialogueTarget [curLetter];
				curLetter += 1;
			}
			if (Input.GetButtonDown ("Fire1")) {
				if (curText != dialogue.Length - 1) {
					if (curDialogue.Length == dialogueTarget.Length) {
						txt.text = "";
						curDialogue = "";
						curLetter = 0;
						curText += 1;
					}
				} else {
					Application.Quit ();
				}
			}

			if (curDialogue.Length != dialogueTarget.Length) {
				if (Input.GetButtonDown ("Fire1")) {
					if (curLetter != 0) {
						curLetter = dialogueTarget.Length;
						curDialogue = dialogueTarget;
						txt.text = dialogueTarget;
					}
				}
			}
		} else {
			txt.text = dialogue [curText];
			if (Input.GetButtonDown ("Fire1")) {
				if (curText != dialogue.Length - 1) {
					if (ending == true) {
						if (talkerParent.GetComponent<IntroText> ().curDialogue.Length != talkerParent.GetComponent<IntroText> ().dialogueTarget.Length) {
							curText += 1;
						}
					} else if (talkerParent.GetComponent<IntroText> ().curDialogue.Length == talkerParent.GetComponent<IntroText> ().dialogueTarget.Length) {
						curText += 1;
					}
				}
			}
		}
	}
}
