using UnityEngine;
using System.Collections;

public class PenguinBridge : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer sr;

    public Transform bridgePoint;

    // Collider do E / Quiz
    public BoxCollider2D interactionCollider;

    // Collider da ponte
    public BoxCollider2D bridgeCollider;

    public float jumpHeight = 2f;
    public float jumpDuration = 0.6f;

    public void StartBridge()
    {
        StartCoroutine(BridgeSequence());
    }

    IEnumerator BridgeSequence()
    {
        // Desativar interação
        interactionCollider.enabled = false;

        // Ponte desativada inicialmente
        bridgeCollider.enabled = false;

        // Virar para a direita antes do salto
        sr.flipX = false;

        Vector3 startPos = transform.position;

        float time = 0f;

        // Salto parabólico até ao BridgePoint
        while (time < jumpDuration)
        {
            float t = time / jumpDuration;

            float height =
                4f *
                jumpHeight *
                t *
                (1f - t);

            transform.position =
                Vector3.Lerp(
                    startPos,
                    bridgePoint.position,
                    t
                )
                +
                Vector3.up * height;

            time += Time.deltaTime;

            yield return null;
        }

        // Garantir posição final
        transform.position =
            bridgePoint.position;

        // Tocar animação
        anim.SetTrigger("Bridge");

        // Esperar terminar a animação
        yield return new WaitForSeconds(0.5f);

        Vector3 startScale =
    transform.localScale;

        Quaternion startRotation =
            transform.rotation;

        float transformTime = 0f;
        float transformDuration = 0.8f;

        while (transformTime < transformDuration)
        {
            float t =
                transformTime /
                transformDuration;

            transform.rotation =
                Quaternion.Lerp(
                    startRotation,
                    bridgePoint.rotation,
                    t
                );

            transform.localScale =
                Vector3.Lerp(
                    startScale,
                    bridgePoint.localScale,
                    t
                );

            transformTime += Time.deltaTime;

            yield return null;
        }

        transform.rotation =
            bridgePoint.rotation;

        transform.localScale =
            bridgePoint.localScale;

        // Ativar collider da ponte
        bridgeCollider.enabled = true;
    }
}