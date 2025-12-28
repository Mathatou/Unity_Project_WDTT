using UnityEngine;

public class BloodyFeetManager : MonoBehaviour
{
    [Header("Configuration des traces")]
    [Tooltip("Prefab du Decal Projector pied gauche")]
    public GameObject leftFootPrefab;
    [Tooltip("Prefab du Decal Projector pied droit")]
    public GameObject rightFootPrefab;

    [Tooltip("Distance à parcourir pour déposer une nouvelle trace")]
    public float stepDistance = 0.8f;
    [Tooltip("Combien de pas le joueur laisse-t-il après avoir marché dans le sang")]
    public int maxBloodySteps = 10;
    [Tooltip("Durée de vie d'une trace au sol avant disparition (0 = infini)")]
    public float footprintLifetime = 10f;

    [Header("État actuel (Lecture seule)")]
    public int currentStepsRemaining = 0;
    private Vector3 lastStepPosition;
    private bool isRightFootTurn = true;

    void Start()
    {
        // Initialise la dernière position connue
        lastStepPosition = transform.position;
    }

    void Update()
    {
        // Si on n'a plus de sang sous les pieds, on ne fait rien
        if (currentStepsRemaining <= 0) return;

        // Calcule la distance parcourue depuis la dernière trace
        float distanceMoved = Vector3.Distance(transform.position, lastStepPosition);

        if (distanceMoved >= stepDistance)
        {
            SpawnFootprint();
            // Met à jour la position de référence pour le prochain pas
            lastStepPosition = transform.position;
        }
    }

    // --- Fonction appelée par la flaque de sang ---
    public void RefillBlood()
    {
        currentStepsRemaining = maxBloodySteps;
        // Optionnel : réinitialiser pour commencer toujours du même pied
        // isRightFootTurn = true; 
    }

    // --- Logique de création de la trace ---
    void SpawnFootprint()
    {
        // 1. Trouver le sol sous le joueur avec un Raycast
        RaycastHit hit;
        // On lance un rayon depuis le centre du joueur, vers le bas
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out hit, 2f))
        {
            // 2. Choisir le bon pied
            GameObject prefabToSpawn = isRightFootTurn ? rightFootPrefab : leftFootPrefab;
            
            // Permet de combattre le "Z-fighting"
            Vector3 SpawnPosition = hit.point + (hit.normal *0.02f);

            // 3. Créer la trace au point d'impact
            GameObject newDecal = Instantiate(prefabToSpawn, SpawnPosition, Quaternion.identity);
            // 4. Orienter la trace pour qu'elle suive la direction du joueur ET la pente du sol
            // On prend la rotation du joueur (axe Y uniquement)
            Quaternion playerRotation = Quaternion.Euler(90, transform.eulerAngles.y, 0);
            // On l'aligne avec la "normale" (l'inclinaison) du sol touché
            newDecal.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * playerRotation;

            // 5. Gérer la durée de vie
            if (footprintLifetime > 0)
            {
                Destroy(newDecal, footprintLifetime);
            }

            // 6. Préparer le prochain pas
            currentStepsRemaining--;
            isRightFootTurn = !isRightFootTurn; // Inverse le pied
        }
    }

    // --- Détection de la flaque ---
    // Cette fonction est appelée automatiquement quand le Character Controller entre dans un Trigger
    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet touché est bien une flaque de sang
        // (Assurez-vous d'avoir mis le Tag "BloodPuddle" sur votre flaque, ou vérifiez le nom)
        if (other.CompareTag("BloodPuddle"))
        {
            RefillBlood();
            Debug.Log("Pieds ensanglantés !");
        }
    }
}