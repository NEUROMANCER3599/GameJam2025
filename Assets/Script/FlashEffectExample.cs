using UnityEngine;

public class FlashEffectExample : MonoBehaviour
{
    [SerializeField] private FlashEffect flashEffect;
    [SerializeField] private KeyCode flashKey; // Debug

    // Update is called once per frame
    void Update()
    {
        // Do FLash here
        if (Input.GetKeyDown(flashKey))
        {
            flashEffect.Flash();
        }
    }
}
