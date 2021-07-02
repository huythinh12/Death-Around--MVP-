using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
public class EnemyDataEditor : OdinMenuEditorWindow
{
    //tạo menu bên ngoài tool 
    [MenuItem("Tools/Enemy Data")]
    private static void OpenWindow()
    {
        GetWindow<EnemyDataEditor>().Show();
    }
    private CreateNewEnemyData createNewEnemyData;
    protected override void OnDestroy()//để tránh lưu lại instance khi đóng window editor nếu ko khi còn lưu lại lúc tạo mới sẽ bị warning vì đã tồn tại instance từ trước
    {
        base.OnDestroy();
        if (createNewEnemyData != null)
        {
            DestroyImmediate(createNewEnemyData.enemyData);
        }
    }
    protected override OdinMenuTree BuildMenuTree()
    {
        //tạo ra 1 tree quản lý các data 
        var tree = new OdinMenuTree();
        createNewEnemyData = new CreateNewEnemyData();
        tree.Add("Create New", createNewEnemyData);//đối số đầu tiên là tạo ra 1 mục mới, thứ 2 là sẽ tạo object từ CreateNewEnemyData.
        //đưa toàn bộ những object mình đã tạo vào cây thư mục của odin
        tree.AddAllAssetsAtPath("Enemy Data", "Assets/Data Assets", typeof(EnemyData));
        return tree;
    }
    protected override void OnBeginDrawEditors()
    {
        base.OnBeginDrawEditors();
        OdinMenuTreeSelection selected = this.MenuTree.Selection;
        SirenixEditorGUI.BeginHorizontalToolbar();
        {
            GUILayout.FlexibleSpace();
            if(SirenixEditorGUI.ToolbarButton("Delete Current"))
            {
                EnemyData asset = selected.SelectedValue as EnemyData;
                string path = AssetDatabase.GetAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
            }
        }
        SirenixEditorGUI.EndHorizontalToolbar();
    }

    public class CreateNewEnemyData
    {
        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]//ẩn field này nhưng nó sẽ hiện trong inline editor thôi
        public EnemyData enemyData;
        //tạo constructor
        public CreateNewEnemyData()
        {
            //lưu ý cái type bên trong phải là 1 Scriptable
            enemyData = ScriptableObject.CreateInstance<EnemyData>();
        }
        [Button("Add New Enemy SO")]
        private void CreateNewData()
        {
            //tạo assets , đối số thứ 2 bao gồm tên đường dẫn và tên file asset muốn lưu
            AssetDatabase.CreateAsset(enemyData, $"Assets/Data Assets/{enemyData.enemyName}.asset");
            AssetDatabase.SaveAssets();//lưu assets này lại vào unity editor
            //sau khi ta tạo ra scriptable ta cần tạo ra instance để nó lưu instance scriptable này vào editor đúng nghĩa

            enemyData = ScriptableObject.CreateInstance<EnemyData>();
            enemyData.enemyName = "New Enemy Data";

        }
    }
}
