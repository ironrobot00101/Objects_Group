using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Knight : Piece
{
    
}

public class Bishop : Piece
{

}
public class Rook : Piece
{

}
public class Pawn : Piece
{

}
public class King : Piece
{

}
public class Queen : Piece
{

}


public class Piece : GameManager
{
    public List<string> pieceType = new List<string>();

    public string type;
    public void SetType(string type)
    {

    }
}

public class GameManager
{
    Piece piece = null;
    //pieces to be used
    public List<Piece> piecesUsed = new List<Piece>();

    //multi-dimensional array for 
    public string[,] gameBoard = new string[8, 8];


    public void Start()
    {
        piece = new Piece();
        piece.SetType("Rook");
        piecesUsed.Add(piece);
    }
}

public class TestDataTypes : Piece
{
    public List<int> listTest = new List<int>();

    public List<AudioClip> audioClips = new List<AudioClip>();

    public Piece chessPieces;

    public void TestMyList()
    {
        //add to list
        listTest.Add(0);
        //remove from list
        listTest.Remove(1);

        //clear a list
        listTest.Clear();
    }

    public void PlayChess(List<Piece> pieces)
    {

    }

    
}
public class Customer
{
    Dictionary<string, string> customerQueue = new Dictionary<string, string>();
    public string nameCurrent;
    public string orderCurrent;
    public void TakeOrder()
    {
        //create something to track people arriving in a deli
        //so they can enter their name, and allow people to be served in the order they arrive
        //example name
        nameCurrent = "John";
        orderCurrent = "pastrami sandwich";
        AddToQueue(nameCurrent, orderCurrent);
        nameCurrent = "Mary";
        orderCurrent = "pickle sandwich";
        AddToQueue(nameCurrent, orderCurrent);

    }

    public void AddToQueue(string name, string order)
    {

        customerQueue.Add(name, order);
    }
}
