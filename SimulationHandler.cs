using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationHandler : MonoBehaviour
{
    Bird[] birds = new Bird[20];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i< birds.Length; i++)
        {
            birds[i] = new Bird();
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < birds.Length; i++)
        {
            birds[i].loop();
        }
    }
}

class Bird
{
    GameObject body;
    Rigidbody physics; 
    public Bird()
    {
        this.body = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        this.body.transform.position = new Vector3(Random.Range(-100,100), Random.Range(-100,100), Random.Range(-100,100));
        //give a random rotation to the bird body
        this.body.transform.Rotate(new Vector3(Random.Range(0,360), Random.Range(0,360), Random.Range(0,360)));
        physics = this.body.AddComponent(typeof(Rigidbody)) as Rigidbody;
        physics.useGravity = false;
    }

    public void loop()
    {
        physics.MovePosition(this.body.transform.position + this.body.transform.forward);
        if (this.body.transform.position.x > 100)
        {
            this.body.transform.position = new Vector3(-100,this.body.transform.position.y,this.body.transform.position.z);
        }
        if (this.body.transform.position.y > 100)
        {
            this.body.transform.position = new Vector3(this.body.transform.position.x, -100, this.body.transform.position.z);
        }
        if (this.body.transform.position.z > 100)
        {
            this.body.transform.position = new Vector3(this.body.transform.position.x, this.body.transform.position.y, -100);
        }
        if (this.body.transform.position.x < -100)
        {
            this.body.transform.position = new Vector3(100,this.body.transform.position.y,this.body.transform.position.z);
        }
        if (this.body.transform.position.y < -100)
        {
            this.body.transform.position = new Vector3(this.body.transform.position.x, 100, this.body.transform.position.z);
        }
        if (this.body.transform.position.z < -100)
        {
            this.body.transform.position = new Vector3(this.body.transform.position.x, this.body.transform.position.y, 100);
        }
        RaycastHit hit;
        //If you hit, x=-, then there is something underneath you
        //if you hit, y=-, then there is something to your left
        float rotUp = 0;
        float rotRight = 0;
        for (int x = -20; x< 20; x++)
        {
            for (int y = -20; y < 20; y++)
            {
                Vector3 forward = (Quaternion.Euler(x,y,0)) * body.transform.TransformDirection(body.transform.forward) * 50;
                Debug.DrawRay(body.transform.position, forward, Color.red);
                
                if (Physics.Raycast(body.transform.position + body.transform.forward, body.transform.forward, out hit, 50))
                {  
                    if (hit.transform.position != this.body.transform.position)
                    {
                        
                        if (x < 0)
                        {
                            rotRight = 1;
                        }else
                        {
                            rotRight = -1;
                        }
                        if(y<0)
                        {
                            rotUp = 1;
                        }else
                        {
                            rotUp = -1;
                        }
                    }
                    
                }
            }

        }
        Quaternion rotate = Quaternion.Euler(new Vector3(rotUp,rotRight, 0));
        physics.MoveRotation(this.body.transform.rotation * rotate);

    }

}
