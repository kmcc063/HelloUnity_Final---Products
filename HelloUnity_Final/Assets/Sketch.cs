using UnityEngine;
using Pathfinding.Serialization.JsonFx; //make sure you include this using

public class Sketch : MonoBehaviour {
    public GameObject Cube;
    public GameObject Sphere;
    public string _WebsiteURL = "http://infomgmt192.azurewebsites.net/tables/Mountain?zumo-api-version=2.0.0";

    void Start () {
        //Reguest.GET can be called passing in your ODATA url as a string in the form:
        //http://{Your Site Name}.azurewebsites.net/tables/{Your Table Name}?zumo-api-version=2.0.0
        //The response produce is a JSON string
        string jsonResponse = Request.GET(_WebsiteURL);

        //Just in case something went wrong with the request we check the reponse and exit if there is no response.
        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }

        //We can now deserialize into an array of objects - in this case the class we created. The deserializer is smart enough to instantiate all the classes and populate the variables based on column name.
        Mountain[] mountains = JsonReader.Deserialize<Mountain[]>(jsonResponse);

        //----------------------
        //YOU WILL NEED TO DECLARE SOME VARIABLES HERE SIMILAR TO THE CREATIVE CODING TUTORIAL

        int i = 0;
        int totalCubes = mountains.Length;
        //float totalDistance = 2.9f;
        //----------------------

        //We can now loop through the array of objects and access each object individually
        foreach (Mountain mountain in mountains)
        {
            //Example of how to use the object
            Debug.Log("This mountain name is: " + mountain.MountainName);
            //----------------------
            //YOUR CODE TO INSTANTIATE NEW PREFABS GOES HERE
            float perc = i / (float)totalCubes;
            float sin = Mathf.Sin(perc * Mathf.PI / 2);

            float x = mountain.X;
            float y = mountain.Y;
            float z = mountain.Z;
            string symbol = mountain.Symbol;
            


            if (symbol == "Cube")
            {
               var newCube = (GameObject)Instantiate(Cube, new Vector3(x, y, z), Quaternion.identity);

                newCube.GetComponent<CubeScript>().SetSize(mountain.Size);
                newCube.GetComponent<CubeScript>().rotateSpeed = .2f + perc * 4.0f;
                //newCube.GetComponent<CubeScript>().rotateSpeed = 0;
                newCube.transform.Find("New Text").GetComponent<TextMesh>().text = mountain.MountainName;//"Hullo Again";
            } 

            if( symbol == "Sphere")
            {
                var newSphere = (GameObject)Instantiate(Sphere, new Vector3(x, y, z), Quaternion.identity);

                newSphere.GetComponent<SphereScript>().SetSize(mountain.Size);
                newSphere.GetComponent<SphereScript>().rotateSpeed = .2f + perc * 4.0f;
                newSphere.transform.Find("New Text").GetComponent<TextMesh>().text = mountain.MountainName;//"Hullo Again";
            }

            i++;

            //----------------------
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
