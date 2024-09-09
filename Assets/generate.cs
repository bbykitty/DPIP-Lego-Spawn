using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RandomObjectSpawner : MonoBehaviour
{

    public GameObject[] myObjects;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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

        if (Input.GetKeyDown(KeyCode.L))
        {
            for(float y = .75f; y < 4; y+=1.5f)
            {
                for(int attempts = 0; attempts < 1000; attempts++)
                {
                    float bx = 2f;
                    float by = 1.5f;
                    float bz = 2f;
                    int rotation = (90*Random.Range(0,2));
                    int randomIndex = Random.Range(0, myObjects.Length);
                    
                    Vector3 randomSpawnPosition = new Vector3(Random.Range(0, 3)*2.1f+1, y, Random.Range(0, 3)*2.1f+1);
                    if(randomIndex > 4)
                    {
                        if(randomIndex < 10)
                        {
                            bx = 3;
                        }else
                        {
                            bx = 4;
                        }
                        if(rotation>0)
                        {
                            randomSpawnPosition = new Vector3(Random.Range(0, 3)*2.1f+1, y, Random.Range(1, 3)*2.1f);
                        }else
                        {
                            randomSpawnPosition = new Vector3(Random.Range(1,3)*2.1f, y, Random.Range(0,3)*2.1f+1);
                            }
                    }
                
                    var quat = Quaternion.Euler(0,rotation,0);
                    if(Physics.OverlapBox(randomSpawnPosition,new Vector3(bx/2,by/3,bz/2)).Length == 0)
                    {
                        GameObject myObject = Instantiate(myObjects[randomIndex], randomSpawnPosition, quat);
                        var bc = myObject.AddComponent<BoxCollider>();
                        bc.size = new Vector3(bx-.1f,by-.08f,bz-.1f);
                        var rb = myObject.AddComponent<Rigidbody>();
                        rb.freezeRotation = true;
                        Debug.Log($"Placed {myObject.name} at {randomSpawnPosition}.");
                    }
                }
            }
        }
        

        if(Input.GetKeyDown(KeyCode.X))
        {
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
