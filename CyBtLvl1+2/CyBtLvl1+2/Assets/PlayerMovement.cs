using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // سرعة حركة اللاعب
    public Transform playerHandPosition; // هذا هو اللي رح نربطه في اليونيتي Editor (نقطة اليد)

    // Start يُستدعى مرة واحدة عند بدء تشغيل اللعبة
    void Start()
    {
        // هذا التأكيد على أن الكائن (اللاعب) له التاج "Player"
        //gameObject.tag = "Player"; // تم تطبيقها يدويا، لكن لا ضرر من وجودها

        // إذا لم يتم ربط playerHandPosition في الـ Inspector، يتم إنشاؤه تلقائياً
        if (playerHandPosition == null)
        {
            Debug.LogWarning("PlayerHandPosition not assigned! Auto-creating a default one.");
            GameObject handGO = new GameObject("PlayerHand_AutoCreated");
            handGO.transform.parent = this.transform; // اجعلها طفلاً للاعب
            handGO.transform.localPosition = new Vector3(0.5f, 0.5f, 0.5f); // عدّل هذا الموقع حسب مكان اليد
            playerHandPosition = handGO.transform;
        }

        // إذا كان الـ Rigidbody غير Is Kinematic، ممكن نستخدم Physics Material لمنع الانزلاق
        // وإلا فهو لا ينزلق لأنه يتحرك بالـ Transform مباشرة.
    }

    // Update يُستدعى كل فريم
    void Update()
    {
        // استقبال مدخلات حركة لوحة المفاتيح
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D أو Left/Right Arrow
        float verticalInput = Input.GetAxis("Vertical");   // W/S أو Up/Down Arrow

        // إنشاء متجه حركة بناءً على المدخلات
        // Note: Vector3.forward و Vector3.right هي اتجاهات عالمية
        // إذا بدك اللاعب يتحرك بالنسبة لاتجاه الكاميرا، تحتاج كود مختلف (أكثر تعقيداً)
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);

        // تطبيع المتجه (Normalize) لمنع الحركة الأسرع عند الحركة القطرية
        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        // تطبيق الحركة بناءً على السرعة والوقت بين الفريمات
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // (اختياري) كود للقفز بسيط لو بدك:
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //    // لو الـ Rigidbody مش Is Kinematic، ممكن تضيف قوة قفز
        //    // GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        // }
    }
}