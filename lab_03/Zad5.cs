using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Zad5 : MonoBehaviour
{
     private int number_of_objects = 10;

     Vector3 generate_vector()
     {
          /*
          Generate vector with random x and z position.
          */
          int x = Random.Range(1, 10);
          int z = Random.Range(1, 10);

          Vector3 new_pos = new Vector3(x, 0.5f, z);

          return new_pos;
     }


     void Start()
     {
          List<Vector3> taken_vectors = new List<Vector3>();
          GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

          // Create <number_of_objects> unique cube objects
          for (int i = 0; i < number_of_objects; i++)
          {
               Vector3 new_pos = generate_vector();
               while (taken_vectors.Contains(new_pos))
               {
                    new_pos = generate_vector();
               }

               Instantiate(cube, new_pos, Quaternion.identity);

               taken_vectors.Add(new_pos);
          }
     }
}
