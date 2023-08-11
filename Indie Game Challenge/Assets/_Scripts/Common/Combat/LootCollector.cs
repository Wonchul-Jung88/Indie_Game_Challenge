using UnityEngine;

//https://www.youtube.com/watch?v=OUB1l9i2Gxg&list=PL10AGOjv18tiiY44JKzGEzeHZCjL0dz43&index=1
public class LootCollector : MonoBehaviour
{
    private Transform lootTracker;

    private void Start()
    {
        // Player‚ÌŽq—v‘f‚Å‚ ‚éLootTracker‚ðŽæ“¾
        lootTracker = transform.parent.Find("LootTracker");
    }

    private void OnTriggerEnter(Collider other)
    {
        Transform parentTransform = other.transform.parent;
        if (parentTransform)
        {
            LootFollow lootFollow = parentTransform.GetComponent<LootFollow>();
            if (lootFollow)
            {
                lootFollow.Target = lootTracker;
                lootFollow.Dectected();
            }
        }
    }
}