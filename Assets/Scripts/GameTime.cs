using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

public class GameTime : MonoBehaviour
{
    [Header("Tiempo")]
    public float tiempo = 0f;
    public float velocidadTiempo = 20f;

    public int horas;
    public int minutos;

    [Header("UI")]
    public TextMeshProUGUI textoReloj;

    [Header("Luces")]
    public Light sol;

    [Header("Mapa")]
    public Tilemap tilemapFondo;

    [Header("Oscuridad")]
    public SpriteRenderer oscuridad;

    void Update()
    {
        tiempo += Time.deltaTime * velocidadTiempo;

        horas = (int)(tiempo / 60) % 24;
        minutos = (int)(tiempo % 60);

        if (textoReloj != null)
            textoReloj.text = horas.ToString("00") + ":" + minutos.ToString("00");

        UpdateCicloDiaNoche();
    }

    void UpdateCicloDiaNoche()
    {
        float hora = (tiempo % (24 * 60f)) / 60f;

        if (sol != null)
            sol.transform.rotation = Quaternion.Euler((hora / 24f) * 360f - 90f, 170f, 0f);

        float targetOscuridad = 0f;

        // 🌙 Noche profunda
        if (hora < 5f || hora >= 19f)
        {
            targetOscuridad = 0.7f;
        }
        // 🌅 Amanecer (5 → 7)
        else if (hora >= 5f && hora < 7f)
        {
            float t = (hora - 5f) / 2f; // 0 → 1
            targetOscuridad = Mathf.Lerp(0.7f, 0f, t);
        }
        // ☀️ Día
        else if (hora >= 7f && hora < 17f)
        {
            targetOscuridad = 0f;
        }
        // 🌇 Atardecer (17 → 19)
        else if (hora >= 17f && hora < 19f)
        {
            float t = (hora - 17f) / 2f;
            targetOscuridad = Mathf.Lerp(0f, 0.7f, t);
        }

        // 🔥 Transición suave REAL
        if (oscuridad != null)
        {
            Color actual = oscuridad.color;
            float nuevoAlpha = Mathf.Lerp(actual.a, targetOscuridad, Time.deltaTime * 2f);

            oscuridad.color = new Color(0, 0, 0, nuevoAlpha);
        }

        // ☀️ Luz del sol más suave
        if (sol != null)
        {
            sol.intensity = Mathf.Lerp(sol.intensity, 1f - targetOscuridad, Time.deltaTime * 2f);
        }
    }
}