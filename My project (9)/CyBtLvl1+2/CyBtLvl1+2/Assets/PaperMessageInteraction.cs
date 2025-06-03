using UnityEngine;

public class PaperMessageInteraction : MonoBehaviour
{
    public GameObject interactionPrompt; // نص "اضغط B للمسح"
    private bool playerInRange = false; // هل اللاعب ضمن المدى؟
    private GameObject playerHoldingMessage = null; // المرجع للاعب اللي ماسك الرسالة

    // نقطة الإمساك في يد اللاعب (ممكن تكون GameObject فارغ في PlayerCapsule)
    public Transform playerHandTransform; 

    // هذا الكود بيشتغل لما كائن (Collider) يدخل للمنطقة تبع الكوليدر تبع الرسالة
    void OnTriggerEnter(Collider other)
    {
        // تأكد إن اللي دخل هو اللاعب (رح نتعرف عليه عن طريق الـ Tag)
        if (other.CompareTag("Player")) 
        {
            playerInRange = true;
            Debug.Log("Player entered range of message.");
            // ممكن هنا نظهر رسالة "اضغط B للمسح"
            if (interactionPrompt != null) 
            {
                interactionPrompt.SetActive(true);
            }
        }
    }

    // هذا الكود بيشتغل لما كائن (Collider) يخرج من المنطقة تبع الكوليدر تبع الرسالة
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player left range of message.");
            // ممكن هنا نخفي رسالة "اضغط B للمسح"
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(false);
            }
        }
    }

    void Update()
    {
        // إذا اللاعب ضمن المدى ولسه ما مسك الرسالة
        if (playerInRange && playerHoldingMessage == null)
        {
            // إذا ضغط اللاعب على زر B (يمكنك تغيير KeyCode.B حسب رغبتك)
            if (Input.GetKeyDown(KeyCode.B))
            {
                Debug.Log("Player pressed B. Picking up message.");
                PickUpMessage();
            }
        }
    }

    void PickUpMessage()
    {
        // إخفاء الورقة من المشهد (ممكن تخفي الـ Mesh Renderer أو الـ GameObject كله)
        // نفضل تعطيل الـ Renderer والـ Collider عشان ما تختفي تماماً لو بدنا نرجعها بعدين
        // GetComponent<MeshRenderer>().enabled = false; 
        // GetComponent<Collider>().enabled = false; 

        // الأفضل إننا نعمل SetParent للاعب
        this.transform.SetParent(playerHandTransform); // نجعل الرسالة طفلاً لـ HandTransform
        this.transform.localPosition = Vector3.zero; // نضعها عند نقطة يد اللاعب بالضبط
        this.transform.localRotation = Quaternion.identity; // نضبط دورتها لتكون مستقيمة

        // تعطيل الـ Rigidbody تبع الرسالة عشان ما تتأثر بالفيزياء بعد الإمساك
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().detectCollisions = false; // منع التصادمات

        // إخفاء رسالة التفاعل
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }

        playerHoldingMessage = playerHandTransform.parent.gameObject; // نحدد اللاعب اللي ماسك الرسالة
    }

    // يمكن إضافة دالة لرمي الرسالة إذا أردت
    public void DropMessage()
    {
        if (playerHoldingMessage != null)
        {
            this.transform.SetParent(null); // نفصل الرسالة عن اللاعب
            //GetComponent<MeshRenderer>().enabled = true; // إظهار الرسالة
            //GetComponent<Collider>().enabled = true; // تفعيل الكوليدر

            GetComponent<Rigidbody>().isKinematic = false; // تفعيل الفيزياء مرة أخرى
            GetComponent<Rigidbody>().detectCollisions = true; // تفعيل التصادمات

            // ممكن نضيف قوة دفع خفيفة عشان "ترمي" الرسالة
            // GetComponent<Rigidbody>().AddForce(transform.forward * 5f, ForceMode.Impulse); 

            playerHoldingMessage = null; // اللاعب ما عاد ماسك الرسالة
        }
    }
}