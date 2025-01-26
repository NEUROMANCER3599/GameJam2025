using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float TextureUnitSizeX;
    private float TextureUnitSizeY;
    [SerializeField] private Vector2 ParallaxEffect;
    [SerializeField] private bool InfiniteHorizontal;
    [SerializeField] private bool InfiniteVertical;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        TextureUnitSizeX = (texture.width / sprite.pixelsPerUnit) *  transform.localScale.x;
        TextureUnitSizeY = (texture.height / sprite.pixelsPerUnit) * transform.localScale.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * ParallaxEffect.x, deltaMovement.y * ParallaxEffect.y);
        lastCameraPosition = cameraTransform.position;

        if (InfiniteHorizontal)
        {
            if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= TextureUnitSizeX)
            {
                float offsetPositionX = (cameraTransform.position.x - transform.position.x) % TextureUnitSizeX;
                transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
            }
        }

        if (InfiniteVertical)
        {
            if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= TextureUnitSizeY)
            {
                float offsetPositionY = (cameraTransform.position.y - transform.position.y) % TextureUnitSizeY;
                transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offsetPositionY);
            }
        }
       
    }
}
