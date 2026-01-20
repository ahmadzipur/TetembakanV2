using UnityEngine;
using System.IO.Ports;
using System.Globalization;
using UnityEngine.InputSystem;

public class GyroFPSCamera : MonoBehaviour
{
    public string portName = "COM3";
    public int baudRate = 9600;
    bool serialConnected = false;


    public Transform playerBody;

    [Header("Sensitivity")]
    public float pitchSensitivity = 5.0f;
    public float yawSensitivity = 5.0f;
    public float rollSensitivity = 2.5f;

    [Header("Clamp (Visual Only)")]
    public float maxPitch = 90f;
    // public float maxYaw = 100f;
    public float maxRoll = 90f;

    [Header("Smoothing")]
    [Range(0.05f, 0.5f)]
    public float smoothFactor = 0.15f;

    [Header("Anti Drift")]
    public float deadZone = 0.001f;

    [Header("Auto Return")]
    public float returnSpeed = 3.0f;


    SerialPort serial;

    float pitch, yaw, roll;
    float targetPitch, targetYaw, targetRoll;

    float basePitch, baseYaw, baseRoll;

    void Start()
    {
        portName = PlayerPrefs.GetString("GYRO_COM_PORT", portName);
        try
        {
            serial = new SerialPort(portName, baudRate);
            serial.ReadTimeout = 1;
            serial.Open();

            serialConnected = true;
            Debug.Log($"Serial CONNECTED ({portName})");
        }
        catch (System.Exception e)
        {
            serialConnected = false;
            Debug.LogError($"Serial FAILED ({portName}) : {e.Message}");
        }
    }

    void Update()
    {
        if (serial == null || !serial.IsOpen) return;

        // 🔘 Kalibrasi arah depan
        if (Keyboard.current != null && Keyboard.current.oKey.wasPressedThisFrame)
        {
            Calibrate();
        }

        if (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame)
        {
            Calibrate();
        }


        // ===== Baca SEMUA data yang tersedia =====
        while (serial.BytesToRead > 0)
        {

            try
            {
                string line = serial.ReadLine();
                string[] v = line.Split(',');
                if (v.Length < 3) return;

                float dPitch = float.Parse(v[0], CultureInfo.InvariantCulture);
                float dYaw = float.Parse(v[1], CultureInfo.InvariantCulture);
                float dRoll = float.Parse(v[2], CultureInfo.InvariantCulture);

                dPitch = Mathf.Abs(dPitch) < deadZone ? 0f : dPitch;
                dYaw = Mathf.Abs(dYaw) < deadZone ? 0f : dYaw;
                dRoll = Mathf.Abs(dRoll) < deadZone ? 0f : dRoll;

                // 🔥 Integrasi TANPA clamp
                targetPitch += dPitch * pitchSensitivity * Time.deltaTime;
                targetYaw += -dYaw * yawSensitivity * Time.deltaTime;
                targetRoll += -dRoll * rollSensitivity * Time.deltaTime;
            }
            catch { }
        }
    }

    void Calibrate()
    {
        basePitch = targetPitch;
        baseYaw = targetYaw;
        baseRoll = targetRoll;

        Debug.Log("Gyro calibrated (baseline set)");
    }


    void LateUpdate()
    {
        // Offset dari baseline
        float calibratedPitch = targetPitch - basePitch;
        float calibratedYaw = targetYaw - baseYaw;
        // float calibratedRoll  = targetRoll  - baseRoll;
        float calibratedRoll  = targetRoll  - baseRoll;
        // AUTO RETURN KE TENGAH (ROLL SAJA)
        targetRoll = Mathf.Lerp(targetRoll, baseRoll, Time.deltaTime * returnSpeed );

        // 🔒 Clamp OUTPUT (visual saja)
        calibratedPitch = Mathf.Clamp(calibratedPitch, -maxPitch, maxPitch);
        // calibratedYaw = Mathf.Clamp(calibratedYaw, -maxYaw, maxYaw);
        // calibratedRoll = Mathf.Clamp(targetRoll, -maxRoll, maxRoll);

        // Smooth
        pitch = Mathf.Lerp(pitch, calibratedPitch, smoothFactor);
        yaw = Mathf.Lerp(yaw, calibratedYaw, smoothFactor);
        roll = Mathf.Lerp(roll, calibratedRoll, smoothFactor);

        // Apply rotation
        playerBody.localRotation = Quaternion.Euler(0f, yaw, 0f);
        transform.localRotation = Quaternion.Euler(pitch, 0f, roll);
    }

    void OnDestroy()
    {
        if (serial != null && serial.IsOpen)
            serial.Close();
    }
}
