using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class PlayerXPManager : MonoBehaviour
{
    public int CurrentXP = 0; // XP actuel du joueur
    public int CurrentLevel = 0; // Niveau actuel du joueur
    public int XPToNextLevel { get; private set; } = 100; // XP requis pour le prochain niveau
    public GameObject panelLvlUp; // Panel affich� lors de la mont�e de niveau
    public GameObject panelInvoc; // Panel affich� � certains niveaux
    public TextMeshProUGUI lvlText;
    [SerializeField] private float levelUpMultiplier = 1.2f; // Facteur multiplicatif pour l'XP requis � chaque niveau
    [SerializeField] private int baseXPToNextLevel = 100; // XP de base requis pour le premier niveau

    public Slider xpBar; // R�f�rence � la barre d'XP

    public delegate void OnLevelUp(int newLevel);
    public event OnLevelUp LevelUpEvent;

    private Queue<IEnumerator> levelUpQueue = new Queue<IEnumerator>(); // File pour g�rer les panneaux s�quentiellement
    private bool isProcessingLevelUp = false;

    private void Start()
    {
        ResetXPSystem();
        UpdateXPBar();
    }

    public void AddXP(int amount)
    {
        int realAmount = Mathf.RoundToInt(amount * (1 + PlayerStats.xpModifier));
        print(realAmount);
        CurrentXP += realAmount;
        CheckLevelUp();
        UpdateXPBar();
        lvlText.text = CurrentLevel.ToString();
    }

    private void CheckLevelUp()
    {
        while (CurrentXP >= XPToNextLevel)
        {
            CurrentXP -= XPToNextLevel;
            CurrentLevel++;
            XPToNextLevel = Mathf.RoundToInt(baseXPToNextLevel * Mathf.Pow(levelUpMultiplier, CurrentLevel - 1));

            // Ajouter l'affichage des panneaux dans la file
            levelUpQueue.Enqueue(HandleLevelUpPanel());

            // Ouvrir le panel d'invocation � des niveaux pairs
            if (CurrentLevel % 2 == 0)
            {
                levelUpQueue.Enqueue(HandleInvocPanel());
            }

            // D�clencher l'�v�nement de mont�e de niveau
            LevelUpEvent?.Invoke(CurrentLevel);
            Debug.Log($"Level Up! New Level: {CurrentLevel}, XP for Next Level: {XPToNextLevel}");
        }

        // Si aucune autre coroutine n'est en cours, commencer � traiter la file
        if (!isProcessingLevelUp)
        {
            StartCoroutine(ProcessLevelUpQueue());
        }
    }

    private IEnumerator HandleLevelUpPanel()
    {
        panelLvlUp.SetActive(true);
        yield return new WaitForSeconds(0.1f); // Temps d'affichage du panneau
        panelLvlUp.SetActive(false);
    }

    private IEnumerator HandleInvocPanel()
    {
        panelInvoc.SetActive(true);
        yield return new WaitForSeconds(0.1f); // Temps d'affichage du panneau
        panelInvoc.SetActive(false);
    }

    private IEnumerator ProcessLevelUpQueue()
    {
        isProcessingLevelUp = true;

        while (levelUpQueue.Count > 0)
        {
            IEnumerator currentAction = levelUpQueue.Dequeue();
            yield return StartCoroutine(currentAction);
        }

        isProcessingLevelUp = false;
    }

    private void UpdateXPBar()
    {
        if (xpBar != null)
        {
            xpBar.value = (float)CurrentXP / XPToNextLevel;
        }
    }

    public void ResetXPSystem()
    {
        CurrentXP = 0;
        CurrentLevel = 0;
        XPToNextLevel = baseXPToNextLevel;
        UpdateXPBar();
    }
}
