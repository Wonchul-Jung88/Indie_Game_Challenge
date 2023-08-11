using UnityEngine;

//https://www.youtube.com/watch?v=OUB1l9i2Gxg&list=PL10AGOjv18tiiY44JKzGEzeHZCjL0dz43&index=1
public class LootFollow : MonoBehaviour
{
    [HideInInspector] public Transform Target;
    private Vector3 _velocity = Vector3.zero;
    public float MinModifier;
    public float MaxModifier;
    private bool _isFollowing = false;
    private bool _isDetected = false;

    public void StartFollowing()
    {
        _isFollowing = true;
    }

    public void Dectected()
    {
        _isDetected = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFollowing && _isDetected)
        {
            transform.position = Vector3.SmoothDamp(transform.position, Target.position, ref _velocity, Time.deltaTime * Random.Range(MinModifier, MaxModifier));
        }
    }
}