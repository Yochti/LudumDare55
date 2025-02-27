using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthManagment : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 3;

    private void Start()
    {
        //Cette partie se lance au debut et fais en sorte que tu commences avec ton nombre maximum de vie
        currentHealth = maxHealth;
    }
    private void Update()
    {
        //Ca se joue toutes les frames cette partie, ca controle le nombre de vie et si tu en as 0 tu es mort.
        if(currentHealth <= 0)
        {
            print("tu es mort");
            // affiche le panneau de gameOver quand il sera fait
        }
        //Si ton nombre de vie actuel depasse ton nombre de vie max ca remet ton nb de vie actuel au nb de vie max
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // se joue quand un gameObject avec le tag enemy rentre dans la zone et ca appel la fonction takedamage qui va enlever 1 vie
        if(collision.CompareTag("Enemy"))
        {
            takeDamage(1);
        }
    }
    //enleve un certain nombre de vie en fonnction de quel nombre tu mets dans la paranthese quand tu appeles la fonction.
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
    }
    //ajoute un certain nombre de vie en fonnction de quel nombre tu mets dans la paranthese quand tu appeles la fonction. Tu me l'as pas vrmt demandé mais si jamais tu veux un truc pour heal c'est deja en place

    public void heal(int healAmount)
    {
        currentHealth += healAmount;
    }
}
