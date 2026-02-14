using System;
using UnityEngine;
using UnityEngine.UI;

public class StarEffect : MonoBehaviour
{
    [Header("Animation Settings")]
    public float jumpForce = 200f;      // force initiale vers le haut
    public float gravity = -500f;       // gravité appliquée
    public float shrinkSpeed = 0.5f;    // vitesse de réduction
    public float fadeDuration = 0.1f;   // fade in rapide
    public float lifetime = 3f;         // durée avant destruction
    public float randomPower;
    public AnimationCurve randomPowerRotationMultCurve;

    private RectTransform rectTransform;
    private Image image;
    private float verticalVelocity;
    private float powerRotationMult;
    public float rotationPower;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        // Commence transparent
        if (image != null)
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);

        rotationPower = UnityEngine.Random.Range(-randomPower, randomPower);
        rectTransform.localScale = Vector3.one * UnityEngine.Random.Range(0.8f, 1.2f);
        powerRotationMult = randomPowerRotationMultCurve.Evaluate(Math.Abs(rotationPower) / randomPower);

        verticalVelocity = jumpForce;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        float dt = Time.deltaTime;
        rectTransform.Rotate(0, 0, 360f * rotationPower * powerRotationMult * dt);

        // Fade in rapide
        if (image != null && image.color.a < 1f)
        {
            Color c = image.color;
            c.a += dt / fadeDuration;
            c.a = Mathf.Clamp01(c.a);
            image.color = c;
        }

        // Appliquer gravité
        verticalVelocity += gravity * dt;
        rectTransform.anchoredPosition += new Vector2(rotationPower * dt, verticalVelocity * dt);

        // Réduction de taille
        rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, Vector3.zero, shrinkSpeed * dt);
    }
}