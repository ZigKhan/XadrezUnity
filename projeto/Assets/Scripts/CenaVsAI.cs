using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CenaVsAI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BoardManager.Instance.SpawnAllChessman();
    }

    // Update is called once per frame
    void Update()
    {
        BoardManager.Instance.UpdateSelection();
        BoardManager.Instance.DrawChessBoard();
        //BoardManager.Instance.CheckAttackedSquares();

        if (!BoardManager.Instance.temAI)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //se o clique for dentro do tabuleiro
                if (BoardManager.Instance.selectionX >= 0 && BoardManager.Instance.selectionY >= 0)
                {
                    //se nenhuma peça tiver sido selecionada o clique seleciona else move a peça selecionada
                    if (BoardManager.Instance.selectedPeça == null)
                    {
                        BoardHighlight.Instance.HidehighLights();
                        //selecionar a peça
                        BoardManager.Instance.SelecionePeça(BoardManager.Instance.selectionX, BoardManager.Instance.selectionY);
                    }
                    else
                    {
                        BoardHighlight.Instance.HidehighLights();
                        Vector3 a = BoardManager.Instance.GetTileCenter(BoardManager.Instance.selectionX, BoardManager.Instance.selectionY);
                        if (BoardManager.Instance.selectedPeça.CurrentX != a.x && BoardManager.Instance.selectedPeça.CurrentY != a.z)
                        {
                            BoardManager.Instance.MovePeça(BoardManager.Instance.selectionX, BoardManager.Instance.selectionY);
                        }
                        //mova a peça
                        //MovePeça(selectionX, selectionY);
                    }




                }
            }
        }
        else if (BoardManager.Instance.temAI && BoardManager.Instance.isWhiteTurn)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //se o clique for dentro do tabuleiro
                if (BoardManager.Instance.selectionX >= 0 && BoardManager.Instance.selectionY >= 0)
                {
                    //se nenhuma peça tiver sido selecionada o clique seleciona else move a peça selecionada
                    if (BoardManager.Instance.selectedPeça == null)
                    {
                        BoardHighlight.Instance.HidehighLights();
                        //selecionar a peça
                        BoardManager.Instance.SelecionePeça(BoardManager.Instance.selectionX, BoardManager.Instance.selectionY);
                    }
                    else
                    {
                        BoardHighlight.Instance.HidehighLights();
                        Vector3 a = BoardManager.Instance.GetTileCenter(BoardManager.Instance.selectionX, BoardManager.Instance.selectionY);
                        if (BoardManager.Instance.selectedPeça.CurrentX != a.x && BoardManager.Instance.selectedPeça.CurrentY != a.z)
                        {
                            BoardManager.Instance.MovePeça(BoardManager.Instance.selectionX, BoardManager.Instance.selectionY);
                        }
                        //mova a peça
                        //MovePeça(selectionX, selectionY);
                    }




                }
            }
        }
        else if (BoardManager.Instance.temAI && !BoardManager.Instance.isWhiteTurn)
        {
            BoardManager.Instance.alfa.Clear();
            BoardManager.Instance.beta.Clear();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (BoardManager.Instance.Chessmans[i, j] != null)
                    {
                        if (!BoardManager.Instance.Chessmans[i, j].isWhite)
                        {
                            PeçaDefault referencia = BoardManager.Instance.Chessmans[i, j];
                            AI.Instance.guardaMoves(BoardManager.Instance.alfa, referencia);
                        }
                        else if (BoardManager.Instance.Chessmans[i, j].isWhite)
                        {
                            PeçaDefault referencia = BoardManager.Instance.Chessmans[i, j];
                            AI.Instance.guardaMoves(BoardManager.Instance.beta, referencia);
                        }
                    }
                }
            }
            /*int sizeOfList = 0;
            int gerarNumero;
            sizeOfList = alfa.Count;
            gerarNumero = Random.Range(1, sizeOfList);
            
            Debug.Log(gerarNumero);
            Debug.Log(alfa[gerarNumero].x);
            */
            Moves teste = AI.Instance.evaluateMove(BoardManager.Instance.alfa, BoardManager.Instance.beta);
            BoardManager.Instance.SelecionePeça(teste.peçaDoMove.CurrentX, teste.peçaDoMove.CurrentY);
            BoardManager.Instance.MovePeça(teste.x, teste.y);




        }
        if (BoardManager.Instance.capturedKing == true)
        {
            BoardManager.Instance.capturedKing = false;
            SceneManager.LoadScene("Menu");
        }


        //Debug.Log(BoardManager.Instance.Chessmans[0, 0].neverMoved);
        //Debug.Log(BoardManager.Instance.Chessmans[4, 0].neverMoved);
    }
}
