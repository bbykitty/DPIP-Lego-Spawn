using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class RandomObjectSpawner : MonoBehaviour
{

    public GameObject[] myObjects;
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
    public int[] numBlocks = new int[20];
    public int[] maxBlocks = { 14, 14, 14, 14, 14, 14, 14, 14, 14, 14, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

    IEnumerator Screenshot()
    {
        yield return new WaitForSeconds(1);
        string folderPath = Application.dataPath +"\\Screenshots";
        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);

        string structureName = System.DateTime.Now.ToString("MM-dd-yyyy_HH-mm-ss");
        folderPath = System.IO.Path.Combine(folderPath, structureName);
        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);


        Collider[] colliders = Physics.OverlapBox(new Vector3(3, 3, 3), new Vector3(5, 5, 5));
        using (StreamWriter sw = new StreamWriter(folderPath + "\\" + structureName + "_coordinates.csv"))
        {
            sw.WriteLine("Block,X,Y,Z,Rotation");
            foreach (Collider block in colliders)
            {
                var blockPos = block.transform.position;
                double block_x = (double)((int)System.Math.Round(blockPos[0])-1)/2;
                double block_y = (int)System.Math.Round(blockPos[1])/2;
                double block_z = (double)((int)System.Math.Round(blockPos[2])-1)/2;
                bool rotated = block_x % 1 != 0;
                sw.WriteLine($"{block.name},{block_x},{block_y},{block_z},{rotated}");
            }
        }

        List<Camera> myCams = new List<Camera> { cam1, cam2, cam3 };
        for (int i = 0; i < 3; i++)
        {
            cam1.enabled = false;
            cam2.enabled = false;
            cam3.enabled = false;
            int randCam = Random.Range(0, myCams.Count);
            Camera myCamera = myCams[randCam];
            myCamera.enabled = true;
            string screenshotName = structureName + "_" + i + ".png";
            ScreenCapture.CaptureScreenshot($"{folderPath}\\{structureName}_{i+1}.png");
            //ScreenCapture.CaptureScreenshot($"Participant_{i}.png");
            Debug.Log(folderPath + structureName + "_" + i + ".png");
            myCams.RemoveAt(randCam);
            yield return new WaitForSeconds(.1f);
        }

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            float x = 2f;
            float y = 1.5f;
            float z = 2f;
            int rotation = (90*Random.Range(0,2));
            Vector3 randomSpawnPosition = new Vector3(Random.Range(0, 6), 6, Random.Range(0, 6));
            int randomIndex = Random.Range(0, myObjects.Length);
            if(randomIndex > 4)
            {
                if(randomIndex < 10)
                {
                    x = 3;
                }else
                {
                    x = 4;
                }
            }
            var quat = Quaternion.Euler(0,rotation,0);
            GameObject myObject = Instantiate(myObjects[randomIndex], randomSpawnPosition, quat);
            var rb = myObject.AddComponent<Rigidbody>();
            rb.freezeRotation = true;
            var bc = myObject.AddComponent<BoxCollider>();
            bc.size = new Vector3(x-.1f,y-.08f,z-.1f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (float y = .75f; y < 4; y += 1.5f)
            {
                for (int attempts = 0; attempts < 1000; attempts++)
                {
                    float bx = 2f;
                    float by = 1.5f;
                    float bz = 2f;
                    int rotation = (90 * Random.Range(0, 2));
                    int randomIndex = Random.Range(0, 10);
                    while(numBlocks[randomIndex] == maxBlocks[randomIndex])
                    {
                        Debug.Log($"Too many of item {randomIndex}, choosing new...");
                        randomIndex = Random.Range(0, 10);
                    }
                    Vector3 randomSpawnPosition = new Vector3(Random.Range(0, 3) * 2.1f + 1, y, Random.Range(0, 3) * 2.1f + 1);
                    if (randomIndex > 4)
                    {
                        bx = 4;
                        if (rotation > 0)
                        {
                            randomSpawnPosition = new Vector3(Random.Range(0, 3) * 2.1f + 1, y, Random.Range(1, 3) * 2.1f);
                        } else
                        {
                            randomSpawnPosition = new Vector3(Random.Range(1, 3) * 2.1f, y, Random.Range(0, 3) * 2.1f + 1);
                        }
                    }

                    var quat = Quaternion.Euler(0, rotation, 0);
                    if (Physics.OverlapBox(randomSpawnPosition, new Vector3(bx / 2, by / 3, bz / 2)).Length == 0)
                    {
                        GameObject myObject = Instantiate(myObjects[randomIndex], randomSpawnPosition, quat);
                        var bc = myObject.AddComponent<BoxCollider>();
                        bc.size = new Vector3(bx - .1f, by - .08f, bz - .1f);
                        var rb = myObject.AddComponent<Rigidbody>();
                        rb.freezeRotation = true;
                        numBlocks[randomIndex] += 1;
                    }
                }
            }
            StartCoroutine(Screenshot());
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            for (float y = .75f; y < 4; y += 1.5f)
            {
                for (int attempts = 0; attempts < 20; attempts++)
                {
                    float bx = 2f;
                    float by = 1.5f;
                    float bz = 2f;
                    int rotation = (90 * Random.Range(0, 2));
                    int randomIndex = Random.Range(0, myObjects.Length);
                    while (numBlocks[randomIndex] == maxBlocks[randomIndex])
                    {
                        Debug.Log($"Too many of item {randomIndex}, choosing new...");
                        randomIndex = Random.Range(0, myObjects.Length);
                    }
                    Vector3 randomSpawnPosition = new Vector3(Random.Range(0, 3) * 2.1f + 1, y, Random.Range(0, 3) * 2.1f + 1);
                    if (randomIndex > 4)
                    {
                        if (randomIndex < 15)
                        {
                            bx = 4;
                        }
                        else
                        {
                            bx = 3;
                        }
                        if (rotation > 0)
                        {
                            randomSpawnPosition = new Vector3(Random.Range(0, 4) * 2.1f + 1, y, Random.Range(1, 4) * 2.1f);
                        }
                        else
                        {
                            randomSpawnPosition = new Vector3(Random.Range(1, 4) * 2.1f, y, Random.Range(0, 4) * 2.1f + 1);
                        }
                    }

                    var quat = Quaternion.Euler(0, rotation, 0);
                    if (Physics.OverlapBox(randomSpawnPosition, new Vector3(bx / 2, by / 3, bz / 2)).Length == 0)
                    {
                        GameObject myObject = Instantiate(myObjects[randomIndex], randomSpawnPosition, quat);
                        var bc = myObject.AddComponent<BoxCollider>();
                        bc.size = new Vector3(bx - .1f, by - .08f, bz - .1f);
                        var rb = myObject.AddComponent<Rigidbody>();
                        rb.freezeRotation = true;
                        numBlocks[randomIndex] += 1;
                        Debug.Log(string.Join(", ", maxBlocks));
                        Debug.Log(string.Join(", ", numBlocks));
                    }
                }
            }
            StartCoroutine(Screenshot());
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            numBlocks = new int[20];
            Collider[] colliders;
            if((colliders = Physics.OverlapBox(new Vector3(2,8,2),new Vector3(10,7,10))).Length > 0)
            {
                foreach(var collider in colliders)
                {
                    Destroy(collider.gameObject);
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            Collider[] colliders;
            if((colliders = Physics.OverlapBox(new Vector3(2,8,2),new Vector3(10,2.5f,10))).Length > 0)
            {
                foreach(var collider in colliders)
                {
                    Destroy(collider.gameObject);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Collider[] colliders;
            if((colliders = Physics.OverlapBox(new Vector3(2,.75f,2),new Vector3(10,.5f,10))).Length > 0)
            {
                foreach(var collider in colliders)
                {
                    Destroy(collider.gameObject);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Collider[] colliders;
            if((colliders = Physics.OverlapBox(new Vector3(2,2.25f,2),new Vector3(10,.5f,10))).Length > 0)
            {
                foreach(var collider in colliders)
                {
                    Destroy(collider.gameObject);
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Collider[] colliders;
            if((colliders = Physics.OverlapBox(new Vector3(2,4,2),new Vector3(10,.5f,10))).Length > 0)
            {
                foreach(var collider in colliders)
                {
                    Destroy(collider.gameObject);
                }
            }
        }
    }
}
