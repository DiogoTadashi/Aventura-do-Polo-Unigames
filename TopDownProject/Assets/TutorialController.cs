using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    public GameObject movementTutorial;
    public GameObject interactionTutorial;
    public GameObject combatTutorial;

    private Animator[] movementAnimators;
    private Animator[] interactionAnimators;
    private Animator[] combatAnimators;

    private bool hasInteracted = false;
    private bool hasCombatTutorialShown = false;
    private bool isGamePaused = false;

    void Start()
    {
        // Encontrando todos os Animators dentro dos GameObjects principais
        movementAnimators = movementTutorial.GetComponentsInChildren<Animator>();
        interactionAnimators = interactionTutorial.GetComponentsInChildren<Animator>();
        combatAnimators = combatTutorial.GetComponentsInChildren<Animator>();

        if (movementAnimators.Length == 0)
            Debug.LogError("❌ Nenhum Animator encontrado no MovementTutorial!");
        if (interactionAnimators.Length == 0)
            Debug.LogError("❌ Nenhum Animator encontrado no InteractionTutorial!");
        if (combatAnimators.Length == 0)
            Debug.LogError("❌ Nenhum Animator encontrado no CombatTutorial!");

        movementTutorial.SetActive(true);
        PlayAnimations(movementAnimators);

        interactionTutorial.SetActive(false);
        combatTutorial.SetActive(false);

        PauseGame();
    }

    public void ShowInteractionTutorial()
    {
        if (!hasInteracted)
        {
            interactionTutorial.SetActive(true);
            PlayAnimations(interactionAnimators);
            hasInteracted = true;
            PauseGame();
        }
    }

    public void ShowCombatTutorial()
    {
        if (!hasCombatTutorialShown)
        {
            combatTutorial.SetActive(true);
            PlayAnimations(combatAnimators);
            hasCombatTutorialShown = true;
            PauseGame();
        }
    }

    private void PlayAnimations(Animator[] animators)
    {
        foreach (Animator animator in animators)
        {
            if (animator != null)
            {
                animator.SetTrigger("ShowTutorial");
            }
            else
            {
                Debug.LogError("❌ Algum Animator não foi encontrado! Certifique-se de que todas as imagens têm um Animator.");
            }
        }
    }

    public void PauseGame()
    {
        if (!isGamePaused)
        {
            Time.timeScale = 0f;
            isGamePaused = true;
        }
    }

    public void CloseTutorial(GameObject tutorialPopup)
    {
        tutorialPopup.SetActive(false);

        if (!movementTutorial.activeSelf &&
            !interactionTutorial.activeSelf &&
            !combatTutorial.activeSelf)
        {
            Time.timeScale = 1f;
            isGamePaused = false;
        }
    }
}