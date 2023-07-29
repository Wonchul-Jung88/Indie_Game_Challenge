using UnityEngine;
using UnityEngine.Animations.Rigging;

public class NPCInteractionController : MonoBehaviour
{
    public GameObject _merchantImage;
    private GameObject _usingImage;

    //�A�^�b�`����RigBuilder���w�肵�܂��BInspector����ݒ肵�Ă��������B
    public RigBuilder rigBuilder;
    //�A�j���[�V�����̕ω����x
    public float rigAnimationSpeed = 1f;

    //��b�\���ǂ����𔻒f����t���O
    public bool CanTalk { get; private set; } = false;

    //�ڕW�Ƃ���A�j���[�V�����̃E�F�C�g
    private float targetWeight = 0;

    private void Start()
    {
        if (rigBuilder == null)
        {
            Debug.LogError("RigBuilder���ݒ肳��Ă��܂���B");
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
