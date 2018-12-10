using UnityEngine;

 // The script is serializable so that we can edit it in the inspector

    [System.Serializable]
    public class ColorToGameObject  {

    public Color color; // Reference to the color that we will choose -> if this color is presented in the template then the script will know what prefab to spawn

    public GameObject prefab; //Reference to the prefab of the above mentioned color
}
