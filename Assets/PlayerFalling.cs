using Unity.Cinemachine;
using UnityEngine;

public class PlayerFalling : MonoBehaviour
{
    [Header("Parameter")]
    private Rigidbody2D rb;
    public float velocityThreshold = -30f;
    public float duration = 3f;
    
    private float elapsedTime = 0f; // Time counter for velocity below the threshold
    [SerializeField] private Camera mainCamera;
    private CinemachineBrain cinemachineBrain;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cinemachineBrain = mainCamera.GetComponent<CinemachineBrain>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // ตรวจสอบว่าความเร็วในแกน Y เกินค่าที่กำหนด
        if (rb.velocity.y < velocityThreshold)
        {
            elapsedTime += Time.deltaTime;

            // หากเวลาเกินค่าที่กำหนดให้ทำบางอย่าง
            if (elapsedTime >= duration)
            {
                PerformAction();
                elapsedTime = 0f; // รีเซ็ตตัวจับเวลาเพื่อเริ่มนับใหม่
            }
        }
        else
        {
            // รีเซ็ตเวลาถ้าความเร็วกลับมาเกิน Threshold
            elapsedTime = 0f;
        }
    }
    
    private void PerformAction()
    {
        // Disable CameraTracking
        if (cinemachineBrain != null)
        {
            cinemachineBrain.enabled = false;
            Debug.Log("Cinemachine Brain disabled.");
        }
        
        Debug.Log("Velocity exceeded the threshold for 3 seconds!");
        // ทำบางอย่างที่คุณต้องการ
    }
}
