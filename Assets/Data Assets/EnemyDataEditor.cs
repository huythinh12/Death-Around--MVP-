//using UnityEngine;
//using UnityEditor;
//using Sirenix.OdinInspector;
//using Sirenix.OdinInspector.Editor;
//using Sirenix.Utilities.Editor;
//public class EnemyDataEditor : OdinMenuEditorWindow
//{
//    [MenuItem("Tools/Enemy Data")]
//    private static void OpenWindow()
//    {
//        GetWindow<EnemyDataEditor>().Show();
//    }
//    private CreateNewEnemyData createNewEnemyData;
//    protected override void OnDestroy()
//    {
//        base.OnDestroy();
//        if (createNewEnemyData != null)
//        {
//            DestroyImmediate(createNewEnemyData.enemyData);
//        }
//    }
//    protected override OdinMenuTree BuildMenuTree()
//    {
//        var tree = new OdinMenuTree();
//        createNewEnemyData = new CreateNewEnemyData();
//        tree.Add("Create New", createNewEnemyData);
//        tree.AddAllAssetsAtPath("Enemy Data", "Assets/Data Assets", typeof(EnemyData));
//        return tree;
//    }
//    protected override void OnBeginDrawEditors()
//    {
//        base.OnBeginDrawEditors();
//        OdinMenuTreeSelection selected = this.MenuTree.Selection;
//        SirenixEditorGUI.BeginHorizontalToolbar();
//        {
//            GUILayout.FlexibleSpace();
//            if (SirenixEditorGUI.ToolbarButton("Delete Current"))
//            {
//                EnemyData asset = selected.SelectedValue as EnemyData;
//                string path = AssetDatabase.GetAssetPath(asset);
//                AssetDatabase.DeleteAsset(path);
//                AssetDatabase.SaveAssets();
//            }
//        }
//        SirenixEditorGUI.EndHorizontalToolbar();
//    }

//    public class CreateNewEnemyData
//    {
//        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
//        public EnemyData enemyData;

//        public CreateNewEnemyData()
//        {
//            enemyData = ScriptableObject.CreateInstance<EnemyData>();
//        }
//        [Button("Add New Enemy SO")]
//        private void CreateNewData()
//        {

//            AssetDatabase.CreateAsset(enemyData, $"Assets/Data Assets/{enemyData.enemyName}.asset");
//            AssetDatabase.SaveAssets();

//            enemyData = ScriptableObject.CreateInstance<EnemyData>();
//            enemyData.enemyName = "New Enemy Data";

//        }
//    }
//}
