using UnityEngine;

public class TutorialTrigger : MonoBehaviour {
    public GameObject tutorialPopup;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            tutorialPopup.SetActive(true);
        }
    }

    public void CloseTutorial() {
        tutorialPopup.SetActive(false);
    }
}
