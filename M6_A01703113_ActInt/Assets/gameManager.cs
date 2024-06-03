using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bulletCounterText;

    // Tags para las balas del jugador y del Boss
    [SerializeField] private string playerBulletTag = "Proyectil";

    // Update is called once per frame
    void Update()
    {
        // Obtener todas las balas del jugador y del Boss
        GameObject[] bossBullets = GameObject.FindGameObjectsWithTag(playerBulletTag);

        // Contar el total de balas
        int totalBullets = bossBullets.Length;

        // Actualizar el texto en la UI
        bulletCounterText.text = "Balas: " + totalBullets;
    }
}
