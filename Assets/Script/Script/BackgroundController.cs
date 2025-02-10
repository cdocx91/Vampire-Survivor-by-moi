using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public Transform cameraTransform; // R�f�rence � la cam�ra (suivant le joueur)
    public float parallaxEffectMultiplier = 0.5f; // Effet de parallaxe
    private Vector3 lastCameraPosition;

    private float textureUnitSizeX;
    private float textureUnitSizeY;

    void Start()
    {
        lastCameraPosition = cameraTransform.position;

        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;

        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier, deltaMovement.y * parallaxEffectMultiplier);
        lastCameraPosition = cameraTransform.position;

        // V�rifie si la cam�ra d�passe la moiti� du sprite pour le reg�n�rer
        if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x + offsetX, transform.position.y);
        }

        if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeY)
        {
            float offsetY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
            transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offsetY);
        }
    }
}
