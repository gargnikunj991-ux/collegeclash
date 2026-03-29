using UnityEngine;
using System.Collections.Generic;

public class CaptureZone : MonoBehaviour
{
    private HashSet<PlayerTeam> bluePlayers = new HashSet<PlayerTeam>();
    private HashSet<PlayerTeam> redPlayers = new HashSet<PlayerTeam>();

    [Header("Capture Settings")]
    private float captureProgress = 0.5f; // 0 = red, 1 = blue
    private TeamSide currentOwner = TeamSide.None;

    [Header("Scoring")]
    public float pointsPerSecond = 1f;
    private float nextScoreTime;

    [Header("Visuals")]
    public MeshRenderer zoneRenderer;

    public Color blueColor = Color.blue;
    public Color redColor = Color.red;
    public Color neutralColor = Color.white;
    public Color contestedColor = Color.yellow;

    private Material zoneMaterial;

    void Awake()
    {
        if (zoneRenderer == null)
            zoneRenderer = GetComponentInChildren<MeshRenderer>();

        if (zoneRenderer != null)
            zoneMaterial = zoneRenderer.material;
    }

    void Start()
    {
        currentOwner = TeamSide.None;
    }

    void Update()
    {
        CleanInvalidPlayers();
        HandleCapture();
        HandleScoring();
        UpdateVisuals();
    }

    void HandleCapture()
    {
        float blueRate = 0f;
        float redRate = 0f;

        foreach (var p in bluePlayers)
            if (p != null) blueRate += p.GetCaptureRate();

        foreach (var p in redPlayers)
            if (p != null) redRate += p.GetCaptureRate();

        // 🔴 CONTESTED → no progress at all
        if (blueRate > 0 && redRate > 0)
        {
            return;
        }

        if (blueRate > 0)
        {
            captureProgress += blueRate * Time.deltaTime;
        }
        else if (redRate > 0)
        {
            captureProgress -= redRate * Time.deltaTime;
        }

        captureProgress = Mathf.Clamp01(captureProgress);

        // Ownership only changes at full capture
        if (captureProgress >= 1f)
            currentOwner = TeamSide.TeamA;

        if (captureProgress <= 0f)
            currentOwner = TeamSide.TeamB;
    }

    void HandleScoring()
    {
        if (Time.time < nextScoreTime) return;

        // 🔴 STOP scoring if contested
        if (bluePlayers.Count > 0 && redPlayers.Count > 0)
            return;

        if (currentOwner == TeamSide.TeamA)
        {
            ScoreManager.Instance.AddPoint(0);
            nextScoreTime = Time.time + (1f / pointsPerSecond);
        }
        else if (currentOwner == TeamSide.TeamB)
        {
            ScoreManager.Instance.AddPoint(1);
            nextScoreTime = Time.time + (1f / pointsPerSecond);
        }
    }

    void UpdateVisuals()
    {
        if (zoneMaterial == null) return;

        int blue = bluePlayers.Count;
        int red = redPlayers.Count;

        // 🔴 CONTESTED COLOR
        if (blue > 0 && red > 0)
        {
            zoneMaterial.color = contestedColor;
            return;
        }

        // Otherwise show owner
        if (currentOwner == TeamSide.TeamA)
            zoneMaterial.color = blueColor;
        else if (currentOwner == TeamSide.TeamB)
            zoneMaterial.color = redColor;
        else
            zoneMaterial.color = neutralColor;
    }

    void CleanInvalidPlayers()
    {
        bluePlayers.RemoveWhere(p => p == null);
        redPlayers.RemoveWhere(p => p == null);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerTeam p = other.GetComponent<PlayerTeam>();
        if (p == null) return;

        if (p.team == TeamSide.TeamA)
            bluePlayers.Add(p);
        else if (p.team == TeamSide.TeamB)
            redPlayers.Add(p);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerTeam p = other.GetComponent<PlayerTeam>();
        if (p == null) return;

        if (p.team == TeamSide.TeamA)
            bluePlayers.Remove(p);
        else if (p.team == TeamSide.TeamB)
            redPlayers.Remove(p);
    }
}