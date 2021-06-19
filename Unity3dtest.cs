using UnityEngine;
using UnityEditor;





public class CaptureTransform : ScriptableObject {
	

	private static Object[] gameObjects;
	private static Vector3[] positions;
	private static Quaternion[] rotations;
	private static Vector3[] scales;

	[MenuItem ("Custom/Copy Transforms")]
	static void CaptureGameObject() {
        gameObjects = GetSelectedGameObjects();
        
		int length = gameObjects.Length;
		string str;
		GameObject go;
		
		positions 	= new Vector3[ length ];
		rotations	= new Quaternion[ length ];
		scales 		= new Vector3[ length ];
		
		for (int i = 0; i < length; i++) 
		{	
			go = (GameObject) gameObjects[i];
			
			positions[i] 	= CloneVector3D( go.transform.position );
			rotations[i] 	= CloneQuaternion( go.transform.rotation );
			scales[i] 		= CloneVector3D( go.transform.localScale );
			
			str  = "---------------------- \n";
			str += go.name + " >> ";
			str += "position: " + go.transform.position.ToString() + " | ";
			str += "rotation: " +go.transform.rotation.ToString() + " | ";
			str += "scale: " +go.transform.localScale.ToString();
			
			Debug.Log(str);
			
		}
    }
	
	[MenuItem ("Custom/Apply Transforms")]
	static void ApplyTransformToGameObject() {
		
		Selection.objects = gameObjects;
		
		int length = gameObjects.Length;
		GameObject go;
		
		for (int i = 0; i < length; i++) 
		{	
			go = (GameObject) gameObjects[i];
			go.transform.position = positions[i];
			go.transform.rotation = rotations[i];
			go.transform.localScale = scales[i];
			
		}
    }
	
	
	static Object[] GetSelectedGameObjects()
    {
        return Selection.GetFiltered(typeof(GameObject), SelectionMode.DeepAssets);
    }
	
	static Vector3 CloneVector3D ( Vector3 v )
	{
		return new Vector3(v.x, v.y, v.z);
	}
	
	static Quaternion CloneQuaternion ( Quaternion q )
	{
		return new Quaternion(q.x, q.y, q.z, q.w);
	}
}
