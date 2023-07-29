using UnityEngine;
using UnityEngine.Animations.Rigging;

public class NPCInteractionController : MonoBehaviour
{
    public GameObject _merchantImage;
    private GameObject _usingImage;

    //アタッチするRigBuilderを指定します。Inspectorから設定してください。
    public RigBuilder rigBuilder;
    //アニメーションの変化速度
    public float rigAnimationSpeed = 1f;

    //会話可能かどうかを判断するフラグ
    public bool CanTalk { get; private set; } = false;

    //目標とするアニメーションのウェイト
    private float targetWeight = 0;

    private void Start()
    {
        if (rigBuilder == null)
        {
            Debug.LogError("RigBuilderが設定されていません。");
            return;
        }

        foreach (var rig in rigBuilder.layers)
        {
            rig.rig.weight = 0;
        }
    }

    private void Update()
    {
        foreach (var rig in rigBuilder.layers)
        {
            rig.rig.weight = Mathf.Lerp(rig.rig.weight, targetWeight, Time.deltaTime * rigAnimationSpeed);
        }

        _usingImage.transform.position = Camera.main.WorldToScreenPoint(transform.position + 2 * Vector3.up);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BoxColliderTag"))
        {
            targetWeight = 1;
            _usingImage = Instantiate(_merchantImage, FindObjectOfType<Canvas>().transform);
        }
        else if (other.gameObject.CompareTag("SphereColliderTag"))
        {
            CanTalk = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BoxColliderTag"))
        {
            targetWeight = 0;
            Destroy(_usingImage);
        }
        else if (other.gameObject.CompareTag("SphereColliderTag"))
        {
            CanTalk = false;
        }
    }
}
