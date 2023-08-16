using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlastWaveExplosion : MonoBehaviour
{
    public int PointsCount;
    public float MaxRadius;
    public float speed;
    public float startWidth;
    public float force;
    public int BlastDamage;// { get; set; }

    private LineRenderer _lineRenderer;

    private void OnEnable()
    {
        _lineRenderer = GetComponent<LineRenderer>();

        _lineRenderer.positionCount = PointsCount + 1;
    }

    private bool isBlastRunning = false;

    private void Update()
    {
        if (!isBlastRunning)
        {
            StartCoroutine(Blast());
        }
    }

    public IEnumerator Blast()
    {
        isBlastRunning = true;

        float currentRadius = 0.0f;

        while (currentRadius < MaxRadius)
        {
            currentRadius += Time.deltaTime * speed;
            Draw(currentRadius);
            Damage(currentRadius);
            yield return null;
        }

        _lineRenderer.widthMultiplier = Mathf.Lerp(0f, startWidth, 1f - currentRadius / MaxRadius);

        if (transform.parent) // e‚ª‘¶Ý‚·‚éê‡
        {
            Destroy(transform.parent.gameObject);
        }

        isBlastRunning = false;

        Destroy(gameObject); // Destroy the object when the blast is done.
    }

    public void Damage(float currentRadius)
    {
        Collider[] hittingObjects = Physics.OverlapSphere(transform.position, currentRadius);

        foreach (Collider hitObject in hittingObjects)
        {
            EnemyAITutorial enemyAI = hitObject.gameObject.GetComponent<EnemyAITutorial>();
            if ( enemyAI != null && !enemyAI.isDead )
            {
                Rigidbody rb = hitObject.GetComponent<Rigidbody>();
                if (!rb)
                    continue;

                Vector3 direction = (hitObject.transform.position - transform.position).normalized;
                rb.AddForce(direction * force, ForceMode.Impulse);
                enemyAI.TakeDamageToDie(BlastDamage);
            }
        }
    }

    private void Draw(float currentRadius)
    {
        float angleBetweenPoints = 360.0f / PointsCount;

        for ( int i = 0; i <= PointsCount; i++ )
        {
            float angle = angleBetweenPoints * i * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0f);
            Vector3 position = direction * currentRadius;

            _lineRenderer.SetPosition(i, position);
        }
    }
}
