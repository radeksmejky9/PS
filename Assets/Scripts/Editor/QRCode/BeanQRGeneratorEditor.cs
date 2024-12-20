using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BeanQRGenerator))]
public class BeanQRGeneratorEditor : Editor
{
    private BeanQRGenerator bean;
    private bool init = false;
    private string downloadsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile) + "/Downloads";

    private void OnEnable()
    {
        bean = (BeanQRGenerator)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate QR Code"))
        {
            bean.InitGenerator();
            init = true;
        }

        if (init && bean.QRGenerator.QRCodeTexture != null)
            DisplayQRCode();
    }

    private void DisplayQRCode()
    {
        GUILayout.Label("QR Code Preview:");
        GUILayout.Label(new GUIContent(bean.QRGenerator.QRCodeTexture), GUILayout.Width(256), GUILayout.Height(256));
        string name = $"{bean.QRGenerator.SnappingPoint.Building}-{bean.QRGenerator.SnappingPoint.Room}";

        if (GUILayout.Button("Save QR Code to PC"))
        {
            string path = EditorUtility.SaveFilePanel("Save QR Code", downloadsPath, $"QR-{name}.png", "png");
            bean.QRGenerator.SaveQRCode(path);
        }
        if (GUILayout.Button("Save QR Code to Snapping Points Folder"))
        {
            AssetDatabase.CreateAsset(bean.QRGenerator, $"Assets/ScriptableObjects/SnappingPoints/{name}.asset");
            AssetDatabase.SaveAssets();
        }
    }
}
