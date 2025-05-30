using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManagerPrimarCity : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    public int sceneToLoad = 5;
    public Animator animator;

    private Queue<string> sentences;

    void Start ()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(DialogueCity dialogue)
    {

        animator.SetBool("isOpen", true);
        nameText.text = dialogue.name;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    void Update()
    {
        if (animator.GetBool("isOpen") && Input.GetKeyDown(KeyCode.E))
        {
            DisplayNextSentence();
        }
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);

        //if (currentStep == 3)
        //{
        //    StartCoroutine(StartRewindForSeconds(7f));
        //}
    }
    IEnumerator StartRewindForSeconds(float seconds)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Rewind rewind = player.GetComponent<Rewind>();
            if (rewind != null)
            {
                rewind.StartRewind();
                yield return new WaitForSeconds(seconds);
                rewind.StopRewind();
                SceneManager.LoadScene(2);

            }
        }
    }


}
