using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMovement : MonoBehaviour {
	//Vector3 moveJump = Vector2.zero;
	//float horMove, vertMove;

 //   public Door[] doorUp;

 //   public DoorRight[] doorRight;

	//void Start(){
	//	SheetAssigner SA = FindObjectOfType<SheetAssigner>();
	//	Vector2 tempJump = SA.roomDimensions;
	//	moveJump = new Vector3(tempJump.x, tempJump.y, 0); //distance b/w rooms: to be used for movement
	//}
	//void Update()
	//{
	//	//if (Input.GetKeyDown("w") || Input.GetKeyDown("s") || 
	//	//	Input.GetKeyDown("a") || Input.GetKeyDown("d")) //if any 'wasd' key is pressed
	//	//{
	//	//	horMove = System.Math.Sign(Input.GetAxisRaw("Horizontal"));//capture input
	//	//	vertMove = System.Math.Sign(Input.GetAxisRaw("Vertical"));
	//	//	Vector3 tempPos = transform.position;
	//	//	tempPos += Vector3.right * horMove * moveJump.x; //jump bnetween rooms based opn input
	//	//	tempPos += Vector3.up * vertMove * moveJump.y;
	//	//	transform.position = tempPos;
	//	//}

 //       doorUp = FindObjectsOfType<Door>();

 //       doorRight = FindObjectsOfType<DoorRight>();

 //       foreach (Door door in doorUp)
 //       {
 //           if (door.IsGoingUp == true)
 //           {
 //               vertMove = System.Math.Sign(Input.GetAxisRaw("Vertical"));
 //               Vector3 tempPos = transform.position;
 //               tempPos += Vector3.up * vertMove * moveJump.y;
 //               transform.position = tempPos;
 //           }
 //       }

 //       foreach (DoorRight DoorRight in doorRight)
 //       {
 //           if (DoorRight.IsGoingRight == true)
 //           {
 //               horMove = System.Math.Sign(Input.GetAxisRaw("Horizontal"));//capture input
 //               Vector3 tempPos = transform.position;
 //               tempPos += Vector3.right * moveJump.x; //jump bnetween rooms based opn input
 //               transform.position = tempPos;
 //           }
 //       }
	//}
}
