using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [Header("Réglages")]
    public Light targetLight; // Glisse ta lumière ici

    [Space]
    [Tooltip("Intensité minimum (lumière presque éteinte)")]
    public float minIntensity = 0f;

    [Tooltip("Intensité maximum (lumière allumée)")]
    public float maxIntensity = 2f;

    [Tooltip("Vitesse du clignotement (plus c'est bas, plus c'est nerveux)")]
    [Range(0.01f, 0.5f)]
    public float flickerSpeed = 0.05f;

    [Tooltip("Lissage pour éviter l'effet stroboscope (0 = dur, 10 = doux)")]
    [Range(0, 20)]
    public int smoothing = 5;

    Queue<float> smoothQueue;
    float lastSum = 0;

    void Start()
    {
        // Si tu as oublié de mettre la light, il la cherche tout seul
        if (targetLight == null) targetLight = GetComponent<Light>();

        smoothQueue = new Queue<float>(smoothing);
    }

    void Update()
    {
        if (targetLight == null) return;

        // On nettoie la file d'attente si elle est pleine
        while (smoothQueue.Count >= smoothing)
        {
            lastSum -= smoothQueue.Dequeue();
        }

        // 1. On génère une nouvelle intensité cible aléatoire
        float newVal = Random.Range(minIntensity, maxIntensity);

        // 2. On l'ajoute à la liste pour faire la moyenne
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        // 3. On applique la moyenne (lissage)
        targetLight.intensity = lastSum / (float)smoothQueue.Count;
    }
}